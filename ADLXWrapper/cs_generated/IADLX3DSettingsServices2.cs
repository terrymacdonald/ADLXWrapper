using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DSettingsServices2 : adlx::IADLX3DSettingsServices1")]
public unsafe partial struct IADLX3DSettingsServices2
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLX3DSettingsServices2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLX3DSettingsServices2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLX3DSettingsServices2* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetAntiLag(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAntiLag** pp3DAntiLag);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetChill(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DChill** pp3DChill);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetBoost(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DBoost** pp3DBoost);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetImageSharpening(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DImageSharpening** pp3DImageSharpening);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetEnhancedSync(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DEnhancedSync** pp3DEnhancedSync);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetWaitForVerticalRefresh(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DWaitForVerticalRefresh** pp3DWaitForVerticalRefresh);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFrameRateTargetControl(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DFrameRateTargetControl** pp3DFrameRateTargetControl);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetAntiAliasing(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAntiAliasing** pp3DAntiAliasing);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMorphologicalAntiAliasing(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DMorphologicalAntiAliasing** pp3DMorphologicalAntiAliasing);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetAnisotropicFiltering(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAnisotropicFiltering** pp3DAnisotropicFiltering);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTessellation(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DTessellation** pp3DTessellation);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetRadeonSuperResolution(IADLX3DSettingsServices2* pThis, IADLX3DRadeonSuperResolution** pp3DRadeonSuperResolution);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetResetShaderCache(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DResetShaderCache** pp3DResetShaderCache);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Get3DSettingsChangedHandling(IADLX3DSettingsServices2* pThis, IADLX3DSettingsChangedHandling** pp3DSettingsChangedHandling);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetAMDFluidMotionFrames(IADLX3DSettingsServices2* pThis, IADLX3DAMDFluidMotionFrames** pp3DAMDFluidMotionFrames);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetImageSharpenDesktop(IADLX3DSettingsServices2* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DImageSharpenDesktop** pp3DImageSharpenDesktop);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetAntiLag([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAntiLag** pp3DAntiLag)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetAntiLag>((IntPtr)(lpVtbl[3]))(pThis, pGPU, pp3DAntiLag);
        }
    }

    public ADLX_RESULT GetChill([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DChill** pp3DChill)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetChill>((IntPtr)(lpVtbl[4]))(pThis, pGPU, pp3DChill);
        }
    }

    public ADLX_RESULT GetBoost([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DBoost** pp3DBoost)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetBoost>((IntPtr)(lpVtbl[5]))(pThis, pGPU, pp3DBoost);
        }
    }

    public ADLX_RESULT GetImageSharpening([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DImageSharpening** pp3DImageSharpening)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetImageSharpening>((IntPtr)(lpVtbl[6]))(pThis, pGPU, pp3DImageSharpening);
        }
    }

    public ADLX_RESULT GetEnhancedSync([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DEnhancedSync** pp3DEnhancedSync)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetEnhancedSync>((IntPtr)(lpVtbl[7]))(pThis, pGPU, pp3DEnhancedSync);
        }
    }

    public ADLX_RESULT GetWaitForVerticalRefresh([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DWaitForVerticalRefresh** pp3DWaitForVerticalRefresh)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetWaitForVerticalRefresh>((IntPtr)(lpVtbl[8]))(pThis, pGPU, pp3DWaitForVerticalRefresh);
        }
    }

    public ADLX_RESULT GetFrameRateTargetControl([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DFrameRateTargetControl** pp3DFrameRateTargetControl)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFrameRateTargetControl>((IntPtr)(lpVtbl[9]))(pThis, pGPU, pp3DFrameRateTargetControl);
        }
    }

    public ADLX_RESULT GetAntiAliasing([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAntiAliasing** pp3DAntiAliasing)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetAntiAliasing>((IntPtr)(lpVtbl[10]))(pThis, pGPU, pp3DAntiAliasing);
        }
    }

    public ADLX_RESULT GetMorphologicalAntiAliasing([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DMorphologicalAntiAliasing** pp3DMorphologicalAntiAliasing)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMorphologicalAntiAliasing>((IntPtr)(lpVtbl[11]))(pThis, pGPU, pp3DMorphologicalAntiAliasing);
        }
    }

    public ADLX_RESULT GetAnisotropicFiltering([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DAnisotropicFiltering** pp3DAnisotropicFiltering)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetAnisotropicFiltering>((IntPtr)(lpVtbl[12]))(pThis, pGPU, pp3DAnisotropicFiltering);
        }
    }

    public ADLX_RESULT GetTessellation([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DTessellation** pp3DTessellation)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTessellation>((IntPtr)(lpVtbl[13]))(pThis, pGPU, pp3DTessellation);
        }
    }

    public ADLX_RESULT GetRadeonSuperResolution(IADLX3DRadeonSuperResolution** pp3DRadeonSuperResolution)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetRadeonSuperResolution>((IntPtr)(lpVtbl[14]))(pThis, pp3DRadeonSuperResolution);
        }
    }

    public ADLX_RESULT GetResetShaderCache([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DResetShaderCache** pp3DResetShaderCache)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetResetShaderCache>((IntPtr)(lpVtbl[15]))(pThis, pGPU, pp3DResetShaderCache);
        }
    }

    public ADLX_RESULT Get3DSettingsChangedHandling(IADLX3DSettingsChangedHandling** pp3DSettingsChangedHandling)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Get3DSettingsChangedHandling>((IntPtr)(lpVtbl[16]))(pThis, pp3DSettingsChangedHandling);
        }
    }

    public ADLX_RESULT GetAMDFluidMotionFrames(IADLX3DAMDFluidMotionFrames** pp3DAMDFluidMotionFrames)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetAMDFluidMotionFrames>((IntPtr)(lpVtbl[17]))(pThis, pp3DAMDFluidMotionFrames);
        }
    }

    public ADLX_RESULT GetImageSharpenDesktop([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLX3DImageSharpenDesktop** pp3DImageSharpenDesktop)
    {
        fixed (IADLX3DSettingsServices2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetImageSharpenDesktop>((IntPtr)(lpVtbl[18]))(pThis, pGPU, pp3DImageSharpenDesktop);
        }
    }
}
