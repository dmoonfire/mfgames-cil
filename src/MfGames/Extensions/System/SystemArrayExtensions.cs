// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

namespace MfGames.Extensions.System
{
	/// <summary>
	/// Contains extensions to System.Array.
	/// </summary>
	public static class SystemArrayExtensions
	{
		#region Methods

		/// <summary>
		/// Splices the specified old array by creating a new array from the given offset
		/// to the end.
		/// </summary>
		/// <typeparam name="TItem">The type of the item.</typeparam>
		/// <param name="oldArray">The old array.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		public static TItem[] Splice<TItem>(
			this TItem[] oldArray,
			int offset)
		{
			return Splice(
				oldArray,
				offset,
				oldArray.Length - offset);
		}

		/// <summary>
		/// Splices the specified old array by creating a new array from the given offset
		/// for a number of count items.
		/// </summary>
		/// <typeparam name="TItem">The type of the item.</typeparam>
		/// <param name="oldArray">The old array.</param>
		/// <param name="offset">The offset.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		public static TItem[] Splice<TItem>(
			this TItem[] oldArray,
			int offset,
			int count)
		{
			// Check for nulls and blanks.
			if (oldArray == null)
			{
				return null;
			}

			// Create a new array and copy into it.
			var newArray = new TItem[count];

			for (int index = offset;
				index < offset + count;
				index++)
			{
				newArray[index - offset] = oldArray[index];
			}

			// Return the resulting array.
			return newArray;
		}

		#endregion
	}
}
