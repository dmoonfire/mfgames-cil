using System;
using MfGames.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	/// <summary>
	/// Summary description for FractionUnitTest
	/// </summary>
	[TestClass]
	public class FractionTests
	{
		#region Test Context
		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}
		#endregion

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		#region Decimal Values
		[TestMethod]
		public void ThirdDecimalValue()
		{
			Fraction fraction = new Fraction(1, 3);
			Assert.AreEqual(0.33333, Math.Round(fraction.Value, 5));
		}

		[TestMethod]
		public void QuarterDecimalValue()
		{
			Fraction fraction = new Fraction(1, 4);
			Assert.AreEqual(0.25, fraction.Value);
		}
		#endregion

		#region Simplification
		[TestMethod]
		public void SimplifyThirdNumerator()
		{
			Fraction fraction = new Fraction(1, 3);
			Assert.AreEqual(1, fraction.Simplify().Numerator);
		}

		[TestMethod]
		public void SimplifyThirdDenominator()
		{
			Fraction fraction = new Fraction(1, 3);
			Assert.AreEqual(3, fraction.Simplify().Denominator);
		}

		[TestMethod]
		public void SimplifyThirdNumerator3()
		{
			Fraction fraction = new Fraction(3, 9);
			Assert.AreEqual(1, fraction.Simplify().Numerator);
		}

		[TestMethod]
		public void SimplifyThirdDenominator3()
		{
			Fraction fraction = new Fraction(3, 9);
			Assert.AreEqual(3, fraction.Simplify().Denominator);
		}

		[TestMethod]
		public void ThirdNumerator3()
		{
			Fraction fraction = new Fraction(3, 9);
			Assert.AreEqual(3, fraction.Numerator);
		}

		[TestMethod]
		public void ThirdDenominator3()
		{
			Fraction fraction = new Fraction(3, 9);
			Assert.AreEqual(9, fraction.Denominator);
		}
		#endregion
	}
}
