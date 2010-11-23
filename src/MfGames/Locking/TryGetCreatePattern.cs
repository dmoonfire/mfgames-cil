using System.Threading;

using MfGames.Delegates;

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
		public static TOutput Invoke<TInput, TOutput>(
			ReaderWriterLockSlim readerWriterLockSlim,
		    TInput input,
		    TryGetHandler<TInput, TOutput> tryGetHandler,
		    CreateHandler<TInput, TOutput> createHandler)
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