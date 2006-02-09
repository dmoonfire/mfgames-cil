/*
 * Copyright (C) 2005, Moonfire Games
 *
 * This file is part of MfGames.Utility.
 *
 * The MfGames.Utility library is free software; you can redistribute
 * it and/or modify it under the terms of the GNU Lesser General
 * Public License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307
 * USA
 */

namespace MfGames.Utility
{
	using System;
	using System.Collections;
	using System.Reflection;

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
		/// <summary>
		/// Creates an empty type chooser object.
		/// </summary>
		public TypeChooser()
		{
		}
#endregion

#region Adding
		// Contains a hash of lists
		private Hashtable lists = new Hashtable();

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
			ArrayList list = (ArrayList) lists[typeName];
      
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
				IList list = (IList) chooser.lists[key];

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
			ArrayList list = new ArrayList();

			// Ignore nulls (since that happens at the top-most object)
			if (type == null)
				return list;

			// Add this type's keys
			IList l = (IList) lists[type.FullName];

			if (l != null)
				list.AddRange(l);

			// Add the base types
			list.AddRange(Select(type.BaseType));

			// Add the interfaces, if any
			Type [] interfaces
				= type.FindInterfaces(new TypeFilter(FindAllTypes), null);

			foreach (Type iType in interfaces)
				list.AddRange(Select(iType));

			// Return the list
			return list;
		}
#endregion
	}
}
