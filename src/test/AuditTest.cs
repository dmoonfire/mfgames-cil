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
	using NUnit.Framework;

	[TestFixture]
	public class AuditTest
	{
#region Simple Setting
		[Test] public void TestNone()
		{
			AuditInnerTest ait = new AuditInnerTest();
			Assert.AreEqual(Severity.None, ait.AuditSeverity);
		}

		[Test] public void TestAlert()
		{
			AuditInnerTest ait = new AuditInnerTest();
			ait.SetAuditMessage(Severity.Alert, "test");
			Assert.AreEqual(Severity.Alert, ait.AuditSeverity);
		}

		[Test] public void TestError()
		{
			AuditInnerTest ait = new AuditInnerTest();
			ait.SetAuditMessage(Severity.Error, "test");
			Assert.AreEqual(Severity.Error, ait.AuditSeverity);
		}
#endregion
	}

	public class AuditInnerTest : Auditable
	{
	}
}
