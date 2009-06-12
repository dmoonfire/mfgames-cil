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
	/// Represents an input chain, which consists of one or more links (or
	/// collection of inputs).
	/// </summary>
	public class Chain : List<ChainLink>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Chain"/> class.
		/// </summary>
		public Chain()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Chain"/> class.
		/// </summary>
		/// <param name="links">The links.</param>
		public Chain(params ChainLink[] links)
		{
			AddRange(links);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Chain"/> class.
		/// </summary>
		/// <param name="list">The list.</param>
		public Chain(IEnumerable<ChainLink> list)
		{
			AddRange(list);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Chain"/> class by
		/// creating a single-link chain with the given inputs.
		/// </summary>
		/// <param name="inputs">The inputs.</param>
		public Chain(params string[] inputs)
		{
			Add(new ChainLink(inputs));
		}

		#endregion

		#region Chain Operations

		/// <summary>
		/// Creates a subchain from the given index to the end.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public Chain Subchain(int index)
		{
			return new Chain(GetRange(index, Count - index));
		}

		#endregion
	}
}