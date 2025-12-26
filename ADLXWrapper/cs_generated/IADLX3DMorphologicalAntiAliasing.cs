using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DMorphologicalAntiAliasing.xml' path='doc/member[@name="IADLX3DMorphologicalAntiAliasing"]/*' />
[NativeTypeName("struct IADLX3DMorphologicalAntiAliasing : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DMorphologicalAntiAliasing
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, int>)(lpVtbl[0]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, int>)(lpVtbl[1]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DMorphologicalAntiAliasing.xml' path='doc/member[@name="IADLX3DMorphologicalAntiAliasing.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DMorphologicalAntiAliasing.xml' path='doc/member[@name="IADLX3DMorphologicalAntiAliasing.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLX3DMorphologicalAntiAliasing.xml' path='doc/member[@name="IADLX3DMorphologicalAntiAliasing.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DMorphologicalAntiAliasing*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DMorphologicalAntiAliasing*)Unsafe.AsPointer(ref this), enable);
    }
}
