#region Namespaces

using System;
using System.Threading;

#endregion

namespace MfGames.Locking
{
	/// <summary>
	/// Defines a ReaderWriterLockSlim read-only lock.
	/// </summary>
	public class WriteLock : IDisposable
	{
		#region Constructors
		
		public WriteLock(ReaderWriterLockSlim readerWriterLockSlim)
		{
			this.readerWriterLockSlim = readerWriterLockSlim;
			readerWriterLockSlim.EnterWriteLock();
		}
		
		#endregion
		
		#region Destructors
		
		public void Dispose()
		{
			readerWriterLockSlim.ExitWriteLock();
		}
		
		#endregion
		
		#region Locking
		
		private readonly ReaderWriterLockSlim readerWriterLockSlim;
		
		#endregion
	}
}