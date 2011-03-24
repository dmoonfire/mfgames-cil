#region Copyright and License

// Copyright (c) 2005-2011, Moonfire Games
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

#endregion

namespace MfGames.Reporting
{
	/// <summary>
	/// A formatter class that creates severity messages from logs and reports
	/// them.
	/// </summary>
	public class Logger
	{
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

		#region Logging

		private readonly object context;

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
			var message = new SeverityMessage(
				severity, 
				String.Format(format, arguments));
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
	}
}