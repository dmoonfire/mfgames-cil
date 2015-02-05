// <copyright file="HierarchicalPathException.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Exceptions
{
    using System;

    /// <summary>
    /// Represents an exception while processing HierarchicalPath objects.
    /// </summary>
    public class HierarchicalPathException : Exception
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalPathException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public HierarchicalPathException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HierarchicalPathException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public HierarchicalPathException(
            string message,
            Exception exception)
            : base(message,
                exception)
        {
        }

        #endregion
    }
}