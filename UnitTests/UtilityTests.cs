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

using MfGames.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	[TestClass]
	public class UtilityTests
	{
		[TestMethod]
		public void TestWeightedSelectorEmpty()
		{
			WeightedSelector ws = new WeightedSelector();
			Assert.AreEqual(ws.Total, 0);
		}

		[TestMethod]
		public void TestWeightedSelectorAdd2()
		{
			WeightedSelector ws = new WeightedSelector();
			ws["bob"] = 5;
			ws["gary"] = 5;
			Assert.AreEqual(ws.Total, 10);
		}

		[TestMethod]
		public void TestWeightedSelectorReplace()
		{
			WeightedSelector ws = new WeightedSelector();
			ws["bob"] = 5;
			ws["gary"] = 5;
			ws["bob"] = 2;
			Assert.AreEqual(ws.Total, 7);
		}

		[TestMethod]
		public void TestMd5HexString()
		{
			Assert.AreEqual("5e027396789a18c37aeda616e3d7991b",
			                Convert.ToMd5HexString("jim"));
			Assert.AreEqual("8621ffdbc5698829397d97767ac13db3",
			                Convert.ToMd5HexString("dragon"));
		}
	}
}