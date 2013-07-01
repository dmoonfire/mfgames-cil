// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

namespace MfGames.Enumerations
{
	/// <summary>
	/// Defines the standard levels of severity used by
	/// the library.
	/// </summary>
	public enum Severity
	{
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
