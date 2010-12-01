namespace MfGames.Logging
{
	/// <summary>
	/// Implements a logger that does nothing.
	/// </summary>
	public class NullLogger : ILogger
	{
		#region Logging

		/// <summary>
		/// Logs the given log event.
		/// </summary>
		public void Report(object sender, LogEvent logEvent)
		{
		}
		
		#endregion
	}
}
