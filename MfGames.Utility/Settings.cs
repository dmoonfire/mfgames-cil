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

namespace MfGames.Utility
{
	/// <summary>
	/// A settings object, or more importantly classes that extend it,
	/// are the core part of the configuration system. The basic
	/// interface only provides for setting and retrieving string
	/// values from a group/name organization. Extending classes may
	/// wrap int, long, or other objects as part of the configuration.
	///
	/// Settings are also nested. Multiple settings may be wrapped
	/// around each other to provide variables. Internally, all
	/// settings are backed by another ISettings object which may be
	/// another Settings object or eventually a ISettingsStore class.
	/// </summary>
	public class Settings
	: ISettings
	{
		#region Constructors
		public Settings(ISettings baseSettings)
		{
			// Valid the variable and save it
			if (baseSettings == null)
				throw new Exception(
					"Cannot back a settings object with a null");

			this.baseSettings = baseSettings;
		}
		#endregion

		#region Backing Properties
		private ISettings baseSettings;
		#endregion

		#region Accessing
		/// <summary>
		/// This is the primary method for getting and setting values
		/// in the system. If a null is set, then the value is
		/// removed.
		/// </summary>
		public string this[string group, string variable]
		{
			get { return baseSettings[group, variable]; }
			set { baseSettings[group, variable] = value; }
		}

		/// <summary>
		/// This method returns true if a specific group/variable
		/// exists within the system.
		/// </summary>
		public bool Contains(string group, string variable)
		{
			return baseSettings.Contains(group, variable);
		}
		#endregion
	}
}
