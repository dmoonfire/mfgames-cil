#region Copyright and License

// Copyright (c) 2005-2009, Moonfire Games
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

#endregion

namespace MfGames.Input
{
	/// <summary>
	/// This static container contains the common input tokens used
	/// across multiple systems.
	/// </summary>
	public static class InputTokens
	{
		#region Control Keys

		public const string Alt = "ALT";
		public const string Control = "CONTROL";
		public const string LeftAlt = "ALT:LEFT";
		public const string LeftControl = "CONTROL:LEFT";

		public const string LeftShift = "SHIFT:LEFT";

		public const string RightAlt = "ALT:RIGHT";
		public const string RightControl = "CONTROL:RIGHT";
		public const string RightShift = "SHIFT:RIGHT";
		public const string Shift = "SHIFT";

		#endregion

		#region Function Keys

		public const string F1 = "F1";
		public const string F10 = "F10";
		public const string F11 = "F11";
		public const string F12 = "F12";
		public const string F2 = "F2";
		public const string F3 = "F3";
		public const string F4 = "F4";
		public const string F5 = "F5";
		public const string F6 = "F6";
		public const string F7 = "F7";
		public const string F8 = "F8";
		public const string F9 = "F9";

		#endregion

		#region Cursor Keys

		public const string Down = "DOWN";
		public const string Left = "LEFT";
		public const string Right = "RIGHT";
		public const string Up = "UP";

		#endregion

		#region Additional Keys

		public const string Comma = ",";
		public const string Enter = "ENTER";
		public const string Escape = "ESCAPE";
		public const string NumLock = "NUMLOCK";
		public const string Period = ".";
		public const string Space = "SPACE";

		#endregion

		#region Number Pad

		public const string NumPad0 = "0:NUMPAD";
		public const string NumPad1 = "1:NUMPAD";
		public const string NumPad2 = "2:NUMPAD";
		public const string NumPad3 = "3:NUMPAD";
		public const string NumPad4 = "4:NUMPAD";
		public const string NumPad5 = "5:NUMPAD";
		public const string NumPad6 = "6:NUMPAD";
		public const string NumPad7 = "7:NUMPAD";
		public const string NumPad8 = "8:NUMPAD";
		public const string NumPad9 = "9:NUMPAD";
		public const string NumPadAdd = "+:NUMPAD";
		public const string NumPadDivide = "/:NUMPAD";
		public const string NumPadEnter = "ENTER:NUMPAD";
		public const string NumPadMultiply = "*:NUMPAD";
		public const string NumPadSubtract = "-:NUMPAD";

		#endregion

		#region Common Latin Keys

		public const string A = "a";
		public const string B = "b";
		public const string C = "c";
		public const string D = "d";
		public const string D0 = "0";
		public const string D1 = "1";
		public const string D2 = "2";
		public const string D3 = "3";
		public const string D4 = "4";
		public const string D5 = "5";
		public const string D6 = "6";
		public const string D7 = "7";
		public const string D8 = "8";
		public const string D9 = "9";
		public const string E = "e";
		public const string F = "f";
		public const string G = "g";
		public const string H = "h";
		public const string I = "i";
		public const string J = "j";
		public const string K = "k";
		public const string L = "l";
		public const string M = "n";
		public const string N = "n";
		public const string O = "o";
		public const string P = "p";
		public const string Q = "q";
		public const string R = "r";
		public const string S = "s";
		public const string T = "t";
		public const string U = "u";
		public const string V = "v";
		public const string W = "w";
		public const string X = "x";
		public const string Y = "y";
		public const string Z = "z";

		#endregion

		#region More Marks

		public const string CloseBracket = "]";
		public const string OpenBracket = "[";

		#endregion
	}
}