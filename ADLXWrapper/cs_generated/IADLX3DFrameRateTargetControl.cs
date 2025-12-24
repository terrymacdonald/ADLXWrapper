using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DFrameRateTargetControl.xml' path='doc/member[@name="IADLX3DFrameRateTargetControl"]/*' />
[NativeTypeName("struct IADLX3DFrameRateTargetControl : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DFrameRateTargetControl
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, int>)(lpVtbl[0]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, int>)(lpVtbl[1]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DFrameRateTargetControl.xml' path='doc/member[@name="IADLX3DFrameRateTargetControl.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DFrameRateTargetControl.xml' path='doc/member[@name="IADLX3DFrameRateTargetControl.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLX3DFrameRateTargetControl.xml' path='doc/member[@name="IADLX3DFrameRateTargetControl.GetFPSRange"]/*' />
    public ADLX_RESULT GetFPSRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), range);
    }

    /// <include file='IADLX3DFrameRateTargetControl.xml' path='doc/member[@name="IADLX3DFrameRateTargetControl.GetFPS"]/*' />
    public ADLX_RESULT GetFPS([NativeTypeName("adlx_int *")] int* currentFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), currentFPS);
    }

    /// <include file='IADLX3DFrameRateTargetControl.xml' path='doc/member[@name="IADLX3DFrameRateTargetControl.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, byte, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), enable);
    }

    /// <include file='IADLX3DFrameRateTargetControl.xml' path='doc/member[@name="IADLX3DFrameRateTargetControl.SetFPS"]/*' />
    public ADLX_RESULT SetFPS([NativeTypeName("adlx_int")] int maxFPS)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DFrameRateTargetControl*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DFrameRateTargetControl*)Unsafe.AsPointer(ref this), maxFPS);
    }
}
