using System;

namespace ADLXWrapper
{
	/// <summary>
	/// Lightweight lifetime wrapper for raw ADLX interface pointers.
	/// </summary>
	public readonly unsafe struct ADLXInterfaceHandle : IDisposable
	{
		private readonly IntPtr _ptr;
		private readonly bool _owns;

		private ADLXInterfaceHandle(IntPtr ptr, bool owns)
		{
			_ptr = ptr;
			_owns = owns;
		}

		public bool IsInvalid => _ptr == IntPtr.Zero;

        public static ADLXInterfaceHandle From(void* ptr, bool addRef = true)
        {
            if (ptr == null) throw new ArgumentNullException(nameof(ptr));
            var intPtr = (IntPtr)ptr;
            if (addRef)
            {
                ADLXUtils.AddRefInterface(intPtr);
            }
            return new ADLXInterfaceHandle(intPtr, owns: true);
        }

        /// <summary>
        /// Create a non-owning handle for interfaces that are not ref-counted (no Acquire/Release).
        /// </summary>
        public static ADLXInterfaceHandle FromNonRefCounted(void* ptr)
        {
            if (ptr == null) throw new ArgumentNullException(nameof(ptr));
            return new ADLXInterfaceHandle((IntPtr)ptr, owns: false);
        }

		public T* As<T>() where T : unmanaged => (T*)_ptr;

		public void Dispose()
		{
			if (_owns && _ptr != IntPtr.Zero)
			{
				ADLXUtils.ReleaseInterface(_ptr);
			}
		}

		public static implicit operator IntPtr(ADLXInterfaceHandle handle) => handle._ptr;
		public static implicit operator IADLXInterface*(ADLXInterfaceHandle handle) => (IADLXInterface*)handle._ptr;
	}
}

