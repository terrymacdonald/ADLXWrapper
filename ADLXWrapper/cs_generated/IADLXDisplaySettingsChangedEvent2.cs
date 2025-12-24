using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplaySettingsChangedEvent2.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent2"]/*' />
[NativeTypeName("struct IADLXDisplaySettingsChangedEvent2 : adlx::IADLXDisplaySettingsChangedEvent1")]
public unsafe partial struct IADLXDisplaySettingsChangedEvent2
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, int>)(lpVtbl[0]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, int>)(lpVtbl[1]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXChangedEvent.GetOrigin" />
    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.GetDisplay" />
    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsFreeSyncChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsFreeSyncChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[5]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsVSRChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsVSRChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[6]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsGPUScalingChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUScalingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[7]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsScalingModeChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsScalingModeChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[8]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsIntegerScalingChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsIntegerScalingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[9]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsColorDepthChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsColorDepthChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[10]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsPixelFormatChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsPixelFormatChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[11]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsHDCPChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsHDCPChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[12]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsCustomColorHueChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorHueChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[13]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsCustomColorSaturationChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorSaturationChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[14]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsCustomColorBrightnessChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorBrightnessChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[15]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsCustomColorTemperatureChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorTemperatureChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[16]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsCustomColorContrastChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorContrastChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[17]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsCustomResolutionChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomResolutionChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[18]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent.IsVariBrightChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsVariBrightChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[19]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <inheritdoc cref="IADLXDisplaySettingsChangedEvent1.IsDisplayBlankingChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayBlankingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[20]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent2.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent2.IsDisplayConnectivityExperienceChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayConnectivityExperienceChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent2*, byte>)(lpVtbl[21]))((IADLXDisplaySettingsChangedEvent2*)Unsafe.AsPointer(ref this)) != 0;
    }
}
