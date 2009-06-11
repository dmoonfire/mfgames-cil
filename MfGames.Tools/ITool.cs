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

namespace MfGames.Tools
{
	/// <summary>
	/// Represents a basic tool for use in the ToolManager (and
	/// RegisterTool method).
	/// </summary>
	public interface ITool
	{
		/// <summary>
		/// Executes the service with the given parameters.
		/// </summary>
		void Process(string[] args);

		#region Properties

		/// <summary>
		/// Returns a service description. Typically this is a single phrase
		/// or sentance, with a period at the end.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Returns a list of service names that this service handles. These
		/// are the second argument of the system, which is a string name,
		/// typically dash-delimeted for words.
		/// </summary>
		string ToolName { get; }

		#endregion
	}
}