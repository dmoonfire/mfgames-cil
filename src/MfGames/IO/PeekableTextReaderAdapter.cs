// <copyright file="PeekableTextReaderAdapter.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.IO;

namespace MfGames.IO
{
	/// <summary>
	/// An adapter that allows for peeking a number of lines ahead of the underlying
	/// text reader.
	/// </summary>
	public class PeekableTextReaderAdapter : IDisposable
	{
		#region Fields

		/// <summary>
		/// Contain the underlying reader for this adapter.
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
			PeekableLineCount = peekableLineCount;
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
		public TextReader UnderlyingReader { get { return underlyingReader; } }

		#endregion

		#region Public Methods and Operators

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		public void Dispose()
		{
			underlyingReader.Dispose();
		}

		/// <summary>
		/// Returns the line in the buffer, with one line ahead being lineOffset 0.
		/// </summary>
		/// <param name="linesOffset">
		/// The zero-based offset of the peeked lines.
		/// </param>
		/// <returns>
		/// The resulting line.
		/// </returns>
		public string PeekLine(int linesOffset)
		{
			// If we don't have a buffer, then populate the entire buffer. This takes
			// advantage of ReadLine() returning null repeatedly even after the end
			// of buffer.
			if (peekBuffer == null)
			{
				peekBuffer = new string[PeekableLineCount];

				for (var i = 0; i < PeekableLineCount; i++)
				{
					peekBuffer[i] = UnderlyingReader.ReadLine();
				}
			}

			// If we haven't loaded the buffer, then do it.
			return peekBuffer[linesOffset];
		}

		/// <summary>
		/// Reads the line.
		/// </summary>
		/// <returns>
		/// The next line or null if the end of buffer has been reached.
		/// </returns>
		public string ReadLine()
		{
			// Load the buffer, if we need it.
			PeekLine(0);

			// Reading lines is fairly simple. We return the first index, then shift
			// everything down the buffer list.
			string results = peekBuffer[0];

			// Shift everything down.
			for (var i = 0; i < PeekableLineCount - 1; i++)
			{
				peekBuffer[i] = peekBuffer[i + 1];
			}

			// Populate the final one.
			peekBuffer[PeekableLineCount - 1] =
				UnderlyingReader.ReadLine();

			// Return the resulting line.
			return results;
		}

		#endregion
	}
}
