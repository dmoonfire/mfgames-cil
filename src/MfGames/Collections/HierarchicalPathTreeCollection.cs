#region Copyright and License

// Copyright (C) 2005-2011 by Moonfire Games
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
using System.Linq;

#endregion

namespace MfGames.Collections
{
    /// <summary>
    /// Organizes a collection of key/value pairs where the key is a
    /// HierarchicalPath and the value for a given key is a TValue.
    /// </summary>
    public class HierarchicalPathTreeCollection<TValue>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalPathTreeCollection&lt;TValue&gt;"/> class.
        /// </summary>
        public HierarchicalPathTreeCollection()
        {
            children =
                new Dictionary<string, HierarchicalPathTreeCollection<TValue>>();
        }

        #endregion

        #region Factory

        private readonly
            Dictionary<string, HierarchicalPathTreeCollection<TValue>> children;

		/// <summary>
		/// Contains the number of child trees within the collection.
		/// </summary>
    	public int ChildCount
    	{
    		get { return children.Count; }
    	}

    	/// <summary>
        /// Gets the count of items, recursively going through the child elements.
        /// </summary>
        public int Count
        {
			get
			{
				return (HasItem ? 1 : 0) + children.Values.Sum(child => child.Count);
			}
        }

		/// <summary>
		/// Contains the number of nodes in the tree.
		/// </summary>
    	public int NodeCount
    	{
    		get
    		{
    			return 1 + children.Values.Sum(child => child.NodeCount);
    		}
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
                HasItem = true;
                return;
            }

            // We have at least one more level. Figure out the top-level string
            // and see if we have to create the child tree node for it.
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
                return HasItem;
            }

            // Check to see if this tree contains the next level in the path.
            if (children.ContainsKey(path.First))
            {
                return children[path.First].Contains(path.Splice(1));
            }

            // Otherwise, we don't have the child so there is no chance
            // we can contain the item.
            return false;
        }

        /// <summary>
        /// Retrieves an item at the given path. If the item doesn't exist,
        /// then a NotFoundException.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public TValue Get(HierarchicalPath path)
        {
            // If we are at the top-level path, return ourselves.
            if (path.Count == 0)
            {
                // If we have the item, then return it. If we don't have the
                // item, then throw an exception.
                if (HasItem)
                {
                    // We have the item, so return it.
                    return Item;
                }

                // Throw a not found exception since we can't find it.
                throw new KeyNotFoundException("Cannot retrieve value at path " + path);
            }

            // Get the top-level element for a child and make sure we have it.
            string topLevel = path.First;

            if (!children.ContainsKey(topLevel))
            {
				throw new KeyNotFoundException("Cannot retrieve value at path " + path);
            }

			// Pass the request into the child level.
            HierarchicalPath childPath = path.Splice(1);
			HierarchicalPathTreeCollection<TValue> child = children[topLevel];

			return child.Get(childPath);
        }

		/// <summary>
		/// Retrieves the child-level tree collection for a given path.
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public HierarchicalPathTreeCollection<TValue> GetChild(HierarchicalPath path)
		{
			// If we are at the top-level path, return ourselves.
			if (path.Count == 0)
			{
				return this;
			}

			// Get the top-level element for a child and make sure we have it.
			string topLevel = path.First;

			if (!children.ContainsKey(topLevel))
			{
				throw new KeyNotFoundException("Cannot retrieve value at path " + path);
			}

			// Pass the request to the child.
			HierarchicalPathTreeCollection<TValue> child = children[topLevel];

			return child.GetChild(path.Splice(1));
		}

    	#endregion

        #region Item

        /// <summary>
        /// Internal flag to determine if the item is populated.
        /// </summary>
        public bool HasItem { get; private set; }

        /// <summary>
        /// Gets or sets the item at this level. This is the equivelant of
        /// setting or getting the path "/".
        /// </summary>
        public TValue Item { get; set; }

        #endregion
    }
}