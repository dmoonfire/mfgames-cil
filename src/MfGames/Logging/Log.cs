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

using MfGames.Utility.Annotations;

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// A log object appropriate for embedding in other objects, such as
	/// static classes. This is a read-only class with a number of
	/// constructors.
	/// </summary>
	public class Log
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Log"/> class.
		/// </summary>
		/// <param name="context">The sender.</param>
		public Log(object context)
		{
			Sender = context;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Contains the current log sender for logs sent out by this container.
		/// </summary>
		public object Sender { get; private set; }

		#endregion

		/// <summary>
		/// Sends an alert message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Alert(string format, params object[] arguments)
		{
			Logger.Instance.Alert(Sender, format, arguments);
		}

		/// <summary>
		/// Sends an alert message.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Alert(Exception exception, string format, params object[] arguments)
		{
			Logger.Instance.Alert(Sender, exception, format, arguments);
		}

		/// <summary>
		/// Sends a debug message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Debug(string format, params object[] arguments)
		{
			Logger.Instance.Debug(Sender, format, arguments);
		}

		/// <summary>
		/// Sends a debug message.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Debug(Exception exception, string format, params object[] arguments)
		{
			Logger.Instance.Debug(Sender, exception, format, arguments);
		}

		/// <summary>
		/// Sends an error message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Error(string format, params object[] arguments)
		{
			Logger.Instance.Error(Sender, format, arguments);
		}

		/// <summary>
		/// Sends an error message.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Error(Exception exception, string format, params object[] arguments)
		{
			Logger.Instance.Error(Sender, exception, format, arguments);
		}

		/// <summary>
		/// Sends a fatal message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Fatal(string format, params object[] arguments)
		{
			Logger.Instance.Fatal(Sender, format, arguments);
		}

		/// <summary>
		/// Sends a fatal message.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Fatal(Exception exception, string format, params object[] arguments)
		{
			Logger.Instance.Fatal(Sender, exception, format, arguments);
		}

		/// <summary>
		/// Sends an info message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Info(string format, params object[] arguments)
		{
			Logger.Instance.Info(Sender, format, arguments);
		}

		/// <summary>
		/// Sends an info message.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Info(Exception exception, string format, params object[] arguments)
		{
			Logger.Instance.Info(Sender, exception, format, arguments);
		}

		/// <summary>
		/// Sends a trace message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Trace(string format, params object[] arguments)
		{
			Logger.Instance.Trace(Sender, format, arguments);
		}

		/// <summary>
		/// Sends a trace message.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		[StringFormatMethod("format")]
		public void Trace(Exception exception, string format, params object[] arguments)
		{
			Logger.Instance.Trace(Sender, exception, format, arguments);
		}
	}
}