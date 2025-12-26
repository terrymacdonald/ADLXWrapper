using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DSettingsChangedEvent2.xml' path='doc/member[@name="IADLX3DSettingsChangedEvent2"]/*' />
[NativeTypeName("struct IADLX3DSettingsChangedEvent2 : adlx::IADLX3DSettingsChangedEvent1")]
public unsafe partial struct IADLX3DSettingsChangedEvent2
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, int>)(lpVtbl[0]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, int>)(lpVtbl[1]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXChangedEvent.GetOrigin" />
    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.GetGPU" />
    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, IADLXGPU**, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this), ppGPU);
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsAntiLagChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsAntiLagChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[5]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsChillChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsChillChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[6]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsBoostChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsBoostChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[7]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsImageSharpeningChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsImageSharpeningChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[8]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsEnhancedSyncChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsEnhancedSyncChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[9]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsWaitForVerticalRefreshChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsWaitForVerticalRefreshChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[10]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsFrameRateTargetControlChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsFrameRateTargetControlChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[11]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsAntiAliasingChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsAntiAliasingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[12]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsMorphologicalAntiAliasingChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsMorphologicalAntiAliasingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[13]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsAnisotropicFilteringChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsAnisotropicFilteringChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[14]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsTessellationModeChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsTessellationModeChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[15]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsRadeonSuperResolutionChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsRadeonSuperResolutionChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[16]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent.IsResetShaderCache" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsResetShaderCache()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[17]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLX3DSettingsChangedEvent1.IsAMDFluidMotionFramesChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsAMDFluidMotionFramesChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[18]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLX3DSettingsChangedEvent2.xml' path='doc/member[@name="IADLX3DSettingsChangedEvent2.IsImageSharpenDesktopChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsImageSharpenDesktopChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedEvent2*, byte>)(lpVtbl[19]))((IADLX3DSettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }
}
