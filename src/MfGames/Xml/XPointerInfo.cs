// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace MfGames.Xml
{
	/// <summary>
	/// Information class that parses the XPointer and creates a representation
	/// of the framework. This is an immutable class once parsed.
	/// </summary>
	public class XPointerInfo
	{
		#region Properties

		/// <summary>
		/// Gets a value indicating whether this is a valid XPointer.
		/// </summary>
		public bool IsValid { get; private set; }

		#endregion

		#region Methods

		/// <summary>
		/// Uses the parsed XPointer to select from the given reader.
		/// </summary>
		/// <param name="xmlReader">The XML reader.</param>
		/// <returns></returns>
		public XPathNodeIterator SelectFrom(XmlReader xmlReader)
		{
			// Start by creating an XPath navigator.
			var xpathDocument = new XPathDocument(xmlReader);
			XPathNavigator navigator = xpathDocument.CreateNavigator();

			// Select and return the nodes.
			XPathNodeIterator nodes = navigator.Select(xpointers[0]);
			return nodes;
		}

		private void Parse(string xpointer)
		{
			// Go through the input string and loop through it.
			for (int i = 0;
				i < xpointer.Length;
				i++)
			{
				// Look for a function definition.
				string function;
				string argument;

				if (TryParseFunction(
					ref xpointer,
					out function,
					out argument))
				{
					// We parsed a function, so figure out what to do from there.
					switch (function)
					{
						case "xmlns":
							int equalIndex = argument.IndexOf(
								"=",
								StringComparison.Ordinal);
							string prefix = argument.Substring(
								0,
								equalIndex);
							string ns = argument.Substring(equalIndex + 1);

							namespaces[prefix] = ns;

							break;

						case "xpointer":
							// Add in an xpointer() function.
							xpointers.Add(argument);
							break;
					}
				}
			}

			// Mark ourselves as valid.
			IsValid = true;
		}

		/// <summary>
		/// Tries to parse a function from the beginning of the xpointer string.
		/// </summary>
		/// <param name="xpointer">The xpointer string to parse.</param>
		/// <param name="function">The resulting function, if found.</param>
		/// <param name="argument">The argument for the function, if found.</param>
		/// <returns>True if a function was found, otherwise false.</returns>
		private bool TryParseFunction(
			ref string xpointer,
			out string function,
			out string argument)
		{
			// If we have a null or blank string, skip it.
			if (string.IsNullOrEmpty(xpointer))
			{
				function = null;
				argument = null;
				return false;
			}

			// Look for a parser function at the beginning of the string.
			var regex = new Regex("^(\\w+)\\(");
			Match match = regex.Match(xpointer);

			if (!match.Success)
			{
				function = null;
				argument = null;
				return false;
			}

			// Remove the function from the xpointer.
			xpointer = xpointer.Substring(match.Groups[0].Length);

			// Pull out the arguments which is the first unbalanced close paren.
			int parentCount = 1;

			for (int i = 0;
				i < xpointer.Length;
				i++)
			{
				if (xpointer[i] == '(')
				{
					// If we get a "(" we need one additional ")" before we're
					// done.
					parentCount++;
				}

				if (xpointer[i] == ')')
				{
					// We decrement the counter when we encounter a ")". If this
					// is the last one, then we set the argument, trim it off
					// the pointer, and finish up.
					if (--parentCount == 0)
					{
						// We get the function from the regular expression and
						// the argument from the length we just figured out.
						function = match.Groups[1].Value;
						argument = xpointer.Substring(
							0,
							i);

						// Strip off the function including the trailing ")".
						xpointer = xpointer.Substring(i + 1);

						// Return true to indicate we found the function.
						return true;
					}
				}
			}

			// Populate the fields and return it.
			function = null;
			argument = null;
			return true;
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="XPointerInfo"/> class.
		/// </summary>
		/// <param name="xpointer">The xpointer.</param>
		public XPointerInfo(string xpointer)
		{
			// Allocate the internal specifications.
			namespaces = new Dictionary<string, string>();
			xpointers = new List<string>();

			// If we have a blank string, we aren't valid.
			if (string.IsNullOrEmpty(xpointer))
			{
				IsValid = false;
				return;
			}

			// Parse the contents of the XPointer.
			Parse(xpointer);
		}

		#endregion

		#region Fields

		private readonly Dictionary<string, string> namespaces;
		private readonly List<string> xpointers;

		#endregion
	}
}
