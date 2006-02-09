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
	using System.Text;

	/// <summary>
	/// Formats an object with various argument and positional
	/// parameters and produces a nice "usage" screen.
	/// </summary>
	public class UsageFormatter : Logable
	{
#region Attributes
		/// <summary>
		/// Returns a single formatted parameter name.
		/// </summary>
		protected virtual string FormatParameter(PositionalAttribute pa)
		{
			if (pa.IsOptional)
				return "[" + pa.Name + "]";
			else
				return pa.Name;
		}

		/// <summary>
		/// Returns a sorted list of positional attributes.
		/// </summary>
		protected virtual IList GetPositional(object container)
		{
			// Find all the potential members
			Type cType = container.GetType();
			MemberInfo [] mis =
				cType.FindMembers(MemberTypes.Field | MemberTypes.Property,
					BindingFlags.Public | BindingFlags.NonPublic
					| BindingFlags.Instance | BindingFlags.Static,
					null, null);
			Hashtable names = new Hashtable();

			foreach (MemberInfo mi in mis)
			{
				// Check for attributes
				object[] attributes =
					mi.GetCustomAttributes(typeof(PositionalAttribute), true);
	
				foreach (PositionalAttribute pa in attributes)
					names[pa.Index] = pa;
			}

			// Sort the list
			ArrayList list = new ArrayList();
			list.AddRange(names.Keys);
			list.Sort();

			// Format the list
			ArrayList ns = new ArrayList();

			foreach (int index in list)
			{
				ns.Add((PositionalAttribute) names[index]);
			}

			// We didn't find any
			return ns;
		}

		/// <summary>
		/// Returns a list of positional names, formatted for optional or
		/// non-optional as needed.
		/// </summary>
		protected virtual IList GetPositionalNames(object container)
		{
			// Format the list
			ArrayList ns = new ArrayList();

			foreach (PositionalAttribute pa in GetPositional(container))
			{
				ns.Add(FormatParameter(pa));
			}

			// We didn't find any
			return ns;
		}

		/// <summary>
		/// Returns true if there is at least one parameter
		/// (non-positional) defined.
		/// </summary>
		protected bool HasParameters(object container)
		{
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
					mi.GetCustomAttributes(typeof(IArgumentAttribute), true);

				if (attributes.Length > 0)
					return true;
			}

			// We didn't find any
			return false;
		}
#endregion

#region Formatting
		/// <summary>
		/// Formats the usage based on the object.
		/// </summary>
		public virtual void Format(object container, string message,
			Exception exception)
		{
			// Format the message
			FormatMessage(message, exception);

			// Write out the usage
			StringBuilder buffer = new StringBuilder();
			buffer.Append("USAGE:");

			/*
			  if (Prefix != "")
			  {
			  buffer.Append(" ");
			  buffer.Append(Prefix);
			  }
			*/

			// Check for options
			if (HasParameters(container))
			{
				buffer.Append(" [options]");
			}

			foreach (string posArg in GetPositionalNames(container))
			{
				buffer.Append(" ");
				buffer.Append(posArg);
			}

			// Write out the usage line
			WriteLine(buffer.ToString());

			// Go through the positionals
			IList list = GetPositional(container);

			if (list.Count > 0)
			{
				WriteLine("");
				WriteLine("Arguments:");

				foreach (PositionalAttribute pa in list)
				{
					WriteLine("  " + pa.Name + " - " + pa.Description);
				}
			}

			// Go through the arguments
		}

		/// <summary>
		/// Formats the message for this usage.
		/// </summary>
		protected virtual void FormatMessage(string message, Exception exception)
		{
			// Check for a message
			if (message != null)
				WriteLine(message);

			// Check for an exception
			if (exception != null)
				WriteLine(exception.ToString());
		}

		/// <summary>
		/// Writes out a single line of the usage.
		/// </summary>
		protected virtual void WriteLine(string message)
		{
			Console.WriteLine(message);
		}
#endregion
	}
}
