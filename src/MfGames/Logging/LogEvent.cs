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
	/// Encapsulates the functionality of a logging message.
	/// </summary>
	public class LogEvent : EventArgs
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LogEvent"/> class.
		/// </summary>
		public LogEvent()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LogEvent"/> class.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <param name="severity">The severity.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public LogEvent(string category, Severity severity, string format, params object[] arguments)
		{
			Category = category;
			Message = String.Format(format, arguments);
			Severity = severity;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LogEvent"/> class.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <param name="severity">The severity.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public LogEvent(string category, Severity severity, Exception exception, string format, params object[] arguments)
		{
			Category = category;
			Message = String.Format(format, arguments);
			Exception = exception;
			Severity = severity;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the exception associated with the log event.
		/// </summary>
		/// <value>The exception.</value>
		public Exception Exception { get; set; }

		/// <summary>
		/// Gets or sets the log message.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the severity of the message.
		/// </summary>
		/// <value>The severity.</value>
		public Severity Severity { get; set; }

		/// <summary>
		/// Gets or sets the category.
		/// </summary>
		/// <value>The category.</value>
		public string Category { get; set; }
		
		#endregion
	}
}