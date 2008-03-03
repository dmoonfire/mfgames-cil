using System;
using System.Collections.Generic;

namespace MfGames.Input
{
	/// <summary>
	/// Defines a basic token mapping dictionary. This translates a
	/// token, such as right control ("CR") to one or more additional
	/// tokens, such as control ("C").
	///
	/// The initial mapping dictionary is empty, but
	/// AddStandardMappings() method populates with the most common
	/// mappings.
	/// </summary>
	public class TokenMappingDictionary
	: Dictionary<string,List<string>>
	{
		#region Collection Operations
		/// <summary>
		/// Adds a mapping from one token to another token. Duplicates
		/// are automatically ignored.
		/// </summary>
		public void Add(string fromToken, string toToken)
		{
			// Sanity checking on the parameters
			if (String.IsNullOrEmpty(fromToken))
				throw new Exception("fromToken cannot be null or blank");

			if (String.IsNullOrEmpty(toToken))
				throw new Exception("toToken cannot be null or blank");

			// Get the key
			if (!ContainsKey(fromToken))
				Add(fromToken, new List<string>());

			List<string> list = this[fromToken];

			// Add it
			if (!list.Contains(toToken))
				list.Add(toToken);
		}
		#endregion

		#region Standard Mapping
		/// <summary>
		/// Adds the following mappings to the dictionary:
		/// <ul>
		/// <li>RC: C</li>
		/// <li>LC: C</li>
		/// <li>RS: S</li>
		/// <li>LS: S</li>
		/// </ul>
		/// </summary>
		public void AddStandardMappings()
		{
			// Control keys
			Add(InputTokens.RightControl, InputTokens.Control);
			Add(InputTokens.LeftControl,  InputTokens.Control);

			Add(InputTokens.RightShift, InputTokens.Shift);
			Add(InputTokens.LeftShift,  InputTokens.Shift);
		}
		#endregion
	}
}
