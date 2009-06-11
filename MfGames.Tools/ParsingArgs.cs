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
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

using MfGames.Logging;

#endregion

namespace MfGames.Tools
{
	/// <summary>
	/// public class for handling the arguments for parsing a
	/// command-line element.
	/// </summary>
	public class ParsingArgs
	{
		#region Constants

		/// <summary>
		/// Contains the regex for identifying an argument.
		/// </summary>
		private static readonly Regex ArgumentRegex =
			new Regex("^([A-Za-z0-9_-]+)([:=](.*))?$", RegexOptions.Compiled);

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs the arguments element and pops off the first
		/// element for processing.
		/// </summary>
		public ParsingArgs(List<string> args, object container)
		{
			// We have a problem if blank
			if (args == null || args.Count == 0)
				throw new Exception("Cannot build arguments on an empty list");

			if (container == null)
				throw new Exception("Cannot use a null container object");

			// Save the elements
			this.container = container;
			arguments = args;

			// Remove the first item
			first = args[0];
			args.RemoveAt(0);
		}

		#endregion

		#region Properties

		private List<string> arguments;
		private object container;
		private string first;
		private bool ignoreDash = false;
		private int lastPosition = -1;
		private Log log = new Log(typeof(ParsingArgs));
		private int position = 0;

		/// <summary>
		/// Contains a list of other arguments of this property.
		/// </summary>
		public List<string> Arguments
		{
			get { return arguments; }
		}

		/// <summary>
		/// Contains the first token or element on the command-line.
		/// </summary>
		public string First
		{
			get { return first; }
		}

		/// <summary>
		/// This contains the last position that was set by the
		/// setter.
		/// </summary>
		public int LastPosition
		{
			get { return lastPosition; }
		}

		#endregion

		#region Parsing

		/// <summary>
		/// Returns the first element and grabs a new one.
		/// </summary>
		public string Advance()
		{
			// Save the old one
			string old = first;

			// Sanity checking
			if (arguments.Count > 0)
			{
				// Save the new first
				first = arguments[0];
				arguments.RemoveAt(0);
			}
			else
			{
				// Just clear the first
				first = null;
			}

			// Return the old value
			return old;
		}

		/// <summary>
		/// Processes the first element and attempt to place
		/// everything in the proper place. This returns false if it
		/// cannot be processed, otherwise, it returns true.
		/// </summary>
		public bool Process()
		{
			// Check out the first one
			if (first == "--")
			{
				// -- means to stop processing all parameters
				// this point. So "tool -- -a" would give "-a" as the
				// first positional attribute.
				ignoreDash = true;
				Advance();
				return true;
			}
			else if (!ignoreDash && first.StartsWith("--"))
			{
				// This is a long attribute. They can be in any of the
				// follow methods:
				// --argument
				// --argument=bob
				// --argument bob
				// --no-argument
				return ProcessLong(first.Substring(2));
			}
			else if (!ignoreDash && first.StartsWith("-"))
			{
				// Short arguments. These are single letter of the
				// format:
				// -a
				// -afilename
				// -a filename
				// -abcd (which is -a -b -c -d equiv)
				return ProcessShort(first.Substring(1));
			}
			else
			{
				// This is a potentially positional
				// argument. Positional are identified by scanning the
				// container for one more properties that have the
				// Positional attribute of the appropriate number.
				return ProcessPositional();
			}
		}

		/// <summary>
		/// Processes a long argument. This is in the format of:
		///
		///   argument
		///   argument=value
		///   argument value
		///   argument=value,value,value
		///   argument value,value,value
		///   argument:value
		///   argument:value,value,value
		///
		/// For simplicity sake, an argument can only be letters,
		/// numbers, dash, and hypehn (with dash and hypen
		/// interchangable). If 'argument' is a bool value, then no
		/// argument is needed.
		/// </summary>
		private bool ProcessLong(string arg)
		{
			// See if we have a match
			if (!ArgumentRegex.IsMatch(arg))
				// Not sure what to do
				return false;

			// Pull it out
			Match m = ArgumentRegex.Match(arg);
			string name = m.Groups[1].ToString().Replace("_", "-");

			// See if we are negating it (for --no-argument)
			bool isNo = false;

			if (name.StartsWith("no-"))
			{
				name = name.Substring(3);
				isNo = true;
			}

			// Search for the arguments
			MemberInfo mi = GetSetter(name);

			if (mi == null)
				return false;

			// Figure out the type for later
			var fi = mi as FieldInfo;
			var pi = mi as PropertyInfo;
			Type type = null;

			// Figure out how to set it
			if (fi != null)
				type = fi.FieldType;
			else if (pi != null)
				type = pi.PropertyType;

			// See if we are a property
			string value = m.Groups[3].ToString();

			if (type != typeof(Boolean))
			{
				// We need to grab the next argument
				if (m.Groups[2].ToString().Length == 0)
				{
					// We didn't have "argument=" or "argument:"
					value = Advance();

					// If we don't have a value, burp
					if (value == null)
					{
						log.Error("Cannot find a property value");
						return false;
					}
				}

				// We have a property, so set it
				if (fi != null)
					fi.SetValue(container, MapType(type, value));
				else if (pi != null)
					pi.GetSetMethod().Invoke(container, new object[] { MapType(type, value) });
			}
			else
			{
				// Set the value
				if (fi != null)
					fi.SetValue(container, !isNo);
				else if (pi != null)
					pi.GetSetMethod().Invoke(container, new object[] { !isNo });
			}

			// We processed it
			Advance();
			return true;
		}

