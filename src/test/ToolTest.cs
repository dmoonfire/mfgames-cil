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
	using NUnit.Framework;

	/// <summary>
	/// Performs the various tests on the MfGames.Utility.Tool
	/// namespace. This uses a few private classes to handle the
	/// actual filling of command arguments.
	/// </summary>
	[TestFixture] public class ToolTest
	{
#region Long Parameters
		[ExpectedException(typeof(ToolException))]
		[Test] public void TestNullArguments()
		{
			new CommandLineArguments(null);
		}

		[Test] public void TestNoArguments()
		{
			CommandLineArguments cla = new CommandLineArguments(new string [] {});
			Assert.AreEqual(0, cla.RemainingArguments.Count);
		}

		[Test] public void TestSingleArguments()
		{
			CommandLineArguments cla =
				new CommandLineArguments(new string [] {
						"arg",
					});
			Assert.AreEqual(1, cla.RemainingArguments.Count);
			Assert.AreEqual("arg", cla.RemainingArguments[0].ToString());
		}

		[Test] public void TestC1F1()
		{
			CommandLineArguments cla =
				new CommandLineArguments(new string [] {
						"--c1f1=bob",
						"gary",
					});
			cla.Scanners.Add(LongArgumentScanner.DoubleDash);

			// Verify the initial counts
			Assert.AreEqual(2, cla.RemainingArguments.Count);
			Assert.AreEqual("--c1f1=bob", cla.RemainingArguments[0].ToString());

			// Process it
			Container1 c1 = new Container1();
			cla.Process(c1);
			Assert.AreEqual(1, cla.RemainingArguments.Count,
				"Number of arguments");
			Assert.AreEqual("gary", cla.RemainingArguments[0].ToString(),
				"Next non-processed argument");
			Assert.AreEqual("bob", c1.f1,
				"Value in the container1");
		}
#endregion

#region Positional Parameters
		[Test] public void TestSinglePositional()
		{
			CommandLineArguments cla =
				new CommandLineArguments(new string [] {
						"arg",
					});

			// Pre-testing
			Assert.AreEqual(1, cla.RemainingArguments.Count);
			Assert.AreEqual("arg", cla.RemainingArguments[0].ToString());

			// Process it
			Container2 c2 = new Container2();
			cla.Process(c2);

			// Check it
			Assert.AreEqual(0, cla.RemainingArguments.Count);
			Assert.AreEqual("arg", c2.Positional1);
		}

		[ExpectedException(typeof(ToolException))]
		[Test] public void TestMissingPositional()
		{
			CommandLineArguments cla =
				new CommandLineArguments(new string [] {});

			// Pre-testing
			Assert.AreEqual(0, cla.RemainingArguments.Count);

			// Process it
			Container2 c2 = new Container2();
			cla.Process(c2);
		}

		[Test] public void TestSinglePositionalPlus()
		{
			CommandLineArguments cla =
				new CommandLineArguments(new string [] {
						"arg",
						"bob",
					});

			// Pre-testing
			Assert.AreEqual(2, cla.RemainingArguments.Count);
			Assert.AreEqual("arg", cla.RemainingArguments[0].ToString());
			Assert.AreEqual("bob", cla.RemainingArguments[1].ToString());

			// Process it
			Container2 c2 = new Container2();
			cla.Process(c2);

			// Check it
			Assert.AreEqual(1, cla.RemainingArguments.Count);
			Assert.AreEqual("bob", cla.RemainingArguments[0].ToString());
			Assert.AreEqual("arg", c2.Positional1);
		}
#endregion
	}

	/// <summary>
	/// The first of two classes used for scanning parameters.
	/// </summary>
	public class Container1
	{
		[LongArgument("c1f1", HasParameter = true)]
		public string f1 = null;

		[LongArgument("c1f2")]
		private string f2 = null;

		public string F2 { get { return f2; } }

		private string p2 = null;
		private string p1 = null;

		[LongArgument("c1p1")]
		public string P1
		{
			get { return p1; }
			set { p1 = value; }
		}

		[LongArgument("prop1")]
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
		[LongArgument("c2f1", HasParameter = true)]
		public string f1 = null;

		private string pos1 = null;

		[Positional(0, Description = "The first positional argument.",
			IsOptional = false)]
		public string Positional1
		{
			get { return pos1; }
			set { pos1 = value; }
		}
	}
}
