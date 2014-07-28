// <copyright file="TryGetHandler.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.Locking
{
    /// <summary>
    /// Defines the common try/get handler to retrieve an item of a given type.
    /// </summary>
    /// <typeparam name="TInput">
    /// </typeparam>
    /// <typeparam name="TOutput">
    /// </typeparam>
    public delegate bool TryGetHandler<TInput, TOutput>(
        TInput input, out TOutput output);
}