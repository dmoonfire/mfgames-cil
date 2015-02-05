// <copyright file="HierarchicalSelector.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.HierarchicalPaths
{
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Extends the <see cref="HierarchicalPath"/> to include functionality for
    /// selecting HierarchicalPaths and returning them.
    /// </summary>
    public class HierarchicalSelector : HierarchicalPath
    {
        #region Fields

        /// <summary>
        /// </summary>
        private object[] operations;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalSelector"/> class.
        /// </summary>
        /// <param name="isRelative">
        /// </param>
        public HierarchicalSelector(bool isRelative)
            : base(isRelative)
        {
            this.ParseSelectors();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalSelector"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        public HierarchicalSelector(string path)
            : base(path)
        {
            this.ParseSelectors();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalSelector"/> class.
        /// </summary>
        /// <param name="levels">
        /// The levels.
        /// </param>
        /// <param name="isRelative">
        /// if set to <c>true</c> [is relative].
        /// </param>
        public HierarchicalSelector(
            IEnumerable<string> levels,
            bool isRelative)
            : base(levels,
                isRelative)
        {
            this.ParseSelectors();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalSelector"/> class.
        /// </summary>
        /// <param name="levels">
        /// The levels.
        /// </param>
        /// <param name="startIndex">
        /// The start I ndex.
        /// </param>
        /// <param name="isRelative">
        /// if set to <c>true</c> [is relative].
        /// </param>
        public HierarchicalSelector(
            IEnumerable<string> levels,
            int startIndex,
            bool isRelative)
            : base(levels,
                startIndex,
                isRelative)
        {
            this.ParseSelectors();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalSelector"/> class.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        public HierarchicalSelector(
            string path,
            HierarchicalPath context)
            : base(path,
                context)
        {
            this.ParseSelectors();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Determines whether the specified path is match for the selector.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified path is match; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMatch(HierarchicalPath path)
        {
            return this.IsMatch(
                path,
                0);
        }

        /// <summary>
        /// Determines whether the specified path is match for the selector.
        /// </summary>
        /// <param name="path">
        /// The path.
        /// </param>
        /// <param name="startIndex">
        /// The start index.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified path is match; otherwise, <c>false</c>.
        /// </returns>
        public bool IsMatch(
            HierarchicalPath path,
            int startIndex)
        {
            // If the selector is longer than the given path, it will never
            // match the path.
            if (this.Levels.Count > path.Levels.Count)
            {
                return false;
            }

            // Go through all the elements of the selector.
            for (int selectorIndex = 0, pathIndex = startIndex;
                selectorIndex < this.Levels.Count;
                selectorIndex++)
            {
                // Check to see if we have a special function for the selector's
                // level.
                object operation = this.operations[selectorIndex];
                string pathLevel = path.Levels[pathIndex];

                if (operation == null)
                {
                    // Just do a string comparison between the two levels.
                    if (this.Levels[selectorIndex] != pathLevel)
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
                    if (!((Regex)operation).IsMatch(pathLevel))
                    {
                        // It doesn't match the regular expression.
                        return false;
                    }

                    pathIndex++;
                    continue;
                }

                // If we got this far, we are a double-star match. If this is
                // the last index in the selector, it will match everything.
                if (selectorIndex == this.Levels.Count - 1)
                {
                    return true;
                }

                // Loop through the remaing elements of the path and see if
                // we can find a subtree match.
                var subSelector = (HierarchicalSelector)operation;

                for (int starIndex = pathIndex;
                    starIndex < path.Levels.Count;
                    starIndex++)
                {
                    // If this is a match, use it.
                    if (subSelector.IsMatch(
                        path,
                        starIndex))
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
        /// <param name="path">
        /// The path.
        /// </param>
        /// <returns>
        /// <c>true</c> if the specified path is match; otherwise, <c>false</c>.
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
        /// <param name="paths">
        /// The paths.
        /// </param>
        /// <returns>
        /// </returns>
        public IList<HierarchicalPath> SelectFrom(
            IEnumerable<HierarchicalPath> paths)
        {
            // Create a list of paths that we selected.
            var selected = new List<HierarchicalPath>();

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

        #region Methods

        /// <summary>
        /// Parses the given path and converts the various ** and * elements into
        /// regular expressions for selection.
        /// </summary>
        private void ParseSelectors()
        {
            // Create a selector regex that matches the length.
            this.operations = new object[this.Levels.Count];

            for (int index = 0; index < this.Levels.Count; index++)
            {
                // Check for "**" by itself which means zero or more levels.
                string level = this.Levels[index];

                if (level == "**")
                {
                    // Create a sub-tree of the selector and put it into this
                    // object.
                    this.operations[index] =
                        new HierarchicalSelector(
                            this.Levels,
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

                    this.operations[index] = new Regex(buffer.ToString());
                    continue;
                }

                // For everything else, leave it null to indicate that a strict
                // string is required.
                this.operations[index] = null;
            }
        }

        #endregion
    }
}