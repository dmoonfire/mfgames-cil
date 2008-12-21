using System.Collections.Generic;

namespace MfGames.Input
{
	/// <summary>
	/// Represents a single set of elements in a single link of the
	/// input chain.
	/// </summary>
	public class ChainLink
		: HashSet<string>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="ChainLink"/> class.
		/// </summary>
		public ChainLink()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ChainLink"/> class.
		/// </summary>
		/// <param name="inputs">The inputs.</param>
		public ChainLink(params string[] inputs)
		{
			foreach (string input in inputs)
				Add(input);
		}
		#endregion Constructors
	}
}
