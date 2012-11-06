// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System.IO;
using System.Xml;
using MfGames.Xml;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class XIncludeReaderTests
	{
		/// <summary>
		/// Writes the XML results.
		/// </summary>
		/// <param name="xml">The input XML.</param>
		/// <returns>The resulting XML from an identity write.</returns>
		private static string WriteXmlResults(string xml)
		{
			// Set up the XML writing to produce consistent results.
			var writerSettings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
			};
			var stringWriter = new StringWriter();

			// Set up the reader by chaining into the include reader.
			using (var stringReader = new StringReader(xml))
			using (XmlReader xmlReader = XmlReader.Create(stringReader))
			using (var includeReader = new TestXIncludeReader(xmlReader))
				// Set up the include reader's loading.

				// Set up the identity writer so we can verify the results
				// using string.
			using (XmlWriter xmlWriter = XmlWriter.Create(
				stringWriter,
				writerSettings))
			using (var identityWriter = new XmlIdentityWriter(xmlWriter))
				identityWriter.Load(includeReader);

			// Return the resulting string.
			string results = stringWriter.ToString();

			return results;
		}

		/// <summary>
		/// A private class that creates an appropriate XML reader on demand.
		/// </summary>
		private class TestXIncludeReader: XIncludeReader
		{
			public TestXIncludeReader(XmlReader underlyingReader)
				: base(underlyingReader)
			{
			}

			/// <summary>
			/// Gets the included XML reader based on the current node.
			/// </summary>
			/// <returns></returns>
			protected override XmlReader GetIncludedXmlReader()
			{
				string xml;

				switch (GetAttribute("href"))
				{
					case "b.xml":
						xml = "<b />";
						break;
					case "c.xml":
						xml = "<c xmlns:xi='http://www.w3.org/2003/XInclude'><xi:include href='b.xml'/></c>";
						break;
					default:
						return null;
				}

				// Create an XML reader from the string.
				var stringReader = new StringReader(xml);
				XmlReader xmlReader = Create(stringReader);

				return xmlReader;
			}
		}

		/// <summary>
		/// Tests the framework.
		/// </summary>
		[Test]
		public void TestFramework()
		{
		}

		[Test]
		public void TestIncludeSimple()
		{
			// Arrange
			const string xml =
				"<a xmlns:xi='http://www.w3.org/2003/XInclude'><xi:include href='b.xml'/></a>";
			const string expected =
				"<a xmlns:xi=\"http://www.w3.org/2003/XInclude\"><b /></a>";

			// Act
			string results = WriteXmlResults(xml);

			// Assert
			Assert.AreEqual(
				expected,
				results);
		}

		[Test]
		public void TestIncludeRecursive()
		{
			// Arrange
			const string xml =
				"<a xmlns:xi='http://www.w3.org/2003/XInclude'><xi:include href='c.xml'/></a>";
			const string expected =
				"<a xmlns:xi=\"http://www.w3.org/2003/XInclude\"><c xmlns:xi=\"http://www.w3.org/2003/XInclude\"><b /></c></a>";

			// Act
			string results = WriteXmlResults(xml);

			// Assert
			Assert.AreEqual(
				expected,
				results);
		}

		[Test]
		public void TestNonIncluding()
		{
			// Arrange
			const string xml = "<a>a</a>";
			const string expected = "<a>a</a>";

			// Act
			string results = WriteXmlResults(xml);

			// Assert
			Assert.AreEqual(
				expected,
				results);
		}
	}
}
