// <copyright file="SeverityMessage.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Reporting
{
    using System;

    using MfGames.Enumerations;

    /// <summary>
    /// Represents a message with an associated severity. This is similar
    /// to <see cref="Exception"/>, but can represents messages of other
    /// levels.
    /// </summary>
    public class SeverityMessage
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SeverityMessage"/>
        /// class with an info severity.
        /// </summary>
        /// <param name="text">
        /// The message text.
        /// </param>
        public SeverityMessage(string text)
            : this(Severity.Info,
                text)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SeverityMessage"/> class.
        /// </summary>
        /// <param name="severity">
        /// The severity.
        /// </param>
        /// <param name="text">
        /// The message text.
        /// </param>
        public SeverityMessage(
            Severity severity,
            string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException("message");
            }

            this.Severity = severity;
            this.Text = text;
            this.Created = DateTime.UtcNow;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets when the message was created.
        /// </summary>
        public DateTime Created { get; protected set; }

        /// <summary>
        /// Contains the severity of the message.
        /// </summary>
        public Severity Severity { get; protected set; }

        /// <summary>
        /// Contains the message text.
        /// </summary>
        public string Text { get; protected set; }

        #endregion
    }
}