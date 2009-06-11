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
using System.Globalization;

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// Implements the US-specific time zones.
	/// </summary>
	public class TimeZoneUS : TimeZone
	{
		private string dtName;
		private int dtOffset;
		private string stName;
		private int stOffset;

		/// <summary>
		/// Creates the basic US zone with the three-letter names.
		/// </summary>
		public TimeZoneUS(
			string standardName,
			int standardOffset,
			string daylightName,
			int daylightOffset)
		{
			stOffset = standardOffset;
			dtOffset = daylightOffset;
			stName = standardName;
			dtName = daylightName;
		}

		/// <summary>
		/// Returns the name of the daylight name.
		/// </summary>
		public override string DaylightName
		{
			get { return dtName; }
		}

		/// <summary>
		/// Returns the name of the standard time.
		/// </summary>
		public override string StandardName
		{
			get { return stName; }
		}

		/// <summary>
		/// Returns the daylight changes for UTC (never).
		/// </summary>
		public override DaylightTime GetDaylightChanges(int year)
		{
			// We need a start and stop time
			DateTime start, end;
			Calendar cal = new GregorianCalendar();

			// 2007+ at this point has a slightly different start/stop
			if (year >= 2007)
			{
				start = new DateTime(year, 4, 1, 2, 0, 0);
				end = new DateTime(year, 11, 1, 2, 0, 0);
			}
			else
			{
				start = new DateTime(year, 3, 1, 2, 0, 0);
				end = new DateTime(year, 12, 1, 2, 0, 0);
			}

			// Move forward for the start date until we have a sunday
			while (cal.GetDayOfWeek(start) != DayOfWeek.Sunday)
				start = cal.AddDays(start, 1);

			// Move backwards from the end date to the last sunday
			while (cal.GetDayOfWeek(end) != DayOfWeek.Sunday)
				start = cal.AddDays(end, 1);

			// Create it
			return new DaylightTime(start, end, new TimeSpan(stOffset - dtOffset, 0, 0));
		}

		/// <summary>
		/// Returns the UTC offset for this time zone.
		/// </summary>
		public override TimeSpan GetUtcOffset(DateTime when)
		{
			return new TimeSpan(stOffset, 0, 0);
		}
	}
}