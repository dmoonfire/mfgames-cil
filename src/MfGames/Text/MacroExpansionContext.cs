// <copyright file="MacroExpansionContext.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

using System.Collections.Generic;

namespace MfGames.Text
{
	public class MacroExpansionContext : IMacroExpansionContext
	{
		#region Constructors and Destructors

		public MacroExpansionContext()
			: this(new Dictionary<string, object>())
		{
		}

		public MacroExpansionContext(IDictionary<string, object> values)
		{
			Values = values;
		}

		#endregion

		#region Public Properties

		public IDictionary<string, object> Values { get; private set; }

		#endregion
	}
}
