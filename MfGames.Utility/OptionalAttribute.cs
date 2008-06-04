#region Copyright
/*
 * Copyright (C) 2005-2008, Moonfire Games
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
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

using System;
using System.Reflection;

namespace MfGames.Utility
{
	/// <summary>
	/// Contains the internal functionality for a single command-line
	/// argument descriptor. This indicates that the given property will
	/// be set when the command-line is parsed (using the given object)
	/// and those command-line arguments are removed.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
			AllowMultiple = true, Inherited = true)]
	public class OptionalAttribute
	: Attribute
	{
		public OptionalAttribute(string argumentName)
		{
			this.name = argumentName;
		}

		#region Properties
		private string name = null;

		/// <summary>
		/// Contains the registered name of this attribute.
		/// </summary>
		public string Name { get { return name; } }
		#endregion
	}
}
