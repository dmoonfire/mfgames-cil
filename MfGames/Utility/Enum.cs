using System;

namespace MfGames.Utility
{
	public static class Enum<T>
	{
		/// <summary>
		/// Parses an enumeration string and returns the results.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static T Parse(string value)
		{
			return (T) Enum.Parse(typeof(T), value);
		}
	}
}
