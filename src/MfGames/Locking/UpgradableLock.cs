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
		
		public UpgradableLock(ReaderWriterLockSlim readerWriterLockSlim)
		{
			this.readerWriterLockSlim = readerWriterLockSlim;
			readerWriterLockSlim.EnterUpgradeableReadLock();
		}
		
		#endregion
		
		#region Destructors
		
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