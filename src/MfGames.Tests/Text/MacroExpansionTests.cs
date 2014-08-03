// <copyright file="MacroExpansionTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace UnitTests.Text
{
    using System.Collections.Generic;

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

            string results = expansion.Expand(format, macros);

            // Verify the results.
            Assert.AreEqual(string.Empty, results);
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
            var expansion = new MacroExpansion("{", "}");

            string results = expansion.Expand(format, macros);

            // Verify the results.
            Assert.AreEqual("Value A", results);
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
            var expansion = new MacroExpansion("{", "}");

            string results = expansion.Expand(format, macros);

            // Verify the results.
            Assert.AreEqual("$(MacroA)", results);
        }

        /// <summary>
        /// Verifies formatting of integer values.
        /// </summary>
        [Test]
        public void FormattedIntegerExpansion()
        {
            // Set up the input.
            string format = "$(MacroA:0000)";
            var macros = new Dictionary<string, object> { { "MacroA", 13 } };

            // Perform the expansion.
            var expansion = new MacroExpansion();

            string results = expansion.Expand(format, macros);

            // Verify the results.
            Assert.AreEqual("0013", results);
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

            string results = expansion.Expand(format, macros);

            // Verify the results.
            Assert.AreEqual("aValue Ab", results);
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

            string results = expansion.Expand(format, macros);

            // Verify the results.
            Assert.AreEqual("Value A", results);
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

            string results = expansion.Expand(format, macros);

            // Verify the results.
            Assert.AreEqual(null, results);
        }

        /// <summary>
        /// Verifies that numbers are expanded as string.
        /// </summary>
        [Test]
        public void NumericalExpansion()
        {
            // Set up the input.
            string format = "$(MacroA)";
            var macros = new Dictionary<string, object> { { "MacroA", 17 } };

            // Perform the expansion.
            var expansion = new MacroExpansion();

            string results = expansion.Expand(format, macros);

            // Verify the results.
            Assert.AreEqual("17", results);
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

            string results = expansion.Expand(format, macros);

            // Verify the results.
            Assert.AreEqual("Value A", results);
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

            string results = expansion.Expand(format, macros);

            // Verify the results.
            Assert.AreEqual("Value A", results);
        }

        #endregion
    }
}