using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DAnisotropicFiltering.xml' path='doc/member[@name="IADLX3DAnisotropicFiltering"]/*' />
[NativeTypeName("struct IADLX3DAnisotropicFiltering : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DAnisotropicFiltering
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAnisotropicFiltering*, int>)(lpVtbl[0]))((IADLX3DAnisotropicFiltering*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAnisotropicFiltering*, int>)(lpVtbl[1]))((IADLX3DAnisotropicFiltering*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAnisotropicFiltering*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DAnisotropicFiltering*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DAnisotropicFiltering.xml' path='doc/member[@name="IADLX3DAnisotropicFiltering.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAnisotropicFiltering*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DAnisotropicFiltering*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DAnisotropicFiltering.xml' path='doc/member[@name="IADLX3DAnisotropicFiltering.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAnisotropicFiltering*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DAnisotropicFiltering*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLX3DAnisotropicFiltering.xml' path='doc/member[@name="IADLX3DAnisotropicFiltering.GetLevel"]/*' />
    public ADLX_RESULT GetLevel(ADLX_ANISOTROPIC_FILTERING_LEVEL* currentLevel)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAnisotropicFiltering*, ADLX_ANISOTROPIC_FILTERING_LEVEL*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DAnisotropicFiltering*)Unsafe.AsPointer(ref this), currentLevel);
    }

    /// <include file='IADLX3DAnisotropicFiltering.xml' path='doc/member[@name="IADLX3DAnisotropicFiltering.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAnisotropicFiltering*, byte, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DAnisotropicFiltering*)Unsafe.AsPointer(ref this), enable);
    }

    /// <include file='IADLX3DAnisotropicFiltering.xml' path='doc/member[@name="IADLX3DAnisotropicFiltering.SetLevel"]/*' />
    public ADLX_RESULT SetLevel(ADLX_ANISOTROPIC_FILTERING_LEVEL level)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAnisotropicFiltering*, ADLX_ANISOTROPIC_FILTERING_LEVEL, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DAnisotropicFiltering*)Unsafe.AsPointer(ref this), level);
    }
}
