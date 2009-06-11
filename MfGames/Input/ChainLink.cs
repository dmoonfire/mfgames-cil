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

using System.Collections.Generic;

#endregion

namespace MfGames.Input
{
	/// <summary>
	/// Represents a single set of elements in a single link of the
	/// input chain.
	/// </summary>
	public class ChainLink : HashSet<string>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ChainLink"/> class.
		/// </summary>
		public ChainLink()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ChainLink"/> class.
		/// </summary>
		/// <param name="inputs">The inputs.</param>
		public ChainLink(params string[] inputs)
		{
			foreach (string input in inputs)
				Add(input);
		}

		#endregion Constructors
	}
}