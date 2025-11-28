using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    [NativeTypeName("struct IADLXGPUsChangedHandling : adlx::IADLXInterface")]
    public unsafe partial struct IADLXGPUsChangedHandling
    {
        public void** lpVtbl;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_long")]
        public delegate int _Acquire(IADLXGPUsChangedHandling* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_long")]
        public delegate int _Release(IADLXGPUsChangedHandling* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _QueryInterface(IADLXGPUsChangedHandling* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _AddGPUsListEventListener(IADLXGPUsChangedHandling* pThis, [NativeTypeName("adlx::IADLXGPUsEventListener *")] IADLXGPUsEventListener* pListener);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _RemoveGPUsListEventListener(IADLXGPUsChangedHandling* pThis, [NativeTypeName("adlx::IADLXGPUsEventListener *")] IADLXGPUsEventListener* pListener);

        [return: NativeTypeName("const wchar_t *")]
        public static ushort* IID()
        {
            return "IADLXInterface";
        }

        [return: NativeTypeName("adlx_long")]
        public int Acquire()
        {
            fixed (IADLXGPUsChangedHandling* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
            }
        }

        [return: NativeTypeName("adlx_long")]
        public int Release()
        {
            fixed (IADLXGPUsChangedHandling* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
            }
        }

        public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
        {
            fixed (IADLXGPUsChangedHandling* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
            }
        }

        public ADLX_RESULT AddGPUsListEventListener([NativeTypeName("adlx::IADLXGPUsEventListener *")] IADLXGPUsEventListener* pListener)
        {
            fixed (IADLXGPUsChangedHandling* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_AddGPUsListEventListener>((IntPtr)(lpVtbl[3]))(pThis, pListener);
            }
        }

        public ADLX_RESULT RemoveGPUsListEventListener([NativeTypeName("adlx::IADLXGPUsEventListener *")] IADLXGPUsEventListener* pListener)
        {
            fixed (IADLXGPUsChangedHandling* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_RemoveGPUsListEventListener>((IntPtr)(lpVtbl[4]))(pThis, pListener);
            }
        }
    }
}
