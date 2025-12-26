using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSystem2.xml' path='doc/member[@name="IADLXSystem2"]/*' />
[NativeTypeName("struct IADLXSystem2 : adlx::IADLXSystem1")]
public unsafe partial struct IADLXSystem2
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, int>)(lpVtbl[0]))((IADLXSystem2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, int>)(lpVtbl[1]))((IADLXSystem2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystem2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXSystem1.GetPowerTuningServices" />
    public ADLX_RESULT GetPowerTuningServices(IADLXPowerTuningServices** ppPowerTuningServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, IADLXPowerTuningServices**, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystem2*)Unsafe.AsPointer(ref this), ppPowerTuningServices);
    }

    /// <include file='IADLXSystem2.xml' path='doc/member[@name="IADLXSystem2.GetMultimediaServices"]/*' />
    public ADLX_RESULT GetMultimediaServices(IADLXMultimediaServices** ppMultiMediaServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, IADLXMultimediaServices**, ADLX_RESULT>)(lpVtbl[4]))((IADLXSystem2*)Unsafe.AsPointer(ref this), ppMultiMediaServices);
    }

    /// <include file='IADLXSystem2.xml' path='doc/member[@name="IADLXSystem2.GetGPUAppsListChangedHandling"]/*' />
    public ADLX_RESULT GetGPUAppsListChangedHandling(IADLXGPUAppsListChangedHandling** ppGPUAppsListChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem2*, IADLXGPUAppsListChangedHandling**, ADLX_RESULT>)(lpVtbl[5]))((IADLXSystem2*)Unsafe.AsPointer(ref this), ppGPUAppsListChangedHandling);
    }
}
