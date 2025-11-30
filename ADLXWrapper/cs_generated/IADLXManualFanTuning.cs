using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualFanTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualFanTuning
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualFanTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualFanTuning* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualFanTuning* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFanTuningRanges(IADLXManualFanTuning* pThis, ADLX_IntRange* speedRange, ADLX_IntRange* temperatureRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFanTuningStates(IADLXManualFanTuning* pThis, IADLXManualFanTuningStateList** ppStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetEmptyFanTuningStates(IADLXManualFanTuning* pThis, IADLXManualFanTuningStateList** ppStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsValidFanTuningStates(IADLXManualFanTuning* pThis, [NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates, [NativeTypeName("adlx_int *")] int* errorIndex);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetFanTuningStates(IADLXManualFanTuning* pThis, [NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedZeroRPM(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetZeroRPMState(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetZeroRPMState(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_bool")] byte set);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedMinAcousticLimit(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinAcousticLimitRange(IADLXManualFanTuning* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinAcousticLimit(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_int *")] int* value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMinAcousticLimit(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_int")] int value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedMinFanSpeed(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinFanSpeedRange(IADLXManualFanTuning* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinFanSpeed(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_int *")] int* value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMinFanSpeed(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_int")] int value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedTargetFanSpeed(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTargetFanSpeedRange(IADLXManualFanTuning* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTargetFanSpeed(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_int *")] int* value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetTargetFanSpeed(IADLXManualFanTuning* pThis, [NativeTypeName("adlx_int")] int value);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetFanTuningRanges(ADLX_IntRange* speedRange, ADLX_IntRange* temperatureRange)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFanTuningRanges>((IntPtr)(lpVtbl[3]))(pThis, speedRange, temperatureRange);
        }
    }

    public ADLX_RESULT GetFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFanTuningStates>((IntPtr)(lpVtbl[4]))(pThis, ppStates);
        }
    }

    public ADLX_RESULT GetEmptyFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetEmptyFanTuningStates>((IntPtr)(lpVtbl[5]))(pThis, ppStates);
        }
    }

    public ADLX_RESULT IsValidFanTuningStates([NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsValidFanTuningStates>((IntPtr)(lpVtbl[6]))(pThis, pStates, errorIndex);
        }
    }

    public ADLX_RESULT SetFanTuningStates([NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetFanTuningStates>((IntPtr)(lpVtbl[7]))(pThis, pStates);
        }
    }

    public ADLX_RESULT IsSupportedZeroRPM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedZeroRPM>((IntPtr)(lpVtbl[8]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetZeroRPMState([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetZeroRPMState>((IntPtr)(lpVtbl[9]))(pThis, isSet);
        }
    }

    public ADLX_RESULT SetZeroRPMState([NativeTypeName("adlx_bool")] byte set)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetZeroRPMState>((IntPtr)(lpVtbl[10]))(pThis, set);
        }
    }

    public ADLX_RESULT IsSupportedMinAcousticLimit([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedMinAcousticLimit>((IntPtr)(lpVtbl[11]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetMinAcousticLimitRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinAcousticLimitRange>((IntPtr)(lpVtbl[12]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetMinAcousticLimit([NativeTypeName("adlx_int *")] int* value)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinAcousticLimit>((IntPtr)(lpVtbl[13]))(pThis, value);
        }
    }

    public ADLX_RESULT SetMinAcousticLimit([NativeTypeName("adlx_int")] int value)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMinAcousticLimit>((IntPtr)(lpVtbl[14]))(pThis, value);
        }
    }

    public ADLX_RESULT IsSupportedMinFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedMinFanSpeed>((IntPtr)(lpVtbl[15]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetMinFanSpeedRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinFanSpeedRange>((IntPtr)(lpVtbl[16]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetMinFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinFanSpeed>((IntPtr)(lpVtbl[17]))(pThis, value);
        }
    }

    public ADLX_RESULT SetMinFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMinFanSpeed>((IntPtr)(lpVtbl[18]))(pThis, value);
        }
    }

    public ADLX_RESULT IsSupportedTargetFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedTargetFanSpeed>((IntPtr)(lpVtbl[19]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetTargetFanSpeedRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTargetFanSpeedRange>((IntPtr)(lpVtbl[20]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetTargetFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTargetFanSpeed>((IntPtr)(lpVtbl[21]))(pThis, value);
        }
    }

    public ADLX_RESULT SetTargetFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        fixed (IADLXManualFanTuning* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetTargetFanSpeed>((IntPtr)(lpVtbl[22]))(pThis, value);
        }
    }
}
