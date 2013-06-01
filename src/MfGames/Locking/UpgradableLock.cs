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
	public class UpgradableLock: IDisposable
	{
		#region Methods

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			readerWriterLockSlim.ExitUpgradeableReadLock();
		}

		#endregion

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

		#region Fields

		private readonly ReaderWriterLockSlim readerWriterLockSlim;

		#endregion
	}
}
