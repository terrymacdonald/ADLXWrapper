using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DFidelityFXSuperResolution.xml' path='doc/member[@name="IADLX3DFidelityFXSuperResolution"]/*' />
[NativeTypeName("struct IADLX3DFidelityFXSuperResolution : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DFidelityFXSuperResolution
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXSuperResolution*, int>)(lpVtbl[0]))((IADLX3DFidelityFXSuperResolution*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXSuperResolution*, int>)(lpVtbl[1]))((IADLX3DFidelityFXSuperResolution*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXSuperResolution*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DFidelityFXSuperResolution*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DFidelityFXSuperResolution.xml' path='doc/member[@name="IADLX3DFidelityFXSuperResolution.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXSuperResolution*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DFidelityFXSuperResolution*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DFidelityFXSuperResolution.xml' path='doc/member[@name="IADLX3DFidelityFXSuperResolution.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXSuperResolution*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DFidelityFXSuperResolution*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLX3DFidelityFXSuperResolution.xml' path='doc/member[@name="IADLX3DFidelityFXSuperResolution.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXSuperResolution*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DFidelityFXSuperResolution*)Unsafe.AsPointer(ref this), enable);
    }
}
