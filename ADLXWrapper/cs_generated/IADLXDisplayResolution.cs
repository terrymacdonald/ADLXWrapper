using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayResolution : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayResolution
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayResolution* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayResolution* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayResolution* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetValue(IADLXDisplayResolution* pThis, ADLX_CustomResolution* customResolution);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetValue(IADLXDisplayResolution* pThis, ADLX_CustomResolution customResolution);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetValue(ADLX_CustomResolution* customResolution)
    {
        fixed (IADLXDisplayResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetValue>((IntPtr)(lpVtbl[3]))(pThis, customResolution);
        }
    }

    public ADLX_RESULT SetValue(ADLX_CustomResolution customResolution)
    {
        fixed (IADLXDisplayResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetValue>((IntPtr)(lpVtbl[4]))(pThis, customResolution);
        }
    }
}
