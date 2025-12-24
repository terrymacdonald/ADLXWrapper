using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1"]/*' />
[NativeTypeName("struct IADLXDisplayVariBright1 : adlx::IADLXDisplayVariBright")]
public unsafe partial struct IADLXDisplayVariBright1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, int>)(lpVtbl[0]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, int>)(lpVtbl[1]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.IsSupported" />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.IsEnabled" />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.SetEnabled" />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.IsCurrentMaximizeBrightness" />
    public ADLX_RESULT IsCurrentMaximizeBrightness([NativeTypeName("adlx_bool *")] bool* maximizeBrightness)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), maximizeBrightness);
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.IsCurrentOptimizeBrightness" />
    public ADLX_RESULT IsCurrentOptimizeBrightness([NativeTypeName("adlx_bool *")] bool* optimizeBrightness)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), optimizeBrightness);
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.IsCurrentBalanced" />
    public ADLX_RESULT IsCurrentBalanced([NativeTypeName("adlx_bool *")] bool* balanced)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), balanced);
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.IsCurrentOptimizeBattery" />
    public ADLX_RESULT IsCurrentOptimizeBattery([NativeTypeName("adlx_bool *")] bool* optimizeBattery)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), optimizeBattery);
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.IsCurrentMaximizeBattery" />
    public ADLX_RESULT IsCurrentMaximizeBattery([NativeTypeName("adlx_bool *")] bool* maximizeBattery)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), maximizeBattery);
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.SetMaximizeBrightness" />
    public ADLX_RESULT SetMaximizeBrightness()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.SetOptimizeBrightness" />
    public ADLX_RESULT SetOptimizeBrightness()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.SetBalanced" />
    public ADLX_RESULT SetBalanced()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.SetOptimizeBattery" />
    public ADLX_RESULT SetOptimizeBattery()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, ADLX_RESULT>)(lpVtbl[14]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXDisplayVariBright.SetMaximizeBattery" />
    public ADLX_RESULT SetMaximizeBattery()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, ADLX_RESULT>)(lpVtbl[15]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.IsBacklightAdaptiveSupported"]/*' />
    public ADLX_RESULT IsBacklightAdaptiveSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[16]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.IsBacklightAdaptiveEnabled"]/*' />
    public ADLX_RESULT IsBacklightAdaptiveEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[17]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.SetBacklightAdaptiveEnabled"]/*' />
    public ADLX_RESULT SetBacklightAdaptiveEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, byte, ADLX_RESULT>)(lpVtbl[18]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.IsBatteryLifeSupported"]/*' />
    public ADLX_RESULT IsBatteryLifeSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[19]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.IsBatteryLifeEnabled"]/*' />
    public ADLX_RESULT IsBatteryLifeEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[20]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.SetBatteryLifeEnabled"]/*' />
    public ADLX_RESULT SetBatteryLifeEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, byte, ADLX_RESULT>)(lpVtbl[21]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.IsWindowsPowerModeSupported"]/*' />
    public ADLX_RESULT IsWindowsPowerModeSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[22]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.IsWindowsPowerModeEnabled"]/*' />
    public ADLX_RESULT IsWindowsPowerModeEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[23]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.SetWindowsPowerModeEnabled"]/*' />
    public ADLX_RESULT SetWindowsPowerModeEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, byte, ADLX_RESULT>)(lpVtbl[24]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.IsFullScreenVideoSupported"]/*' />
    public ADLX_RESULT IsFullScreenVideoSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[25]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.IsFullScreenVideoEnabled"]/*' />
    public ADLX_RESULT IsFullScreenVideoEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, bool*, ADLX_RESULT>)(lpVtbl[26]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayVariBright1.xml' path='doc/member[@name="IADLXDisplayVariBright1.SetFullScreenVideoEnabled"]/*' />
    public ADLX_RESULT SetFullScreenVideoEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVariBright1*, byte, ADLX_RESULT>)(lpVtbl[27]))((IADLXDisplayVariBright1*)Unsafe.AsPointer(ref this), enabled);
    }
}
