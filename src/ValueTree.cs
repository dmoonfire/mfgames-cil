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

	/// <summary>
	/// Extends an AttributeTree and wraps a basic value around it,
	/// including some translation features for converting to and from
	/// various common types.
	/// </summary>
	public class ValueTree : AttributeTree
	{
		/// <summary>
		/// Construct the empty or default values.
		/// </summary>
		public ValueTree()
		{
		}

		/// <summary>
		/// Methods for changing what object is used for child elements.
		/// </summary>
		public override AttributeTree CreateClone()
		{
			return new ValueTree();
		}

#region Properties
		/// <summary>
		/// Contains the integer value or throws an exception.
		/// </summary>
		public int Int32
		{
			get { return Convert.ToInt32(String); }
			set { String = value.ToString(); }
		}

		/// <summary>
		/// Contains the set value, or null if no value.
		/// </summary>
		public virtual string String
		{
			get
			{
				// Check for null
				return (string) Attributes["value"];
			}

			set
			{
				Attributes["value"] = value;
			}
		}
#endregion
	}
}
