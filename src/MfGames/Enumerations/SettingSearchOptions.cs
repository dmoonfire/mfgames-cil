using System;

namespace MfGames.Enumerations
{
	/// <summary>
	/// Defines the various options for searching values in settings.
	/// </summary>
	[Flags]
	public enum SettingSearchOptions
	{
		/// <summary>
		/// Indicates no options which means if the key doesn't exist, then
		/// return the default.
		/// </summary>
		None = 0,
		
		/// <summary>
		/// Search the parents of the HierarchicalPath if the item can't be found.
		/// </summary>
		SearchHierarchicalParents = 1,
		
		/// <summary>
		/// Search any parent settings when the item can't be found.
		/// </summary>
		SearchParentSettings = 2,

		/// <summary>
		/// If set, it searches the parent settings first. Otherwise, the hierarchical
		/// parents will be searched first.
		/// </summary>
		DepthFirstSearch = 4,
	}
}
