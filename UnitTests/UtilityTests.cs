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

using MfGames.Collections;
using MfGames.Utility;

using NUnit.Framework;

#endregion

namespace UnitTests
{
	[TestFixture]
	public class UtilityTests
	{
		[Test]
		public void TestMd5HexString()
		{
			Assert.AreEqual("5e027396789a18c37aeda616e3d7991b",
			                ExtendedConvert.ToMd5HexString("jim"));
			Assert.AreEqual("8621ffdbc5698829397d97767ac13db3",
			                ExtendedConvert.ToMd5HexString("dragon"));
		}

		[Test]
		public void TestWeightedSelectorAdd2()
		{
			var ws = new WeightedSelector();
			ws["bob"] = 5;
			ws["gary"] = 5;
			Assert.AreEqual(ws.Total, 10);
		}

		[Test]
		public void TestWeightedSelectorEmpty()
		{
			var ws = new WeightedSelector();
			Assert.AreEqual(ws.Total, 0);
		}

		[Test]
		public void TestWeightedSelectorReplace()
		{
			var ws = new WeightedSelector();
			ws["bob"] = 5;
			ws["gary"] = 5;
			ws["bob"] = 2;
			Assert.AreEqual(ws.Total, 7);
		}
	}
}