using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MfGames.Time
{
	/// <summary>
	/// Event arguments for a timing update event.
	/// </summary>
	public class UpdateEventArgs
		: EventArgs
	{
		#region Timing Properties
		private long elapsedTicks;
		private int skipped;

		/// <summary>
		/// Gets the calculated number of elapsed seconds since the last update.
		/// </summary>
		/// <value>The elapsed seconds.</value>
		public double ElapsedSeconds
		{
			get { return elapsedTicks / 10000000.0; }
		}

		/// <summary>
		/// Gets or sets the elapsed ticks since the last update.
		/// </summary>
		/// <value>The elapsed ticks.</value>
		public long ElapsedTicks
		{
			get { return elapsedTicks; }
			set { elapsedTicks = value; }
		}

		/// <summary>
		/// Gets or sets the number of skipped cycles, if there are any.
		/// </summary>
		/// <value>The skipped.</value>
		public int Skipped
		{
			get { return skipped; }
			set { skipped = value; }
		}
		#endregion
	}
}
