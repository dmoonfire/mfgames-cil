// <copyright file="SeverityMessageEventArgs.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;

namespace MfGames.Reporting
{
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
			Message = message;
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
