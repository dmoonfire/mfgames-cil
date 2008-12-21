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

using System;

namespace MfGames.Logging
{
	/// <summary>
	/// A simple console logger that writes out all the log messages to
	/// the error stream.
	/// </summary>
	public class ConsoleLogger : ILogSink
	{
		/// <summary>
		/// Writes out a log message at the given severity.
		/// </summary>
		public void Log(Severity level, string context, string msg, Exception e)
		{
			// Write out the message to standard error
			Console.Error.WriteLine("{0,5}: {1}: {2}", level, context, msg);

			// Add the stack trace if we have an exception
			if (e != null)
			{
				Console.Error.WriteLine(e.StackTrace);
			}
		}
	}
}
