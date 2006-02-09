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
	using MfGames.Utility.Dice;
	using System.IO;
	using System.Text;

	/// <summary>
	/// Factory for generating dice values from a given string. This
	/// calls the internal grammar for parsing and throws an exception
	/// if it cannot parse it.
	/// </summary>
	public class DiceFactory
	{
		/// <summary>
		/// Since this is a static singleton, hide the constructor.
		/// </summary>
		private DiceFactory() {}

		/// <summary>
		/// Parses the given format and returns the IDice object that
		/// represents that data or throws a DiceException if it
		/// cannot be parsed.
		/// </summary>
		public static IDice Parse(string format)
		{
			// Create the scanner and parser
			byte [] bytes = Encoding.UTF8.GetBytes(format);
			MemoryStream stream = new MemoryStream(bytes);
			Scanner scanner = new Scanner(stream);
			scanner.Format = format;
			Parser parser = new Parser(scanner);

			// Parse the results
			IDice dice = parser.Parse();

			// Check for errors
			if (parser.Errors.Count > 0)
			{
				throw new DiceException("Cannot parse: " + format);
			}

			// Return the dice
			return dice;
		}
	}
}
