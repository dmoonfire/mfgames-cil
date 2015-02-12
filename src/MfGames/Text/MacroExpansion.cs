// <copyright file="MacroExpansion.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MfGames.Text
{
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
		#region Fields

		private readonly IMacroExpansionSegment[] segments;

		private Regex regex;

		#endregion

		#region Constructors and Destructors

		/// <summary>
		/// Initializes a new instance of the <see cref="MacroExpansion"/> class.
		/// </summary>
		/// <param name="format">The format string to process.</param>
		/// <param name="beginDelimiter">The begin delimiter.</param>
		/// <param name="endDelimiter">The end delimiter. </param>
		/// <param name="escapeCharacter">The escape character to use.</param>
		public MacroExpansion(
			string format,
			string beginDelimiter = "$(",
			string endDelimiter = ")",
			char escapeCharacter = '\\')
			: this(beginDelimiter, endDelimiter, escapeCharacter)
		{
			segments = ParseSegments(
				format,
				beginDelimiter,
				endDelimiter,
				escapeCharacter);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MacroExpansion"/> class.
		/// </summary>
		/// <param name="format">The format string to process.</param>
		/// <param name="beginDelimiter">The begin delimiter.</param>
		/// <param name="endDelimiter">The end delimiter. </param>
		/// <param name="escapeCharacter">The escape character to use.</param>
		public MacroExpansion(
			IEnumerable<IMacroExpansionSegment> segments,
			string beginDelimiter = "$(",
			string endDelimiter = ")",
			char escapeCharacter = '\\')
			: this(beginDelimiter, endDelimiter, escapeCharacter)
		{
			if (segments != null)
			{
				this.segments = segments.ToArray();
			}
		}

		private MacroExpansion(
			string beginDelimiter,
			string endDelimiter,
			char escapeCharacter)
		{
			// Establish our contracts.
			if (string.IsNullOrEmpty(beginDelimiter) ||
				string.IsNullOrEmpty(endDelimiter))
			{
				throw new Exception(
					"beginDelimiter and endDelimiter must be valid.");
			}

			// Save the member variables.
			BeginDelimiter = beginDelimiter;
			EndDelimiter = endDelimiter;
			EscapeCharacter = escapeCharacter;
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

		/// <summary>
		/// Gets the escape character which is used to escape the next character.
		/// </summary>
		public char EscapeCharacter { get; private set; }

		public IEnumerable<IMacroExpansionSegment> Segments
		{
			get { return new List<IMacroExpansionSegment>(segments); }
		}

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
		public static string EscapeRegex(string text)
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
		/// Parses the given fragment and generates a list of segments
		/// representing the pattern.
		/// </summary>
		public static IMacroExpansionSegment[] ParseSegments(
			string format,
			string beginDelimiter = "$(",
			string endDelimiter = ")",
			char escapeCharacter = '\\')
		{
			// Check for parameters.
			if (format == null)
			{
				return null;
			}

			if (beginDelimiter == null)
			{
				throw new ArgumentNullException("beginDelimiter");
			}

			if (endDelimiter == null)
			{
				throw new ArgumentNullException("endDelimiter");
			}

			// Split apart the segments on the delimiters.
			List<string> parts = SplitSegments(
				format,
				beginDelimiter,
				endDelimiter,
				escapeCharacter);

			// Create a list of segments.
			var list = new List<IMacroExpansionSegment>();
			var inMacro = false;
			var macroIndex = 1;

			foreach (string part in parts)
			{
				if (part == beginDelimiter)
				{
					inMacro = true;
				}
				else if (part == endDelimiter)
				{
					inMacro = false;
				}
				else if (inMacro)
				{
					var segment = new VariableMacroExpansionSegment(part, macroIndex);
					macroIndex++;
					list.Add(segment);
				}
				else
				{
					var segment = new ConstantMacroExpansionSegment(part);
					list.Add(segment);
				}
			}

			// Return the resulting collection as an array since we won't
			// be changing it again.
			return list.ToArray();
		}

		public static List<string> SplitSegments(
			string format,
			string beginDelimiter = "$(",
			string endDelimiter = ")",
			char escapeCharacter = '\\')
		{
			// Check for parameters.
			if (format == null)
			{
				return null;
			}

			if (beginDelimiter == null)
			{
				throw new ArgumentNullException("beginDelimiter");
			}

			if (endDelimiter == null)
			{
				throw new ArgumentNullException("endDelimiter");
			}

			// We'll be using these quite a lot, so pull them out here.
			int beginLength = beginDelimiter.Length;
			int endLength = endDelimiter.Length;

			// First start by breaking apart the string on the delimiters
			// regardless of recursion.
			var parts = new List<string>();
			var buffer = new StringBuilder();
			string current = format;

			for (var stop = 0; stop < format.Length; stop++)
			{
				// If we have an escape character, just add the next character.
				char c = format[stop];

				if (c == escapeCharacter && stop + 2 < format.Length)
				{
					buffer.Append(format[stop + 1]);
					stop++;
					continue;
				}

				// If we at the beginning of a macro, then split it out.
				if (current.StartsWith(beginDelimiter))
				{
					// If we have anything in the buffer, add it.
					if (buffer.Length > 0)
					{
						parts.Add(buffer.ToString());
						buffer.Length = 0;
					}

					// Add the delimiter.
					parts.Add(beginDelimiter);
					current = current.Substring(beginLength);
					stop += beginLength - 1;
					continue;
				}

				// If we are at the end of a macro, then split it out.
				if (current.StartsWith(endDelimiter))
				{
					// If we have anything in the buffer, add it.
					if (buffer.Length > 0)
					{
						parts.Add(buffer.ToString());
						buffer.Length = 0;
					}

					// Add the delimiter.
					parts.Add(endDelimiter);
					current = current.Substring(endLength);
					stop += endLength - 1;
					continue;
				}

				// Otherwise, add it to the buffer.
				buffer.Append(c);

				if (current.Length > 0)
				{
					current = current.Substring(1);
				}
			}

			// If we have anything left in the buffer, add it.
			if (buffer.Length > 0)
			{
				parts.Add(buffer.ToString());
			}

			// Return the resulting list.
			return parts;
		}

		/// <summary>
		/// Expands the specified format with the given macros.
		/// </summary>
		/// <remarks>
		/// This is thread-safe.
		/// </remarks>
		/// <param name="macros">
		/// The macros collection to expand.
		/// </param>
		/// <returns>
		/// The expanded format with all of the parameters.
		/// </returns>
		public string Expand(IDictionary<string, object> macros)
		{
			// If we have no segments, then return null.
			if (segments == null)
			{
				return null;
			}

			// Build up a list of all the strings and combine them together.
			IEnumerable<string> texts = segments.Select(s => s.Expand(macros));

			return string.Concat(texts);
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
		public Regex GetRegex()
		{
			// If we have already calculated the regex, use it.
			if (regex != null)
			{
				return regex;
			}

			// If we have no segments, then return null.
			if (segments == null)
			{
				return null;
			}

			// Get the pattern and format it.
			string pattern = GetRegexPattern();

			regex = new Regex(pattern);

			return regex;
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
			Dictionary<string, string> additionalExpressions,
			MacroExpansionRegexOptions options)
		{
			//// Parse the macro and get the regular expression patterns.
			//string pattern;
			//List<string> groups;

			//Parse(
			//	additionalExpressions,
			//	options,
			//	out pattern,
			//	out groups);

			//// Create a regular expression from it and return the results.
			//var regex = new Regex(pattern);

			//return regex;
			throw new NotImplementedException();
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
		public string GetRegexPattern()
		{
			// If the segments are null, we return null.
			if (segments == null)
			{
				return null;
			}

			// Build up a list of all the strings and combine them together.
			IEnumerable<string> texts = segments.Select(s => s.GetRegex());
			string pattern = "^" + string.Concat(texts) + "$";

			return pattern;
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
		public Dictionary<string, string> Parse(string input)
		{
			// Get a regex of the pattern.
			Regex regex = GetRegex();
			Match match = regex.Match(input);

			// If we weren't successful, then return null.
			if (!match.Success)
			{
				return null;
			}

			// Go through and populate the dictionary.
			var results = new Dictionary<string, string>();

			foreach (IMacroExpansionSegment segment in segments)
			{
				segment.Match(results, match);
			}

			return results;
		}

		#endregion
	}
}
