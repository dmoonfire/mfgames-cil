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
	using System.Collections;

	/// <summary>
	/// Defines an asset provider that takes a list of asset providers
	/// and gives a central access. This is a form of layered access to
	/// the providers, except that the first one found is returned.
	/// </summary>
	public class ListAssetProvider : IAssetProvider
	{
		/// <summary>
		/// Constructs an empty list asset provider.
		/// </summary>
		public ListAssetProvider()
		{
			providers = new ArrayList();
		}

#region Assets
		/// <summary>
		/// Retrieves an asset from the provider. If the second parameter
		/// is true and the asset cannot be found, an AssetException is
		/// thrown. Otherwise, this function will return null if it cannot
		/// be found.
		/// </summary>
		public IAsset GetAsset(NodeRef path, bool exceptionIfMissing)
		{
			// Go through the assets
			foreach (IAssetProvider provider in providers)
			{
				try
				{
					// We always throw an exception to handle the processing
					return provider.GetAsset(path, true);
				}
				catch {}
			}

			// If we get this far, every asset threw an exception trying to
			// get the asset path.
			if (exceptionIfMissing)
			{
				throw new AssetException("Cannot find " +
					path + " in " + providers.Count
					+ " providers");
			}
			else
			{
				return null;
			}
		}
#endregion

#region Providers
		private ArrayList providers;

		/// <summary>
		/// Adds an asset provider to the list.
		/// </summary>
		public void AddAssetProvider(IAssetProvider provider)
		{
			if (provider == null)
				throw new AssetException("Cannot add a null provider");

			providers.Add(provider);
		}
#endregion
	}
}
