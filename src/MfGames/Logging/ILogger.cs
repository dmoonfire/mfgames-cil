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
		/// Reports the specified log message, formatting it as needed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="severity">The severity.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		void Report(object sender, Severity severity, Exception exception, string format, params object[] arguments);
	}
}