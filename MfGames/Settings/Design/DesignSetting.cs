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

namespace MfGames.Settings.Design
{
	/// <summary>
	/// Design class for settings. This contains the various properties common with
	/// all settings and is used for the design stylesheet to create a custom, typesafe
	/// class.
	/// </summary>
	public class DesignSetting : IComparable<DesignSetting>
	{
		#region Properties

		/// <summary>
		/// Contains the default value for this setting. If this is null, then
		/// there is no default.
		/// </summary>
		public string Default { get; set; }

		/// <summary>
		/// Contains the format of this setting object.
		/// </summary>
		public FormatType Format { get; set; }

		/// <summary>
		/// The group name for the settings. All settings are organized on a
		/// group/name basis where "bob/gary" and "steve/gary" are separate
		/// properties (but must be in two different setting containers).
		/// </summary>
		public string Group { get; set; }

		/// <summary>
		/// Sets or gets the name of the setting. This is the public property
		/// of the settings object, so it must be a valid C# name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Contains a string version of the type of this setting.
		/// </summary>
		public string TypeName { get; set; }

		/// <summary>
		/// This determines the usage of this setting, either a settable
		/// value or a constant.
		/// </summary>
		public UsageType Usage { get; set; }

		#endregion

		#region IComparable<DesignSetting> Members

		public int CompareTo(DesignSetting other)
		{
			return Name.CompareTo(other.Name);
		}

		#endregion
	}
}