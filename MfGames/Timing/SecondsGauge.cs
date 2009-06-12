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
	/// A simple guage which attempts to keep track of how many ticks or
	/// activations per second. This is used to calculate the FPS of a
	/// program.
	/// </summary>
	public class SecondsGauge : ITickable
	{
		private readonly int[] buckets;
		private readonly int span = 5;

		public SecondsGauge()
		{
			buckets = new int[span];

			for (int i = 0; i < span; i++)
				buckets[i] = 0;
		}

		public SecondsGauge(int newSpan)
		{
			span = newSpan;
			buckets = new int[span];

			for (int i = 0; i < span; i++)
				buckets[i] = 0;
		}

		#region Counting

		private long lastSecond = DateTime.Now.Second;

		private long loops;

		public bool IsTickable
		{
			get { return true; }
		}

		/// <summary>
		/// Enables the guage to be added directly to a tick manager.
		/// </summary>
		public void OnTick(object sender, TickArgs args)
		{
			Activate();
		}

		public void Activate()
		{
			Activate(1);
		}

		/// <summary>
		/// Activates the counter for the current second.
		/// </summary>
		public void Activate(int amount)
		{
			lock (this)
			{
				// Figure if we need a roll-over
				long now = DateTime.Now.Second;

				if (now != lastSecond)
				{
					// Set up the new time
					lastSecond = now;
					loops++;

					// Move the buckets over
					for (int i = 1; i < span; i++)
					{
						buckets[i] = buckets[i - 1];
					}

					// Reset the last one
					buckets[0] = 0;
				}

				// Add to the bucket
				buckets[0] += amount;
			}
		}

		#endregion

		#region Results

		public double Average
		{
			get
			{
				double total = 0.0;

				for (int i = 1; i < span; i++)
					total += buckets[i];

				return total / span;
			}
		}

		public bool IsFull
		{
			get { return loops >= span; }
		}

		#endregion
	}
}