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
	using System.ComponentModel;
	using System.Reflection;

	/// <summary>
	/// This class manages command line argument processing. This also
	/// does the various processing on the command line using the
	/// Process() method.
	/// </summary>
	public class CommandLineArguments : Logable
	{
		/// <summary>
		/// Constructs the object from the given string [] block.
		/// </summary>
		public CommandLineArguments(string [] args)
		{
			// Check for nulls
			if (args == null)
				throw new ToolException("Cannot create arguments from a null "
					+ "argument");

			// Save the arguments
			unparsed = new ArrayList(args.Length);
			unparsed.AddRange(args);
		}

#region Processing
		private ArrayList scanners = new ArrayList();

		/// <summary>
		/// Contains the array list of scanners.
		/// </summary>
		public ArrayList Scanners
		{
			get { return scanners; }
		}

		/// <summary>
		/// Attempts to assign the value into a property or field that has
		/// the appropriate attribute and matches the name.
		/// </summary>
		private bool Assign(object container, ArgumentScanner scanner)
		{
			// Keep track if we found at least one
			bool found = false;

			// Find all the potential members
			Type cType = container.GetType();
			MemberInfo [] mis =
				cType.FindMembers(MemberTypes.Field | MemberTypes.Property,
					BindingFlags.Public | BindingFlags.NonPublic
					| BindingFlags.Instance | BindingFlags.Static,
					null, null);

			foreach (MemberInfo mi in mis)
			{
				// Check for attributes
				object[] attributes = mi.GetCustomAttributes(scanner.AttributeType,
					true);

				foreach (IArgumentAttribute iaa in attributes)
				{
					// Check the name with the parameter
					if (!scanner.Arguments.Contains(iaa.Name))
					{
						// Don't bother
						continue;
					}

					// Make sure we can set this value

					// Get our variables and assign the value
					string [] vals = (string []) scanner.Arguments[iaa.Name];

					if (Assign(container, mi, vals, true))
						found = true;
				}
			}

			// Return if we found at least one
			return found;
		}

		/// <summary>
		/// Assigns the given array of strings into the given member.
		/// </summary>
		private bool Assign(object container, MemberInfo mi,
			string [] vals, bool isTrue)
		{
			// Switch based on the type of property
			if (mi is PropertyInfo)
			{
				// Cast it
				PropertyInfo pi = (PropertyInfo) mi;

				// Check for boolean or other special formatting
				if (pi.PropertyType == typeof(bool))
				{
					// Check for yes or no
					if (vals.Length > 0 && vals[0].ToLower() == "yes")
						isTrue = true;
					else if (vals.Length > 0 && vals[0].ToLower() == "no")
						isTrue = false;

					// Set the value
					try
					{
						pi.SetValue(container, isTrue, null);
					}
					catch (Exception e)
					{
						throw new ToolException("Cannot assign the "
							+ mi + " property: " + isTrue, e);
					}

					return true;
				}

				// If all else fails
				try
				{
					pi.SetValue(container, MapType(pi.PropertyType, vals), null);
				}
				catch (Exception e)
				{
					throw new ToolException("Cannot assign the "
						+ mi + " property: " + vals, e);
				}
			}
			else if (mi is FieldInfo)
			{
				// Cast it
				FieldInfo fi = (FieldInfo) mi;

				// Check for boolean or other special formatting
				if (fi.FieldType == typeof(bool))
				{
					// Check for yes or no
					if (vals.Length > 0 && vals[0].ToLower() == "yes")
						isTrue = true;
					else if (vals.Length > 0 && vals[0].ToLower() == "no")
						isTrue = false;

					// Set the value
					try
					{
						fi.SetValue(container, isTrue);
					}
					catch (Exception e)
					{
						throw new ToolException("Cannot assign the "
							+ mi + " field: " + isTrue, e);
					}

					return true;
				}

				// If all else fails
				try
				{
					fi.SetValue(container, MapType(fi.FieldType, vals));
				}
				catch (Exception e)
				{
					throw new ToolException("Cannot assign the "
						+ mi + " field: " + vals, e);
				}
			}

			// Return good
			return true;
		}

		/// <summary>
		/// Attempts to assign a positional argument, with a given
		/// index. This scans through the container object, find the
		/// appropriate positional attribute, and sets that member.
		///
		/// This returns true if it can be assigned. If multiple
		/// properties or fields can be set, they are.
		/// </summary>
		private bool Assign(object container, string argument, int position)
		{
			// Keep track if we found at least one
			bool found = false;

			// Find all the potential members
			Type cType = container.GetType();
			MemberInfo [] mis =
				cType.FindMembers(MemberTypes.Field | MemberTypes.Property,
					BindingFlags.Public | BindingFlags.NonPublic
					| BindingFlags.Instance | BindingFlags.Static,
					null, null);

			foreach (MemberInfo mi in mis)
			{
				// Check for attributes
				object[] attributes =
					mi.GetCustomAttributes(typeof(PositionalAttribute), true);

				foreach (PositionalAttribute pa in attributes)
				{
					// Check the name with the parameter
					if (pa.Index != position)
					{
						// Don't bother
						continue;
					}

					// Make sure we can set this value

					// Get our variables and assign the value
					string [] vals = new string [] { argument };

					if (Assign(container, mi, vals, true))
						found = true;
				}
			}

			// Return if we found at least one
			return found;
		}

		/// <summary>
		/// Checks for the required positional attributes. If there are
		/// unassigned positional attributes, and they were not set, it
		/// throws an exception.
		/// </summary>
		protected void CheckPositionalCount(object container, int index)
		{
			// Find all the potential members
			Type cType = container.GetType();
			MemberInfo [] mis =
				cType.FindMembers(MemberTypes.Field | MemberTypes.Property,
					BindingFlags.Public | BindingFlags.NonPublic
					| BindingFlags.Instance | BindingFlags.Static,
					null, null);
	
			// Loop forever
			while (true)
			{
				// Go through each member
				bool found = false;

				foreach (MemberInfo mi in mis)
				{
					// Check for attributes
					object[] attributes =
						mi.GetCustomAttributes(typeof(PositionalAttribute), true);
	  
					foreach (PositionalAttribute pa in attributes)
					{
						// Check index
						if (pa.Index == index)
						{
							// We found it
							found = true;

							// Check for optional
							if (pa.IsOptional)
								return;

							// Check for positional
							throw new ToolException("Missing positional attribute "
								+ index);
						}
					}
				}

				// Check if we found it
				if (!found)
					return;

				// Increment it
				index++;
			}
		}

		/// <summary>
		/// Processes the command-line arguments, using the various
		/// attributes of the given class to handle the processing.
		/// Found parameters are removed.
		/// </summary>
		public void Process(object container)
		{
			Process(container, true);
		}

		/// <summary>
		/// Processes the command-line arguments, using the various
		/// attributes of the given class to handle the processing. If the
		/// second parameter is true, then the arguments are removed as
		/// they are processed.
		/// </summary>
		public void Process(object container, bool remove)
		{
			// Go through each of our scanners for each of the elements
			ArrayList remain = new ArrayList();
			int positionalIndex = 0;
      
			foreach (string arg in unparsed)
			{
				// Try to process each one
				bool found = false;

				foreach (ArgumentScanner scanner in scanners)
				{
					// Try to match this one
					if (scanner.IsMatch(arg))
					{
						// We found it and the data is populated, so we need to
						// find an attribute that matches.
						if (!Assign(container, scanner))
						{
							// We couldn't assign it, but we matched, so keep going
							continue;
						}

						// See if we need to remove it
						if (!remove)
						{
							remain.Add(arg);
						}

						// We are done with scanning
						found = true;
						break;
					}
				}

				// If we didn't find it the first time, check a positional
				if (!found && Assign(container, arg, positionalIndex))
				{
					found = true;
					positionalIndex++;
				}

				// If we didn't find it, this time, add it to the list
				if (!found)
					remain.Add(arg);
			}

			// Check the positional attributes are okay
			CheckPositionalCount(container, positionalIndex);

			// Replace the arguments
			unparsed = remain;
		}

		/// <summary>
		/// Maps the string [] values to a type that is acceptable to the
		/// given type.
		/// </summary>
		private object MapType(Type type, string [] args)
		{
			// Get a converter for this specific type
			TypeConverter converter = TypeDescriptor.GetConverter(type);

			if (converter.CanConvertFrom(args.GetType()))
			{
				// Assign the string array directly
				return converter.ConvertFrom(args);
			}
			else if (converter.CanConvertFrom(typeof(string)))
			{
				// Combine the strings
				string arg = String.Join(",", args);
				return converter.ConvertFromString(arg);
			}

			// We can't convert, so complain to the user
			throw new ToolException("Cannot convert to " + type);
		}
#endregion

#region Properties
		private ArrayList unparsed;

		/// <summary>
		/// Contains the list of arguments that remain unparsed. This is a
		/// live copy, so changes are reflected back in this object.
		/// </summary>
		public ArrayList RemainingArguments
		{
			get { return unparsed; }
		}
#endregion
	}
}
