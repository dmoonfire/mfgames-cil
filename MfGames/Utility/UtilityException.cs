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
	using System;

	/// <summary>
	/// Top-level for all utility exceptions. This indicates a general
	/// problem with the Utility library or one of the methods inside it.
	/// </summary>
	public class UtilityException : Exception
	{
		public UtilityException(string msg)
			: base(msg)
		{
		}

		public UtilityException(string msg, Exception e)
			: base(msg, e)
		{
		}
	}
}
