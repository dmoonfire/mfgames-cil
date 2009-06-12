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

namespace MfGames.Input
{
	/// <summary>
	/// Creates a processing event args which contains an associated
	/// InputManager, token, and a flag for continued processing.
	/// </summary>
	public class InputEventArgs : EventArgs
	{
		#region Constructors

		public InputEventArgs(InputManager manager, string token)
		{
			// Check the arguments
			if (manager == null)
				throw new Exception("manager cannot be null");

			if (String.IsNullOrEmpty(token))
				throw new Exception("token cannot be null or blank");

			// Set the values
			this.manager = manager;
			this.token = token;
		}

		#endregion

		#region Properties

		/// <summary>
		/// This flag determines if the system should continue to
		/// process the events. If an event sets this to false, the
		/// InputManager will cease processing.
		/// </summary>
		public bool ContinueProcessing = true;

		private InputManager manager;
		private string token;

		/// <summary>
		/// Contains the manager that triggered this event.
		/// </summary>
		public InputManager Manager
		{
			get { return manager; }
		}

		/// <summary>
		/// Contains a read-only token which triggered this event.
		/// </summary>
		public string Token
		{
			get { return token; }
		}

		#endregion
	}
}