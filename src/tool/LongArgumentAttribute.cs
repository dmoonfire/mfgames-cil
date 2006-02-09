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
	/// Contains the internal functionality for a single command-line
	/// argument descriptor. This indicates that the given property will
	/// be set when the command-line is parsed (using the given object)
	/// and those command-line arguments are removed.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property
		| AttributeTargets.Field,
		AllowMultiple = true, Inherited = true)]
	public class LongArgumentAttribute : Attribute, IArgumentAttribute
	{
		public LongArgumentAttribute(string argumentName)
		{
			this.name = argumentName;
		}

#region Properties
		private string name = null;
		private bool hasParameter = false;
		private bool parameterOptional = false;

		/// <summary>
		/// Indicates that this argument has a parameter. This defaults to
		/// false. 
		/// </summary>
		public bool HasParameter
		{
			get { return hasParameter; }
			set { hasParameter = value; }
		}

		/// <summary>
		/// Contains the registered name of this attribute.
		/// </summary>
		public string Name { get { return name; } }

		/// <summary>
		/// Indicates of a parameter is optional. This defaults to false.
		/// </summary>
		public bool ParameterOptional
		{
			get { return parameterOptional; }
			set { parameterOptional = value; }
		}
#endregion
	}
}
