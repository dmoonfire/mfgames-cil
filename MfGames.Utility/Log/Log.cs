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
using System.IO;
using System.Reflection;

namespace MfGames.Utility
{
	/// <summary>
	/// A log object appropriate for embedding in other objects, such as
	/// static classes. This is a read-only class with a number of
	/// constructors.
	/// </summary>
	[Serializable]
    public class Log : Logable
	{
		#region Constructors
		public Log(string context)
		{
			this.context = context;
		}

		public Log(Type type)
			: this(type.ToString())
		{
		}

		public Log(object obj)
			: this(obj.GetType().ToString())
		{
		}
		#endregion

		#region Properties
		private string context;

		/// <summary>
		/// Contains the current log context for this log object.
		/// </summary>
		public override string LogContext
		{
			get { return context; }
			//set { context = value ?? ""; }
		}
		#endregion
	}
}
