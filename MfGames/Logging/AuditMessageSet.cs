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

using System.Collections.Generic;

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// Defines a basic set of audit messages.
	/// </summary>
	public class AuditMessageSet : HashSet<AuditMessage>, IAuditMessageCollection
	{
		#region Properties

		/// <summary>
		/// Gets the highest (most important) severity in the collection.
		/// </summary>
		/// <value>The severity.</value>
		public Severity Severity
		{
			get
			{
				Severity severity = Severity.None;

				foreach (AuditMessage auditMessage in this)
				{
					if (severity < auditMessage.Severity)
						severity = auditMessage.Severity;
				}

				return severity;
			}
		}

		#endregion

		#region Collection Methods

		/// <summary>
		/// Adds the specified severity and message to the set.
		/// </summary>
		/// <param name="severity">The severity.</param>
		/// <param name="message">The message.</param>
		public void Add(Severity severity, string message)
		{
			Add(severity, message, message);
		}

		/// <summary>
		/// Adds the specified severity and message to the set.
		/// </summary>
		/// <param name="severity">The severity.</param>
		/// <param name="key">The key.</param>
		/// <param name="message">The message.</param>
		public void Add(Severity severity, object key, string message)
		{
			Add(new AuditMessage(severity, key, message));
		}

		/// <summary>
		/// Removes the specified message from the audit set.
		/// </summary>
		/// <param name="key">The key.</param>
		public void Remove(object key)
		{
			foreach (AuditMessage auditMessage in this)
			{
				if (auditMessage.Key == key)
				{
					Remove(auditMessage);
					return;
				}
			}
		}

		#endregion
	}
}