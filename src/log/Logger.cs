/*
 * Copyright (C) 2005, Moonfire Games
 *
 * This file is part of MfGames.Utility.
 *
 * The MfGames.Utility library is free software; you can redistribute
 * it and/or modify it under the terms of the GNU Lesser General
 * Public License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307
 * USA
 */

namespace MfGames.Utility
{
	using System;

	public abstract class Logger
	{
#region Logger Singleton
		private static ILogSink logger = new ConsoleLogger();

		/// <summary>
		/// Contains the ILogger used for the system. This is a singleton
		/// and cannot be null. Attempting to assign a null will throw an
		/// exception.
		/// </summary>
		public static ILogSink Singleton
		{
			get { return logger; }
			set
			{
				// Check for nulls
				if (value == null)
					throw new UtilityException("Cannot assign a null logger");

				// Set it since we don't have a null
				logger = value;
			}
		}
#endregion

#region Alert
		public static void Alert(string context, string msg,
			params object [] parms)
		{
			Alert(context, null, msg, parms);
		}

		public static void Alert(string context,
			Exception e,
			string msg,
			params object [] parms)
		{
			logger.Log(Severity.Alert, context,
				String.Format(msg, parms), e);
		}

		public static void Alert(Type type,
			string msg,
			params object [] parms)
		{
			Alert(type.ToString(), null, msg, parms);
		}

		public static void Alert(Type type,
			Exception e,
			string msg,
			params object [] parms)
		{
			Alert(type.ToString(), e, msg, parms);
		}
#endregion

#region Debug
		public static void Debug(string context, string msg,
			params object [] parms)
		{
			Debug(context, null, msg, parms);
		}

		public static void Debug(string context,
			Exception e,
			string msg,
			params object [] parms)
		{
			logger.Log(Severity.Debug, context,
				String.Format(msg, parms), e);
		}

		public static void Debug(Type type,
			string msg,
			params object [] parms)
		{
			Debug(type.ToString(), null, msg, parms);
		}

		public static void Debug(Type type,
			Exception e,
			string msg,
			params object [] parms)
		{
			Debug(type.ToString(), e, msg, parms);
		}
#endregion

#region Error
		public static void Error(string context, string msg,
			params object [] parms)
		{
			Error(context, null, msg, parms);
		}

		public static void Error(string context,
			Exception e,
			string msg,
			params object [] parms)
		{
			logger.Log(Severity.Error, context,
				String.Format(msg, parms), e);
		}

		public static void Error(Type type,
			string msg,
			params object [] parms)
		{
			Error(type.ToString(), null, msg, parms);
		}

		public static void Error(Type type,
			Exception e,
			string msg,
			params object [] parms)
		{
			Error(type.ToString(), e, msg, parms);
		}
#endregion

#region Fatal
		public static void Fatal(string context, string msg,
			params object [] parms)
		{
			Fatal(context, null, msg, parms);
		}

		public static void Fatal(string context,
			Exception e,
			string msg,
			params object [] parms)
		{
			logger.Log(Severity.Fatal, context,
				String.Format(msg, parms), e);
		}

		public static void Fatal(Type type,
			string msg,
			params object [] parms)
		{
			Fatal(type.ToString(), null, msg, parms);
		}

		public static void Fatal(Type type,
			Exception e,
			string msg,
			params object [] parms)
		{
			Fatal(type.ToString(), e, msg, parms);
		}
#endregion

#region Info
		public static void Info(string context, string msg,
			params object [] parms)
		{
			Info(context, null, msg, parms);
		}

		public static void Info(string context,
			Exception e,
			string msg,
			params object [] parms)
		{
			logger.Log(Severity.Info, context,
				String.Format(msg, parms), e);
		}

		public static void Info(Type type,
			string msg,
			params object [] parms)
		{
			Info(type.ToString(), null, msg, parms);
		}

		public static void Info(Type type,
			Exception e,
			string msg,
			params object [] parms)
		{
			Info(type.ToString(), e, msg, parms);
		}
#endregion

#region Trace
		public static void Trace(string context,
			string msg,
			params object [] parms)
		{
			Trace(context, null, msg, parms);
		}

		public static void Trace(string context,
			Exception e,
			string msg,
			params object [] parms)
		{
			logger.Log(Severity.Trace, context, msg, e);
		}

		public static void Trace(Type type,
			string msg,
			params object [] parms)
		{
			Trace(type.ToString(), null, msg, parms);
		}

		public static void Trace(Type type,
			Exception e,
			string msg,
			params object [] parms)
		{
			Trace(type.ToString(), e, msg, parms);
		}
#endregion
	}
}
