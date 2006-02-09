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
	using System.Xml.Serialization;

	/// <summary>
	/// Represents an attribute tree which is a class that contains a
	/// hashtable (attributes) and may contain zero or more named child
	/// objects (tree). The class is fairly straightforward, but it
	/// allows additional functionality to create the tree values
	/// automatically.
	/// </summary>
	public class AttributeTree : Logable, ICloneable
	{
#region Constructors
		/// <summary>
		/// Creates an empty AttributeTree object.
		/// </summary>
		public AttributeTree()
		{
			// Set our path
			path = new NodeRef("/");

			// Create our children
			children = new AttributeTreeCollection(this);
		}
#endregion

#region Children
		/// <summary>
		/// Returns the attribute tree node for the given path. If it does
		/// not exist, a null is returned.
		/// </summary>
		public AttributeTree this[NodeRef nref]
		{
			get { return this[nref, false]; }
		}

		/// <summary>
		/// Returns an attribute tree object that represents the path. If
		/// the second parameter is true, then it automatically creates it
		/// and any parent objects above it as needed.
		/// </summary>
		public AttributeTree this[NodeRef nref, bool create]
		{
			get
			{
				// Throw exceptions for null
				if (nref == null)
					throw new UtilityException("Cannot retrieve a null node");

				// If we have a zero-length path ("/"), then we mean this one.
				if (nref.Count == 0)
					return this;

				// Return it from the children
				return children[nref, create];
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
		/// Ensures that the node is created and returns it as
		/// appropriate. This automatically creates any parent nodes.
		/// </summary>
		public AttributeTree Create(string name)
		{
			return Create(new NodeRef(name));
		}

		/// <summary>
		/// Ensures that the node is created and returns it as
		/// appropriate. This automatically creates any parent nodes.
		/// </summary>
		public AttributeTree Create(NodeRef nref)
		{
			// Create it
			AttributeTree at = this[nref, true];
			return at;
		}
#endregion

#region Merging
		/// <summary>
		/// Methods for changing what object is used for child elements.
		/// </summary>
		public virtual AttributeTree CreateClone()
		{
			return new AttributeTree();
		}

		/// <summary>
		/// Provides a method for cloning (or duplicating) the attribute
		/// tree. This performs a deep copy of the clone.
		/// </summary>
		public object Clone()
		{
			// Copy the hash and its attributes
			AttributeTree at = CreateClone();

			if (at == null)
				throw new UtilityException("CrateClone cannot return null.");

			// Copy the attributes
			at.attributes = attributes.Clone() as Hashtable;

			// Copy the children
			foreach (string key in children.Keys)
			{
				// Clone this one
				AttributeTree cat = (AttributeTree) children[key];
				at.children.Add(key, (AttributeTree) cat.Clone());
			}

			// Return it
			return at;
		}

		/// <summary>
		/// Merges this attribute tree with the given attribute
		/// tree. Merging is done by copying all of the attributes from
		/// the given tree into the current one. Then, for every child in
		/// both trees, they are copied in. Nodes that are not the destination
		/// tree are just copied (deep copy) while those in the tree are
		/// merged as per this method, recursively.
		/// </summary>
		public void Merge(AttributeTree source)
		{
			// Copy the attributes
			foreach (string key in source.attributes.Keys)
				attributes[key] = source.attributes[key];

			// Copy the children
			foreach (string name in source.children.Keys)
			{
				// Get the attribute tree
				AttributeTree at = (AttributeTree) source.children[name];

				if (at == null)
				{
					throw new UtilityException("Cannot retrieve object from "
						+ name);
				}

				// Check to see if this is already here
				if (children[name] != null)
				{
					// We have to merge
					AttributeTree dat = (AttributeTree) children[name];
					dat.Merge(at);
				}
				else
				{
					AttributeTree cloned = (AttributeTree) at.Clone();
					children.Add(name, cloned);
				}
			}
		}

		/// <summary>
		/// Called when a new child is created (but not cloned).
		/// </summary>
		public virtual void OnCreatedAsChild(NodeRef nref,
			AttributeTree parent)
		{
			Path = new NodeRef(parent.Path.ToString() + nref.ToString());
		}
#endregion

#region Properties
		// Contains the attributes
		private Hashtable attributes = new Hashtable();

		// Contains the named children
		private AttributeTreeCollection children = null;

		// Contains the path
		private NodeRef path = null;

		/// <summary>
		/// Allows access to the attributes of this node (or element).
		/// </summary>
		[XmlIgnore]
		public IDictionary Attributes
		{
			get { return attributes; }
		}

		/// <summary>
		/// Allows access to the child tree objects.
		/// </summary>
		[XmlIgnore]
		public AttributeTreeCollection Children
		{
			get { return children; }
		}

		/// <summary>
		/// Contains an XML serializable array of children.
		/// </summary>
		[XmlArray("Children")]
		[XmlArrayItem("Child")]
		public ArrayList ChildrenArray
		{
			get
			{
				return new ArrayList(Children.Values);
			}

			set
			{
				foreach (AttributeTree at in value)
				{
					children[new NodeRef("/" + at.Path.Name)] = at;
				}
			}
		}

		/// <summary>
		/// Contains the path of this node.
		/// </summary>
		public NodeRef Path
		{
			get { return path; }
			set { path = value; }
		}
#endregion
	}
}
