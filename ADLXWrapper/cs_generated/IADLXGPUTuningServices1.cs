using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUTuningServices1.xml' path='doc/member[@name="IADLXGPUTuningServices1"]/*' />
[NativeTypeName("struct IADLXGPUTuningServices1 : adlx::IADLXGPUTuningServices")]
public unsafe partial struct IADLXGPUTuningServices1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, int>)(lpVtbl[0]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, int>)(lpVtbl[1]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.GetGPUTuningChangedHandling" />
    public ADLX_RESULT GetGPUTuningChangedHandling(IADLXGPUTuningChangedHandling** ppGPUTuningChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPUTuningChangedHandling**, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), ppGPUTuningChangedHandling);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.IsAtFactory" />
    public ADLX_RESULT IsAtFactory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* isFactory)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, isFactory);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.ResetToFactory" />
    public ADLX_RESULT ResetToFactory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.IsSupportedAutoTuning" />
    public ADLX_RESULT IsSupportedAutoTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.IsSupportedPresetTuning" />
    public ADLX_RESULT IsSupportedPresetTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.IsSupportedManualGFXTuning" />
    public ADLX_RESULT IsSupportedManualGFXTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.IsSupportedManualVRAMTuning" />
    public ADLX_RESULT IsSupportedManualVRAMTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.IsSupportedManualFanTuning" />
    public ADLX_RESULT IsSupportedManualFanTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.IsSupportedManualPowerTuning" />
    public ADLX_RESULT IsSupportedManualPowerTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, supported);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.GetAutoTuning" />
    public ADLX_RESULT GetAutoTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppAutoTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppAutoTuning);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.GetPresetTuning" />
    public ADLX_RESULT GetPresetTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppPresetTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppPresetTuning);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.GetManualGFXTuning" />
    public ADLX_RESULT GetManualGFXTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualGFXTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppManualGFXTuning);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.GetManualVRAMTuning" />
    public ADLX_RESULT GetManualVRAMTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualVRAMTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppManualVRAMTuning);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.GetManualFanTuning" />
    public ADLX_RESULT GetManualFanTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualFanTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppManualFanTuning);
    }

    /// <inheritdoc cref="IADLXGPUTuningServices.GetManualPowerTuning" />
    public ADLX_RESULT GetManualPowerTuning([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXInterface** ppManualPowerTuning)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXInterface**, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppManualPowerTuning);
    }

    /// <include file='IADLXGPUTuningServices1.xml' path='doc/member[@name="IADLXGPUTuningServices1.GetSmartAccessMemory"]/*' />
    public ADLX_RESULT GetSmartAccessMemory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXSmartAccessMemory** ppSmartAccessMemory)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningServices1*, IADLXGPU*, IADLXSmartAccessMemory**, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPUTuningServices1*)Unsafe.AsPointer(ref this), pGPU, ppSmartAccessMemory);
    }
}
