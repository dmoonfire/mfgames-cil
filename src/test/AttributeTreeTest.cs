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
  
	[TestFixture] public class AttributeTreeTest
	{
#region Missing
		[Test] public void CheckNull()
		{
			// Create an empty one
			AttributeTree at = new AttributeTree();
			Assert.IsTrue(at.Children["/missing"] == null);
		}

		[Test] public void CheckNull2()
		{
			// Create an empty one
			AttributeTree at = new AttributeTree();
			Assert.IsTrue(at.Children["/missing", false] == null);
		}

		[Test] public void CheckNullSub()
		{
			// Create an empty one
			AttributeTree at = new AttributeTree();
			Assert.IsTrue(at.Children["/missing/inner/node"] == null);
		}
#endregion

#region Creation
		[Test] public void CheckCreate1()
		{
			AttributeTree at = new AttributeTree();
			Assert.IsTrue(at.Children["/missing/inner/node", true] != null);
		}      

		[Test] public void CheckCreate2()
		{
			AttributeTree at = new AttributeTree();
			Assert.IsTrue(at.Children["/missing", true] != null);
		}      

		[Test] public void CheckCreate3()
		{
			AttributeTree at = new AttributeTree();
			AttributeTree at2 = at.Children["/missing/inner/node", true];
			Assert.IsTrue(at2 != null);
			Assert.IsTrue(at.Children["/missing/inner", false] != null);
		}      

		[Test] public void CheckCreate4()
		{
			AttributeTree at = new AttributeTree();
			AttributeTree at2 = at.Children["/found/inner", true];
			at2.Create("/node");
			Assert.IsTrue(at.Children["/found/inner/node", false] != null);
		}      
#endregion

		[Test] public void AddingPluses()
		{
			AttributeTree at = new AttributeTree();
			AttributeTree at2 = at["/Test/Test +1", true];
			Assert.AreEqual("Test +1", at2.Path.Name);
		}
	}
}
