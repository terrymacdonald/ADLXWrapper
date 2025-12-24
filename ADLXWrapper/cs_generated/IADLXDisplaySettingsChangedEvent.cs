using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent"]/*' />
[NativeTypeName("struct IADLXDisplaySettingsChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXDisplaySettingsChangedEvent
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, int>)(lpVtbl[0]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, int>)(lpVtbl[1]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXChangedEvent.GetOrigin" />
    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.GetDisplay"]/*' />
    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsFreeSyncChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsFreeSyncChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[5]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsVSRChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsVSRChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[6]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsGPUScalingChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUScalingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[7]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsScalingModeChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsScalingModeChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[8]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsIntegerScalingChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsIntegerScalingChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[9]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsColorDepthChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsColorDepthChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[10]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsPixelFormatChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsPixelFormatChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[11]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsHDCPChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsHDCPChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[12]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsCustomColorHueChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorHueChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[13]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsCustomColorSaturationChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorSaturationChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[14]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsCustomColorBrightnessChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorBrightnessChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[15]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsCustomColorTemperatureChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorTemperatureChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[16]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsCustomColorContrastChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorContrastChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[17]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsCustomResolutionChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomResolutionChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[18]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplaySettingsChangedEvent.xml' path='doc/member[@name="IADLXDisplaySettingsChangedEvent.IsVariBrightChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsVariBrightChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[19]))((IADLXDisplaySettingsChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
