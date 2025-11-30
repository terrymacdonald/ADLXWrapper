using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXPowerTuningServices : adlx::IADLXInterface")]
public unsafe partial struct IADLXPowerTuningServices
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXPowerTuningServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXPowerTuningServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXPowerTuningServices* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetPowerTuningChangedHandling(IADLXPowerTuningServices* pThis, IADLXPowerTuningChangedHandling** ppPowerTuningChangedHandling);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSmartShiftMax(IADLXPowerTuningServices* pThis, IADLXSmartShiftMax** ppSmartShiftMax);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXPowerTuningServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXPowerTuningServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXPowerTuningServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetPowerTuningChangedHandling(IADLXPowerTuningChangedHandling** ppPowerTuningChangedHandling)
    {
        fixed (IADLXPowerTuningServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetPowerTuningChangedHandling>((IntPtr)(lpVtbl[3]))(pThis, ppPowerTuningChangedHandling);
        }
    }

    public ADLX_RESULT GetSmartShiftMax(IADLXSmartShiftMax** ppSmartShiftMax)
    {
        fixed (IADLXPowerTuningServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSmartShiftMax>((IntPtr)(lpVtbl[4]))(pThis, ppSmartShiftMax);
        }
    }
}

public partial struct IADLXPowerTuningServices
{
}
