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

namespace MfGames
{
	/// <summary>
	/// Set of utility functions for creating a singleton instance of
	/// a random number generator that may be used anywhere. This
	/// takes any System.Random derived class as the singleton, but
	/// defaults to the MersenneTwister in this package.
	/// </summary>
	public static class RandomManager
	{
		#region Singleton

		private static Random random;

		[ThreadStatic]
		private static Random threadRandom;

		/// <summary>
		/// Gets or sets a static random generator.
		/// </summary>
		/// <value>The random.</value>
		public static Random Random
		{
			get
			{
				// Create a new random if it hasn't been set
				if (random == null)
				{
					random = new Random();
				}

				// Return the results
				return random;
			}

			set { random = value; }
		}

		/// <summary>
		/// Gets or sets a thread-specific, static random generator.
		/// </summary>
		/// <value>The random.</value>
		public static Random ThreadRandom
		{
			get
			{
				// Create a new random if it hasn't been set
				if (threadRandom == null)
				{
					threadRandom = new Random();
				}

				// Return the results
				return threadRandom;
			}

			set { threadRandom = value; }
		}

		#endregion
	}
}