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

using NUnit.Framework;

namespace MfGames.Utility
{
	[TestFixture] public class UtilityTest
	{
		[Test] public void TestWeightedSelectorEmpty()
		{
			WeightedSelector ws = new WeightedSelector();
			Assert.AreEqual(ws.Total, 0);
		}

		[Test] public void TestWeightedSelectorAdd2()
		{
			WeightedSelector ws = new WeightedSelector();
			ws["bob"] = 5;
			ws["gary"] = 5;
			Assert.AreEqual(ws.Total, 10);
		}

		[Test] public void TestWeightedSelectorReplace()
		{
			WeightedSelector ws = new WeightedSelector();
			ws["bob"] = 5;
			ws["gary"] = 5;
			ws["bob"] = 2;
			Assert.AreEqual(ws.Total, 7);
		}

		[Test] public void TestMd5HexString()
		{
			Assert.AreEqual("5e027396789a18c37aeda616e3d7991b",
				MfConvert.ToMd5HexString("jim"));
			Assert.AreEqual("8621ffdbc5698829397d97767ac13db3",
				MfConvert.ToMd5HexString("dragon"));
		}
	}
}
