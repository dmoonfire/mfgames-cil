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

using MfGames.Logging;

#endregion

namespace MfGames.Entropy
{
	public class Parser
	{
		private const int _DICE = 1;
		private const int _EOF = 0;
		private const int _MINUS = 4;
		private const int _NUMBER = 2;
		private const int _PLUS = 3;
		private const int maxT = 5;
		private const int minErrDist = 2;

		private const bool T = true;
		private const bool x = false;

		private IDice dice = null;

		private int errDist = minErrDist;
		private Errors errors;
		public Token la; // lookahead token
		public Scanner scanner;
		private bool[,] set = { { T, x, x, x, x, x, x } };
		public Token token; // last recognized token

		public Parser(Scanner scanner)
		{
			this.scanner = scanner;
			errors = new Errors();
			errors.Format = scanner.Format;
		}

		public Errors Errors
		{
			get { return errors; }
		}

		private void SynErr(int n)
		{
			if (errDist >= minErrDist)
				errors.SynErr(la.line, la.col, n);

			errDist = 0;
		}

		public void SemErr(string msg)
		{
			if (errDist >= minErrDist)
				errors.Error(token.line, token.col, msg);

			errDist = 0;
		}

		private void Get()
		{
			for (;;)
			{
				token = la;
				la = scanner.Scan();

				if (la.kind <= maxT)
				{
					++errDist;
					break;
				}

				la = token;
			}
		}

		protected void Expect(int n)
		{
			if (la.kind == n)
				Get();
			else
			{
				SynErr(n);
			}
		}

		protected bool StartOf(int s)
		{
			return set[s, la.kind];
		}

		private void Dice()
		{
			Addition(out dice);
		}

		private void Addition(out IDice dice)
		{
			IDice d1, d2 = null;
			Subtraction(out d1);

			if (la.kind == 3)
			{
				Get();
				Addition(out d2);
			}

			if (d2 == null)
				dice = d1;
			else
				dice = new AdditionDice(d1, d2);
		}

		private void Subtraction(out IDice dice)
		{
			IDice d1, d2 = null;
			Expression(out d1);

			if (la.kind == 4)
			{
				Get();
				Subtraction(out d2);
			}

			if (d2 == null)
				dice = d1;
			else
				dice = new SubtractionDice(d1, d2);
		}

		private void Expression(out IDice dice)
		{
			int number = 0;
			int sides = 0;
			Integer(out number);

			if (la.kind == 1)
			{
				Get();
				Integer(out sides);
			}

			if (sides == 0)
				dice = new ConstantDice(number);
			else
				dice = new RandomDice(number, sides);
		}

		private void Integer(out int value)
		{
			Expect(2);
			value = Int32.Parse(token.val);
		}

		public IDice Parse()
		{
			la = new Token();
			la.val = "";
			Get();
			Dice();

			Expect(0);
			return dice;
		}
	}

	public class Errors : Logable
	{
		public int Count = 0; // number of errors detected
		public string errMsgFormat = "{3} ({0},{1}): {2}"; // 0=line, 1=column, 2=text
		public string Format = null;

		public void SynErr(int line, int col, int n)
		{
			string s;
			switch (n)
			{
			case 0:
				s = "EOF expected";
				break;
			case 1:
				s = "DICE expected";
				break;
			case 2:
				s = "NUMBER expected";
				break;
			case 3:
				s = "PLUS expected";
				break;
			case 4:
				s = "MINUS expected";
				break;
			case 5:
				s = "??? expected";
				break;

			default:
				s = "error " + n;
				break;
			}
			Alert(errMsgFormat, line, col, s, Format);
			Count++;
		}

		public void SemErr(int line, int col, int n)
		{
			Alert(errMsgFormat, line, col, ("error " + n), Format);
			Count++;
		}

		public void Error(int line, int col, string s)
		{
			Alert(errMsgFormat, line, col, s, Format);
			Count++;
		}

		public void Exception(string s)
		{
			/*
			        Console.WriteLine(s);
			        System.Environment.Exit(1);
			 */
		}
	}
}