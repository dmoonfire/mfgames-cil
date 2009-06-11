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
using System.Collections;
using System.Xml.Serialization;

using MfGames.Logging;

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// An audited object is one that implements both the logging
	/// interface and contains a basic functionality for auditing. This
	/// implements the IAudited object.
	///
	/// This class is serializable, but the data is not stored. Instead,
	/// the object is expected to rebuild it as the properties are set.
	/// </summary>
	[Serializable]
	public class Auditable : Logable, IAuditable
	{
		// Contains the hash table with all of the messages
		[NonSerialized]
		private readonly Hashtable auditMessages = new Hashtable();

		// Contains the calculated audit severity
		[NonSerialized]
		private Severity auditSeverity = Severity.None;

		#region IAuditable Members

		/// <summary>
		/// This event is triggered when a message is changed in the
		/// system. The given severity will be Severity.None if it is
		/// removed.
		/// </summary>
		public event AuditMessageHandler AuditMessageChanged;

		/// <summary>
		/// This event is triggered when the severity changes. This
		/// will not be called if the message changes, but the
		/// severity does not.
		/// </summary>
		public event AuditSeverityHandler AuditSeverityChanged;

		/// <summary>
		/// Returns a hashtable that contains messages as the key
		/// and the severity as the value for the key value.
		/// </summary>
		[XmlIgnore]
		public virtual Hashtable AuditMessages
		{
			get { return auditMessages; }
		}

		/// <summary>
		/// Returns the highest audit severity of all current audit
		/// messages. If there are no audit messages, then this returns
		/// Severity.None.
		/// </summary>
		[XmlIgnore]
		public virtual Severity AuditSeverity
		{
			get { return auditSeverity; }
		}

		#endregion

		/// <summary>
		/// Clears any audit messages that the object may contain.
		/// </summary>
		public void ClearAuditMessage(string message)
		{
			// Ignore blanks
			if (message == null || message.Trim() == "" || auditMessages[message] == null)
				return;

			// Remove it and recalculate the levels
			auditMessages.Remove(message);
			FireAuditMessageChanged(this, message, Severity.None);
			RecalculateAuditSeverity();
		}

		/// <summary>
		/// Clears out all of the audit messages.
		/// </summary>
		public void ClearAuditMessages()
		{
			// Go through each one since we want to trigger the
			// various events.
			var list = new ArrayList(auditMessages.Keys);

			foreach (string message in list)
				ClearAuditMessage(message);
		}

		/// <summary>
		/// Fires the audit message change message.
		/// </summary>
		protected void FireAuditMessageChanged(
			object sender, string key, Severity severity)
		{
			if (AuditMessageChanged != null)
				AuditMessageChanged(sender, new AuditMessageArgs(key, severity));
		}

		/// <summary>
		/// Fires the audit severity changed message.
		/// </summary>
		protected void FireAuditSeverityChanged(
			object sender, Severity oldSeverity, Severity newSeverity)
		{
			if (AuditSeverityChanged != null)
			{
				var args = new AuditSeverityArgs(oldSeverity, newSeverity);
				AuditSeverityChanged(sender, args);
			}
		}

		/// <summary>
		/// Recalculates the public serverity level.
		/// </summary>
		private Severity RecalculateAuditSeverity()
		{
			// Set the initial level
			Severity oldSeverity = auditSeverity;
			Severity s = Severity.None;

			foreach (Severity s1 in AuditMessages.Values)
				if (s1 > s)
					s = s1;

			// Set it
			auditSeverity = s;

			// Check for change
			if (s != oldSeverity)
				FireAuditSeverityChanged(this, oldSeverity, s);

			// Return it
			return s;
		}

		/// <summary>
		/// Sets an audit message and recalculates the severity. If the
		/// severity is Severity.None, then this removes it from the
		/// public hash representation. Nulls and blanks for a message
		/// are ignored.
		/// </summary>
		public void SetAuditMessage(Severity severity, string message)
		{
			// Ignore blanks
			if (message == null || message.Trim() == "")
				return;

			// Check for None
			if (severity == Severity.None)
			{
				ClearAuditMessage(message);
			}
			else
			{
				// Don't bother if it hasn't changed
				if (auditMessages[message] != null &&
				    (Severity) auditMessages[message] == severity)
					return;

				// Set the level
				auditMessages[message] = severity;
			}

			// Recalculate the severity
			FireAuditMessageChanged(this, message, severity);
			RecalculateAuditSeverity();
		}

		/// <summary>
		/// Sets or clears the audit message, based on a boolean
		/// condition.
		/// </summary>
		public void SetAuditMessage(Severity severity, string message, bool condition)
		{
			if (condition)
				SetAuditMessage(severity, message);
			else
				ClearAuditMessage(message);
		}
	}
}