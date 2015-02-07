// <copyright file="IMacroExpansionSegment.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

namespace MfGames.Text
{
	public interface IMacroExpansionSegment
	{
		#region Public Methods and Operators

		string Expand(IDictionary<string, object> macros);

		string GetRegex();

		#endregion
	}
}
