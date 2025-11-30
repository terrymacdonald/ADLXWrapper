using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXMemoryTimingDescription : adlx::IADLXInterface")]
public unsafe partial struct IADLXMemoryTimingDescription
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXMemoryTimingDescription* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXMemoryTimingDescription* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXMemoryTimingDescription* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDescription(IADLXMemoryTimingDescription* pThis, ADLX_MEMORYTIMING_DESCRIPTION* description);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXMemoryTimingDescription* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXMemoryTimingDescription* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXMemoryTimingDescription* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetDescription(ADLX_MEMORYTIMING_DESCRIPTION* description)
    {
        fixed (IADLXMemoryTimingDescription* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDescription>((IntPtr)(lpVtbl[3]))(pThis, description);
        }
    }
}
