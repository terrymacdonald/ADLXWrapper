using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    public unsafe partial struct IADLXInterface
    {
        public void** lpVtbl;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_long")]
        public delegate int _Acquire(IADLXInterface* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_long")]
        public delegate int _Release(IADLXInterface* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _QueryInterface(IADLXInterface* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

        [return: NativeTypeName("const wchar_t *")]
        public static ushort* IID()
        {
            return "IADLXInterface";
        }

        [return: NativeTypeName("adlx_long")]
        public int Acquire()
        {
            fixed (IADLXInterface* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
            }
        }

        [return: NativeTypeName("adlx_long")]
        public int Release()
        {
            fixed (IADLXInterface* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
            }
        }

        public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
        {
            fixed (IADLXInterface* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
            }
        }
    }
}
