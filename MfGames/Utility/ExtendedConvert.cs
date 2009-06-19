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
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

using MfGames.Utility.Annotations;
using MfGames.Utility.Converters;

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// Contains various useful string conversion and hash methods. All
	/// of the methods in this class are static.
	/// </summary>
	public static class ExtendedConvert
	{
		#region Initialization

		/// <summary>
		/// Initializes the <see cref="ExtendedConvert"/> class.
		/// </summary>
		static ExtendedConvert()
		{
			// Register the common converters used.
			IExtendedConverter converter = new ColorConverter();
			RegisterConverter(typeof(System.Drawing.Color), typeof(string), converter);
			RegisterConverter(typeof(string), typeof(System.Drawing.Color), converter);
		}

		#endregion

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

		#region Type Conversions

		private static Dictionary<Type, Dictionary<Type, IExtendedConverter>> converters = new Dictionary<Type, Dictionary<Type, IExtendedConverter>>();

		/// <summary>
		/// Changes the given value to something that matches the converted type.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="convertType">Type of the convert.</param>
		/// <returns></returns>
		public static object ChangeType(object value, Type convertType)
		{
			// Check our custom converters first.
			if (converters.ContainsKey(value.GetType()))
			{
				Dictionary<Type, IExtendedConverter> conversions = converters[value.GetType()];

				if (conversions.ContainsKey(convertType))
				{
					return conversions[convertType].Convert(value, convertType);
				}
			}

			// Failing everything else, fall back to the default converter.
			return Convert.ChangeType(value, convertType);
		}

		/// <summary>
		/// Registers the converter to change a value from fromType to toType. If there is a converter already registered,
		/// it will be replaced by this one.
		/// </summary>
		/// <param name="fromType">From type.</param>
		/// <param name="toType">To type.</param>
		/// <param name="converter">The converter.</param>
		public static void RegisterConverter([NotNull] Type fromType, [NotNull] Type toType, [NotNull] IExtendedConverter converter)
		{
			// Check for null values in any of the paramters.
			if (fromType == null)
			{
				throw new ArgumentNullException("fromType");
			}

			if (toType == null)
			{
				throw new ArgumentNullException("toType");
			}

			if (converter == null)
			{
				throw new ArgumentNullException("converter");
			}

			// Get or create the from type.
			if (!converters.ContainsKey(fromType))
			{
				converters.Add(fromType, new Dictionary<Type, IExtendedConverter>());
			}

			Dictionary<Type, IExtendedConverter> fromConverters = converters[fromType];

			// Get or create the to type and set the converter to that key.
			if (!fromConverters.ContainsKey(toType))
			{
				fromConverters.Add(toType, converter);
			}
			else
			{
				fromConverters[toType] = converter;
			}
		}

		/// <summary>
		/// Converts the given object into a string representation.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string ToString(object value)
		{
			return (string) ChangeType(value, typeof(string));
		}

		#endregion
	}

	/// <summary>
	/// Generics-based implementation of the extended converter.
	/// </summary>
	/// <typeparam name="TFrom">The type of from.</typeparam>
	/// <typeparam name="TTo">The type of to.</typeparam>
	public static class ExtendedConvert<TFrom, TTo>
	{
		/// <summary>
		/// Changes the type from the given value to the type specified by the interface.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static TTo ChangeType(object value)
		{
			return (TTo) ExtendedConvert.ChangeType(value, typeof(TTo));
		}
	}
}