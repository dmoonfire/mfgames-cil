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
	/// Represents a fractional number, keeping the individual components.
	/// </summary>
	public class Fraction
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Fraction"/> class.
		/// </summary>
		public Fraction()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Fraction"/> class.
		/// </summary>
		/// <param name="numerator">The numerator.</param>
		/// <param name="denominator">The denominator.</param>
		public Fraction(int numerator, int denominator)
		{
			Numerator = numerator;
			Denominator = denominator;
		}

		#endregion

		#region Properties

		private int denominator;
		private int numerator;

		/// <summary>
		/// Gets or sets the denominator of the fraction.
		/// </summary>
		/// <value>The denominator.</value>
		public int Denominator
		{
			get { return denominator; }
			set { denominator = value; }
		}

		/// <summary>
		/// Gets the mixed fraction denominator.
		/// </summary>
		/// <value>The mixed denominator.</value>
		public int MixedDenominator
		{
			get { return denominator; }
		}

		/// <summary>
		/// Gets the mixed fraction numerator.
		/// </summary>
		/// <value>The mixed numerator.</value>
		public int MixedNumerator
		{
			get { return numerator % denominator; }
		}

		/// <summary>
		/// Gets the mixed fraction whole.
		/// </summary>
		/// <value>The mixed whole.</value>
		public int MixedWhole
		{
			get { return numerator / denominator; }
		}

		/// <summary>
		/// Gets or sets the numerator of the fraction.
		/// </summary>
		/// <value>The numerator.</value>
		public int Numerator
		{
			get { return numerator; }
			set { numerator = value; }
		}

		/// <summary>
		/// Gets the double value of this fraction.
		/// </summary>
		/// <value>The value.</value>
		public double Value
		{
			get { return (numerator) / ((double) denominator); }
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Simplifies this fraction instance and returns a new fraction.
		/// </summary>
		public Fraction Simplify()
		{
			int gcf = Math.GreatestCommonFactor(numerator, denominator);

			return new Fraction(numerator / gcf, denominator / gcf);
		}

		#endregion
	}
}