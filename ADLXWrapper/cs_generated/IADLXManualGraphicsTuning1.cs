using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualGraphicsTuning1.xml' path='doc/member[@name="IADLXManualGraphicsTuning1"]/*' />
[NativeTypeName("struct IADLXManualGraphicsTuning1 : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualGraphicsTuning1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, int>)(lpVtbl[0]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, int>)(lpVtbl[1]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXManualGraphicsTuning1.xml' path='doc/member[@name="IADLXManualGraphicsTuning1.GetGPUTuningRanges"]/*' />
    public ADLX_RESULT GetGPUTuningRanges(ADLX_IntRange* frequencyRange, ADLX_IntRange* voltageRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, ADLX_IntRange*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), frequencyRange, voltageRange);
    }

    /// <include file='IADLXManualGraphicsTuning1.xml' path='doc/member[@name="IADLXManualGraphicsTuning1.GetGPUTuningStates"]/*' />
    public ADLX_RESULT GetGPUTuningStates(IADLXManualTuningStateList** ppGFXStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, IADLXManualTuningStateList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), ppGFXStates);
    }

    /// <include file='IADLXManualGraphicsTuning1.xml' path='doc/member[@name="IADLXManualGraphicsTuning1.GetEmptyGPUTuningStates"]/*' />
    public ADLX_RESULT GetEmptyGPUTuningStates(IADLXManualTuningStateList** ppGFXStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, IADLXManualTuningStateList**, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), ppGFXStates);
    }

    /// <include file='IADLXManualGraphicsTuning1.xml' path='doc/member[@name="IADLXManualGraphicsTuning1.IsValidGPUTuningStates"]/*' />
    public ADLX_RESULT IsValidGPUTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pGFXStates, [NativeTypeName("adlx_int *")] int* errorIndex)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, IADLXManualTuningStateList*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), pGFXStates, errorIndex);
    }

    /// <include file='IADLXManualGraphicsTuning1.xml' path='doc/member[@name="IADLXManualGraphicsTuning1.SetGPUTuningStates"]/*' />
    public ADLX_RESULT SetGPUTuningStates([NativeTypeName("adlx::IADLXManualTuningStateList *")] IADLXManualTuningStateList* pGFXStates)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualGraphicsTuning1*, IADLXManualTuningStateList*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualGraphicsTuning1*)Unsafe.AsPointer(ref this), pGFXStates);
    }
}
