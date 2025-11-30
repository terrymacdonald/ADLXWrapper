using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualFanTuning1 : adlx::IADLXManualFanTuning")]
public unsafe partial struct IADLXManualFanTuning1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualFanTuning1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualFanTuning1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualFanTuning1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFanTuningRanges(IADLXManualFanTuning1* pThis, ADLX_IntRange* speedRange, ADLX_IntRange* temperatureRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFanTuningStates(IADLXManualFanTuning1* pThis, IADLXManualFanTuningStateList** ppStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetEmptyFanTuningStates(IADLXManualFanTuning1* pThis, IADLXManualFanTuningStateList** ppStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsValidFanTuningStates(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates, [NativeTypeName("adlx_int *")] int* errorIndex);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetFanTuningStates(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedZeroRPM(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetZeroRPMState(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetZeroRPMState(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_bool")] byte set);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedMinAcousticLimit(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinAcousticLimitRange(IADLXManualFanTuning1* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinAcousticLimit(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_int *")] int* value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMinAcousticLimit(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_int")] int value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedMinFanSpeed(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinFanSpeedRange(IADLXManualFanTuning1* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinFanSpeed(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_int *")] int* value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMinFanSpeed(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_int")] int value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedTargetFanSpeed(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTargetFanSpeedRange(IADLXManualFanTuning1* pThis, ADLX_IntRange* tuningRange);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTargetFanSpeed(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_int *")] int* value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetTargetFanSpeed(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_int")] int value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDefaultFanTuningStates(IADLXManualFanTuning1* pThis, IADLXManualFanTuningStateList** ppStates);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinAcousticLimitDefault(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_int *")] int* defaultVal);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMinFanSpeedDefault(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_int *")] int* defaultVal);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTargetFanSpeedDefault(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_int *")] int* defaultVal);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDefaultZeroRPMState(IADLXManualFanTuning1* pThis, [NativeTypeName("adlx_bool *")] bool* defaultVal);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetFanTuningRanges(ADLX_IntRange* speedRange, ADLX_IntRange* temperatureRange)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFanTuningRanges>((IntPtr)(lpVtbl[3]))(pThis, speedRange, temperatureRange);
        }
    }

    public ADLX_RESULT GetFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFanTuningStates>((IntPtr)(lpVtbl[4]))(pThis, ppStates);
        }
    }

    public ADLX_RESULT GetEmptyFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetEmptyFanTuningStates>((IntPtr)(lpVtbl[5]))(pThis, ppStates);
        }
    }

    public ADLX_RESULT IsValidFanTuningStates([NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsValidFanTuningStates>((IntPtr)(lpVtbl[6]))(pThis, pStates, errorIndex);
        }
    }

    public ADLX_RESULT SetFanTuningStates([NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetFanTuningStates>((IntPtr)(lpVtbl[7]))(pThis, pStates);
        }
    }

    public ADLX_RESULT IsSupportedZeroRPM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedZeroRPM>((IntPtr)(lpVtbl[8]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetZeroRPMState([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetZeroRPMState>((IntPtr)(lpVtbl[9]))(pThis, isSet);
        }
    }

    public ADLX_RESULT SetZeroRPMState([NativeTypeName("adlx_bool")] byte set)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetZeroRPMState>((IntPtr)(lpVtbl[10]))(pThis, set);
        }
    }

    public ADLX_RESULT IsSupportedMinAcousticLimit([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedMinAcousticLimit>((IntPtr)(lpVtbl[11]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetMinAcousticLimitRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinAcousticLimitRange>((IntPtr)(lpVtbl[12]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetMinAcousticLimit([NativeTypeName("adlx_int *")] int* value)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinAcousticLimit>((IntPtr)(lpVtbl[13]))(pThis, value);
        }
    }

    public ADLX_RESULT SetMinAcousticLimit([NativeTypeName("adlx_int")] int value)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMinAcousticLimit>((IntPtr)(lpVtbl[14]))(pThis, value);
        }
    }

    public ADLX_RESULT IsSupportedMinFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedMinFanSpeed>((IntPtr)(lpVtbl[15]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetMinFanSpeedRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinFanSpeedRange>((IntPtr)(lpVtbl[16]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetMinFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinFanSpeed>((IntPtr)(lpVtbl[17]))(pThis, value);
        }
    }

    public ADLX_RESULT SetMinFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMinFanSpeed>((IntPtr)(lpVtbl[18]))(pThis, value);
        }
    }

    public ADLX_RESULT IsSupportedTargetFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedTargetFanSpeed>((IntPtr)(lpVtbl[19]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetTargetFanSpeedRange(ADLX_IntRange* tuningRange)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTargetFanSpeedRange>((IntPtr)(lpVtbl[20]))(pThis, tuningRange);
        }
    }

    public ADLX_RESULT GetTargetFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTargetFanSpeed>((IntPtr)(lpVtbl[21]))(pThis, value);
        }
    }

    public ADLX_RESULT SetTargetFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetTargetFanSpeed>((IntPtr)(lpVtbl[22]))(pThis, value);
        }
    }

    public ADLX_RESULT GetDefaultFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDefaultFanTuningStates>((IntPtr)(lpVtbl[23]))(pThis, ppStates);
        }
    }

    public ADLX_RESULT GetMinAcousticLimitDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinAcousticLimitDefault>((IntPtr)(lpVtbl[24]))(pThis, defaultVal);
        }
    }

    public ADLX_RESULT GetMinFanSpeedDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMinFanSpeedDefault>((IntPtr)(lpVtbl[25]))(pThis, defaultVal);
        }
    }

    public ADLX_RESULT GetTargetFanSpeedDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTargetFanSpeedDefault>((IntPtr)(lpVtbl[26]))(pThis, defaultVal);
        }
    }

    public ADLX_RESULT GetDefaultZeroRPMState([NativeTypeName("adlx_bool *")] bool* defaultVal)
    {
        fixed (IADLXManualFanTuning1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDefaultZeroRPMState>((IntPtr)(lpVtbl[27]))(pThis, defaultVal);
        }
    }
}
