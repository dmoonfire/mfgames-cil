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

namespace MfGames.Enumerations
{
	/// <summary>
	/// Defines the standard levels of severity used by various API.
	/// </summary>
	public enum Severity
	{
		/// <summary>
		/// Trace level, used for function tracing.
		/// </summary>
		Trace,

		/// <summary>
		/// Indicates a severity used for debugging.
		/// </summary>
		Debug,

		/// <summary>
		/// Used for informational purposes.
		/// </summary>
		Info,

		/// <summary>
		/// Used for warning notices.
		/// </summary>
		Alert,

		/// <summary>
		/// Used for a non-fatal error condition.
		/// </summary>
		Error,

		/// <summary>
		/// Used for fatal conditions, usually indicating the application or
		/// something has stopped.
		/// </summary>
		Fatal,
	}
}