using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MfGames.Settings.Design
{
	public enum UsageType
	{
		/// <summary>
		/// Settings that are persisted through the XML store.
		/// </summary>
		Setting,

		/// <summary>
		/// Constants that aren't persisted and cannot be changed.
		/// </summary>
		Constant,

		/// <summary>
		/// Settings that are changable but are not saved in the store.
		/// </summary>
		Transient,
	}
}
