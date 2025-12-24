using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualGraphicsTuning2_1.xml' path='doc/member[@name="IADLXManualGraphicsTuning2_1"]/*' />
[NativeTypeName("struct IADLXManualGraphicsTuning2_1 : adlx::IADLXManualGraphicsTuning2")]
public unsafe partial struct IADLXManualGraphicsTuning2_1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int>)(lpVtbl[0]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int>)(lpVtbl[1]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXManualGraphicsTuning2.GetGPUMinFrequencyRange" />
    public ADLX_RESULT GetGPUMinFrequencyRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <inheritdoc cref="IADLXManualGraphicsTuning2.GetGPUMinFrequency" />
    public ADLX_RESULT GetGPUMinFrequency([NativeTypeName("adlx_int *")] int* minFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), minFreq);
    }

    /// <inheritdoc cref="IADLXManualGraphicsTuning2.SetGPUMinFrequency" />
    public ADLX_RESULT SetGPUMinFrequency([NativeTypeName("adlx_int")] int minFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), minFreq);
    }

    /// <inheritdoc cref="IADLXManualGraphicsTuning2.GetGPUMaxFrequencyRange" />
    public ADLX_RESULT GetGPUMaxFrequencyRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <inheritdoc cref="IADLXManualGraphicsTuning2.GetGPUMaxFrequency" />
    public ADLX_RESULT GetGPUMaxFrequency([NativeTypeName("adlx_int *")] int* maxFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), maxFreq);
    }

    /// <inheritdoc cref="IADLXManualGraphicsTuning2.SetGPUMaxFrequency" />
    public ADLX_RESULT SetGPUMaxFrequency([NativeTypeName("adlx_int")] int maxFreq)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), maxFreq);
    }

    /// <inheritdoc cref="IADLXManualGraphicsTuning2.GetGPUVoltageRange" />
    public ADLX_RESULT GetGPUVoltageRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <inheritdoc cref="IADLXManualGraphicsTuning2.GetGPUVoltage" />
    public ADLX_RESULT GetGPUVoltage([NativeTypeName("adlx_int *")] int* volt)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), volt);
    }

    /// <inheritdoc cref="IADLXManualGraphicsTuning2.SetGPUVoltage" />
    public ADLX_RESULT SetGPUVoltage([NativeTypeName("adlx_int")] int volt)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), volt);
    }

    /// <include file='IADLXManualGraphicsTuning2_1.xml' path='doc/member[@name="IADLXManualGraphicsTuning2_1.GetGPUMinFrequencyDefault"]/*' />
    public ADLX_RESULT GetGPUMinFrequencyDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int*, ADLX_RESULT>)(lpVtbl[12]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), defaultVal);
    }

    /// <include file='IADLXManualGraphicsTuning2_1.xml' path='doc/member[@name="IADLXManualGraphicsTuning2_1.GetGPUMaxFrequencyDefault"]/*' />
    public ADLX_RESULT GetGPUMaxFrequencyDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), defaultVal);
    }

    /// <include file='IADLXManualGraphicsTuning2_1.xml' path='doc/member[@name="IADLXManualGraphicsTuning2_1.GetGPUVoltageDefault"]/*' />
    public ADLX_RESULT GetGPUVoltageDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning2_1*, int*, ADLX_RESULT>)(lpVtbl[14]))((IADLXManualGraphicsTuning2_1*)Unsafe.AsPointer(ref this), defaultVal);
    }
}
