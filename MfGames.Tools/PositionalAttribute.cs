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

namespace MfGames.Tools
{
	/// <summary>
	/// Represents a positional attribute which is used after the long
	/// and short arguments are processed. This will not match an
	/// argument attribute (based on the regexes).
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
		AllowMultiple = false, Inherited = true)]
	public class PositionalAttribute : Attribute
	{
		/// <summary>
		/// Constructs an empty positional attribute.
		/// </summary>
		public PositionalAttribute()
		{
		}

		/// <summary>
		/// Constructs the positional attribute with the given index.
		/// </summary>
		public PositionalAttribute(int index)
		{
			this.index = index;
		}

		#region Properties

		private string description = null;
		private int index = 0;
		private string name = null;
		private bool optional = false;

		/// <summary>
		/// Contains a short, hopefully one-line description which is used
		/// for the usage functionality.
		/// </summary>
		public string Description
		{
			get { return description; }
			set { description = value; }
		}

		/// <summary>
		/// Contains the zero-based index for the position. A position 0
		/// attribute will get the first one, the position 1 gets the
		/// next, etc.
		/// </summary>
		public int Index
		{
			get { return index; }
			set { index = value; }
		}

		/// <summary>
		/// Indicates of a parameter is optional. This defaults to false.
		/// </summary>
		public bool IsOptional
		{
			get { return optional; }
			set { optional = value; }
		}

		/// <summary>
		/// Contains the name of the positional attribute, which defaults
		/// to "argX" where X is the Index property.
		/// </summary>
		public string Name
		{
			get
			{
				if (name == null)
					return "arg" + Index;
				else
					return name;
			}

			set { name = value; }
		}

		#endregion
	}
}