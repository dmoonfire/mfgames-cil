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

namespace MfGames.Time
{
	/// <summary>
	/// Event arguments for a timing update event.
	/// </summary>
	public class UpdateEventArgs : EventArgs
	{
		#region Timing Properties

		private long elapsedTicks;

		/// <summary>
		/// Gets the calculated number of elapsed seconds since the last update.
		/// </summary>
		/// <value>The elapsed seconds.</value>
		public double ElapsedSeconds
		{
			get { return elapsedTicks / 10000000.0; }
		}

		/// <summary>
		/// Gets or sets the elapsed ticks since the last update.
		/// </summary>
		/// <value>The elapsed ticks.</value>
		public long ElapsedTicks
		{
			get { return elapsedTicks; }
			set { elapsedTicks = value; }
		}

		/// <summary>
		/// Gets or sets the number of skipped cycles, if there are any.
		/// </summary>
		/// <value>The skipped.</value>
		public int Skipped { get; set; }

		#endregion
	}
}