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

namespace MfGames.Utility
{
	/// <summary>
	/// Extends an AttributeTree and wraps a basic value around it,
	/// including some translation features for converting to and from
	/// various common types.
	/// </summary>
	public class ValueTree : AttributeTree
	{
		/// <summary>
		/// Methods for changing what object is used for child elements.
		/// </summary>
		public override AttributeTree CreateClone()
		{
			return new ValueTree();
		}

		#region Properties

		/// <summary>
		/// Contains the integer value or throws an exception.
		/// </summary>
		public int Int32
		{
			get { return System.Convert.ToInt32(String); }
			set { String = value.ToString(); }
		}

		/// <summary>
		/// Contains the set value, or null if no value.
		/// </summary>
		public virtual string String
		{
			get
			{
				// Check for null
				return (string) Attributes["value"];
			}

			set { Attributes["value"] = value; }
		}

		#endregion
	}
}