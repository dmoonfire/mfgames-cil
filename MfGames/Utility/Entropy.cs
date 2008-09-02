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
	/// Set of utility functions for creating a singleton instance of
	/// a random number generator that may be used anywhere. This
	/// takes any System.Random derived class as the singleton, but
	/// defaults to the MersenneTwister in this package.
	/// </summary>
	public static class Entropy
	{
		#region Singleton
		private static Random random;

		public static Random Random
		{
			get
			{
				// Create a new random if it hasn't been set
				if (random == null)
					random = new MersenneRandom();

				// Return the results
				return random;
			}

			set
			{
				random = value;
			}
		}
		#endregion

		#region Integer Functions
		/// <summary>
		/// Returns a value between 0 and Int32.MaxValue.
		/// </summary>
		public static int Next()
		{
			return Random.Next();
		}

		/// <summary>
		/// Returns a value between 0 and max.
		/// </summary>
		public static int Next(int max)
		{
			return Random.Next(max);
		}

		/// <summary>
		/// Returns a value between min and max.
		/// </summary>
		public static int Next(int min, int max)
		{
			return Random.Next(min, max);
		}
		#endregion

		#region Double Functions
		/// <summary>
		/// Returns a double value between 0 and 1.
		/// </summary>
		public static double NextDouble()
		{
			return Random.NextDouble();
		}

		/// <summary>
		/// Returns a double between 0 and max.
		/// </summary>
		public static double NextDouble(double max)
		{
			return Random.NextDouble() * max;
		}

		/// <summary>
		/// Returns a double between min and max.
		/// </summary>
		public static double NextDouble(double min, double max)
		{
			return Random.NextDouble() * (max - min) + min;
		}
		#endregion

		#region Float Functions
		/// <summary>
		/// Returns a float value between 0 and 1.
		/// </summary>
		public static float NextFloat()
		{
			return (float) Random.NextDouble();
		}

		/// <summary>
		/// Returns a float between 0 and max.
		/// </summary>
		public static float NextFloat(float max)
		{
			return (float) Random.NextDouble() * max;
		}

		/// <summary>
		/// Returns a float between min and max.
		/// </summary>
		public static float NextFloat(float min, float max)
		{
			return (float) Random.NextDouble() * (max - min) + min;
		}
		#endregion
	}
}
