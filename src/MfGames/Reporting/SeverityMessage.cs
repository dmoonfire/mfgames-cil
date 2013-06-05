// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;
using MfGames.Enumerations;

namespace MfGames.Reporting
{
	/// <summary>
	/// Represents a message with an associated severity. This is similar
	/// to <see cref="Exception"/>, but can represents messages of other
	/// levels.
	/// </summary>
	public class SeverityMessage
	{
		#region Properties

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

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SeverityMessage"/>
		/// class with an info severity.
		/// </summary>
		/// <param name="text">The message text.</param>
		public SeverityMessage(string text)
			: this(Severity.Info, text)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SeverityMessage"/> class.
		/// </summary>
		/// <param name="severity">The severity.</param>
		/// <param name="text">The message text.</param>
		public SeverityMessage(
			Severity severity,
			string text)
		{
			if (String.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("message");
			}

			Severity = severity;
			Text = text;
			Created = DateTime.UtcNow;
		}

		#endregion
	}
}
