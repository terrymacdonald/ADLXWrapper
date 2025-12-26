using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DChill.xml' path='doc/member[@name="IADLX3DChill"]/*' />
[NativeTypeName("struct IADLX3DChill : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DChill
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int>)(lpVtbl[0]))((IADLX3DChill*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int>)(lpVtbl[1]))((IADLX3DChill*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DChill*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DChill.xml' path='doc/member[@name="IADLX3DChill.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DChill*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DChill.xml' path='doc/member[@name="IADLX3DChill.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DChill*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLX3DChill.xml' path='doc/member[@name="IADLX3DChill.GetFPSRange"]/*' />
    public ADLX_RESULT GetFPSRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DChill*)Unsafe.AsPointer(ref this), range);
    }

    /// <include file='IADLX3DChill.xml' path='doc/member[@name="IADLX3DChill.GetMinFPS"]/*' />
    public ADLX_RESULT GetMinFPS([NativeTypeName("adlx_int *")] int* currentMinFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DChill*)Unsafe.AsPointer(ref this), currentMinFPS);
    }

    /// <include file='IADLX3DChill.xml' path='doc/member[@name="IADLX3DChill.GetMaxFPS"]/*' />
    public ADLX_RESULT GetMaxFPS([NativeTypeName("adlx_int *")] int* currentMaxFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DChill*)Unsafe.AsPointer(ref this), currentMaxFPS);
    }

    /// <include file='IADLX3DChill.xml' path='doc/member[@name="IADLX3DChill.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, byte, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DChill*)Unsafe.AsPointer(ref this), enable);
    }

    /// <include file='IADLX3DChill.xml' path='doc/member[@name="IADLX3DChill.SetMinFPS"]/*' />
    public ADLX_RESULT SetMinFPS([NativeTypeName("adlx_int")] int minFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int, ADLX_RESULT>)(lpVtbl[9]))((IADLX3DChill*)Unsafe.AsPointer(ref this), minFPS);
    }

    /// <include file='IADLX3DChill.xml' path='doc/member[@name="IADLX3DChill.SetMaxFPS"]/*' />
    public ADLX_RESULT SetMaxFPS([NativeTypeName("adlx_int")] int maxFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DChill*, int, ADLX_RESULT>)(lpVtbl[10]))((IADLX3DChill*)Unsafe.AsPointer(ref this), maxFPS);
    }
}
