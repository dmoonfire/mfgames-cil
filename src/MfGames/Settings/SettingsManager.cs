using System.Collections.Generic;

using MfGames.Enumerations;

namespace MfGames.Settings
{
	/// <summary>
	/// Defines a manager of settings. A SettingsManager can zero or more parent
	/// settings to allow a recursive looking of values. It uses HierarchicalPath
	/// to identify the key in the settings and uses generics to provide typesafe
	/// settings.
	/// </summary>
	public class SettingsManager
	{
		#region Constructors
		
		public SettingsManager()
		{
			parents = new List<SettingsManager>();
			settings = new Dictionary<HierarchicalPath, object>();
		}
		
		#endregion
		
		#region Parents
		
		private List<SettingsManager> parents;
		
		public List<SettingsManager> Parents
		{
			get { return parents; }
		}
		
		#endregion
		
		#region Settings
		
		private Dictionary<HierarchicalPath, object> settings;
		
		public TSetting Get<TSetting>(HierarchicalPath path)
			where TSetting: new()
		{
			return Get<TSetting>(path, SettingSearchOptions.None);
		}
		
		public TSetting Get<TSetting>(
		                              HierarchicalPath path,
		                              SettingSearchOptions searchOptions)
			where TSetting: new()
		{
			TSetting output;
			SettingsManager manager;
			
			if (!TryGet<TSetting>(path, searchOptions, out output, out manager))
			{
				output = new TSetting();
			}
			
			return output;
		}
		
		public bool Contains(HierarchicalPath path)
		{
			return Contains(path, SettingSearchOptions.None);
		}
		
		public bool Contains(HierarchicalPath path, SettingSearchOptions searchOptions)
		{
			object output;
			SettingsManager manager;
			return TryGet<object>(path, searchOptions, out output, out manager);
		}

		public bool TryGet<TSetting>(
			HierarchicalPath path, 
			SettingSearchOptions searchOptions,
			out SettingsManager containingManager)
			where TSetting: new()
		{
			TSetting output;
			return TryGet<TSetting>(path, searchOptions, out output, out containingManager);
		}

		public bool TryGet<TSetting>(
			HierarchicalPath path, 
			SettingSearchOptions searchOptions,
			out TSetting output)
			where TSetting: new()
		{
			SettingsManager manager;
			return TryGet<TSetting>(path, searchOptions, out output, out manager);
		}

		public bool TryGet<TSetting>(
			HierarchicalPath path, 
			SettingSearchOptions searchOptions,
			out TSetting output,
			out SettingsManager containingManager)
			where TSetting: new()
		{
			// Try the current settings for the key.
			if (settings.ContainsKey(path))
			{
				output = Map<TSetting>(settings[path]);
				containingManager = this;
				return true;
			}
			
			// Check to see if we need to search for the parents and we want to check it first.
			bool searchSettings = (searchOptions & SettingSearchOptions.SearchParentSettings) == SettingSearchOptions.SearchParentSettings;
			bool searchPaths = (searchOptions & SettingSearchOptions.SearchHierarchicalParents) == SettingSearchOptions.SearchHierarchicalParents;
			bool depthFirst = (searchOptions & SettingSearchOptions.DepthFirstSearch) == SettingSearchOptions.DepthFirstSearch;
			
			if (searchSettings && depthFirst)
			{
				foreach (SettingsManager parent in parents)
				{
					if (parent.TryGet<TSetting>(path, searchOptions, out output, out containingManager))
					{
						return true;
					}
				}
			}
			
			if (searchPaths && path.Count != 1)
			{
				HierarchicalPath currentPath = path.ParentPath;

				while (true)
				{
					if (settings.ContainsKey(currentPath))
					{
						output = Map<TSetting>(settings[currentPath]);
						containingManager = this;
						return true;
					}
					
					if (currentPath.Count == 1)
					{
						break;
					}
					
					currentPath = currentPath.ParentPath;
				}
			}
			
			if (searchSettings && !depthFirst)
			{
				foreach (SettingsManager parent in parents)
				{
					if (parent.TryGet<TSetting>(path, searchOptions, out output, out containingManager))
					{
						return true;
					}
				}
			}
			
			// If we got this far, we couldn't find it with the given settings. So, we set the default
			// value and return false.
			output = default(TSetting);
			containingManager = null;
			return false;
		}
		
		private TSetting Map<TSetting>(object input)
			where TSetting: new()
		{
			if (input is TSetting)
			{
				return (TSetting) input;
			}
			
			return new TSetting();
		}
		
		#endregion
		
		#region Serialization
		
		#endregion
	}
}