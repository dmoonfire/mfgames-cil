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
using System.Security.Cryptography;
using System.Text;

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// Contains various useful string conversion and hash methods. All
	/// of the methods in this class are static.
	/// </summary>
	public abstract class ExtendedConvert
	{
		#region String Functions

		private static readonly char[] HexDigits = {
		                                           	'0', '1', '2', '3', '4', '5', '6'
		                                           	, '7', '8', '9', 'a', 'b', 'c',
		                                           	'd', 'e', 'f'
		                                           };

		/// <summary>
		/// Generates a hex string from a give set of bytes. This code
		/// came from the Microsoft site.
		/// </summary>
		public static string ToHexString(byte[] input)
		{
			// Allocate the space
			var chars = new char[input.Length * 2];

			// Go through and convert all of them
			for (int i = 0; i < input.Length; i++)
			{
				int b = input[i];
				chars[i * 2] = HexDigits[b >> 4];
				chars[i * 2 + 1] = HexDigits[b & 0xF];
			}

			// Convert to a string and return
			return new string(chars);
		}

		/// <summary>
		/// Generates a MD5 hash, using only hex characters, in lower case.
		/// </summary>
		public static string ToMd5HexString(string input)
		{
			// Convert into bytes. Don't use UnicodeEncoding here, it break
			// compatibility with MySQL and Linux's md5 stuff (learned the hard
			// way).
			var ue = new UTF8Encoding();
			byte[] input2 = ue.GetBytes(input);

			// First encrypt it
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] hash = md5.ComputeHash(input2);
			return ToHexString(hash);
		}

		/// <summary>
		/// Generates a MD5 hash which includes some additional characters
		/// in the uppper ranges.
		/// </summary>
		public static string ToMd5String(string input)
		{
			// Convert into bytes
			var ue = new UTF8Encoding();
			byte[] input2 = ue.GetBytes(input);

			// First encrypt it
			MD5 md5 = new MD5CryptoServiceProvider();
			byte[] hash = md5.ComputeHash(input2);
			return Convert.ToBase64String(hash);
		}

		#endregion
	}
}