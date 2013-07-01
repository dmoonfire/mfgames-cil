// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using MfGames.HierarchicalPaths;
using NUnit.Framework;

namespace UnitTests
{
	/// <summary>
	/// Testing fixture to test all of the various methods or possible
	/// errors involved with a hierarchical selectors.
	/// </summary>
	[TestFixture]
	public class HierarchicalSelectorTests
	{
		#region Methods

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
