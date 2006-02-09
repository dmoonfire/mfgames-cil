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
	using System.Text.RegularExpressions;

	/// <summary>
	/// This implements a static, singleton-style class that managed a
	/// configuration or customization area. It allows for
	/// $HOME/.MfGames/ on Unix/Linux while creating a folder called
	/// MfGames in the "Application Data" folder on Windows.
	/// </summary>
	public class ConfigStorage : Logable
	{
#region Constructor
		/// <summary>
		/// Creates a new storage constructor with the given key.
		/// </summary>
		public ConfigStorage(string storageKey)
		{
			// Save the field
			this.storageKey = storageKey;

			// Initialize the storage
			InitStorage();
		}
#endregion

#region Singleton
		// Contains the singleton, if one is defined
		private static ConfigStorage singleton = null;

		/// <summary>
		/// Contains the singleton storage as defined by the
		/// application. This is null unless defined by the application.
		/// </summary>
		public static ConfigStorage Singleton
		{
			get { return singleton; }
			set { singleton = value; }
		}
#endregion

#region Storage Key
		// Contains the regex used to verify the storage key
		private static readonly Regex ValidateStorageKey
		= new Regex("^[\\w\\d ]+$");

		// Contains the key which defines where/what type of config
		// storage to create. This is used as the directory name of the
		// top level and many of the functions will throw an exception if
		// this is not defined.
		private string storageKey = null;

		/// <summary>
		/// Getter and setter for StorageKey. A storage key can consist of
		/// spaces, letters, and numbers, but no other special
		/// characters. It is case-sensitive.
		/// </summary>
		public string StorageKey
		{
			get
			{
				// Check for null, if so, throw an exception
				if (storageKey == null)
					throw new Exception("StorageKey is not defined");

				// Return the key
				return storageKey;
			}
			set
			{
				// Cause problems if we get a null
				if (value == null)
					throw new Exception("Cannot assign a null to StorageKey");

				// Check for invalid key
				if (!ValidateStorageKey.IsMatch(value))
					throw new Exception("StorageKey may only have letters, "
						+ "numbers, and spaces.");

				// Set it, while trimming. Leading and trailing spaces are
				// bad in general and make life harder. So we do it silently.
				storageKey = value.Trim();
				Debug("StorageKey: {0}", storageKey);
			}
		}
#endregion

#region Storage Directory
		/// <summary>
		/// Creates the top-level directory for storage. This assumes that
		/// the storage key is defined.
		/// </summary>
		public void InitStorage()
		{
			// Create it
			DirectoryInfo dir = StorageDirectory;

			if (!dir.Exists)
			{
				Info("Created storage area: {0}", dir.FullName);
				dir.Create();
			}
		}

		/// <summary>
		/// Returns a DirectoryInfo representing the storage area. The
		/// directory is not ensured to be created, unless the
		/// InitStorage() method is called.
		/// </summary>
		public DirectoryInfo StorageDirectory
		{
			get
			{
				return new DirectoryInfo(Environment
					.GetFolderPath(Environment
						.SpecialFolder
						.ApplicationData)
					+ Path.DirectorySeparatorChar
					+ StorageKey);
			}
		}

		/// <summary>
		/// Returns a directory to represent a subapplication inside the
		/// system. This assumes that the storage directory has already
		/// been created, but it will create folders if they have not
		/// been. This automatically creates the folder for the
		/// application.
		/// </summary>
		public DirectoryInfo GetDirectory(string appName)
		{
			// Cause problems if we get a null
			if (appName == null)
				throw new Exception("Cannot create a null application");
      
			// Check for invalid key
			if (!ValidateStorageKey.IsMatch(appName))
				throw new Exception("Application names may only have letters, "
					+ "numbers, and spaces.");

			// Get the path
			string path = StorageDirectory.FullName
				+ Path.DirectorySeparatorChar
				+ appName;
			DirectoryInfo dir = new DirectoryInfo(path);

			// Create it if it doesn't exist
			if (!dir.Exists)
			{
				Info("Creating application folder: {0}", path);
				dir.Create();
			}

			// Return it
			return dir;
		}
#endregion
	}
}
