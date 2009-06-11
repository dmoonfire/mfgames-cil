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
	/// Implements a low-profile logging context for the MfGames and related libraries. To
	/// connect to the logging interface, one must just implement an ILogSink and assign it
	/// to Logger.Instance.
	/// </summary>
	public static class Logger
	{
		#region Logger Singleton

		private static ILogSink logger;

		/// <summary>
		/// Contains the ILogger used for the system. This is a singleton
		/// and cannot be null. Attempting to assign a null will throw an
		/// exception.
		/// </summary>
		public static ILogSink Instance
		{
			get { return logger; }
			set
			{
				// Set it since we don't have a null
				logger = value;
			}
		}

		#endregion

		#region Alert

		public static void Alert(string context, string msg, params object[] parms)
		{
			Alert(context, null, msg, parms);
		}

		public static void Alert(
			string context, Exception e, string msg, params object[] parms)
		{
			if (logger != null)
			{
				logger.Log(Severity.Alert, context, String.Format(msg, parms), e);
			}
		}

		public static void Alert(Type type, string msg, params object[] parms)
		{
			Alert(type.ToString(), null, msg, parms);
		}

		public static void Alert(
			Type type, Exception e, string msg, params object[] parms)
		{
			Alert(type.ToString(), e, msg, parms);
		}

		#endregion

		#region Debug

		public static void Debug(string context, string msg, params object[] parms)
		{
			Debug(context, null, msg, parms);
		}

		public static void Debug(
			string context, Exception e, string msg, params object[] parms)
		{
			if (logger != null)
			{
				logger.Log(Severity.Debug, context, String.Format(msg, parms), e);
			}
		}

		public static void Debug(Type type, string msg, params object[] parms)
		{
			Debug(type.ToString(), null, msg, parms);
		}

		public static void Debug(
			Type type, Exception e, string msg, params object[] parms)
		{
			Debug(type.ToString(), e, msg, parms);
		}

		#endregion

		#region Error

		public static void Error(string context, string msg, params object[] parms)
		{
			Error(context, null, msg, parms);
		}

		public static void Error(
			string context, Exception e, string msg, params object[] parms)
		{
			if (logger != null)
			{
				logger.Log(Severity.Error, context, String.Format(msg, parms), e);
			}
		}

		public static void Error(Type type, string msg, params object[] parms)
		{
			Error(type.ToString(), null, msg, parms);
		}

		public static void Error(
			Type type, Exception e, string msg, params object[] parms)
		{
			Error(type.ToString(), e, msg, parms);
		}

		#endregion

		#region Fatal

		public static void Fatal(string context, string msg, params object[] parms)
		{
			Fatal(context, null, msg, parms);
		}

		public static void Fatal(
			string context, Exception e, string msg, params object[] parms)
		{
			if (logger != null)
			{
				logger.Log(Severity.Fatal, context, String.Format(msg, parms), e);
			}
		}

		public static void Fatal(Type type, string msg, params object[] parms)
		{
			Fatal(type.ToString(), null, msg, parms);
		}

		public static void Fatal(
			Type type, Exception e, string msg, params object[] parms)
		{
			Fatal(type.ToString(), e, msg, parms);
		}

		#endregion

		#region Info

		public static void Info(string context, string msg, params object[] parms)
		{
			Info(context, null, msg, parms);
		}

		public static void Info(
			string context, Exception e, string msg, params object[] parms)
		{
			if (logger != null)
			{
				logger.Log(Severity.Info, context, String.Format(msg, parms), e);
			}
		}

		public static void Info(Type type, string msg, params object[] parms)
		{
			Info(type.ToString(), null, msg, parms);
		}

		public static void Info(
			Type type, Exception e, string msg, params object[] parms)
		{
			Info(type.ToString(), e, msg, parms);
		}

		#endregion

		#region Trace

		public static void Trace(string context, string msg, params object[] parms)
		{
			Trace(context, null, msg, parms);
		}

		public static void Trace(
			string context, Exception e, string msg, params object[] parms)
		{
			if (logger != null)
			{
				logger.Log(Severity.Trace, context, msg, e);
			}
		}

		public static void Trace(Type type, string msg, params object[] parms)
		{
			Trace(type.ToString(), null, msg, parms);
		}

		public static void Trace(
			Type type, Exception e, string msg, params object[] parms)
		{
			Trace(type.ToString(), e, msg, parms);
		}

		#endregion
	}
}