using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices"]/*' />
[NativeTypeName("struct IADLX3DSettingsServices : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DSettingsServices
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, int>)(lpVtbl[0]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, int>)(lpVtbl[1]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetAntiLag"]/*' />
    public ADLX_RESULT GetAntiLag([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAntiLag** pp3DAntiLag)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DAntiLag**, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DAntiLag);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetChill"]/*' />
    public ADLX_RESULT GetChill([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DChill** pp3DChill)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DChill**, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DChill);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetBoost"]/*' />
    public ADLX_RESULT GetBoost([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DBoost** pp3DBoost)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DBoost**, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DBoost);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetImageSharpening"]/*' />
    public ADLX_RESULT GetImageSharpening([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DImageSharpening** pp3DImageSharpening)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DImageSharpening**, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DImageSharpening);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetEnhancedSync"]/*' />
    public ADLX_RESULT GetEnhancedSync([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DEnhancedSync** pp3DEnhancedSync)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DEnhancedSync**, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DEnhancedSync);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetWaitForVerticalRefresh"]/*' />
    public ADLX_RESULT GetWaitForVerticalRefresh([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DWaitForVerticalRefresh** pp3DWaitForVerticalRefresh)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DWaitForVerticalRefresh**, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DWaitForVerticalRefresh);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetFrameRateTargetControl"]/*' />
    public ADLX_RESULT GetFrameRateTargetControl([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DFrameRateTargetControl** pp3DFrameRateTargetControl)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DFrameRateTargetControl**, ADLX_RESULT>)(lpVtbl[9]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DFrameRateTargetControl);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetAntiAliasing"]/*' />
    public ADLX_RESULT GetAntiAliasing([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAntiAliasing** pp3DAntiAliasing)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DAntiAliasing**, ADLX_RESULT>)(lpVtbl[10]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DAntiAliasing);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetMorphologicalAntiAliasing"]/*' />
    public ADLX_RESULT GetMorphologicalAntiAliasing([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DMorphologicalAntiAliasing** pp3DMorphologicalAntiAliasing)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DMorphologicalAntiAliasing**, ADLX_RESULT>)(lpVtbl[11]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DMorphologicalAntiAliasing);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetAnisotropicFiltering"]/*' />
    public ADLX_RESULT GetAnisotropicFiltering([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAnisotropicFiltering** pp3DAnisotropicFiltering)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DAnisotropicFiltering**, ADLX_RESULT>)(lpVtbl[12]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DAnisotropicFiltering);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetTessellation"]/*' />
    public ADLX_RESULT GetTessellation([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DTessellation** pp3DTessellation)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DTessellation**, ADLX_RESULT>)(lpVtbl[13]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DTessellation);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetRadeonSuperResolution"]/*' />
    public ADLX_RESULT GetRadeonSuperResolution(IADLX3DRadeonSuperResolution** pp3DRadeonSuperResolution)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLX3DRadeonSuperResolution**, ADLX_RESULT>)(lpVtbl[14]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pp3DRadeonSuperResolution);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.GetResetShaderCache"]/*' />
    public ADLX_RESULT GetResetShaderCache([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DResetShaderCache** pp3DResetShaderCache)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLXGPU*, IADLX3DResetShaderCache**, ADLX_RESULT>)(lpVtbl[15]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pGPU, pp3DResetShaderCache);
    }

    /// <include file='IADLX3DSettingsServices.xml' path='doc/member[@name="IADLX3DSettingsServices.Get3DSettingsChangedHandling"]/*' />
    public ADLX_RESULT Get3DSettingsChangedHandling(IADLX3DSettingsChangedHandling** pp3DSettingsChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsServices*, IADLX3DSettingsChangedHandling**, ADLX_RESULT>)(lpVtbl[16]))((IADLX3DSettingsServices*)Unsafe.AsPointer(ref this), pp3DSettingsChangedHandling);
    }
}
