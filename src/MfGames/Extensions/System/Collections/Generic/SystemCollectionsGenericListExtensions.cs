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

using System.Collections.Generic;

#endregion

namespace MfGames.Extensions.System.Collections.Generic
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