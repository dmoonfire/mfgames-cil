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