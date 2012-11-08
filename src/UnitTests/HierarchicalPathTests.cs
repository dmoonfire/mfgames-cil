// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

#region Namespaces

using System.Collections.Generic;
using MfGames.Exceptions;
using MfGames.HierarchicalPaths;
using NUnit.Framework;

#endregion

namespace UnitTests
{
	/// <summary>
	/// Testing fixture to test all of the various methods or possible
	/// errors involved with a node reference.
	/// </summary>
	[TestFixture]
	public class HierarchicalPathTests
	{
		#region Methods

		[Test]
		public void AreEqual()
		{
			// Setup
			var expected = new HierarchicalPath("/Application/Quit");

			// Operation
			var path = new HierarchicalPath("/Application/Quit");

			// Verification
			Assert.AreEqual(
				expected,
				path);
			Assert.IsTrue(expected == path);
		}

		/// <summary>
		/// Tests the index accessor to the created children.
		/// </summary>
		[Test]
		public void ChildIndex()
		{
			var up = new HierarchicalPath("/dir1/sub1");
			HierarchicalPath c1 = up["sub2/sub3"];
			Assert.AreEqual(
				"/dir1/sub1/sub2/sub3",
				c1.Path);
		}

		/// <summary>
		/// Tests the number of components
		/// </summary>
		[Test]
		public void Count1()
		{
			var nr = new HierarchicalPath("/a/b/c");
			Assert.AreEqual(
				3,
				nr.Count);
		}

		/// <summary>
		/// Tests the number of simple components
		/// </summary>
		[Test]
		public void Count2()
		{
			var nr = new HierarchicalPath("/a");
			Assert.AreEqual(
				1,
				nr.Count);
		}

		/// <summary>
		/// Tests the number of the root context
		/// </summary>
		[Test]
		public void Count3()
		{
			var nr = new HierarchicalPath("/");
			Assert.AreEqual(
				0,
				nr.Count);
		}

		/// <summary>
		/// Tests the leading "/a/b/../c" path construct.
		/// </summary>
		[Test]
		public void DoubleDot()
		{
			var nr = new HierarchicalPath("/a/b/../c");
			Assert.AreEqual(
				"/a/c",
				nr.Path);
		}

		/// <summary>
		/// Tests the leading "/a/.." path construct.
		/// </summary>
		[Test]
		public void DoubleDotTop()
		{
			var nr = new HierarchicalPath("/a/..");
			Assert.AreEqual(
				"/",
				nr.Path);
		}

		[Test]
		public void InDictionary()
		{
			// Setup
			var dictionary = new Dictionary<HierarchicalPath, string>();
			dictionary[new HierarchicalPath("/Application/Quit")] = "yes";

			// Operation
			var key = new HierarchicalPath("/Application/Quit");
			string value = dictionary[key];

			// Verification
			Assert.IsTrue(dictionary.ContainsKey(key));
			Assert.AreEqual(
				"yes",
				value);
		}

		/// <summary>
		/// Tests the leading "/../.." path construct.
		/// </summary>
		[Test]
		[ExpectedException(typeof (InvalidPathException))]
		public void InvalidDoubleDot2()
		{
			var nr = new HierarchicalPath("/../..");
			Assert.AreEqual(
				"/",
				nr.Path);
		}

		/// <summary>
		/// Tests the leading "." path.
		/// </summary>
		[Test]
		public void LeadingDot()
		{
			var context = new HierarchicalPath("/");
			var path = new HierarchicalPath(
				".",
				context);
			Assert.AreEqual(
				"/",
				path.Path);
		}

		/// <summary>
		/// Tests the leading "./" path construct.
		/// </summary>
		[Test]
		public void LeadingDotSlash()
		{
			var context = new HierarchicalPath("/");
			var nr = new HierarchicalPath(
				"./",
				context);
			Assert.AreEqual(
				"/",
				nr.Path);
		}

		/// <summary>
		/// Tests the name of the components
		/// </summary>
		[Test]
		public void Name()
		{
			var nr = new HierarchicalPath("/a/b/c");
			Assert.AreEqual(
				"c",
				nr.Last);
		}

		/// <summary>
		/// Test lack of absolute path.
		/// </summary>
		[Test]
		public void NoAbsolute()
		{
			var path = new HierarchicalPath("foo");
			Assert.IsTrue(path.IsRelative);
			Assert.AreEqual(
				"./foo",
				path.Path);
		}

		/// <summary>
		/// Tests that the request for the direct path is valid.
		/// </summary>
		[Test]
		public void ParentPath()
		{
			var up = new HierarchicalPath("/dir1/sub1");
			string up1 = up.Parent.Path;
			Assert.AreEqual(
				"/dir1",
				up1);
		}

		/// <summary>
		/// Tests that a basic parent request returns the proper value.
		/// </summary>
		[Test]
		public void ParentRef()
		{
			var up = new HierarchicalPath("/dir1/sub1");
			HierarchicalPath up1 = up.Parent;
			Assert.AreEqual(
				"/dir1",
				up1.Path);
		}

