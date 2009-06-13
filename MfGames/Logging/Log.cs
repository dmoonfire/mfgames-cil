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

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// A log object appropriate for embedding in other objects, such as
	/// static classes. This is a read-only class with a number of
	/// constructors.
	/// </summary>
	[Serializable]
	public class Log
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Log"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public Log(object context)
		{
			this.context = context;
		}

		#endregion

		#region Properties

		private readonly object context;

		/// <summary>
		/// Contains the current log context for this log object.
		/// </summary>
		public object LogContext
		{
			get { return context; }
		}

		#endregion

		public void Alert(string msg, params object[] parms)
		{
			Logger.Alert(LogContext, String.Format(msg, parms));
		}

		public void Alert(Exception e, string msg, params object[] parms)
		{
			Logger.Alert(LogContext, e, String.Format(msg, parms));
		}

		public static void Alert(Type type, string msg, params object[] parms)
		{
			Logger.Alert(type.ToString(), String.Format(msg, parms));
		}

		public void Debug(string msg, params object[] parms)
		{
			Logger.Debug(LogContext, msg, parms);
		}

		public void Debug(Exception e, string msg, params object[] parms)
		{
			Logger.Debug(LogContext, e, msg, parms);
		}

		public static void Debug(Type type, string msg, params object[] parms)
		{
			Logger.Debug(type.ToString(), msg, parms);
		}

		public void Error(string msg, params object[] parms)
		{
			Logger.Error(LogContext, msg, parms);
		}

		public void Error(Exception e, string msg, params object[] parms)
		{
			Logger.Error(LogContext, e, msg, parms);
		}

		public static void Error(Type type, string msg, params object[] parms)
		{
			Logger.Error(type.ToString(), msg, parms);
		}

		public void Fatal(string msg, params object[] parms)
		{
			Logger.Fatal(LogContext, String.Format(msg, parms));
		}

		public void Fatal(Exception e, string msg, params object[] parms)
		{
			Logger.Fatal(LogContext, e, msg, parms);
		}

		public static void Fatal(Type type, string msg, params object[] parms)
		{
			Logger.Fatal(type.ToString(), String.Format(msg, parms));
		}

		public void Info(string msg, params object[] parms)
		{
			Logger.Info(LogContext, msg, parms);
		}

		public void Info(Exception e, string msg, params object[] parms)
		{
			Logger.Info(LogContext, e, msg, parms);
		}

		public static void Info(Type type, string msg, params object[] parms)
		{
			Logger.Info(type.ToString(), String.Format(msg, parms));
		}

		public void Trace(string msg, params object[] parms)
		{
			Logger.Trace(LogContext, msg, parms);
		}

		public void Trace(Exception e, string msg, params object[] parms)
		{
			Logger.Trace(LogContext, e, msg, parms);
		}

		public static void Trace(Type type, string msg, params object[] parms)
		{
			Logger.Trace(type.ToString(), String.Format(msg, parms));
		}
	}
}