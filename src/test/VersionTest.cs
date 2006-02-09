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
  
	[TestFixture] public class VersionTest
	{
#region Parsing
		[ExpectedException(typeof(UtilityException))]
		[Test] public void ParseNull()
		{
			Version v = new Version(null);
			Assert.IsTrue(v == null, "Never get here");
		}

		[ExpectedException(typeof(UtilityException))]
		[Test] public void ParseBlank()
		{
			Version v = new Version("");
			Assert.IsTrue(v == null, "Never get here");
		}
    
		[ExpectedException(typeof(UtilityException))]
		[Test] public void ParseSpace()
		{
			Version v = new Version(" ");
			Assert.IsTrue(v == null, "Never get here");
		}

		[ExpectedException(typeof(UtilityException))]
		[Test] public void ParseInnerSpace()
		{
			Version v = new Version("1 2.3");
			Assert.IsTrue(v == null, "Never get here");
		}

		[Test] public void ParseSingleNumber()
		{
			Version v = new Version("1");
			Assert.AreEqual("1", v.ToString());
		}

		[Test] public void ParseTwoNumbers()
		{
			Version v = new Version("1.2");
			Assert.AreEqual("1.2", v.ToString());
		}

		[Test] public void ParseThreeNumbers()
		{
			Version v = new Version("1.2.3");
			Assert.AreEqual("1.2.3", v.ToString());
		}

		[Test] public void ParseDebianVersion()
		{
			Version v = new Version("1.2.3-4");
			Assert.AreEqual("1.2.3-4", v.ToString());
		}

		[Test] public void ParseDebianVersion2()
		{
			Version v = new Version("1.2-3.4d");
			Assert.AreEqual("1.2-3.4d", v.ToString());
		}

		[Test] public void ParseTextVersion()
		{
			Version v = new Version("1.2b3");
			Assert.AreEqual("1.2b3", v.ToString());
		}
#endregion

#region Version Equals
		/*
		  [Test] public void Compare()
		  {
		  Version v1 = new Version("");
		  Version v2 = new Version("");
		  Assert.IsTrue(v1 == v2);
		  }
		*/   

		[Test] public void CompareSingle()
		{
			Version v1 = new Version("1");
			Version v2 = new Version("1");
			Assert.IsTrue(v1 == v2);
		}

		[Test] public void CompareSingleLess()
		{
			Version v1 = new Version("1");
			Version v2 = new Version("2");
			Assert.IsFalse(v1 == v2);
		}

		[Test] public void CompareSingleGreater()
		{
			Version v1 = new Version("2");
			Version v2 = new Version("1");
			Assert.IsFalse(v1 == v2);
		}

		[Test] public void CompareSingleText()
		{
			Version v1 = new Version("1");
			Version v2 = new Version("1a");
			Assert.IsFalse(v1 == v2);
		}

		[Test] public void CompareSingleTextEqual()
		{
			Version v1 = new Version("1a");
			Version v2 = new Version("1a");
			Assert.IsTrue(v1 == v2);
		}

		[Test] public void CompareSingleTextLess()
		{
			Version v1 = new Version("1a");
			Version v2 = new Version("2a");
			Assert.IsFalse(v1 == v2);
		}

		[Test] public void CompareSingleTextGreater()
		{
			Version v1 = new Version("2a");
			Version v2 = new Version("1a");
			Assert.IsFalse(v1 == v2);
		}

		[Test] public void CompareDouble()
		{
			Version v1 = new Version("1.1");
			Version v2 = new Version("1.1");
			Assert.IsTrue(v1 == v2);
		}

		[Test] public void CompareDoubleLess()
		{
			Version v1 = new Version("1.1");
			Version v2 = new Version("1.2");
			Assert.IsFalse(v1 == v2);
		}

		[Test] public void CompareDoubleGreater()
		{
			Version v1 = new Version("1.2");
			Version v2 = new Version("1.1");
			Assert.IsFalse(v1 == v2);
		}

		[Test] public void CompareDoubleText()
		{
			Version v1 = new Version("1.1");
			Version v2 = new Version("1.1a");
			Assert.IsFalse(v1 == v2);
		}

		[Test] public void CompareDoubleTextEqual()
		{
			Version v1 = new Version("1.1a");
			Version v2 = new Version("1.1a");
			Assert.IsTrue(v1 == v2);
		}

		[Test] public void CompareDoubleTextLess()
		{
			Version v1 = new Version("1.1a");
			Version v2 = new Version("1.2a");
			Assert.IsFalse(v1 == v2);
		}

		[Test] public void CompareDoubleTextGreater()
		{
			Version v1 = new Version("1.2a");
			Version v2 = new Version("1.1a");
			Assert.IsFalse(v1 == v2);
		}
#endregion

#region Version Comparison
		[Test] public void LessSingle()
		{
			Version v1 = new Version("1");
			Version v2 = new Version("1");
			Assert.IsFalse(v1 < v2);
		}

		[Test] public void LessSingleLess()
		{
			Version v1 = new Version("1");
			Version v2 = new Version("2");
			Assert.IsTrue(v1 < v2);
		}

		[Test] public void LessSingleGreater()
		{
			Version v1 = new Version("2");
			Version v2 = new Version("1");
			Assert.IsFalse(v1 < v2);
		}

		[Test] public void LessSingleText()
		{
			Version v1 = new Version("1");
			Version v2 = new Version("1a");
			Assert.IsFalse(v1 < v2);
		}

		[Test] public void LessSingleTextEqual()
		{
			Version v1 = new Version("1a");
			Version v2 = new Version("1a");
			Assert.IsFalse(v1 < v2);
		}

		[Test] public void LessSingleTextLess()
		{
			Version v1 = new Version("1a");
			Version v2 = new Version("2a");
			Assert.IsTrue(v1 < v2);
		}

		[Test] public void LessSingleTextGreater()
		{
			Version v1 = new Version("2a");
			Version v2 = new Version("1a");
			Assert.IsFalse(v1 < v2);
		}
#endregion

#region Greater Than Testing
		[Test] public void GreaterSingle()
		{
			Version v1 = new Version("1");
			Version v2 = new Version("1");
			Assert.IsFalse(v1 > v2);
		}

		[Test] public void GreaterSingleLess()
		{
			Version v1 = new Version("1");
			Version v2 = new Version("2");
			Assert.IsFalse(v1 > v2);
		}

		[Test] public void GreaterSingleGreater()
		{
			Version v1 = new Version("2");
			Version v2 = new Version("1");
			Assert.IsTrue(v1 > v2);
		}

		[Test] public void GreaterSingleText()
		{
			Version v1 = new Version("1");
			Version v2 = new Version("1a");
			Assert.IsFalse(v1 > v2);
		}

		[Test] public void GreaterSingleTextEqual()
		{
			Version v1 = new Version("1a");
			Version v2 = new Version("1a");
			Assert.IsFalse(v1 > v2);
		}

		[Test] public void GreaterSingleTextLess()
		{
			Version v1 = new Version("1a");
			Version v2 = new Version("2a");
			Assert.IsFalse(v1 > v2);
		}

		[Test] public void GreaterSingleTextGreater()
		{
			Version v1 = new Version("2a");
			Version v2 = new Version("1a");
			Assert.IsTrue(v1 > v2);
		}
#endregion

#region CompareOp
		[Test] public void CompareOpEqual()
		{
			Version v1 = new Version("1.2.3");
			Assert.IsTrue(v1.CompareOp("= 1.2.3"), "With space");
		}

		[Test] public void CompareOpEqual2()
		{
			Version v1 = new Version("1.2.3");
			Assert.IsTrue(v1.CompareOp("=1.2.3"), "Without space");
		}

		[Test] public void CompareOpEqual3()
		{
			Version v1 = new Version("1.2.3");
			Assert.IsTrue(v1.CompareOp("=   1.2.3"), "With too many spaces");
		}
#endregion
	}
}
