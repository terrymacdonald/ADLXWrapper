using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning"]/*' />
[NativeTypeName("struct IADLXGPUPresetTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUPresetTuning
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, int>)(lpVtbl[0]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, int>)(lpVtbl[1]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.IsSupportedPowerSaver"]/*' />
    public ADLX_RESULT IsSupportedPowerSaver([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.IsSupportedQuiet"]/*' />
    public ADLX_RESULT IsSupportedQuiet([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.IsSupportedBalanced"]/*' />
    public ADLX_RESULT IsSupportedBalanced([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.IsSupportedTurbo"]/*' />
    public ADLX_RESULT IsSupportedTurbo([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.IsSupportedRage"]/*' />
    public ADLX_RESULT IsSupportedRage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.IsCurrentPowerSaver"]/*' />
    public ADLX_RESULT IsCurrentPowerSaver([NativeTypeName("adlx_bool *")] bool* isPowerSaver)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), isPowerSaver);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.IsCurrentQuiet"]/*' />
    public ADLX_RESULT IsCurrentQuiet([NativeTypeName("adlx_bool *")] bool* isQuiet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), isQuiet);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.IsCurrentBalanced"]/*' />
    public ADLX_RESULT IsCurrentBalanced([NativeTypeName("adlx_bool *")] bool* isBalance)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), isBalance);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.IsCurrentTurbo"]/*' />
    public ADLX_RESULT IsCurrentTurbo([NativeTypeName("adlx_bool *")] bool* isTurbo)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), isTurbo);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.IsCurrentRage"]/*' />
    public ADLX_RESULT IsCurrentRage([NativeTypeName("adlx_bool *")] bool* isRage)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, bool*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this), isRage);
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.SetPowerSaver"]/*' />
    public ADLX_RESULT SetPowerSaver()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.SetQuiet"]/*' />
    public ADLX_RESULT SetQuiet()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.SetBalanced"]/*' />
    public ADLX_RESULT SetBalanced()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.SetTurbo"]/*' />
    public ADLX_RESULT SetTurbo()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXGPUPresetTuning.xml' path='doc/member[@name="IADLXGPUPresetTuning.SetRage"]/*' />
    public ADLX_RESULT SetRage()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUPresetTuning*, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUPresetTuning*)Unsafe.AsPointer(ref this));
    }
}
