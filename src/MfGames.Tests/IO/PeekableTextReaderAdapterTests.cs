// <copyright file="PeekableTextReaderAdapterTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace UnitTests.IO
{
    using System;
    using System.IO;

    using MfGames.IO;

    using NUnit.Framework;

    /// <summary>
    /// Tests the various functionality of if the PeekableTextReaderAdapter.
    /// </summary>
    [TestFixture]
    public class PeekableTextReaderAdapterTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// Verifies what happens when a negative size is passed into the adapter.
        /// </summary>
        [Test]
        public void CreateWithNegativeSize()
        {
            StringReader reader = this.CreateReader(string.Empty);
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new PeekableTextReaderAdapter(
                    reader, 
                    -1));
        }

        /// <summary>
        /// Verifies what happens when a null reader is passed into the adapter.
        /// </summary>
        [Test]
        public void CreateWithNullReader()
        {
            Assert.Throws<ArgumentNullException>(
                () => new PeekableTextReaderAdapter(null));
        }

        /// <summary>
        /// Verifies what happens when a positive size is passed into the adapter.
        /// </summary>
        [Test]
        public void CreateWithPostiveSize()
        {
            StringReader reader = this.CreateReader(string.Empty);
            var adapter = new PeekableTextReaderAdapter(
                reader, 
                13);

            Assert.IsNotNull(adapter);
        }

        /// <summary>
        /// Verifies what happens when a zero size is passed into the adapter.
        /// </summary>
        [Test]
        public void CreateWithZeroSize()
        {
            StringReader reader = this.CreateReader(string.Empty);
            Assert.Throws<ArgumentOutOfRangeException>(
                () => new PeekableTextReaderAdapter(
                    reader, 
                    0));
        }

        /// <summary>
        /// Verifies functionality of a single read on five lines.
        /// </summary>
        [Test]
        public void ReadFiveLine1()
        {
            StringReader reader = this.CreateReader(
                "One", 
                "Two", 
                "Three", 
                "Four", 
                "Five");
            var adapter = new PeekableTextReaderAdapter(
                reader, 
                3);

            Assert.AreEqual(
                "One", 
                adapter.ReadLine(), 
                "Read line is not expected.");
            Assert.AreEqual(
                "Two", 
                adapter.PeekLine(0), 
                "Peek(0) is not expected.");
            Assert.AreEqual(
                "Three", 
                adapter.PeekLine(1), 
                "Peek(1) is not expected.");
            Assert.AreEqual(
                "Four", 
                adapter.PeekLine(2), 
                "Peek(2) is not expected.");
        }

        /// <summary>
        /// Verifies functionality of a single read on five lines.
        /// </summary>
        [Test]
        public void ReadFiveLine3()
        {
            StringReader reader = this.CreateReader(
                "One", 
                "Two", 
                "Three", 
                "Four", 
                "Five");
            var adapter = new PeekableTextReaderAdapter(
                reader, 
                3);

            Assert.AreEqual(
                "One", 
                adapter.ReadLine(), 
                "Read line is not expected.");
            Assert.AreEqual(
                "Two", 
                adapter.ReadLine(), 
                "Read line #2 is not expected.");
            Assert.AreEqual(
                "Three", 
                adapter.ReadLine(), 
                "Read line #3 is not expected.");
            Assert.AreEqual(
                "Four", 
                adapter.PeekLine(0), 
                "Peek(0) is not expected.");
            Assert.AreEqual(
                "Five", 
                adapter.PeekLine(1), 
                "Peek(1) is not expected.");
            Assert.AreEqual(
                null, 
                adapter.PeekLine(2), 
                "Peek(2) is not expected.");
        }

        /// <summary>
        /// Verifies functionality of a single read on a single line.
        /// </summary>
        [Test]
        public void ReadSingleLine1()
        {
            StringReader reader = this.CreateReader("One");
            var adapter = new PeekableTextReaderAdapter(
                reader, 
                3);

            Assert.AreEqual(
                "One", 
                adapter.ReadLine(), 
                "Read line is not expected.");
            Assert.AreEqual(
                null, 
                adapter.PeekLine(0), 
                "Peek(0) is not expected.");
            Assert.AreEqual(
                null, 
                adapter.PeekLine(1), 
                "Peek(1) is not expected.");
            Assert.AreEqual(
                null, 
                adapter.PeekLine(2), 
                "Peek(2) is not expected.");
        }

        /// <summary>
        /// Verifies functionality of two reads on a single line.
        /// </summary>
        [Test]
        public void ReadSingleLine2()
        {
            StringReader reader = this.CreateReader("One");
            var adapter = new PeekableTextReaderAdapter(
                reader, 
                3);

            Assert.AreEqual(
                "One", 
                adapter.ReadLine(), 
                "Read line is not expected.");
            Assert.AreEqual(
                null, 
                adapter.ReadLine(), 
                "Read 2 line is not expected.");
            Assert.AreEqual(
                null, 
                adapter.PeekLine(0), 
                "Peek(0) is not expected.");
            Assert.AreEqual(
                null, 
                adapter.PeekLine(1), 
                "Peek(1) is not expected.");
            Assert.AreEqual(
                null, 
                adapter.PeekLine(2), 
                "Peek(2) is not expected.");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates a reader that reads through a given number of lines.
        /// </summary>
        /// <param name="lines">
        /// </param>
        /// <returns>
        /// </returns>
        private StringReader CreateReader(params string[] lines)
        {
            string buffer = string.Join(
                Environment.NewLine, 
                lines);
            var reader = new StringReader(buffer);
            return reader;
        }

        #endregion
    }
}