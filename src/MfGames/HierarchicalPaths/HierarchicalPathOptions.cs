// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

#region Namespaces

using System;

#endregion

namespace MfGames.HierarchicalPaths
{
	/// <summary>
	/// Defines the options used for creating and parsing hierarchical paths.
	/// </summary>
	[Flags]
	public enum HierarchicalPathOptions
	{
		/// <summary>
		/// Defines no additional options.
		/// </summary>
		None,

		/// <summary>
		/// If included, the path will intern the various string components of
		/// the path in an attempt to save memory.
		/// </summary>
		InternStrings,
	}
}
