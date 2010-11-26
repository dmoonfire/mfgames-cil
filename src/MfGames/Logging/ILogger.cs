#region Namespaces

using System;

using MfGames.Enumerations;

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// Defines a common logger signature that allows for a flexible reporting
	/// of log messages.
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Logs the given log event.
		/// </summary>
		void Report(object sender, LogEvent logEvent);
	}
}