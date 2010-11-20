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
using System.Configuration;

#endregion

namespace MfGames.Settings
{
	/// <summary>
	/// Allows for retrieval of application configuration values.
	/// </summary>
	public static class ConfigManager
	{
		/// <summary>
		/// Gets the specified boolean key from the application's .config file. If the key does not
		/// exist, or if it cannot be parsed as an integer, then the default value is returned.
		/// </summary>
		/// <param name="configurationKey">The configuration key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static bool Get(string configurationKey, bool defaultValue)
		{
			bool value;

			if (Boolean.TryParse(ConfigurationManager.AppSettings.Get(configurationKey), out value))
			{
				return value;
			}

			return defaultValue;
		}

		/// <summary>
		/// Gets the specified double key from the application's .config file. If the key does not
		/// exist, or if it cannot be parsed as an integer, then the default value is returned.
		/// </summary>
		/// <param name="configurationKey">The configuration key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static double Get(string configurationKey, double defaultValue)
		{
			double value;

			if (Double.TryParse(ConfigurationManager.AppSettings.Get(configurationKey), out value))
			{
				return value;
			}

			return defaultValue;
		}

		/// <summary>
		/// Gets the specified single key from the application's .config file. If the key does not
		/// exist, or if it cannot be parsed as an integer, then the default value is returned.
		/// </summary>
		/// <param name="configurationKey">The configuration key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static float Get(string configurationKey, float defaultValue)
		{
			float value;

			if (Single.TryParse(ConfigurationManager.AppSettings.Get(configurationKey), out value))
			{
				return value;
			}

			return defaultValue;
		}

		/// <summary>
		/// Gets the specified Guid from the application's .config file. If the key does not
		/// exist, or if it cannot be parsed as an integer, then the default value is returned.
		/// </summary>
		/// <param name="configurationKey">The configuration key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static Guid Get(string configurationKey, Guid defaultValue)
		{
			// Pull out the Guid value from the string. We don't have TryParse for Guid's, so
			// we need to do a bit to check the value just to avoid the overhead of try/catch.
			string guidValue = ConfigurationManager.AppSettings.Get(configurationKey);

			if (!String.IsNullOrEmpty(guidValue))
			{
				try
				{
					// Parse the return the results, using the try/catch to catch any errors.
					return new Guid(guidValue);
				}
				catch
				{
					// If there were any problems, just return the default value.
					return defaultValue;
				}
			}

			return defaultValue;
		}

		/// <summary>
		/// Gets the specified integer key from the application's .config file. If the key does not
		/// exist, or if it cannot be parsed as an integer, then the default value is returned.
		/// </summary>
		/// <param name="configurationKey">The configuration key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static int Get(string configurationKey, int defaultValue)
		{
			int value;

			if (Int32.TryParse(ConfigurationManager.AppSettings.Get(configurationKey), out value))
			{
				return value;
			}

			return defaultValue;
		}

		/// <summary>
		/// Gets the specified Int64 key from the application's .config file. If the key does not
		/// exist, or if it cannot be parsed as an integer, then the default value is returned.
		/// </summary>
		/// <param name="configurationKey">The configuration key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static long Get(string configurationKey, long defaultValue)
		{
			long value;

			if (Int64.TryParse(ConfigurationManager.AppSettings.Get(configurationKey), out value))
			{
				return value;
			}

			return defaultValue;
		}

		/// <summary>
		/// Gets the specified short key from the application's .config file. If the key does not
		/// exist, or if it cannot be parsed as an integer, then the default value is returned.
		/// </summary>
		/// <param name="configurationKey">The configuration key.</param>
		/// <param name="defaultValue">The default value.</param>
		/// <returns></returns>
		public static int Get(string configurationKey, short defaultValue)
		{
			short value;

			if (Int16.TryParse(ConfigurationManager.AppSettings.Get(configurationKey), out value))
			{
				return value;
			}

			return defaultValue;
		}

		/// <summary>
		/// Gets the specified configuration key from the application's .config file.
		/// </summary>
		/// <param name="configurationKey">The configuration key.</param>
		/// <returns></returns>
		public static string Get(string configurationKey)
		{
			return ConfigurationManager.AppSettings.Get(configurationKey);
		}
	}
}