using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DAntiLag.xml' path='doc/member[@name="IADLX3DAntiLag"]/*' />
[NativeTypeName("struct IADLX3DAntiLag : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DAntiLag
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag*, int>)(lpVtbl[0]))((IADLX3DAntiLag*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag*, int>)(lpVtbl[1]))((IADLX3DAntiLag*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DAntiLag*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DAntiLag.xml' path='doc/member[@name="IADLX3DAntiLag.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DAntiLag*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DAntiLag.xml' path='doc/member[@name="IADLX3DAntiLag.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DAntiLag*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLX3DAntiLag.xml' path='doc/member[@name="IADLX3DAntiLag.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DAntiLag*)Unsafe.AsPointer(ref this), enable);
    }
}
