// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

namespace MfGames.HierarchicalPaths
{
	/// <summary>
	/// Describes objects that have an HierarchicalPath associated with them.
	/// </summary>
	public interface IHierarchicalPathContainer
	{
		/// <summary>
		/// Gets the hierarchical path associated with the instance.
		/// </summary>
		HierarchicalPath HierarchicalPath { get; }
	}
}
