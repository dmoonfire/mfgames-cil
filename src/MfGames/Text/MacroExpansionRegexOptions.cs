// <copyright file="MacroExpansionRegexOptions.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Text
{
    using System;

    /// <summary>
    /// Provides the options for regular expression generation.
    /// </summary>
    [Flags]
    public enum MacroExpansionRegexOptions
    {
        /// <summary>
        /// The default value, which produces a capturing group.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Causes the generated regular expression to be non-capturing groups.
        /// </summary>
        NonCapturing = 1,
    }
}