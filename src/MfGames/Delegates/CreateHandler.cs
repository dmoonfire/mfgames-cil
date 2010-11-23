namespace MfGames.Delegates
{
	/// <summary>
	/// Defines the a create item handler.
	/// </summary>
	public delegate TOutput CreateHandler<TInput, TOutput>(TInput input);
}