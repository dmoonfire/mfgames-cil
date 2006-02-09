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
	using System.Reflection;

	/// <summary>
	/// Constructs a basic asset provider that takes an assembly and
	/// provides files from the manifest.
	///
	/// This provider assumes that the files in the manifest stream are
	/// stored in a specific format: "a/b/c" where the leading "/" of
	/// the NodeRef is automatically stripped (see StripLeadingSlash
	/// property).
	/// </summary>
	public class AssemblyAssetProvider : Logable, IAssetProvider
	{
#region Constructors
		/// <summary>
		/// Creates a provider with a given assembly.
		/// </summary>
		public AssemblyAssetProvider(Assembly assembly)
		{
			if (assembly == null)
				throw new AssetException("Cannot create an assembly asset provider "
					+ "with a null assembly");

			this.assembly = assembly;
		}
#endregion

#region Assets
		/// <summary>
		/// Retrieves an asset from the provider. If the second parameter
		/// is true and the asset cannot be found, an AssetException is
		/// thrown. Otherwise, this function will return null if it cannot
		/// be found.
		/// </summary>
		public IAsset GetAsset(NodeRef path, bool exceptionIfMissing)
		{
			// Create the assembly asset and return it
			try
			{
				// Create and return the assembly
				return new AssemblyAsset(this, path);
			}
			catch (Exception e)
			{
				// Throw the exception if we were requested to
				if (exceptionIfMissing)
					throw e;
				else
					return null;
			}
		}
#endregion

#region Debugging
		/// <summary>
		/// Debugs this provider by dumping out important data.
		/// </summary>
		public void Debug()
		{
			Debug("Dumping assembly names:");
      
			foreach (string name in assembly.GetManifestResourceNames())
			{
				Debug("  Resouce: {0}", name);
			}
		}
#endregion

#region Properties
		private Assembly assembly;
		private bool stripLeadingSlash = true;

		/// <summary>
		/// Contains the assembly that this provider is keyed into.
		/// </summary>
		public Assembly Assembly
		{
			get { return assembly; }
		}

		/// <summary>
		/// Contains true of the various assets should strip the leading
		/// slash of the path (as a NodeRef) before attempting to retrieve
		/// the asset.
		/// </summary>
		public bool StripLeadingSlash
		{
			get { return stripLeadingSlash; }
			set { stripLeadingSlash = value; }
		}
#endregion
	}
}
