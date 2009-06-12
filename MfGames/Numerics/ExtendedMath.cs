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

namespace MfGames.Numerics
{
	/// <summary>
	/// Additional mathematics and algorithims.
	/// </summary>
	public static class ExtendedMath
	{
		/// <summary>
		/// Returns the greatest common factory (GCF) of two integers.
		/// </summary>
		/// <param name="a">A non-zero integer.</param>
		/// <param name="b">A non-zero integer.</param>
		/// <returns></returns>
		public static int GreatestCommonFactor(int a, int b)
		{
			int high = System.Math.Max(a, b);
			int low = System.Math.Min(a, b);
			int tmp = high % low;

			while (tmp != 0)
			{
				high = low;
				low = tmp;
				tmp = high % low;
			}

			return low;
		}
	}
}