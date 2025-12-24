using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DTessellation.xml' path='doc/member[@name="IADLX3DTessellation"]/*' />
[NativeTypeName("struct IADLX3DTessellation : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DTessellation
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, int>)(lpVtbl[0]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, int>)(lpVtbl[1]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DTessellation.xml' path='doc/member[@name="IADLX3DTessellation.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DTessellation.xml' path='doc/member[@name="IADLX3DTessellation.GetMode"]/*' />
    public ADLX_RESULT GetMode(ADLX_TESSELLATION_MODE* currentMode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, ADLX_TESSELLATION_MODE*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), currentMode);
    }

    /// <include file='IADLX3DTessellation.xml' path='doc/member[@name="IADLX3DTessellation.GetLevel"]/*' />
    public ADLX_RESULT GetLevel(ADLX_TESSELLATION_LEVEL* currentLevel)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, ADLX_TESSELLATION_LEVEL*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), currentLevel);
    }

    /// <include file='IADLX3DTessellation.xml' path='doc/member[@name="IADLX3DTessellation.SetMode"]/*' />
    public ADLX_RESULT SetMode(ADLX_TESSELLATION_MODE mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, ADLX_TESSELLATION_MODE, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), mode);
    }

    /// <include file='IADLX3DTessellation.xml' path='doc/member[@name="IADLX3DTessellation.SetLevel"]/*' />
    public ADLX_RESULT SetLevel(ADLX_TESSELLATION_LEVEL level)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DTessellation*, ADLX_TESSELLATION_LEVEL, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DTessellation*)Unsafe.AsPointer(ref this), level);
    }
}