		/// <summary>
		/// Tests various broken parsing tests.
		/// </summary>
		[Test]
		public void ParsePluses()
		{
			var nr = new HierarchicalPath("/Test/Test +1/Test +2");
			Assert.AreEqual(
				"/Test/Test +1/Test +2",
				nr.ToString());
			Assert.AreEqual(
				"Test +2",
				nr.Last);
		}

		/// <summary>
		/// Just tests the basic construction of a path.
		/// </summary>
		[Test]
		public void Simple()
		{
			var up = new HierarchicalPath("/dir1/sub1");
			Assert.AreEqual(
				"/dir1/sub1",
				up.Path);
		}

		/// <summary>
		/// Tests for the basic case of StartsWith.
		/// </summary>
		[Test]
		public void StartsWith1()
		{
			var nr = new HierarchicalPath("/this/is");
			var sr = new HierarchicalPath("/this/is/a/path");
			Assert.AreEqual(
				false,
				nr.StartsWith(sr));
		}

		/// <summary>
		/// Tests for the case of not including.
		/// </summary>
		[Test]
		public void StartsWith2()
		{
			var nr = new HierarchicalPath("/this/is");
			var sr = new HierarchicalPath("/not/in/is/a/path");
			Assert.AreEqual(
				false,
				nr.StartsWith(sr));
		}

		/// <summary>
		/// Tests for the identical cases
		/// </summary>
		[Test]
		public void StartsWith3()
		{
			var nr = new HierarchicalPath("/this/is");
			var sr = new HierarchicalPath("/this/is");
			Assert.AreEqual(
				true,
				nr.StartsWith(sr));
		}

		/// <summary>
		/// Tests for the same parents
		/// </summary>
		[Test]
		public void StartsWith4()
		{
			var nr = new HierarchicalPath("/this/is");
			var sr = new HierarchicalPath("/this");
			Assert.AreEqual(
				true,
				nr.StartsWith(sr));
		}

		/// <summary>
		/// Tests for a sub path isolation.
		/// </summary>
		[Test]
		public void SubRef1()
		{
			var nr = new HierarchicalPath("/this/is");
			var sr = new HierarchicalPath("/this/is/a/path");
			Assert.AreEqual(
				"./a/path",
				sr.GetPathAfter(nr).Path);
		}

		/// <summary>
		/// Tests for a equal sub path isolation.
		/// </summary>
		[Test]
		public void SubRef2()
		{
			var nr = new HierarchicalPath("/this/is");
			var sr = new HierarchicalPath("/this/is");
			Assert.AreEqual(
				".",
				nr.GetPathAfter(sr).Path);
		}

		/// <summary>
		/// Tests for reverse items.
		/// </summary>
		[Test]
		[ExpectedException(typeof (HierarchicalPathException))]
		public void SubRef3()
		{
			var nr = new HierarchicalPath("/this/is/a/path");
			var sr = new HierarchicalPath("/this/is");
			sr.GetPathAfter(nr);
		}

		/// <summary>
		/// Tests for completely different sub reference.
		/// </summary>
		[Test]
		[ExpectedException(typeof (HierarchicalPathException))]
		public void SubRef4()
		{
			var nr = new HierarchicalPath("/this/is/a/path");
			var sr = new HierarchicalPath("/not/a/path");
			nr.GetPathAfter(sr);
		}

		/// <summary>
		/// Tests getting a subpath with a plus symbol.
		/// </summary>
		[Test]
		public void SubpathWithPluses()
		{
			var nr = new HierarchicalPath("/Test +1/A");
			var nr2 = new HierarchicalPath("/Test +1");
			HierarchicalPath nr3 = nr.GetPathAfter(nr2);
			Assert.AreEqual(
				"./A",
				nr3.ToString());
		}

		/// <summary>
		/// Tests an unescaped path.
		/// </summary>
		[Test]
		public void TestEscapedNone()
		{
			var nr = new HierarchicalPath("/a/b/c");
			Assert.AreEqual(
				"/a/b/c",
				nr.ToString(),
				"String comparison");
		}

		/// <summary>
		/// Tests the basic create child functionality.
		/// </summary>
		[Test]
		public void TestNodeCreateChild()
		{
			var up = new HierarchicalPath("/dir1/sub1");
			HierarchicalPath c1 = up.Append("sub2");
			Assert.AreEqual(
				"/dir1/sub1/sub2",
				c1.Path);
		}

		[Test]
		public void TestSplice1()
		{
			// Setup
			var path = new HierarchicalPath("/a/b/c/d/e");

			// Operation
			HierarchicalPath results = path.Splice(
				0,
				2);

			// Verification
			Assert.AreEqual(
				"/a/b",
				results.ToString());
		}

		[Test]
		public void TestSplice2()
		{
			// Setup
			var path = new HierarchicalPath("/a/b/c/d/e");

			// Operation
			HierarchicalPath results = path.Splice(
				2,
				2);

			// Verification
			Assert.AreEqual(
				"./c/d",
				results.ToString());
		}

		[Test]
		public void TestSplice3()
		{
			// Setup
			var path = new HierarchicalPath("/a/b/c/d/e");

			// Operation
			HierarchicalPath results = path.Splice(
				3,
				2);

			// Verification
			Assert.AreEqual(
				"./d/e",
				results.ToString());
		}

		#endregion
	}
}
