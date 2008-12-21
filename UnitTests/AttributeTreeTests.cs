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
	public class AttributeTreeTest
	{
		#region Missing
		[TestMethod]
		public void CheckNull()
		{
			// Create an empty one
			AttributeTree at = new AttributeTree();
			Assert.IsTrue(at.Children["/missing"] == null);
		}

		[TestMethod]
		public void CheckNull2()
		{
			// Create an empty one
			AttributeTree at = new AttributeTree();
			Assert.IsTrue(at.Children["/missing", false] == null);
		}

		[TestMethod]
		public void CheckNullSub()
		{
			// Create an empty one
			AttributeTree at = new AttributeTree();
			Assert.IsTrue(at.Children["/missing/inner/node"] == null);
		}
		#endregion

		#region Creation
		[TestMethod]
		public void CheckCreate1()
		{
			AttributeTree at = new AttributeTree();
			Assert.IsTrue(at.Children["/missing/inner/node", true] != null);
		}

		[TestMethod]
		public void CheckCreate2()
		{
			AttributeTree at = new AttributeTree();
			Assert.IsTrue(at.Children["/missing", true] != null);
		}

		[TestMethod]
		public void CheckCreate3()
		{
			AttributeTree at = new AttributeTree();
			AttributeTree at2 = at.Children["/missing/inner/node", true];
			Assert.IsTrue(at2 != null);
			Assert.IsTrue(at.Children["/missing/inner", false] != null);
		}

		[TestMethod]
		public void CheckCreate4()
		{
			AttributeTree at = new AttributeTree();
			AttributeTree at2 = at.Children["/found/inner", true];
			at2.Create("/node");
			Assert.IsTrue(at.Children["/found/inner/node", false] != null);
		}
		#endregion

		[TestMethod]
		public void AddingPluses()
		{
			AttributeTree at = new AttributeTree();
			AttributeTree at2 = at["/Test/Test +1", true];
			Assert.AreEqual("Test +1", at2.Path.Name);
		}
	}
}