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

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// Implements a very basic countdown feature that lets the
	/// program set a timer, then reduce it by a given amount. The
	/// countdown aspect is intended to be triggered by code, either
	/// using the ITickable interface or programmically.
	/// </summary>
	public class SecondsCountdown : ITickable
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
			original = reset;
		}

		#endregion

		#region Counting Properties

		private double current = 0;
		public EventHandler Finished;
		private double original = 0;
		private bool triggered = false;

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

		private bool isTickable = true;

		#region ITickable Members

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