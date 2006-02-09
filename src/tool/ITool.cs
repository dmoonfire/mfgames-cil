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
	/// <summary>
	/// Represents a basic tool for use in the ToolManager (and
	/// RegisterTool method).
	/// </summary>
	public interface ITool
	{
		/// <summary>
		/// Executes the service with the given parameters.
		/// </summary>
		void Process(string [] args);

#region Properties
		/// <summary>
		/// Returns a service description. Typically this is a single phrase
		/// or sentance, with a period at the end.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Returns a list of service names that this service handles. These
		/// are the second argument of the system, which is a string name,
		/// typically dash-delimeted for words.
		/// </summary>
		string ToolName { get; }
#endregion
	}
}
