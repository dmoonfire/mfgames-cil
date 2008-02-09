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
using System.Globalization;

namespace MfGames.Utility
{
	/// <summary>
	/// Implements the basic functionality of a TimeZone for this
	/// library. This allows for the creation of specific time zones
	/// that follow the basic UTC format (no daylight saving time).
	/// </summary>
	public class TimeZoneUtc
	: TimeZone
	{
		private int offset = 0;
		private string name;

		/// <summary>
		/// Creates the basic UTC zone with an offset.
		/// </summary>
		public TimeZoneUtc(string shortName, int offset)
		{
			this.offset = offset;
			this.name = shortName;
		}

		/// <summary>
		/// Returns the name of the daylight name.
		/// </summary>
		public override string DaylightName { get { return name; } }

		/// <summary>
		/// Returns the name of the standard time.
		/// </summary>
		public override string StandardName { get { return name; } }

		/// <summary>
		/// Returns the daylight changes for UTC (never).
		/// </summary>
		public override DaylightTime GetDaylightChanges(int year)
		{
			return new DaylightTime(
				new DateTime(1),
				new DateTime(2),
				new TimeSpan(0, 0, 0));
		}

		/// <summary>
		/// Returns the UTC offset for this time zone.
		/// </summary>
		public override TimeSpan GetUtcOffset(DateTime when)
		{
			return new TimeSpan(offset, 0, 0);
		}
	}
}
