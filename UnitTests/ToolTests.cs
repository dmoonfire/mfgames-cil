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

using System.Collections.Generic;

using MfGames.Tools;

using NUnit.Framework;

#endregion

namespace UnitTests
{
	/// <summary>
	/// Performs the various tests on the MfGames.Utility.Tool
	/// namespace. This uses a few private classes to handle the
	/// actual filling of command arguments.
	/// </summary>
	[TestFixture]
	public class ToolTest
	{
		[Test]
		public void TestC1F1()
		{
			// Set up the command line
			var args = new List<string>();
			args.Add("--c1f1=bob");
			args.Add("gary");

			// Create the tool manager
			var tm = new ToolManager();
			var c1 = new Container1();

			// Parse the container's properties
			tm.Process(args, c1);

			// Process it
			Assert.AreEqual("bob", c1.f1);
		}

		[Test]
		public void TestSinglePositional()
		{
			// Set up the command line
			var args = new List<string>();
			args.Add("arg");

			// Create the tool manager and process the container
			var tm = new ToolManager();
			var c2 = new Container2();
			tm.Process(args, c2);

			// Check it
			Assert.AreEqual("arg", c2.Positional1);
		}
	}

	/// <summary>
	/// The first of two classes used for scanning parameters.
	/// </summary>
	public class Container1
	{
		[Optional("c1f1")]
		public string f1;

		[Optional("c1f2")]
		private string f2;

		public string F2
		{
			get { return f2; }
		}

		[Optional("c1p1")]
		public string P1 { get; set; }

		[Optional("prop1")]
		public string P2 { get; set; }

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
		public string f1;

		[System.ComponentModel.Description("The first positional attribute")]
		[Positional(0)]
		public string Positional1 { get; set; }
	}
}