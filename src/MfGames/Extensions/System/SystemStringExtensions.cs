// <copyright file="SystemStringExtensions.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Extensions.System
{
	public static class SystemStringExtensions
	{
		#region Public Methods and Operators

		/// <summary>
		/// Returns a null if the input value is null or a blank string.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static string NullIfBlank(this string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				return null;
			}

			return value;
		}

		#endregion
	}
}
