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
	using System.Collections;
	using System.IO;
	using System.Text.RegularExpressions;

	/// <summary>
	/// Implements a full-featured version parsing and comparison
	/// class. This, like String, is an immutable class. Any methods
	/// that would adjust a version return a new one.
	/// </summary>
	[Serializable]
	public class Version : Logable
	{
#region Constants
		// Contains the regex for whitespace matching
		private static readonly Regex regexSpace = new Regex(@"\s");

		// Contains the simple matcher for string (number, followed
		// by... stuff)
		private static readonly Regex regexPart = new Regex(@"(\d+)([\d\w]*)");
#endregion

#region Constructors and Construction
		/// <summary>
		/// Constructs an empty version with a version of zero ("0").
		/// </summary>
		public Version()
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
		public Version(string version)
		{
			// Check for null and blank
			if (version == null || version.Trim() == "")
			{
				throw new UtilityException("Cannot parse a null or blank version");
			}

			// Save the string version and remove the spaces
			this.version = version = version.Trim();

			// Check for spaces
			if (regexSpace.IsMatch(version))
			{
				throw new UtilityException("Versions cannot have whitespace");
			}

			// Split the version into parts. We also allocate the space for
			// everything before parsing.
			string [] parts = version.Split('.', '-');
			numerics = new int [parts.Length];
			strings = new string [parts.Length];

			for (int i = 0; i < parts.Length; i++)
			{
				// Check for match and sanity checking
				if (!regexPart.IsMatch(parts[i]))
					throw new UtilityException("Cannot parse part '" + parts[i]
						+ "' of '" + version + "'");

				// Pull out the parts
				Match match = regexPart.Match(parts[i]);
				string strNumber = match.Groups[1].Value;
				strings[i] = match.Groups[2].Value;

				try
				{
					// Try to parse the integer
					numerics[i] = Int32.Parse(strNumber);
				}
				catch (Exception e)
				{
					throw new UtilityException("Cannot numerically parse '"
						+ parts[i] + "'");
				}
			}
		}
#endregion

#region Properties
		// Contains the string version
		private string version = null;

		// Contains the numeric parts of the version
		private int [] numerics = null;

		// Contains the string parts of the version
		private string [] strings = null;
#endregion
    
#region Operators
		/// <summary>
		/// Determines if the two versions are syntactically equal. If all
		/// the version parts are identical, then so is the entire version.
		/// </summary>
		public static bool operator ==(Version v1, Version v2)
		{
			return v1.ToString() == v2.ToString();
		}
    
		/// <summary>
		/// Determines if the two versions are syntactically equal. If all
		/// the version parts are identical, then so is the entire version.
		/// </summary>
		public static bool operator !=(Version v1, Version v2)
		{
			return v1.ToString() != v2.ToString();
		}
    
		/// <summary>
		/// Determines if the first is less than the second one. There are
		/// some conditions where a version is neither less than or greater
		/// than another version, specifcally with version parts that have
		/// text in it.
		/// </summary>
		public static bool operator <(Version v1, Version v2)
		{
			// Make sure v1 has the less parts
			if (v1.numerics.Length > v2.numerics.Length)
			{
				Version v3 = v2;
				v2 = v1;
				v1 = v3;
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
					return false;

				// Compare the numbers. If num1 is less than num2, then the
				// rest of the version will be less.
				if (num1 < num2)
					return true;
			}

			// We never got something that explicitly was less or invalid,
			// so assume false (equals).
			return false;
		}
    
		/// <summary>
		/// Determines if the first version is less than or equal to
		/// the second version. See the &lt; operator for more conditions.
		/// </summary>
		public static bool operator <=(Version v1, Version v2)
		{
			// Just do the reverse, its easier
			return v1 == v2 || v1 < v2;
		}

		/// <summary>
		/// Determines if the first version is greater than the second
		/// version. See the &lt; operator for more conditions.
		/// </summary>
		public static bool operator >(Version v1, Version v2)
		{
			// Just do the reverse, its easier
			return v2 < v1;
		}

		/// <summary>
		/// Determines if the first version is greater than or equal to
		/// the second version. See the &lt; operator for more conditions.
		/// </summary>
		public static bool operator >=(Version v1, Version v2)
		{
			// Just do the reverse, its easier
			return v1 == v2 || v2 < v1;
		}

		/// <summary>
		/// A Debian-like parsing of version numbers that encodes the
		/// operation into the string. For example, "&gt; 2.3.4" would be
		/// true if the Version object was 2.3.5 but not 2.3.3 or 2.3.4.
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
				Alert("Cannot find version for operation: " + operation);
				return false;
			}

			// Check the op
			Version v = new Version(ver);
      
			switch (op)
			{
			case "<": return this < v;
			case "<=": return this <= v;
			case "=": return this == v;
			case ">": return this > v;
			case ">=": return this >= v;
			default:
				throw new UtilityException("Cannot identify operation: " + op);
			}
		}
#endregion
    
#region Common Object Methods
		/// <summary>
		/// Determines if the object is equal to the current one. In cases
		/// where the object is not a Version class, it returns false.
		/// </summary>
		public override bool Equals(object obj)
		{
			try
			{
				Version v = obj as Version;
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
