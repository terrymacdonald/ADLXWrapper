using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualGraphicsTuning2.xml' path='doc/member[@name="IADLXManualGraphicsTuning2"]/*' />
[NativeTypeName("struct IADLXManualGraphicsTuning2 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualGraphicsTuning2
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int>)(lpVtbl[0]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int>)(lpVtbl[1]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXManualGraphicsTuning2.xml' path='doc/member[@name="IADLXManualGraphicsTuning2.GetGPUMinFrequencyRange"]/*' />
    public ADLX_RESULT GetGPUMinFrequencyRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <include file='IADLXManualGraphicsTuning2.xml' path='doc/member[@name="IADLXManualGraphicsTuning2.GetGPUMinFrequency"]/*' />
    public ADLX_RESULT GetGPUMinFrequency([NativeTypeName("adlx_int *")] int* minFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), minFreq);
    }

    /// <include file='IADLXManualGraphicsTuning2.xml' path='doc/member[@name="IADLXManualGraphicsTuning2.SetGPUMinFrequency"]/*' />
    public ADLX_RESULT SetGPUMinFrequency([NativeTypeName("adlx_int")] int minFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), minFreq);
    }

    /// <include file='IADLXManualGraphicsTuning2.xml' path='doc/member[@name="IADLXManualGraphicsTuning2.GetGPUMaxFrequencyRange"]/*' />
    public ADLX_RESULT GetGPUMaxFrequencyRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <include file='IADLXManualGraphicsTuning2.xml' path='doc/member[@name="IADLXManualGraphicsTuning2.GetGPUMaxFrequency"]/*' />
    public ADLX_RESULT GetGPUMaxFrequency([NativeTypeName("adlx_int *")] int* maxFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), maxFreq);
    }

    /// <include file='IADLXManualGraphicsTuning2.xml' path='doc/member[@name="IADLXManualGraphicsTuning2.SetGPUMaxFrequency"]/*' />
    public ADLX_RESULT SetGPUMaxFrequency([NativeTypeName("adlx_int")] int maxFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), maxFreq);
    }

    /// <include file='IADLXManualGraphicsTuning2.xml' path='doc/member[@name="IADLXManualGraphicsTuning2.GetGPUVoltageRange"]/*' />
    public ADLX_RESULT GetGPUVoltageRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <include file='IADLXManualGraphicsTuning2.xml' path='doc/member[@name="IADLXManualGraphicsTuning2.GetGPUVoltage"]/*' />
    public ADLX_RESULT GetGPUVoltage([NativeTypeName("adlx_int *")] int* volt)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), volt);
    }

    /// <include file='IADLXManualGraphicsTuning2.xml' path='doc/member[@name="IADLXManualGraphicsTuning2.SetGPUVoltage"]/*' />
    public ADLX_RESULT SetGPUVoltage([NativeTypeName("adlx_int")] int volt)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2*, int, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualGraphicsTuning2*)Unsafe.AsPointer(ref this), volt);
    }
}
