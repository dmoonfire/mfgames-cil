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

using MfGames;
using MfGames.Collections;

using NUnit.Framework;

#endregion

namespace UnitTests
{
    /// <summary>
    /// Test the <see cref="HierarchicalPathTreeCollection{TValue}"/> functionality.
    /// </summary>
    [TestFixture]
    public class HierarchicalPathTreeCollectionTests
    {
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
            collection.Add(path, 234);

            // Verification
            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual(0, collection.Item);
            Assert.AreEqual(234, collection.Get(path));
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
            Assert.AreEqual(0, collection.Count);
        }

        [Test]
        public void TestMultipleDepthAdds()
        {
            // Setup
            var collection = new HierarchicalPathTreeCollection<int>();
            var path1 = new HierarchicalPath("/a/b");
            var path2 = new HierarchicalPath("/a/c");

            // Operation
            collection.Add(path1, 234);
            collection.Add(path2, 567);

            // Verification
            Assert.AreEqual(2, collection.Count);
			Assert.AreEqual(4, collection.NodeCount);
            Assert.AreEqual(0, collection.Item);
            Assert.AreEqual(2, collection.GetChild(new HierarchicalPath("/a")).Count);
            Assert.AreEqual(234, collection.Get(path1));
            Assert.AreEqual(567, collection.Get(path2));
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
            collection.Add(path, 234);

            // Verification
            Assert.AreEqual(1, collection.Count);
            Assert.AreEqual(234, collection.Item);
        }
    }
}