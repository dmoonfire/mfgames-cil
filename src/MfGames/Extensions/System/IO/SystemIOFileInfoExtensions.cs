// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System.IO;
using System.Security.Cryptography;

namespace MfGames.Extensions.System.IO
{
	/// <summary>
	/// Extensions for System.IO.FileInfo.
	/// </summary>
	public static class SystemIOFileInfoExtensions
	{
		#region Methods

		/// <summary>
		/// Gets the basename.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public static string GetBasename(this FileInfo file)
		{
			string name = file.Name.Replace(file.Extension, "");
			return name;
		}

		/// <summary>
		/// Hashes the file with SHA256 and returns the results as a byte
		/// array.
		/// </summary>
		/// <param name="file">The file.</param>
		/// <returns></returns>
		public static byte[] GetSha256(this FileInfo file)
		{
			// If it doesn't exist, throw an exception.
			if (!file.Exists)
			{
				throw new FileNotFoundException("Cannot find file to hash.", file.FullName);
			}

			// Open up a stream in a way that lets others also read it.
			using (
				FileStream stream = file.Open(
					FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				using (var buffered = new BufferedStream(stream))
				{
					using (var hasher = new SHA256Managed())
					{
						byte[] results = hasher.ComputeHash(buffered);
						return results;
					}
				}
			}
		}

		#endregion
	}
}
