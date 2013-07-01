// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;
using System.Collections.Generic;
using System.Text;
using MfGames.Exceptions;
using MfGames.Extensions.System;

namespace MfGames.HierarchicalPaths
{
	/// <summary>
	/// Represents an hierarchical path given in the same form as a Unix file
	/// path. This is chosen because the forward slash does not require
	/// escaping in C# strings and it is a well-known paradim for representing
	/// a reference in a tree structure. The individual parts of the path
	/// are called Levels.
	/// 
	/// There are two forms of paths: absolute and relative. Absolute paths
	/// always start with a leading forward slash ("/") and relative start with
	/// a period and slash ("."). Otherwise, they follow the same rules for
	/// formatting. No path ends with a trailing slash nor can a path element
	/// be blank. In these cases (e.g., "/root//child/"), the doubled slash
	/// will be collapsed into a single one (e.g., "/root/child").
	/// 
	/// Paths can include any Unicode character without escaping except for
	/// the backslash and the forward slash. In both cases, the two slashes
	/// must be escaped with a backslash (e.g., "\\" and "\/").
	/// 
	/// HierarchicalPath is a read-only object. Once created, no methods directly
	/// alter the object. Instead, they return a new modified path.
	/// </summary>
	[Serializable]
	public class HierarchicalPath: IComparable<HierarchicalPath>
	{
		#region Properties

		/// <summary>
		/// Returns the nth element of the path.
		/// </summary>
		public string this[int index]
		{
			get { return levels[index]; }
		}

		/// <summary>
		/// A simple accessor that allows retrieval of a child path
		/// from this one. This, in effect, calls Append(). The
		/// exception is if the path is given as ".." which then returns
		/// the parent object as appropriate (this will already return
		/// something, unlike ParentPath or Parent.
		/// </summary>
		public HierarchicalPath this[string childPath]
		{
			get { return Append(childPath); }
		}

		/// <summary>
		/// Returns the number of components in the path.
		/// </summary>
		public int Count
		{
			get { return levels.Length; }
		}

		/// <summary>
		/// Gets the first level (or root) in the path.
		/// </summary>
		/// <value>The first.</value>
		public string First
		{
			get
			{
				// If we have a level, return the value.
				if (levels.Length > 0)
				{
					return levels[0];
				}

				// We don't have any, so return the root element.
				return isRelative
					? "."
					: "/";
			}
		}

		/// <summary>
		/// Contains a flag if the path is relative or absolute.
		/// </summary>
		public bool IsRelative
		{
			get { return isRelative; }
		}

		/// <summary>
		/// Gets the last level in the path.
		/// </summary>
		/// <value>The last.</value>
		public string Last
		{
			get
			{
				// If we have a level, return the value.
				if (levels.Length > 0)
				{
					return levels[levels.Length - 1];
				}

				// We don't have any, so return the root element.
				return isRelative
					? "."
					: "/";
			}
		}

		/// <summary>
		/// Contains an array of individual levels within the path.
		/// </summary>
		public IList<string> Levels
		{
			get { return new List<string>(levels); }
		}

		/// <summary>
		/// Returns the node reference for a parent. If this is already
		/// the root, it will automatically return null on this object.
		/// </summary>
		public HierarchicalPath Parent
		{
			get
			{
				// If we have no parts, we don't have a parent.
				if (levels.Length == 0)
				{
					return null;
				}

				// If we have exactly one level, then we are just the root.
				if (levels.Length == 1)
				{
					return isRelative
						? RelativeRoot
						: AbsoluteRoot;
				}

				// Create a new path without the last item in it.
				var parentLevels = new string[levels.Length - 1];

				for (int index = 0;
					index < levels.Length - 1;
					index++)
				{
					parentLevels[index] = levels[index];
				}

				return new HierarchicalPath(parentLevels, isRelative);
			}
		}

