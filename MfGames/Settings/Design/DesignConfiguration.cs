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
using System.Collections.Generic;

#endregion

namespace MfGames.Settings.Design
{
	/// <summary>
	/// Represents the top-level configuration object for various settings.
	/// </summary>
	public class DesignConfiguration
	{
		#region Properties

		/// <summary>
		/// Contains the name of this configuration class.
		/// </summary>
		public string ClassName { get; set; }

		/// <summary>
		/// Contains the namespace to generate for a file.
		/// </summary>
		public string Namespace { get; set; }

		#endregion

		#region Groups

		private readonly List<DesignGroup> groups = new List<DesignGroup>();

		/// <summary>
		/// Retrives a design group of the given name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public DesignGroup this[string name]
		{
			get
			{
				foreach (DesignGroup group in groups)
					if (group.Name == name)
						return group;

				throw new Exception("Cannot find group: " + name);
			}
		}

		/// <summary>
		/// Contains a list of groups in this settings object.
		/// </summary>
		public List<DesignGroup> Groups
		{
			get { return groups; }
		}

		/// <summary>
		/// Returns true if there is a group with the given name.
		/// </summary>
		/// <param name="name"></param>
		/// <returns></returns>
		public bool Contains(string name)
		{
			foreach (DesignGroup group in groups)
				if (group.Name == name)
					return true;

			return false;
		}

		#endregion
	}
}