// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

#region Namespaces

using System;

#endregion

namespace MfGames.Reporting
{
	/// <summary>
	/// Wraps a severity message in an event argument class.
	/// </summary>
	public class SeverityMessageEventArgs: EventArgs
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SeverityMessageEventArgs"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public SeverityMessageEventArgs(SeverityMessage message)
		{
			Message = message;
		}

		#endregion

		#region Properties

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
