using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualFanTuning1.xml' path='doc/member[@name="IADLXManualFanTuning1"]/*' />
[NativeTypeName("struct IADLXManualFanTuning1 : adlx::IADLXManualFanTuning")]
public unsafe partial struct IADLXManualFanTuning1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int>)(lpVtbl[0]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int>)(lpVtbl[1]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.GetFanTuningRanges" />
    public ADLX_RESULT GetFanTuningRanges(ADLX_IntRange* speedRange, ADLX_IntRange* temperatureRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, ADLX_IntRange*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), speedRange, temperatureRange);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.GetFanTuningStates" />
    public ADLX_RESULT GetFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, IADLXManualFanTuningStateList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), ppStates);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.GetEmptyFanTuningStates" />
    public ADLX_RESULT GetEmptyFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, IADLXManualFanTuningStateList**, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), ppStates);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.IsValidFanTuningStates" />
    public ADLX_RESULT IsValidFanTuningStates([NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, IADLXManualFanTuningStateList*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), pStates, errorIndex);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.SetFanTuningStates" />
    public ADLX_RESULT SetFanTuningStates([NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, IADLXManualFanTuningStateList*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), pStates);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.IsSupportedZeroRPM" />
    public ADLX_RESULT IsSupportedZeroRPM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.GetZeroRPMState" />
    public ADLX_RESULT GetZeroRPMState([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), isSet);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.SetZeroRPMState" />
    public ADLX_RESULT SetZeroRPMState([NativeTypeName("adlx_bool")] byte set)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, byte, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), set);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.IsSupportedMinAcousticLimit" />
    public ADLX_RESULT IsSupportedMinAcousticLimit([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.GetMinAcousticLimitRange" />
    public ADLX_RESULT GetMinAcousticLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[12]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.GetMinAcousticLimit" />
    public ADLX_RESULT GetMinAcousticLimit([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), value);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.SetMinAcousticLimit" />
    public ADLX_RESULT SetMinAcousticLimit([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int, ADLX_RESULT>)(lpVtbl[14]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), value);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.IsSupportedMinFanSpeed" />
    public ADLX_RESULT IsSupportedMinFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, bool*, ADLX_RESULT>)(lpVtbl[15]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.GetMinFanSpeedRange" />
    public ADLX_RESULT GetMinFanSpeedRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[16]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.GetMinFanSpeed" />
    public ADLX_RESULT GetMinFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), value);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.SetMinFanSpeed" />
    public ADLX_RESULT SetMinFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int, ADLX_RESULT>)(lpVtbl[18]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), value);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.IsSupportedTargetFanSpeed" />
    public ADLX_RESULT IsSupportedTargetFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, bool*, ADLX_RESULT>)(lpVtbl[19]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.GetTargetFanSpeedRange" />
    public ADLX_RESULT GetTargetFanSpeedRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[20]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.GetTargetFanSpeed" />
    public ADLX_RESULT GetTargetFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int*, ADLX_RESULT>)(lpVtbl[21]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), value);
    }

    /// <inheritdoc cref="IADLXManualFanTuning.SetTargetFanSpeed" />
    public ADLX_RESULT SetTargetFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int, ADLX_RESULT>)(lpVtbl[22]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualFanTuning1.xml' path='doc/member[@name="IADLXManualFanTuning1.GetDefaultFanTuningStates"]/*' />
    public ADLX_RESULT GetDefaultFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, IADLXManualFanTuningStateList**, ADLX_RESULT>)(lpVtbl[23]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), ppStates);
    }

    /// <include file='IADLXManualFanTuning1.xml' path='doc/member[@name="IADLXManualFanTuning1.GetMinAcousticLimitDefault"]/*' />
    public ADLX_RESULT GetMinAcousticLimitDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int*, ADLX_RESULT>)(lpVtbl[24]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), defaultVal);
    }

    /// <include file='IADLXManualFanTuning1.xml' path='doc/member[@name="IADLXManualFanTuning1.GetMinFanSpeedDefault"]/*' />
    public ADLX_RESULT GetMinFanSpeedDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int*, ADLX_RESULT>)(lpVtbl[25]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), defaultVal);
    }

    /// <include file='IADLXManualFanTuning1.xml' path='doc/member[@name="IADLXManualFanTuning1.GetTargetFanSpeedDefault"]/*' />
    public ADLX_RESULT GetTargetFanSpeedDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, int*, ADLX_RESULT>)(lpVtbl[26]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), defaultVal);
    }

    /// <include file='IADLXManualFanTuning1.xml' path='doc/member[@name="IADLXManualFanTuning1.GetDefaultZeroRPMState"]/*' />
    public ADLX_RESULT GetDefaultZeroRPMState([NativeTypeName("adlx_bool *")] bool* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning1*, bool*, ADLX_RESULT>)(lpVtbl[27]))((IADLXManualFanTuning1*)Unsafe.AsPointer(ref this), defaultVal);
    }
}
