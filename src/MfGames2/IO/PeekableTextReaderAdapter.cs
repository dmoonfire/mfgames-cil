// <copyright file="PeekableTextReaderAdapter.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.IO
{
    using System;
    using System.IO;
    using System.Runtime.Remoting;

    /// <summary>
    /// An adapter that allows for peeking a number of lines ahead of the underlying
    /// text reader.
    /// </summary>
    public class PeekableTextReaderAdapter
    {
        #region Fields

        /// <summary>
        /// </summary>
        private readonly TextReader underlyingReader;

        /// <summary>
        /// Contains the pre-fetched line from the buffer.
        /// </summary>
        private string[] peekBuffer;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PeekableTextReaderAdapter"/> class.
        /// </summary>
        /// <param name="underlyingReader">
        /// The underlying reader.
        /// </param>
        /// <param name="peekableLineCount">
        /// The number of lines to peek ahead.
        /// </param>
        public PeekableTextReaderAdapter(
            TextReader underlyingReader, 
            int peekableLineCount = 1)
        {
            // Establish our contracts.
            if (underlyingReader == null)
            {
                throw new ArgumentNullException("underlyingReader");
            }

            if (peekableLineCount <= 0)
            {
                throw new ArgumentOutOfRangeException(
                    "peekableLineCount", 
                    "peekableLineCount cannot be zero or less.");
            }

            // Save the reader so we can wrap it.
            this.underlyingReader = underlyingReader;
            this.PeekableLineCount = peekableLineCount;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the number of lines that the buffer will allow peaking ahead.
        /// </summary>
        public int PeekableLineCount { get; private set; }

        /// <summary>
        /// Gets the underlying text reader.
        /// </summary>
        public TextReader UnderlyingReader
        {
            get
            {
                return this.underlyingReader;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            this.underlyingReader.Close();
        }

        /// <summary>
        /// Creates the object reference.
        /// </summary>
        /// <param name="requestedType">
        /// Type of the requested.
        /// </param>
        /// <returns>
        /// </returns>
        public ObjRef CreateObjRef(Type requestedType)
        {
            return this.underlyingReader.CreateObjRef(requestedType);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        public void Dispose()
        {
            this.underlyingReader.Dispose();
        }

        /// <summary>
        /// Gets the lifetime service.
        /// </summary>
        /// <returns>
        /// </returns>
        public object GetLifetimeService()
        {
            return this.underlyingReader.GetLifetimeService();
        }

        /// <summary>
        /// Initializes the lifetime service.
        /// </summary>
        /// <returns>
        /// </returns>
        public object InitializeLifetimeService()
        {
            return this.underlyingReader.InitializeLifetimeService();
        }

        /// <summary>
        /// Peeks this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        public int Peek()
        {
            return this.underlyingReader.Peek();
        }

        /// <summary>
        /// Returns the line in the buffer, with one line ahead being lineOffset 0.
        /// </summary>
        /// <param name="linesOffset">
        /// </param>
        /// <returns>
        /// The resulting line.
        /// </returns>
        public string PeekLine(int linesOffset)
        {
            return this.peekBuffer[linesOffset];
        }

        /// <summary>
        /// Reads this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        public int Read()
        {
            return this.underlyingReader.Read();
        }

        /// <summary>
        /// Reads the specified buffer.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// </returns>
        public int Read(
            char[] buffer, 
            int index, 
            int count)
        {
            return this.underlyingReader.Read(
                buffer, 
                index, 
                count);
        }

        /// <summary>
        /// Reads the block.
        /// </summary>
        /// <param name="buffer">
        /// The buffer.
        /// </param>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="count">
        /// The count.
        /// </param>
        /// <returns>
        /// </returns>
        public int ReadBlock(
            char[] buffer, 
            int index, 
            int count)
        {
            return this.underlyingReader.ReadBlock(
                buffer, 
                index, 
                count);
        }

        /// <summary>
        /// Reads the line.
        /// </summary>
        /// <returns>
        /// </returns>
        public string ReadLine()
        {
            // If we don't have a buffer, then populate the entire buffer. This takes
            // advantage of ReadLine() returning null repeatedly even after the end
            // of buffer.
            if (this.peekBuffer == null)
            {
                this.peekBuffer = new string[this.PeekableLineCount];

                for (int i = 0; i < this.PeekableLineCount; i++)
                {
                    this.peekBuffer[i] = this.UnderlyingReader.ReadLine();
                }
            }

            // Reading lines is fairly simple. We return the first index, then shift
            // everything down the buffer list.
            string results = this.peekBuffer[0];

            // Shift everything down.
            for (int i = 0; i < this.PeekableLineCount - 1; i++)
            {
                this.peekBuffer[i] = this.peekBuffer[i + 1];
            }

            // Populate the final one.
            this.peekBuffer[this.PeekableLineCount - 1] =
                this.UnderlyingReader.ReadLine();

            // Return the resulting line.
            return results;
        }

        /// <summary>
        /// Reads to end.
        /// </summary>
        /// <returns>
        /// </returns>
        public string ReadToEnd()
        {
            return this.underlyingReader.ReadToEnd();
        }

        #endregion
    }
}