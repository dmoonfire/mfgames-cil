#region Copyright and License

// Copyright (c) 2005-2011, Moonfire Games
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

#if DEBUG

#region Namespaces

using MfGames.Entropy;

using NUnit.Framework;

#endregion

namespace MfGames.Dice.Tests
{
	/// <summary>
	/// Tests out the various dice parsing and generation routines.
	/// </summary>
	[TestFixture]
	public class TestDice
	{
		[Test]
		public void TestConstant()
		{
			IDice dice = DiceFactory.Parse("2");
			Assert.AreEqual("2", dice.ToString());
			Assert.AreEqual(typeof(ConstantDice), dice.GetType());
		}

		[Test]
		public void TestMultipleAddition()
		{
			IDice dice = DiceFactory.Parse("1d4+1d6");
			Assert.AreEqual("1d4+1d6", dice.ToString());
		}

		[Test]
		public void TestMultipleRandom()
		{
			IDice dice = DiceFactory.Parse("4d4");
			Assert.AreEqual("4d4", dice.ToString());
		}

		[Test]
		public void TestSimpleAddition()
		{
			IDice dice = DiceFactory.Parse("1d4+2");
			Assert.AreEqual("1d4+2", dice.ToString());
		}

		[Test]
		public void TestSimpleDice()
		{
			IDice dice = DiceFactory.Parse("1d4");
			Assert.AreEqual("1d4", dice.ToString());
			Assert.AreEqual(typeof(RandomDice), dice.GetType());
		}

		[Test]
		public void TestSimpleSubtraction()
		{
			IDice dice = DiceFactory.Parse("1d4-2");
			Assert.AreEqual("1d4-2", dice.ToString());
		}
	}
}

#endif