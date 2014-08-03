// <copyright file="SystemCollectionsGenericEnumerableExtensions.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Extensions.System.Collections.Generic
{
    using global::System;

    using global::System.Collections.Generic;

    /// <summary>
    /// Extension methods for IEnumerable&lt;string&gt;.
    /// </summary>
    public static class SystemCollectionsGenericEnumerableExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Joins the strings into a single one.
        /// </summary>
        /// <param name="input">
        /// The input lines.
        /// </param>
        /// <returns>
        /// A single line joined by newlines.
        /// </returns>
        public static string Join(this IEnumerable<string> input)
        {
            return string.Join(Environment.NewLine, input);
        }

        #endregion
    }
}