using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DEnhancedSync.xml' path='doc/member[@name="IADLX3DEnhancedSync"]/*' />
[NativeTypeName("struct IADLX3DEnhancedSync : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DEnhancedSync
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DEnhancedSync*, int>)(lpVtbl[0]))((IADLX3DEnhancedSync*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DEnhancedSync*, int>)(lpVtbl[1]))((IADLX3DEnhancedSync*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DEnhancedSync*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DEnhancedSync*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DEnhancedSync.xml' path='doc/member[@name="IADLX3DEnhancedSync.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DEnhancedSync*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DEnhancedSync*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DEnhancedSync.xml' path='doc/member[@name="IADLX3DEnhancedSync.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DEnhancedSync*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DEnhancedSync*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLX3DEnhancedSync.xml' path='doc/member[@name="IADLX3DEnhancedSync.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DEnhancedSync*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DEnhancedSync*)Unsafe.AsPointer(ref this), enable);
    }
}
