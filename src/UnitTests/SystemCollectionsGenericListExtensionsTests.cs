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
using System.Collections.Generic;

using MfGames.Collections;
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
    	#region Ordering/Sorting

		[Test]
		public void MixedOrderPaths()
		{
			// Setup
			var list = new List<IHierarchicalPathContainer>();
			list.Add(new HierarchicalPathKeyValue<int>("/z/b/d", 1));
			list.Add(new HierarchicalPathKeyValue<int>("/z/b", 2));
			list.Add(new HierarchicalPathKeyValue<int>("/z/a", 3));
			list.Add(new HierarchicalPathKeyValue<int>("/b", 4));
			list.Add(new HierarchicalPathKeyValue<int>("/z/b/c", 5));

			// Operation
			list.OrderByHierarchicalPath();

			// Verification
			Assert.AreEqual("/z/b", list[0].HierarchicalPath.ToString());
			Assert.AreEqual("/z/b/d", list[1].HierarchicalPath.ToString());
			Assert.AreEqual("/z/b/c", list[2].HierarchicalPath.ToString());
			Assert.AreEqual("/z/a", list[3].HierarchicalPath.ToString());
			Assert.AreEqual("/b", list[4].HierarchicalPath.ToString());
		}

    	#endregion

    	#region Selection

    	/// <summary>
    	/// Tests selection from an empty selector.
    	/// </summary>
    	[Test]
    	[ExpectedException(typeof(InvalidOperationException))]
    	public void EmptySelector()
    	{
    		// Setup
    		var list = new List<string>();

    		// Test
    		list.GetRandom();
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
    		for (int i = 0; i < 100; i++)
    		{
    			list.GetRandom();
    		}
    	}

    	#endregion
    }
}