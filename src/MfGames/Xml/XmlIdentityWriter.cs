// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System.Xml;

namespace MfGames.Xml
{
	/// <summary>
	/// An XML writer that takes an XmlReader object and writes out the
	/// contents to the 
	/// </summary>
	public class XmlIdentityWriter: XmlProxyWriter
	{
		#region Methods

		/// <summary>
		/// Parses through the specified XML reader and writes out an
		/// identity XML representing the contents.
		/// </summary>
		/// <param name="reader">The reader.</param>
		public void Load(XmlReader reader)
		{
			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						WriteElement(reader);
						break;

					case XmlNodeType.Text:
						WriteText(reader);
						break;

					case XmlNodeType.Whitespace:
					case XmlNodeType.SignificantWhitespace:
						WriteWhitespace(reader);
						break;

					case XmlNodeType.CDATA:
						WriteCData(reader);
						break;

					case XmlNodeType.EntityReference:
						WriteEntityRef(reader);
						break;

					case XmlNodeType.XmlDeclaration:
					case XmlNodeType.ProcessingInstruction:
						WriteProcessingInstruction(reader);
						break;

					case XmlNodeType.DocumentType:
						WriteDocumentType(reader);
						break;

					case XmlNodeType.Comment:
						WriteComment(reader);
						break;

					case XmlNodeType.EndElement:
						WriteEndElement();
						break;
				}
			}
		}

		/// <summary>
		/// Writes the C data.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteCData(XmlReader reader)
		{
			WriteCData(reader.Value);
		}

		/// <summary>
		/// Writes the comment.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteComment(XmlReader reader)
		{
			WriteComment(reader.Value);
		}

		/// <summary>
		/// Writes the type of the document.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteDocumentType(XmlReader reader)
		{
			WriteDocType(
				reader.Name,
				reader.GetAttribute("PUBLIC"),
				reader.GetAttribute("SYSTEM"),
				reader.Value);
		}

		/// <summary>
		/// Writes the element.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteElement(XmlReader reader)
		{
			// Write out the start element using the input prefix and namespace.
			WriteStartElement(
				reader.Prefix,
				reader.LocalName,
				reader.NamespaceURI);

			// Write out all the attributes.
			if (reader.HasAttributes)
			{
				reader.MoveToFirstAttribute();

				do
				{
					WriteAttributeString(
						reader.Prefix,
						reader.LocalName,
						reader.NamespaceURI,
						reader.Value);
				}
				while (reader.MoveToNextAttribute());

				// We have to move back to the element so the rest of the
				// processing works properly.
				reader.MoveToElement();
			}

			// If we have an empty element, we won't see an EndElement for this
			// node. So, we need to write it out. We use WriteEndElement()
			// which will create an empty node (<a/> instead of <a></a>).
			if (reader.IsEmptyElement)
			{
				WriteEndElement();
			}
		}

		/// <summary>
		/// Writes the end element.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteEndElement(XmlReader reader)
		{
			WriteFullEndElement();
		}

		/// <summary>
		/// Writes the entity ref.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteEntityRef(XmlReader reader)
		{
			WriteEntityRef(reader.Name);
		}

		/// <summary>
		/// Writes the processing instruction.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteProcessingInstruction(XmlReader reader)
		{
			WriteProcessingInstruction(
				reader.Name,
				reader.Value);
		}

		/// <summary>
		/// Writes the text.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteText(XmlReader reader)
		{
			WriteString(reader.Value);
		}

		/// <summary>
		/// Writes the whitespace.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteWhitespace(XmlReader reader)
		{
			WriteWhitespace(reader.Value);
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlIdentityWriter"/> class.
		/// </summary>
		/// <param name="writer">The XML </param>
		public XmlIdentityWriter(XmlWriter writer)
			: base(writer)
		{
		}

		#endregion
	}
}
