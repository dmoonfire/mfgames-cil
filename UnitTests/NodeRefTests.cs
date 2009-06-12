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

using NUnit.Framework;

#endregion

namespace UnitTests
{
	/// <summary>
	/// Testing fixture to test all of the various methods or possible
	/// errors involved with a node reference.
	/// </summary>
	[TestFixture]
	public class NodeRefTests
	{
		/// <summary>
		/// Tests the index accessor to the created children.
		/// </summary>
		[Test]
		public void ChildIndex()
		{
			var up = new NodeRef("/dir1/sub1");
			NodeRef c1 = up["sub2/sub3"];
			Assert.AreEqual("/dir1/sub1/sub2/sub3", c1.Path);
		}

		/// <summary>
		/// Tests the number of components
		/// </summary>
		[Test]
		public void Count1()
		{
			var nr = new NodeRef("/a/b/c");
			Assert.AreEqual(3, nr.Count);
		}

		/// <summary>
		/// Tests the number of simple components
		/// </summary>
		[Test]
		public void Count2()
		{
			var nr = new NodeRef("/a");
			Assert.AreEqual(1, nr.Count);
		}

		/// <summary>
		/// Tests the number of the root context
		/// </summary>
		[Test]
		public void Count3()
		{
			var nr = new NodeRef("/");
			Assert.AreEqual(0, nr.Count);
		}

		/// <summary>
		/// Tests the leading "/a/b/../c" path construct.
		/// </summary>
		[Test]
		public void DoubleDot()
		{
			var nr = new NodeRef("/a/b/../c");
			Assert.AreEqual("/a/c", nr.Path);
		}

		/// <summary>
		/// Tests the leading "/a/.." path construct.
		/// </summary>
		[Test]
		public void DoubleDotTop()
		{
			var nr = new NodeRef("/a/..");
			Assert.AreEqual("/", nr.Path);
		}

		/// <summary>
		/// Tests for the basic case of Includes.
		/// <summary>
		[Test]
		public void Includes1()
		{
			var nr = new NodeRef("/this/is");
			var sr = new NodeRef("/this/is/a/path");
			Assert.AreEqual(true, nr.Includes(sr));
		}

		/// <summary>
		/// Tests for the case of not including.
		/// </summary>
		[Test]
		public void Includes2()
		{
			var nr = new NodeRef("/this/is");
			var sr = new NodeRef("/not/in/is/a/path");
			Assert.AreEqual(false, nr.Includes(sr));
		}

		/// <summary>
		/// Tests for the identical cases
		/// <summary>
		[Test]
		public void Includes3()
		{
			var nr = new NodeRef("/this/is");
			var sr = new NodeRef("/this/is");
			Assert.AreEqual(true, nr.Includes(sr));
		}

		/// <summary>
		/// Tests for the same parents
		/// <summary>
		[Test]
		public void Includes4()
		{
			var nr = new NodeRef("/this/is");
			var sr = new NodeRef("/this");
			Assert.AreEqual(false, nr.Includes(sr));
		}

		/// <summary>
		/// Tests the leading "/../.." path construct.
		/// </summary>
		[Test]
		public void InvalidDoubleDot2()
		{
			var nr = new NodeRef("/../..");
			Assert.AreEqual("/", nr.Path);
		}

		/// <summary>
		/// Tests the leading "." path.
		/// </summary>
		[Test]
		public void LeadingDot()
		{
			var context = new NodeRef("/");
			var nr = new NodeRef(".", context);
			Assert.AreEqual("/", nr.Path);
		}

		/// <summary>
		/// Tests the leading "./" path construct.
		/// </summary>
		[Test]
		public void LeadingDotSlash()
		{
			var context = new NodeRef("/");
			var nr = new NodeRef("./", context);
			Assert.AreEqual("/", nr.Path);
		}

		/// <summary>
		/// Tests the name of the components
		/// </summary>
		[Test]
		public void Name()
		{
			var nr = new NodeRef("/a/b/c");
			Assert.AreEqual("c", nr.Name);
		}

		/// <summary>
		/// Test lack of absolute path.
		/// </summary>
		[ExpectedException(typeof(NotAbsolutePathException))]
		[Test]
		public void NoAbsolute()
		{
			new NodeRef("foo");
		}

		/// <summary>
		/// Tests that the request for the direct path is valid.
		/// </summary>
		[Test]
		public void ParentPath()
		{
			var up = new NodeRef("/dir1/sub1");
			string up1 = up.ParentPath;
			Assert.AreEqual("/dir1", up1);
		}

