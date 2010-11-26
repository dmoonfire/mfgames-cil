#region Namespaces

using System;
using System.Threading;

#endregion

namespace MfGames.Locking
{
	/// <summary>
	/// Defines a ReaderWriterLockSlim read-only lock.
	/// </summary>
	public class ReadLock : IDisposable
	{
		#region Constructors
		
		public ReadLock(ReaderWriterLockSlim readerWriterLockSlim)
		{
			this.readerWriterLockSlim = readerWriterLockSlim;
			readerWriterLockSlim.EnterReadLock();
		}
		
		#endregion
		
		#region Destructors
		
		public void Dispose()
		{
			readerWriterLockSlim.ExitReadLock();
		}
		
		#endregion
		
		#region Locking
		
		private readonly ReaderWriterLockSlim readerWriterLockSlim;
		
		#endregion
	}
}