using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MfGames.Input
{
	/// <summary>
	/// Arguments for an event where a chain has been completed by the
	/// ChainInputManager.
	/// </summary>
	public class ChainInputEventArgs
		: EventArgs
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ChainInputEventArgs"/> class.
		/// </summary>
		/// <param name="manager">The manager.</param>
		/// <param name="chain">The chain.</param>
		public ChainInputEventArgs(ChainInputManager manager, Chain chain)
		{
			// Check the arguments
			if (manager == null)
				throw new Exception("manager cannot be null");

			if (chain == null)
				throw new Exception("chain cannot be null or blank");

			// Set the values
			this.manager = manager;
			this.chain = chain;
		}
		#endregion

		#region Properties
		private readonly ChainInputManager manager;
		private readonly Chain chain;

		/// <summary>
		/// This flag determines if the system should continue to
		/// process the events. If an event sets this to false, the
		/// InputManager will cease processing.
		/// </summary>
		public bool ContinueProcessing = true;

		/// <summary>
		/// Contains the manager that triggered this event.
		/// </summary>
		public ChainInputManager Manager
		{
			get
			{
				return manager;
			}
		}

		/// <summary>
		/// Contains the input chain that triggered this event.
		/// </summary>
		public Chain Chain
		{
			get
			{
				return chain;
			}
		}
		#endregion
	}
}
