// <copyright file="MacroExpansion.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Text
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Text.RegularExpressions;

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
            : this("$(", 
                ")")
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
        public MacroExpansion(
            string beginDelimiter, 
            string endDelimiter)
        {
            // Establish our contracts.
            if (string.IsNullOrEmpty(beginDelimiter)
                || string.IsNullOrEmpty(endDelimiter))
            {
                throw new Exception("beginDelimiter and endDelimiter must be valid.");
            }

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
        /// Escapes the given text so it can be put inside a regular expression.
        /// </summary>
        /// <param name="text">
        /// The text.
        /// </param>
        /// <returns>
        /// </returns>
        public string EscapeRegex(string text)
        {
            text = text.Replace(
                @"\", 
                @"\\");
            text = text.Replace(
                ".", 
                @"\.");
            text = text.Replace(
                "(", 
                @"\(");
            text = text.Replace(
                ")", 
                @"\)");
            return text;
        }

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
        public string Expand(
            string format, 
            IDictionary<string, object> macros)
        {
            // If we have a null or blank string, return it immediately.
            if (format == null || format.Trim().Length == 0)
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

                int endIndex = format.IndexOf(
                    this.EndDelimiter, 
                    beginIndex);

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

                string before = format.Substring(
                    0, 
                    beginIndex);
                string macro = format.Substring(
                    macroStart, 
                    macroLength);
                string after = format.Substring(macroEnd);

                // Pull out the formatting string for the macro.
                string macroFormat = null;
                int formatIndex = macro.IndexOf(":");

                if (formatIndex > 0)
                {
                    macroFormat = macro.Substring(formatIndex + 1);
                    macro = macro.Substring(
                        0, 
                        formatIndex);
                }

                // Attempt to resolve the macro.
                object value;

                if (!macros.TryGetValue(
                    macro, 
                    out value))
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
                    value = string.Format(
                        "{0:" + macroFormat + "}", 
                        value);
                }

                // Replace the value in the string.
                format = string.Join(
                    string.Empty, 
                    before, 
                    value.ToString(), 
                    after);
            }
        }

        /// <summary>
        /// Builds a regular expression that can scan the output of a macro and produce
        /// the reverse operation.
        /// </summary>
        /// <param name="macro">
        /// The macro to parse.
        /// </param>
        /// <returns>
        /// A regular expression for the macro.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot build a regular expression from a macro that doesn't have a format for every substitution.
        /// </exception>
        public Regex GetRegex(string macro)
        {
            var additionalExpressions = new Dictionary<string, string>();

            return this.GetRegex(
                macro, 
                additionalExpressions, 
                MacroExpansionRegexOptions.Default);
        }

        /// <summary>
        /// Gets the regex.
        /// </summary>
        /// <param name="macro">
        /// The macro.
        /// </param>
        /// <param name="additionalExpressions">
        /// The additional expressions.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <returns>
        /// </returns>
        public Regex GetRegex(
            string macro, 
            Dictionary<string, string> additionalExpressions, 
            MacroExpansionRegexOptions options)
        {
            // Parse the macro and get the regular expression patterns.
            string pattern;
            List<string> groups;

            this.Parse(
                macro, 
                additionalExpressions, 
                options, 
                out pattern, 
                out groups);

            // Create a regular expression from it and return the results.
            var regex = new Regex(pattern);

            return regex;
        }

        /// <summary>
        /// Gets the regular expression pattern for a given format.
        /// </summary>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// A regular expression that represents the given format.
        /// </returns>
        public string GetRegexPattern(string format)
        {
            format = format.Replace(
                "0", 
                @"\d");
            return format;
        }

        /// <summary>
        /// Parses a given format and input and returns a dictionary of the results.
        /// </summary>
        /// <param name="macro">
        /// The macro to parse.
        /// </param>
        /// <param name="input">
        /// The input to parse.
        /// </param>
        /// <returns>
        /// A dictionary of results.
        /// </returns>
        public Dictionary<string, string> Parse(
            string macro, 
            string input)
        {
            // Parse the macro and get the regular expression patterns.
            string pattern;
            List<string> groups;

            this.Parse(
                macro, 
                new Dictionary<string, string>(), 
                MacroExpansionRegexOptions.Default, 
                out pattern, 
                out groups);

            // Create a regular expression from it and see if we can parse it.
            var regex = new Regex(pattern);
            Match match = regex.Match(input);

            if (!match.Success)
            {
                throw new InvalidOperationException(
                    string.Format(
                        "Cannot parse '{0}' from format '{1}'.", 
                        input, 
                        macro));
            }

            // Go through the groups and populate them.
            var results = new Dictionary<string, string>();

            for (int index = 0; index < groups.Count; index++)
            {
                string macroName = groups[index];
                results[macroName] = match.Groups[index + 1].Value;
            }

            return results;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Parses a given macro and produces a regular expression to parse it and
        /// a list of macros in the same order.
        /// </summary>
        /// <param name="macro">
        /// The macro to parse.
        /// </param>
        /// <param name="additionalExpressions">
        /// The additional expressions.
        /// </param>
        /// <param name="options">
        /// The options.
        /// </param>
        /// <param name="pattern">
        /// The resulting regex pattern.
        /// </param>
        /// <param name="groups">
        /// The resulting groups.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// Cannot build a regular expression from a macro that doesn't have a format for every substitution.
        /// </exception>
        private void Parse(
            string macro, 
            Dictionary<string, string> additionalExpressions, 
            MacroExpansionRegexOptions options, 
            out string pattern, 
            out List<string> groups)
        {
            // Go through the string and start building up a regular expression. This
            // expression is always anchored at the beginning and end.
            bool isNonCapturing = (options
                & MacroExpansionRegexOptions.NonCapturing) != 0;
            var buffer = new StringBuilder();

            if (!isNonCapturing)
            {
                buffer.Append("^");
            }

            // Loop through the macro and pull out the fields.
            groups = new List<string>();

            while (!(macro == null || macro.Trim().Length == 0))
            {
                // Look for the next macro.
                int start = macro.IndexOf(this.BeginDelimiter);

                if (start < 0)
                {
                    // There are no more macros.
                    buffer.Append(this.EscapeRegex(macro));
                    break;
                }

                // Make sure we have an end macro.
                int end = macro.IndexOf(
                    this.EndDelimiter, 
                    start);

                if (end < 0)
                {
                    // This is an unterminated macro.
                    buffer.Append(this.EscapeRegex(macro));
                    break;
                }

                // If there is anything before the line, then add it to the buffer.
                if (start > 0)
                {
                    string before = macro.Substring(
                        0, 
                        start);
                    buffer.Append(this.EscapeRegex(before));
                }

                // Pull out the substitution element and trim the macro down to the
                // text to the right of the macro.
                string sub = macro.Substring(
                    start + this.BeginDelimiter.Length, 
                    end - start - this.BeginDelimiter.Length);
                string newMacro = macro.Substring(
                    end + this.EndDelimiter.Length);
                macro = newMacro;

                // If we don't have a colon format in the macro, then we have an invalid
                // state.
                int colonIndex = sub.IndexOf(":", StringComparison.Ordinal);
                string[] parts;

                if (colonIndex > 0)
                {
                    parts = new string[]
                        {
                            sub.Substring(0, colonIndex),
                            sub.Substring(colonIndex + 1)
                        };
                }
                else
                {
                    // Check to see if we have the variable in the additional expressions.
                    string additional;

                    if (additionalExpressions.TryGetValue(
                        sub, 
                        out additional))
                    {
                        parts = new[] { sub, additional };
                    }
                    else
                    {
                        throw new InvalidOperationException(
                            "Cannot build a regular expression from a macro that doesn't have "
                                + "a format for every substitution.");
                    }
                }

                // Convert the format into a regular expression.
                string subPattern = this.GetRegexPattern(parts[1]);

                buffer.AppendFormat(
                    "{1}{0})", 
                    subPattern, 
                    isNonCapturing ? "(?:" : "(");

                // Add the group to the list.
                groups.Add(parts[0]);
            }

            // Anchor the end of the string.
            if (!isNonCapturing)
            {
                buffer.Append("$");
            }

            // Return the resulting regular expression.
            pattern = buffer.ToString();
        }

        #endregion
    }
}