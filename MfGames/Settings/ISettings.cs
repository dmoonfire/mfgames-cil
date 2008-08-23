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

namespace MfGames.Settings
{
	/// <summary>
	/// Describes the common interface for various setting objects.
	/// </summary>
	public interface ISettings
	{
		#region Accessors
		// This is the primary method for getting and setting values
		// in the system. If a null is set, then the value is
		// removed.
		string this[string group, string variable]
		{
			get;
			set;
		}

		// This method returns true if a specific group/variable
		// exists within the system.
		bool Contains(string group, string variable);
		#endregion
	}
}
