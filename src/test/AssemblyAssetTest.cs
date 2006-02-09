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
	/// <summary>
	/// Performs the various tests on the MfGames.Utility.Tool
	/// namespace. This uses a few private classes to handle the
	/// actual filling of command arguments.
	/// </summary>
	[TestFixture] public class AssemblyAssetTest
	{
		[Test]
		public void TestAssemblySimple()
		{
			AssemblyAssetProvider aap =
				new AssemblyAssetProvider(GetType().Assembly);
			IAsset asset =
				aap.GetAsset(new NodeRef("/AssemblyAssetTest.cs"), false);
			Assert.IsNotNull(asset);
		}

		[Test]
		public void TestAssemblySimple2()
		{
			AssemblyAssetProvider aap =
				new AssemblyAssetProvider(GetType().Assembly);
			IAsset asset = aap.GetAsset(new NodeRef("/foo/bar"), false);
			Assert.IsNotNull(asset);
		}

		[Test]
		public void TestAssemblyMissingSlash()
		{
			AssemblyAssetProvider aap =
				new AssemblyAssetProvider(GetType().Assembly);
			IAsset asset = aap.GetAsset(new NodeRef("/gary/bob"), false);
			Assert.IsNull(asset);
		}

		[Test, ExpectedException(typeof(AssetException))]
		public void TestAssemblyMissingException()
		{
			AssemblyAssetProvider aap =
				new AssemblyAssetProvider(GetType().Assembly);
			aap.GetAsset(new NodeRef("/gary/bob"), true);
		}

		[Test]
		public void TestAssemblyStrippedSlash()
		{
			AssemblyAssetProvider aap =
				new AssemblyAssetProvider(GetType().Assembly);
			aap.StripLeadingSlash = false;
			IAsset asset = aap.GetAsset(new NodeRef("/gary/bob"), false);
			Assert.IsNotNull(asset);
		}
	}
}
