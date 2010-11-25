#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System;

using MfGames.Enumerations;

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// Implements a low-profile logging sender for the MfGames and related libraries. To
	/// connect to the logging interface, the code just needs to attach to the Log event.
	/// </summary>
	public class Logger : ILogger
	{
		#region Singleton

		private static Logger logger = new Logger();

		/// <summary>
		/// Contains the ILogger used for the system. This is a singleton
		/// and cannot be null. Attempting to assign a null will throw an
		/// exception. Setting this to null will assign a default logger and this
		/// property will never be null.
		/// </summary>
		public static Logger Instance
		{
			get { return logger; }
			set { logger = value ?? new Logger(); }
		}

		#endregion

		#region Eventing

		/// <summary>
		/// Gets a value indicating whether this instance is enabled and at least one listener is registered.
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is enabled; otherwise, <c>false</c>.
		/// </value>
		public bool IsEnabled
		{
			get { return Log != null; }
		}

		/// <summary>
		/// Occurs when a log message is received.
		/// </summary>
		public event EventHandler<LogEventArgs> Log;

		/// <summary>
		/// Reports the specified log message.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="MfGames.Logging.LogEventArgs"/> instance containing the event data.</param>
		public void Report(object sender, LogEventArgs args)
		{
			if (Log != null)
			{
				Log(sender, args);
			}
		}

		/// <summary>
		/// Reports the specified log message, formatting it as needed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="severity">The severity.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Report(object sender, Severity severity, string format, params object[] arguments)
		{
			var args = new LogEventArgs(severity, format, arguments);
			Report(sender, args);
		}

		/// <summary>
		/// Reports the specified log message, formatting it as needed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="severity">The severity.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Report(object sender, Severity severity, Exception exception, string format, params object[] arguments)
		{
			var args = new LogEventArgs(severity, exception, format, arguments);
			Report(sender, args);
		}

		#endregion

		#region Convience Methods

		#region Alert

		/// <summary>
		/// Posts an alert logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Alert(object sender, string format, params object[] arguments)
		{
			Alert(sender, null, format, arguments);
		}

		/// <summary>
		/// Posts an alert logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Alert(object sender, Exception exception, string format, params object[] arguments)
		{
			Report(sender, Severity.Alert, exception, format, arguments);
		}

		#endregion

		#region Debug

		/// <summary>
		/// Posts a debug logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Debug(object sender, string format, params object[] arguments)
		{
			Debug(sender, null, format, arguments);
		}

		/// <summary>
		/// Posts a debug logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Debug(object sender, Exception exception, string format, params object[] arguments)
		{
			Report(sender, Severity.Debug, exception, format, arguments);
		}

		#endregion

		#region Error

		/// <summary>
		/// Posts an error logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Error(object sender, string format, params object[] arguments)
		{
			Error(sender, null, format, arguments);
		}

		/// <summary>
		/// Posts an error logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Error(object sender, Exception exception, string format, params object[] arguments)
		{
			Report(sender, Severity.Error, exception, format, arguments);
		}

		#endregion

		#region Fatal

		/// <summary>
		/// Posts a fatal logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Fatal(object sender, string format, params object[] arguments)
		{
			Fatal(sender, null, format, arguments);
		}

		/// <summary>
		/// Posts a fatal logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Fatal(object sender, Exception exception, string format, params object[] arguments)
		{
			Report(sender, Severity.Fatal, exception, format, arguments);
		}

		#endregion

		#region Info

		/// <summary>
		/// Posts an info logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Info(object sender, string format, params object[] arguments)
		{
			Info(sender, null, format, arguments);
		}

		/// <summary>
		/// Posts an info logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Info(object sender, Exception exception, string format, params object[] arguments)
		{
			Report(sender, Severity.Info, exception, format, arguments);
		}

		#endregion

		#region Trace

		/// <summary>
		/// Posts a trace logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Trace(object sender, string format, params object[] arguments)
		{
			Trace(sender, null, format, arguments);
		}

		/// <summary>
		/// Posts a trace logging mesage.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Trace(object sender, Exception exception, string format, params object[] arguments)
		{
			Report(sender, Severity.Trace, exception, format, arguments);
		}

		#endregion

		#endregion
	}
}