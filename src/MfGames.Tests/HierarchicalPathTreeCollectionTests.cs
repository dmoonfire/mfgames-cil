// <copyright file="HierarchicalPathTreeCollectionTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace UnitTests
{
    using MfGames.Collections;
    using MfGames.HierarchicalPaths;

    using NUnit.Framework;

    /// <summary>
    /// Test the <see cref="HierarchicalPathTreeCollection{TValue}"/> functionality.
    /// </summary>
    [TestFixture]
    public class HierarchicalPathTreeCollectionTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Tests adding item at depth.
        /// </summary>
        [Test]
        public void TestDepthAdd()
        {
            // Setup
            var collection = new HierarchicalPathTreeCollection<int>();
            var path = new HierarchicalPath("/a/b");

            // Operation
            collection.Add(
                path, 
                234);

            // Verification
            Assert.AreEqual(
                1, 
                collection.Count);
            Assert.AreEqual(
                0, 
                collection.Item);
            Assert.AreEqual(
                234, 
                collection.Get(path));
        }

        /// <summary>
        /// Tests the state of an empty collection.
        /// </summary>
        [Test]
        public void TestEmpty()
        {
            // Setup
            var collection = new HierarchicalPathTreeCollection<int>();

            // Operation

            // Verification
            Assert.AreEqual(
                0, 
                collection.Count);
        }

        /// <summary>
        /// </summary>
        [Test]
        public void TestMultipleDepthAdds()
        {
            // Setup
            var collection = new HierarchicalPathTreeCollection<int>();
            var path1 = new HierarchicalPath("/a/b");
            var path2 = new HierarchicalPath("/a/c");

            // Operation
            collection.Add(
                path1, 
                234);
            collection.Add(
                path2, 
                567);

            // Verification
            Assert.AreEqual(
                2, 
                collection.Count);
            Assert.AreEqual(
                4, 
                collection.NodeCount);
            Assert.AreEqual(
                0, 
                collection.Item);
            Assert.AreEqual(
                2, 
                collection.GetChild(new HierarchicalPath("/a"))
                    .Count);
            Assert.AreEqual(
                234, 
                collection.Get(path1));
            Assert.AreEqual(
                567, 
                collection.Get(path2));
        }

        /// <summary>
        /// Tests adding a single item to the root path.
        /// </summary>
        [Test]
        public void TestRootAdd()
        {
            // Setup
            var collection = new HierarchicalPathTreeCollection<int>();
            var path = new HierarchicalPath("/");

            // Operation
            collection.Add(
                path, 
                234);

            // Verification
            Assert.AreEqual(
                1, 
                collection.Count);
            Assert.AreEqual(
                234, 
                collection.Item);
        }

        #endregion
    }
}