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

	/// <summary>
	/// An audited class is based on the concept that a class is capable
	/// of indicating problems or issues with its properties and
	/// internal structures.
	///
	/// The way this is organized, a class can create the audit messages
	/// during creation or setting (such as part of the getter) or at
	/// the point of the message request.
	/// </summary>
	public interface IAuditable
	{
		/// <summary>
		/// Returns a hashtable that contains messages as the key
		/// and the severity as the value for the key value.
		/// </summary>
		Hashtable AuditMessages { get; }

		/// <summary>
		/// Returns the highest audit severity of all current audit
		/// messages. If there are no audit messages, then this returns
		/// Severity.None.
		/// </summary>
		Severity AuditSeverity { get; }

		/// <summary>
		/// This event is triggered when a message is changed in the
		/// system. The given severity will be Severity.None if it is
		/// removed.
		/// </summary>
		event AuditMessageHandler AuditMessageChanged;

		/// <summary>
		/// This event is triggered when the severity changes. This
		/// will not be called if the message changes, but the
		/// severity does not.
		/// </summary>
		event AuditSeverityHandler AuditSeverityChanged;
	}
}
