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
	/// Implements a very basic countdown feature that lets the
	/// program set a timer, then reduce it by a given amount. The
	/// countdown aspect is intended to be triggered by code, either
	/// using the ITickable interface or programmically.
	/// </summary>
	public class SecondsCountdown
	: ITickable
	{
		#region Constructors
		/// <summary>
		/// Creates a countdown initialized to zero.
		/// </summary>
		public SecondsCountdown()
		{
		}

		/// <summary>
		/// Creates a countdown at a given seconds and sets the reset
		/// to the same value.
		/// </summary>
		public SecondsCountdown(double seconds)
		{
			current = original = seconds;
		}

		/// <summary>
		/// Creates a countdown and sets the current and reset values
		/// appropriate.
		/// </summary>
		public SecondsCountdown(double current, double reset)
		{
			this.current = current;
			this.original = reset;
		}
		#endregion

		#region Counting Properties
		private double current = 0;
		private double original = 0;
		private bool triggered = false;

		public EventHandler Finished;

		/// <summary>
		/// Contains the current number of seconds, as a double, left
		/// in the countdown.
		/// </summary>
		public double CurrentSeconds
		{
			get { return current; }
			set
			{
				// Set the value
				current = value;

				// Check the timer and fire an event if we are done
				if (!triggered && current <= 0)
				{
					// Reset our trigger
					triggered = true;

					// See if we need to fire the event
					if (Finished != null)
						Finished(this, new EventArgs());
				}
			}
		}

		/// <summary>
		/// Returns true if the current countdown has been triggered.
		/// </summary>
		public bool IsTriggered
		{
			get { return triggered; }
			set { triggered = value; }
		}

		/// <summary>
		/// Contains the number of seconds if the countdown is reset.
		/// </summary>
		public double ResetSeconds
		{
			get { return original; }
			set { original = value; }
		}
		
		/// <summary>
		/// Resets the counter to the ResetSeconds value.
		/// </summary>
		public void Reset()
		{
			current = original;
			triggered = false;
		}
		#endregion

		#region ITickable Members
		private bool isTickable = true;

		/// <summary>
		/// If this is true, then then countdown listens to
		/// ticks. Setting this to false will pause the countdown.
		/// </summary>
		public bool IsTickable
		{ 
			get { return isTickable; }
			set { isTickable = value; }
		}

		/// <summary>
		/// Event fired when a tick is found.
		/// </summary>
		public void OnTick(object sender, TickArgs args)
		{
			CurrentSeconds -= args.Seconds;
		}
		#endregion
	}
}
