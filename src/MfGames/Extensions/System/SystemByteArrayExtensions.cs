// <copyright file="SystemByteArrayExtensions.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;

namespace MfGames.Extensions.System
{
	/// <summary>
	/// Extension methods for byte[] classes.
	/// </summary>
	public static class SystemByteArrayExtensions
	{
		#region Public Methods and Operators

		/// <summary>
		/// Converts a byte array into a hex string.
		/// </summary>
		/// <param name="array">
		/// The array to convert.
		/// </param>
		/// <returns>
		/// A formatted string or null.
		/// </returns>
		public static string ToHexString(this byte[] array)
		{
			// If we have a null array, just skip it.
			if (array == null)
			{
				return null;
			}

			// This is a very inefficent method, but works for the time being.
			string hex = BitConverter.ToString(array)
				.Replace(
					"-",
					string.Empty);
			return hex;
		}

		#endregion
	}
}
