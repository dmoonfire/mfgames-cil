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

	/// <summary>
	/// Describes the basic argument scanner.
	/// </summary>
	public abstract class ArgumentScanner : Logable
	{
		/// <summary>
		/// Contains the dictionary with the argument names.
		/// </summary>
		public abstract IDictionary Arguments { get; }

		/// <summary>
		/// Returns the type of attribute class that this scanner looks
		/// for.
		/// </summary>
		public abstract Type AttributeType { get; }

		/// <summary>
		/// Used to identify if this argument can be matched against the
		/// format of this scanner. This function has a secondary effect
		/// of pulling out the individual components and preparing them
		/// for use with the ArgumentNames property.
		/// </summary>
		public abstract bool IsMatch(string argument);
	}
}
