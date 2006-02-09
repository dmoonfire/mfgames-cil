/*
 * Copyright (C) 2005, Moonfire Games
 *
 * This file is part of MfGames.Utility.
 *
 * The MfGames.Utility library is free software; you can redistribute
 * it and/or modify it under the terms of the GNU Lesser General
 * Public License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307
 * USA
 */

namespace MfGames.Utility
{
	using System;
	using System.Threading;

	/// <summary>
	/// This simple class handles a single "tick" thread. It is given a
	/// certain time to sleep between the tick. On each tick, it sends a
	/// TickEvent to all delegates.
	/// </summary>
	public class TickManager : Logable
	{
#region Thread Management
		private Thread serverThread = null;

		private bool stopThread = true;

		private int skippedTicks = 0;

		private int processSkipped = 0;

		private bool processing = false;

		private long lastTick = DateTime.Now.Ticks;

		/// <summary>
		/// Processes the tick server thread. This keeps track of the
		/// number of skipped ticks, to give a more accurate count or
		/// status of each tick.
		/// </summary>
		private void Run()
		{
			// Loops until the system indicates a stop
			while (!stopThread)
			{
				try
				{
					// Sleep for a little bit
					try
					{
						Thread.Sleep(tickSpan);
					}
					catch (Exception e)
					{
						Error("Cannot sleep (" + tickSpan + "): " + e);
					}
	  
					// Lock to see if we are processing
					lock (this) {
						// If we are processing, just increment it
						if (processing)
						{
							skippedTicks++;
							continue;
						}
	    
						// Process the counter
						processSkipped = skippedTicks;
						skippedTicks = 0;
						processing = true;
					}
	  
					// Perform the actual threaded tick
					RunTicker();
				}
				catch (Exception e)
				{
					Error("Exception in ticker thread: " + e);
				}
			}
		}

		/// <summary>
		/// Executes a single tick statement in a second thread.
		/// </summary>
		public void RunTicker()
		{
			try
			{
				// Execute the tick
				if (TickEvent != null)
				{
					// Create the arguments
					TickArgs args = new TickArgs();
					args.Skipped = processSkipped;
					long now = DateTime.Now.Ticks;
					args.LastTick = now - lastTick;
					lastTick = now;
	  
					// Trigger the ticker
					try
					{
						TickEvent(args);
					}
					catch (Exception e)
					{
						Error("Cannot run ticker tick: " + e);
					}
				}
	
				// Clear the flag. This is in a locked block because the testing
				// of it is also in the same lock.
				lock (this)
					processing = false;
			}
			catch (Exception e)
			{
				Error("Error in the RunTicker thread: " + e);
			}
		}

		public void Start()
		{
			try
			{
				// Prepare for the server thread
				stopThread = false;
				serverThread = new Thread(new ThreadStart(Run));
				serverThread.IsBackground = true;
				serverThread.Priority = ThreadPriority.Lowest;
				serverThread.Start();
				Debug("Started tick manager thread");
			}
			catch (Exception e)
			{
				Error("Cannot start tick manager thread", e);
			}
		}

		public void Stop()
		{
			// Mark everything to stop
			stopThread = true;

			// Join the server
			try
			{
				serverThread.Join();
				serverThread = null;
			}
			catch { }

			// Make noise
			while (processing)
				Thread.Sleep(tickSpan);

			Debug("Stopped tick manager thread");
		}
#endregion

#region Tick Duration
		private int tickSpan = 1000;

		public event TickHandler TickEvent;

		/// <summary>
		/// Convienance function to add an ITickable object into the tick
		/// manager.
		/// </summary>
		public void Add(ITickable tickable)
		{
			TickEvent += new TickHandler(tickable.OnTick);
		}

		public int TicksPerSecond
		{
			get { return 1000 / tickSpan; }
			set { tickSpan = 1000 / value; }
		}

		public int TickSpan
		{
			get { return tickSpan; }
			set
			{
				if (value < 1)
					throw new Exception("Cannot set a negative or zero sleep time");

				tickSpan = value;
			}
		}
#endregion
	}

	/// <summary>
	/// Handles the ticks as they are processed by the system.
	/// </summary>
	public delegate void TickHandler(TickArgs args);

	public class TickArgs
	{
		public int Skipped = 0;
		public long LastTick = 0;

		/// <summary>
		/// Calculates a rate, adjusted for seconds. This method takes the
		/// rate that something should happen every second and adjusts it
		/// by the amount of time since the last tick (typically less than
		/// the rate, but potential by more if there are a lot of skipped
		/// cycles).
		/// </summary>
		public int RatePerSecond(int rate)
		{
			double off = (double) LastTick / 1000000.0 * (double) rate;
			return (int) off;
		}

		/// <summary>
		/// Calculates a rate, adjusted for seconds. This method takes the
		/// rate that something should happen every second and adjusts it
		/// by the amount of time since the last tick (typically less than
		/// the rate, but potential by more if there are a lot of skipped
		/// cycles).
		/// </summary>
		public double RatePerSecond(double rate)
		{
			return (double) LastTick / 1000000.0 * (double) rate;
		}
	}
}
