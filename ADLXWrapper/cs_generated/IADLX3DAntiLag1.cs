using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DAntiLag1.xml' path='doc/member[@name="IADLX3DAntiLag1"]/*' />
[NativeTypeName("struct IADLX3DAntiLag1 : adlx::IADLX3DAntiLag")]
public unsafe partial struct IADLX3DAntiLag1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag1*, int>)(lpVtbl[0]))((IADLX3DAntiLag1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag1*, int>)(lpVtbl[1]))((IADLX3DAntiLag1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DAntiLag1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLX3DAntiLag.IsSupported" />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag1*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DAntiLag1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLX3DAntiLag.IsEnabled" />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag1*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DAntiLag1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <inheritdoc cref="IADLX3DAntiLag.SetEnabled" />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag1*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DAntiLag1*)Unsafe.AsPointer(ref this), enable);
    }

    /// <include file='IADLX3DAntiLag1.xml' path='doc/member[@name="IADLX3DAntiLag1.GetLevel"]/*' />
    public ADLX_RESULT GetLevel(ADLX_ANTILAG_STATE* level)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag1*, ADLX_ANTILAG_STATE*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DAntiLag1*)Unsafe.AsPointer(ref this), level);
    }

    /// <include file='IADLX3DAntiLag1.xml' path='doc/member[@name="IADLX3DAntiLag1.SetLevel"]/*' />
    public ADLX_RESULT SetLevel(ADLX_ANTILAG_STATE level)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiLag1*, ADLX_ANTILAG_STATE, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DAntiLag1*)Unsafe.AsPointer(ref this), level);
    }
}
