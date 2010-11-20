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

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// Contains the arguments for an audit message being sent.
	/// </summary>
	public class AuditMessageEventArgs : EventArgs
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditMessageEventArgs"/> class.
		/// </summary>
		/// <param name="auditMessage">The audit message.</param>
		public AuditMessageEventArgs(AuditMessage auditMessage)
		{
			AuditMessage = auditMessage;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AuditMessageEventArgs"/> class.
		/// </summary>
		/// <param name="auditMessage">The audit message.</param>
		/// <param name="oldSeverity">The old severity.</param>
		public AuditMessageEventArgs(AuditMessage auditMessage, Severity oldSeverity)
		{
			AuditMessage = auditMessage;
			OldSeverity = oldSeverity;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the audit message.
		/// </summary>
		/// <value>The audit message.</value>
		public AuditMessage AuditMessage { get; private set; }

		/// <summary>
		/// Gets or sets the old severity level before the message. If this is a new
		/// message, it will be None.
		/// </summary>
		/// <value>The old severity.</value>
		public Severity OldSeverity { get; private set; }

		#endregion
	}
}