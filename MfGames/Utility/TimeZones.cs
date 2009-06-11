#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System;
using System.Collections;

using MfGames.Logging;

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// TimeZones handles the mapping and loading of System.TimeZone
	/// objects for given names. This is implemented as a large table
	/// worth of data, using a hash table for the lookup.
	/// </summary>
	public class TimeZones : Logable
	{
		#region Lookup and Parsing

		private static readonly Hashtable zones = new Hashtable();

		/// <summary>
		/// Takes a string in a given format and returns a TimeZone
		/// object for that zone. If there is no such zone, this
		/// throws a UtilityException.
		/// </summary>
		public static TimeZone ToTimeZone(string name)
		{
			// Just try a basic lookup
			var tz = (TimeZone) zones[name];

			if (tz == null)
				throw new UtilityException("Cannot find time zone: " + name);

			return tz;
		}

		#endregion

		#region Static Zones

		public static readonly TimeZone Utc = new TimeZoneUtc("UTC", 0);

		#endregion

		#region Static Table Building

		// Static constructor of the tables.
		static TimeZones()
		{
			// UTC timezone
			zones["UTC"] = Utc;

			// US basic time zones
			Register("US/Pacific", new TimeZoneUS("PST", -8, "PDT", -7));
			Register("US/Mountain", new TimeZoneUS("MST", -7, "MDT", -6));
			Register("US/Central", new TimeZoneUS("CST", -6, "CDT", -5));
			Register("US/Eastern", new TimeZoneUS("EST", -5, "EDT", -4));

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
			if (tzName.Contains("/"))
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