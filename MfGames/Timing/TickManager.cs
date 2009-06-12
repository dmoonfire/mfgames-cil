#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

#region Namespaces

using System;
using System.Threading;

using MfGames.Logging;

#endregion

namespace MfGames.Utility
{
	/// <summary>
	/// This simple class handles a single "tick" thread. It is given a
	/// certain time to sleep between the tick. On each tick, it sends a
	/// TickEvent to all delegates.
	/// </summary>
	public class TickManager
	{
		#region Thread Management

		private long lastTick = DateTime.Now.Ticks;
		private bool processing;
		private int processSkipped;
		private Thread serverThread;

		private int skippedTicks;
		private bool stopThread = true;

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
					lock (this)
					{
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
					var args = new TickArgs();
					args.Skipped = processSkipped;
					long now = DateTime.Now.Ticks;
					args.LastTick = now - lastTick;
					lastTick = now;

					// Trigger the ticker
					try
					{
						TickEvent(this, args);
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
				serverThread = new Thread(Run);
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
			catch
			{
			}

			// Make noise
			while (processing)
				Thread.Sleep(tickSpan);

			Debug("Stopped tick manager thread");
		}

		#endregion

		#region Tick Duration

		private int tickSpan = 1000;

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

		public event EventHandler<TickArgs> TickEvent;

		/// <summary>
		/// Convienance function to add an ITickable object into the tick
		/// manager.
		/// </summary>
		public void Add(ITickable tickable)
		{
			TickEvent += tickable.OnTick;
		}

		#endregion
	}
}