#region Namespaces

using System.Collections.Generic;

#endregion

namespace MfGames.Extensions
{
	/// <summary>
	/// Extends IList-derived classes with additional extensions.
	/// </summary>
	public static class SystemCollectionsGenericListExtensions
	{
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
	}
}