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
	/// Contains a nested tree of settings objects which are set and get into 
	/// various objects.
	/// </summary>
	public class SettingsCollection
	{
		#region Settings

		/// <summary>
		/// Clears the specified settings for the given object.
		/// </summary>
		/// <param name="settingsObject">The settings object.</param>
		public void Clear(object settingsObject)
		{
		}

		/// <summary>
		/// Gets the specified settings and populates the settingsObject.
		/// </summary>
		/// <param name="settingsObject">The settings object.</param>
		/// <returns></returns>
		public bool Get(object settingsObject)
		{
			return false;
		}

		/// <summary>
		/// Sets the specified settings from the given object.
		/// </summary>
		/// <param name="settingsObject">The settings object.</param>
		public void Set(object settingsObject)
		{
		}

		#endregion

		#region Cloning

		/// <summary>
		/// Clones this instance.
		/// </summary>
		/// <returns></returns>
		public SettingsCollection Clone()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Clones the specified settings for the given object.
		/// </summary>
		/// <param name="settingsObject">The settings object.</param>
		/// <returns></returns>
		public SettingsCollection Clone(object settingsObject)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}