// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System.Xml;

namespace MfGames.Xml
{
	/// <summary>
	/// Implements an XML writer that wraps around another XML writer while
	/// allowing for overrides for the individual functions.
	/// </summary>
	public class XmlProxyWriter: XmlWriter
	{
		#region Properties

		/// <summary>
		/// When overridden in a derived class, gets the state of the writer.
		/// </summary>
		/// <returns>
		/// One of the <see cref="T:System.Xml.WriteState"/> values.
		///   </returns>
		public override WriteState WriteState
		{
			get { return UnderlyingWriter.WriteState; }
		}

		/// <summary>
		/// Gets the underlying XML writer.
		/// </summary>
		protected XmlWriter UnderlyingWriter { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// When overridden in a derived class, closes this stream and the underlying stream.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// A call is made to write more output after Close has been called or the result of this call is an invalid XML document.
		///   </exception>
		public override void Close()
		{
			UnderlyingWriter.Close();
		}

		/// <summary>
		/// When overridden in a derived class, flushes whatever is in the buffer to the underlying streams and also flushes the underlying stream.
		/// </summary>
		public override void Flush()
		{
			UnderlyingWriter.Flush();
		}

		/// <summary>
		/// When overridden in a derived class, returns the closest prefix defined in the current namespace scope for the namespace URI.
		/// </summary>
		/// <param name="ns">The namespace URI whose prefix you want to find.</param>
		/// <returns>
		/// The matching prefix or null if no matching namespace URI is found in the current scope.
		/// </returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ns"/> is either null or String.Empty.
		///   </exception>
		public override string LookupPrefix(string ns)
		{
			return UnderlyingWriter.LookupPrefix(ns);
		}

		/// <summary>
		/// When overridden in a derived class, encodes the specified binary bytes as Base64 and writes out the resulting text.
		/// </summary>
		/// <param name="buffer">Byte array to encode.</param>
		/// <param name="index">The position in the buffer indicating the start of the bytes to write.</param>
		/// <param name="count">The number of bytes to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer"/> is null.
		///   </exception>
		///   
		/// <exception cref="T:System.ArgumentException">
		/// The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>.
		///   </exception>
		///   
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> or <paramref name="count"/> is less than zero.
		///   </exception>
		public override void WriteBase64(
			byte[] buffer,
			int index,
			int count)
		{
			UnderlyingWriter.WriteBase64(buffer, index, count);
		}

		/// <summary>
		/// When overridden in a derived class, writes out a &lt;![CDATA[...]]&gt; block containing the specified text.
		/// </summary>
		/// <param name="text">The text to place inside the CDATA block.</param>
		/// <exception cref="T:System.ArgumentException">
		/// The text would result in a non-well formed XML document.
		///   </exception>
		public override void WriteCData(string text)
		{
			UnderlyingWriter.WriteCData(text);
		}

		/// <summary>
		/// When overridden in a derived class, forces the generation of a character entity for the specified Unicode character value.
		/// </summary>
		/// <param name="ch">The Unicode character for which to generate a character entity.</param>
		/// <exception cref="T:System.ArgumentException">
		/// The character is in the surrogate pair character range, 0xd800 - 0xdfff.
		///   </exception>
		public override void WriteCharEntity(char ch)
		{
			UnderlyingWriter.WriteCharEntity(ch);
		}

		/// <summary>
		/// When overridden in a derived class, writes text one buffer at a time.
		/// </summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position in the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer"/> is null.
		///   </exception>
		///   
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> or <paramref name="count"/> is less than zero.
		/// -or-
		/// The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>; the call results in surrogate pair characters being split or an invalid surrogate pair being written.
		///   </exception>
		///   
		/// <exception cref="T:System.ArgumentException">
		/// The <paramref name="buffer"/> parameter value is not valid.
		///   </exception>
		public override void WriteChars(
			char[] buffer,
			int index,
			int count)
		{
			UnderlyingWriter.WriteChars(buffer, index, count);
		}

		/// <summary>
		/// When overridden in a derived class, writes out a comment &lt;!--...--&gt; containing the specified text.
		/// </summary>
		/// <param name="text">Text to place inside the comment.</param>
		/// <exception cref="T:System.ArgumentException">
		/// The text would result in a non-well formed XML document.
		///   </exception>
		public override void WriteComment(string text)
		{
			UnderlyingWriter.WriteComment(text);
		}

		/// <summary>
		/// When overridden in a derived class, writes the DOCTYPE declaration with the specified name and optional attributes.
		/// </summary>
		/// <param name="name">The name of the DOCTYPE. This must be non-empty.</param>
		/// <param name="pubid">If non-null it also writes PUBLIC "pubid" "sysid" where <paramref name="pubid"/> and <paramref name="sysid"/> are replaced with the value of the given arguments.</param>
		/// <param name="sysid">If <paramref name="pubid"/> is null and <paramref name="sysid"/> is non-null it writes SYSTEM "sysid" where <paramref name="sysid"/> is replaced with the value of this argument.</param>
		/// <param name="subset">If non-null it writes [subset] where subset is replaced with the value of this argument.</param>
		/// <exception cref="T:System.InvalidOperationException">
		/// This method was called outside the prolog (after the root element).
		///   </exception>
		///   
		/// <exception cref="T:System.ArgumentException">
		/// The value for <paramref name="name"/> would result in invalid XML.
		///   </exception>
		public override void WriteDocType(
			string name,
			string pubid,
			string sysid,
			string subset)
		{
			UnderlyingWriter.WriteDocType(name, pubid, sysid, subset);
		}

		/// <summary>
		/// When overridden in a derived class, closes the previous <see cref="M:System.Xml.XmlWriter.WriteStartAttribute(System.String,System.String)"/> call.
		/// </summary>
		public override void WriteEndAttribute()
		{
			UnderlyingWriter.WriteEndAttribute();
		}

		/// <summary>
		/// When overridden in a derived class, closes any open elements or attributes and puts the writer back in the Start state.
		/// </summary>
		/// <exception cref="T:System.ArgumentException">
		/// The XML document is invalid.
		///   </exception>
		public override void WriteEndDocument()
		{
			UnderlyingWriter.WriteEndDocument();
		}

		/// <summary>
		/// When overridden in a derived class, closes one element and pops the corresponding namespace scope.
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// This results in an invalid XML document.
		///   </exception>
		public override void WriteEndElement()
		{
			UnderlyingWriter.WriteEndElement();
		}

		/// <summary>
		/// When overridden in a derived class, writes out an entity reference as &amp;name;.
		/// </summary>
		/// <param name="name">The name of the entity reference.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name"/> is either null or String.Empty.
		///   </exception>
		public override void WriteEntityRef(string name)
		{
			UnderlyingWriter.WriteEntityRef(name);
		}

		/// <summary>
		/// When overridden in a derived class, closes one element and pops the corresponding namespace scope.
		/// </summary>
		public override void WriteFullEndElement()
		{
			UnderlyingWriter.WriteFullEndElement();
		}

		/// <summary>
		/// When overridden in a derived class, writes out a processing instruction with a space between the name and text as follows: &lt;?name text?&gt;.
		/// </summary>
		/// <param name="name">The name of the processing instruction.</param>
		/// <param name="text">The text to include in the processing instruction.</param>
		/// <exception cref="T:System.ArgumentException">
		/// The text would result in a non-well formed XML document.
		///   <paramref name="name"/> is either null or String.Empty.
		/// This method is being used to create an XML declaration after <see cref="M:System.Xml.XmlWriter.WriteStartDocument"/> has already been called.
		///   </exception>
		public override void WriteProcessingInstruction(
			string name,
			string text)
		{
			UnderlyingWriter.WriteProcessingInstruction(name, text);
		}

		/// <summary>
		/// When overridden in a derived class, writes raw markup manually from a character buffer.
		/// </summary>
		/// <param name="buffer">Character array containing the text to write.</param>
		/// <param name="index">The position within the buffer indicating the start of the text to write.</param>
		/// <param name="count">The number of characters to write.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer"/> is null.
		///   </exception>
		///   
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index"/> or <paramref name="count"/> is less than zero.
		/// -or-
		/// The buffer length minus <paramref name="index"/> is less than <paramref name="count"/>.
		///   </exception>
		public override void WriteRaw(
			char[] buffer,
			int index,
			int count)
		{
			UnderlyingWriter.WriteRaw(buffer, index, count);
		}

		/// <summary>
		/// When overridden in a derived class, writes raw markup manually from a string.
		/// </summary>
		/// <param name="data">String containing the text to write.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="data"/> is either null or String.Empty.
		///   </exception>
		public override void WriteRaw(string data)
		{
			UnderlyingWriter.WriteRaw(data);
		}

		/// <summary>
		/// When overridden in a derived class, writes the start of an attribute with the specified prefix, local name, and namespace URI.
		/// </summary>
		/// <param name="prefix">The namespace prefix of the attribute.</param>
		/// <param name="localName">The local name of the attribute.</param>
		/// <param name="ns">The namespace URI for the attribute.</param>
		public override void WriteStartAttribute(
			string prefix,
			string localName,
			string ns)
		{
			UnderlyingWriter.WriteStartAttribute(prefix, localName, ns);
		}

		/// <summary>
		/// When overridden in a derived class, writes the XML declaration with the version "1.0".
		/// </summary>
		/// <exception cref="T:System.InvalidOperationException">
		/// This is not the first write method called after the constructor.
		///   </exception>
		public override void WriteStartDocument()
		{
			UnderlyingWriter.WriteStartDocument();
		}

		/// <summary>
		/// When overridden in a derived class, writes the XML declaration with the version "1.0" and the standalone attribute.
		/// </summary>
		/// <param name="standalone">If true, it writes "standalone=yes"; if false, it writes "standalone=no".</param>
		/// <exception cref="T:System.InvalidOperationException">
		/// This is not the first write method called after the constructor.
		///   </exception>
		public override void WriteStartDocument(bool standalone)
		{
			UnderlyingWriter.WriteStartDocument(standalone);
		}

		/// <summary>
		/// When overridden in a derived class, writes the specified start tag and associates it with the given namespace and prefix.
		/// </summary>
		/// <param name="prefix">The namespace prefix of the element.</param>
		/// <param name="localName">The local name of the element.</param>
		/// <param name="ns">The namespace URI to associate with the element.</param>
		/// <exception cref="T:System.InvalidOperationException">
		/// The writer is closed.
		///   </exception>
		public override void WriteStartElement(
			string prefix,
			string localName,
			string ns)
		{
			UnderlyingWriter.WriteStartElement(prefix, localName, ns);
		}

		/// <summary>
		/// When overridden in a derived class, writes the given text content.
		/// </summary>
		/// <param name="text">The text to write.</param>
		/// <exception cref="T:System.ArgumentException">
		/// The text string contains an invalid surrogate pair.
		///   </exception>
		public override void WriteString(string text)
		{
			UnderlyingWriter.WriteString(text);
		}

		/// <summary>
		/// When overridden in a derived class, generates and writes the surrogate character entity for the surrogate character pair.
		/// </summary>
		/// <param name="lowChar">The low surrogate. This must be a value between 0xDC00 and 0xDFFF.</param>
		/// <param name="highChar">The high surrogate. This must be a value between 0xD800 and 0xDBFF.</param>
		/// <exception cref="T:System.ArgumentException">
		/// An invalid surrogate character pair was passed.
		///   </exception>
		public override void WriteSurrogateCharEntity(
			char lowChar,
			char highChar)
		{
			UnderlyingWriter.WriteSurrogateCharEntity(lowChar, highChar);
		}

		/// <summary>
		/// When overridden in a derived class, writes out the given white space.
		/// </summary>
		/// <param name="ws">The string of white space characters.</param>
		/// <exception cref="T:System.ArgumentException">
		/// The string contains non-white space characters.
		///   </exception>
		public override void WriteWhitespace(string ws)
		{
			UnderlyingWriter.WriteWhitespace(ws);
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlProxyWriter"/> class.
		/// </summary>
		/// <param name="writer">The writer.</param>
		public XmlProxyWriter(XmlWriter writer)
		{
			UnderlyingWriter = writer;
		}

		#endregion
	}
}
