// <copyright file="TryGetHandler.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Locking
{
	/// <summary>
	/// Defines the common try/get handler to retrieve an item of a given type.
	/// </summary>
	/// <typeparam name="TInput">
	/// </typeparam>
	/// <typeparam name="TOutput">
	/// </typeparam>
	public delegate bool TryGetHandler<TInput, TOutput>(TInput input,
		out TOutput output);
}