		/// <summary>
		/// Returns the string version of the path including escaping for
		/// the special characters.
		/// </summary>
		public string Path
		{
			get
			{
				// Check for the simple paths.
				if (levels.Length == 0)
				{
					return isRelative
						? "."
						: "/";
				}

				// Build up the path including any escaping.
				var buffer = new StringBuilder();

				if (isRelative)
				{
					buffer.Append(".");
				}

				foreach (string level in levels)
				{
					buffer.Append("/");
					buffer.Append(level.Replace("\\", "\\\\").Replace("/", "\\/"));
				}

				// Return the resulting string.
				return buffer.ToString();
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Creates a child from this node, by creating the path that uses
		/// this object as a context.
		/// </summary>
		public HierarchicalPath Append(string childPath)
		{
			// By the rules, prefixing "./" will also create the desired
			// results and use this as a context.
			return new HierarchicalPath("./" + childPath, this);
		}

		/// <summary>
		/// Compares the path to another path.
		/// </summary>
		/// <param name="other"></param>
		/// <returns></returns>
		public int CompareTo(HierarchicalPath other)
		{
			return ToString().CompareTo(other.ToString());
		}

		/// <summary>
		/// Does an equality check on the other path.
		/// </summary>
		/// <param name="other">The other.</param>
		/// <returns></returns>
		public bool Equals(HierarchicalPath other)
		{
			// Make sure that the other is not null.
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			// If we are identical objects, then return true.
			if (ReferenceEquals(this, other))
			{
				return true;
			}

			// Check for the relatively.
			if (other.isRelative != isRelative)
			{
				return false;
			}

			// Equality on the array of strings doesn't work as expected, so we
			// compare each string to itself.
			string[] otherLevels = other.levels;

			if (otherLevels.Length != levels.Length)
			{
				return false;
			}

			for (int i = 0;
				i < otherLevels.Length;
				i++)
			{
				if (!otherLevels[i].Equals(levels[i]))
				{
					return false;
				}
			}

			// We got this far, therefore we are equal.
			return true;
		}

		/// <summary>
		/// Determines whether the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <param name="obj">The <see cref="T:System.Object"/> to compare with the current <see cref="T:System.Object"/>.</param>
		/// <returns>
		/// true if the specified <see cref="T:System.Object"/> is equal to the current <see cref="T:System.Object"/>; otherwise, false.
		/// </returns>
		/// <exception cref="T:System.NullReferenceException">The <paramref name="obj"/> parameter is null.</exception>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			if (obj.GetType() != typeof (HierarchicalPath))
			{
				return false;
			}

			return Equals((HierarchicalPath) obj);
		}

		/// <summary>
		/// Overrides the hash code to prevent the errors.
		/// </summary>
		public override int GetHashCode()
		{
			unchecked
			{
				// Build up the hash, starting with the flag.
				int hashCode = isRelative.GetHashCode() * 397;

				// We can't use the array itself because it doesn't produce
				// a consistent hash code for dictionary operations.
				for (int i = 0;
					i < levels.Length;
					i++)
				{
					hashCode ^= levels[i].GetHashCode();
				}

				// Return the results.
				return hashCode;
			}
		}

		/// <summary>
		/// Gets the child path of this path after the root path. If the path
		/// doesn't start with the rootPath, an exception is thrown.
		/// </summary>
		/// <param name="rootPath">The root path.</param>
		/// <returns></returns>
		public HierarchicalPath GetPathAfter(HierarchicalPath rootPath)
		{
			// Check for the starting path.
			if (!StartsWith(rootPath))
			{
				throw new HierarchicalPathException(
					"The two paths don't have a common prefix");
			}

			// Strip off the root path and create a new path.
			var newLevels = new string[levels.Length - rootPath.levels.Length];

			for (int index = rootPath.levels.Length;
				index < levels.Length;
				index++)
			{
				newLevels[index - rootPath.levels.Length] = levels[index];
			}

			// Create the new path and return it. This will always be relative
			// to the given path since it is a subset.
			return new HierarchicalPath(newLevels, true);
		}

		/// <summary>
		/// Gets the subpath starting with the given index.
		/// </summary>
		/// <param name="firstIndex">The first index.</param>
		/// <returns></returns>
		public HierarchicalPath Splice(int firstIndex)
		{
			return new HierarchicalPath(levels, firstIndex, true);
		}

		/// <summary>
		/// Splices the path into a subset of the path, creating a
		/// new path.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		public HierarchicalPath Splice(
			int offset,
			int count)
		{
			return Splice(offset, count, offset > 0);
		}

		/// <summary>
		/// Splices the path into a subset of the path, creating a
		/// new path.
		/// </summary>
		/// <param name="offset">The offset.</param>
		/// <param name="count">The count.</param>
		/// <param name="makeRelative">if set to <c>true</c> [make relative].</param>
		/// <returns></returns>
		public HierarchicalPath Splice(
			int offset,
			int count,
			bool makeRelative)
		{
			string[] newLevels = levels.Splice(offset, count);
			var hierarchicalPath = new HierarchicalPath(newLevels, makeRelative);
			return hierarchicalPath;
		}

