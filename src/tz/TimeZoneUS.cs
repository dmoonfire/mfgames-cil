using System;
using System.Globalization;

namespace MfGames.Utility
{
	/// <summary>
	/// Implements the US-specific time zones.
	/// </summary>
	public class TimeZoneUS
	: TimeZone
	{
		private int stOffset;
		private int dtOffset;
		private string stName;
		private string dtName;

		/// <summary>
		/// Creates the basic US zone with the three-letter names.
		/// </summary>
		public TimeZoneUS(
			string standardName, int standardOffset,
			string daylightName, int daylightOffset)
		{
			this.stOffset = standardOffset;
			this.dtOffset = daylightOffset;
			this.stName = standardName;
			this.dtName = daylightName;
		}

		/// <summary>
		/// Returns the name of the daylight name.
		/// </summary>
		public override string DaylightName { get { return dtName; } }

		/// <summary>
		/// Returns the name of the standard time.
		/// </summary>
		public override string StandardName { get { return stName; } }

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
			return new DaylightTime(start, end,
				new TimeSpan(stOffset - dtOffset, 0, 0));
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