		/// <summary>
		/// Tests that a basic parent request returns the proper value.
		/// </summary>
		[Test]
		public void ParentRef()
		{
			var up = new NodeRef("/dir1/sub1");
			NodeRef up1 = up.ParentRef;
			Assert.AreEqual("/dir1", up1.Path);
		}

		/// <summary>
		/// Tests various broken parsing tests.
		/// </summary>
		[Test]
		public void ParsePluses()
		{
			var nr = new NodeRef("/Test/Test +1/Test +2");
			Assert.AreEqual("/Test/Test +1/Test +2", nr.ToString());
			Assert.AreEqual("Test +2", nr.Name);
		}

		/// <summary>
		/// Just tests the basic construction of a path.
		/// </summary>
		[Test]
		public void Simple()
		{
			var up = new NodeRef("/dir1/sub1");
			Assert.AreEqual("/dir1/sub1", up.Path);
		}

		[Test]
		public void SubPluses()
		{
			var nr = new NodeRef("/Test +1/A");
			var nr2 = new NodeRef("/Test +1");
			NodeRef nr3 = nr2.GetSubRef(nr);
			Assert.AreEqual("/A", nr3.ToString());
		}

		/// <summary>
		/// Tests for a sub path isolation.
		/// </summary>
		[Test]
		public void SubRef1()
		{
			var nr = new NodeRef("/this/is");
			var sr = new NodeRef("/this/is/a/path");
			Assert.AreEqual("/a/path", nr.GetSubRef(sr).Path);
		}

		/// <summary>
		/// Tests for a equal sub path isolation.
		/// </summary>
		[Test]
		public void SubRef2()
		{
			var nr = new NodeRef("/this/is");
			var sr = new NodeRef("/this/is");
			Assert.AreEqual("/", nr.GetSubRef(sr).Path);
		}

		/// <summary>
		/// Tests for reverse items.
		/// </summary>
		[Test]
		public void SubRef3()
		{
			var nr = new NodeRef("/this/is/a/path");
			var sr = new NodeRef("/this/is");
			Assert.AreEqual("/this/is", nr.GetSubRef(sr).Path);
		}

		/// <summary>
		/// Tests for completely different sub reference.
		/// </summary>
		[Test]
		public void SubRef4()
		{
			var nr = new NodeRef("/this/is/a/path");
			var sr = new NodeRef("/not/a/path");
			Assert.AreEqual("/not/a/path", nr.GetSubRef(sr).Path);
		}

		/// <summary>
		/// Tests the regex matching for a simple string.
		/// </summary>
		[Test]
		public void TestDoubleStarMatch()
		{
			var nr = new NodeRef("/a/b/c");
			Assert.IsTrue(nr.IsMatch("/**/c"));
		}

		/// <summary>
		/// Tests an unescaped path.
		/// </summary>
		[Test]
		public void TestEscapedNone()
		{
			var nr = new NodeRef("/a/b/c");
			Assert.AreEqual("/a/b/c", nr.ToString(), "String comparison");
		}

		/// <summary>
		/// Tests an escaped *
		/// </summary>
		[Test]
		public void TestEscapedStar()
		{
			var nr = new NodeRef("/*/*/*");
			Assert.AreEqual("/*/*/*", nr.ToString(), "String comparison");
			Assert.IsTrue(nr.Includes(new NodeRef("/*/*/*/abb")), "nr.Includes");
		}

		/// <summary>
		/// Tests the basic create child functionality.
		/// </summary>
		[Test]
		public void TestNodeCreateChild()
		{
			var up = new NodeRef("/dir1/sub1");
			NodeRef c1 = up.CreateChild("sub2");
			Assert.AreEqual("/dir1/sub1/sub2", c1.Path);
		}

		/// <summary>
		/// Tests the regex matching for a simple string.
		/// </summary>
		[Test]
		public void TestSingleStarMatch()
		{
			var nr = new NodeRef("/a/b/c");
			Assert.IsTrue(nr.IsMatch("/a/*/c"));
		}

		/// <summary>
		/// Tests the regex matching for a simple string.
		/// </summary>
		[Test]
		public void TestSingleStarMatch2()
		{
			var nr = new NodeRef("/a/b/c");
			Assert.IsTrue(nr.IsMatch("/*/*/c"));
		}

		/// <summary>
		/// Tests the regex matching for a simple string.
		/// </summary>
		[Test]
		public void TestSingleStarMatch3()
		{
			var nr = new NodeRef("/a/b/c");
			Assert.IsFalse(nr.IsMatch("/*/*/*/c"));
		}
	}
}