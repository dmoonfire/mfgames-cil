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

namespace MfGames.Entropy
{
	/// <summary>
	/// Simple proxy interface between the MersenneTwister and the
	/// base System.Random class.
	/// </summary>
	public class MersenneTwisterRandom : Random
	{
		#region Constructors

		/// <summary>
		/// Constructs the twister with a default seed.
		/// </summary>
		public MersenneTwisterRandom()
		{
			twister = new MersenneTwister();
		}

		/// <summary>
		/// Constructs the twister with a given seed.
		/// </summary>
		public MersenneTwisterRandom(int seed)
		{
			twister = new MersenneTwister(seed);
		}

		#endregion

		#region Properties

		private readonly MersenneTwister twister;

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