// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

namespace MfGames.Locking
{
	/// <summary>
	/// Defines the common try/get handler to retrieve an item of a given type.
	/// </summary>
	public delegate bool TryGetHandler<TInput, TOutput>(TInput input,
		out TOutput output);
}
