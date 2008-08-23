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
	/// Implements the arguments for a ticked event.
	/// </summary>
	public class TickArgs
	: EventArgs
	{
		public int Skipped = 0;
		public long LastTick = 0;

		/// <summary>
		/// Contains the number of seconds, as a double, for this
		/// argument.
		/// </summary>
		public double Seconds
		{
			get
			{
				return (double) LastTick / 1000000.0;
			}
		}

		/// <summary>
		/// Calculates a rate, adjusted for seconds. This method takes the
		/// rate that something should happen every second and adjusts it
		/// by the amount of time since the last tick (typically less than
		/// the rate, but potential by more if there are a lot of skipped
		/// cycles).
		/// </summary>
		public int RatePerSecond(int rate)
		{
			double off = (double) LastTick / 1000000.0 * (double) rate;
			return (int) off;
		}

		/// <summary>
		/// Calculates a rate, adjusted for seconds. This method takes the
		/// rate that something should happen every second and adjusts it
		/// by the amount of time since the last tick (typically less than
		/// the rate, but potential by more if there are a lot of skipped
		/// cycles).
		/// </summary>
		public double RatePerSecond(double rate)
		{
			return (double) LastTick / 1000000.0 * (double) rate;
		}
	}
}
