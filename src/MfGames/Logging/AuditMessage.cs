#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
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

using MfGames.Enumerations;

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// Represents a single auditing message with a given severity.
	/// 
	/// This class may be extended to allow for additional information to be passed
	/// through the auditing interface.
	/// </summary>
	public struct AuditMessage
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditMessage"/> struct.
		/// </summary>
		/// <param name="severity">The severity.</param>
		/// <param name="message">The message.</param>
		public AuditMessage(Severity severity, string message)
			: this()
		{
			// Check the parameters.
			if (String.IsNullOrEmpty(message))
			{
				throw new ArgumentException("Message cannot be blank or null", "message");
			}

			// Save the values in the structure.
			Severity = severity;
			Message = message;
			Key = message;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditMessage"/> struct.
		/// </summary>
		/// <param name="severity">The severity.</param>
		/// <param name="key">The key.</param>
		/// <param name="message">The message.</param>
		public AuditMessage(Severity severity, object key, string message)
			: this()
		{
			// Check the parameters.
			if (String.IsNullOrEmpty(message))
			{
				throw new ArgumentException("Message cannot be blank or null", "message");
			}

			// Save the values in the structure.
			Severity = severity;
			Message = message;
			Key = key;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the object used to define uniqueness of a message.
		/// </summary>
		/// <value>The uniqueness.</value>
		public object Key { get; private set; }

		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; private set; }

		/// <summary>
		/// Gets the severity.
		/// </summary>
		/// <value>The severity.</value>
		public Severity Severity { get; private set; }

		#endregion

		#region Comparison and Operators

		/// <summary>
		/// Indicates whether this instance and a specified object are equal.
		/// </summary>
		/// <param name="obj">Another object to compare to.</param>
		/// <returns>
		/// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false.
		/// </returns>
		public override bool Equals(object obj)
		{
			if (obj is AuditMessage)
			{
				var auditMessage = (AuditMessage) obj;
				return auditMessage.Key == Key;
			}

			return false;
		}

		/// <summary>
		/// Returns the hash code for this instance.
		/// </summary>
		/// <returns>
		/// A 32-bit signed integer that is the hash code for this instance.
		/// </returns>
		public override int GetHashCode()
		{
			return Key == null ? 0 : Key.GetHashCode();
		}

		#endregion
	}
}