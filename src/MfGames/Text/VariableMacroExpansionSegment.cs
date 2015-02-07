// <copyright file="VariableMacroExpansionSegment.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System;
using System.Collections.Generic;

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

		public string Expand(IDictionary<string, object> macros)
		{
			// Pull out the object from the macros.
			object value = macros[Field];

			// See if we have a format.
			if (Format != null)
			{
				// See if we have a zero-padding format.
				if (Format == new string('0', Format.Length))
				{
					value = Convert.ToString(value).PadLeft(Format.Length, '0');
				}
			}

			// Return the results.
			return Convert.ToString(value);
		}

		public string GetRegex()
		{
			// If we don't have a format, blow up.
			if (Format == null)
			{
				throw new InvalidOperationException(
					"Cannot use GetRegex() without a format in all variables.");
			}

			// See if we have a zero-padding format.
			if (Format == new string('0', Format.Length))
			{
				return "(" + Format.Replace("0", "\\d") + ")";
			}

			// If we haven't figured it out, throw up.
			throw new InvalidOperationException(
				"Cannot create RegeEx from format: " + Format + ".");
		}

		#endregion
	}
}
