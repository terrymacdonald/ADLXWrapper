using System;
using System.Threading;

namespace ADLXWrapper
{
    /// <summary>
    /// Global synchronization gate for ADLX native calls.
    /// Use read locks for normal operations, write locks for init/terminate.
    /// </summary>
    internal static class ADLXSync
    {
        private static readonly ReaderWriterLockSlim _lock = new(LockRecursionPolicy.SupportsRecursion);

        public static IDisposable EnterRead() => new Scope(_lock, read: true);
        public static IDisposable EnterWrite() => new Scope(_lock, read: false);

        private readonly struct Scope : IDisposable
        {
            private readonly ReaderWriterLockSlim _lock;
            private readonly bool _read;

            public Scope(ReaderWriterLockSlim rw, bool read)
            {
                _lock = rw;
                _read = read;
                if (read)
                    _lock.EnterReadLock();
                else
                    _lock.EnterWriteLock();
            }

            public void Dispose()
            {
                if (_read)
                    _lock.ExitReadLock();
                else
                    _lock.ExitWriteLock();
            }
        }
    }
}
