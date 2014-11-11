// <copyright file="ExtendedVersionTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)

#pragma warning disable 1591

namespace UnitTests
{
    using System;

    using MfGames;

    using NUnit.Framework;

    /// <summary>
    /// Tests the ExtendedVersion class.
    /// </summary>
    [TestFixture]
    public class ExtendedVersionTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        [Test]
        public void CompareDouble()
        {
            var v1 = new ExtendedVersion("1.1");
            var v2 = new ExtendedVersion("1.1");
            Assert.IsTrue(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareDoubleGreater()
        {
            var v1 = new ExtendedVersion("1.2");
            var v2 = new ExtendedVersion("1.1");
            Assert.IsFalse(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareDoubleLess()
        {
            var v1 = new ExtendedVersion("1.1");
            var v2 = new ExtendedVersion("1.2");
            Assert.IsFalse(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareDoubleText()
        {
            var v1 = new ExtendedVersion("1.1");
            var v2 = new ExtendedVersion("1.1a");
            Assert.IsFalse(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareDoubleTextEqual()
        {
            var v1 = new ExtendedVersion("1.1a");
            var v2 = new ExtendedVersion("1.1a");
            Assert.IsTrue(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareDoubleTextGreater()
        {
            var v1 = new ExtendedVersion("1.2a");
            var v2 = new ExtendedVersion("1.1a");
            Assert.IsFalse(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareDoubleTextLess()
        {
            var v1 = new ExtendedVersion("1.1a");
            var v2 = new ExtendedVersion("1.2a");
            Assert.IsFalse(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareOpEqual()
        {
            var v1 = new ExtendedVersion("1.2.3");
            Assert.IsTrue(
                v1.Compare("= 1.2.3"), 
                "With space");
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareOpEqual2()
        {
            var v1 = new ExtendedVersion("1.2.3");
            Assert.IsTrue(
                v1.Compare("=1.2.3"), 
                "Without space");
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareOpEqual3()
        {
            var v1 = new ExtendedVersion("1.2.3");
            Assert.IsTrue(
                v1.Compare("=   1.2.3"), 
                "With too many spaces");
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareSingle()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1");
            Assert.IsTrue(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareSingleGreater()
        {
            var v1 = new ExtendedVersion("2");
            var v2 = new ExtendedVersion("1");
            Assert.IsFalse(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareSingleLess()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("2");
            Assert.IsFalse(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareSingleText()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareSingleTextEqual()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsTrue(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareSingleTextGreater()
        {
            var v1 = new ExtendedVersion("2a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void CompareSingleTextLess()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("2a");
            Assert.IsFalse(v1 == v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GreaterSingle()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1");
            Assert.IsFalse(v1 > v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GreaterSingleDouble()
        {
            var v1 = new ExtendedVersion("2.0");
            var v2 = new ExtendedVersion("1");
            Assert.IsTrue(v1 > v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GreaterSingleDoubleLess()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("2.0");
            Assert.IsFalse(v1 > v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GreaterSingleGreater()
        {
            var v1 = new ExtendedVersion("2");
            var v2 = new ExtendedVersion("1");
            Assert.IsTrue(v1 > v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GreaterSingleLess()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("2");
            Assert.IsFalse(v1 > v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GreaterSingleText()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 > v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GreaterSingleTextEqual()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 > v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GreaterSingleTextGreater()
        {
            var v1 = new ExtendedVersion("2a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsTrue(v1 > v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void GreaterSingleTextLess()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("2a");
            Assert.IsFalse(v1 > v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void HighVersesLowLessThan()
        {
            var v1 = new ExtendedVersion("20081.0");
            var v2 = new ExtendedVersion("20071.2.0.0");
            Assert.IsFalse(v1 <= v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void LessSingle()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1");
            Assert.IsFalse(v1 < v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void LessSingleDouble()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("2.0");
            Assert.IsTrue(v1 < v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void LessSingleDoubleGreater()
        {
            var v1 = new ExtendedVersion("2.0");
            var v2 = new ExtendedVersion("1");
            Assert.IsFalse(v1 < v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void LessSingleGreater()
        {
            var v1 = new ExtendedVersion("2");
            var v2 = new ExtendedVersion("1");
            Assert.IsFalse(v1 < v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void LessSingleLess()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("2");
            Assert.IsTrue(v1 < v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void LessSingleText()
        {
            var v1 = new ExtendedVersion("1");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 < v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void LessSingleTextEqual()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 < v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void LessSingleTextGreater()
        {
            var v1 = new ExtendedVersion("2a");
            var v2 = new ExtendedVersion("1a");
            Assert.IsFalse(v1 < v2);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void LessSingleTextLess()
        {
            var v1 = new ExtendedVersion("1a");
            var v2 = new ExtendedVersion("2a");
            Assert.IsTrue(v1 < v2);
        }

        /// <summary>
        /// </summary>
        [ExpectedException(typeof(Exception))]
        [Test]
        public void ParseBlank()
        {
            var v = new ExtendedVersion(string.Empty);
            Assert.IsTrue(
                v == null, 
                "Never get here");
        }

        /// <summary>
        /// </summary>
        [Test]
        public void ParseDebianVersion()
        {
            var v = new ExtendedVersion("1.2.3-4");
            Assert.AreEqual(
                "1.2.3-4", 
                v.ToString());
        }

        /// <summary>
        /// </summary>
        [Test]
        public void ParseDebianVersion2()
        {
            var v = new ExtendedVersion("1.2-3.4d");
            Assert.AreEqual(
                "1.2-3.4d", 
                v.ToString());
        }

        /// <summary>
        /// </summary>
        [ExpectedException(typeof(Exception))]
        [Test]
        public void ParseInnerSpace()
        {
            var v = new ExtendedVersion("1 2.3");
            Assert.IsTrue(
                v == null, 
                "Never get here");
        }

        /// <summary>
        /// </summary>
        [ExpectedException(typeof(Exception))]
        [Test]
        public void ParseNull()
        {
            var v = new ExtendedVersion(null);
            Assert.IsTrue(
                v == null, 
                "Never get here");
        }

        /// <summary>
        /// </summary>
        [Test]
        public void ParseSingleNumber()
        {
            var v = new ExtendedVersion("1");
            Assert.AreEqual(
                "1", 
                v.ToString());
        }

        /// <summary>
        /// </summary>
        [ExpectedException(typeof(Exception))]
        [Test]
        public void ParseSpace()
        {
            var v = new ExtendedVersion(" ");
            Assert.IsTrue(
                v == null, 
                "Never get here");
        }

        /// <summary>
        /// </summary>
        [Test]
        public void ParseTextVersion()
        {
            var v = new ExtendedVersion("1.2b3");
            Assert.AreEqual(
                "1.2b3", 
                v.ToString());
        }

        /// <summary>
        /// </summary>
        [Test]
        public void ParseThreeNumbers()
        {
            var v = new ExtendedVersion("1.2.3");
            Assert.AreEqual(
                "1.2.3", 
                v.ToString());
        }

        /// <summary>
        /// </summary>
        [Test]
        public void ParseTwoNumbers()
        {
            var v = new ExtendedVersion("1.2");
            Assert.AreEqual(
                "1.2", 
                v.ToString());
        }

        /// <summary>
        /// </summary>
        [Test]
        public void ZeroVersesLotsLessThan()
        {
            var v1 = new ExtendedVersion("0.0");
            var v2 = new ExtendedVersion("20071.2.0.0");
            Assert.IsTrue(v1 <= v2);
        }

        #endregion
    }
}

#pragma warning restore 1591