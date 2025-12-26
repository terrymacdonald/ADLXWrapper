using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUAutoTuning.xml' path='doc/member[@name="IADLXGPUAutoTuning"]/*' />
[NativeTypeName("struct IADLXGPUAutoTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUAutoTuning
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, int>)(lpVtbl[0]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, int>)(lpVtbl[1]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXGPUAutoTuning.xml' path='doc/member[@name="IADLXGPUAutoTuning.IsSupportedUndervoltGPU"]/*' />
    public ADLX_RESULT IsSupportedUndervoltGPU([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUAutoTuning.xml' path='doc/member[@name="IADLXGPUAutoTuning.IsSupportedOverclockGPU"]/*' />
    public ADLX_RESULT IsSupportedOverclockGPU([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUAutoTuning.xml' path='doc/member[@name="IADLXGPUAutoTuning.IsSupportedOverclockVRAM"]/*' />
    public ADLX_RESULT IsSupportedOverclockVRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUAutoTuning.xml' path='doc/member[@name="IADLXGPUAutoTuning.IsCurrentUndervoltGPU"]/*' />
    public ADLX_RESULT IsCurrentUndervoltGPU([NativeTypeName("adlx_bool *")] bool* isUndervoltGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), isUndervoltGPU);
    }

    /// <include file='IADLXGPUAutoTuning.xml' path='doc/member[@name="IADLXGPUAutoTuning.IsCurrentOverclockGPU"]/*' />
    public ADLX_RESULT IsCurrentOverclockGPU([NativeTypeName("adlx_bool *")] bool* isOverclockGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), isOverclockGPU);
    }

    /// <include file='IADLXGPUAutoTuning.xml' path='doc/member[@name="IADLXGPUAutoTuning.IsCurrentOverclockVRAM"]/*' />
    public ADLX_RESULT IsCurrentOverclockVRAM([NativeTypeName("adlx_bool *")] bool* isOverclockVRAM)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), isOverclockVRAM);
    }

    /// <include file='IADLXGPUAutoTuning.xml' path='doc/member[@name="IADLXGPUAutoTuning.StartUndervoltGPU"]/*' />
    public ADLX_RESULT StartUndervoltGPU([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, IADLXGPUAutoTuningCompleteListener*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), pCompleteListener);
    }

    /// <include file='IADLXGPUAutoTuning.xml' path='doc/member[@name="IADLXGPUAutoTuning.StartOverclockGPU"]/*' />
    public ADLX_RESULT StartOverclockGPU([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, IADLXGPUAutoTuningCompleteListener*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), pCompleteListener);
    }

    /// <include file='IADLXGPUAutoTuning.xml' path='doc/member[@name="IADLXGPUAutoTuning.StartOverclockVRAM"]/*' />
    public ADLX_RESULT StartOverclockVRAM([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteListener *")] IADLXGPUAutoTuningCompleteListener* pCompleteListener)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuning*, IADLXGPUAutoTuningCompleteListener*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUAutoTuning*)Unsafe.AsPointer(ref this), pCompleteListener);
    }
}
