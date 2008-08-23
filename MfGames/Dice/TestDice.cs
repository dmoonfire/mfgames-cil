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

namespace MfGames.Utility
{
	using NUnit.Framework;

	/// <summary>
	/// Tests out the various dice parsing and generation routines.
	/// </summary>
	[TestFixture] public class TestDice
	{
		[Test] public void TestConstant()
		{
			IDice dice = DiceFactory.Parse("2");
			Assert.AreEqual("2", dice.ToString());
			Assert.AreEqual(typeof(ConstantDice), dice.GetType());
		}

		[Test] public void TestSimpleDice()
		{
			IDice dice = DiceFactory.Parse("1d4");
			Assert.AreEqual("1d4", dice.ToString());
			Assert.AreEqual(typeof(RandomDice), dice.GetType());
		}

		[Test] public void TestMultipleRandom()
		{
			IDice dice = DiceFactory.Parse("4d4");
			Assert.AreEqual("4d4", dice.ToString());
		}

		[Test] public void TestSimpleAddition()
		{
			IDice dice = DiceFactory.Parse("1d4+2");
			Assert.AreEqual("1d4+2", dice.ToString());
		}

		[Test] public void TestSimpleSubtraction()
		{
			IDice dice = DiceFactory.Parse("1d4-2");
			Assert.AreEqual("1d4-2", dice.ToString());
		}

		[Test] public void TestMultipleAddition()
		{
			IDice dice = DiceFactory.Parse("1d4+1d6");
			Assert.AreEqual("1d4+1d6", dice.ToString());
		}
	}
}
