#region Copyright and License

// Copyright (c) 2005-2011, Moonfire Games
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

#region Namespaces

using System;

#endregion

namespace MfGames
{
    /// <summary>
    /// A static singleton class used to coordinate various translation
    /// methodologies. To change how translation works, just change the Translate
    /// property to an appropriate Action.
    /// </summary>
    public class TranslatorManager
    {
        #region Constructors

        static TranslatorManager()
        {
            translate = IdentityTranslate;
        }

        #endregion

        #region Translator

        private static Func<string, string> translate;

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

        #region Identity Translation

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
    }
}