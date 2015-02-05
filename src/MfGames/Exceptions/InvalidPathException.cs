// <copyright file="InvalidPathException.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Exceptions
{
    using System;

    /// <summary>
    /// Indicates that the given path does not conform to the format
    /// required by the system. This should be an indicator of invalid
    /// characters or something of that manner.
    /// </summary>
    public class InvalidPathException : HierarchicalPathException
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPathException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The MSG.
        /// </param>
        public InvalidPathException(string msg)
            : base(msg)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPathException"/> class.
        /// </summary>
        /// <param name="msg">
        /// The MSG.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        public InvalidPathException(
            string msg,
            Exception e)
            : base(msg,
                e)
        {
        }

        #endregion
    }
}