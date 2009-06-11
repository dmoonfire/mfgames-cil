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
using System.Collections;

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// Implements a basic type chooser which allows a system to add
	/// zero or more objects as strings to a type name
	/// ("MfGames.Utility.TypeChooser" for example). Then, by giving the
	/// system a type object, it will return the list of all objects
	/// that match the type, or any parent class or interface of that
	/// type.
	/// </summary>
	public class TypeChooser
	{
		#region Constructors

		#endregion

		#region Adding

		// Contains a hash of lists
		private readonly Hashtable lists = new Hashtable();

		/// <summary>
		/// Adds a list of objects to the hash table, keyed by the string
		/// name which should be the name of the class or interface
		/// type.
		/// </summary>
		public void Add(string typeName, object objectToAdd)
		{
			// Sanity checking
			if (typeName == null || typeName == "")
			{
				throw new UtilityException("Cannot add a null type");
			}

			// Ignore null objects
			if (objectToAdd == null)
				return;

			// Get the lists based on the key
			var list = (ArrayList) lists[typeName];

			if (list == null)
			{
				list = new ArrayList();
				lists[typeName] = list;
			}

			// Add it
			list.Add(objectToAdd);
		}

		/// <summary>
		/// Adds all of the elements in a type chooser to this one. This
		/// does not replace, just appends to the individual lists.
		/// </summary>
		public void AddRange(TypeChooser chooser)
		{
			// Go through the keys of the chooser
			foreach (string key in chooser.lists.Keys)
			{
				// Get the list
				var list = (IList) chooser.lists[key];

				foreach (object obj in list)
				{
					Add(key, obj);
				}
			}
		}

		#endregion

		#region Selecting

		/// <summary>
		/// Returns the type of types in the chooser.
		/// </summary>
		public int Count
		{
			get { return lists.Count; }
		}

		/// <summary>
		/// Delegate for filtering through the types. This just indicates
		/// that all types should be found.
		/// </summary>
		private bool FindAllTypes(Type m, object filter)
		{
			return true;
		}

		/// <summary>
		/// Selects the range of all the lists that are keyed on the type
		/// name given or the name of any parent class or interface. For
		/// example, giving typeof(string) will return any for
		/// "System.String" or "System.Object" and so on.
		/// </summary>
		public IList Select(Type type)
		{
			// Create a list
			var list = new ArrayList();

			// Ignore nulls (since that happens at the top-most object)
			if (type == null)
				return list;

			// Add this type's keys
			var l = (IList) lists[type.FullName];

			if (l != null)
				list.AddRange(l);

			// Add the base types
			list.AddRange(Select(type.BaseType));

			// Add the interfaces, if any
			Type[] interfaces = type.FindInterfaces(FindAllTypes, null);

			foreach (Type iType in interfaces)
				list.AddRange(Select(iType));

			// Return the list
			return list;
		}

		#endregion
	}
}