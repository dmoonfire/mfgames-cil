// <copyright file="IMacroExpansionContext.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

namespace MfGames.Text
{
	public interface IMacroExpansionContext
	{
		#region Public Properties

		IDictionary<string, object> Values { get; }

		#endregion
	}
}
