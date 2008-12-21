using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using MfGames.Numerics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
/// <summary>
/// Summary description for Point2Tests
/// </summary>
	[TestClass]
	public class Point2Tests
	{
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

		#region EmptyCreate
		[TestMethod]
		public void EmptyCreateInt32()
		{
			Point2<int> point = new Point2<int>();
			Assert.AreEqual(0, point.X);
			Assert.AreEqual(0, point.Y);
		}
		#endregion Emptycreate

		#region DefinedCreate
		[TestMethod]
		public void DefinedCreateInt32()
		{
			Point2<int> point = new Point2<int>(7, 19);
			Assert.AreEqual(7, point.X);
			Assert.AreEqual(19, point.Y);
		}
		#endregion DefinedCreate
	}
}
