// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;

namespace MfGames.Exceptions
{
	/// <summary>
	/// Indicates that the given path does not conform to the format
	/// required by the system. This should be an indicator of invalid
	/// characters or something of that manner.
	/// </summary>
	public class InvalidPathException: HierarchicalPathException
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
		public InvalidPathException(
			string msg,
			Exception e)
			: base(msg, e)
		{
		}

		#endregion
	}
}
