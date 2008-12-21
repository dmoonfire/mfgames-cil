#region Copyright
/*
 * Copyright (C) 2005-2008, Moonfire Games
 *
 * This file is part of MfGames.Utility.
 *
 * The MfGames.Utility library is free software; you can redistribute
 * it and/or modify it under the terms of the GNU Lesser General
 * Public License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

using MfGames.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
/// <summary>
/// Testing fixture to test all of the various methods or possible
/// errors involved with a node reference.
/// </summary>
	[TestClass]
	public class NodeRefTests
	{
		#region Basic Construction Tests
		/// <summary>
		/// Just tests the basic construction of a path.
		/// </summary>
		[TestMethod]
		public void Simple()
		{
			NodeRef up = new NodeRef("/dir1/sub1");
			Assert.AreEqual("/dir1/sub1", up.Path);
		}

		/// <summary>
		/// Test lack of absolute path.
		/// </summary>
		[ExpectedException(typeof(NotAbsolutePathException))]
		[TestMethod]
		public void NoAbsolute()
		{
			new NodeRef("foo");
		}

		/// <summary>
		/// Tests the leading "." path.
		/// </summary>
		[TestMethod]
		public void LeadingDot()
		{
			NodeRef context = new NodeRef("/");
			NodeRef nr = new NodeRef(".", context);
			Assert.AreEqual("/", nr.Path);
		}

		/// <summary>
		/// Tests the leading "./" path construct.
		/// </summary>
		[TestMethod]
		public void LeadingDotSlash()
		{
			NodeRef context = new NodeRef("/");
			NodeRef nr = new NodeRef("./", context);
			Assert.AreEqual("/", nr.Path);
		}

		/// <summary>
		/// Tests the leading "/a/.." path construct.
		/// </summary>
		[TestMethod]
		public void DoubleDotTop()
		{
			NodeRef nr = new NodeRef("/a/..");
			Assert.AreEqual("/", nr.Path);
		}

		/// <summary>
		/// Tests the leading "/a/b/../c" path construct.
		/// </summary>
		[TestMethod]
		public void DoubleDot()
		{
			NodeRef nr = new NodeRef("/a/b/../c");
			Assert.AreEqual("/a/c", nr.Path);
		}

		/// <summary>
		/// Tests the leading "/../.." path construct.
		/// </summary>
		[TestMethod]
		public void InvalidDoubleDot2()
		{
			NodeRef nr = new NodeRef("/../..");
			Assert.AreEqual("/", nr.Path);
		}

		/// <summary>
		/// Tests for the basic case of Includes.
		/// <summary>
		[TestMethod]
		public void Includes1()
		{
			NodeRef nr = new NodeRef("/this/is");
			NodeRef sr = new NodeRef("/this/is/a/path");
			Assert.AreEqual(true, nr.Includes(sr));
		}

		/// <summary>
		/// Tests for the case of not including.
		/// </summary>
		[TestMethod]
		public void Includes2()
		{
			NodeRef nr = new NodeRef("/this/is");
			NodeRef sr = new NodeRef("/not/in/is/a/path");
			Assert.AreEqual(false, nr.Includes(sr));
		}

		/// <summary>
		/// Tests for the identical cases
		/// <summary>
		[TestMethod]
		public void Includes3()
		{
			NodeRef nr = new NodeRef("/this/is");
			NodeRef sr = new NodeRef("/this/is");
			Assert.AreEqual(true, nr.Includes(sr));
		}

		/// <summary>
		/// Tests for the same parents
		/// <summary>
		[TestMethod]
		public void Includes4()
		{
			NodeRef nr = new NodeRef("/this/is");
			NodeRef sr = new NodeRef("/this");
			Assert.AreEqual(false, nr.Includes(sr));
		}

		/// <summary>
		/// Tests for a sub path isolation.
		/// </summary>
		[TestMethod]
		public void SubRef1()
		{
			NodeRef nr = new NodeRef("/this/is");
			NodeRef sr = new NodeRef("/this/is/a/path");
			Assert.AreEqual("/a/path", nr.GetSubRef(sr).Path);
		}

		/// <summary>
		/// Tests for a equal sub path isolation.
		/// </summary>
		[TestMethod]
		public void SubRef2()
		{
			NodeRef nr = new NodeRef("/this/is");
			NodeRef sr = new NodeRef("/this/is");
			Assert.AreEqual("/", nr.GetSubRef(sr).Path);
		}

		/// <summary>
		/// Tests for reverse items.
		/// </summary>
		[TestMethod]
		public void SubRef3()
		{
			NodeRef nr = new NodeRef("/this/is/a/path");
			NodeRef sr = new NodeRef("/this/is");
			Assert.AreEqual("/this/is", nr.GetSubRef(sr).Path);
		}

		/// <summary>
		/// Tests for completely different sub reference.
		/// </summary>
		[TestMethod]
		public void SubRef4()
		{
			NodeRef nr = new NodeRef("/this/is/a/path");
			NodeRef sr = new NodeRef("/not/a/path");
			Assert.AreEqual("/not/a/path", nr.GetSubRef(sr).Path);
		}

		/// <summary>
		/// Tests the number of components
		/// </summary>
		[TestMethod]
		public void Count1()
		{
			NodeRef nr = new NodeRef("/a/b/c");
			Assert.AreEqual(3, nr.Count);
		}

		/// <summary>
		/// Tests the number of simple components
		/// </summary>
		[TestMethod]
		public void Count2()
		{
			NodeRef nr = new NodeRef("/a");
			Assert.AreEqual(1, nr.Count);
		}

		/// <summary>
		/// Tests the number of the root context
		/// </summary>
		[TestMethod]
		public void Count3()
		{
			NodeRef nr = new NodeRef("/");
			Assert.AreEqual(0, nr.Count);
		}

		/// <summary>
		/// Tests the name of the components
		/// </summary>
		[TestMethod]
		public void Name()
		{
			NodeRef nr = new NodeRef("/a/b/c");
			Assert.AreEqual("c", nr.Name);
		}

		/// <summary>
		/// Tests an unescaped path.
		/// </summary>
		[TestMethod]
		public void TestEscapedNone()
		{
			NodeRef nr = new NodeRef("/a/b/c");
			Assert.AreEqual("/a/b/c", nr.ToString(),
			                "String comparison");
		}

		/// <summary>
		/// Tests an escaped *
		/// </summary>
		[TestMethod]
		public void TestEscapedStar()
		{
			NodeRef nr = new NodeRef("/*/*/*");
			Assert.AreEqual("/*/*/*", nr.ToString(),
			                "String comparison");
			Assert.IsTrue(nr.Includes(new NodeRef("/*/*/*/abb")),
			              "nr.Includes");
		}

		/// <summary>
		/// Tests the regex matching for a simple string.
		/// </summary>
		[TestMethod]
		public void TestSingleStarMatch()
		{
			NodeRef nr = new NodeRef("/a/b/c");
			Assert.IsTrue(nr.IsMatch("/a/*/c"));
		}

		/// <summary>
		/// Tests the regex matching for a simple string.
		/// </summary>
		[TestMethod]
		public void TestSingleStarMatch2()
		{
			NodeRef nr = new NodeRef("/a/b/c");
			Assert.IsTrue(nr.IsMatch("/*/*/c"));
		}

		/// <summary>
		/// Tests the regex matching for a simple string.
		/// </summary>
		[TestMethod]
		public void TestSingleStarMatch3()
		{
			NodeRef nr = new NodeRef("/a/b/c");
			Assert.IsFalse(nr.IsMatch("/*/*/*/c"));
		}

		/// <summary>
		/// Tests the regex matching for a simple string.
		/// </summary>
		[TestMethod]
		public void TestDoubleStarMatch()
		{
			NodeRef nr = new NodeRef("/a/b/c");
			Assert.IsTrue(nr.IsMatch("/**/c"));
		}
		#endregion

		#region Child Tests
		/// <summary>
		/// Tests the basic create child functionality.
		/// </summary>
		[TestMethod]
		public void TestNodeCreateChild()
		{
			NodeRef up = new NodeRef("/dir1/sub1");
			NodeRef c1 = up.CreateChild("sub2");
			Assert.AreEqual("/dir1/sub1/sub2", c1.Path);
		}

		/// <summary>
		/// Tests the index accessor to the created children.
		/// </summary>
		[TestMethod]
		public void ChildIndex()
		{
			NodeRef up = new NodeRef("/dir1/sub1");
			NodeRef c1 = up["sub2/sub3"];
			Assert.AreEqual("/dir1/sub1/sub2/sub3", c1.Path);
		}
		#endregion

		#region Parent Tests
		/// <summary>
		/// Tests that a basic parent request returns the proper value.
		/// </summary>
		[TestMethod]
		public void ParentRef()
		{
			NodeRef up = new NodeRef("/dir1/sub1");
			NodeRef up1 = up.ParentRef;
			Assert.AreEqual("/dir1", up1.Path);
		}

		/// <summary>
		/// Tests that the request for the direct path is valid.
		/// </summary>
		[TestMethod]
		public void ParentPath()
		{
			NodeRef up = new NodeRef("/dir1/sub1");
			string up1 = up.ParentPath;
			Assert.AreEqual("/dir1", up1);
		}
		#endregion

		/// <summary>
		/// Tests various broken parsing tests.
		/// </summary>
		[TestMethod]
		public void ParsePluses()
		{
			NodeRef nr = new NodeRef("/Test/Test +1/Test +2");
			Assert.AreEqual("/Test/Test +1/Test +2", nr.ToString());
			Assert.AreEqual("Test +2", nr.Name);
		}

		[TestMethod]
		public void SubPluses()
		{
			NodeRef nr = new NodeRef("/Test +1/A");
			NodeRef nr2 = new NodeRef("/Test +1");
			NodeRef nr3 = nr2.GetSubRef(nr);
			Assert.AreEqual("/A", nr3.ToString());
		}
	}
}