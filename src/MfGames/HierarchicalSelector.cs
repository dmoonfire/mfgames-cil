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

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace MfGames
{
	/// <summary>
	/// Extends the HierarchicalPath to include functionality for selecting
	/// HierarchicalPaths and returning them.
	/// </summary>
	public class HierarchicalSelector : HierarchicalPath
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalSelector"/> class.
		/// </summary>
		/// <param name="isRelative"></param>
		public HierarchicalSelector(bool isRelative)
			: base(isRelative)
		{
			ParseSelectors();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalSelector"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		public HierarchicalSelector(string path)
			: base(path)
		{
			ParseSelectors();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalSelector"/> class.
		/// </summary>
		/// <param name="levels">The levels.</param>
		/// <param name="isRelative">if set to <c>true</c> [is relative].</param>
		public HierarchicalSelector(
			string[] levels,
			bool isRelative)
			: base(levels, isRelative)
		{
			ParseSelectors();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalSelector"/> class.
		/// </summary>
		/// <param name="levels">The levels.</param>
		/// <param name="startIndex">The start I ndex.</param>
		/// <param name="isRelative">if set to <c>true</c> [is relative].</param>
		public HierarchicalSelector(
			string[] levels,
			int startIndex,
			bool isRelative)
			: base(levels, startIndex, isRelative)
		{
			ParseSelectors();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalSelector"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="context">The context.</param>
		public HierarchicalSelector(
			string path,
			HierarchicalPath context)
			: base(path, context)
		{
			ParseSelectors();
		}

		#endregion

		#region Parsing

		private object[] operations;

		/// <summary>
		/// Parses the given path and converts the various ** and * elements into
		/// regular expresions for selection.
		/// </summary>
		private void ParseSelectors()
		{
			// Create a selector regex that matches the length.
			operations = new object[Levels.Length];

			for (int index = 0; index < Levels.Length; index++)
			{
				// Check for "**" by itself which means zero or more levels.
				string level = Levels[index];

				if (level == "**")
				{
					// Create a sub-tree of the selector and put it into this
					// object.
					operations[index] = new HierarchicalSelector(
						Levels, 
						index + 1,
						true);
					continue;
				}

				// Check for globbing operators ("*") which become ".*?" regex.
				if (level.Contains("*"))
				{
					// Go through the string and convert all the "*" into regex.
					var buffer = new StringBuilder();

					for (int c = 0; c < level.Length; c++)
					{
						if (level[c] == '*')
						{
							buffer.Append(".*?");
						}
						else
						{
							buffer.Append(level[c]);
						}
					}

					operations[index] = new Regex(buffer.ToString());
					continue;
				}

				// For everything else, leave it null to indicate that a strict
				// string is required.
				operations[index] = null;
			}
		}

		#endregion

		#region Matching

		/// <summary>
		/// Determines whether the specified path is match for the selector.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// 	<c>true</c> if the specified path is match; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMatch(HierarchicalPath path)
		{
			return IsMatch(path, 0);
		}

		/// <summary>
		/// Determines whether the specified path is match for the selector.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="startIndex">The start index.</param>
		/// <returns>
		/// 	<c>true</c> if the specified path is match; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMatch(HierarchicalPath path, int startIndex)
		{
			// If the selector is longer than the given path, it will never
			// match the path.
			if (Levels.Length > path.Levels.Length)
			{
				return false;
			}

			// Go through all the elements of the selector.
			for (int selectorIndex = 0, pathIndex = startIndex;
			     selectorIndex < Levels.Length;
			     selectorIndex++)
			{
				// Check to see if we have a special function for the selector's
				// level.
				object operation = operations[selectorIndex];
				string pathLevel = path.Levels[pathIndex];

				if (operation == null)
				{
					// Just do a string comparison between the two levels.
					if (Levels[selectorIndex] != pathLevel)
					{
						// It doesn't match, so return false.
						return false;
					}

					pathIndex++;
					continue;
				}

				// If we are regular expression, then use that.
				if (operation is Regex)
				{
					if (!((Regex) operation).IsMatch(pathLevel))
					{
						// It doesn't match the regular expression.
						return false;
					}

					pathIndex++;
					continue;
				}

				// If we got this far, we are a double-star match. If this is
				// the last index in the selector, it will match everything.
				if (selectorIndex == Levels.Length - 1)
				{
					return true;
				}

				// Loop through the remaing elements of the path and see if
				// we can find a subtree match.
				HierarchicalSelector subSelector = (HierarchicalSelector) operation;

				for (int starIndex = pathIndex; starIndex < path.Levels.Length; starIndex++)
				{
					// If this is a match, use it.
					if (subSelector.IsMatch(path, starIndex))
					{
						return true;
					}
				}

				// We couldn't find a subtree match.
				return false;
			}

			// If we got through the entire loop, we have a match.
			return true;
		}

		/// <summary>
		/// Determines whether the specified path is match for the selector.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		/// 	<c>true</c> if the specified path is match; otherwise, <c>false</c>.
		/// </returns>
		public bool IsMatch(string path)
		{
			var hierarchicalPath = new HierarchicalPath(path);
			return IsMatch(hierarchicalPath);
		}

		/// <summary>
		/// Uses the current selector to retrieve a list of matching paths
		/// from the given collection.
		/// </summary>
		/// <param name="paths">The paths.</param>
		/// <returns></returns>
		public IList<HierarchicalPath> SelectFrom(IEnumerable<HierarchicalPath> paths)
		{
			// Create a list of paths that we selected.
			List<HierarchicalPath> selected = new List<HierarchicalPath>();

			foreach (HierarchicalPath path in paths)
			{
				if (IsMatch(path))
				{
					selected.Add(path);
				}
			}

			// Return the list of selected paths.
			return selected;
		}

		#endregion
	}
}