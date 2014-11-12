// <copyright file="HierarchicalPathOptions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.HierarchicalPaths
{
    using System;

    /// <summary>
    /// Defines the options used for creating and parsing hierarchical paths.
    /// </summary>
    [Flags]
    public enum HierarchicalPathOptions
    {
        /// <summary>
        /// Defines no additional options.
        /// </summary>
        None, 

        /// <summary>
        /// If included, the path will intern the various string components of
        /// the path in an attempt to save memory.
        /// </summary>
        InternStrings, 
    }
}