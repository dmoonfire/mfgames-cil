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

#endregion

namespace MfGames.Settings
{
	/// <summary>
	/// A settings object, or more importantly classes that extend it,
	/// are the core part of the configuration system. The basic
	/// interface only provides for setting and retrieving string
	/// values from a group/name organization. Extending classes may
	/// wrap int, long, or other objects as part of the configuration.
	///
	/// Settings are also nested. Multiple settings may be wrapped
	/// around each other to provide variables. publicly, all
	/// settings are backed by another ISettings object which may be
	/// another Settings object or eventually a ISettingsStore class.
	/// </summary>
	public class Settings : ISettings
	{
		#region Constructors

		public Settings(ISettings baseSettings)
		{
			// Valid the variable and save it
			if (baseSettings == null)
				throw new Exception("Cannot back a settings object with a null object");

			this.baseSettings = baseSettings;
		}

		#endregion

		#region Backing Properties

		private ISettings baseSettings;

		#endregion

		#region Accessing

		/// <summary>
		/// This is the primary method for getting and setting values
		/// in the system. If a null is set, then the value is
		/// removed.
		/// </summary>
		public string this[string group, string variable]
		{
			get { return baseSettings[group, variable]; }
			set { baseSettings[group, variable] = value; }
		}

		/// <summary>
		/// This method returns true if a specific group/variable
		/// exists within the system.
		/// </summary>
		public bool Contains(string group, string variable)
		{
			return baseSettings.Contains(group, variable);
		}

		#endregion
	}
}