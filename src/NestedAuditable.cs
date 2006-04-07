/*
 * Copyright (C) 2006, Moonfire Games
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

using System;
using System.Collections;

namespace MfGames.Utility
{
	/// <summary>
	/// The nested auditable class allows for inner auditable, with a
	/// prefix on the message. This allows for an automatic and
	/// recursive handling of messags, including passing up and down.
	/// </summary>
	public class NestedAuditable
	: Auditable
	{
		// An internal list of auditable objects and their prefexes.
		private Hashtable prefixes = new Hashtable();

		/// <summary>
		/// Adds an auditable into this nested one, including
		/// registering for new messages.
		/// </summary>
		public void AddAuditable(IAuditable auditable)
		{
			// Add the listener
			auditable.AuditMessageChanged += OnAuditMessageChanged;

			// Add the audit messages
			string pmsg = "";
			Hashtable a = auditable.AuditMessages;

			if (prefixes.Contains(auditable))
				pmsg = prefixes[auditable].ToString();

			foreach (string msg in a.Keys)
				SetAuditMessage((Severity) a[msg], pmsg + msg);
		}

		/// <summary>
		/// Adds an inner auditable object, including a message prefix
		/// for the messages.
		/// </summary>
		public void AddAuditable(IAuditable auditable, string message)
		{
			if (auditable == null)
				return;

			if (message != null && message != "")
				prefixes[auditable] = message;

			AddAuditable(auditable);
		}

		/// <summary>
		/// Triggered when an inner auditable changes.
		/// </summary>
		private void OnAuditMessageChanged
			(object sender, AuditMessageArgs args)
		{
			// Check for a prefix
			IAuditable auditable = sender as IAuditable;
			string msg = args.Message;

			if (auditable != null && prefixes.Contains(auditable))
			{
				msg = prefixes[auditable].ToString() + msg;
			}

			// Add it ourselve
			SetAuditMessage(args.Severity, msg);
		}

		/// <summary>
		/// Removes an auditable object from this one. This
		/// automatically removes the auditable's messages from this
		/// one.
		/// </summary>
		public void RemoveAuditable(IAuditable auditable)
		{
			RemoveAuditable(auditable, true);
		}

		/// <summary>
		/// Removes an auditable from this object, optionally removing
		/// the messages.
		/// </summary>
		public void RemoveAuditable(IAuditable auditable, bool remove)
		{
			// Remove our messages
			if (remove)
			{
				string pmsg = "";
				
				if (prefixes.Contains(auditable))
					pmsg = prefixes[auditable].ToString();
				
				foreach (string msg in auditable.AuditMessages.Keys)
					ClearAuditMessage(pmsg + msg);
			}

			// Remove the prefix
			if (prefixes.Contains(auditable))
				prefixes.Remove(auditable);

			// Remove the message
			auditable.AuditMessageChanged -= OnAuditMessageChanged;
		}
	}
}
