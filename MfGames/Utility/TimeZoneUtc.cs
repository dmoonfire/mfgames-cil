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
	/// Implements the basic functionality of a TimeZone for this
	/// library. This allows for the creation of specific time zones
	/// that follow the basic UTC format (no daylight saving time).
	/// </summary>
	public class TimeZoneUtc : TimeZone
	{
		private string name;
		private int offset = 0;

		/// <summary>
		/// Creates the basic UTC zone with an offset.
		/// </summary>
		public TimeZoneUtc(string shortName, int offset)
		{
			this.offset = offset;
			name = shortName;
		}

		/// <summary>
		/// Returns the name of the daylight name.
		/// </summary>
		public override string DaylightName
		{
			get { return name; }
		}

		/// <summary>
		/// Returns the name of the standard time.
		/// </summary>
		public override string StandardName
		{
			get { return name; }
		}

		/// <summary>
		/// Returns the daylight changes for UTC (never).
		/// </summary>
		public override DaylightTime GetDaylightChanges(int year)
		{
			return new DaylightTime(new DateTime(1),
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