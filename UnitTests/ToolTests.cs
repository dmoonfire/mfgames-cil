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

using System.Collections.Generic;
using MfGames.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
	/// <summary>
	/// Performs the various tests on the MfGames.Utility.Tool
	/// namespace. This uses a few private classes to handle the
	/// actual filling of command arguments.
	/// </summary>
	[TestClass]
	public class ToolTest
	{
		#region Long Parameters
		[TestMethod]
		public void TestC1F1()
		{
			// Set up the command line
			List<string> args = new List<string>();
			args.Add("--c1f1=bob");
			args.Add("gary");

			// Create the tool manager
			ToolManager tm = new ToolManager();
			Container1 c1 = new Container1();

			// Parse the container's properties
			tm.Process(args, c1);

			// Process it
			Assert.AreEqual("bob", c1.f1);
		}
		#endregion

		#region Positional Parameters
		[TestMethod]
		public void TestSinglePositional()
		{
			// Set up the command line
			List<string> args = new List<string>();
			args.Add("arg");

			// Create the tool manager and process the container
			ToolManager tm = new ToolManager();
			Container2 c2 = new Container2();
			tm.Process(args, c2);

			// Check it
			Assert.AreEqual("arg", c2.Positional1);
		}
		#endregion
	}

	/// <summary>
	/// The first of two classes used for scanning parameters.
	/// </summary>
	public class Container1
	{
		[Optional("c1f1")]
		public string f1 = null;

		[Optional("c1f2")]
		private string f2 = null;

		public string F2 { get { return f2; } }

		private string p2 = null;
		private string p1 = null;

		[Optional("c1p1")]
		public string P1
		{
			get { return p1; }
			set { p1 = value; }
		}

		[Optional("prop1")]
		public string P2
		{
			get { return p2; }
			set { p2 = value; }
		}

		public string P3
		{
			get { return "bob"; }
		}
	}

	/// <summary>
	/// The first of two classes used for scanning parameters.
	/// </summary>
	public class Container2
	{
		[Optional("c2f1")]
		public string f1 = null;

		private string pos1 = null;

		[System.ComponentModel.Description("The first positional attribute")]
		[Positional(0)]
		public string Positional1
		{
			get { return pos1; }
			set { pos1 = value; }
		}
	}
}