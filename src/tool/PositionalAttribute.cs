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
	using System.Reflection;

	/// <summary>
	/// Represents a positional attribute which is used after the long
	/// and short arguments are processed. This will not match an
	/// argument attribute (based on the regexes).
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
		AllowMultiple = false, Inherited = true)]
	public class PositionalAttribute : Attribute
	{
		/// <summary>
		/// Constructs an empty positional attribute.
		/// </summary>
		public PositionalAttribute()
		{
		}

		/// <summary>
		/// Constructs the positional attribute with the given index.
		/// </summary>
		public PositionalAttribute(int index)
		{
			this.index = index;
		}

#region Properties
		private int index = 0;
		private string description = null;
		private bool optional = true;
		private string name = null;

		/// <summary>
		/// Contains a short, hopefully one-line description which is used
		/// for the usage functionality.
		/// </summary>
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		/// in the usage
		/// <summary>
		/// Contains the zero-based index for the position. A position 0
		/// attribute will get the first one, the position 1 gets the
		/// next, etc.
		/// </summary>
		public int Index
		{
			get { return index; }
			set { index = value; }
		}

		/// <summary>
		/// Indicates of a parameter is optional. This defaults to false.
		/// </summary>
		public bool IsOptional
		{
			get { return optional; }
			set { optional = value; }
		}

		/// <summary>
		/// Contains the name of the positional attribute, which defaults
		/// to "argX" where X is the Index property.
		/// </summary>
		public string Name
		{
			get
			{
				if (name == null)
					return "arg" + Index;
				else
					return name;
			}

			set { name = value; }
		}
#endregion
	}
}
