using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning"]/*' />
[NativeTypeName("struct IADLXManualFanTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualFanTuning
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int>)(lpVtbl[0]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int>)(lpVtbl[1]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.GetFanTuningRanges"]/*' />
    public ADLX_RESULT GetFanTuningRanges(ADLX_IntRange* speedRange, ADLX_IntRange* temperatureRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, ADLX_IntRange*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), speedRange, temperatureRange);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.GetFanTuningStates"]/*' />
    public ADLX_RESULT GetFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, IADLXManualFanTuningStateList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), ppStates);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.GetEmptyFanTuningStates"]/*' />
    public ADLX_RESULT GetEmptyFanTuningStates(IADLXManualFanTuningStateList** ppStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, IADLXManualFanTuningStateList**, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), ppStates);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.IsValidFanTuningStates"]/*' />
    public ADLX_RESULT IsValidFanTuningStates([NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, IADLXManualFanTuningStateList*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), pStates, errorIndex);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.SetFanTuningStates"]/*' />
    public ADLX_RESULT SetFanTuningStates([NativeTypeName("adlx::IADLXManualFanTuningStateList *")] IADLXManualFanTuningStateList* pStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, IADLXManualFanTuningStateList*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), pStates);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.IsSupportedZeroRPM"]/*' />
    public ADLX_RESULT IsSupportedZeroRPM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.GetZeroRPMState"]/*' />
    public ADLX_RESULT GetZeroRPMState([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), isSet);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.SetZeroRPMState"]/*' />
    public ADLX_RESULT SetZeroRPMState([NativeTypeName("adlx_bool")] byte set)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, byte, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), set);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.IsSupportedMinAcousticLimit"]/*' />
    public ADLX_RESULT IsSupportedMinAcousticLimit([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.GetMinAcousticLimitRange"]/*' />
    public ADLX_RESULT GetMinAcousticLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[12]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.GetMinAcousticLimit"]/*' />
    public ADLX_RESULT GetMinAcousticLimit([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.SetMinAcousticLimit"]/*' />
    public ADLX_RESULT SetMinAcousticLimit([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int, ADLX_RESULT>)(lpVtbl[14]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.IsSupportedMinFanSpeed"]/*' />
    public ADLX_RESULT IsSupportedMinFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, bool*, ADLX_RESULT>)(lpVtbl[15]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.GetMinFanSpeedRange"]/*' />
    public ADLX_RESULT GetMinFanSpeedRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[16]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.GetMinFanSpeed"]/*' />
    public ADLX_RESULT GetMinFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.SetMinFanSpeed"]/*' />
    public ADLX_RESULT SetMinFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int, ADLX_RESULT>)(lpVtbl[18]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.IsSupportedTargetFanSpeed"]/*' />
    public ADLX_RESULT IsSupportedTargetFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, bool*, ADLX_RESULT>)(lpVtbl[19]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.GetTargetFanSpeedRange"]/*' />
    public ADLX_RESULT GetTargetFanSpeedRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[20]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.GetTargetFanSpeed"]/*' />
    public ADLX_RESULT GetTargetFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int*, ADLX_RESULT>)(lpVtbl[21]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualFanTuning.xml' path='doc/member[@name="IADLXManualFanTuning.SetTargetFanSpeed"]/*' />
    public ADLX_RESULT SetTargetFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuning*, int, ADLX_RESULT>)(lpVtbl[22]))((IADLXManualFanTuning*)Unsafe.AsPointer(ref this), value);
    }
}
