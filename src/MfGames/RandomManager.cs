// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;

namespace MfGames
{
	/// <summary>
	/// Set of utility functions for creating a singleton instance of
	/// a random number generator that may be used anywhere. This
	/// takes any System.Random derived class as the singleton.
	/// </summary>
	public static class RandomManager
	{
		#region Properties

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

		#region Fields

		private static Random random;

		[ThreadStatic]
		private static Random threadRandom;

		#endregion
	}
}
