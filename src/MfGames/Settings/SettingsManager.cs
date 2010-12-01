#region Copyright and License

// Copyright (c) 2005-2011, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System.Collections.Generic;

using MfGames.Enumerations;

#endregion

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

		/// <summary>
		/// Initializes a new instance of the <see cref="SettingsManager"/> class.
		/// </summary>
		public SettingsManager()
		{
			parents = new List<SettingsManager>();
			settings = new Dictionary<HierarchicalPath, object>();
		}

		#endregion

		#region Parents

		private readonly List<SettingsManager> parents;

		/// <summary>
		/// Gets the parent settings.
		/// </summary>
		/// <value>The parents.</value>
		public List<SettingsManager> Parents
		{
			get { return parents; }
		}

		#endregion

		#region Settings

		private readonly Dictionary<HierarchicalPath, object> settings;

		/// <summary>
		/// Determines whether the settings contain the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified path]; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(HierarchicalPath path)
		{
			return Contains(path, SettingSearchOptions.None);
		}

		/// <summary>
		/// Determines whether the settings contain the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="searchOptions">The search options.</param>
		/// <returns>
		/// 	<c>true</c> if [contains] [the specified path]; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(
			HierarchicalPath path,
			SettingSearchOptions searchOptions)
		{
			object output;
			SettingsManager manager;
			return TryGet(path, searchOptions, out output, out manager);
		}

		/// <summary>
		/// Gets a settings at the specific path.
		/// </summary>
		/// <typeparam name="TSetting">The type of the setting.</typeparam>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public TSetting Get<TSetting>(HierarchicalPath path) where TSetting: new()
		{
			return Get<TSetting>(path, SettingSearchOptions.None);
		}

		/// <summary>
		/// Gets a settings at the specific path.
		/// </summary>
		/// <typeparam name="TSetting">The type of the setting.</typeparam>
		/// <param name="path">The path.</param>
		/// <param name="searchOptions">The search options.</param>
		/// <returns></returns>
		public TSetting Get<TSetting>(
			HierarchicalPath path,
			SettingSearchOptions searchOptions) where TSetting: new()
		{
			TSetting output;
			SettingsManager manager;

			if (!TryGet(path, searchOptions, out output, out manager))
			{
				output = new TSetting();
			}

			return output;
		}

		/// <summary>
		/// Maps the specified input into the appropriate format.
		/// </summary>
		/// <typeparam name="TSetting">The type of the setting.</typeparam>
		/// <param name="input">The input.</param>
		/// <returns></returns>
		private TSetting Map<TSetting>(object input) where TSetting: new()
		{
			if (input is TSetting)
			{
				return (TSetting) input;
			}

			return new TSetting();
		}

		/// <summary>
		/// Tries to get settings at the given path without creating it if
		/// missing.
		/// </summary>
		/// <typeparam name="TSetting">The type of the setting.</typeparam>
		/// <param name="path">The path.</param>
		/// <param name="searchOptions">The search options.</param>
		/// <param name="containingManager">The containing manager.</param>
		/// <returns></returns>
		public bool TryGet<TSetting>(
			HierarchicalPath path,
			SettingSearchOptions searchOptions,
			out SettingsManager containingManager) where TSetting: new()
		{
			TSetting output;
			return TryGet(path, searchOptions, out output, out containingManager);
		}

		/// <summary>
		/// Tries to get settings at the given path without creating it if
		/// missing.
		/// </summary>
		/// <typeparam name="TSetting">The type of the setting.</typeparam>
		/// <param name="path">The path.</param>
		/// <param name="searchOptions">The search options.</param>
		/// <param name="output">The output.</param>
		/// <returns></returns>
		public bool TryGet<TSetting>(
			HierarchicalPath path,
			SettingSearchOptions searchOptions,
			out TSetting output) where TSetting: new()
		{
			SettingsManager manager;
			return TryGet(path, searchOptions, out output, out manager);
		}

		/// <summary>
		/// Tries to get settings at the given path without creating it if
		/// missing.
		/// </summary>
		/// <typeparam name="TSetting">The type of the setting.</typeparam>
		/// <param name="path">The path.</param>
		/// <param name="searchOptions">The search options.</param>
		/// <param name="output">The output.</param>
		/// <param name="containingManager">The containing manager.</param>
		/// <returns></returns>
		public bool TryGet<TSetting>(
			HierarchicalPath path,
			SettingSearchOptions searchOptions,
			out TSetting output,
			out SettingsManager containingManager) where TSetting: new()
		{
			// Try the current settings for the key.
			if (settings.ContainsKey(path))
			{
				output = Map<TSetting>(settings[path]);
				containingManager = this;
				return true;
			}

			// Check to see if we need to search for the parents and we want to check it first.
			bool searchSettings = (searchOptions &
			                       SettingSearchOptions.SearchParentSettings) ==
			                      SettingSearchOptions.SearchParentSettings;
			bool searchPaths = (searchOptions &
			                    SettingSearchOptions.SearchHierarchicalParents) ==
			                   SettingSearchOptions.SearchHierarchicalParents;
			bool depthFirst = (searchOptions & SettingSearchOptions.DepthFirstSearch) ==
			                  SettingSearchOptions.DepthFirstSearch;

			if (searchSettings && depthFirst)
			{
				foreach (SettingsManager parent in parents)
				{
					if (parent.TryGet(path, searchOptions, out output, out containingManager))
					{
						return true;
					}
				}
			}

			if (searchPaths && path.Count != 1)
			{
				HierarchicalPath currentPath = path.Parent;

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

					currentPath = currentPath.Parent;
				}
			}

			if (searchSettings && !depthFirst)
			{
				foreach (SettingsManager parent in parents)
				{
					if (parent.TryGet(path, searchOptions, out output, out containingManager))
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

		#endregion

		#region Serialization

		#endregion
	}
}