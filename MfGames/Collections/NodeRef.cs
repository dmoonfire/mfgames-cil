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
using System.Text.RegularExpressions;

#endregion

namespace MfGames.Collections
{
	/// <summary>
	/// A node ref is a generic reference string that bears some
	/// resemblance to the Unix file system path. It is used for a path
	/// to either assets or some sort of public system. Methods are
	/// included for moving up in the path, bounding it to prevent
	/// referencing a file outside of the reference's scope.
	///
	/// A top-level reference starts with "/" as <i>absolute</i>. All
	/// NodeRef objects are absolute, to create a reference without a
	/// leading "/" requires a second, non-null NodeRef object to
	/// identify the current context of the reference. Paths also never
	/// end in a trailing "/".
	///
	/// This is also a read-only object. Once created, no methods alter
	/// the public for the class. Methods that return a different path
	/// always create a new object.
	///
	/// The choice of using the "/" character as a seperate is that it
	/// requires no escaping in a normal C# string. Any regex
	/// characters (such as "*", "[", "]", "(", ")") are escaped
	/// automatically.
	/// </summary>
	[Serializable]
	public class NodeRef
	{
		#region Constants

		// Contains the common root context
		public static readonly NodeRef RootContext = new NodeRef();

		#endregion

		#region Constructors

		/// <summary>
		/// Private constructor that makes a root context.
		/// </summary>
		public NodeRef()
		{
			pref = "/";
			parts = new string[] { };
		}

		/// <summary>
		/// Constructs a node reference using only the given path. If the
		/// path is invalid in any manner, including not being absolute,
		/// an exception is thrown.
		/// </summary>
		public NodeRef(string path)
		{
			// Create the path components
			ParsePath(path, null);
		}

		public NodeRef(string path, NodeRef context)
		{
			// Create the path components
			ParsePath(path, context);
		}

		#endregion

		#region Path Construction

		/// Frequently used regex to simplify dupliate "//" characters
		private static readonly Regex findDoubleSlashRegex = new Regex("//+");

		// Regex to remove leading slashes

		// Regex to find "/./" references
		private static readonly Regex findHereRegex = new Regex("/\\./");

		// Regex to find the "/<something>/../"

		// Regex to find the "/../"
		private static readonly Regex findInvalidUpRegex = new Regex("/\\.\\./");
		private static readonly Regex findLeadingSlashesRegex = new Regex("^/+");
		private static readonly Regex findRefUpRegex = new Regex("/[^/]+/\\.\\./");
		private static readonly Regex findTrailingSlashesRegex = new Regex("/+$");

		/// <summary>
		/// This parses the given path and builds up the public
		/// representation into memory. This representation is used
		/// for path and additional processing.
		/// </summary>
		private void ParsePath(string path, NodeRef context)
		{
			// Perform some sanity checking on the path
			if (path == null)
				throw new InvalidPathException("Cannot create a node " + "ref from a null");

			// Check for absolute path
			if (!path.StartsWith("/"))
			{
				// We don't have an absolute path, so check the context
				if (context == null)
				{
					throw new NotAbsolutePathException("Cannot create " +
					                                   "absolute path from '" + path + "'");
				}

				// Construct the new path from this context
				path = context.Path + "/" + path;
			}

			// Save the original
			//string orig = path;

			// Start by escaping the escapes
			path = path.Replace("\\", "\\\\");

			// Normalize the path by adding a trailing "/" and reducing
			// double "//" into single ones. This is done with regexes to
			// make it easier and faster. We add the "/" to simplify our
			// regexes later; we will also remove it as the last bit.
			path += "/";
			path = findDoubleSlashRegex.Replace(path, "/");
			//Debug("Processing 1: {0}", path);

			// Normalize the invalid characters
			//Debug("Processing 2: {0}", path);

			// Normalize the "/./" and the "/../" references. Also remove
			// the "/../" stuff at the beginning, by just deleting it.
			path = findHereRegex.Replace(path, "/");
			path = findRefUpRegex.Replace(path, "/");
			path = findInvalidUpRegex.Replace(path, "/");
			//Debug("Processing: {0} => {1}", orig, path);

			// Finally, remove the leading and trailing slash that we put in.
			//Debug("Processing 1: {0} => {1}", orig, path);
			path = findTrailingSlashesRegex.Replace(path, "");
			path = findLeadingSlashesRegex.Replace(path, "").Trim();
			//Debug("Processing 2: {0} => {1}", orig, path);

			// We need to do is make sure the regex characters are not
			// allowed in the string. We do this by just replacing all
			// the important ones with escaped versions.
			regexable = "/" +
			            path.Replace("+", "\\+").Replace("(", "\\(").Replace(")", "\\)").
			            	Replace("[", "\\[").Replace("]", "\\]").Replace(".", "\\.").
			            	Replace("*", "\\*").Replace("?", "\\?");
			//Debug("Processing 3: {0}", path);

			// We now have a normalized path, without a leading or a
			// trailing slash. If this is a blank string (one with no
			// length), it means that the reference was "/". Otherwise, it
			// represents some sort of path that had at least one
			// element. We also save the entire string version because we
			// are both read-only and we make the assumption that memory can
			// handle it.
			if (path.Equals(""))
			{
				// This is an empty path, which means it was a "/" reference
				parts = new string[] { };
				pref = "/";
			}
			else
			{
				// Split it along the slash characters
				parts = path.Split('/');
				pref = "/" + path;
			}
		}

		#endregion

