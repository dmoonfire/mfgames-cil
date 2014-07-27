// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using MfGames.Xml;
using NUnit.Framework;

namespace UnitTests
{
	[TestFixture]
	public class XIncludeReaderTests
	{
		#region Methods

		[Test]
		public void InnerEmptyTags()
		{
			// Arrange
			const string xml = "<a><b c=\"t\" /><c c=\"a\"/></a>";
			const string expected = "<a><b c=\"t\" /><c c=\"a\" /></a>";

			// Act
			string results = WriteXmlResults(xml);

			// Assert
			Assert.AreEqual(expected, results);
		}

		[Test]
        [Ignore("Disabling temporarily for whitespace issues.")]
		public void TestFileIncludeRecursive()
		{
			// Arrange
			const string xml =
				"<a xmlns:xi='http://www.w3.org/2003/XInclude'><xi:include href='XInclude/cfile.xml'></xi:include></a>";
			const string expected =
				"<a xmlns:xi=\"http://www.w3.org/2003/XInclude\">\r\n<c xmlns:xinclude=\"http://www.w3.org/2001/XInclude\">\r\n\t\r\n<b />\r\n</c></a>";

			// Act
			string results = WriteXmlResults(xml);

			// Assert
			Assert.AreEqual(expected, results);
		}

		[Test]
		public void TestFileIncludeRecursiveWithXPointer()
		{
			// Arrange
			const string xml =
				"<a xmlns:xi='http://www.w3.org/2003/XInclude'><xi:include href='XInclude/cfile.xml' xpointer='xpointer(/*/*)'></xi:include></a>";
			const string expected =
				"<a xmlns:xi=\"http://www.w3.org/2003/XInclude\"><b /></a>";

			// Act
			string results = WriteXmlResults(xml);

			// Assert
			Assert.AreEqual(expected, results);
		}

		[Test]
        [Ignore("Disabling temporarily for whitespace issues.")]
        public void TestFileIncludeSimpleWithClosingTag()
		{
			// Arrange
			const string xml =
				"<a xmlns:xi='http://www.w3.org/2003/XInclude'><xi:include href='XInclude/bfile.xml'></xi:include></a>";
			const string expected =
				"<a xmlns:xi=\"http://www.w3.org/2003/XInclude\">\r\n<b /></a>";

			// Act
			string results = WriteXmlResults(xml);

			// Assert
			Assert.AreEqual(expected, results);
		}

		/// <summary>
		/// Tests the framework.
		/// </summary>
		[Test]
		public void TestFramework()
		{
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
			Assert.AreEqual(expected, results);
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
			Assert.AreEqual(expected, results);
		}

		[Test]
		public void TestIncludeSimpleWithClosingTag()
		{
			// Arrange
			const string xml =
				"<a xmlns:xi='http://www.w3.org/2003/XInclude'><xi:include href='b.xml'></xi:include></a>";
			const string expected =
				"<a xmlns:xi=\"http://www.w3.org/2003/XInclude\"><b /></a>";

			// Act
			string results = WriteXmlResults(xml);

			// Assert
			Assert.AreEqual(expected, results);
		}

		[Test]
		public void TestIncludeSimpleWithTrailing()
		{
			// Arrange
			const string xml =
				"<a xmlns:xi='http://www.w3.org/2003/XInclude'><xi:include href='b.xml'/><c /></a>";
			const string expected =
				"<a xmlns:xi=\"http://www.w3.org/2003/XInclude\"><b /><c /></a>";

			// Act
			string results = WriteXmlResults(xml);

			// Assert
			Assert.AreEqual(expected, results);
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
			Assert.AreEqual(expected, results);
		}

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
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader))
				{
					using (var includeReader = new TestXIncludeReader(xmlReader))
					{
						// Set up the identity writer so we can verify the results
						// using string.
						using (
							XmlWriter xmlWriter = XmlWriter.Create(stringWriter, writerSettings))
						{
							using (var identityWriter = new XmlIdentityWriter(xmlWriter))
							{
								identityWriter.Load(includeReader);
							}
						}
					}
				}
			}

			// Pull out the resulting string.
			string results = stringWriter.ToString();

			// Report the input and output.
			Console.WriteLine(" Input XML: " + xml);
			Console.WriteLine("Output XML: " + results);

			// Return the results.
			return results;
		}

		#endregion

		#region Nested Type: TestXIncludeReader

		/// <summary>
		/// A private class that creates an appropriate XML reader on demand.
		/// </summary>
		private class TestXIncludeReader: XIncludeReader
		{
			#region Methods

			/// <summary>
			/// Gets the included XML reader based on the current node.
			/// </summary>
			/// <returns></returns>
			protected override IEnumerable<XmlReader> CreateIncludedReaders()
			{
				string xml;
				string href = GetAttribute("href");

				switch (href)
				{
					case "b.xml":
						xml = "<b />";
						break;
					case "c.xml":
						xml = "<c xmlns:xi='http://www.w3.org/2003/XInclude'>"
							+ "<xi:include href='b.xml'/></c>";
						break;
					default:
						// Use the base implementation for everything else.
						return base.CreateIncludedReaders();
				}

				// Create an XML reader from the string.
				var stringReader = new StringReader(xml);
				XmlReader xmlReader = Create(stringReader);

				return new[]
				{
					xmlReader
				};
			}

			#endregion

			#region Constructors

			public TestXIncludeReader(XmlReader underlyingReader)
				: base(underlyingReader)
			{
			}

			#endregion
		}

		#endregion
	}
}
