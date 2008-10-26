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
	: Dictionary<string, List<string>>
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
	}
}
