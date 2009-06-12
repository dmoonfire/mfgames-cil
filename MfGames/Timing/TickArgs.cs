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
	/// Implements the arguments for a ticked event.
	/// </summary>
	public class TickArgs : EventArgs
	{
		public long LastTick;
		public int Skipped;

		/// <summary>
		/// Contains the number of seconds, as a double, for this
		/// argument.
		/// </summary>
		public double Seconds
		{
			get { return LastTick / 1000000.0; }
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
			double off = LastTick / 1000000.0 * rate;
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
			return LastTick / 1000000.0 * rate;
		}
	}
}