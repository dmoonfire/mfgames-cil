#region Copyright and License

// Copyright (c) 2005-2011, Moonfire Games
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

using System.IO;
using System.Text;

#endregion

namespace MfGames.Entropy
{
	/// <summary>
	/// Factory for generating dice values from a given string. This
	/// calls the public grammar for parsing and throws an exception
	/// if it cannot parse it.
	/// </summary>
	public class DiceFactory
	{
		/// <summary>
		/// Since this is a static singleton, hide the constructor.
		/// </summary>
		private DiceFactory()
		{
		}

		/// <summary>
		/// Parses the given format and returns the IDice object that
		/// represents that data or throws a DiceException if it
		/// cannot be parsed.
		/// </summary>
		public static IDice Parse(string format)
		{
			// Create the scanner and parser
			byte[] bytes = Encoding.UTF8.GetBytes(format);
			var stream = new MemoryStream(bytes);
			var scanner = new Scanner(stream);
			scanner.Format = format;
			var parser = new Parser(scanner);

			// Parse the results
			IDice dice = parser.Parse();

			// Check for errors
			if (parser.Errors.Count > 0)
			{
				throw new DiceException("Cannot parse: " + format);
			}

			// Return the dice
			return dice;
		}
	}
}