// <copyright file="VariableMacroExpansionSegment.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MfGames.Text
{
	/// <summary>
	/// Contains a variable segment for a macro.
	/// </summary>
	public class VariableMacroExpansionSegment : IMacroExpansionSegment
	{
		#region Constructors and Destructors

		public VariableMacroExpansionSegment(string format, int macroIndex)
		{
			// Save the simple variables.
			MacroIndex = macroIndex;

			// Split out the parts.
			string[] parts = format.Split(new[] { ':' }, 2);

			Field = parts[0];

			if (parts.Length > 1 && !string.IsNullOrWhiteSpace(parts[1]))
			{
				Format = parts[1];
			}
		}

		#endregion

		#region Public Properties

		public string Field { get; private set; }
		public string Format { get; private set; }
		public int MacroIndex { get; private set; }

		#endregion

		#region Public Methods and Operators

		public static string Expand(
			string field,
			string format,
			IDictionary<string, object> macros)
		{
			// Pull out the object from the macros.
			object value = macros[field];

			// See if we have a format, if we do, try to parse it.
			if (format != null)
			{
				int intValue;

				if (Int32.TryParse(value.ToString(), out intValue))
				{
					value = intValue.ToString(format);
				}
			}

			// Return the results.
			return Convert.ToString(value);
		}

		public static string GetRegex(string format)
		{
			// If we don't have a format, blow up.
			if (format == null)
			{
				throw new InvalidOperationException(
					"Cannot use GetRegex() without a format in all variables.");
			}

			// Look for simple formats.
			string pattern = null;

			if (format.Length > 1)
			{
				char first = format[0];
				int precision;

				if (Int32.TryParse(format.Substring(1), out precision))
				{
					switch (first)
					{
						case 'D':
							pattern = "";

							for (var i = 0; i < precision; i++)
							{
								pattern += "\\d";
							}
							break;

						case 'G':
							pattern = "\\d+";
							break;
					}
				}
			}

			// If we haven't figured it out, throw up.
			if (pattern == null)
			{
				throw new InvalidOperationException(
					"Cannot create RegeEx from format: " + format + ".");
			}

			// Return the pattern.
			return string.Format("({0})", pattern);
		}

		public string Expand(IDictionary<string, object> macros)
		{
			return Expand(Field, Format, macros);
		}

		public string GetRegex()
		{
			return GetRegex(Format);
		}

		public void Match(Dictionary<string, string> results, Match match)
		{
			results[Field] = match.Groups[MacroIndex].Value;
		}

		#endregion
	}
}
