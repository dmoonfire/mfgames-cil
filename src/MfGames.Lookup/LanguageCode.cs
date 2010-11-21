using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MfGames.Lookup
{
	/// <summary>
	/// Represents a language code in normalized format. A language consists of
	/// a ISO 639-3 language code along with an optional script and region codes
	/// as described in RFC 5646 (http://tools.ietf.org/html/rfc5646).
	/// </summary>
	public class LanguageCode
	{
		#region Codes

		public string Language { get; set; }

		#endregion
	}
}
