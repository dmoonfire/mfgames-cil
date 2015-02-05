// <copyright file="NestableUpgradableReadLock.cs" company="Moonfire Games">
//   Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// <license href="http://mfgames.com/mfgames-cil/license">
//   MIT License (MIT)
// </license>

namespace MfGames.Locking
{
    using System;
    using System.Threading;

    /// <summary>
    /// Defines a ReaderWriterLockSlim upgradable read lock.
    /// </summary>
    public class NestableUpgradableReadLock : IDisposable
    {
        #region Fields

        /// <summary>
        /// </summary>
        private readonly bool lockAcquired;

        /// <summary>
        /// </summary>
        private readonly ReaderWriterLockSlim readerWriterLockSlim;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadLock"/> class.
        /// </summary>
        /// <param name="readerWriterLockSlim">
        /// The reader writer lock slim.
        /// </param>
        public NestableUpgradableReadLock(
            ReaderWriterLockSlim readerWriterLockSlim)
        {
            // Keep track of the lock since we'll need it to release the lock.
            this.readerWriterLockSlim = readerWriterLockSlim;

            // If we already have a read or write lock, we don't do anything.
            if (readerWriterLockSlim.IsUpgradeableReadLockHeld
                || readerWriterLockSlim.IsWriteLockHeld)
            {
                this.lockAcquired = false;
            }
            else
            {
                readerWriterLockSlim.EnterUpgradeableReadLock();
                this.lockAcquired = true;
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            if (this.lockAcquired)
            {
                this.readerWriterLockSlim.ExitUpgradeableReadLock();
            }
        }

        #endregion
    }
}