using System;
using System.Threading;

namespace RCSoft.Core.ComponentModel
{
    public class WriteLockDisposable : IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;

        public WriteLockDisposable(ReaderWriterLockSlim rwLock)
        {
            _rwLock = rwLock;
            _rwLock.EnterReadLock();
        }
        void IDisposable.Dispose()
        {
            if (_rwLock.RecursiveWriteCount > 0)
            _rwLock.ExitWriteLock();
        }
    }
}
