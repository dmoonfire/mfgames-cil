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
	using System.IO;

	/// <summary>
	/// Defines a single asset or data object. This can be an image, a
	/// file, or just about anything. An asset may be on the filesystem,
	/// an assembly, or another source. Since the method for creating an
	/// asset is specific, this interface actually has no methods for
	/// writing to or creating new assets.
	///
	/// The model for the IAsset is System.IO.FileInfo, but for a more
	/// specialized (read-only) use of generic sources (assemblies,
	/// filesystem, zip files, etc).
	///
	/// Opening up the stream without closing it can thrown an
	/// exception, depending on the provider of the asset.
	/// </summary>
	public interface IAsset
	{
#region Properties
		/// <summary>
		/// Contains the length or size of the asset. If this is -1 if the
		/// size cannot be read for some reason.
		/// </summary>
		long Size { get; }
#endregion

#region Streams and Readers
		/// <summary>
		/// Opens a stream for reading to this asset.
		/// </summary>
		Stream OpenStream();

		/// <summary>
		/// Opens a StreamReader object for this asset.
		/// </summary>
		StreamReader OpenText();
#endregion
	}
}
