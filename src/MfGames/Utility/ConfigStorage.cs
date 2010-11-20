#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System;
using System.IO;
using System.Text.RegularExpressions;

using MfGames.Logging;

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// This implements a static, singleton-style class that managed a
	/// configuration or customization area. It allows for
	/// $HOME/.MfGames/ on Unix/Linux while creating a folder called
	/// MfGames in the "Application Data" folder on Windows.
	/// </summary>
	public class ConfigStorage
	{
		#region Constructor

		/// <summary>
		/// Creates a new storage constructor with the given key.
		/// </summary>
		public ConfigStorage(string storageKey)
		{
			// Save the field, this also creates the storage.
			StorageName = storageKey;
		}

		#endregion

		#region Singleton

		/// <summary>
		/// Gets or sets the application directory which can be stored by the application.
		/// </summary>
		/// <value>The application directory.</value>
		public static DirectoryInfo ApplicationDirectory { get; set; }

		/// <summary>
		/// Contains the singleton storage as defined by the
		/// application. This is null unless defined by the application.
		/// </summary>
		public static ConfigStorage Instance { get; set; }

		#endregion

		#region Storage Key

		// Contains the regex used to verify the storage key
		private static readonly Regex ValidateStorageKey = new Regex("^[\\w\\d ]+$");

		// Contains the key which defines where/what type of config
		// storage to create. This is used as the directory name of the
		// top level and many of the functions will throw an exception if
		// this is not defined.
		private string storageName;

		/// <summary>
		/// Getter and setter for StorageName. A storage key can consist of
		/// spaces, letters, and numbers, but no other special
		/// characters. It is case-sensitive.
		/// </summary>
		public string StorageName
		{
			get
			{
				// Return the key
				return storageName;
			}
			set
			{
				// If we are null or blank, then just clear it.
				if (string.IsNullOrEmpty(value) || value.Trim().Length == 0)
				{
					storageName = null;
				}

				// Check for invalid key
				if (!ValidateStorageKey.IsMatch(value))
				{
					throw new Exception("StorageName may only have letters, numbers, and spaces.");
				}

				// Set it, while trimming. Leading and trailing spaces are
				// bad in general and make life harder. So we do it silently.
				storageName = value.Trim();
				Initialize();
			}
		}

		#endregion

		#region Storage Directory

		/// <summary>
		/// Returns a DirectoryInfo representing the storage area. The
		/// directory is not ensured to be created, unless the
		/// Initialize() method is called.
		/// </summary>
		public DirectoryInfo StorageDirectory
		{
			get
			{
				string directoryName = string.Format("{0}{1}{2}",
				                                     Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
				                                     Path.DirectorySeparatorChar,
				                                     StorageName);
				return new DirectoryInfo(directoryName);
			}
		}

		/// <summary>
		/// Creates the top-level directory for storage. This assumes that
		/// the storage key is defined.
		/// </summary>
		public void Initialize()
		{
			// If we don't have a group, then ignore it.
			if (storageName == null)
			{
				return;
			}

			// Create it
			DirectoryInfo dir = StorageDirectory;

			if (!dir.Exists)
			{
				Log.Info("Created storage area: {0}", dir.FullName);
				dir.Create();
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
			{
				throw new Exception("Cannot create a null application");
			}

			// Check for invalid key
			if (!ValidateStorageKey.IsMatch(appName))
			{
				throw new Exception("Application names may only have letters, numbers, and spaces.");
			}

			// Get the path
			string path = StorageDirectory.FullName + Path.DirectorySeparatorChar + appName;
			var dir = new DirectoryInfo(path);

			// Create it if it doesn't exist
			if (!dir.Exists)
			{
				Log.Info("Creating application folder: {0}", path);
				dir.Create();
			}

			// Return it
			return dir;
		}

		#endregion

		#region Logging

		private Log internalLog;

		/// <summary>
		/// Gets the logging interface for this class.
		/// </summary>
		/// <value>The log.</value>
		private Log Log
		{
			get
			{
				if (internalLog == null)
				{
					internalLog = new Log(this);
				}

				return internalLog;
			}
		}

		#endregion
	}
}