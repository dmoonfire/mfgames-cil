// <copyright file="MacroExpansionTests.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using MfGames.Text;

using NUnit.Framework;

namespace UnitTests.Text
{
	/// <summary>
	/// Tests the functionality of the MacroExpansion class.
	/// </summary>
	[TestFixture]
	public class MacroExpansionTests
	{
		#region Public Methods and Operators

		/// <summary>
		/// Verifies that the expansion on a blank string is a blank string.
		/// </summary>
		[Test]
		public void BlankString()
		{
			// Set up the input.
			string format = string.Empty;
			var macros = new Dictionary<string, object>
			{
				{ "MacroA", "Value A" }
			};

			// Perform the expansion.
			var expansion = new MacroExpansion(format);
			string results = expansion.Expand(macros);

			// Verify the results.
			Assert.AreEqual(
				string.Empty,
				results);
		}

		/// <summary>
		/// Verifies using different separators but with the old macro delimiters.
		/// </summary>
		[Test]
		public void DifferentSeparatorsNewMacros()
		{
			// Set up the input.
			var format = "{MacroA}";
			var macros = new Dictionary<string, object>
			{
				{ "MacroA", "Value A" }
			};

			// Perform the expansion.
			var expansion = new MacroExpansion(format, "{", "}");
			string results = expansion.Expand(macros);

			// Verify the results.
			Assert.AreEqual(
				"Value A",
				results);
		}

		/// <summary>
		/// Verifies using different separators but with the old macro delimiters.
		/// </summary>
		[Test]
		public void DifferentSeparatorsOldMacros()
		{
			// Set up the input.
			var format = "$(MacroA)";
			var macros = new Dictionary<string, object>
			{
				{ "MacroA", "Value A" }
			};

			// Perform the expansion.
			var expansion = new MacroExpansion(format, "{", "}");
			string results = expansion.Expand(macros);

			// Verify the results.
			Assert.AreEqual(
				"$(MacroA)",
				results);
		}

		/// <summary>
		/// Verifies formatting of integer values.
		/// </summary>
		[Test]
		public void FormattedIntegerExpansion()
		{
			// Set up the input.
			var format = "$(MacroA:D4)";
			var macros = new Dictionary<string, object>
			{
				{ "MacroA", 13 }
			};

			// Perform the expansion.
			var expansion = new MacroExpansion(format);
			string results = expansion.Expand(macros);

			// Verify the results.
			Assert.AreEqual(
				"0013",
				results);
		}

		/// <summary>
		/// Verifies formatting of integer values.
		/// </summary>
		[Test]
		public void FormattedIntegerExpansionNonPadded()
		{
			// Set up the input.
			var format = "$(MacroA:d)";
			var macros = new Dictionary<string, object>
			{
				{ "MacroA", 13 }
			};

			// Perform the expansion.
			var expansion = new MacroExpansion(format);
			string results = expansion.Expand(macros);

			// Verify the results.
			Assert.AreEqual(
				"13",
				results);
		}

		[Test]
		public void GetRegexForAlpha3()
		{
			// Pull out the regular expression for a given format.
			var format = "$(MacroA:S3)";
			var expansion = new MacroExpansion(format);

			Regex regex = expansion.GetRegex();

			// Verify the results.
			Assert.AreEqual(
				@"^(\w{3})$",
				regex.ToString());
		}

		[Test]
		public void GetRegexForAlpha3OrLess()
		{
			// Pull out the regular expression for a given format.
			var format = "$(MacroA:S,3)";
			var expansion = new MacroExpansion(format);

			Regex regex = expansion.GetRegex();

			// Verify the results.
			Assert.AreEqual(
				@"^(\w{,3})$",
				regex.ToString());
		}

		[Test]
		public void GetRegexForAlpha3OrMore()
		{
			// Pull out the regular expression for a given format.
			var format = "$(MacroA:S3,)";
			var expansion = new MacroExpansion(format);

			Regex regex = expansion.GetRegex();

			// Verify the results.
			Assert.AreEqual(
				@"^(\w{3,})$",
				regex.ToString());
		}

		[Test]
		public void GetRegexForAlpha3To5()
		{
			// Pull out the regular expression for a given format.
			var format = "$(MacroA:S3,5)";
			var expansion = new MacroExpansion(format);

			Regex regex = expansion.GetRegex();

			// Verify the results.
			Assert.AreEqual(
				@"^(\w{3,5})$",
				regex.ToString());
		}

		/// <summary>
		/// Tests that a macro with an integer format will parse correctly.
		/// </summary>
		[Test]
		public void GetRegexForComplexPattern()
		{
			// Pull out the regular expression for a given format.
			var format = "/test/section-$(MacroA:D4)/$(MacroB:D2).txt";
			var expansion = new MacroExpansion(format);

			Regex regex = expansion.GetRegex();

			// Verify the results.
			Assert.AreEqual(
				@"^/test/section-(\d\d\d\d)/(\d\d)\.txt$",
				regex.ToString());
		}

		/// <summary>
		/// Tests that a macro with an integer format will parse correctly.
		/// </summary>
		[Test]
		public void GetRegexForSimpleInteger()
		{
			// Pull out the regular expression for a given format.
			var format = "$(MacroA:D4)";
			var expansion = new MacroExpansion(format);

			Regex regex = expansion.GetRegex();

			// Verify the results.
			Assert.AreEqual(
				@"^(\d\d\d\d)$",
				regex.ToString());
		}

