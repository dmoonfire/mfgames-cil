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

using MfGames.Numerics;

using NUnit.Framework;

#endregion

namespace UnitTests
{
	/// <summary>
	/// Summary description for FractionUnitTest
	/// </summary>
	[TestFixture]
	public class FractionTests
	{
		[Test]
		public void QuarterDecimalValue()
		{
			var fraction = new Fraction(1, 4);
			Assert.AreEqual(0.25, fraction.Value);
		}

		[Test]
		public void SimplifyThirdDenominator()
		{
			var fraction = new Fraction(1, 3);
			Assert.AreEqual(3, fraction.Simplify().Denominator);
		}

		[Test]
		public void SimplifyThirdDenominator3()
		{
			var fraction = new Fraction(3, 9);
			Assert.AreEqual(3, fraction.Simplify().Denominator);
		}

		[Test]
		public void SimplifyThirdNumerator()
		{
			var fraction = new Fraction(1, 3);
			Assert.AreEqual(1, fraction.Simplify().Numerator);
		}

		[Test]
		public void SimplifyThirdNumerator3()
		{
			var fraction = new Fraction(3, 9);
			Assert.AreEqual(1, fraction.Simplify().Numerator);
		}

		[Test]
		public void ThirdDecimalValue()
		{
			var fraction = new Fraction(1, 3);
			Assert.AreEqual(0.33333, Math.Round(fraction.Value, 5));
		}

		[Test]
		public void ThirdDenominator3()
		{
			var fraction = new Fraction(3, 9);
			Assert.AreEqual(9, fraction.Denominator);
		}

		[Test]
		public void ThirdNumerator3()
		{
			var fraction = new Fraction(3, 9);
			Assert.AreEqual(3, fraction.Numerator);
		}
	}
}