		#region Path Operations

		// Constaints the string version of the entire path
		private string[] parts;
		private string pref;

		// Contains the string usable in regexes
		private string regexable;

		// Contains the various parts of the path, for comparison

		/// <summary>
		/// Returns the nth element of the path.
		/// </summary>
		public string this[int index]
		{
			get { return parts[index]; }
		}

		/// <summary>
		/// Returns the string path for comparison or values.
		/// </summary>
		public string Path
		{
			get { return pref; }
			set { pref = value; }
		}

		/// <summary>
		/// Returns the number of components in the path.
		/// </summary>
		public int Count
		{
			get { return parts.Length; }
		}

		/// <summary>
		/// Returns the bottom-most name of the node reference.
		/// </summary>
		public string Name
		{
			get
			{
				if (parts.Length == 0)
					return ".";
				else
					return parts[parts.Length - 1];
			}

			set { }
		}

		/// <summary>
		/// Returns a NodeRef which has this node's path removed from the
		/// beginning. If the given reference is not included (as per the
		/// Includes), it will be returned completely.
		/// </summary>
		public NodeRef GetSubRef(NodeRef nodeRef)
		{
			// Check for includes
			if (!Includes(nodeRef))
				return nodeRef;

			// Remove the first part. There is an easy method because we are
			// so strict about paths. We use the root context in the case
			// where the leading / is removed.
			string path = Regex.Replace(nodeRef.Path, "^" + regexable, "");

			return new NodeRef(path, RootContext);
		}

		/// <summary>
		/// Compares two node references.
		/// </summary>
		public override bool Equals(object obj)
		{
			var path = (NodeRef) obj;
			return pref.Equals(path.pref);
		}

		/// <summary>
		/// Overrides the hash code to prevent the errors.
		/// </summary>
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Returns true if this path includes the given path. This means
		/// that given path is under or part of this node's path.
		/// </summary>
		public bool Includes(NodeRef path)
		{
			// We have a real easy way of finding this
			return Regex.IsMatch(path.ToString(), "^" + regexable);
		}

		/// <summary>
		/// Returns true if this results in a match in the string
		/// using a variant pattern matching. A single "*" matches
		/// anything other than the path separator while "**" matches
		/// anything including a path separator. The pattern does not
		/// have to start with a leading slash.
		/// </summary>
		public bool IsMatch(string pattern)
		{
			// First sanatize the regular expressions
			string regex =
				pattern.Replace("\\", "\\\\").Replace("+", "\\+").Replace("(", "\\(").
					Replace(")", "\\)").Replace("[", "\\[").Replace("]", "\\]").Replace(".",
					                                                                    "\\.").
					Replace("?", "\\?");

			// The "**" includes anything, including a path separator
			// while the "*" only includes everything but a path
			// separator.
			regex = regex.Replace("*", "[^/]+");
			regex = regex.Replace("[^/]+[^/]+", ".*");

			// Check for the match
			return Regex.IsMatch(regexable, regex);
		}

		/// <summary>
		/// Returns the path when requested as a string.
		/// </summary>
		public override string ToString()
		{
			return pref;
		}

		#endregion

		#region Child Path Operations

		/// <summary>
		/// A simple accessor that allows retrieval of a child path
		/// from this one. This, in effect, calls CreateChild(). The
		/// exception is if the path is given as ".." which then returns
		/// the parent object as appropriate (this will already return
		/// something, unlike ParentRef or ParentPath.
		/// </summary>
		public NodeRef this[string childPath]
		{
			get { return CreateChild(childPath); }
		}

		/// <summary>
		/// Creates a child from this node, by creating the path that uses
		/// this object as a context.
		/// </summary>
		public NodeRef CreateChild(string childPath)
		{
			// By the rules, prefixing "./" will also create the desired
			// results and use this as a context.
			return new NodeRef("./" + childPath, this);
		}

		/// <summary>
		/// This returns a file system path, appropriate to the
		/// filesystem, but without any roots. So, "/a/b" comes out as
		/// "a\b" on Windows and "a/b" on Unix.
		/// </summary>
		public string ToFileSystemPath()
		{
			// Get the path
			string nref = pref.Substring(1);

			// We always use "/", so map it if we need "\"
			if (System.IO.Path.DirectorySeparatorChar != '/')
			{
				nref = Regex.Replace(nref,
				                     "/",
				                     Regex.Escape(
				                     	System.IO.Path.DirectorySeparatorChar.ToString()));
			}

			//Debug("Trying {0} to {1}", pref, nref);

			// Return it
			return nref;
		}

		#endregion

		#region Parent Path Operations

		/// <summary>
		/// Returns the node reference for a parent. If this is already
		/// the root, it will automatically return null on this object.
		/// </summary>
		public NodeRef ParentRef
		{
			get
			{
				// If we have no parts, we don't have a parent.
				if (parts.Length == 0)
					return null;

				// Just append a ".." to the path and rebuild it, and it will
				// create the proper reference.
				return new NodeRef("..", this);
			}
		}

		/// <summary>
		/// Returns the text-version of the parent path. This just
		/// generates the parent and builds up the string path from
		/// that. If this is already the top, it returns null.
		/// </summary>
		public string ParentPath
		{
			get
			{
				// Get the parent
				NodeRef parent = ParentRef;

				// If we got a null, return a null to say "no more"
				if (parent == null)
					return null;

				// Return the parent's string
				return parent.Path;
			}
		}

		#endregion
	}
}