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
using System.Collections.Generic;

#endregion

namespace MfGames.Reporting
{
    /// <summary>
    /// Contains an unordered collection of messages along with various query
    /// methods for determining the contents of the collection
    /// </summary>
    public class SeverityMessageCollection : HashSet<SeverityMessage>
    {
        #region Severity

        /// <summary>
        /// Gets the highest severity in the collection. If there are no elements
        /// in the collection, this returns Severity.Debug.
        /// </summary>
        public Severity? HighestSeverity
        {
            get
            {
                // Check for an empty collection.
                if (Count == 0)
                {
                    return null;
                }

                // Go through and gather the severity from the collection.
                int highest = (int) Severity.Debug;

                foreach (SeverityMessage message in this)
                {
                    highest = Math.Max((int) message.Severity, highest);
                }

                // Return the resulting severity.
                return (Severity) highest;
            }
        }

        #endregion
    }
}