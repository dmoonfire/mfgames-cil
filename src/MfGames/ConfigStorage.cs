// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;
using System.IO;

namespace MfGames
{
	/// <summary>
	/// Contains a series of static methods used to finding and managing directories
	/// and files inside the configuration area. On a Unix platform, this will be
	/// located in $HOME/.config and in Application Data on the Windows platform.
	/// </summary>
	public static class ConfigStorage
	{
		#region Properties

		/// <summary>
		/// Contains the configuration directory for the current user.
		/// </summary>
		public static string ConfigurationDirectory
		{
			get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); }
		}

		/// <summary>
		/// Contains the configuration directory for the current user.
		/// </summary>
		public static DirectoryInfo ConfigurationDirectoryInfo
		{
			get { return new DirectoryInfo(ConfigurationDirectory); }
		}

		#endregion

		#region Methods

		/// <summary>
		/// Retrieves, creating if needed, a directory inside the user's configuration area.
		/// </summary>
		public static string GetDirectory(params string[] relativeDirectories)
		{
			return GetDirectoryInfo(true, relativeDirectories).FullName;
		}

		/// <summary>
		/// Retrieves, creating if requested, a directory inside the user's configuration area.
		/// </summary>
		public static string GetDirectory(
			bool create,
			params string[] relativeDirectories)
		{
			return GetDirectoryInfo(create, relativeDirectories).FullName;
		}

		/// <summary>
		/// Retrieves, creating if needed, a directory inside the user's configuration area.
		/// </summary>
		public static DirectoryInfo GetDirectoryInfo(
			params string[] relativeDirectories)
		{
			return GetDirectoryInfo(true, relativeDirectories);
		}

		/// <summary>
		/// Retrieves, creating if requested, a directory inside the user's configuration area.
		/// </summary>
		public static DirectoryInfo GetDirectoryInfo(
			bool create,
			params string[] relativeDirectories)
		{
			// Start at the top-level configuration directory and create all the relative
			// directories before returning the inner-mode directory.
			DirectoryInfo directory = ConfigurationDirectoryInfo;

			foreach (string relativeDirectory in relativeDirectories)
			{
				// Create the new child directory.
				directory =
					new DirectoryInfo(Path.Combine(directory.FullName, relativeDirectory));

				// Create the directory if we were requested to create it and it doesn't exist.
				if (create && !directory.Exists)
				{
					directory.Create();
				}
			}

			// Return the resulting directory.
			return directory;
		}

		#endregion
	}
}
