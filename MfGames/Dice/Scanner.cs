#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System;
using System.Collections;
using System.IO;

#endregion

namespace MfGames.Utility.Dice
{
	public class Token
	{
		public int col; // token column (starting at 0)
		public int kind; // token kind
		public int line; // token line (starting at 1)
		public Token next; // ML 2005-03-11 Tokens are kept in linked list
		public int pos; // token position in the source text (starting at 0)
		public string val; // token value
	}

	public class Buffer
	{
		public const char EOF = (char) 256;
		private const int MAX_BUFFER_LENGTH = 64 * 1024; // 64KB
		private byte[] buf; // input buffer

		private int bufLen; // length of buffer

		private int bufStart;
		            // position of first byte in buffer relative to input stream

		private int fileLen; // length of input stream
		private bool isUserStream; // was the stream opened by the user?
		private int pos; // current position in buffer
		private Stream stream; // input stream (seekable)

		public Buffer(Stream s, bool isUserStream)
		{
			stream = s;
			this.isUserStream = isUserStream;
			fileLen = bufLen = (int) s.Length;

			if (stream.CanSeek && bufLen > MAX_BUFFER_LENGTH)
				bufLen = MAX_BUFFER_LENGTH;

			buf = new byte[bufLen];
			bufStart = Int32.MaxValue; // nothing in the buffer so far
			Pos = 0; // setup buffer to position 0 (start)

			if (bufLen == fileLen)
				Close();
		}

		public int Pos
		{
			get { return pos + bufStart; }
			set
			{
				if (value < 0)
					value = 0;
				else if (value > fileLen)
					value = fileLen;

				if (value >= bufStart && value < bufStart + bufLen)
				{
					// already in buffer
					pos = value - bufStart;
				}
				else if (stream != null)
				{
					// must be swapped in
					stream.Seek(value, SeekOrigin.Begin);
					bufLen = stream.Read(buf, 0, buf.Length);
					bufStart = value;
					pos = 0;
				}
				else
				{
					pos = fileLen - bufStart; // make Pos return fileLen
				}
			}
		}

		~Buffer()
		{
			Close();
		}

		private void Close()
		{
			if (!isUserStream && stream != null)
			{
				stream.Close();
				stream = null;
			}
		}

		public int Read()
		{
			if (pos < bufLen)
			{
				return buf[pos++];
			}
			else if (Pos < fileLen)
			{
				Pos = Pos; // shift buffer start to Pos
				return buf[pos++];
			}
			else
			{
				return EOF;
			}
		}

		public int Peek()
		{
			if (pos < bufLen)
			{
				return buf[pos];
			}
			else if (Pos < fileLen)
			{
				Pos = Pos; // shift buffer start to Pos
				return buf[pos];
			}
			else
			{
				return EOF;
			}
		}

		public string GetString(int beg, int end)
		{
			int len = end - beg;
			var buf = new char[len];
			int oldPos = Pos;
			Pos = beg;
			for (int i = 0; i < len; i++)
				buf[i] = (char) Read();
			Pos = oldPos;
			return new String(buf);
		}
	}

	public class Scanner
	{
		private const int charSetSize = 256;
		private const int eofSym = 0; /* pdt */
		private const char EOL = '\n';
		private const int maxT = 5;
		private const int noSym = 5;

		public Buffer buffer; // scanner buffer

		private char ch; // current input character
		public string Format;
		private BitArray ignore; // set of characters to be ignored by the scanner
		private int line; // line number of current character
		private int lineStart; // start position of current line
		private int oldEols; // EOLs that appeared in a comment;
		private int pos; // column number of current character

		private Token pt; // current peek token

		private short[] start = {
		                        	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		                        	, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		                        	0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 4, 0, 0, 2, 2, 2, 2, 2
		                        	, 2, 2, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0,
		                        	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		                        	, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0,
		                        	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		                        	, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		                        	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		                        	, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		                        	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		                        	, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		                        	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
		                        	, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		                        	0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, -1
		                        };

		private Token t; // current token

		private int tlen; // length of current token

		private Token tokens;
		              // list of tokens already peeked (first token is a dummy)

		private char[] tval = new char[128]; // text of current token

		public Scanner(string fileName)
		{
			try
			{
				Stream stream = new FileStream(fileName,
				                               FileMode.Open,
				                               FileAccess.Read,
				                               FileShare.Read);
				buffer = new Buffer(stream, false);
				Init();
			}
			catch (IOException)
			{
				Console.WriteLine("--- Cannot open file {0}", fileName);
				Environment.Exit(1);
			}
		}

		public Scanner(Stream s)
		{
			buffer = new Buffer(s, true);
			Init();
		}

		private void Init()
		{
			pos = -1;
			line = 1;
			lineStart = 0;
			oldEols = 0;
			NextCh();
			ignore = new BitArray(charSetSize + 1);
			ignore[' '] = true; // blanks are always white space
			ignore[9] = true;
			ignore[10] = true;
			ignore[13] = true;
			pt = tokens = new Token(); // first token is a dummy
		}

		private void NextCh()
		{
			if (oldEols > 0)
			{
				ch = EOL;
				oldEols--;
			}
			else
			{
				ch = (char) buffer.Read();
				pos++;

				// replace isolated '\r' by '\n' in order to make
				// eol handling uniform across Windows, Unix and Mac
				if (ch == '\r' && buffer.Peek() != '\n')
					ch = EOL;

				if (ch == EOL)
				{
					line++;
					lineStart = pos + 1;
				}
			}
		}

		private void AddCh()
		{
			if (tlen >= tval.Length)
			{
				var newBuf = new char[2 * tval.Length];
				Array.Copy(tval, 0, newBuf, 0, tval.Length);
				tval = newBuf;
			}

			tval[tlen++] = ch;
			NextCh();
		}

		protected void CheckLiteral()
		{
			switch (t.val)
			{
			default:
				break;
			}
		}

		private Token NextToken()
		{
			while (ignore[ch])
				NextCh();

			t = new Token();
			t.pos = pos;
			t.col = pos - lineStart + 1;
			t.line = line;
			int state = start[ch];
			tlen = 0;
			AddCh();

			switch (state)
			{
			case -1:
			{
				t.kind = eofSym;
				break;
			} // NextCh already done
			case 0:
			{
				t.kind = noSym;
				break;
			} // NextCh already done
			case 1:
			{
				t.kind = 1;
				break;
			}
			case 2:

				if ((ch >= '0' && ch <= '9'))
				{
					AddCh();
					goto case 2;
				}
				else
				{
					t.kind = 2;
					break;
				}

			case 3:
			{
				t.kind = 3;
				break;
			}
			case 4:
			{
				t.kind = 4;
				break;
			}
			}
			t.val = new String(tval, 0, tlen);
			return t;
		}

		// get the next token (possibly a token already seen during peeking)
		public Token Scan()
		{
			if (tokens.next == null)
			{
				return NextToken();
			}
			else
			{
				pt = tokens = tokens.next;
				return tokens;
			}
		}

		// peek for the next token, ignore pragmas
		public Token Peek()
		{
			if (pt.next == null)
			{
				do
				{
					pt = pt.next = NextToken();
				}
				while (pt.kind > maxT); // skip pragmas
			}
			else
			{
				do
				{
					pt = pt.next;
				}
				while (pt.kind > maxT);
			}

			return pt;
		}

		// make sure that peeking starts at the current scan position
		public void ResetPeek()
		{
			pt = tokens;
		}
	} // end Scanner
}