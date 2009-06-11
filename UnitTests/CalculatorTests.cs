using System;

using MfGames.Numerics;

using NUnit.Framework;

namespace UnitTests
{
	/// <summary>
	/// Summary description for CalculatorTests
	/// </summary>
	[TestFixture]
	public class CalculatorTests
	{
		[Test]
		public void AddInt32()
		{
			Assert.AreEqual(26, Calculator.Add(7, 19));
		}

		[Test]
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

		[Test]
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

		[Test]
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
	}
}