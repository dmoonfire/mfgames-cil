using MfGames.Utility;
using System;
using System.Collections.Generic;
using MfGames.Logging;

namespace MfGames.Input
{
	/// <summary>
	/// The input manager is a centralized manager object that handles
	/// the capture and processing of various input tokens. Input
	/// tokens are generically a string that identifies the character
	/// pressed. In most cases, single character tokens, such as the
	/// keys on the keyboard, are identified as their lower-case
	/// analog, such as "x". Other tokens are used for special keys,
	/// such as the function keys ("F1", always in upper case), return
	/// ("RET"), or mouse buttons ("MB1"). Special tokens are assigned
	/// to the control key ("C"), shift ("S"), and alt key ("A").
	///
	/// There is a provision to allow a key duplication to fire
	/// multiple events. For example, this lets you assign both right
	/// control ("CR") and left control ("CL") to also activate the
	/// control ("C") input.
	///
	/// This class processes the input tokens in multiple
	/// methodologies. The most basic of this processing is the
	/// handling of InputActivated and InputDeactivated events which
	/// correspond to KeyDown/KeyUp,
	/// MouseButtonDown/MouseButtonUp. Furthermore, the InputManager
	/// provides a IsActivated() method which returns true if a specific
	/// input is activated.
	///
	/// Eventing is linearaly processed, enabling a listener for the
	/// given event halt processing further down the chain. This
	/// allows for the capture of input tokens.
	///
	/// While this class is typically used as a singleton, there is no
	/// functionality in this class to ensure singleton processing.
	/// </summary>
	public class InputManager
	{
		#region Enabling/Disabling
		private Dictionary<string,int> activatedCounter =
			new Dictionary<string,int>();

		/// <summary>
		/// This event is used when an input is
		/// activated. Reactivating an input more than once will not
		/// trigger multiple events unless the DuplicatedEvents flag
		/// is set.
		/// </summary>
		public event EventHandler<InputEventArgs> InputActivated;

		/// <summary>
		/// This event is used when an input is
		/// activated. Reactivating an input more than once will not
		/// trigger multiple events unless the DuplicatedEvents flag
		/// is set.
		/// </summary>
		public event EventHandler<InputEventArgs> InputDeactivated;

		/// <summary>
		/// If this is set to true, then multiple requests for the
		/// same input will repeatedly trigger the various events.
		/// </summary>
		public bool DuplicateEvents = false;

		/// <summary>
		/// Triggers the processing for activating an input. This is
		/// the equivalent of the various key, button, and joystick
		/// down functions.
		/// </summary>
		public void ActivateInput(string token)
		{
			// Sanity checking on input token
			if (String.IsNullOrEmpty(token))
				throw new NullReferenceException(
					"token cannot be null or empty");

			// Add the index for activation counter
			bool fireEvent = true;

			if (activatedCounter.ContainsKey(token))
			{
				// Just increment the counter
				activatedCounter[token]++;

				// Only fire the event if we want duplicates
				fireEvent = DuplicateEvents;
			}
			else
			{
				// Set the initial counter
				activatedCounter.Add(token, 1);
			}

			// Fire the various events for activation
			if (fireEvent && InputActivated != null)
			{
				// Create the arguments
				InputEventArgs args = new InputEventArgs(this, token);

				// Loop through the delegates
				foreach (Delegate d in InputActivated.GetInvocationList())
				{
					// Invoke this one
                    d.DynamicInvoke(this, args);

					// See if we are done processing
					if (!args.ContinueProcessing)
						break;
				}
			}

			// See if we have a token mapping. We do this after all
			// the events to have a clearly defined order of
			// activations.
			if (tokenMapping != null &&
				tokenMapping.ContainsKey(token))
			{
				// Activate this for all the mapped tokens
				foreach (string mappedToken in tokenMapping[token])
					ActivateInput(mappedToken);
			}

			// See if we are automatically collapsing
			if (AutoCollapseTokens &&
				token.LastIndexOf(":") > 0)
			{
				// Get the new token
				string newToken = token.Substring(0, token.LastIndexOf(":"));
				ActivateInput(newToken);
			}
		}

		/// <summary>
		/// Triggers the processing for deactivating inputs from the
		/// system. This is the equivalent of the key or button up
		/// functions used in various libraries.
		/// </summary>
		public void DeactivateInput(string token)
		{
			// Sanity checking on input token
			if (String.IsNullOrEmpty(token))
				throw new NullReferenceException(
					"token cannot be null or empty");

			// Add the index for activation counter
			bool fireEvent = false;

			if (activatedCounter.ContainsKey(token))
			{
				// Just decrement the counter
				activatedCounter[token]--;

				// See if we are done with this input
				if (activatedCounter[token] == 0)
				{
					// We fire the event
					fireEvent = true;

					// Remove it
					activatedCounter.Remove(token);
				}
				else
				{
					// We see if we want to see every change
					fireEvent = DuplicateEvents;
				}
			}

			// Fire the various events for activation
			if (fireEvent && InputDeactivated != null)
			{
				// Create the arguments
				InputEventArgs args = new InputEventArgs(this, token);

				// Loop through the delegates
				foreach (Delegate d in InputDeactivated.GetInvocationList())
				{
					// Invoke this one
					d.DynamicInvoke(this, args);

					// See if we are done processing
					if (!args.ContinueProcessing)
						break;
				}
			}

			// See if we have a token mapping. We do this after all
			// the events to have a clearly defined order of
			// activations.
			if (tokenMapping != null &&
				tokenMapping.ContainsKey(token))
			{
				// Activate this for all the mapped tokens
				foreach (string mappedToken in tokenMapping[token])
					DeactivateInput(mappedToken);
			}

			// See if we are automatically collapsing
			if (AutoCollapseTokens &&
				token.LastIndexOf(":") > 0)
			{
				// Get the new token
				string newToken = token.Substring(0, token.LastIndexOf(":"));
				DeactivateInput(newToken);
			}
		}

		/// <summary>
		/// This returns true if the given token has been activated in
		/// this manager.
		/// </summary>
		public bool IsActivated(string token)
		{
			// Sanity checking on input token
			if (String.IsNullOrEmpty(token))
				throw new NullReferenceException(
					"token cannot be null or empty");

			// See if we have it
			return activatedCounter.ContainsKey(token);
		}
		#endregion

		#region Input Mapping
		private TokenMappingDictionary tokenMapping;

		/// <summary>
		/// If this is set to true, then the standard tokens (such as
		/// "CONTROL:RIGHT") will be collapsed down to their left-most
		/// elements (such as "CONTROL:RIGHT" collapsing down to
		/// "CONTROL"). If there are multiple colons, each one will
		/// collapse down to the next level ("A:B:C" collapses to
		/// "A:B" collapses to "A").
		/// </summary>
		public bool AutoCollapseTokens = false;

		/// <summary>
		/// Creates a token mapping object for this input. If this is
		/// null, then input token mapping will be
		/// performed. Otherwise, no such processing will take place.
		/// </summary>
		public TokenMappingDictionary TokenMapping
		{
			get { return tokenMapping; }
			set { tokenMapping = value; }
		}
		#endregion

		#region Logging
		private Log log;
		
		/// <summary>
		/// Contains the lazy-loaded logging context for this
		/// object. This ties into the generic logging system from
		/// MfGames.Utility.Logger.
		/// </summary>
		public Log Log
		{
			get
			{
				if (log == null)
					log = new Log(this);

				return log;
			}
		}
		#endregion
	}
}
