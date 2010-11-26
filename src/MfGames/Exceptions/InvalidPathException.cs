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

namespace MfGames.Exceptions
{
	/// <summary>
	/// Indicates that the given path does not conform to the format
	/// required by the system. This should be an indicator of invalid
	/// characters or something of that manner.
	/// </summary>
	public class InvalidPathException : Exception
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidPathException"/> class.
		/// </summary>
		/// <param name="msg">The MSG.</param>
		public InvalidPathException(string msg)
			: base(msg)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="InvalidPathException"/> class.
		/// </summary>
		/// <param name="msg">The MSG.</param>
		/// <param name="e">The e.</param>
		public InvalidPathException(string msg, Exception e)
			: base(msg, e)
		{
		}

		#endregion
	}
}