// <copyright file="SeverityMessageEventArgs.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Reporting
{
    using System;

    /// <summary>
    /// Wraps a severity message in an event argument class.
    /// </summary>
    public class SeverityMessageEventArgs : EventArgs
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SeverityMessageEventArgs"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public SeverityMessageEventArgs(SeverityMessage message)
        {
            this.Message = message;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the message associated with this event.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public SeverityMessage Message { get; protected set; }

        #endregion
    }
}