namespace MfGames.Delegates
{
	/// <summary>
	/// Defines the common try/get handler to retrieve an item of a given type.
	/// </summary>
	public delegate bool TryGetHandler<TInput, TOutput>(TInput input, out TOutput output);
}