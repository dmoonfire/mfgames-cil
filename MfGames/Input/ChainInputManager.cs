namespace MfGames.Input
{
	/// <summary>
	/// This manager extends the functionality of the InputManager to
	/// handle chained input events. This idea comes from Emacs, but
	/// it implements a design pattern where the application registers
	/// a series of commands, such as "C-x C-c" (control x, control c)
	/// with a specific command. These chains can be singular, such as
	/// "C-s" for the typical Save command or "C-S-s" for the Save As
	/// keyboard command.
	/// </summary>
	public class ChainInputManager
	: InputManager
	{
	}
}
