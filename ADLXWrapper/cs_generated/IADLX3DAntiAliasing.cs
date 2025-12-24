using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DAntiAliasing.xml' path='doc/member[@name="IADLX3DAntiAliasing"]/*' />
[NativeTypeName("struct IADLX3DAntiAliasing : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DAntiAliasing
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiAliasing*, int>)(lpVtbl[0]))((IADLX3DAntiAliasing*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiAliasing*, int>)(lpVtbl[1]))((IADLX3DAntiAliasing*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiAliasing*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DAntiAliasing*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DAntiAliasing.xml' path='doc/member[@name="IADLX3DAntiAliasing.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiAliasing*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DAntiAliasing*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DAntiAliasing.xml' path='doc/member[@name="IADLX3DAntiAliasing.GetMode"]/*' />
    public ADLX_RESULT GetMode(ADLX_ANTI_ALIASING_MODE* currentMode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiAliasing*, ADLX_ANTI_ALIASING_MODE*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DAntiAliasing*)Unsafe.AsPointer(ref this), currentMode);
    }

    /// <include file='IADLX3DAntiAliasing.xml' path='doc/member[@name="IADLX3DAntiAliasing.GetLevel"]/*' />
    public ADLX_RESULT GetLevel(ADLX_ANTI_ALIASING_LEVEL* currentLevel)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiAliasing*, ADLX_ANTI_ALIASING_LEVEL*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DAntiAliasing*)Unsafe.AsPointer(ref this), currentLevel);
    }

    /// <include file='IADLX3DAntiAliasing.xml' path='doc/member[@name="IADLX3DAntiAliasing.GetMethod"]/*' />
    public ADLX_RESULT GetMethod(ADLX_ANTI_ALIASING_METHOD* currentMethod)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiAliasing*, ADLX_ANTI_ALIASING_METHOD*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DAntiAliasing*)Unsafe.AsPointer(ref this), currentMethod);
    }

    /// <include file='IADLX3DAntiAliasing.xml' path='doc/member[@name="IADLX3DAntiAliasing.SetMode"]/*' />
    public ADLX_RESULT SetMode(ADLX_ANTI_ALIASING_MODE mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiAliasing*, ADLX_ANTI_ALIASING_MODE, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DAntiAliasing*)Unsafe.AsPointer(ref this), mode);
    }

    /// <include file='IADLX3DAntiAliasing.xml' path='doc/member[@name="IADLX3DAntiAliasing.SetLevel"]/*' />
    public ADLX_RESULT SetLevel(ADLX_ANTI_ALIASING_LEVEL level)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiAliasing*, ADLX_ANTI_ALIASING_LEVEL, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DAntiAliasing*)Unsafe.AsPointer(ref this), level);
    }

    /// <include file='IADLX3DAntiAliasing.xml' path='doc/member[@name="IADLX3DAntiAliasing.SetMethod"]/*' />
    public ADLX_RESULT SetMethod(ADLX_ANTI_ALIASING_METHOD method)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAntiAliasing*, ADLX_ANTI_ALIASING_METHOD, ADLX_RESULT>)(lpVtbl[9]))((IADLX3DAntiAliasing*)Unsafe.AsPointer(ref this), method);
    }
}
