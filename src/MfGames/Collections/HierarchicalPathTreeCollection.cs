// <copyright file="HierarchicalPathTreeCollection.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Collections
{
    using System.Collections.Generic;
    using System.Linq;

    using MfGames.HierarchicalPaths;

    /// <summary>
    /// Organizes a collection of key/value pairs where the key is a
    /// HierarchicalPath and the value for a given key is a TValue. The
    /// tree is broken into levels, based on the HierarchicalPath. For
    /// example, storing "/a/b" and "/a/c" would have only one item at the
    /// root level ("a") and underneath the "a" node would be two nodes
    /// ("b" and "c").
    /// 
    /// Items are the actual values for a given path. These are the
    /// values for Count and the IEnumerable implementations. These are recursive
    /// across the lower nodes.
    /// 
    /// To access the different nodes ("a", "b", and "c" in the above example),
    /// the various Node properties and methods are used. See Nodes and NodeCount
    /// for examples.
    /// </summary>
    /// <typeparam name="TValue">
    /// </typeparam>
    public class HierarchicalPathTreeCollection<TValue>
    {
        #region Fields

        /// <summary>
        /// </summary>
        private readonly
            Dictionary<string, HierarchicalPathTreeCollection<TValue>> nodes;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalPathTreeCollection&lt;TValue&gt;"/> class.
        /// </summary>
        public HierarchicalPathTreeCollection()
            : this(HierarchicalPath.AbsoluteRoot)
        {
        }

        /// <summary>
        /// Initializes a new instance of the class with a given path.
        /// </summary>
        /// <param name="path">
        /// </param>
        private HierarchicalPathTreeCollection(HierarchicalPath path)
        {
            this.Path = path;
            this.nodes =
                new Dictionary<string, HierarchicalPathTreeCollection<TValue>>();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the count of items, recursively going through the child elements.
        /// </summary>
        public int Count
        {
            get
            {
                return (this.HasItem ? 1 : 0)
                    + this.nodes.Values.Sum(child => child.Count);
            }
        }

        /// <summary>
        /// The number of nodes directly underneath the current tree node.
        /// </summary>
        public int DirectNodeCount
        {
            get
            {
                return this.nodes.Count;
            }
        }

        /// <summary>
        /// Contains an enumerable collection of nodes directly under the tree.
        /// </summary>
        public IEnumerable<HierarchicalPathTreeCollection<TValue>> DirectNodes
        {
            get
            {
                return this.nodes.Values;
            }
        }

        /// <summary>
        /// Internal flag to determine if the item is populated.
        /// </summary>
        public bool HasItem { get; private set; }

        /// <summary>
        /// Gets or sets the item at this level. This is the equivelant of
        /// setting or getting the path "/".
        /// </summary>
        public TValue Item { get; set; }

        /// <summary>
        /// Contains the number of nodes in the tree, calculated recursively.
        /// </summary>
        public int NodeCount
        {
            get
            {
                return 1 + this.nodes.Values.Sum(child => child.NodeCount);
            }
        }

        /// <summary>
        /// Gets the path of the tree collection, relative to the root.
        /// </summary>
        public HierarchicalPath Path { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Adds an item at the specified path.
        /// </summary>
        /// <param name="path">
        /// </param>
        /// <param name="item">
        /// </param>
        public void Add(string path, TValue item)
        {
            this.Add(new HierarchicalPath(path), item);
        }

        /// <summary>
        /// Adds an item at the specified path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(HierarchicalPath path, TValue item)
        {
            // If are the top-level path, set the item.
            if (path.Count == 0)
            {
                this.Item = item;
                this.HasItem = true;
                return;
            }

            // We have at least one more level. Figure out the top-level string
            // and see if we have to create the child tree node for it.
            string topLevel = path.First;

            if (!this.nodes.ContainsKey(topLevel))
            {
                this.nodes[topLevel] = this.CreateChild(topLevel);
            }

            // Pull out the child tree so we can add it.
            HierarchicalPathTreeCollection<TValue> child = this.nodes[topLevel];
            HierarchicalPath childPath = path.Splice(1);

            child.Add(childPath, item);
        }

        /// <summary>
        /// Determines whether the collection contains a collection at that path.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// <c>true</c> if [contains] [the specified path]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(HierarchicalPath path)
        {
            // If we have no levels, then we found it.
            if (path.Count == 0)
            {
                return this.HasItem;
            }

            // Check to see if this tree contains the next level in the path.
            if (this.nodes.ContainsKey(path.First))
            {
                return this.nodes[path.First].Contains(path.Splice(1));
            }

            // Otherwise, we don't have the child so there is no chance
            // we can contain the item.
            return false;
        }

        /// <summary>
        /// Retrieves an item at the given path. If the item doesn't exist,
        /// then a NotFoundException.
        /// </summary>
        /// <param name="path">
        /// </param>
        /// <returns>
        /// </returns>
        public TValue Get(HierarchicalPath path)
        {
            // If we are at the top-level path, return ourselves.
            if (path.Count == 0)
            {
                // If we have the item, then return it. If we don't have the
                // item, then throw an exception.
                if (this.HasItem)
                {
                    // We have the item, so return it.
                    return this.Item;
                }

                // Throw a not found exception since we can't find it.
                throw new KeyNotFoundException(
                    "Cannot retrieve value at path " + path);
            }

            // Get the top-level element for a child and make sure we have it.
            string topLevel = path.First;

            if (!this.nodes.ContainsKey(topLevel))
            {
                throw new KeyNotFoundException(
                    "Cannot retrieve value at path " + path);
            }

            // Pass the request into the child level.
            HierarchicalPath childPath = path.Splice(1);
            HierarchicalPathTreeCollection<TValue> child = this.nodes[topLevel];

            return child.Get(childPath);
        }

        /// <summary>
        /// Retrieves the child-level tree collection for a given path.
        /// </summary>
        /// <param name="path">
        /// </param>
        /// <returns>
        /// </returns>
        public HierarchicalPathTreeCollection<TValue> GetChild(
            HierarchicalPath path)
        {
            // If we are at the top-level path, return ourselves.
            if (path.Count == 0)
            {
                return this;
            }

            // Get the top-level element for a child and make sure we have it.
            string topLevel = path.First;

            if (!this.nodes.ContainsKey(topLevel))
            {
                throw new KeyNotFoundException(
                    "Cannot retrieve value at path " + path);
            }

            // Pass the request to the child.
            HierarchicalPathTreeCollection<TValue> child = this.nodes[topLevel];

            return child.GetChild(path.Splice(1));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates the child collection. This is called when the tree needs to
        /// create a child tree.
        /// </summary>
        /// <param name="childNodeName">
        /// </param>
        /// <returns>
        /// </returns>
        protected virtual HierarchicalPathTreeCollection<TValue> CreateChild(
            string childNodeName)
        {
            return
                new HierarchicalPathTreeCollection<TValue>(
                    new HierarchicalPath(childNodeName, this.Path));
        }

        #endregion
    }
}