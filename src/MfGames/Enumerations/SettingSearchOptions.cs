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

#endregion

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