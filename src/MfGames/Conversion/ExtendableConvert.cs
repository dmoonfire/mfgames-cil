// <copyright file="ExtendableConvert.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)

using SystemConvert = System.Convert;

namespace MfGames.Conversion
{
    using System.Globalization;

    /// <summary>
    /// Defines a plugable conversion system. This functions much like System
    /// </summary>
    public class ExtendableConvert
    {
        #region Constructors and Destructors

        /// <summary>
        /// </summary>
        static ExtendableConvert()
        {
            Instance = new ExtendableConvert();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets a static instance of the extendable converter.
        /// </summary>
        public static ExtendableConvert Instance { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Converts the specified input to the given output.
        /// </summary>
        /// <typeparam name="TInput">
        /// The type of the input.
        /// </typeparam>
        /// <typeparam name="TOutput">
        /// The type of the output.
        /// </typeparam>
        /// <param name="input">
        /// The input object of TInput.
        /// </param>
        /// <returns>
        /// A resulting TOutput.
        /// </returns>
        public TOutput Convert<TInput, TOutput>(TInput input)
        {
            return (TOutput)SystemConvert.ChangeType(
                input, 
                typeof(TOutput),
                CultureInfo.InvariantCulture);
        }

        #endregion
    }
}