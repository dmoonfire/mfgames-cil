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

using MfGames.HierarchicalPaths;

#endregion

namespace MfGames.Extensions.System.Collections.Generic
{
    /// <summary>
    /// Extends IList-derived classes with additional extensions.
    /// </summary>
    public static class SystemCollectionsGenericListExtensions
	{
		#region Ordering/Sorting

		/// <summary>
		/// Shuffles the contents of the list so that each HierarchicalPath is
		/// followed directly by the items underneath it, but still retain the
		/// relative order elements that aren't in the hierarchicy.
		/// 
		/// For example, given "/z/a", "/z", and "/b", it would sort them into
		/// "/z", "/z/a", "/b".
		/// </summary>
		/// <typeparam name="TItem">The type of the item.</typeparam>
		/// <param name="list">The list.</param>
		public static void OrderByHierarchicalPath<TItem>(this IList<TItem> list)
			where TItem: IHierarchicalPathContainer
		{
			// If the list is empty or has a single time, we don't have to do 
			// anything.
			int count = list.Count;

			if (count <= 1)
			{
				return;
			}

			// For the first path, go through the list and perform a bubble
			// sort to reorder the elements so that parent elements will be
			// before the child ones.
			for (int startIndex = 0; startIndex < count - 1; startIndex++)
			{
				// Pull out the path at this index.
				HierarchicalPath startPath = list[startIndex].HierarchicalPath;
				bool startOver = false;

				// Go through all the items after the start index.
				for (int testIndex = startIndex + 1; testIndex < count; testIndex++)
				{
					// Pull out the test path for comparison.
					HierarchicalPath testPath = list[testIndex].HierarchicalPath;

					// Check for equal levels since we don't swap equal-level
					// elements.
					if (startPath.Count == testPath.Count)
					{
						continue;
					}

					// Check to see which one has the least number of elements
					// since that will be "higher" on the list.
					if (startPath.StartsWith(testPath))
					{
						// We have to insert the parent before the current start
						// index, then start processing again.
						TItem item = list[testIndex];
						list.RemoveAt(testIndex);
						list.Insert(startIndex, item);

						// Decrement the start index to start again.
						startOver = true;
						break;
					}
				}

				// If we are starting over, we shift the index back slight and
				// start the outer loop again.
				if (startOver)
				{
					startIndex--;
					break;
				}
			}

			// The second pass involves grouping the related items together.
			// This is a 2-loop process. The first loop is the item we are
			// comparing against. The second looks for items that are underneath
			// the test path and brings them before items that are not.
			for (int startIndex = 0; startIndex < count - 1; startIndex++)
			{
				// Pull out the path at this index.
				HierarchicalPath startPath = list[startIndex].HierarchicalPath;

				// Go through all the items after the start index.
				int lastChildIndex = startIndex;
				bool foundNonChild = false;

				for (int testIndex = startIndex + 1; testIndex < count; testIndex++)
				{
					// Pull out the test path for comparison.
					HierarchicalPath testPath = list[testIndex].HierarchicalPath;

					// Check to see if testPath is underneath the startPath.
					if (testPath.StartsWith(startPath))
					{
						// Check to see if we have a non-child between the last
 						// child path and this one.
						if (foundNonChild)
						{
							// Increment the last child index since we'll be
							// inserting this new item there.
							lastChildIndex++;

							// Remove the item from the test position and insert
							// it into the updated child index.
							TItem item = list[testIndex];
							list.RemoveAt(testIndex);
							list.Insert(lastChildIndex, item);

							// Move the index back to it (and a bit more to
							// handle the for() loop incrementer.
							testIndex = lastChildIndex - 1;

							// Clear out the non child flag.
							foundNonChild = false;
						}
						else
						{
							// This is a child item, just mark it and continue.
							lastChildIndex = testIndex;
						}
					}
					else
					{
						// This isn't a child path
						foundNonChild = true;
					}
				}
			}
		}

		#endregion

		#region Positional

		/// <summary>
		/// Gets the last item in the list.
		/// </summary>
		/// <param name="list">The list.</param>
		/// <returns></returns>
		public static TItem GetLast<TItem>(this IList<TItem> list)
    	{
    		if (list.Count == 0)
    		{
    			return default(TItem);
    		}

    		return list[list.Count - 1];
    	}

    	/// <summary>
    	/// Removes the last item in the list.
    	/// </summary>
    	/// <typeparam name="TItem">The type of the item.</typeparam>
    	/// <param name="list">The list.</param>
    	/// <returns></returns>
    	public static TItem RemoveLast<TItem>(this IList<TItem> list)
    	{
    		if (list.Count == 0)
    		{
    			return default(TItem);
    		}

    		TItem last = list[list.Count - 1];
    		list.RemoveAt(list.Count - 1);
    		return last;
    	}

    	#endregion

    	#region Selection

    	/// <summary>
    	/// Chooses a random item from the list using the random from RandomManager.
    	/// </summary>
    	public static TItem GetRandom<TItem>(this IList<TItem> list)
    	{
    		return GetRandom(list, RandomManager.Random);
    	}

    	/// <summary>
    	/// Chooses a random item from the list using the given random.
    	/// </summary>
    	public static TItem GetRandom<TItem>(this IList<TItem> list, Random random)
    	{
    		// If we have an empty list, then we can't return anything.
    		if (list.Count == 0)
    		{
    			throw new InvalidOperationException("Cannot randomly select if there are no items in the list.");
    		}
			
    		// Pick a random item from the list.
    		int index = random.Next(0, list.Count);
    		return list[index];
    	}

    	#endregion
    }
}