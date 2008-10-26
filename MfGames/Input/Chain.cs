using System.Collections.Generic;

namespace MfGames.Input
{
	/// <summary>
	/// Represents an input chain, which consists of one or more links (or
	/// collection of inputs).
	/// </summary>
	public class Chain
		: List<ChainLink>
	{
		#region Constructors
		/// <summary>
		/// Initializes a new instance of the <see cref="Chain"/> class.
		/// </summary>
		public Chain()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Chain"/> class.
		/// </summary>
		/// <param name="links">The links.</param>
		public Chain(params ChainLink[] links)
		{
			AddRange(links);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Chain"/> class.
		/// </summary>
		/// <param name="list">The list.</param>
		public Chain(IEnumerable<ChainLink> list)
		{
			AddRange(list);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Chain"/> class by
		/// creating a single-link chain with the given inputs.
		/// </summary>
		/// <param name="inputs">The inputs.</param>
		public Chain(params string[] inputs)
		{
			Add(new ChainLink(inputs));
		}
		#endregion

		#region Chain Operations
		/// <summary>
		/// Creates a subchain from the given index to the end.
		/// </summary>
		/// <param name="index">The index.</param>
		/// <returns></returns>
		public Chain Subchain(int index)
		{
			return new Chain(GetRange(index, Count - index));
		}
		#endregion
	}
}
