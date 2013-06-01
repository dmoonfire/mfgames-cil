// Copyright 2005-2012 Moonfire Games
// Released under the MIT license
// http://mfgames.com/mfgames-cil/license

using System;
using System.Threading;

namespace MfGames.Locking
{
	/// <summary>
	/// Defines a ReaderWriterLockSlim read-only lock.
	/// </summary>
	public class NestableReadLock: IDisposable
	{
		#region Methods

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			if (lockAcquired)
			{
				readerWriterLockSlim.ExitReadLock();
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ReadLock"/> class.
		/// </summary>
		/// <param name="readerWriterLockSlim">The reader writer lock slim.</param>
		public NestableReadLock(ReaderWriterLockSlim readerWriterLockSlim)
		{
			// Keep track of the lock since we'll need it to release the lock.
			this.readerWriterLockSlim = readerWriterLockSlim;

			// If we already have a read or write lock, we don't do anything.
			if (readerWriterLockSlim.IsReadLockHeld
				|| readerWriterLockSlim.IsUpgradeableReadLockHeld
				|| readerWriterLockSlim.IsWriteLockHeld)
			{
				lockAcquired = false;
			}
			else
			{
				readerWriterLockSlim.EnterReadLock();
				lockAcquired = true;
			}
		}

		#endregion

		#region Fields

		private readonly bool lockAcquired;
		private readonly ReaderWriterLockSlim readerWriterLockSlim;

		#endregion
	}
}
