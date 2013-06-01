// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;

namespace MfGames.Reporting
{
	/// <summary>
	/// A formatter class that creates severity messages from logs and reports
	/// them.
	/// </summary>
	public class Logger
	{
		#region Methods

		/// <summary>
		/// Logs an alert message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Alert(
			string format,
			params object[] arguments)
		{
			Log(Severity.Alert, format, arguments);
		}

		/// <summary>
		/// Logs a debug message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Debug(
			string format,
			params object[] arguments)
		{
			Log(Severity.Debug, format, arguments);
		}

		/// <summary>
		/// Logs an error message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Error(
			string format,
			params object[] arguments)
		{
			Log(Severity.Error, format, arguments);
		}

		/// <summary>
		/// Logs a fatal message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Fatal(
			string format,
			params object[] arguments)
		{
			Log(Severity.Fatal, format, arguments);
		}

		/// <summary>
		/// Logs an info message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Info(
			string format,
			params object[] arguments)
		{
			Log(Severity.Info, format, arguments);
		}

		/// <summary>
		/// Creates a severity message with a given message and logs it using
		/// the instance's context.
		/// </summary>
		/// <param name="severity">The severity.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Log(
			Severity severity,
			string format,
			params object[] arguments)
		{
			var message = new SeverityMessage(severity, String.Format(format, arguments));
			Log(message);
		}

		/// <summary>
		/// Logs the specified message to the logging subsystem.
		/// </summary>
		/// <param name="message">The message.</param>
		public void Log(SeverityMessage message)
		{
			LogManager.Log(context, message);
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Logger"/> class.
		/// </summary>
		public Logger()
			: this(null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Logger"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public Logger(object context)
		{
			this.context = context;
		}

		#endregion

		#region Fields

		private readonly object context;

		#endregion
	}
}
