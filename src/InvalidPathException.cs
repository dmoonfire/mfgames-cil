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
  
	/// <summary>
	/// Indicates that the given path does not conform to the format
	/// required by the system. This should be an indicator of invalid
	/// characters or something of that manner.
	/// </summary>
	public class InvalidPathException : UtilityException
	{
		public InvalidPathException(string msg)
			: base(msg)
		{
		}

		public InvalidPathException(string msg, Exception e)
			: base(msg, e)
		{
		}
	}
}
