// <copyright file="HierarchicalSelectorTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace UnitTests
{
    using MfGames.HierarchicalPaths;

    using NUnit.Framework;

    /// <summary>
    /// Testing fixture to test all of the various methods or possible
    /// errors involved with a hierarchical selectors.
    /// </summary>
    [TestFixture]
    public class HierarchicalSelectorTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Tests the regex matching for a simple string.
        /// </summary>
        [Test]
        public void TestDoubleStarMatch()
        {
            var nr = new HierarchicalSelector("/**/c");
            Assert.IsTrue(nr.IsMatch("/a/b/c"));
        }

        /// <summary>
        /// Tests the regex matching for a simple string.
        /// </summary>
        [Test]
        public void TestSingleStarMatch()
        {
            var nr = new HierarchicalSelector("/a/*/c");
            Assert.IsTrue(nr.IsMatch("/a/b/c"));
        }

        /// <summary>
        /// Tests the regex matching for a simple string.
        /// </summary>
        [Test]
        public void TestSingleStarMatch2()
        {
            var nr = new HierarchicalSelector("/*/*/c");
            Assert.IsTrue(nr.IsMatch("/a/b/c"));
        }

        /// <summary>
        /// Tests the regex matching for a simple string.
        /// </summary>
        [Test]
        public void TestSingleStarMatch3()
        {
            var nr = new HierarchicalSelector("/*/*/*/c");
            Assert.IsFalse(nr.IsMatch("/a/b/c"));
        }

        #endregion
    }
}