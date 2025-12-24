using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices"]/*' />
[NativeTypeName("struct IADLXDisplayServices : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayServices
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, int>)(lpVtbl[0]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, int>)(lpVtbl[1]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetNumberOfDisplays"]/*' />
    public ADLX_RESULT GetNumberOfDisplays([NativeTypeName("adlx_uint *")] uint* numDisplays)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, uint*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), numDisplays);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetDisplays"]/*' />
    public ADLX_RESULT GetDisplays(IADLXDisplayList** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplayList**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.Get3DLUT"]/*' />
    public ADLX_RESULT Get3DLUT([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplay3DLUT** ppDisp3DLUT)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplay3DLUT**, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppDisp3DLUT);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetGamut"]/*' />
    public ADLX_RESULT GetGamut([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGamut** ppDispGamut)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayGamut**, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppDispGamut);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetGamma"]/*' />
    public ADLX_RESULT GetGamma([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGamma** ppDispGamma)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayGamma**, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppDispGamma);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetDisplayChangedHandling"]/*' />
    public ADLX_RESULT GetDisplayChangedHandling(IADLXDisplayChangedHandling** ppDisplayChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplayChangedHandling**, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), ppDisplayChangedHandling);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetFreeSync"]/*' />
    public ADLX_RESULT GetFreeSync([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayFreeSync** ppFreeSync)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayFreeSync**, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppFreeSync);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetVirtualSuperResolution"]/*' />
    public ADLX_RESULT GetVirtualSuperResolution([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayVSR** ppVSR)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayVSR**, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppVSR);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetGPUScaling"]/*' />
    public ADLX_RESULT GetGPUScaling([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGPUScaling** ppGPUScaling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayGPUScaling**, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppGPUScaling);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetScalingMode"]/*' />
    public ADLX_RESULT GetScalingMode([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayScalingMode** ppScalingMode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayScalingMode**, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppScalingMode);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetIntegerScaling"]/*' />
    public ADLX_RESULT GetIntegerScaling([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayIntegerScaling** ppIntegerScaling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayIntegerScaling**, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppIntegerScaling);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetColorDepth"]/*' />
    public ADLX_RESULT GetColorDepth([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayColorDepth** ppColorDepth)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayColorDepth**, ADLX_RESULT>)(lpVtbl[14]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppColorDepth);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetPixelFormat"]/*' />
    public ADLX_RESULT GetPixelFormat([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayPixelFormat** ppPixelFormat)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayPixelFormat**, ADLX_RESULT>)(lpVtbl[15]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppPixelFormat);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetCustomColor"]/*' />
    public ADLX_RESULT GetCustomColor([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayCustomColor** ppCustomColor)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayCustomColor**, ADLX_RESULT>)(lpVtbl[16]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppCustomColor);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetHDCP"]/*' />
    public ADLX_RESULT GetHDCP([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayHDCP** ppHDCP)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayHDCP**, ADLX_RESULT>)(lpVtbl[17]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppHDCP);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetCustomResolution"]/*' />
    public ADLX_RESULT GetCustomResolution([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayCustomResolution** ppCustomResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayCustomResolution**, ADLX_RESULT>)(lpVtbl[18]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppCustomResolution);
    }

    /// <include file='IADLXDisplayServices.xml' path='doc/member[@name="IADLXDisplayServices.GetVariBright"]/*' />
    public ADLX_RESULT GetVariBright([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayVariBright** ppVariBright)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayServices*, IADLXDisplay*, IADLXDisplayVariBright**, ADLX_RESULT>)(lpVtbl[19]))((IADLXDisplayServices*)Unsafe.AsPointer(ref this), pDisplay, ppVariBright);
    }
}
