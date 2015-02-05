// <copyright file="DateTimeExtensions.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;

namespace MfGames.Extensions.System
{
	/// <summary>
	/// Contains various extension methods for System.DateTime.
	/// </summary>
	public static class DateTimeExtensions
	{
		#region Public Methods and Operators

		/// <summary>
		/// Converts a given DateTime into the Julian Date.
		/// </summary>
		/// <param name="dateTime">The DateTime to convert into Julian Date.</param>
		/// <returns>The resulting Julian Date as a double.</returns>
		public static double ToJulianDate(this DateTime dateTime)
		{
			// http://stackoverflow.com/questions/5248827/convert-datetime-to-julian-date-in-c-sharp-tooadate-safe
			double results = dateTime.ToOADate() + 2415018.5;
			return results;
		}

		/// <summary>
		/// Converts a given DateTime into the Julian Date.
		/// </summary>
		/// <param name="dateTime">The DateTime to convert into Julian Date.</param>
		/// <returns>The resulting Julian Date as a decimal.</returns>
		public static decimal ToJulianDateDecimal(this DateTime dateTime)
		{
			// http://stackoverflow.com/questions/5248827/convert-datetime-to-julian-date-in-c-sharp-tooadate-safe
			decimal results = (decimal)dateTime.ToOADate() + 2415018.5m;
			return results;
		}

		#endregion
	}
}
