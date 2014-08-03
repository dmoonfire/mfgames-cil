// <copyright file="MacroExpansion.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Text
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.Contracts;

    /// <summary>
    /// Utility class for expanding macros from a format string and a collection of
    /// variables.
    /// </summary>
    /// <remarks>
    /// This immutable class is thread-safe but not pure. Each of the expansion calls
    /// do not affect the class itself, but there may be side effects of the macro
    /// expansion.
    /// </remarks>
    public class MacroExpansion
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MacroExpansion"/> class.
        /// </summary>
        public MacroExpansion()
            : this("$(", ")")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MacroExpansion"/> class.
        /// </summary>
        /// <param name="beginDelimiter">
        /// The begin delimiter.
        /// </param>
        /// <param name="endDelimiter">
        /// The end delimiter.
        /// </param>
        public MacroExpansion(string beginDelimiter, string endDelimiter)
        {
            // Establish our contracts.
            Contract.Requires(beginDelimiter != null);
            Contract.Requires(beginDelimiter.Length > 0);
            Contract.Requires(endDelimiter != null);
            Contract.Requires(endDelimiter.Length > 0);

            // Save the member variables.
            this.BeginDelimiter = beginDelimiter;
            this.EndDelimiter = endDelimiter;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the begin delimiter string which identifies a macro to be expanded. The
        /// code allows for nested macros, but neither the begin or end delimiters may be
        /// used in a macro name. This defaults to "$(".
        /// </summary>
        /// <value>
        /// The begin delimiter.
        /// </value>
        public string BeginDelimiter { get; private set; }

        /// <summary>
        /// Gets the end delimiter which identifies the end of a macro. This defaults to ")".
        /// </summary>
        /// <value>
        /// The end delimiter.
        /// </value>
        public string EndDelimiter { get; private set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Expands the specified format with the given macros.
        /// </summary>
        /// <remarks>
        /// This is thread-safe.
        /// </remarks>
        /// <param name="format">
        /// The format string using the given macros.
        /// </param>
        /// <param name="macros">
        /// The macros collection to expand.
        /// </param>
        /// <returns>
        /// </returns>
        public string Expand(string format, IDictionary<string, object> macros)
        {
            // If we have a null or blank string, return it immediately.
            if (string.IsNullOrWhiteSpace(format))
            {
                return format;
            }

            // Loop until we run out.
            while (true)
            {
                // Check to see if we have both a begin and end delimiter in the string.
                int beginIndex = format.LastIndexOf(this.BeginDelimiter);

                if (beginIndex < 0)
                {
                    // These will never resolve into a string.
                    return format;
                }

                int endIndex = format.IndexOf(this.EndDelimiter, beginIndex);

                if (endIndex < 0)
                {
                    // This will never resolve into a format.
                    return format;
                }

                // Pull out the components.
                int macroLength = endIndex - beginIndex
                    - this.BeginDelimiter.Length;
                int macroStart = beginIndex + this.BeginDelimiter.Length;
                int macroEnd = endIndex + this.EndDelimiter.Length;

                string before = format.Substring(0, beginIndex);
                string macro = format.Substring(macroStart, macroLength);
                string after = format.Substring(macroEnd);

                // Pull out the formatting string for the macro.
                string macroFormat = null;
                int formatIndex = macro.IndexOf(":");

                if (formatIndex > 0)
                {
                    macroFormat = macro.Substring(formatIndex + 1);
                    macro = macro.Substring(0, formatIndex);
                }

                // Attempt to resolve the macro.
                object value;

                if (!macros.TryGetValue(macro, out value))
                {
                    // We couldn't find it.
                    throw new InvalidOperationException(
                        "Cannot find macro '" + macro + "'.");
                }

                if (value == null)
                {
                    value = string.Empty;
                }

                // Format the value if we have one.
                if (macroFormat != null)
                {
                    value = string.Format("{0:" + macroFormat + "}", value);
                }

                // Replace the value in the string.
                format = string.Join(
                    string.Empty, before, value.ToString(), after);
            }
        }

        #endregion
    }
}