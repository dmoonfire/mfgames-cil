using System;
using System.Collections.Generic;

namespace MfGames.Collections
{
	/// <summary>
	/// Represents a class that allows for keys to have a specific weight or
	/// frequency and for them to be random selected.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	public class WeightedSelector<TKey>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="WeightedSelector&lt;TKey&gt;"/> class.
		/// </summary>
		public WeightedSelector()
		{
			entries = new List<WeightedSelectorEntry>();
		}

		#endregion

		#region Items

		private readonly List<WeightedSelectorEntry> entries;

		/// <summary>
		/// Gets or sets the total weights of all the items in the collection.
		/// </summary>
		/// <value>The count.</value>
		public int Count { get; private set; }

		/// <summary>
		/// Gets or sets the <see cref="System.Int32"/> with the specified key.
		/// </summary>
		/// <value></value>
		public int this[TKey key]
		{
			get
			{
				WeightedSelectorEntry entry = Get(key);

				return entry == null ? 0 : entry.Weight;
			}

			set
			{
				// If we get a null, then throw an argument exception.
				if (Equals(key, default(TKey)))
				{
					throw new ArgumentNullException("key");
				}

				// See if the entry exists in the list already.
				WeightedSelectorEntry entry = Get(key);

				if (entry == null)
				{
					// We couldn't find it, so add a new one.
					entry = new WeightedSelectorEntry();
					entry.Key = key;
					entry.Weight = value;
					entries.Add(entry);

					Count += value;
				}
				else
				{
					// Just update the weight of the value.
					Count -= entry.Weight;
					entry.Weight = value;
					Count += entry.Weight;
				}
			}
		}

		/// <summary>
		/// Gets the specified entry for a given key.
		/// </summary>
		/// <param name="key">The key.</param>
		/// <returns></returns>
		private WeightedSelectorEntry Get(TKey key)
		{
			// If we get a null, then throw an argument exception.
			if (Equals(key, default(TKey)))
			{
				throw new ArgumentNullException("key");
			}

			// Loop through the keys until we find it.
			foreach (var entry in entries)
			{
				if (key.Equals(entry.Key))
				{
					return entry;
				}
			}

			// Can't find it.
			return null;
		}

		#endregion

		#region Selection

		/// <summary>
		/// Gets the random.
		/// </summary>
		/// <returns></returns>
		public TKey GetRandomItem()
		{
			return GetRandomItem(RandomManager.ThreadRandom);
		}

		/// <summary>
		/// Gets the random.
		/// </summary>
		/// <returns></returns>
		public TKey GetRandomItem(Random random)
		{
			// If we have no items, then throw an exception.
			if (Count == 0)
			{
				throw new InvalidOperationException("No items in selector to choose");
			}

			// Get a random number with counts in the list. We will be reducing
			// this with each entry until it is negative, which means we found
			// the right item.
			int selection = random.Next(Count - 1);

			foreach (var entry in entries)
			{
				selection -= entry.Weight;

				if (selection < 0)
				{
					return entry.Key;
				}
			}

			// Can't find the item.
			throw new InvalidOperationException("Unable to entry from given collection");
		}

		#endregion

		/// <summary>
		/// Represents a single entry into the weighted selector which represets
		/// a key with a given frequency or weight.
		/// </summary>
		private class WeightedSelectorEntry
		{
			public TKey Key;
			public int Weight = 1;
		}
	}
}