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

using System;
using System.IO;
using System.Reflection;

namespace MfGames.Utility
{
	/// <summary>
	/// Collects the basic functionality for logging on a class level into
	/// a single class that all extending classes may use. These methods
	/// are also public to enable other functions to initiate logs. The logging
	/// is acutally performed by the Logger singleton in this package.
	/// </summary>
	[Serializable]
	public abstract class Logable
	{
		public virtual string LogContext
		{
			get { return GetType().FullName; }
		}
        
		public void Alert(string msg, params object [] parms)
		{
			Logger.Alert(LogContext, String.Format(msg, parms));
		}

		public void Alert(Exception e, string msg, params object [] parms)
		{
			Logger.Alert(LogContext, e, String.Format(msg, parms));
		}

		public static void Alert(Type type, string msg, params object [] parms)
		{
			Logger.Alert(type.ToString(), String.Format(msg, parms));
		}

		public void Debug(string msg, params object [] parms)
		{
			Logger.Debug(LogContext, msg, parms);
		}
        
		public void Debug(Exception e, string msg, params object [] parms)
		{
			Logger.Debug(LogContext, e, msg, parms);
		}
        
		public static void Debug(Type type, string msg, params object [] parms)
		{
			Logger.Debug(type.ToString(), msg, parms);
		}
        
		public void Error(string msg, params object [] parms)
		{
			Logger.Error(LogContext, msg, parms);
		}
        
		public void Error(Exception e, string msg, params object [] parms)
		{
			Logger.Error(LogContext, e, msg, parms);
		}
        
		public static void Error(Type type, string msg, params object [] parms)
		{
			Logger.Error(type.ToString(), msg, parms);
		}
        
		public void Fatal(string msg, params object [] parms)
		{
			Logger.Fatal(LogContext, String.Format(msg, parms));
		}
        
		public void Fatal(Exception e, string msg, params object [] parms)
		{
			Logger.Fatal(LogContext, e, msg, parms);
		}
        
		public static void Fatal(Type type, string msg, params object [] parms)
		{
			Logger.Fatal(type.ToString(), String.Format(msg, parms));
		}
        
		public void Info(string msg, params object [] parms)
		{
			Logger.Info(LogContext, msg, parms);
		}
        
		public void Info(Exception e, string msg, params object [] parms)
		{
			Logger.Info(LogContext, e, msg, parms);
		}
        
		public static void Info(Type type, string msg, params object [] parms)
		{
			Logger.Info(type.ToString(), String.Format(msg, parms));
		}
        
		public void Trace(string msg, params object [] parms)
		{
			Logger.Trace(LogContext, msg, parms);
		}

		public void Trace(Exception e, string msg, params object [] parms)
		{
			Logger.Trace(LogContext, e, msg, parms);
		}

		public static void Trace(Type type, string msg, params object [] parms)
		{
			Logger.Trace(type.ToString(), String.Format(msg, parms));
		}
	}
}
