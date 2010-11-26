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
		
		public Log()
			: this(null, null)
		{
		}
		
		public Log(object context)
			: this(context, null)
		{
		}
		
		public Log(ILogger logger)
			: this(null, logger)
		{
		}
		
		public Log(object context, ILogger logger)
		{
			this.logger = logger;
			this.context = context;
			this.category = Convert.ToString(context);
		}
		
		#endregion
		
		#region Logging
		
		private ILogger logger;
		private object context;
		private string category;
		
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

		public void Alert(string format, params object[] arguments)
		{
			Report(new LogEvent(category, Severity.Alert, format, arguments));
		}
		
		public void Debug(string format, params object[] arguments)
		{
			Report(new LogEvent(category, Severity.Debug, format, arguments));
		}
		
		public void Error(string format, params object[] arguments)
		{
			Report(new LogEvent(category, Severity.Error, format, arguments));
		}
		
		public void Fatal(string format, params object[] arguments)
		{
			Report(new LogEvent(category, Severity.Fatal, format, arguments));
		}

		public void Info(string format, params object[] arguments)
		{
			Report(new LogEvent(category, Severity.Info, format, arguments));
		}
				
		#endregion
	}
}
