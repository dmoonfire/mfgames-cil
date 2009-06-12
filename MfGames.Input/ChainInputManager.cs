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
using System.Collections.Generic;

#endregion

namespace MfGames.Input
{
	/// <summary>
	/// This manager extends the functionality of the InputManager to
	/// handle chained input events. This idea comes from Emacs, but
	/// it implements a design pattern where the application registers
	/// a series of commands, such as "C-x C-c" (control x, control c)
	/// with a specific command. These chains can be singular, such as
	/// "C-s" for the typical Save command or "C-S-s" for the Save As
	/// keyboard command.
	/// </summary>
	public class ChainInputManager : InputManager
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ChainInputManager"/> class.
		/// </summary>
		public ChainInputManager()
		{
			// Set up the current tree.
			currentTree = rootTree;

			// Add the default non-reset tokens.
			nonResetTokens.Add(InputTokens.RightControl);
			nonResetTokens.Add(InputTokens.LeftControl);
			nonResetTokens.Add(InputTokens.Control);
			nonResetTokens.Add(InputTokens.RightShift);
			nonResetTokens.Add(InputTokens.LeftShift);
			nonResetTokens.Add(InputTokens.Shift);
		}

		#endregion Constructors

		#region Processing

		/// <summary>
		/// Processes the activate input event.
		/// </summary>
		/// <param name="token">The token.</param>
		/// <param name="fireEvent">if set to <c>true</c> [fire event].</param>
		protected override void ProcessActivateInput(string token, bool fireEvent)
		{
			// Get a hash of all the currently activated inputs. Then, go through all
			// of the child chain link trees of the current tree and see if any
			// are completely fulfilled.
			HashSet<string> currentInputs = ActivatedInputs;

			foreach (ChainLinkTree childTree in currentTree)
			{
				// See if we can be activated.
				if (childTree.CanActivate(currentInputs))
				{
					// Activate any inputs.
					childTree.Activate(this);

					// See if we need to move down the chain.
					if (childTree.Count > 0)
						// Move down the input tree.
						currentTree = childTree;
					else
						// Reset the tree to the top.
						currentTree = rootTree;

					// We processed something, so stop entirely.
					return;
				}
			}

			// See if we have a reset input token, which causes the root tree to
			// activate. We use this by checking for non-reset tokens.
			if (currentTree != rootTree)
			{
				foreach (string input in currentInputs)
				{
					if (!nonResetTokens.Contains(input))
					{
						// This is a reset token.
						currentTree = rootTree;
					}
				}
			}
		}

		#endregion Processing

		#region Chains

		private readonly ChainLinkTree rootTree = new ChainLinkTree();
		private ChainLinkTree currentTree;

		/// <summary>
		/// Registers the specified chain with a given callback.
		/// </summary>
		/// <param name="chain">The chain.</param>
		/// <param name="callback">The callback.</param>
		public void Register(Chain chain, EventHandler<ChainInputEventArgs> callback)
		{
			// Register the chain with the root-level chain.
			Register(rootTree, chain, 0, callback);
		}

		/// <summary>
		/// Registers the specified tree.
		/// </summary>
		/// <param name="tree">The tree.</param>
		/// <param name="chain">The chain.</param>
		/// <param name="index">The index.</param>
		/// <param name="callback">The callback.</param>
		private static void Register(
			ChainLinkTree tree,
			Chain chain,
			int index,
			EventHandler<ChainInputEventArgs> callback)
		{
			// Grab the appropriate chains.
			ChainLink link = chain[index];

			// Get (or create) a link tree for the current chain.
			if (!tree.Contains(link))
				tree.Add(new ChainLinkTree(new Chain(chain.GetRange(0, index + 1)), link));

			ChainLinkTree childTree = tree[link];

			// See if we are at the end of the chain or if we need to keep moving down.
			if (chain.Count == index + 1)
			{
				// Register the callback since we are done processing.
				childTree.Callbacks.Add(callback);
			}
			else
			{
				// Keep moving down the three.
				Register(childTree, chain, index + 1, callback);
			}
		}

		#endregion

		#region Reset Processing

		private readonly HashSet<string> nonResetTokens = new HashSet<string>();

		/// <summary>
		/// Gets the tokens that do not reset the inputs.
		/// </summary>
		/// <value>The non reset tokens.</value>
		public HashSet<string> NonResetTokens
		{
			get { return nonResetTokens; }
		}

		#endregion Reset Processing

		#region Inner Class - ChainLinkTree

		/// <summary>
		/// Encapsulates the code needed to handle chained inputs. Each tree
		/// is a parent of child trees and may have a list of delegates
		/// listening to each level.
		/// </summary>
		private class ChainLinkTree : List<ChainLinkTree>
		{
			#region Constructors

			/// <summary>
			/// Initializes a new instance of the <see cref="ChainLinkTree"/> class.
			/// </summary>
			public ChainLinkTree()
			{
			}

			/// <summary>
			/// Initializes a new instance of the <see cref="ChainLinkTree"/> class.
			/// </summary>
			/// <param name="chain">The chain.</param>
			/// <param name="chainLink">The chain link.</param>
			public ChainLinkTree(Chain chain, ChainLink chainLink)
			{
				this.chain = chain;
				this.chainLink = chainLink;
			}

			#endregion

			#region List Operations

			public ChainLinkTree this[ChainLink link]
			{
				get
				{
					foreach (ChainLinkTree tree in this)
						if (tree.chainLink == link)
							return tree;

					throw new Exception("Cannot find the given tree: " + link);
				}
			}

			/// <summary>
			/// Determines whether this list contains the specified chain link.
			/// </summary>
			/// <param name="link">The link.</param>
			/// <returns>
			///     <c>true</c> if [contains] [the specified chain link]; otherwise, <c>false</c>.
			/// </returns>
			public bool Contains(ChainLink link)
			{
				foreach (ChainLinkTree tree in this)
					if (tree.chainLink == link)
						return true;

				return false;
			}

			#endregion List Operations

			#region Link Processing

			private readonly Chain chain;
			private readonly ChainLink chainLink;

			/// <summary>
			/// Determines whether this chain can be activated with the set of given
			/// inputs.
			/// </summary>
			/// <param name="inputs">The inputs.</param>
			/// <returns>
			///     <c>true</c> if this instance can activate the specified inputs; otherwise, <c>false</c>.
			/// </returns>
			public bool CanActivate(ICollection<string> inputs)
			{
				// Empty chains are never activatable.
				if (chainLink == null || chainLink.Count == 0)
					return false;

				// Go through each of the link's inputs.
				foreach (string input in chainLink)
					if (!inputs.Contains(input))
						return false;

				// We can be activated because everything matched.
				return true;
			}

			#endregion

			#region Callbacks

			private readonly List<EventHandler<ChainInputEventArgs>> callbacks =
				new List<EventHandler<ChainInputEventArgs>>();

			/// <summary>
			/// Gets the list of callbacks for this tree.
			/// </summary>
			/// <value>The callbacks.</value>
			public List<EventHandler<ChainInputEventArgs>> Callbacks
			{
				get { return callbacks; }
			}

			/// <summary>
			/// Activates any listening delegates on this tree.
			/// </summary>
			public void Activate(ChainInputManager manager)
			{
				// Call the events until one indicates we should stop processing.
				var args = new ChainInputEventArgs(manager, chain);

				foreach (EventHandler<ChainInputEventArgs> callback in callbacks)
				{
					// Call the event delegate.
					callback(manager, args);

					// See if we should stop processing.
					if (!args.ContinueProcessing)
						return;
				}
			}

			#endregion
		}

		#endregion
	}
}