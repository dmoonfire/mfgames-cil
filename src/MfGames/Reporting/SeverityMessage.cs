#region Copyright and License

// Copyright (c) 2005-2011, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System;

#endregion

namespace MfGames.Reporting
{
	/// <summary>
	/// Represents a message with an associated severity. This is similar
	/// to <see cref="Exception"/>, but can represents messages of other
	/// levels.
	/// </summary>
	public class SeverityMessage
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SeverityMessage"/>
		/// class with an info severity.
		/// </summary>
		/// <param name="text">The message text.</param>
		public SeverityMessage(string text)
			: this(Severity.Info, text)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SeverityMessage"/> class.
		/// </summary>
		/// <param name="severity">The severity.</param>
		/// <param name="text">The message text.</param>
		public SeverityMessage(
			Severity severity,
			string text)
		{
			if (String.IsNullOrEmpty(text))
			{
				throw new ArgumentNullException("message");
			}

			Severity = severity;
			Text = text;
			Created = DateTime.UtcNow;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets when the message was created.
		/// </summary>
		public DateTime Created { get; protected set; }

		/// <summary>
		/// Contains the severity of the message.
		/// </summary>
		public Severity Severity { get; protected set; }

		/// <summary>
		/// Contains the message text.
		/// </summary>
		public string Text { get; protected set; }

		#endregion
	}
}