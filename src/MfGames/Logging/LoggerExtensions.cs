#region Namespaces

using System;

using MfGames.Enumerations;

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// Defines common extensions for log messages.
	/// </summary>
	public static class LoggerExtensions
	{
		/// <summary>
		/// Reports a message to the specified logger.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="severity">The severity.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Report(
			this ILogger logger,
			object sender,
			Severity severity,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, severity, null, format, arguments);
		}

		#region Alert

		/// <summary>
		/// Posts an alert logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Alert(
			this ILogger logger,
			object sender,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Alert, null, format, arguments);
		}

		/// <summary>
		/// Posts an alert logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Alert(
			this ILogger logger,
			object sender,
			Exception exception,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Alert, exception, format, arguments);
		}

		#endregion

		#region Debug

		/// <summary>
		/// Posts a debug logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Debug(
			this ILogger logger,
			object sender,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Debug, null, format, arguments);
		}

		/// <summary>
		/// Posts a debug logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Debug(
			this ILogger logger,
			object sender,
			Exception exception,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Debug, exception, format, arguments);
		}

		#endregion

		#region Error

		/// <summary>
		/// Posts an error logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Error(
			this ILogger logger,
			object sender,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Error, null, format, arguments);
		}

		/// <summary>
		/// Posts an error logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Error(
			this ILogger logger,
			object sender,
			Exception exception,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Error, exception, format, arguments);
		}

		#endregion

		#region Fatal

		/// <summary>
		/// Posts a fatal logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Fatal(
			this ILogger logger,
			object sender,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Fatal, null, format, arguments);
		}

		/// <summary>
		/// Posts a fatal logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Fatal(
			this ILogger logger,
			object sender,
			Exception exception,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Fatal, exception, format, arguments);
		}

		#endregion

		#region Info

		/// <summary>
		/// Posts an info logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Info(
			this ILogger logger,
			object sender,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Info, null, format, arguments);
		}

		/// <summary>
		/// Posts an info logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Info(
			this ILogger logger,
			object sender,
			Exception exception,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Info, exception, format, arguments);
		}

		#endregion

		#region Trace

		/// <summary>
		/// Posts a trace logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Trace(
			this ILogger logger,
			object sender,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Trace, null, format, arguments);
		}

		/// <summary>
		/// Posts a trace logging mesage.
		/// </summary>
		/// <param name="logger">The logger.</param>
		/// <param name="sender">The sender.</param>
		/// <param name="exception">The exception.</param>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public static void Trace(
			this ILogger logger,
			object sender,
			Exception exception,
			string format,
			params object[] arguments)
		{
			logger.Report(sender, Severity.Trace, exception, format, arguments);
		}

		#endregion
	}
}