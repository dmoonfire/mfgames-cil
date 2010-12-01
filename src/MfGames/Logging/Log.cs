#region MyRegion

using System;

using MfGames.Enumerations;

#endregion

namespace MfGames.Logging
{
	/// <summary>
	/// A wrapper around the logging object.
	/// </summary>
	public class Log
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Log"/> class.
		/// </summary>
		public Log()
			: this(null, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Log"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public Log(object context)
			: this(context, null)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Log"/> class.
		/// </summary>
		/// <param name="logger">The logger.</param>
		public Log(ILogger logger)
			: this(null, logger)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Log"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <param name="logger">The logger.</param>
		public Log(object context, ILogger logger)
		{
			this.logger = logger;
			this.context = context;
			category = Convert.ToString(context);
		}
		
		#endregion
		
		#region Logging
		
		private readonly ILogger logger;
		private readonly object context;
		private readonly string category;

		/// <summary>
		/// Reports the specified log event.
		/// </summary>
		/// <param name="logEvent">The log event.</param>
		public void Report(LogEvent logEvent)
		{
			// Get the logger to use. If one isn't set for the Log object,
			// then use the singleton version.
			ILogger currentLogger = logger;
			
			if (currentLogger == null)
			{
				currentLogger = LogManager.Instance;
			}
			
			// Log the message.
			currentLogger.Report(context ?? this, logEvent);
		}

		/// <summary>
		/// Logs an alert message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Alert(string format, params object[] arguments)
		{
			Report(new LogEvent(category, Severity.Alert, format, arguments));
		}

		/// <summary>
		/// Logs a debug message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Debug(string format, params object[] arguments)
		{
			Report(new LogEvent(category, Severity.Debug, format, arguments));
		}

		/// <summary>
		/// Logs an error message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Error(string format, params object[] arguments)
		{
			Report(new LogEvent(category, Severity.Error, format, arguments));
		}

		/// <summary>
		/// Logs a fatal message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Fatal(string format, params object[] arguments)
		{
			Report(new LogEvent(category, Severity.Fatal, format, arguments));
		}

		/// <summary>
		/// Logs an info message.
		/// </summary>
		/// <param name="format">The format.</param>
		/// <param name="arguments">The arguments.</param>
		public void Info(string format, params object[] arguments)
		{
			Report(new LogEvent(category, Severity.Info, format, arguments));
		}
				
		#endregion
	}
}
