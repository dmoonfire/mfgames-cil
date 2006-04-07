/*
 * Copyright (C) 2005, Moonfire Games
 *
 * This file is part of MfGames.Utility.
 *
 * The MfGames.Utility library is free software; you can redistribute
 * it and/or modify it under the terms of the GNU Lesser General
 * Public License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307
 * USA
 */

namespace MfGames.Utility
{
	using System;
	using System.Collections;
	using System.IO;
	using System.Xml.Serialization;

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
		private Hashtable auditMessages = new Hashtable();

		// Contains the calculated audit severity
		[NonSerialized]
		private Severity auditSeverity = Severity.None;

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

		/// <summary>
		/// Clears any audit messages that the object may contain.
		/// </summary>
		public void ClearAuditMessage(string message)
		{
			// Ignore blanks
			if (message == null || message.Trim() == "" ||
				auditMessages[message] == null)
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
			ArrayList list = new ArrayList(auditMessages.Keys);

			foreach (string message in list)
				ClearAuditMessage(message);
		}

		/// <summary>
		/// Fires the audit message change message.
		/// </summary>
		protected void FireAuditMessageChanged(object sender,
			string key,
			Severity severity)
		{
			if (AuditMessageChanged != null)
				AuditMessageChanged(sender,
					new AuditMessageArgs(key, severity));
		}

		/// <summary>
		/// Fires the audit severity changed message.
		/// </summary>
		protected void FireAuditSeverityChanged(object sender,
			Severity oldSeverity,
			Severity newSeverity)
		{
			if (AuditSeverityChanged != null)
			{
				AuditSeverityArgs args =
					new AuditSeverityArgs(oldSeverity, newSeverity);
				AuditSeverityChanged(sender, args);
			}
									 
		}

		/// <summary>
		/// Recalculates the internal serverity level.
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
		/// internal hash representation. Nulls and blanks for a message
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
		public void SetAuditMessage(Severity severity, string message,
			bool condition)
		{
			if (condition == true)
				SetAuditMessage(severity, message);
			else
				ClearAuditMessage(message);
		}
	}
}
