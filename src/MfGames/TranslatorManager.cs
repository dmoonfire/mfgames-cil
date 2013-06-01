// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;

namespace MfGames
{
	/// <summary>
	/// A static singleton class used to coordinate various translation
	/// methodologies. To change how translation works, just change the Translate
	/// property to an appropriate Action.
	/// </summary>
	public class TranslatorManager
	{
		#region Properties

		/// <summary>
		/// Translate function. Pass in the key of the string and a translated
		/// value will be returned as part of the function. Setting this to null
		/// will use the TranslatorManager.IdentityTranslate method.
		/// </summary>
		public static Func<string, string> Translate
		{
			get { return translate; }
			set { translate = value ?? IdentityTranslate; }
		}

		#endregion

		#region Methods

		/// <summary>
		/// The identity translate return the input as the output, in effect
		/// performing no translations.
		/// </summary>
		/// <param name="input">The input string or translation key.</param>
		/// <returns>The input parameter.</returns>
		public static string IdentityTranslate(string input)
		{
			return input;
		}

		#endregion

		#region Constructors

		static TranslatorManager()
		{
			translate = IdentityTranslate;
		}

		#endregion

		#region Fields

		private static Func<string, string> translate;

		#endregion
	}
}
