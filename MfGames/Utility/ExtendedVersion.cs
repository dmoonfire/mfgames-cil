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
using System.Text.RegularExpressions;

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// Implements a full-featured version parsing and comparison class. This, like 
	/// <see cref="string">String</see> is an immutable class. Any methods that would 
	/// somehow change a field instead return a new ExtendedVersion object.
	/// </summary>
	[Serializable]
	public class ExtendedVersion
	{
		#region Constants

		// Contains the regex for whitespace matching

		// Contains the simple matcher for string (number, followed
		// by... stuff)
		private static readonly Regex regexPart = new Regex(@"(\d+)([\d\w]*)");
		private static readonly Regex regexSpace = new Regex(@"\s");

		#endregion

		#region Constructors and Construction

		/// <summary>
		/// Constructs an empty version with a version of zero ("0").
		/// </summary>
		public ExtendedVersion()
			: this("0")
		{
		}

		/// <summary>
		/// Constructs a version using the given string as the
		/// version. This breaks up the version into version parts (broken
		/// down by periods (".") and slashes ("-"). A version part
		/// consists of a number, followed optionally by a string. If the
		/// version cannot be parsed, it throws an MfGamesException.
		/// </summary>
		public ExtendedVersion(string version)
		{
			// Check for null and blank
			if (version == null || version.Trim() == "")
			{
				throw new Exception("Cannot parse a null or blank version");
			}

			// Save the string version and remove the spaces
			this.version = version = version.Trim();

			// Check for spaces
			if (regexSpace.IsMatch(version))
			{
				throw new Exception("Versions cannot have whitespace");
			}

			// Split the version into parts. We also allocate the space for
			// everything before parsing.
			string[] parts = version.Split('.', '-');
			numerics = new int[parts.Length];
			strings = new string[parts.Length];

			for (int i = 0; i < parts.Length; i++)
			{
				// Check for match and sanity checking
				if (!regexPart.IsMatch(parts[i]))
					throw new Exception("Cannot parse part '" + parts[i] + "' of '" +
					                           version + "'");

				// Pull out the parts
				Match match = regexPart.Match(parts[i]);
				string strNumber = match.Groups[1].Value;
				strings[i] = match.Groups[2].Value;

				try
				{
					// Try to parse the integer
					numerics[i] = Int32.Parse(strNumber);
				}
				catch
				{
					throw new Exception("Cannot numerically parse '" + parts[i] + "'");
				}
			}
		}

		#endregion

		#region Properties

		// Contains the string version

		// Contains the numeric parts of the version
		private int[] numerics = null;

		// Contains the string parts of the version
		private string[] strings = null;
		private string version = null;

		#endregion

		#region Operators

		/// <summary>
		/// Determines if the two versions are syntactically equal. If all
		/// the version parts are identical, then so is the entire version.
		/// </summary>
		public static bool operator ==(ExtendedVersion v1, ExtendedVersion v2)
		{
			return v1.ToString() == v2.ToString();
		}

		/// <summary>
		/// Determines if the two versions are syntactically equal. If all
		/// the version parts are identical, then so is the entire version.
		/// </summary>
		public static bool operator !=(ExtendedVersion v1, ExtendedVersion v2)
		{
			return v1.ToString() != v2.ToString();
		}

		/// <summary>
		/// Determines if the first is less than the second one. There are
		/// some conditions where a version is neither less than or greater
		/// than another version, specifcally with version parts that have
		/// text in it.
		/// </summary>
		public static bool operator <(ExtendedVersion v1, ExtendedVersion v2)
		{
			// Make sure v1 has the less parts, for simplicicity.
			bool swapped = false;

			if (v1.numerics.Length > v2.numerics.Length)
			{
				ExtendedVersion v3 = v2;
				v2 = v1;
				v1 = v3;
				swapped = true;
			}

			// Go through the various parts
			for (int i = 0; i < v1.numerics.Length; i++)
			{
				// Get the parts
				int num1 = v1.numerics[i];
				string str1 = v1.strings[i];
				int num2 = v2.numerics[i];
				string str2 = v2.strings[i];

				// Make sure strings match. If they do not, then the versions
				// will never match.
				if (str1 != str2)
					return swapped ? true : false;

				// Compare the numbers. If num1 is less than num2, then the
				// rest of the version will be less. If it is the reverse,
				// return it.
				if (num1 < num2)
					return swapped ? false : true;

				if (num1 > num2)
					return swapped ? true : false;
			}

			// We never got something that explicitly was less or invalid,
			// so assume false (equals).
			return swapped ? true : false;
		}

		/// <summary>
		/// Determines if the first version is less than or equal to
		/// the second version. See the &lt; operator for more conditions.
		/// </summary>
		public static bool operator <=(ExtendedVersion v1, ExtendedVersion v2)
		{
			// Just do the reverse, its easier
			return v1 == v2 || v1 < v2;
		}

		/// <summary>
		/// Determines if the first version is greater than the second
		/// version. See the &lt; operator for more conditions.
		/// </summary>
		public static bool operator >(ExtendedVersion v1, ExtendedVersion v2)
		{
			// Just do the reverse, its easier
			return v2 < v1;
		}

		/// <summary>
		/// Determines if the first version is greater than or equal to
		/// the second version. See the &lt; operator for more conditions.
		/// </summary>
		public static bool operator >=(ExtendedVersion v1, ExtendedVersion v2)
		{
			// Just do the reverse, its easier
			return v1 == v2 || v2 < v1;
		}

		/// <summary>
		/// A Debian-like parsing of version numbers that encodes the
		/// operation into the string. For example, "&gt; 2.3.4" would be
		/// true if the ExtendedVersion object was 2.3.5 but not 2.3.3 or 2.3.4.
		///
		/// The following operations are allowed:
		///   "&lt;", "&lt;=", "=", "&gt;", "&gt;="
		///
		/// There may be any number of spaces between the op and the version.
		/// </summary>
		public bool CompareOp(string operation)
		{
			// Pull out the parts.
			// DREM 2005-05-10 This is sloppy because I had a regex
			// brainfart and can't figure it out.
			string op = null;
			string ver = null;

			if (operation.StartsWith(">="))
			{
				op = ">=";
				ver = operation.Substring(2).Trim();
			}
			else if (operation.StartsWith("<="))
			{
				op = "<=";
				ver = operation.Substring(2).Trim();
			}
			else if (operation.StartsWith("<"))
			{
				op = "<";
				ver = operation.Substring(1).Trim();
			}
			else if (operation.StartsWith(">"))
			{
				op = ">";
				ver = operation.Substring(1).Trim();
			}
			else if (operation.StartsWith("="))
			{
				op = "=";
				ver = operation.Substring(1).Trim();
			}
			else
			{
				return false;
			}

			// Check the op
			var v = new ExtendedVersion(ver);

			switch (op)
			{
			case "<":
				return this < v;
			case "<=":
				return this <= v;
			case "=":
				return this == v;
			case ">":
				return this > v;
			case ">=":
				return this >= v;
			default:
				throw new Exception("Cannot identify operation: " + op);
			}
		}

		#endregion

		#region Common Object Methods

		/// <summary>
		/// Determines if the object is equal to the current one. In cases
		/// where the object is not a ExtendedVersion class, it returns false.
		/// </summary>
		public override bool Equals(object obj)
		{
			try
			{
				var v = obj as ExtendedVersion;
				return this == v;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Overrides the hash code for the version, which is based on all
		/// the version parts.
		/// </summary>
		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}

		/// <summary>
		/// Returns the text version of the string.
		/// </summary>
		public override string ToString()
		{
			return version;
		}

		#endregion
	}
}