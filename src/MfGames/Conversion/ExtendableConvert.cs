// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using SystemConvert = System.Convert;

namespace MfGames.Conversion
{
	/// <summary>
	/// Defines a plugable conversion system. This functions much like System
	/// </summary>
	public class ExtendableConvert
	{
		#region Properties

		/// <summary>
		/// Gets or sets a static instance of the extendable converter.
		/// </summary>
		public static ExtendableConvert Instance { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Converts the specified input to the given output.
		/// </summary>
		/// <typeparam name="TInput">The type of the input.</typeparam>
		/// <typeparam name="TOutput">The type of the output.</typeparam>
		/// <param name="input">The input object of TInput.</param>
		/// <returns>A resulting TOutput.</returns>
		public TOutput Convert<TInput, TOutput>(TInput input)
		{
			//throw new FormatException(
			//	string.Format(
			//		"Cannot convert '{0}' from {1} to {2}.",
			//		input,
			//		typeof (TInput),
			//		typeof (TOutput)));
			return (TOutput) SystemConvert.ChangeType(input, typeof (TOutput));
		}

		#endregion

		#region Constructors

		static ExtendableConvert()
		{
			Instance = new ExtendableConvert();
		}

		#endregion
	}
}
