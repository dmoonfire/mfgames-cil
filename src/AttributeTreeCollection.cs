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

namespace MfGames.Utility {
	using System;
	using System.Collections;
	using System.Collections.Specialized;
	using System.Runtime.Serialization;

	[Serializable]
	public class AttributeTreeCollection
    : NameObjectCollectionBase, ISerializable
	{
		// Contains the base tree used for creation
		private AttributeTree baseTree = null;

#region Constructors
		public AttributeTreeCollection()
		{
		}

		public AttributeTreeCollection(AttributeTree baseTree)
		{
			this.baseTree = baseTree;
		}
#endregion

#region Accessors
		public void Add(string name, object value) { base.BaseAdd(name, 
				value); }
		public void Remove(string name){base.BaseRemove(name);}

		/// <summary>
		/// Returns the attribute tree node for the given path. If it does
		/// not exist, a null is returned.
		/// </summary>
		public AttributeTree this[NodeRef nref]
		{
			get { return this[nref, false]; }
			set { BaseSet(nref.ToString(), value); }
		}

		/// <summary>
		/// Returns an attribute tree object that represents the path. If
		/// the second parameter is true, then it automatically creates it
		/// and any parent objects above it as needed.
		///
		/// This does not handle "/" path which means the current
		/// one. Instead, you should retrieve it from the
		/// AttributeTree["/"].
		/// </summary>
		public AttributeTree this[NodeRef nref, bool create]
		{
			get
			{
				// Throw exceptions for null
				if (nref == null)
					throw new UtilityException("Cannot retrieve a null node");

				// If we have a zero-length path ("/"), then we mean this one,
				// so ignore it.
				if (nref.Count == 0)
					return null;

				// Grab the first element
				string first = "/" + nref[0];
				NodeRef firstRef = new NodeRef(first);

				// Check for it
				AttributeTree at = (AttributeTree) base.BaseGet(first);

				if (at == null && !create)
				{
					// We can't find it and we can't create it
					return null;
				}
				else if (at == null)
				{
					// Create it
					at = baseTree.CreateClone();
					at.OnCreatedAsChild(firstRef, baseTree);
					Add(first, at);
				}

				// Return it down the path
				NodeRef nr = firstRef.GetSubRef(nref);
				return at[nr, create];
			}
		}

		/// <summary>
		/// Allows the child to be selected based on node reference. This
		/// is wrapped into a NodeRef and may throw an exception if it is
		/// an invalid path. The default is not to create the elements as
		/// needed.
		/// </summary>
		public AttributeTree this[string name]
		{
			get { return this[new NodeRef(name), false]; }
		}

		/// <summary>
		/// Returns a node reference, creating any required nodes as
		/// needed, if the second parameter is true.
		/// </summary>
		public AttributeTree this[string name, bool create]
		{
			get { return this[new NodeRef(name), create]; }
		}

		/// <summary>
		/// Returns a list of all child items, as an array of
		/// AttributeTree elements.
		/// </summary>
		public AttributeTree [] Values
		{
			get
			{
				// Build up a list of values
				ArrayList list = new ArrayList();

				foreach (string key in Keys)
				{
					list.Add(this[key]);
				}

				// Return it as a cast
				return (AttributeTree []) list.ToArray(typeof(AttributeTree));
			}
		}
#endregion

#region Serialization
		/// <summary>
		/// Constructs the collection using serialization information.
		/// </summary>
		public AttributeTreeCollection(SerializationInfo info, 
			StreamingContext context)
		{
			// Go through the items and add them
			SerializationInfoEnumerator infoItems = info.GetEnumerator();

			while (infoItems.MoveNext())
			{
				base.BaseAdd(infoItems.Name, infoItems.Value);
			}
		}
    
		/// <summary>
		/// Loads in the attribute data from the given serialization
		/// context.
		/// </summary>
		public override void GetObjectData(SerializationInfo info,
			StreamingContext context)
		{
			foreach(string name in base.BaseGetAllKeys())
			{
				try
				{
					info.AddValue(name, base.BaseGet(name));
				}
				catch { }
			}
		}
#endregion
	}
}
