using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DFidelityFXFrameGenUpgradeRatioOption.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgradeRatioOption"]/*' />
[NativeTypeName("struct IADLX3DFidelityFXFrameGenUpgradeRatioOption : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DFidelityFXFrameGenUpgradeRatioOption
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOption*, int>)(lpVtbl[0]))((IADLX3DFidelityFXFrameGenUpgradeRatioOption*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOption*, int>)(lpVtbl[1]))((IADLX3DFidelityFXFrameGenUpgradeRatioOption*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOption*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DFidelityFXFrameGenUpgradeRatioOption*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DFidelityFXFrameGenUpgradeRatioOption.xml' path='doc/member[@name="IADLX3DFidelityFXFrameGenUpgradeRatioOption.Ratio"]/*' />
    public ADLX_RESULT Ratio([NativeTypeName("adlx::ADLX_FFX_FRAME_GEN_RATIO *")] ADLX_FFX_FRAME_GEN_RATIO* ratio)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFidelityFXFrameGenUpgradeRatioOption*, ADLX_FFX_FRAME_GEN_RATIO*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DFidelityFXFrameGenUpgradeRatioOption*)Unsafe.AsPointer(ref this), ratio);
    }
}
