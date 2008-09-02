using System;

namespace MfGames.Input
{
	/// <summary>
	/// Creates a processing event args which contains an associated
	/// InputManager, token, and a flag for continued processing.
	/// </summary>
	public class InputEventArgs
	: EventArgs
	{
		#region Constructors
		public InputEventArgs(InputManager manager, string token)
		{
			// Check the arguments
			if (manager == null)
				throw new Exception("manager cannot be null");

			if (String.IsNullOrEmpty(token))
				throw new Exception("token cannot be null or blank");

			// Set the values
			this.manager = manager;
			this.token = token;
		}
		#endregion

		#region Properties
		private InputManager manager;
		private string token;

		/// <summary>
		/// This flag determines if the system should continue to
		/// process the events. If an event sets this to false, the
		/// InputManager will cease processing.
		/// </summary>
		public bool ContinueProcessing = true;

		/// <summary>
		/// Contains the manager that triggered this event.
		/// </summary>
		public InputManager Manager
		{
			get { return manager; }
		}

		/// <summary>
		/// Contains a read-only token which triggered this event.
		/// </summary>
		public string Token
		{
			get { return token; }
		}
		#endregion
	}
}
