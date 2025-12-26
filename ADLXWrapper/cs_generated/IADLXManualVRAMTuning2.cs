using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualVRAMTuning2.xml' path='doc/member[@name="IADLXManualVRAMTuning2"]/*' />
[NativeTypeName("struct IADLXManualVRAMTuning2 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualVRAMTuning2
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2*, int>)(lpVtbl[0]))((IADLXManualVRAMTuning2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2*, int>)(lpVtbl[1]))((IADLXManualVRAMTuning2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualVRAMTuning2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXManualVRAMTuning2.xml' path='doc/member[@name="IADLXManualVRAMTuning2.IsSupportedMemoryTiming"]/*' />
    public ADLX_RESULT IsSupportedMemoryTiming([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualVRAMTuning2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXManualVRAMTuning2.xml' path='doc/member[@name="IADLXManualVRAMTuning2.GetSupportedMemoryTimingDescriptionList"]/*' />
    public ADLX_RESULT GetSupportedMemoryTimingDescriptionList(IADLXMemoryTimingDescriptionList** ppDescriptionList)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2*, IADLXMemoryTimingDescriptionList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualVRAMTuning2*)Unsafe.AsPointer(ref this), ppDescriptionList);
    }

    /// <include file='IADLXManualVRAMTuning2.xml' path='doc/member[@name="IADLXManualVRAMTuning2.GetMemoryTimingDescription"]/*' />
    public ADLX_RESULT GetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION* description)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2*, ADLX_MEMORYTIMING_DESCRIPTION*, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualVRAMTuning2*)Unsafe.AsPointer(ref this), description);
    }

    /// <include file='IADLXManualVRAMTuning2.xml' path='doc/member[@name="IADLXManualVRAMTuning2.SetMemoryTimingDescription"]/*' />
    public ADLX_RESULT SetMemoryTimingDescription(ADLX_MEMORYTIMING_DESCRIPTION description)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2*, ADLX_MEMORYTIMING_DESCRIPTION, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualVRAMTuning2*)Unsafe.AsPointer(ref this), description);
    }

    /// <include file='IADLXManualVRAMTuning2.xml' path='doc/member[@name="IADLXManualVRAMTuning2.GetMaxVRAMFrequencyRange"]/*' />
    public ADLX_RESULT GetMaxVRAMFrequencyRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualVRAMTuning2*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <include file='IADLXManualVRAMTuning2.xml' path='doc/member[@name="IADLXManualVRAMTuning2.GetMaxVRAMFrequency"]/*' />
    public ADLX_RESULT GetMaxVRAMFrequency([NativeTypeName("adlx_int *")] int* freq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualVRAMTuning2*)Unsafe.AsPointer(ref this), freq);
    }

    /// <include file='IADLXManualVRAMTuning2.xml' path='doc/member[@name="IADLXManualVRAMTuning2.SetMaxVRAMFrequency"]/*' />
    public ADLX_RESULT SetMaxVRAMFrequency([NativeTypeName("adlx_int")] int freq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualVRAMTuning2*, int, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualVRAMTuning2*)Unsafe.AsPointer(ref this), freq);
    }
}
