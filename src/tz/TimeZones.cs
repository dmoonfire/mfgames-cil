/*
 * Copyright (C) 2006, Moonfire Games
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
using System.Collections;
using System.Globalization;

namespace MfGames.Utility
{
	/// <summary>
	/// TimeZones handles the mapping and loading of System.TimeZone
	/// objects for given names. This is implemented as a large table
	/// worth of data, using a hash table for the lookup.
	/// </summary>
	public class TimeZones
	: Logable
	{
#region Lookup and Parsing
		private static Hashtable zones = new Hashtable();

		/// <summary>
		/// Takes a string in a given format and returns a TimeZone
		/// object for that zone. If there is no such zone, this
		/// throws a UtilityException.
		/// </summary>
		public static TimeZone ToTimeZone(string name)
		{
			// Just try a basic lookup
			TimeZone tz = (TimeZone) zones[name];

			if (tz == null)
				throw new UtilityException("Cannot find time zone: " + name);

			return tz;
		}
#endregion

#region Static Zones
		public static readonly TimeZone Utc =
			new TimeZoneUtc("UTC", 0);
#endregion

#region Static Table Building
		// Static constructor of the tables.
		static TimeZones()
		{
			// US basic time zones
			Register("US/Pacific",  new TimeZoneUS("PST", -8, "PDT", -7));
			Register("US/Mountain", new TimeZoneUS("MST", -7, "MDT", -6));
			Register("US/Central",  new TimeZoneUS("CST", -6, "CDT", -5));
			Register("US/Eastern",  new TimeZoneUS("EST", -5, "EDT", -4));

			// Noise
			Logger.Info(typeof(TimeZones),
				"Loaded time zones table with {0} entries",
				zones.Count);
		}

		/// <summary>
		/// Registers the name of the time zone for lookup.
		/// </summary>
		private static void Register(string tzName, TimeZone tz)
		{
			// Register the basic ones
			zones[tzName] = tz;
			zones[tz.DaylightName] = tz;
			zones[tz.StandardName] = tz;

			// Try converting "/" to "-"
			if (tzName.IndexOf("/") > 0)
			{
				if (tzName.Replace("/", "-") != tzName)
					zones[tzName.Replace("/", "-")] = tz;
				
				// Try converting "/" to " "
				if (tzName.Replace("/", " ") != tzName)
					zones[tzName.Replace("/", " ")] = tz;
			}
		}
#endregion
	}
}