		/// <summary>
		/// Returns true if the current path starts with the same elements as
		/// the specified path and they have the same absolute/relative root.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <returns></returns>
		public bool StartsWith(HierarchicalPath path)
		{
			// Make sure we didn't get a null.
			if (path == null)
			{
				return false;
			}

			// If the given path is longer than ourselves, then it won't be.
			if (levels.Length < path.levels.Length)
			{
				return false;
			}

			// If our root type isn't the same, then they don't match.
			if (isRelative != path.isRelative)
			{
				return false;
			}

			// Loop through the elements in the path and make sure they match
			// with the elements of this path.
			for (int index = 0;
				index < path.levels.Length;
				index++)
			{
				if (levels[index] != path.levels[index])
				{
					return false;
				}
			}

			// The current path has all the same elements as the given path.
			return true;
		}

		/// <summary>
		/// Returns the path when requested as a string.
		/// </summary>
		public override string ToString()
		{
			return Path;
		}

		/// <summary>
		/// Appends the level to the list, processing the "." and ".." elements
		/// into the list operation.
		/// </summary>
		/// <param name="levels">The levels.</param>
		/// <param name="level">The level.</param>
		private static void AppendLevel(
			List<string> levels,
			string level)
		{
			// Levels cannot be blank, so throw an exception if we get it.
			if (levels == null)
			{
				throw new ArgumentNullException("levels");
			}

			// If we have a blank level, we do nothing. This way, we can handle
			// various "//" or trailing "/" elements in the parse.
			if (String.IsNullOrEmpty(level))
			{
				return;
			}

			// Check for the path operations in the list.
			if (level == ".")
			{
				// This is a current path operation, which is simply skipped
				// (e.g., "/root/./child" = "/root/child").
				return;
			}

			if (level == "..")
			{
				// This is a "move up" operation which pops the last item off
				// the passed in levels from the list. If there is insuffient
				// levels, it will throw an exception.
				if (levels.Count == 0)
				{
					throw new InvalidPathException("Cannot parse .. with sufficient levels.");
				}

				levels.RemoveAt(levels.Count - 1);
				return;
			}

			// Otherwise, we just append the level to the list.
			levels.Add(level);
		}

		/// <summary>
		/// This parses the given path and sets the internal variables to
		/// represent the given path.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="context">The context.</param>
		/// <param name="options">The options.</param>
		private void ParsePath(
			string path,
			HierarchicalPath context,
			HierarchicalPathOptions options)
		{
			// Perform some sanity checking on the path
			if (String.IsNullOrEmpty(path))
			{
				throw new ArgumentNullException("path");
			}

			// Check for simple paths that are only a / (absolute root) or
			// . (relative root).
			if (path.Length == 1)
			{
				if (path == "/")
				{
					// We can short-cut the processing of this path since there
					// is only one element.
					isRelative = false;
					levels = new string[]
					{
					};
					return;
				}

				if (path == ".")
				{
					// This is a relative path that has no other elements inside
					// it. We can short-cut the parsing and finish up here.
					if (context != null)
					{
						isRelative = context.IsRelative;
						levels = context.levels;
					}
					else
					{
						isRelative = true;
						levels = new string[]
						{
						};
					}

					return;
				}
			}

			// We don't have a simple root path. Check to see if the path starts
			// with a forward slash. If it does, it is an absolute path, otherwise
			// it will be relative.
			var parsedLevels = new List<string>();
			int index = 0;

			if (path.StartsWith("/"))
			{
				// This is an absolute root path.
				isRelative = false;
				index++;
			}
			else
			{
				// This is a relative path, so prepend the context, if we have
				// one to our parsed levels.
				if (context != null)
				{
					// Copy the contents of the context.
					isRelative = context.IsRelative;
					parsedLevels.AddRange(context.Levels);
				}
				else
				{
					// This is a relative path.
					isRelative = true;
				}
			}

			// Go through the remaining elements of the string and break them
			// into the individual levels.
			var currentLevel = new StringBuilder();

			for (; index < path.Length;
				index++)
			{
				// Check for the next character.
				switch (path[index])
				{
					case '\\':
						// Check to see if the escape character is the last
						// character in the input string.
						if (index == path.Length - 1)
						{
							// We have an escape backslash but we are at the end of
							// the line.
							throw new InvalidPathException(
								"Cannot parse with escape at end of line: " + path);
						}

						// Grab the next character after the backslash.
						currentLevel.Append(path[index + 1]);
						index++;

						break;

					case '/':
						AppendLevel(parsedLevels, currentLevel.ToString());
						currentLevel.Length = 0;

						break;

					default:
						// Add the character to the current level.
						currentLevel.Append(path[index]);

						break;
				}
			}

			// Outside of the loop, we check to see if there is anything left
			// in the current level and add it to the list.
			AppendLevel(parsedLevels, currentLevel.ToString());

			// Saved the parsed levels into the levels property.
			levels = parsedLevels.ToArray();

			// If we are interning, then intern all the strings.
			if ((options & HierarchicalPathOptions.InternStrings)
				== HierarchicalPathOptions.InternStrings)
			{
				for (int i = 0;
					i < levels.Length;
					i++)
				{
					levels[i] = String.Intern(levels[i]);
				}
			}
		}

