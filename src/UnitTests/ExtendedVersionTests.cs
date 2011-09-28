#region Copyright and License

// Copyright (C) 2005-2011 by Moonfire Games
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

using MfGames;

using NUnit.Framework;

#endregion

#pragma warning disable 1591

namespace UnitTests
{
    /// <summary>
    /// Tests the ExtendedVersion class.
    /// </summary>
    [TestFixture]
    public class ExtendedVersionTests
    {
        [Test]
        public void CompareDouble()
        {
            var v1 = new ExtendedVersion("1.1");
            var v2 = new ExtendedVersion("1.1");
            Assert.IsTrue(v1 == v2);
        }

        [Test]
        public void CompareDoubleGreater()
        {
            var v1 = new ExtendedVersion("1.2");
            var v2 = new ExtendedVersion("1.1");
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void CompareDoubleLess()
        {
            var v1 = new ExtendedVersion("1.1");
            var v2 = new ExtendedVersion("1.2");
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void CompareDoubleText()
        {
            var v1 = new ExtendedVersion("1.1");
            var v2 = new ExtendedVersion("1.1a");
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void CompareDoubleTextEqual()
        {
            var v1 = new ExtendedVersion("1.1a");
            var v2 = new ExtendedVersion("1.1a");
            Assert.IsTrue(v1 == v2);
        }

        [Test]
        public void CompareDoubleTextGreater()
        {
            var v1 = new ExtendedVersion("1.2a");
            var v2 = new ExtendedVersion("1.1a");
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void CompareDoubleTextLess()
        {
            var v1 = new ExtendedVersion("1.1a");
            var v2 = new ExtendedVersion("1.2a");
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void CompareOpEqual()
        {
            var v1 = new ExtendedVersion("1.2.3");
            Assert.IsTrue(v1.Compare("= 1.2.3"), "With space");
        }

        [Test]
        public void CompareOpEqual2()
        {
            var v1 = new ExtendedVersion("1.2.3");
            Assert.IsTrue(v1.Compare("=1.2.3"), "Without space");
        }

        [Test]
        public void CompareOpEqual3()
        {
            var v1 = new ExtendedVersion("1.2.3");
            Assert.IsTrue(v1.Compare("=   1.2.3"), "With too many spaces");
        }

        [Test]
        public void CompareSingle()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1");
            Assert.IsTrue(v1 == v2);
        }

        [Test]
        public void CompareSingleGreater()
        {
            var v1 = new ExtendedVersion("2");
            var v2 = new ExtendedVersion("1");
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void CompareSingleLess()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("2");
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void CompareSingleText()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void CompareSingleTextEqual()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsTrue(v1 == v2);
        }

        [Test]
        public void CompareSingleTextGreater()
        {
            var v1 = new ExtendedVersion("2a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void CompareSingleTextLess()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("2a");
            Assert.IsFalse(v1 == v2);
        }

        [Test]
        public void GreaterSingle()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1");
            Assert.IsFalse(v1 > v2);
        }

        [Test]
        public void GreaterSingleDouble()
        {
            var v1 = new ExtendedVersion("2.0");
            var v2 = new ExtendedVersion("1");
            Assert.IsTrue(v1 > v2);
        }

        [Test]
        public void GreaterSingleDoubleLess()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("2.0");
            Assert.IsFalse(v1 > v2);
        }

        [Test]
        public void GreaterSingleGreater()
        {
            var v1 = new ExtendedVersion("2");
            var v2 = new ExtendedVersion("1");
            Assert.IsTrue(v1 > v2);
        }

        [Test]
        public void GreaterSingleLess()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("2");
            Assert.IsFalse(v1 > v2);
        }

        [Test]
        public void GreaterSingleText()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 > v2);
        }

        [Test]
        public void GreaterSingleTextEqual()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 > v2);
        }

        [Test]
        public void GreaterSingleTextGreater()
        {
            var v1 = new ExtendedVersion("2a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsTrue(v1 > v2);
        }

        [Test]
        public void GreaterSingleTextLess()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("2a");
            Assert.IsFalse(v1 > v2);
        }

        [Test]
        public void HighVersesLowLessThan()
        {
            var v1 = new ExtendedVersion("20081.0");
            var v2 = new ExtendedVersion("20071.2.0.0");
            Assert.IsFalse(v1 <= v2);
        }

        [Test]
        public void LessSingle()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1");
            Assert.IsFalse(v1 < v2);
        }

        [Test]
        public void LessSingleDouble()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("2.0");
            Assert.IsTrue(v1 < v2);
        }

        [Test]
        public void LessSingleDoubleGreater()
        {
            var v1 = new ExtendedVersion("2.0");
            var v2 = new ExtendedVersion("1");
            Assert.IsFalse(v1 < v2);
        }

        [Test]
        public void LessSingleGreater()
        {
            var v1 = new ExtendedVersion("2");
            var v2 = new ExtendedVersion("1");
            Assert.IsFalse(v1 < v2);
        }

        [Test]
        public void LessSingleLess()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("2");
            Assert.IsTrue(v1 < v2);
        }

        [Test]
        public void LessSingleText()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 < v2);
        }

        [Test]
        public void LessSingleTextEqual()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 < v2);
        }

        [Test]
        public void LessSingleTextGreater()
        {
            var v1 = new ExtendedVersion("2a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 < v2);
        }

        [Test]
        public void LessSingleTextLess()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("2a");
            Assert.IsTrue(v1 < v2);
        }

        [ExpectedException(typeof(Exception))]
        [Test]
        public void ParseBlank()
        {
            var v = new ExtendedVersion("");
            Assert.IsTrue(v == null, "Never get here");
        }

        [Test]
        public void ParseDebianVersion()
        {
            var v = new ExtendedVersion("1.2.3-4");
            Assert.AreEqual("1.2.3-4", v.ToString());
        }

        [Test]
        public void ParseDebianVersion2()
        {
            var v = new ExtendedVersion("1.2-3.4d");
            Assert.AreEqual("1.2-3.4d", v.ToString());
        }

        [ExpectedException(typeof(Exception))]
        [Test]
        public void ParseInnerSpace()
        {
            var v = new ExtendedVersion("1 2.3");
            Assert.IsTrue(v == null, "Never get here");
        }

        [ExpectedException(typeof(Exception))]
        [Test]
        public void ParseNull()
        {
            var v = new ExtendedVersion(null);
            Assert.IsTrue(v == null, "Never get here");
        }

        [Test]
        public void ParseSingleNumber()
        {
            var v = new ExtendedVersion("1");
            Assert.AreEqual("1", v.ToString());
        }

        [ExpectedException(typeof(Exception))]
        [Test]
        public void ParseSpace()
        {
            var v = new ExtendedVersion(" ");
            Assert.IsTrue(v == null, "Never get here");
        }

        [Test]
        public void ParseTextVersion()
        {
            var v = new ExtendedVersion("1.2b3");
            Assert.AreEqual("1.2b3", v.ToString());
        }

        [Test]
        public void ParseThreeNumbers()
        {
            var v = new ExtendedVersion("1.2.3");
            Assert.AreEqual("1.2.3", v.ToString());
        }

        [Test]
        public void ParseTwoNumbers()
        {
            var v = new ExtendedVersion("1.2");
            Assert.AreEqual("1.2", v.ToString());
        }

        [Test]
        public void ZeroVersesLotsLessThan()
        {
            var v1 = new ExtendedVersion("0.0");
            var v2 = new ExtendedVersion("20071.2.0.0");
            Assert.IsTrue(v1 <= v2);
        }
    }
}

#pragma warning restore 1591