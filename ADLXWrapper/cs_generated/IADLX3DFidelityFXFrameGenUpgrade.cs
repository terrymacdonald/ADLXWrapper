using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DFidelityFXFrameGenUpgrade.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgrade"]/*' />
[NativeTypeName("struct IADLX3DFidelityFXFrameGenUpgrade : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DFidelityFXFrameGenUpgrade
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgrade*, int>)(lpVtbl[0]))((IADLX3DFidelityFXFrameGenUpgrade*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgrade*, int>)(lpVtbl[1]))((IADLX3DFidelityFXFrameGenUpgrade*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgrade*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DFidelityFXFrameGenUpgrade*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DFidelityFXFrameGenUpgrade.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgrade.GetAvailableRatios"]/*' />
    public ADLX_RESULT GetAvailableRatios(IADLX3DFidelityFXFrameGenUpgradeRatioOptionList** ratios)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgrade*, IADLX3DFidelityFXFrameGenUpgradeRatioOptionList**, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DFidelityFXFrameGenUpgrade*)Unsafe.AsPointer(ref this), ratios);
    }

    /// <include file='IADLX3DFidelityFXFrameGenUpgrade.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgrade.GetRatio"]/*' />
    public ADLX_RESULT GetRatio([NativeTypeName("adlx::ADLX_FFX_FRAME_GEN_RATIO *")] ADLX_FFX_FRAME_GEN_RATIO* ratio)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgrade*, ADLX_FFX_FRAME_GEN_RATIO*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DFidelityFXFrameGenUpgrade*)Unsafe.AsPointer(ref this), ratio);
    }

    /// <include file='IADLX3DFidelityFXFrameGenUpgrade.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgrade.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgrade*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DFidelityFXFrameGenUpgrade*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DFidelityFXFrameGenUpgrade.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgrade.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgrade*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DFidelityFXFrameGenUpgrade*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLX3DFidelityFXFrameGenUpgrade.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgrade.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgrade*, byte, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DFidelityFXFrameGenUpgrade*)Unsafe.AsPointer(ref this), enable);
    }

    /// <include file='IADLX3DFidelityFXFrameGenUpgrade.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgrade.SetRatio"]/*' />
    public ADLX_RESULT SetRatio([NativeTypeName("adlx::ADLX_FFX_FRAME_GEN_RATIO")] ADLX_FFX_FRAME_GEN_RATIO ratio)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgrade*, ADLX_FFX_FRAME_GEN_RATIO, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DFidelityFXFrameGenUpgrade*)Unsafe.AsPointer(ref this), ratio);
    }
}
