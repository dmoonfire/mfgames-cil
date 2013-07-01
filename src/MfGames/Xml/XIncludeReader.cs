// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.XPath;

namespace MfGames.Xml
{
	/// <summary>
	/// An XML reader that automatically processes XInclude elements.
	/// </summary>
	public class XIncludeReader: XmlProxyReader
	{
		#region Properties

		/// <summary>
		/// Gets the normalized base URI, which is always set.
		/// </summary>
		public Uri NormalizedBaseUri
		{
			get
			{
				string baseUriString = String.IsNullOrEmpty(BaseURI)
					? Path.Combine(Environment.CurrentDirectory, "temporary.xml")
					: BaseURI;
				var uri = new Uri(baseUriString);

				return uri;
			}
		}

		/// <summary>
		/// Gets the underlying XML reader.
		/// </summary>
		protected override XmlReader UnderlyingReader
		{
			get { return CurrentItem.Reader; }
			set
			{
				// Do nothing here.
			}
		}

		/// <summary>
		/// Gets the current item in the stack.
		/// </summary>
		private StackItem CurrentItem
		{
			get { return stack[0]; }
		}

		#endregion

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
				// If we aren't successful, check to see if we are at the last
				// item in the stack. If we are, then we leave it there so any
				// call into the underlying reader will work on that final (or
				// outermost) XML.
				if (stack.Count == 1)
				{
					return false;
				}

				// Pop the stack and recurse into ourselves because we'll be
				// picking up the new popped reader instead of the previous
				// one. There is a slight risk of stack overflow, but we're
				// hoping that someone isn't finishing up 200+ readers in a
				// single loop.
				FinishedCurrentReader();

				// Execute the read on the new event and pass it in.
				successful = Read();
				return successful;
			}

			// If we are trying to write out the XML declaration with an inner
			// element, then skip it since that would be an invalid state.
			if (NodeType == XmlNodeType.XmlDeclaration
				&& stack.Count > 1)
			{
				successful = Read();
				return successful;
			}

			// Check to see if we are working with an XInclude reference.
			if (NamespaceURI == XmlConstants.XIncludeNamespace2003
				|| NamespaceURI == XmlConstants.XIncludeNamespace2001)
			{
				if (LocalName == "include")
				{
					// We don't worry about the ending tag, but we need to
					// handle the start tag if we encounter it.
					if (NodeType == XmlNodeType.Element)
					{
						// We need to attempt to load in a new element.
						StartNewReader();
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
		protected virtual IEnumerable<XmlReader> CreateIncludedReaders()
		{
			// Grab the @href element and make sure we have it.
			string href = GetAttribute("href");

			if (href == null)
			{
				throw new ApplicationException(
					"Cannot locate href attribute from the XInclude tag.");
			}

			// Figure out the base URI for the current reader.
			Uri baseUri = NormalizedBaseUri;

			// Figure out the URI for the new one and use that to create an
			// XML stream.
			var newUri = new Uri(baseUri, href);
			XmlReader reader = Create(newUri.ToString());
			var includeReader = new XIncludeReader(reader);

			// Check to see if we have an XPointer element.
			string xpointerAttribute = GetAttribute("xpointer");

			if (!string.IsNullOrEmpty(xpointerAttribute))
			{
				// Parse the XPointer from this.
				var xpointer = new XPointerInfo(xpointerAttribute);

				if (xpointer.IsValid)
				{
					// Wrap the above reader in an XPath document and pull out
					// the nodes.
					XPathNodeIterator nodes = xpointer.SelectFrom(includeReader);

					// Create a list of readers from the given nodes.
					var includedReaders = new List<XmlReader>();

					while (nodes.MoveNext())
					{
						XPathNavigator node = nodes.Current;

						if (node != null)
						{
							XmlReader nodeReader = node.ReadSubtree();

							includedReaders.Add(nodeReader);
						}
					}

					// Return the resulting readers.
					return includedReaders;
				}
			}

			// Return the resulting reader.
			return new[]
			{
				includeReader
			};
		}

		/// <summary>
		/// Pops the XML reader from the stack and moves back to the previous one.
		/// </summary>
		private void FinishedCurrentReader()
		{
			// Get the current stack item.
			StackItem item = CurrentItem;

			// Close the reader since we're done with it.
			item.Reader.Close();

			// Remove the reader from the front of the stack.
			stack.RemoveAt(0);
		}

		/// <summary>
		/// Uses the current node to figure out a new XML reader to use for
		/// the remaining stream or to use the fallback.
		/// </summary>
		private void StartNewReader()
		{
			// Get the XML reader for the current node. This is a virtual method
			// to allow an extending class to create an appropriate reader.
			IEnumerable<XmlReader> readers = CreateIncludedReaders();

			if (readers == null)
			{
				return;
			}

			// Go through all the readers.
			List<StackItem> items = readers.Select(
				newReader => new StackItem
				{
					Reader = newReader
				}).ToList();

			// Insert the reader into the first position, as the "head" of
			// the stack.
			stack.InsertRange(0, items);
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="XIncludeReader"/> class.
		/// </summary>
		/// <param name="file">The file.</param>
		public XIncludeReader(FileInfo file)
			: this(Create(file.FullName))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XIncludeReader"/> class.
		/// </summary>
		/// <param name="underlyingReader">The underlying reader.</param>
		public XIncludeReader(XmlReader underlyingReader)
			: base(underlyingReader)
		{
			// Create the stack we use for handling XInclude elements.
			stack = new List<StackItem>();

			// Wrap the first reader in the stack.
			var item = new StackItem
			{
				Reader = underlyingReader
			};

			stack.Add(item);
		}

		#endregion

		#region Fields

		private readonly List<StackItem> stack;

		#endregion

		#region Nested Type: StackItem

		/// <summary>
		/// Encapsulates the functionality for handling stacked readers
		/// including base information and context.
		/// </summary>
		private class StackItem
		{
			#region Properties

			public XmlReader Reader { get; set; }

			#endregion
		}

		#endregion
	}
}
