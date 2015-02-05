// <copyright file="WriteLock.cs" company="Moonfire Games">
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
    /// Defines a ReaderWriterLockSlim read-only lock.
    /// </summary>
    public class WriteLock : IDisposable
    {
        #region Fields

        /// <summary>
        /// </summary>
        private readonly ReaderWriterLockSlim readerWriterLockSlim;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="WriteLock"/> class.
        /// </summary>
        /// <param name="readerWriterLockSlim">
        /// The reader writer lock slim.
        /// </param>
        public WriteLock(ReaderWriterLockSlim readerWriterLockSlim)
        {
            this.readerWriterLockSlim = readerWriterLockSlim;
            readerWriterLockSlim.EnterWriteLock();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.readerWriterLockSlim.ExitWriteLock();
        }

        #endregion
    }
}