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

namespace MfGames.Extensions.System
{
	/// <summary>
	/// Contains extensions to System.Array.
	/// </summary>
	public static class SystemArrayExtensions
	{
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
			return Splice(oldArray, offset, oldArray.Length - offset);
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

			for (int index = offset; index < offset + count; index++)
			{
				newArray[index - offset] = oldArray[index];
			}

			// Return the resulting array.
			return newArray;
		}
	}
}