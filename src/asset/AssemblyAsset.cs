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
	using System;
	using System.IO;
	using System.Reflection;

	/// <summary>
	/// Defines a single asset inside the assembly. This is implemented
	/// as a resource inside the assembly.
	/// </summary>
	public class AssemblyAsset : Logable, IAsset
	{
#region Constructors
		/// <summary>
		/// Constructs an asset from the given assembly. If the resource
		/// does not exist, this throws an AssetException.
		/// </summary>
		public AssemblyAsset(AssemblyAssetProvider provider, NodeRef path)
		{
			// Save the fields
			this.provider = provider;
			this.path = path.ToString();

			if (provider.StripLeadingSlash)
				this.path = this.path.Substring(1);

			// Make sure it exists
			bool found = false;

			foreach (string name in provider.Assembly.GetManifestResourceNames())
			{
				if (name == this.path)
				{
					found = true;
					break;
				}
			}

			if (!found)
				throw new AssetException("Cannot find resource '"
					+ path + "' from assembly '"
					+ provider.Assembly.FullName
					+ "'");
		}
#endregion

#region Properties
		private AssemblyAssetProvider provider;
		private string path;

		/// <summary>
		/// Contains the assembly that this asset is keyed into.
		/// </summary>
		public Assembly Assembly
		{
			get { return provider.Assembly; }
		}

		/// <summary>
		/// Contains the provider.
		/// </summary>
		public AssemblyAssetProvider Provider
		{
			get { return provider; }
		}

		/// <summary>
		/// Contains the length or size of the asset. If this is -1 if the
		/// size cannot be read for some reason.
		/// </summary>
		public long Size
		{
			get { return -1; }
		}
#endregion

#region Streams and Readers
		/// <summary>
		/// Opens a stream for reading to this asset.
		/// </summary>
		public Stream OpenStream()
		{
			return Assembly.GetManifestResourceStream(path);
		}

		/// <summary>
		/// Opens a StreamReader object for this asset.
		/// </summary>
		public StreamReader OpenText()
		{
			return new StreamReader(OpenStream());
		}
#endregion
	}
}
