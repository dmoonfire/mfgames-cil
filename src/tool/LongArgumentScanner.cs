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
	using System.Reflection;
	using System.Text.RegularExpressions;

	/// <summary>
	/// Describes a long command line argument format. This handles the
	/// processing for LongArgumentAttribute objects.
	/// </summary>
	public class LongArgumentScanner : ArgumentScanner
	{
#region Static Prebuilts
		/// <summary>
		/// Contains a scanner that handles "--" arguments with a
		/// equal("=") between the variables and a comma for lists.
		/// </summary>
		public static readonly LongArgumentScanner DoubleDash =
			new LongArgumentScanner("^--", "=", ",");
#endregion

#region Constructors
		public LongArgumentScanner(string prefix)
			: this(prefix, null)
		{
		}

		public LongArgumentScanner(string prefix, string separator)
			: this(prefix, separator, null)
		{
		}
    
		public LongArgumentScanner(string prefix, string separator,
			string listSeparator)
		{
			prefixRegex = new Regex(prefix);
      
			if (separator != null)
				equalRegex = new Regex(separator);

			if (listSeparator != null)
				listRegex = new Regex(listSeparator);
		}
#endregion

#region Properties
		private Regex prefixRegex = null;
		private Regex equalRegex = null;
		private Regex listRegex = null;
		private Hashtable names = new Hashtable();
#endregion

#region Scanning
		/// <summary>
		/// Contains the dictionary with the argument names.
		/// </summary>
		public override IDictionary Arguments { get { return names; } }

		/// <summary>
		/// Returns the type of attribute class that this scanner looks
		/// for.
		/// </summary>
		public override Type AttributeType
		{
			get { return typeof(LongArgumentAttribute); }
		}

		/// <summary>
		/// Used to identify if this argument can be matched against the
		/// format of this scanner. This function has a secondary effect
		/// of pulling out the individual components and preparing them
		/// for use with the ArgumentNames property.
		/// </summary>
		public override bool IsMatch(string argument)
		{
			// Clear out the internal fields
			names = new Hashtable();

			// Sanity checking
			if (prefixRegex == null)
				throw new ToolException("Scanner does not include a prefix regex");

			// Check to see if the front matches
			if (!prefixRegex.IsMatch(argument))
			{
				// Don't bother since we don't match
				return false;
			}

			string arg = prefixRegex.Replace(argument, "");
			string name = arg;
			string [] vals = null;

			// Check for equal regex
			if (equalRegex != null)
			{
				// Try to split it out on the equal
				string [] parts = equalRegex.Split(arg, 2);

				if (parts.Length == 2)
				{
					// Put the value into the first element of the vals
					name = parts[0];
					vals = new string [] { parts[1] };

					// Check for list separator
					if (listRegex != null)
					{
						// Split on the list separator fields
						vals = listRegex.Split(parts[1]);
					}
				}
			}

			// Set up the internal variables
			names[name] = vals;
			return true;
		}
#endregion
	}
}
