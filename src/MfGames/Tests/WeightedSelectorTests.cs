#region Namespaces

using System;

using MfGames.Collections;
using MfGames.Exceptions;

using NUnit.Framework;

#endregion

namespace MfGames.Tests
{
	/// <summary>
	/// Tests various functionality of the weighted selectors.
	/// </summary>
	[TestFixture]
	public class WeightedSelectorTests
	{
		/// <summary>
		/// Tests selection from an empty selector.
		/// </summary>
		[Test]
		[ExpectedException(typeof(InvalidOperationException))]
		public void EmptySelector()
		{
			// Setup
			var selector = new WeightedSelector<string>();

			// Test
			selector.GetRandomItem();
		}

		/// <summary>
		/// Tests retrieving from the selector repeatedly with a single item
		/// inside it.
		/// </summary>
		[Test]
		public void SingleWeightSelector()
		{
			// Setup
			var selector = new WeightedSelector<string>();
			selector["bob"] = 1;

			// Test
			for (int i = 0; i < 100; i++)
			{
				selector.GetRandomItem();
			}
		}
	}
}