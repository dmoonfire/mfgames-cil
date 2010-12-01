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
using System.Text;

using MfGames.Exceptions;

#endregion

namespace MfGames
{
	/// <summary>
	/// Represents an hierarchial path given in the same form as a Unix file
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
	/// HierarchialPath is a read-only object. Once created, no methods directly
	/// alter the object. Instead, they return a new modified path.
	/// </summary>
	[Serializable]
	public class HierarchicalPath
	{
		#region Constants

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

		#endregion

		#region Constructors

		/// <summary>
		/// Creates an empty path that is either absolute or relative based
		/// </summary>
		public HierarchicalPath(bool isRelative)
		{
			this.isRelative = isRelative;
			levels = new string[] { };
		}

		/// <summary>
		/// Constructs a node reference using only the given path. If the
		/// path is invalid in any manner, including not being absolute,
		/// an exception is thrown.
		/// </summary>
		public HierarchicalPath(string path)
		{
			// Create the path components
			ParsePath(path, null);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPath"/> class.
		/// </summary>
		/// <param name="levels">The levels.</param>
		/// <param name="isRelative">if set to <c>true</c> [is relative].</param>
		public HierarchicalPath(
			string[] levels,
			bool isRelative)
		{
			this.levels = levels;
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
		{
			// Create the path components
			ParsePath(path, context);
		}

		#endregion

		#region Path Construction

		/// <summary>
		/// This parses the given path and sets the internal variables to
		/// represent the given path.
		/// </summary>
		private void ParsePath(
			string path,
			HierarchicalPath context)
		{
			// Perform some sanity checking on the path
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}

			// Check for the leading characters. If the string starts with "."
			// or "./" then it is a relative root. If it starts with a "/",
			// then it is an absolute path. In all other cases, an invalid
			// exception is thrown.
			var parsedLevels = new List<string>();
			int index = 0;

			if (path == "/")
			{
				// We can short-cut the processing of this path since there
				// is only one element.
				isRelative = false;
				levels = new string[] { };
				return;
			}

			if (path == ".")
			{
				// This is a relative path that has no other elements inside
				// it. We can short-cut the parsing and finish up here.
				isRelative = true;
				levels = new string[] { };
				return;
			}

			if (path.StartsWith("/"))
			{
				// This is an absolute root path.
				isRelative = false;
				index++;
			}
			else if (path.StartsWith("./"))
			{
				// This is a relative path but it potentially has more after
				// it is. So, we first check to see if we have a context to
				// see if we need to prefix the path.
				if (context != null)
				{
					// Copy the contents of the context.
					isRelative = context.IsRelative;
					parsedLevels.AddRange(context.Levels);
				}

				// Move the index forward two to represent the characters we've
				// already parsed.
				index += 2;
			}
			else
			{
				// Unknown leading characters, throw an exception to break out.
				throw new InvalidPathException("Cannot parse path: " + path);
			}

			// Go through the remaining elements of the string and break them
			// into the individual levels.
			var currentLevel = new StringBuilder();

			for (; index < path.Length; index++)
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
						// This is a new path element, so first take the current
						// buffer and add it if it has a length.
						if (currentLevel.Length > 0)
						{
							// Check to see if we are a "move up" operation of "..".
							if (currentLevel.ToString() == "..")
							{
								// Instead of adding to the level, we clear off the
								// last one from the list. If there is not one,
								// we throw an exception.
								if (parsedLevels.Count == 0)
								{
									throw new InvalidCastException(
										"Cannot parse .. with sufficient levels.");
								}

								// Remove the last level parsed and ignore the
								// "..".
								parsedLevels.RemoveAt(parsedLevels.Count - 1);
							}
							else
							{
								// Add the current level to the list of levels.
								parsedLevels.Add(currentLevel.ToString());
							}

							// Clear out the current level.
							currentLevel.Length = 0;
						}

						break;

					default:
						// Add the character to the current level.
						currentLevel.Append(path[index]);

						break;
				}
			}

			// Outside of the loop, we check to see if there is anything left
			// in the current level and add it to the list.
			if (currentLevel.Length > 0)
			{
				// Check to see if we are a "move up" operation of "..".
				if (currentLevel.ToString() == "..")
				{
					// Instead of adding to the level, we clear off the
					// last one from the list. If there is not one,
					// we throw an exception.
					if (parsedLevels.Count == 0)
					{
						throw new InvalidCastException("Cannot parse .. with sufficient levels.");
					}

					// Remove the last level parsed and ignore the
					// "..".
					parsedLevels.RemoveAt(parsedLevels.Count - 1);
				}
				else
				{
					// Add the current level to the list of levels.
					parsedLevels.Add(currentLevel.ToString());
				}
			}

			// Saved the parsed levels into the levels property.
			levels = parsedLevels.ToArray();
		}

		#endregion

		#region Path Properties

		private bool isRelative;
		private string[] levels;

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
				return isRelative ? "." : "/";
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
		/// Returns the nth element of the path.
		/// </summary>
		public string this[int index]
		{
			get { return levels[index]; }
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
				return isRelative ? "." : "/";
			}
		}

		/// <summary>
		/// Contains an array of individual levels within the path.
		/// </summary>
		public string[] Levels
		{
			get { return levels; }
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
					return isRelative ? "." : "/";
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

		#region Comparison

		/// <summary>
		/// Compares two node references.
		/// </summary>
		public override bool Equals(object obj)
		{
			var path = (HierarchicalPath) obj;
			return Path.Equals(path.Path);
		}

		/// <summary>
		/// Overrides the hash code to prevent the errors.
		/// </summary>
		public override int GetHashCode()
		{
			return isRelative.GetHashCode() ^ levels.GetHashCode();
		}

		#endregion

		#region Conversion

		/// <summary>
		/// Returns the path when requested as a string.
		/// </summary>
		public override string ToString()
		{
			return Path;
		}

		#endregion

		#region Child Path Operations

		/// <summary>
		/// A simple accessor that allows retrieval of a child path
		/// from this one. This, in effect, calls CreateChild(). The
		/// exception is if the path is given as ".." which then returns
		/// the parent object as appropriate (this will already return
		/// something, unlike ParentPath or Parent.
		/// </summary>
		public HierarchicalPath this[string childPath]
		{
			get { return CreateChild(childPath); }
		}

		/// <summary>
		/// Creates a child from this node, by creating the path that uses
		/// this object as a context.
		/// </summary>
		public HierarchicalPath CreateChild(string childPath)
		{
			// By the rules, prefixing "./" will also create the desired
			// results and use this as a context.
			return new HierarchicalPath("./" + childPath, this);
		}

		#endregion

		#region Parent Path Operations

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
					return isRelative ? RelativeRoot : AbsoluteRoot;
				}

				// Create a new path without the last item in it.
				var parentLevels = new string[levels.Length - 1];

				for (int index = 0; index < levels.Length - 1; index++)
				{
					parentLevels[index] = levels[index];
				}

				return new HierarchicalPath(parentLevels, isRelative);
			}
		}

		#endregion
	}
}