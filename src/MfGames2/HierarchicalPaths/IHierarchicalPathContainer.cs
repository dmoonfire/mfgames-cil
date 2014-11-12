// <copyright file="IHierarchicalPathContainer.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.HierarchicalPaths
{
    /// <summary>
    /// Describes objects that have an HierarchicalPath associated with them.
    /// </summary>
    public interface IHierarchicalPathContainer
    {
        #region Public Properties

        /// <summary>
        /// Gets the hierarchical path associated with the instance.
        /// </summary>
        HierarchicalPath HierarchicalPath { get; }

        #endregion
    }
}