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

using MfGames.Enumerations;

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// Implements a low-profile logging sender for the MfGames and related libraries. To
	/// connect to the logging interface, the code just needs to attach to the Log event.
	/// </summary>
	public static class LogManager
	{
		#region Singleton

		private static ILogger logger = new NullLogger();

		/// <summary>
		/// Contains the ILogger used for the system. This is a singleton
		/// and cannot be null. Attempting to assign a null will throw an
		/// exception. Setting this to null will assign a default logger and this
		/// property will never be null.
		/// </summary>
		public static ILogger Instance
		{
			get { return logger; }
			set { logger = value ?? new NullLogger(); }
		}

		#endregion

		#region Eventing

		/// <summary>
		/// Logs the given object to the current instance logger.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="log">The log.</param>
		public static void Report(object sender, LogEvent log)
		{
			logger.Report(sender, log);
		}

		#endregion
	}
}