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
    /// Implements the basic pattern for getting an item from a cache using
    /// the ReaderWriterLockSlim class. This attempts to get it using a read-only
    /// lock. If that fails, it gets an upgradable lock, tries again, and if it
    /// still can't find it, upgrades the lock to a write lock to create it.
    /// </summary>
    public static class TryGetCreatePattern
    {
        /// <summary>
        /// Invokes the try/get/create pattern used a condition to test for it
        /// and a constructor function.
        /// </summary>
        /// <param name="readerWriterLockSlim">The reader writer lock slim.</param>
        /// <param name="conditionHandler">The condition handler.</param>
        /// <param name="createHandler">The create handler.</param>
        public static void Invoke(
            ReaderWriterLockSlim readerWriterLockSlim,
            Func<bool> conditionHandler,
            Action createHandler)
        {
            using (new ReadLock(readerWriterLockSlim))
            {
                // Verify that the condition for creating it is false.
                if (!conditionHandler())
                {
                    return;
                }
            }

            // We failed to get the lock using the read-only. We create an upgradable lock
            // and try again since it may have been created with a race condition when the
            // last lock was released and this one was acquired.
            using (new UpgradableLock(readerWriterLockSlim))
            {
                // Verify that the condition for creating it is false.
                if (!conditionHandler())
                {
                    return;
                }

                // We failed to get it in the lock. Upgrade the lock to a write and create it.
                using (new WriteLock(readerWriterLockSlim))
                {
                    createHandler();
                }
            }
        }

        /// <summary>
        /// Invokes the try/get/create pattern using a tryget retrieval and a
        /// creator handler.
        /// </summary>
        /// <typeparam name="TInput">The type of the input.</typeparam>
        /// <typeparam name="TOutput">The type of the output.</typeparam>
        /// <param name="readerWriterLockSlim">The reader writer lock slim.</param>
        /// <param name="input">The input.</param>
        /// <param name="tryGetHandler">The try get handler.</param>
        /// <param name="createHandler">The create handler.</param>
        /// <returns></returns>
        public static TOutput Invoke<TInput, TOutput>(
            ReaderWriterLockSlim readerWriterLockSlim,
            TInput input,
            TryGetHandler<TInput, TOutput> tryGetHandler,
            Func<TInput, TOutput> createHandler)
        {
            // First attempt to get the item using a read-only lock.
            TOutput output;

            using (new ReadLock(readerWriterLockSlim))
            {
                // Try to get the item using the try/get handler.
                if (tryGetHandler(input, out output))
                {
                    // We successful got the item in the read-only cache, so just return it.
                    return output;
                }
            }

            // We failed to get the lock using the read-only. We create an upgradable lock
            // and try again since it may have been created with a race condition when the
            // last lock was released and this one was acquired.
            using (new UpgradableLock(readerWriterLockSlim))
            {
                // Try to get the item using the try/get handler.
                if (tryGetHandler(input, out output))
                {
                    // We successful got the item in this lock, so return it without
                    // upgrading the lock.
                    return output;
                }

                // We failed to get it in the lock. Upgrade the lock to a write and create it.
                using (new WriteLock(readerWriterLockSlim))
                {
                    return createHandler(input);
                }
            }
        }
    }
}