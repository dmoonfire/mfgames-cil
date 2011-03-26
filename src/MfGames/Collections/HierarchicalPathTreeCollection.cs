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

#endregion

namespace MfGames.Collections
{
	/// <summary>
	/// Represents a collection of items, keyed by a <see cref="HierarchicalPath"/>
	/// and arranged into a tree view.
	/// </summary>
	public class HierarchicalPathTreeCollection<TValue>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPathTreeCollection&lt;TValue&gt;"/> class.
		/// </summary>
		public HierarchicalPathTreeCollection()
		{
			children = new Dictionary<string, HierarchicalPathTreeCollection<TValue>>();
		}

		#endregion

		#region Factory

		private readonly Dictionary<string, HierarchicalPathTreeCollection<TValue>>
			children;

		/// <summary>
		/// Gets the count of child items.
		/// </summary>
		public int Count
		{
			get { return children.Count; }
		}

		/// <summary>
		/// Creates the child collection. This is called when the tree needs to
		/// create a child tree.
		/// </summary>
		/// <returns></returns>
		protected virtual HierarchicalPathTreeCollection<TValue> CreateChild()
		{
			return new HierarchicalPathTreeCollection<TValue>();
		}

		#endregion

		#region Collection

		/// <summary>
		/// Determines whether the collection contains a collection at that path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns>
		///   <c>true</c> if [contains] [the specified path]; otherwise, <c>false</c>.
		/// </returns>
		public bool Contains(HierarchicalPath path)
		{
			// If we have no levels, then we found it.
			if (path.Count == 0)
			{
				return true;
			}

			// Check to see if this tree contains the next level.
			if (children.ContainsKey(path.First))
			{
				return children[path.First].Contains(path.Splice(1));
			}

			// Otherwise, we don't have the child so we won't contain it.
			return false;
		}

		/// <summary>
		/// Retrieves the collection at a given path.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public HierarchicalPathTreeCollection<TValue> Get(HierarchicalPath path)
		{
			// If we are at the top-level path, return ourselves.
			if (path.Count == 0)
			{
				return this;
			}

			// Get the top-level element for a child.
			string topLevel = path.First;
			var childPath = path.Splice(1);

			return children[topLevel].Get(childPath);
		}

		/// <summary>
		/// Adds an item at the specified path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="item">The item.</param>
		public void Add(
			HierarchicalPath path,
			TValue item)
		{
			// If are the top-level path, set the item.
			if (path.Count == 0)
			{
				Item = item;
				return;
			}

			// We have at least one more level. Figure out the top-level string
			// and create the key for it.
			string topLevel = path.First;

			if (!children.ContainsKey(topLevel))
			{
				children[topLevel] = CreateChild();
			}

			// Pull out the child tree so we can add it.
			HierarchicalPathTreeCollection<TValue> child = children[topLevel];
			var childPath = path.Splice(1);

			child.Add(childPath, item);
		}

		#endregion

		#region Item

		/// <summary>
		/// Gets or sets the item at this level.
		/// </summary>
		/// <value>
		/// The item.
		/// </value>
		public TValue Item { get; set; }

		#endregion
	}
}