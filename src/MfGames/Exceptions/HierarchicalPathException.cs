// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;

namespace MfGames.Exceptions
{
	/// <summary>
	/// Represents an exception while processing HierarchialPath objects.
	/// </summary>
	public class HierarchicalPathException: ApplicationException
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPathException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		public HierarchicalPathException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="HierarchicalPathException"/> class.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="exception">The exception.</param>
		public HierarchicalPathException(
			string message,
			Exception exception)
			: base(message, exception)
		{
		}

		#endregion
	}
}
