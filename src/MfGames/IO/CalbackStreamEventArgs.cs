// <copyright file="CalbackStreamEventArgs.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.IO
{
    using System;
    using System.IO;

    /// <summary>
    /// Encapsulates the event arguments for a callback stream event.
    /// </summary>
    /// <typeparam name="TStream">
    /// The type of the stream.
    /// </typeparam>
    public class CalbackStreamEventArgs<TStream> : EventArgs
        where TStream : Stream
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CalbackStreamEventArgs{TStream}"/> class.
        /// </summary>
        /// <param name="callbackStream">
        /// The callback stream.
        /// </param>
        public CalbackStreamEventArgs(CallbackStream<TStream> callbackStream)
        {
            this.CallbackStream = callbackStream;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the callback stream.
        /// </summary>
        /// <value>
        /// The callback stream.
        /// </value>
        public CallbackStream<TStream> CallbackStream { get; private set; }

        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        /// <value>
        /// The underlying stream.
        /// </value>
        public TStream UnderlyingStream
        {
            get
            {
                return this.CallbackStream.UnderlyingStream;
            }
        }

        #endregion
    }
}