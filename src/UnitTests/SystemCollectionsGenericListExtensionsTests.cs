// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

#region Namespaces

using System;
using System.Collections.Generic;
using MfGames.Extensions.System.Collections.Generic;
using MfGames.HierarchicalPaths;
using NUnit.Framework;

#endregion

namespace UnitTests
{
	/// <summary>
	/// Tests various functionality of the weighted selectors.
	/// </summary>
	[TestFixture]
	public class SystemCollectionsGenericListExtensionsTests
	{
		/// <summary>
		/// Tests selection from an empty selector.
		/// </summary>
		[Test]
		[ExpectedException(typeof (InvalidOperationException))]
		public void EmptySelector()
		{
			// Setup
			var list = new List<string>();

			// Test
			list.GetRandom();
		}

		[Test]
		public void MixedOrderPaths()
		{
			// Setup
			var list = new List<IHierarchicalPathContainer>();
			list.Add(
				new HierarchicalPathKeyValue<int>(
					"/z/b/d",
					1));
			list.Add(
				new HierarchicalPathKeyValue<int>(
					"/z/b",
					2));
			list.Add(
				new HierarchicalPathKeyValue<int>(
					"/z/a",
					3));
			list.Add(
				new HierarchicalPathKeyValue<int>(
					"/b",
					4));
			list.Add(
				new HierarchicalPathKeyValue<int>(
					"/z/b/c",
					5));

			// Operation
			list.OrderByHierarchicalPath();

			// Verification
			Assert.AreEqual(
				"/z/b",
				list[0].HierarchicalPath.ToString());
			Assert.AreEqual(
				"/z/b/d",
				list[1].HierarchicalPath.ToString());
			Assert.AreEqual(
				"/z/b/c",
				list[2].HierarchicalPath.ToString());
			Assert.AreEqual(
				"/z/a",
				list[3].HierarchicalPath.ToString());
			Assert.AreEqual(
				"/b",
				list[4].HierarchicalPath.ToString());
		}

		/// <summary>
		/// Tests retrieving from the selector repeatedly with a single item
		/// inside it.
		/// </summary>
		[Test]
		public void SingleWeightSelector()
		{
			// Setup
			var list = new List<string>();
			list.Add("bob");

			// Test
			for (int i = 0;
				i < 100;
				i++)
			{
				list.GetRandom();
			}
		}
	}
}
