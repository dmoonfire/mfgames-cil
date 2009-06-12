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

using MfGames.Entropy;

#endregion

namespace MfGames.Numerics
{
	/// <summary>
	/// Implements a simple linear range.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class LinearRange1<T> : IRange1<T>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="LinearRange1&lt;T&gt;"/> class.
		/// </summary>
		public LinearRange1()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="LinearRange1&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="min">The min.</param>
		/// <param name="max">The max.</param>
		public LinearRange1(T min, T max)
		{
			Maximum = max;
			Minimum = min;
		}

		#endregion Constructors

		#region Bounds

		/// <summary>
		/// Gets the maximum value for this numeric range.
		/// </summary>
		/// <value>The maximum.</value>
		public T Maximum { get; set; }

		/// <summary>
		/// Gets the minimum value for this range.
		/// </summary>
		/// <value>The minimum.</value>
		public T Minimum { get; set; }

		#endregion Bounds

		#region Selection

		/// <summary>
		/// Gets a random number that is valid for this range.
		/// </summary>
		/// <returns></returns>
		public T GetRandom()
		{
			double d1 = Convert.ToDouble(Minimum);
			double d2 = Convert.ToDouble(Maximum);
			double r = RandomManager.NextDouble(d1, d2);
			return (T) Convert.ChangeType(r, typeof(T));
		}

		#endregion Selection
	}
}