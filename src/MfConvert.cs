/*
 * Copyright (C) 2005, Moonfire Games
 *
 * This file is part of MfGames.Utility.
 *
 * The MfGames.Utility library is free software; you can redistribute
 * it and/or modify it under the terms of the GNU Lesser General
 * Public License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307
 * USA
 */

namespace MfGames.Utility
{
	using System;
	using System.Security.Cryptography;
	using System.Text;

	/// <summary>
	/// Contains various useful string conversion and hash methods. All
	/// of the methods in this class are static.
	/// </summary>
	public abstract class MfConvert
	{
#region String Functions
		static char[] HEX_DIGITS = {
			'0', '1', '2', '3', '4', '5', '6', '7',
			'8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};

		/// <summary>
		/// Generates a hex string from a give set of bytes. This code
		/// came from the Microsoft site.
		/// </summary>
		public static string ToHexString(byte [] input)
		{
			// Allocate the space
			char[] chars = new char[input.Length * 2];

			// Go through and convert all of them
			for (int i = 0; i < input.Length; i++)
			{
				int b = input[i];
				chars[i * 2] = HEX_DIGITS[b >> 4];
				chars[i * 2 + 1] = HEX_DIGITS[b & 0xF];
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
			UTF8Encoding ue = new UTF8Encoding();
			byte [] input2 = ue.GetBytes(input);
      
			// First encrypt it
			MD5 md5 = new MD5CryptoServiceProvider();
			byte [] hash = md5.ComputeHash(input2);
			return ToHexString(hash);
		}

		/// <summary>
		/// Generates a MD5 hash which includes some additional characters
		/// in the uppper ranges.
		/// </summary>
		public static string ToMd5String(string input)
		{
			// Convert into bytes
			UTF8Encoding ue = new UTF8Encoding();
			byte [] input2 = ue.GetBytes(input);
      
			// First encrypt it
			MD5 md5 = new MD5CryptoServiceProvider();
			byte [] hash = md5.ComputeHash(input2);
			return Convert.ToBase64String(hash);
		}
#endregion
	}
}
