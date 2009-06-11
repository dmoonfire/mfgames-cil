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
using System.Collections.Specialized;
using System.Runtime.Serialization;

#endregion

namespace MfGames.Utility
{
	[Serializable]
	public class AttributeTreeCollection : NameObjectCollectionBase, ISerializable
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
				var firstRef = new NodeRef(first);

				// Check for it
				var at = (AttributeTree) base.BaseGet(first);

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
		public AttributeTree[] Values
		{
			get
			{
				// Build up a list of values
				var list = new ArrayList();

				foreach (string key in Keys)
				{
					list.Add(this[key]);
				}

				// Return it as a cast
				return (AttributeTree[]) list.ToArray(typeof(AttributeTree));
			}
		}

		public void Add(string name, object value)
		{
			base.BaseAdd(name, value);
		}

		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		#endregion

		#region Serialization

		/// <summary>
		/// Constructs the collection using serialization information.
		/// </summary>
		public AttributeTreeCollection(
			SerializationInfo info, StreamingContext context)
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
		public override void GetObjectData(
			SerializationInfo info, StreamingContext context)
		{
			foreach (string name in base.BaseGetAllKeys())
			{
				try
				{
					info.AddValue(name, base.BaseGet(name));
				}
				catch
				{
				}
			}
		}

		#endregion
	}
}