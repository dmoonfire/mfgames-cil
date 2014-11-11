// <copyright file="XmlIdentityWriterTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace UnitTests
{
    using System.IO;
    using System.Xml;

    using MfGames.Xml;

    using NUnit.Framework;

    /// <summary>
    /// Implements tests that excercise the XmlIdentityWriter.
    /// </summary>
    [TestFixture]
    public class XmlIdentityWriterTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        [Test]
        public void FrameworkTest()
        {
        }

        /// <summary>
        /// </summary>
        [Test]
        public void InnerEmptyTag()
        {
            // Arrange
            const string xml = "<a><b /><b /></a>";

            // Act
            var settings = new XmlWriterSettings
                {
                    OmitXmlDeclaration = true, 
                };
            var stringWriter = new StringWriter();

            using (var stringReader = new StringReader(xml))
            {
                using (XmlReader xmlReader = XmlReader.Create(stringReader))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(
                        stringWriter, 
                        settings))
                    {
                        using (
                            var identityWriter = new XmlIdentityWriter(
                                xmlWriter))
                        {
                            identityWriter.Load(xmlReader);
                        }
                    }
                }
            }

            // Assert
            string actual = stringWriter.ToString();

            Assert.AreEqual(
                xml, 
                actual);
        }

        /// <summary>
        /// </summary>
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
            {
                using (XmlReader xmlReader = XmlReader.Create(stringReader))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(
                        stringWriter, 
                        settings))
                    {
                        using (
                            var identityWriter = new XmlIdentityWriter(
                                xmlWriter))
                        {
                            identityWriter.Load(xmlReader);
                        }
                    }
                }
            }

            // Assert
            string actual = stringWriter.ToString();

            Assert.AreEqual(
                xml, 
                actual);
        }

        /// <summary>
        /// </summary>
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
            {
                using (XmlReader xmlReader = XmlReader.Create(stringReader))
                {
                    using (XmlWriter xmlWriter = XmlWriter.Create(
                        stringWriter, 
                        settings))
                    {
                        using (
                            var identityWriter = new XmlIdentityWriter(
                                xmlWriter))
                        {
                            identityWriter.Load(xmlReader);
                        }
                    }
                }
            }

            // Assert
            string actual = stringWriter.ToString();

            Assert.AreEqual(
                xml, 
                actual);
        }

        #endregion
    }
}