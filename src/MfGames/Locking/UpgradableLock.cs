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
using System.Threading;

#endregion

namespace MfGames.Locking
{
    /// <summary>
    /// Defines a ReaderWriterLockSlim read-only lock.
    /// </summary>
    public class UpgradableLock : IDisposable
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="UpgradableLock"/> class.
        /// </summary>
        /// <param name="readerWriterLockSlim">The reader writer lock slim.</param>
        public UpgradableLock(ReaderWriterLockSlim readerWriterLockSlim)
        {
            this.readerWriterLockSlim = readerWriterLockSlim;
            readerWriterLockSlim.EnterUpgradeableReadLock();
        }

        #endregion

        #region Destructors

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            readerWriterLockSlim.ExitUpgradeableReadLock();
        }

        #endregion

        #region Locking

        private readonly ReaderWriterLockSlim readerWriterLockSlim;

        #endregion
    }
}