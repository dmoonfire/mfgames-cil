using System;
using MfGames.Numerics;
using NUnit.Framework;

namespace UnitTests
{
/// <summary>
/// Summary description for FractionUnitTest
/// </summary>
	[TestFixture]
	public class FractionTests
	{
		#region Decimal Values
		[Test]
		public void ThirdDecimalValue()
		{
			Fraction fraction = new Fraction(1, 3);
			Assert.AreEqual(0.33333, System.Math.Round(fraction.Value, 5));
		}

		[Test]
		public void QuarterDecimalValue()
		{
			Fraction fraction = new Fraction(1, 4);
			Assert.AreEqual(0.25, fraction.Value);
		}
		#endregion

		#region Simplification
		[Test]
		public void SimplifyThirdNumerator()
		{
			Fraction fraction = new Fraction(1, 3);
			Assert.AreEqual(1, fraction.Simplify().Numerator);
		}

		[Test]
		public void SimplifyThirdDenominator()
		{
			Fraction fraction = new Fraction(1, 3);
			Assert.AreEqual(3, fraction.Simplify().Denominator);
		}

		[Test]
		public void SimplifyThirdNumerator3()
		{
			Fraction fraction = new Fraction(3, 9);
			Assert.AreEqual(1, fraction.Simplify().Numerator);
		}

		[Test]
		public void SimplifyThirdDenominator3()
		{
			Fraction fraction = new Fraction(3, 9);
			Assert.AreEqual(3, fraction.Simplify().Denominator);
		}

		[Test]
		public void ThirdNumerator3()
		{
			Fraction fraction = new Fraction(3, 9);
			Assert.AreEqual(3, fraction.Numerator);
		}

		[Test]
		public void ThirdDenominator3()
		{
			Fraction fraction = new Fraction(3, 9);
			Assert.AreEqual(9, fraction.Denominator);
		}
		#endregion
	}
}
