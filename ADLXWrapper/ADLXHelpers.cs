using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Linq;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    public static unsafe class ADLXHelpers
    {
        /// <summary>
        /// Try QueryInterface on an ADLX interface given an IID string (wide string).
        /// </summary>
        public static unsafe bool TryQueryInterface(IntPtr pInterface, string interfaceName, out IntPtr resultPtr)
        {
            resultPtr = IntPtr.Zero;
            if (pInterface == IntPtr.Zero || string.IsNullOrEmpty(interfaceName))
                return false;

            var iface = (IADLXInterface*)pInterface;
            var terminated = interfaceName + "\0";
            fixed (char* chars = terminated)
            {
                var result = iface->QueryInterface((ushort*)chars, (void**)&resultPtr);
                return result == ADLX_RESULT.ADLX_OK && resultPtr != IntPtr.Zero;
            }
        }

        /// <summary>
        /// Release an ADLX interface
        /// Decrements the reference count
        /// </summary>
        public static void ReleaseInterface(IntPtr pInterface)
        {
            if (pInterface == IntPtr.Zero)
                return;

            var iface = (IADLXInterface*)pInterface;
            iface->Release();
        }

        /// <summary>
        /// Add reference to an ADLX interface
        /// Increments the reference count
        /// </summary>
        public static void AddRefInterface(IntPtr pInterface)
        {
            if (pInterface == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pInterface));

            var iface = (IADLXInterface*)pInterface;
            iface->Acquire();
        }

        /// <summary>
        /// Helper to marshal ANSI string pointer to managed string
        /// </summary>
        internal static string MarshalString(byte* pStr)
        {
            if (pStr == null)
                return string.Empty;

            return Marshal.PtrToStringAnsi((IntPtr)pStr) ?? string.Empty;
        }

        internal static string MarshalString(sbyte** pStr)
        {
            if (pStr == null || *pStr == null)
                return string.Empty;

            return Marshal.PtrToStringAnsi((IntPtr)(*pStr)) ?? string.Empty;
        }
    }

    /// <summary>
    /// Helper methods for ADLX list operations.
    /// </summary>
    public static unsafe partial class ADLXListHelpers
    {
        /// <summary>
        /// Get the size of a list.
        /// </summary>
        public static uint GetListSize(IntPtr pList)
        {
            if (pList == IntPtr.Zero) throw new ArgumentNullException(nameof(pList));

            return ((IADLXList*)pList)->Size();
        }

        /// <summary>
        /// Check if a list is empty.
        /// </summary>
        public static bool IsListEmpty(IntPtr pList)
        {
            if (pList == IntPtr.Zero) throw new ArgumentNullException(nameof(pList));

            return ((IADLXList*)pList)->Empty();
        }

        /// <summary>
        /// Get item at specific index from list. Callers must dispose the returned pointer.
        /// </summary>
        public static IntPtr GetListItemAt(IntPtr pList, uint index)
        {
            if (pList == IntPtr.Zero) throw new ArgumentNullException(nameof(pList));

            IntPtr pItem;
            var result = ((IADLXList*)pList)->At(index, (IADLXInterface**)&pItem);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, $"Failed to get item at index {index}");
            }

            // Caller must wrap this in a ComPtr and dispose it.
            return pItem;
        }

        public static IntPtr[] ListToArray(IntPtr pList)
        {
            if (pList == IntPtr.Zero)
                return Array.Empty<IntPtr>();

            uint count = GetListSize(pList);
            if (count == 0)
                return Array.Empty<IntPtr>();

            var items = new IntPtr[count];
            for (uint i = 0; i < count; i++)
            {
                // GetListItemAt returns an AddRef'd pointer, so we need to release it later.
                // For now, this helper is kept for compatibility, but its usage should be replaced by ComPtr.
                items[i] = GetListItemAt(pList, i); 
            }

            return items;
        }
    }

    /// <summary> 
    /// A helper struct to manage the lifetime of a COM pointer from ADLX.
    /// It automatically calls Release() when disposed.
    /// </summary>
    /// <typeparam name="T">The type of the COM interface pointer.</typeparam>
    internal readonly unsafe struct ComPtr<T> : IDisposable where T : unmanaged
    {
        private readonly T* _ptr;

        public ComPtr(T* ptr)
        {
            _ptr = ptr;
        }

        public T* Get() => _ptr;

        public void Dispose()
        {
            if (_ptr != null)
            {
                ((IADLXInterface*)_ptr)->Release();
            }
        }

        // Allow implicit conversion to the raw pointer for convenience in calls
        public static implicit operator T*(ComPtr<T> comPtr) => comPtr._ptr;
    }
}
