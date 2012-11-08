// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;
using System.Collections.Generic;
using System.Xml;

namespace MfGames.Xml
{
	/// <summary>
	/// An XML reader that automatically processes XInclude elements.
	/// </summary>
	public class XIncludeReader: XmlProxyReader
	{
		#region Methods

		/// <summary>
		/// When overridden in a derived class, reads the next node from the stream.
		/// </summary>
		/// <returns>
		/// true if the next node was read successfully; false if there are no more nodes to read.
		/// </returns>
		/// <exception cref="T:System.Xml.XmlException">
		/// An error occurred while parsing the XML.
		///   </exception>
		public override bool Read()
		{
			// Read in the next XML string.
			bool successful = base.Read();

			if (!successful)
			{
				// See if we have another reader on the stack.
				if (xmlReaderStack.Count > 0)
				{
					ReadEndIncludeElement();
					return Read();
				}

				return false;
			}

			// Check to see if we are working with an XInclude reference.
			if (NamespaceURI == XmlConstants.XIncludeNamespace2003
				|| NamespaceURI == XmlConstants.XIncludeNamespace2001)
			{
				if (LocalName == "include")
				{
					// Check for the node type.
					if (NodeType == XmlNodeType.Element)
					{
						// We need to attempt to load in a new element.
						ReadStartIncludeElement();
					}

					// Simply move to the next node since we already dealt with
					// both the Element and EndElement.
					return Read();
				}
			}

			// We were otherwise successful.
			return true;
		}

		/// <summary>
		/// Gets the included XML reader based on the current node.
		/// </summary>
		/// <returns></returns>
		protected virtual XmlReader GetIncludedXmlReader()
		{
			// Grab the @href element.
			string href = GetAttribute("href");

			if (href == null)
			{
				throw new ApplicationException("Cannot include href from the XInclude tag.");
			}

			// Create the resulting XML reader
			XmlReader reader = Create(href);

			return reader;
		}

		/// <summary>
		/// Called when an XInclude element was closed.
		/// </summary>
		protected virtual void ReadEndIncludeElement()
		{
			PopXmlReader();
		}

		/// <summary>
		/// Called when an XInclude element was found.
		/// </summary>
		protected virtual void ReadStartIncludeElement()
		{
			PushXmlReader();
		}

		/// <summary>
		/// Pops the XML reader from the stack and moves back to the previous one.
		/// </summary>
		private void PopXmlReader()
		{
			// Close the current reader.
			UnderlyingReader.Close();

			// Pull out the next reader from the stack.
			int index = xmlReaderStack.Count - 1;

			UnderlyingReader = xmlReaderStack[index];

			// Remove the reader from the stack.
			xmlReaderStack.RemoveAt(index);
		}

		/// <summary>
		/// Uses the current node to figure out a new XML reader to use for
		/// the remaining stream or to use the fallback.
		/// </summary>
		private void PushXmlReader()
		{
			// Get the XML reader for the current node.
			XmlReader innerXmlReader = GetIncludedXmlReader();

			if (innerXmlReader != null)
			{
				// Push this reader on the stack.
				xmlReaderStack.Add(UnderlyingReader);

				// Add the 
				UnderlyingReader = innerXmlReader;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="XIncludeReader"/> class.
		/// </summary>
		/// <param name="underlyingReader">The underlying reader.</param>
		public XIncludeReader(XmlReader underlyingReader)
			: base(underlyingReader)
		{
			xmlReaderStack = new List<XmlReader>();
		}

		#endregion

		#region Fields

		private readonly List<XmlReader> xmlReaderStack;

		#endregion
	}
}
