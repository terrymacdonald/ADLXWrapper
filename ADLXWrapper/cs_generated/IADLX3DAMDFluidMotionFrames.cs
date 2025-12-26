using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DAMDFluidMotionFrames.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames"]/*' />
[NativeTypeName("struct IADLX3DAMDFluidMotionFrames : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DAMDFluidMotionFrames
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, int>)(lpVtbl[0]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, int>)(lpVtbl[1]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DAMDFluidMotionFrames*)Unsafe.AsPointer(ref this), enable);
    }
}
