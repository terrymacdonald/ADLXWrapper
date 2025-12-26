using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualVRAMTuning1.xml' path='doc/member[@name="IADLXManualVRAMTuning1"]/*' />
[NativeTypeName("struct IADLXManualVRAMTuning1 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualVRAMTuning1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, int>)(lpVtbl[0]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, int>)(lpVtbl[1]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXManualVRAMTuning1.xml' path='doc/member[@name="IADLXManualVRAMTuning1.IsSupportedMemoryTiming"]/*' />
    public ADLX_RESULT IsSupportedMemoryTiming([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXManualVRAMTuning1.xml' path='doc/member[@name="IADLXManualVRAMTuning1.GetSupportedMemoryTimingDescriptionList"]/*' />
    public ADLX_RESULT GetSupportedMemoryTimingDescriptionList(IADLXMemoryTimingDescriptionList** ppDescriptionList)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, IADLXMemoryTimingDescriptionList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), ppDescriptionList);
    }

    /// <include file='IADLXManualVRAMTuning1.xml' path='doc/member[@name="IADLXManualVRAMTuning1.GetMemoryTimingDescription"]/*' />
    public ADLX_RESULT GetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION* description)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, ADLX_MEMORYTIMING_DESCRIPTION*, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), description);
    }

    /// <include file='IADLXManualVRAMTuning1.xml' path='doc/member[@name="IADLXManualVRAMTuning1.SetMemoryTimingDescription"]/*' />
    public ADLX_RESULT SetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION description)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, ADLX_MEMORYTIMING_DESCRIPTION, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), description);
    }

    /// <include file='IADLXManualVRAMTuning1.xml' path='doc/member[@name="IADLXManualVRAMTuning1.GetVRAMTuningRanges"]/*' />
    public ADLX_RESULT GetVRAMTuningRanges(ADLX_IntRange* frequencyRange, ADLX_IntRange* voltageRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, ADLX_IntRange*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), frequencyRange, voltageRange);
    }

    /// <include file='IADLXManualVRAMTuning1.xml' path='doc/member[@name="IADLXManualVRAMTuning1.GetVRAMTuningStates"]/*' />
    public ADLX_RESULT GetVRAMTuningStates(IADLXManualTuningStateList** ppVRAMStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, IADLXManualTuningStateList**, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), ppVRAMStates);
    }

    /// <include file='IADLXManualVRAMTuning1.xml' path='doc/member[@name="IADLXManualVRAMTuning1.GetEmptyVRAMTuningStates"]/*' />
    public ADLX_RESULT GetEmptyVRAMTuningStates(IADLXManualTuningStateList** ppVRAMStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, IADLXManualTuningStateList**, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), ppVRAMStates);
    }

    /// <include file='IADLXManualVRAMTuning1.xml' path='doc/member[@name="IADLXManualVRAMTuning1.IsValidVRAMTuningStates"]/*' />
    public ADLX_RESULT IsValidVRAMTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pVRAMStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, IADLXManualTuningStateList*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), pVRAMStates, errorIndex);
    }

    /// <include file='IADLXManualVRAMTuning1.xml' path='doc/member[@name="IADLXManualVRAMTuning1.SetVRAMTuningStates"]/*' />
    public ADLX_RESULT SetVRAMTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pVRAMStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning1*, IADLXManualTuningStateList*, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualVRAMTuning1*)Unsafe.AsPointer(ref this), pVRAMStates);
    }
}
