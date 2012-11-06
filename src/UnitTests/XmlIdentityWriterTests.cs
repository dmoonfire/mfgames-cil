// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System.IO;
using System.Xml;
using MfGames.Xml;
using NUnit.Framework;

namespace UnitTests
{
	/// <summary>
	/// Implements tests that excercise the XmlIdentityWriter.
	/// </summary>
	[TestFixture]
	public class XmlIdentityWriterTests
	{
		[Test]
		public void FrameworkTest()
		{
		}

		[Test]
		public void SingleFullTag()
		{
			// Arrange
			const string xml = "<a>a</a>";

			// Act
			var settings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
			};
			var stringWriter = new StringWriter();

			using (var stringReader = new StringReader(xml))
			using (XmlReader xmlReader = XmlReader.Create(stringReader))
			using (XmlWriter xmlWriter = XmlWriter.Create(
				stringWriter,
				settings))
			using (var identityWriter = new XmlIdentityWriter(xmlWriter))
				identityWriter.Load(xmlReader);

			// Assert
			string actual = stringWriter.ToString();

			Assert.AreEqual(
				xml,
				actual);
		}

		[Test]
		public void SingleTag()
		{
			// Arrange
			const string xml = "<a />";

			// Act
			var settings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
			};

			var stringWriter = new StringWriter();

			using (var stringReader = new StringReader(xml))
			using (XmlReader xmlReader = XmlReader.Create(stringReader))
			using (XmlWriter xmlWriter = XmlWriter.Create(
				stringWriter,
				settings))
			using (var identityWriter = new XmlIdentityWriter(xmlWriter))
				identityWriter.Load(xmlReader);

			// Assert
			string actual = stringWriter.ToString();

			Assert.AreEqual(
				xml,
				actual);
		}
	}
}
