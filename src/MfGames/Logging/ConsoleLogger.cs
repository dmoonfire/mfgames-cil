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

namespace MfGames.Logging
{
	/// <summary>
	/// A simple console logger that writes out all the log messages to
	/// the error stream.
	/// </summary>
	public class ConsoleLogger : ILogger
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleLogger"/> class.
		/// </summary>
		public ConsoleLogger()
		{
			formatString = "{0,5}: {1}";
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ConsoleLogger"/> class.
		/// </summary>
		/// <param name="formatString">The format string.</param>
		public ConsoleLogger(string formatString)
		{
			FormatString = formatString;
		}

		#endregion

		#region Logging

		private string formatString;

		/// <summary>
		/// Gets or sets the format string.
		/// 
		/// 0: Category
		/// 1: Severity
		/// 2: Message
		/// 3: Timestamp (UTC)
		/// 4: Timestamp (Local)
		/// </summary>
		/// <value>The format string.</value>
		public string FormatString
		{
			get { return formatString; }
			set { formatString = value; }
		}

		/// <summary>
		/// Logs the given log event.
		/// </summary>
		public void Report(
			object sender,
			LogEvent logEvent)
		{
			// Write out the message to the console.
			Console.Error.WriteLine(
				formatString,
				logEvent.Category,
				logEvent.Severity,
				logEvent.Message,
				DateTime.UtcNow,
				DateTime.Now);

			// Add the stack trace if we have an exception
			if (logEvent.Exception != null)
			{
				Console.Error.WriteLine(logEvent.Exception.StackTrace);
			}
		}

		#endregion
	}
}