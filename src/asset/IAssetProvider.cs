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
	/// <summary>
	/// Defines the source of assets. In this library, there is the
	/// AssemblyAssetProvider and the FileSystemAssetProvider which
	/// provide assets from the assembly and filesystem respectively.
	///
	/// The primary purpose of the IAssetProvider is to give a static
	/// and consistent access to various sources of files and objects
	/// (known as "assets"). NodeRef gives a consistent and static
	/// access to the assets while still allowing some abstract
	/// functionality, such as a cached asset manager or one that is
	/// version-aware.
	/// </summary>
	public interface IAssetProvider
	{
#region Assets
		/// <summary>
		/// Retrieves an asset from the provider. If the second parameter
		/// is true and the asset cannot be found, an AssetException is
		/// thrown. Otherwise, this function will return null if it cannot
		/// be found.
		/// </summary>
		IAsset GetAsset(NodeRef path, bool exceptionIfMissing);
#endregion
	}
}
