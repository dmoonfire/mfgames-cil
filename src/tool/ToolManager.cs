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
	using System.Collections;
  
	/// <summary>
	/// Implements a basic tool which also includes the internal
	/// processing for help and user input.
	/// </summary>
	public class ToolManager : Logable
	{
		/// <summary>
		/// Sets up logging and prepares to use the sysetm.
		/// </summary>
		protected void Process(string [] args)
		{
			// Build up the arguments and process ourselves
			CommandLineArguments cla = new CommandLineArguments(args);
			SetArgumentParameters(cla);

			try
			{
				cla.Process(this);
			}
			catch (Exception e)
			{
				Usage(this, e.Message, e);
				return;
			}

			// Register the tools
			RegisterTools();

			// Process the command-line
			ProcessCommand(cla);
		}

		/// <summary>
		/// Allows the various settings, such as the parameter formats, to
		/// the arguments before processing.
		/// </summary>
		protected virtual void SetArgumentParameters(CommandLineArguments args)
		{
		}

#region Tools
		private Hashtable tools = new Hashtable();

		/// <summary>
		/// Process the command based on the arguments.
		/// </summary>
		private void ProcessCommand(CommandLineArguments cla)
		{
			// Check for length
			if (cla.RemainingArguments.Count == 0)
			{
				Usage(this, "A tool name is required.", null);
				return;
			}

			// Go through and get it
			string toolName = (string) cla.RemainingArguments[0];
			cla.RemainingArguments.RemoveAt(0);
			ITool tool = (ITool) tools[toolName];

			if (tool == null)
			{
				Usage(this,
					String.Format("Cannot find a tool named '{0}'.",
						toolName), null);
				return;
			}

			// Process the command, both automatically and the results
			try
			{
				cla.Process(tool);
			}
			catch (Exception e)
			{
				Usage(tool, e.Message, e);
				return;
			}

			// Execute the tool
			tool.Process((string []) cla.RemainingArguments
				.ToArray(typeof(string)));
		}

		/// <summary>
		/// Registers a single tool and its keys.
		/// </summary>
		protected void RegisterTool(ITool tool)
		{
			tools[tool.ToolName] = tool;
		}

		/// <summary>
		/// Registers the callbacks into the system by allowing the
		/// extending class to RegisterTool().
		/// </summary>
		protected virtual void RegisterTools()
		{
		}
#endregion Tools

#region Usage
		private void Usage(object container, string message, Exception exception)
		{
			// Check basic usage
			UsageFormatter uf = new UsageFormatter();
			uf.Format(container, message, exception);

			// Check for additional data
			if (container == this)
			{
				Console.WriteLine("");
				Console.WriteLine("The following tools have been registered:");
	
				ArrayList list = new ArrayList();
				list.AddRange(tools.Keys);
				list.Sort();
	
				foreach (string key in list)
				{
					ITool tool = (ITool) tools[key];
	  
					Console.WriteLine("  {0} - {1}", key, tool.Description);
				}
			}

			// Set the code
			Environment.Exit(1);
		}
#endregion
	}
}
