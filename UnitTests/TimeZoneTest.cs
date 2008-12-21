#region Copyright
/*
 * Copyright (C) 2005-2008, Moonfire Games
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
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

using System;
using MfGames.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
/// <summary>
/// Test the time zones.
/// </summary>
	[TestClass]
	public class TestTimeZones
	{
		[TestMethod]
		public void TestSlashUSCentral()
		{
			TimeZone tz = TimeZones.ToTimeZone("US/Central");
			Assert.AreEqual("-06:00:00",
			                tz.GetUtcOffset(new DateTime(2005, 1, 1)).ToString());
		}

		[TestMethod]
		public void TestSlashUSCentralCST()
		{
			TimeZone tz = TimeZones.ToTimeZone("US/Central");
			Assert.AreEqual("CST", tz.StandardName);
		}

		[TestMethod]
		public void TestSlashUSCentralCDT()
		{
			TimeZone tz = TimeZones.ToTimeZone("US/Central");
			Assert.AreEqual("CDT", tz.DaylightName);
		}
	}
}