// <copyright file="MacroExpansionTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace UnitTests.Text
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    using MfGames.Text;

    using NUnit.Framework;

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
            var expansion = new MacroExpansion();

            string results = expansion.Expand(
                format, 
                macros);

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
            string format = "{MacroA}";
            var macros = new Dictionary<string, object>
                {
                    { "MacroA", "Value A" }
                };

            // Perform the expansion.
            var expansion = new MacroExpansion(
                "{", 
                "}");

            string results = expansion.Expand(
                format, 
                macros);

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
            string format = "$(MacroA)";
            var macros = new Dictionary<string, object>
                {
                    { "MacroA", "Value A" }
                };

            // Perform the expansion.
            var expansion = new MacroExpansion(
                "{", 
                "}");

            string results = expansion.Expand(
                format, 
                macros);

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
            string format = "$(MacroA:0000)";
            var macros = new Dictionary<string, object>
                {
                    { "MacroA", 13 }
                };

            // Perform the expansion.
            var expansion = new MacroExpansion();

            string results = expansion.Expand(
                format, 
                macros);

            // Verify the results.
            Assert.AreEqual(
                "0013", 
                results);
        }

        /// <summary>
        /// Tests that a macro with an integer format will parse correctly.
        /// </summary>
        [Test]
        public void GetRegexForComplexPattern()
        {
            // Pull out the regular expression for a given format.
            string format = "/test/section-$(MacroA:0000)/$(MacroB:00).txt";
            var expansion = new MacroExpansion();

            Regex regex = expansion.GetRegex(format);

            // Verify the results.
            Assert.AreEqual(
                @"^/test/section-(\d\d\d\d)/(\d\d)\.txt$", 
                regex.ToString());
        }

        /// <summary>
        /// Tests that a macro with an integer format will parse correctly.
        /// </summary>
        [Test]
        public void GetRegexForSimplePaddedInteger()
        {
            // Pull out the regular expression for a given format.
            string format = "$(MacroA:0000)";
            var expansion = new MacroExpansion();

            Regex regex = expansion.GetRegex(format);

            // Verify the results.
            Assert.AreEqual(
                @"^(\d\d\d\d)$", 
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
            string format = "$(MacroA:00) $(MacroA)";
            var expansion = new MacroExpansion();

            // Verify that it throws an exception.
            Assert.Throws<InvalidOperationException>(
                () => expansion.GetRegex(format));
        }

        /// <summary>
        /// Tests that giving a macro without any formatting information throws
        /// an exception.
        /// </summary>
        [Test]
        public void GetRegexThrowsWithoutFormatting()
        {
            // Pull out the regular expression for a given format.
            string format = "$(MacroA)";
            var expansion = new MacroExpansion();

            // Verify that it throws an exception.
            Assert.Throws<InvalidOperationException>(
                () => expansion.GetRegex(format));
        }

        /// <summary>
        /// Verifies expansion with text before and after the macro.
        /// </summary>
        [Test]
        public void InnerExpansion()
        {
            // Set up the input.
            string format = "a$(MacroA)b";
            var macros = new Dictionary<string, object>
                {
                    { "MacroA", "Value A" }
                };

            // Perform the expansion.
            var expansion = new MacroExpansion();

            string results = expansion.Expand(
                format, 
                macros);

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
            string format = "$(Macro A)";
            var macros = new Dictionary<string, object>
                {
                    { "Macro A", "Value A" }
                };

            // Perform the expansion.
            var expansion = new MacroExpansion();

            string results = expansion.Expand(
                format, 
                macros);

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
            var expansion = new MacroExpansion();

            string results = expansion.Expand(
                format, 
                macros);

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
            string format = "$(MacroA)";
            var macros = new Dictionary<string, object>
                {
                    { "MacroA", 17 }
                };

            // Perform the expansion.
            var expansion = new MacroExpansion();

            string results = expansion.Expand(
                format, 
                macros);

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
            string format = "$(MacroA:0000)";
            string input = "0123";
            var expansion = new MacroExpansion();

            Dictionary<string, string> results = expansion.Parse(
                format, 
                input);

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
        /// Verifies recursive expansion of macros.
        /// </summary>
        [Test]
        public void RecursiveExpansion()
        {
            // Set up the input.
            string format = "$(Start)$(MacroB))";
            var macros = new Dictionary<string, object>
                {
                    { "MacroA", "Value A" }, 
                    { "MacroB", "A" }, 
                    { "Start", "$(Macro" }
                };

            // Perform the expansion.
            var expansion = new MacroExpansion();

            string results = expansion.Expand(
                format, 
                macros);

            // Verify the results.
            Assert.AreEqual(
                "Value A", 
                results);
        }

        /// <summary>
        /// Verifies the expansion of a simple macro.
        /// </summary>
        [Test]
        public void SimpleExpansion()
        {
            // Set up the input.
            string format = "$(MacroA)";
            var macros = new Dictionary<string, object>
                {
                    { "MacroA", "Value A" }
                };

            // Perform the expansion.
            var expansion = new MacroExpansion();

            string results = expansion.Expand(
                format, 
                macros);

            // Verify the results.
            Assert.AreEqual(
                "Value A", 
                results);
        }

        #endregion
    }
}