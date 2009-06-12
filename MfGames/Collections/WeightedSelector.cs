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
using System.Collections;

using MfGames.Entropy;
using MfGames.Logging;

#endregion

namespace MfGames.Collections
{
	/// <summary>
	/// This class contains a list of keys and their weights. It allows
	/// a random selection of elements, based on the weights.
	/// </summary>
	public class WeightedSelector
	{
		#region Operators

		public int this[object key]
		{
			get
			{
				// Do some sanity checking
				if (key == null)
					return 0;

				// Find the weight, but make sure we always return an integer
				if (weights[key] == null)
					return 0;
				else
					return (int) weights[key];
			}

			set
			{
				// Make sure we are not negative
				if (key == null)
					throw new Exception("Cannot assign a weight to a null key");

				if (value < 0)
					throw new Exception("Cannot assign a negative weight to " + key);

				// Get the previous key and remove its weight
				int w = this[key];
				total -= w;

				// Assign the new one
				weights[key] = value;
				total += value;
			}
		}

		#endregion

		#region Random

		public object RandomObject
		{
			get
			{
				// Just a random element, based on weights
				int sel = RandomManager.Next(0, (int) total);

				return Select(sel);
			}
		}

		#endregion

		#region Weights

		// Contains a hashtable of weights
		private readonly Hashtable weights = new Hashtable();

		// Contains the total weights
		private long total;

		public long Total
		{
			get { return total; }
		}

		/// <summary>
		/// Selects a specific object from the weighted chart, based on
		/// the given index.
		/// </summary>
		protected object Select(int index)
		{
			// Go through the keys
			long running = 0;

			foreach (object key in weights.Keys)
			{
				// Get the weight and add it
				running += this[key];

				// If we passed the results, return it
				if (running >= index)
					return key;
			}

			// Return it
			throw new Exception("Could not select random");
		}

		#endregion
	}
}