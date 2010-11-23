#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
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

#endregion

namespace MfGames.Collections
{
	/// <summary>
	/// Implements a specialized dictionary that is keyed off the type of an object
	/// and a list of zero or more objects. This collection is keyed off the types of an
	/// object, stored as strings, but can be used to select off the <see cref="Type"/> 
	/// as a selector.
	/// 
	/// Implements a basic type chooser which allows a system to add
	/// zero or more objects as strings to a type name
	/// ("MfGames.Utility.TypeHierarchyDictionary" for example). Then, by giving the
	/// system a type object, it will return the list of all objects
	/// that match the type, or any parent class or interface of that
	/// type.
	/// </summary>
	public class TypeHierarchyDictionary<T> : Dictionary<string, List<T>>
	{
		#region Retrieval

		/// <summary>
		/// Selects the range of all the lists that are keyed on the type
		/// name given or the name of any parent class or interface. For
		/// example, giving typeof(string) will return any for
		/// "System.String" or "System.Object" and so on.
		/// </summary>
		public IList<T> Select(Type type)
		{
			// Create a list
			var list = new List<T>();

			// Ignore the null values and just return the empty list. This can be a side-effect
			// of the recursive nature of this function, but also ignores any user input.
			if (type == null)
			{
				return list;
			}

			// See if we have data associated with this type.
			if (ContainsKey(type.ToString()))
			{
				// Get the list and add it to our collection.
				list.AddRange(this[type.ToString()]);
			}

			// Go through the base class and add that one.
			list.AddRange(Select(type.BaseType));

			// Add all the interfaces to the list.
			Type[] interfaces = type.FindInterfaces(delegate { return true; }, null);

			foreach (Type iType in interfaces)
			{
				list.AddRange(Select(iType));
			}

			// Return the resulting list.
			return list;
		}

		#endregion
	}
}