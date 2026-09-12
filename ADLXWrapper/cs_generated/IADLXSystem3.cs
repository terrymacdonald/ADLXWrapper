using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSystem3.xml' path='doc/member[@name="IADLXSystem3"]/*' />
[NativeTypeName("struct IADLXSystem3 : adlx::IADLXSystem2")]
public unsafe partial struct IADLXSystem3
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem3*, int>)(lpVtbl[0]))((IADLXSystem3*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem3*, int>)(lpVtbl[1]))((IADLXSystem3*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem3*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystem3*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXSystem1.GetPowerTuningServices" />
    public ADLX_RESULT GetPowerTuningServices(IADLXPowerTuningServices** ppPowerTuningServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem3*, IADLXPowerTuningServices**, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystem3*)Unsafe.AsPointer(ref this), ppPowerTuningServices);
    }

    /// <inheritdoc cref="IADLXSystem2.GetMultimediaServices" />
    public ADLX_RESULT GetMultimediaServices(IADLXMultimediaServices** ppMultiMediaServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem3*, IADLXMultimediaServices**, ADLX_RESULT>)(lpVtbl[4]))((IADLXSystem3*)Unsafe.AsPointer(ref this), ppMultiMediaServices);
    }

    /// <inheritdoc cref="IADLXSystem2.GetGPUAppsListChangedHandling" />
    public ADLX_RESULT GetGPUAppsListChangedHandling(IADLXGPUAppsListChangedHandling** ppGPUAppsListChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem3*, IADLXGPUAppsListChangedHandling**, ADLX_RESULT>)(lpVtbl[5]))((IADLXSystem3*)Unsafe.AsPointer(ref this), ppGPUAppsListChangedHandling);
    }

    /// <include file='IADLXSystem3.xml' path='doc/member[@name="IADLXSystem3.GetVariableGraphicsMemory"]/*' />
    public ADLX_RESULT GetVariableGraphicsMemory(IADLXVariableGraphicsMemory** ppVariableGraphicsMemory)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem3*, IADLXVariableGraphicsMemory**, ADLX_RESULT>)(lpVtbl[6]))((IADLXSystem3*)Unsafe.AsPointer(ref this), ppVariableGraphicsMemory);
    }
}
