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

using System.Collections;

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// The nested auditable class allows for inner auditable, with a
	/// prefix on the message. This allows for an automatic and
	/// recursive handling of messags, including passing up and down.
	/// </summary>
	public class NestedAuditable : Auditable
	{
		// An public list of auditable objects and their prefexes.
		private readonly Hashtable prefixes = new Hashtable();

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
		private void OnAuditMessageChanged(object sender, AuditMessageArgs args)
		{
			// Check for a prefix
			var auditable = sender as IAuditable;
			string msg = args.Message;

			if (auditable != null && prefixes.Contains(auditable))
			{
				msg = prefixes[auditable] + msg;
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