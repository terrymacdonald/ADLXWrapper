using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DAMDFluidMotionFrames1.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames1"]/*' />
[NativeTypeName("struct IADLX3DAMDFluidMotionFrames1 : adlx::IADLX3DAMDFluidMotionFrames")]
public unsafe partial struct IADLX3DAMDFluidMotionFrames1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, int>)(lpVtbl[0]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, int>)(lpVtbl[1]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLX3DAMDFluidMotionFrames.IsSupported" />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLX3DAMDFluidMotionFrames.IsEnabled" />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <inheritdoc cref="IADLX3DAMDFluidMotionFrames.SetEnabled" />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), enable);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames1.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames1.IsSupportedAlgorithm"]/*' />
    public ADLX_RESULT IsSupportedAlgorithm([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames1.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames1.GetAlgorithm"]/*' />
    public ADLX_RESULT GetAlgorithm([NativeTypeName("adlx::ADLX_AFMF_ALGORITHM *")] ADLX_AFMF_ALGORITHM* algorithm)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, ADLX_AFMF_ALGORITHM*, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), algorithm);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames1.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames1.SetAlgorithm"]/*' />
    public ADLX_RESULT SetAlgorithm([NativeTypeName("adlx::ADLX_AFMF_ALGORITHM")] ADLX_AFMF_ALGORITHM algorithm)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, ADLX_AFMF_ALGORITHM, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), algorithm);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames1.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames1.GetSearchMode"]/*' />
    public ADLX_RESULT GetSearchMode([NativeTypeName("adlx::ADLX_AFMF_SEARCH_MODE_TYPE *")] ADLX_AFMF_SEARCH_MODE_TYPE* mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, ADLX_AFMF_SEARCH_MODE_TYPE*, ADLX_RESULT>)(lpVtbl[9]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), mode);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames1.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames1.SetSearchMode"]/*' />
    public ADLX_RESULT SetSearchMode([NativeTypeName("adlx::ADLX_AFMF_SEARCH_MODE_TYPE")] ADLX_AFMF_SEARCH_MODE_TYPE mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, ADLX_AFMF_SEARCH_MODE_TYPE, ADLX_RESULT>)(lpVtbl[10]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), mode);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames1.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames1.GetPerformanceMode"]/*' />
    public ADLX_RESULT GetPerformanceMode([NativeTypeName("adlx::ADLX_AFMF_PERFORMANCE_MODE_TYPE *")] ADLX_AFMF_PERFORMANCE_MODE_TYPE* mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, ADLX_AFMF_PERFORMANCE_MODE_TYPE*, ADLX_RESULT>)(lpVtbl[11]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), mode);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames1.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames1.SetPerformanceMode"]/*' />
    public ADLX_RESULT SetPerformanceMode([NativeTypeName("adlx::ADLX_AFMF_PERFORMANCE_MODE_TYPE")] ADLX_AFMF_PERFORMANCE_MODE_TYPE mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, ADLX_AFMF_PERFORMANCE_MODE_TYPE, ADLX_RESULT>)(lpVtbl[12]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), mode);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames1.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames1.GetFastMotionResponse"]/*' />
    public ADLX_RESULT GetFastMotionResponse([NativeTypeName("adlx::ADLX_AFMF_FAST_MOTION_RESP *")] ADLX_AFMF_FAST_MOTION_RESP* response)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, ADLX_AFMF_FAST_MOTION_RESP*, ADLX_RESULT>)(lpVtbl[13]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), response);
    }

    /// <include file='IADLX3DAMDFluidMotionFrames1.xml' path='doc/member[@name="IADLX3DAMDFluidMotionFrames1.SetFastMotionResponse"]/*' />
    public ADLX_RESULT SetFastMotionResponse([NativeTypeName("adlx::ADLX_AFMF_FAST_MOTION_RESP")] ADLX_AFMF_FAST_MOTION_RESP response)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DAMDFluidMotionFrames1*, ADLX_AFMF_FAST_MOTION_RESP, ADLX_RESULT>)(lpVtbl[14]))((IADLX3DAMDFluidMotionFrames1*)Unsafe.AsPointer(ref this), response);
    }
}
