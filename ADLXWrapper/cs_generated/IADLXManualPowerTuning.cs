using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualPowerTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualPowerTuning
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualPowerTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualPowerTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualPowerTuning* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetPowerLimitRange(IADLXManualPowerTuning* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetPowerLimit(IADLXManualPowerTuning* pThis, [NativeTypeName("adlx_int *")] int* curVal);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetPowerLimit(IADLXManualPowerTuning* pThis, [NativeTypeName("adlx_int")] int curVal);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedTDCLimit(IADLXManualPowerTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTDCLimitRange(IADLXManualPowerTuning* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTDCLimit(IADLXManualPowerTuning* pThis, [NativeTypeName("adlx_int *")] int* curVal);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetTDCLimit(IADLXManualPowerTuning* pThis, [NativeTypeName("adlx_int")] int curVal);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualPowerTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualPowerTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualPowerTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetPowerLimitRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualPowerTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetPowerLimitRange>((IntPtr)(lpVtbl[3]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetPowerLimit([NativeTypeName("adlx_int *")] int* curVal)
    {
        fixed (IADLXManualPowerTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetPowerLimit>((IntPtr)(lpVtbl[4]))(pThis, curVal);
        }
    }

    public ADLX_RESULT SetPowerLimit([NativeTypeName("adlx_int")] int curVal)
    {
        fixed (IADLXManualPowerTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetPowerLimit>((IntPtr)(lpVtbl[5]))(pThis, curVal);
        }
    }

    public ADLX_RESULT IsSupportedTDCLimit([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualPowerTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedTDCLimit>((IntPtr)(lpVtbl[6]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetTDCLimitRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualPowerTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTDCLimitRange>((IntPtr)(lpVtbl[7]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetTDCLimit([NativeTypeName("adlx_int *")] int* curVal)
    {
        fixed (IADLXManualPowerTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTDCLimit>((IntPtr)(lpVtbl[8]))(pThis, curVal);
        }
    }

    public ADLX_RESULT SetTDCLimit([NativeTypeName("adlx_int")] int curVal)
    {
        fixed (IADLXManualPowerTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetTDCLimit>((IntPtr)(lpVtbl[9]))(pThis, curVal);
        }
    }
}
