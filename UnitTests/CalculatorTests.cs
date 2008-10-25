using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MfGames.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	/// <summary>
	/// Summary description for CalculatorTests
	/// </summary>
	[TestClass]
	public class CalculatorTests
	{
		public CalculatorTests()
		{
			//
			// TODO: Add constructor logic here
			//
		}

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

		#region Add
		[TestMethod]
		public void AddInt32()
		{
			Assert.AreEqual(26, Calculator.Add(7, 19));
		}

		[TestMethod]
		public void PerformanceAddInt32()
		{
			for (int i = 0; i < 1000; i++)
			{
				for (int j = 0; j < 1000; j++)
				{
					Assert.AreEqual(i + j, Calculator.Add(i, j));
				}
			}
		}

		[TestMethod]
		public void DirectAddInt32()
		{
			for (int i = 0; i < 1000; i++)
			{
				for (int j = 0; j < 1000; j++)
				{
					Assert.AreEqual(i + j, Calculator<Int32>.Add(i, j));
				}
			}
		}

		[TestMethod]
		public void ComparisonAddInt32()
		{
			for (int i = 0; i < 1000; i++)
			{
				for (int j = 0; j < 1000; j++)
				{
					Assert.AreEqual(i + j, i + j);
				}
			}
		}
		#endregion
	}
}
