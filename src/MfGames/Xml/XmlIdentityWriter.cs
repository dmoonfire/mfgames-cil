// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System.Xml;

namespace MfGames.Xml
{
	/// <summary>
	/// An XML writer that takes an XmlReader object and writes out the
	/// contents to the writer.
	/// </summary>
	public class XmlIdentityWriter: XmlProxyWriter
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlIdentityWriter"/> class.
		/// </summary>
		/// <param name="writer">The XML writer.</param>
		public XmlIdentityWriter(XmlWriter writer)
			: base(writer)
		{
		}

		#endregion

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

		#region Overridable Writing Methods

		/// <summary>
		/// Writes the end element.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteEndElement(XmlReader reader)
		{
			Writer.WriteFullEndElement();
		}

		/// <summary>
		/// Writes the comment.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteComment(XmlReader reader)
		{
			Writer.WriteComment(reader.Value);
		}

		/// <summary>
		/// Writes the type of the document.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteDocumentType(XmlReader reader)
		{
			Writer.WriteDocType(
				reader.Name,
				reader.GetAttribute("PUBLIC"),
				reader.GetAttribute("SYSTEM"),
				reader.Value);
		}

		/// <summary>
		/// Writes the processing instruction.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteProcessingInstruction(XmlReader reader)
		{
			Writer.WriteProcessingInstruction(
				reader.Name,
				reader.Value);
		}

		/// <summary>
		/// Writes the entity ref.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteEntityRef(XmlReader reader)
		{
			Writer.WriteEntityRef(reader.Name);
		}

		/// <summary>
		/// Writes the C data.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteCData(XmlReader reader)
		{
			Writer.WriteCData(reader.Value);
		}

		/// <summary>
		/// Writes the whitespace.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteWhitespace(XmlReader reader)
		{
			Writer.WriteWhitespace(reader.Value);
		}

		/// <summary>
		/// Writes the text.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteText(XmlReader reader)
		{
			Writer.WriteString(reader.Value);
		}

		/// <summary>
		/// Writes the element.
		/// </summary>
		/// <param name="reader">The reader.</param>
		protected virtual void WriteElement(XmlReader reader)
		{
			// Write out the start element using the input prefix and namespace.
			Writer.WriteStartElement(
				reader.Prefix,
				reader.LocalName,
				reader.NamespaceURI);

			// Write out all the attributes.
			Writer.WriteAttributes(
				reader,
				true);

			// If we have an empty element, we won't see an EndElement for this
			// node. So, we need to write it out. We use WriteEndElement()
			// which will create an empty node (<a/> instead of <a></a>).
			if (reader.IsEmptyElement)
			{
				Writer.WriteEndElement();
			}
		}

		#endregion
	}
}
