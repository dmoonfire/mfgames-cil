#region Copyright
/*
 * Copyright (C) 2005-2008, Moonfire Games
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
 * You should have received a copy of the GNU General Public License
 * along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
#endregion

using MfGames.Utility;

/// <summary>
/// This is the primary input into the tool application. It handles
/// the various processing that use the MfGames.Utility library.
/// </summary>
public class UtilityTool
: ToolManager
{
	#region Program Entry
	/// <summary>
	/// Main entry into the application, this passes code over to the
	/// tool classes for the actual processing.
	/// </summary>
	public static void Main(string [] args)
	{
		// Create the tool and register it
		UtilityTool ut = new UtilityTool();
		ut.RegisterTools(typeof(UtilityTool).Assembly);

		// Process the results
		ut.Process(args);
	}
	#endregion
}
