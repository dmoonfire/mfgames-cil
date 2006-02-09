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

	public class AdditionDice : IDice
	{
		private IDice d1 = null;
		private IDice d2 = null;

		public AdditionDice(IDice d1, IDice d2)
		{
			this.d1 = d1;
			this.d2 = d2;
		}

		public override string ToString()
		{
			return String.Format("{0}+{1}", d1, d2);
		}

		/// <summary>
		/// This adds the results of the two dice and returns the results.
		/// </summary>
		public int Roll
		{
			get
			{
				return d1.Roll + d2.Roll;
			}
		}
	}
}