		/// <summary>
		/// Tests that a macro with an integer format will parse correctly.
		/// </summary>
		[Test]
		public void GetRegexForSimpleIntegerNonPadded()
		{
			// Pull out the regular expression for a given format.
			var format = "$(MacroA:G0)";
			var expansion = new MacroExpansion(format);

			Regex regex = expansion.GetRegex();

			// Verify the results.
			Assert.AreEqual(
				@"^(\d+)$",
				regex.ToString());
		}

		/// <summary>
		/// Tests that giving a macro without any formatting information throws
		/// an exception.
		/// </summary>
		[Test]
		public void GetRegexThrowsWithOneSubstitionWithoutFormatting()
		{
			// Pull out the regular expression for a given format.
			var format = "$(MacroA:D2) $(MacroA)";
			var expansion = new MacroExpansion(format);

			// Verify that it throws an exception.
			Assert.Throws<InvalidOperationException>(
				() => expansion.GetRegex());
		}

		/// <summary>
		/// Tests that giving a macro without any formatting information throws
		/// an exception.
		/// </summary>
		[Test]
		public void GetRegexThrowsWithoutFormatting()
		{
			// Pull out the regular expression for a given format.
			var format = "$(MacroA)";
			var expansion = new MacroExpansion(format);

			// Verify that it throws an exception.
			Assert.Throws<InvalidOperationException>(
				() => expansion.GetRegex());
		}

		/// <summary>
		/// Verifies expansion with text before and after the macro.
		/// </summary>
		[Test]
		public void InnerExpansion()
		{
			// Set up the input.
			var format = "a$(MacroA)b";
			var macros = new Dictionary<string, object>
			{
				{ "MacroA", "Value A" }
			};

			// Perform the expansion.
			var expansion = new MacroExpansion(format);
			string results = expansion.Expand(macros);

			// Verify the results.
			Assert.AreEqual(
				"aValue Ab",
				results);
		}

		/// <summary>
		/// Verifies macros with spaces in their names.
		/// </summary>
		[Test]
		public void MacroNamesWithSpaces()
		{
			// Set up the input.
			var format = "$(Macro A)";
			var macros = new Dictionary<string, object>
			{
				{ "Macro A", "Value A" }
			};

			// Perform the expansion.
			var expansion = new MacroExpansion(format);
			string results = expansion.Expand(macros);

			// Verify the results.
			Assert.AreEqual(
				"Value A",
				results);
		}

		/// <summary>
		/// Verifies that the expansion on a null string is a null.
		/// </summary>
		[Test]
		public void NullString()
		{
			// Set up the input.
			string format = null;
			var macros = new Dictionary<string, object>
			{
				{ "MacroA", "Value A" }
			};

			// Perform the expansion.
			var expansion = new MacroExpansion(format);
			string results = expansion.Expand(macros);

			// Verify the results.
			Assert.AreEqual(
				null,
				results);
		}

		/// <summary>
		/// Verifies that numbers are expanded as string.
		/// </summary>
		[Test]
		public void NumericalExpansion()
		{
			// Set up the input.
			var format = "$(MacroA)";
			var macros = new Dictionary<string, object>
			{
				{ "MacroA", 17 }
			};

			// Perform the expansion.
			var expansion = new MacroExpansion(format);

			string results = expansion.Expand(macros);

			// Verify the results.
			Assert.AreEqual(
				"17",
				results);
		}

		/// <summary>
		/// Parses a simple format and returns the result.
		/// </summary>
		[Test]
		public void ParseSimpleInteger()
		{
			// Pull out the regular expression for a given format.
			var format = "$(MacroA:D4)";
			var input = "0123";
			var expansion = new MacroExpansion(format);

			Dictionary<string, string> results = expansion.Parse(input);

			// Verify the results.
			Assert.AreEqual(
				1,
				results.Count,
				"Number of macro results is unexpected.");
			Assert.IsTrue(
				results.ContainsKey("MacroA"),
				"Results don't contain MacroA.");
			Assert.AreEqual(
				"0123",
				results["MacroA"],
				"The macro value is unexpected.");
		}

		/// <summary>
		/// Parses a simple format and returns the result.
		/// </summary>
		[Test]
		public void ParseSimpleIntegerNonPadded()
		{
			// Pull out the regular expression for a given format.
			var format = "$(MacroA:G0)";
			var input = "123";
			var expansion = new MacroExpansion(format);

			Dictionary<string, string> results = expansion.Parse(input);

			// Verify the results.
			Assert.AreEqual(
				1,
				results.Count,
				"Number of macro results is unexpected.");
			Assert.IsTrue(
				results.ContainsKey("MacroA"),
				"Results don't contain MacroA.");
			Assert.AreEqual(
				"123",
				results["MacroA"],
				"The macro value is unexpected.");
		}

		/// <summary>
		/// Verifies the expansion of a simple macro.
		/// </summary>
		[Test]
		public void SimpleExpansion()
		{
			// Set up the input.
			var format = "$(MacroA)";
			var macros = new Dictionary<string, object>
			{
				{ "MacroA", "Value A" }
			};

			// Perform the expansion.
			var expansion = new MacroExpansion(format);
			string results = expansion.Expand(macros);

			// Verify the results.
			Assert.AreEqual(
				"Value A",
				results);
		}

		#endregion
	}
}
