using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSmartShiftEco : adlx::IADLXInterface")]
public unsafe partial struct IADLXSmartShiftEco
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXSmartShiftEco* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXSmartShiftEco* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXSmartShiftEco* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXSmartShiftEco* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabled(IADLXSmartShiftEco* pThis, [NativeTypeName("adlx_bool *")] bool* enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetEnabled(IADLXSmartShiftEco* pThis, [NativeTypeName("adlx_bool")] byte enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsInactive(IADLXSmartShiftEco* pThis, [NativeTypeName("adlx_bool *")] bool* inactive);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetInactiveReason(IADLXSmartShiftEco* pThis, ADLX_SMARTSHIFT_ECO_INACTIVE_REASON* reason);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXSmartShiftEco* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXSmartShiftEco* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXSmartShiftEco* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXSmartShiftEco* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        fixed (IADLXSmartShiftEco* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabled>((IntPtr)(lpVtbl[4]))(pThis, enabled);
        }
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        fixed (IADLXSmartShiftEco* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetEnabled>((IntPtr)(lpVtbl[5]))(pThis, enabled);
        }
    }

    public ADLX_RESULT IsInactive([NativeTypeName("adlx_bool *")] bool* inactive)
    {
        fixed (IADLXSmartShiftEco* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsInactive>((IntPtr)(lpVtbl[6]))(pThis, inactive);
        }
    }

    public ADLX_RESULT GetInactiveReason(ADLX_SMARTSHIFT_ECO_INACTIVE_REASON* reason)
    {
        fixed (IADLXSmartShiftEco* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetInactiveReason>((IntPtr)(lpVtbl[7]))(pThis, reason);
        }
    }
}
