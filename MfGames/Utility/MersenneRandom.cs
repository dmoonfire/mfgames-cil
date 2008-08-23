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
	/// Simple proxy interface between the MersenneTwister and the
	/// base System.Random class.
	/// </summary>
	public class MersenneRandom
	: Random
	{
		#region Constructors
		/// <summary>
		/// Constructs the twister with a default seed.
		/// </summary>
		public MersenneRandom()
		{
			twister = new MersenneTwister();
		}

		/// <summary>
		/// Constructs the twister with a given seed.
		/// </summary>
		public MersenneRandom(int seed)
		{
			twister = new MersenneTwister(seed);
		}
		#endregion
		
		#region Properties
		private MersenneTwister twister;
		#endregion

		#region Number Generation
		public override int Next()
		{
			return twister.Next();
		}

		public override int Next(int max)
		{
			return twister.Next() % max;
		}

		public override int Next(int min, int max)
		{
			return twister.Next() % (max - min) + min;
		}

		public override double NextDouble()
		{
			return twister.NextDouble();
		}

		public double NextDouble(double min)
		{
			return twister.NextDouble() * (1 - min) + min;
		}

		public double NextDouble(double min, double max)
		{
			return twister.NextDouble() * (max - min) + min;
		}

		public float NextSingle()
		{
			return (float) twister.NextDouble();
		}

		public float NextSingle(float min)
		{
			return NextSingle() * (1 - min) + min;
		}

		public float NextSingle(float min, float max)
		{
			return NextSingle() * (max - min) + min;
		}
		#endregion
	}
}
