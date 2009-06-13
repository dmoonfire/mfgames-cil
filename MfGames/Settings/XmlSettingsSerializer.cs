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

#endregion

namespace MfGames.Settings
{
	public class XmlSettingsSerializer
	{
		#region Reading

		/// <summary>
		/// Reads the specified file and returns the resulting settings.
		/// </summary>
		/// <param name="fileInfo">The file info.</param>
		/// <returns></returns>
		public SettingsCollection Read(FileInfo fileInfo)
		{
			var settings = new SettingsCollection();
			Read(fileInfo, settings);
			return settings;
		}

		/// <summary>
		/// Reads the specified file and places them into the settings object.
		/// </summary>
		/// <param name="fileInfo">The file info.</param>
		/// <param name="settings">The settings.</param>
		public void Read(FileInfo fileInfo, SettingsCollection settings)
		{
			// Check for valid properties.
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}

			if (fileInfo == null)
			{
				throw new ArgumentNullException("fileInfo");
			}
		}

		#endregion

		#region Writing

		/// <summary>
		/// Writes the specified settings to the given file.
		/// </summary>
		/// <param name="fileInfo">The file info.</param>
		/// <param name="settings">The settings.</param>
		public void Write(FileInfo fileInfo, SettingsCollection settings)
		{

		}

		#endregion
	}
}