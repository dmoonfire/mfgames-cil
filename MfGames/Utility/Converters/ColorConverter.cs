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
using System.Drawing;

#endregion

namespace MfGames.Utility.Converters
{
	public class ColorConverter : IExtendedConverter
	{
		#region IExtendedConverter Members

		/// <summary>
		/// Converts the specified value to the given type and returns it.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="convertToType">Type of the convert to.</param>
		/// <returns></returns>
		public object Convert(object value, Type convertToType)
		{
			// Check the type being converted to.
			if (convertToType == typeof(string))
			{
				var color = (Color) value;
				return string.Format("#{0}{1}{2}{3}", ToHex(color.A), ToHex(color.R), ToHex(color.G), ToHex(color.B));
			}

			if (convertToType == typeof(Color))
			{
				return ColorTranslator.FromHtml((string) value);
			}

			return null;
		}

		/// <summary>
		/// Converts the given byte into a two-digit hex string.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		private static string ToHex(byte value)
		{
			return value <= 0xF ? String.Format("0{0:X}", value) : value.ToString("X");
		}

		#endregion
	}
}