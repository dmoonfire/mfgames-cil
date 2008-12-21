#if REMOVED
#region Copyright
/*
 * Copyright (C) 2005-2008, Moonfire Games
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
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

namespace MfGames.Utility
{
	using log4net;
	using System;

	/// <summary>
	/// Implements a basic sink that writes to the
	/// System.Diagnostics.Trace and System.Diagnostics.Debug instead
	/// of to the console.
	/// </summary>
	public class DiagnosticSink : ILogSink
	{
		/// <summary>
		/// Implements the only logging function in the ILogger
		/// interface. This takes the given levels and generates the
		/// proper trace
		/// </summary>
		public void Log(
		        Severity level,
		        string context,
		        string msg,
		        Exception e)
		{
			// Figure out the switch for this context
			// Figure get the logger object for the context
			ILog log = LogManager.GetLogger(context);

			// Switch based on the level, since we can't access a log4net
			// centralized logging method from here.
			switch (level)
			{
			case Severity.Fatal:

				if (log.IsFatalEnabled)
					log.Fatal(msg, e);

				break;
			case Severity.Error:

				if (log.IsErrorEnabled)
					log.Error(msg, e);

				break;
			case Severity.Alert:

				if (log.IsWarnEnabled)
					log.Warn(msg, e);

				break;
			case Severity.Info:

				if (log.IsInfoEnabled)
					log.Info(msg, e);

				break;
			case Severity.Debug:
			case Severity.Trace:
			default:

				if (log.IsDebugEnabled)
					log.Debug(msg, e);

				break;
			}
		}
	}
}
#endif