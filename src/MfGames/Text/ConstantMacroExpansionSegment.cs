// <copyright file="ConstantMacroExpansionSegment.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MfGames.Text
{
	/// <summary>
	/// Contains a single constant text segment for a macro.
	/// </summary>
	public class ConstantMacroExpansionSegment : IMacroExpansionSegment
	{
		#region Constructors and Destructors

		public ConstantMacroExpansionSegment(string text)
		{
			Text = text;
		}

		#endregion

		#region Public Properties

		public string Text { get; private set; }

		#endregion

		#region Public Methods and Operators

		public string Expand(IDictionary<string, object> macros)
		{
			return Text;
		}

		public string GetRegex()
		{
			return MacroExpansion.EscapeRegex(Text);
		}

		public void Match(Dictionary<string, string> results, Match match)
		{
		}

		public override string ToString()
		{
			return Text;
		}

		#endregion
	}
}