		#endregion

		#region Operators

		/// <summary>
		/// Implements the operator ==.
		/// </summary>
		/// <param name="c1">The c1.</param>
		/// <param name="c2">The c2.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator ==(HierarchicalPath c1,
			HierarchicalPath c2)
		{
			if (ReferenceEquals(null, c1)
				&& ReferenceEquals(null, c2))
			{
				return true;
			}

			return c1.Equals(c2);
		}

		/// <summary>
		/// Implements the operator !=.
		/// </summary>
		/// <param name="c1">The c1.</param>
		/// <param name="c2">The c2.</param>
		/// <returns>The result of the operator.</returns>
		public static bool operator !=(HierarchicalPath c1,
			HierarchicalPath c2)
		{
			return !(c1 == c2);
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an empty path that is either absolute or relative based
		/// </summary>
		public HierarchicalPath(bool isRelative)
		{
			this.isRelative = isRelative;
			levels = new string[]
			{
			};
		}

		/// <summary>
		/// Constructs a node reference using only the given path. If the
		/// path is invalid in any manner, including not being absolute,
		/// an exception is thrown.
		/// </summary>
		/// <param name="path">The path.</param>
		public HierarchicalPath(string path)
			: this(path, null, HierarchicalPathOptions.None)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPath"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="options">The options.</param>
		public HierarchicalPath(
			string path,
			HierarchicalPathOptions options)
		{
			// Create the path components
			ParsePath(path, null, options);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPath"/> class.
		/// </summary>
		/// <param name="levels">The levels.</param>
		/// <param name="isRelative">if set to <c>true</c> [is relative].</param>
		public HierarchicalPath(
			IEnumerable<string> levels,
			bool isRelative)
		{
			// Create a sub-array version of the path.
			var parts = new List<string>();

			foreach (string level in levels)
			{
				parts.Add(level);
			}

			// Save the components.
			this.levels = parts.ToArray();
			this.isRelative = isRelative;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPath"/> class.
		/// </summary>
		/// <param name="levels">The levels.</param>
		/// <param name="startIndex">The start index.</param>
		/// <param name="isRelative">if set to <c>true</c> [is relative].</param>
		public HierarchicalPath(
			IEnumerable<string> levels,
			int startIndex,
			bool isRelative)
		{
			// Create a sub-array version of the path.
			var parts = new List<string>();

			foreach (string level in levels)
			{
				parts.Add(level);
			}

			// Get the subset of those levels.
			this.levels = new string[parts.Count - startIndex];

			for (int index = startIndex;
				index < parts.Count;
				index++)
			{
				this.levels[index - startIndex] = parts[index];
			}

			this.isRelative = isRelative;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPath"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="context">The context.</param>
		public HierarchicalPath(
			string path,
			HierarchicalPath context)
			: this(path, context, HierarchicalPathOptions.None)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPath"/> class.
		/// </summary>
		/// <param name="path">The path.</param>
		/// <param name="context">The context.</param>
		/// <param name="options">The options.</param>
		public HierarchicalPath(
			string path,
			HierarchicalPath context,
			HierarchicalPathOptions options)
		{
			// Create the path components
			ParsePath(path, context, options);
		}

		#endregion

		#region Fields

		/// <summary>
		/// Contains static instance for an absolute root path (i.e., "/").
		/// </summary>
		public static readonly HierarchicalPath AbsoluteRoot =
			new HierarchicalPath(false);

		/// <summary>
		/// Contains a static instance of a relative root path (i.e., ".").
		/// </summary>
		public static readonly HierarchicalPath RelativeRoot =
			new HierarchicalPath(true);

		private bool isRelative;
		private string[] levels;

		#endregion
	}
}
