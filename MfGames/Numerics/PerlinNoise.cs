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

using MfGames.Entropy;

#endregion

namespace MfGames.Numerics
{
	/// <summary>
	/// Implements a basic controllable Perlin noise generator.
	///
	/// Stolen from http://www.sepcot.com/blog/2006/08/PDN-PerlinNoise2d
	/// </summary>
	public class PerlinNoise : INoise2
	{
		#region Constructors

		/// <summary>
		/// Creates a Perlin generator with a random seed.
		/// </summary>
		public PerlinNoise()
			: this(RandomManager.Next())
		{
		}

		/// <summary>
		/// Creates a Perlin generator with a given seed value.
		/// </summary>
		/// <param name="seed"></param>
		public PerlinNoise(int seed)
		{
			var random = new MersenneRandom(seed);
			Rank1 = random.Next(1000, 10000);
			Rank2 = random.Next(100000, 1000000);
			Rank3 = random.Next(1000000000, 2000000000);
		}

		#endregion

		#region Properties

		public int Rank1 { get; set; }
		public int Rank2 { get; set; }
		public int Rank3 { get; set; }

		#endregion

		#region Noise Generation

		/// <summary>
		/// Gets the noise for the given X and Y coordinate.
		/// </summary>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns></returns>
		public double GetNoise(int x, int y)
		{
			int n = x + y * 57;
			n = (n << 13) ^ n;

			return (1.0 -
			        ((n * (n * n * Rank1 + Rank2) + Rank3) & 0x7fffffff) / 1073741824.0);
		}

		#endregion
	}
}