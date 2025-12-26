using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DResetShaderCache.xml' path='doc/member[@name="IADLX3DResetShaderCache"]/*' />
[NativeTypeName("struct IADLX3DResetShaderCache : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DResetShaderCache
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DResetShaderCache*, int>)(lpVtbl[0]))((IADLX3DResetShaderCache*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DResetShaderCache*, int>)(lpVtbl[1]))((IADLX3DResetShaderCache*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DResetShaderCache*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DResetShaderCache*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DResetShaderCache.xml' path='doc/member[@name="IADLX3DResetShaderCache.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DResetShaderCache*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DResetShaderCache*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DResetShaderCache.xml' path='doc/member[@name="IADLX3DResetShaderCache.ResetShaderCache"]/*' />
    public ADLX_RESULT ResetShaderCache()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DResetShaderCache*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DResetShaderCache*)Unsafe.AsPointer(ref this));
    }
}
