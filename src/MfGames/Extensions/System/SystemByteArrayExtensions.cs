// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;

namespace MfGames.Extensions.System
{
	/// <summary>
	/// Extension methods for byte[] classes.
	/// </summary>
	public static class SystemByteArrayExtensions
	{
		/// <summary>
		/// Converts a byte array into a hex string.
		/// </summary>
		/// <param name="array">The array to convert.</param>
		/// <returns>A formatted string or null.</returns>
		public static string ToHexString(this byte[] array)
		{
			// If we have a null array, just skip it.
			if (array == null)
			{
				return null;
			}

			// This is a very inefficent method, but works for the time being.
			string hex = BitConverter.ToString(array).Replace("-", "");
			return hex;
		}
	}
}