#region Copyright and License

// Copyright (C) 2005-2011 by Moonfire Games
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

using MfGames.Collections;

using NUnit.Framework;

#endregion

namespace UnitTests
{
    /// <summary>
    /// Tests various functionality of the weighted selectors.
    /// </summary>
    [TestFixture]
    public class WeightedSelectorTests
    {
        /// <summary>
        /// Tests selection from an empty selector.
        /// </summary>
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EmptySelector()
        {
            // Setup
            var selector = new WeightedSelector<string>();

            // Test
            selector.GetRandomItem();
        }

        /// <summary>
        /// Tests retrieving from the selector repeatedly with a single item
        /// inside it.
        /// </summary>
        [Test]
        public void SingleWeightSelector()
        {
            // Setup
            var selector = new WeightedSelector<string>();
            selector["bob"] = 1;

            // Test
            for (int i = 0; i < 100; i++)
            {
                selector.GetRandomItem();
            }
        }
    }
}