		/// <summary>
		/// Goes through all the positional attributes and assigns a
		/// value, if possible. Otherwise, return false.
		/// </summary>
		private bool ProcessPositional()
		{
			// Get the list of all possible attributes
			Type cType = container.GetType();
			MemberInfo[] mis = cType.FindMembers(
				MemberTypes.Field | MemberTypes.Property,
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
				BindingFlags.Static,
				null,
				null);

			foreach (MemberInfo mi in mis)
			{
				// Go through each of the attributes
				foreach (Attribute a in mi.GetCustomAttributes(true))
				{
					// Ignore if we aren't a positional
					if (!(a is PositionalAttribute))
						continue;

					// See if the positional is right
					var pa = (PositionalAttribute) a;

					if (pa.Index == position)
					{
						// We found the right property
						if (mi is FieldInfo)
						{
							// Set this as a field value
							var fi = (FieldInfo) mi;
							Type type = fi.FieldType;

							fi.SetValue(container, MapType(type, first));
						}
						else if (mi is PropertyInfo)
						{
							// Cast it and call the setter
							var pi = (PropertyInfo) mi;
							Type ptype = pi.PropertyType;

							pi.GetSetMethod().Invoke(container,
							                         new object[] { MapType(ptype, first) });
						}

						// Advance the parser and continue
						lastPosition = position;
						position++;
						Advance();
						return true;
					}
				}
			}

			// Didn't find it
			return false;
		}

		/// <summary>
		/// This processes short elements, either a single one (-k) or
		/// a series of short ones (-kab) with a potential field
		/// following.
		/// </summary>
		private bool ProcessShort(string arg)
		{
			return false;
		}

		#endregion

		#region Reflection

		/// <summary>
		/// Scans the container object for a setting that fulfills the
		/// given name, doing a case-insensitive search on the name
		/// and a list of properties. Properties with a Positional
		/// attribute are not scanned and the name is not used if a
		/// Optional attribute is present.
		/// </summary>
		private MemberInfo GetSetter(string name)
		{
			// Get the list of all possible attributes
			Type cType = container.GetType();
			MemberInfo[] mis = cType.FindMembers(
				MemberTypes.Field | MemberTypes.Property,
				BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
				BindingFlags.Static,
				null,
				null);

			foreach (MemberInfo mi in mis)
			{
				// Mark if we found something
				bool useName = true;
				bool nameFound = false;

				// Go through each of the attributes
				foreach (Attribute a in mi.GetCustomAttributes(true))
				{
					// Ignore if we are a positional
					if (a is PositionalAttribute)
					{
						nameFound = false;
						useName = false;
						break;
					}

					// If we aren't an optional, ignore it
					if (!(a is OptionalAttribute))
						continue;

					// Process the optional attribute
					var oa = (OptionalAttribute) a;

					if (oa.Name == name)
					{
						nameFound = true;
						break;
					}
				}

				// If we found it, but don't use it, ignore this
				if (nameFound && !useName)
					continue;

				// See if we found a name
				if (!nameFound && name.ToLower() == mi.Name.ToLower())
					// We use this one anways
					nameFound = true;

				// If we haven't found the name, stop
				if (!nameFound)
					continue;

				// At this point, we have matched on a name, so we
				// need to do some additional validation, then set the
				// property or field.
				if (mi is FieldInfo)
				{
					// Don't do anything
					return mi;
				}
				else if (mi is PropertyInfo)
				{
					// Make sure we can set things
					if (!((PropertyInfo) mi).CanWrite)
					{
						log.Error("Cannot set property value: {0}", mi.Name);
						continue;
					}

					// Return the results
					return mi;
				}
				else
				{
					log.Error("Cannot set property: {0}", mi);
					continue;
				}
			}

			// We couldn't find anything
			return null;
		}

		/// <summary>
		/// Converts a property into a mappable one, including some
		/// that are not automatically processed by the system.
		/// </summary>
		private object MapType(Type type, string value)
		{
			// Check for file info
			if (type == typeof(FileInfo))
				return new FileInfo(value);

				// Check for DirectoryInfo
			else if (type == typeof(DirectoryInfo))
				return new DirectoryInfo(value);

				// Check for integers
			else if (type == typeof(Int32))
				return Int32.Parse(value);

				// Fallback to everything else
			else
				return value;
		}

		#endregion
	}
}