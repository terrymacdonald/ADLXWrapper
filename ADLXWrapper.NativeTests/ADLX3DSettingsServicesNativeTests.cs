using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

[Collection("NativeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLX3DSettingsServicesNativeTests
{
    private readonly NativeSession _session;

    public ADLX3DSettingsServicesNativeTests(NativeSessionFixture fixture)
    {
        _session = fixture.Session;
    }

    [SkippableFact]
    public void Three_d_services_acquire_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out _);
    }

    [SkippableFact]
    public void Three_d_services_query_interface_v1_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        QueryInterfaceOrSkip((IADLXInterface*)services, nameof(IADLX3DSettingsServices1));
    }

    [SkippableFact]
    public void Three_d_services_query_interface_v2_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        QueryInterfaceOrSkip((IADLXInterface*)services, nameof(IADLX3DSettingsServices2));
    }

    [SkippableFact]
    public void Anti_lag_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DAntiLag* antiLag = null;
            var result = services->GetAntiLag(gpu, &antiLag);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Anti-Lag not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var antiLagPtr = new ComPtr<IADLX3DAntiLag>(antiLag);

            bool supported = false;
            if (!AssertResultOrContinue(antiLag->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(antiLag->IsEnabled(&enabled));

            IADLX3DAntiLag1* antiLag1 = null;
            var qi = QueryInterface((IADLXInterface*)antiLag, nameof(IADLX3DAntiLag1), (void**)&antiLag1);
            if (qi == ADLX_RESULT.ADLX_OK && antiLag1 != null)
            {
                using var antiLag1Ptr = new ComPtr<IADLX3DAntiLag1>(antiLag1);
                ADLX_ANTILAG_STATE level = 0;
                AssertResultOrContinue(antiLag1->GetLevel(&level));
            }
        });
    }

    [SkippableFact]
    public void Chill_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DChill* chill = null;
            var result = services->GetChill(gpu, &chill);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Chill not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var chillPtr = new ComPtr<IADLX3DChill>(chill);

            bool supported = false;
            if (!AssertResultOrContinue(chill->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(chill->IsEnabled(&enabled));

            ADLX_IntRange range = default;
            AssertResultOrContinue(chill->GetFPSRange(&range));

            int minFps = 0;
            AssertResultOrContinue(chill->GetMinFPS(&minFps));

            int maxFps = 0;
            AssertResultOrContinue(chill->GetMaxFPS(&maxFps));
        });
    }

    [SkippableFact]
    public void Boost_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DBoost* boost = null;
            var result = services->GetBoost(gpu, &boost);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Boost not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var boostPtr = new ComPtr<IADLX3DBoost>(boost);

            bool supported = false;
            if (!AssertResultOrContinue(boost->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(boost->IsEnabled(&enabled));

            ADLX_IntRange range = default;
            AssertResultOrContinue(boost->GetResolutionRange(&range));

            int resolution = 0;
            AssertResultOrContinue(boost->GetResolution(&resolution));
        });
    }

    [SkippableFact]
    public void Image_sharpening_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DImageSharpening* sharpening = null;
            var result = services->GetImageSharpening(gpu, &sharpening);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Image Sharpening not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var sharpeningPtr = new ComPtr<IADLX3DImageSharpening>(sharpening);

            bool supported = false;
            if (!AssertResultOrContinue(sharpening->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(sharpening->IsEnabled(&enabled));
        });
    }

    [SkippableFact]
    public void Image_sharpen_desktop_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DImageSharpenDesktop* sharpening = null;
            var result = QueryImageSharpenDesktop(services, gpu, &sharpening);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return;
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var sharpeningPtr = new ComPtr<IADLX3DImageSharpenDesktop>(sharpening);

            bool supported = false;
            if (!AssertResultOrContinue(sharpening->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(sharpening->IsEnabled(&enabled));
        });
    }

    [SkippableFact]
    public void Enhanced_sync_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DEnhancedSync* enhanced = null;
            var result = services->GetEnhancedSync(gpu, &enhanced);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Enhanced Sync not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var enhancedPtr = new ComPtr<IADLX3DEnhancedSync>(enhanced);

            bool supported = false;
            if (!AssertResultOrContinue(enhanced->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(enhanced->IsEnabled(&enabled));
        });
    }

    [SkippableFact]
    public void Wait_for_vertical_refresh_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DWaitForVerticalRefresh* vsync = null;
            var result = services->GetWaitForVerticalRefresh(gpu, &vsync);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Wait for Vertical Refresh not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var vsyncPtr = new ComPtr<IADLX3DWaitForVerticalRefresh>(vsync);

            bool supported = false;
            if (!AssertResultOrContinue(vsync->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(vsync->IsEnabled(&enabled));

            ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE mode = 0;
            AssertResultOrContinue(vsync->GetMode(&mode));
        });
    }

    [SkippableFact]
    public void Frame_rate_target_control_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DFrameRateTargetControl* frtc = null;
            var result = services->GetFrameRateTargetControl(gpu, &frtc);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Frame Rate Target Control not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var frtcPtr = new ComPtr<IADLX3DFrameRateTargetControl>(frtc);

            bool supported = false;
            if (!AssertResultOrContinue(frtc->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(frtc->IsEnabled(&enabled));

            ADLX_IntRange range = default;
            AssertResultOrContinue(frtc->GetFPSRange(&range));

            int fps = 0;
            AssertResultOrContinue(frtc->GetFPS(&fps));
        });
    }

    [SkippableFact]
    public void Anti_aliasing_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DAntiAliasing* aa = null;
            var result = services->GetAntiAliasing(gpu, &aa);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Anti-Aliasing not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var aaPtr = new ComPtr<IADLX3DAntiAliasing>(aa);

            bool supported = false;
            if (!AssertResultOrContinue(aa->IsSupported(&supported)) || !supported)
                return;

            ADLX_ANTI_ALIASING_MODE mode = 0;
            AssertResultOrContinue(aa->GetMode(&mode));

            ADLX_ANTI_ALIASING_LEVEL level = 0;
            AssertResultOrContinue(aa->GetLevel(&level));

            ADLX_ANTI_ALIASING_METHOD method = 0;
            AssertResultOrContinue(aa->GetMethod(&method));
        });
    }

    [SkippableFact]
    public void Morphological_anti_aliasing_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DMorphologicalAntiAliasing* mlaa = null;
            var result = services->GetMorphologicalAntiAliasing(gpu, &mlaa);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Morphological Anti-Aliasing not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var mlaaPtr = new ComPtr<IADLX3DMorphologicalAntiAliasing>(mlaa);

            bool supported = false;
            if (!AssertResultOrContinue(mlaa->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(mlaa->IsEnabled(&enabled));
        });
    }

    [SkippableFact]
    public void Anisotropic_filtering_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DAnisotropicFiltering* af = null;
            var result = services->GetAnisotropicFiltering(gpu, &af);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Anisotropic Filtering not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var afPtr = new ComPtr<IADLX3DAnisotropicFiltering>(af);

            bool supported = false;
            if (!AssertResultOrContinue(af->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(af->IsEnabled(&enabled));

            ADLX_ANISOTROPIC_FILTERING_LEVEL level = 0;
            AssertResultOrContinue(af->GetLevel(&level));
        });
    }

    [SkippableFact]
    public void Tessellation_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DTessellation* tessellation = null;
            var result = services->GetTessellation(gpu, &tessellation);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Tessellation not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var tessellationPtr = new ComPtr<IADLX3DTessellation>(tessellation);

            bool supported = false;
            if (!AssertResultOrContinue(tessellation->IsSupported(&supported)) || !supported)
                return;

            ADLX_TESSELLATION_LEVEL level = 0;
            AssertResultOrContinue(tessellation->GetLevel(&level));
        });
    }

    [SkippableFact]
    public void Radeon_super_resolution_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);

        IADLX3DRadeonSuperResolution* rsr = null;
        var result = services->GetRadeonSuperResolution(&rsr);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Radeon Super Resolution not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        using var rsrPtr = new ComPtr<IADLX3DRadeonSuperResolution>(rsr);

        bool supported = false;
        if (!AssertResultOrContinue(rsr->IsSupported(&supported)) || !supported)
            return;

        bool enabled = false;
        AssertResultOrContinue(rsr->IsEnabled(&enabled));

        ADLX_IntRange range = default;
        AssertResultOrContinue(rsr->GetSharpnessRange(&range));

        int sharpness = 0;
        AssertResultOrContinue(rsr->GetSharpness(&sharpness));
    }

    [SkippableFact]
    public void Reset_shader_cache_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLX3DResetShaderCache* reset = null;
            var result = services->GetResetShaderCache(gpu, &reset);
            Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Reset Shader Cache not supported on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var resetPtr = new ComPtr<IADLX3DResetShaderCache>(reset);

            bool supported = false;
            AssertResultOrContinue(reset->IsSupported(&supported));
        });
    }

    [SkippableFact]
    public void Fluid_motion_frames_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);

        IADLX3DAMDFluidMotionFrames* fmf = null;
        ADLX_RESULT result;
        try
        {
            result = QueryFluidMotionFrames(services, &fmf);
        }
        catch (SEHException ex)
        {
            Skip.If(true, $"AMD Fluid Motion Frames call threw SEH (treating as unsupported on this platform): {ex.Message}");
            return;
        }

        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "AMD Fluid Motion Frames not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        using var fmfPtr = new ComPtr<IADLX3DAMDFluidMotionFrames>(fmf);

        bool supported = false;
        if (!AssertResultOrContinue(fmf->IsSupported(&supported)) || !supported)
            return;

        bool enabled = false;
        AssertResultOrContinue(fmf->IsEnabled(&enabled));
    }

    [SkippableFact]
    public void Three_d_settings_changed_listener_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = Get3DServicesComPtrOrSkip(out var services);

        IADLX3DSettingsChangedHandling* handling = null;
        var result = services->Get3DSettingsChangedHandling(&handling);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "3D settings change handling not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        using var handlingPtr = new ComPtr<IADLX3DSettingsChangedHandling>(handling);

        using var listener = new Dummy3DSettingsChangedListener();
        var addResult = handling->Add3DSettingsEventListener(listener.Pointer);
        Skip.If(addResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "3D settings change listeners not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, addResult);

        var removeResult = handling->Remove3DSettingsEventListener(listener.Pointer);
        Assert.Equal(ADLX_RESULT.ADLX_OK, removeResult);
    }

    private void SkipIfNoAdlxSupport()
    {
        Skip.IfNot(ADLXApiHelper.IsADLXDllAvailable(out var dllError), $"ADLX DLL unavailable: {dllError}");
        Skip.IfNot(ADLXHardwareDetection.HasAMDGPU(out var gpuError), $"AMD GPU not detected: {gpuError}");
    }

    private unsafe ComPtr<IADLX3DSettingsServices> Get3DServicesComPtrOrSkip(out IADLX3DSettingsServices* services)
    {
        services = null;
        ADLX_RESULT result;
        var system = _session.System;
        fixed (IADLX3DSettingsServices** pServices = &services)
        {
            result = system->Get3DSettingsServices(pServices);
        }

        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "3D Settings services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return new ComPtr<IADLX3DSettingsServices>(services);
    }

    private unsafe ComPtr<IADLXGPUList> GetGpuListOrSkip(out IADLXGPUList* list)
    {
        IADLXGPUList* local = null;
        var result = _session.System->GetGPUs(&local);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        list = local;
        return new ComPtr<IADLXGPUList>(local);
    }

    private static bool AssertResultOrContinue(ADLX_RESULT result)
    {
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE)
            return false;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return true;
    }

    private static unsafe ADLX_RESULT QueryInterface(IADLXInterface* iface, string name, void** obj)
    {
        var terminated = name + "\0";
        fixed (char* chars = terminated)
        {
            return iface->QueryInterface((ushort*)chars, obj);
        }
    }

    private static unsafe void QueryInterfaceOrSkip(IADLXInterface* iface, string name)
    {
        void* queried = null;
        var result = QueryInterface(iface, name, &queried);
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE)
            return;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)queried);
        ((IADLXInterface*)queried)->Release();
    }

    private unsafe delegate void GpuVisitor(IADLXGPU* gpu);

    private unsafe void ForEachGpu(IADLXGPUList* list, GpuVisitor visitor)
    {
        var count = list->Size();
        Skip.If(count == 0, "No GPUs returned by ADLX.");

        for (uint i = 0; i < count; i++)
        {
            IADLXGPU* gpu = null;
            var gpuResult = list->At(i, &gpu);
            if (gpuResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                continue;

            Assert.Equal(ADLX_RESULT.ADLX_OK, gpuResult);
            using var gpuPtr = new ComPtr<IADLXGPU>(gpu);
            visitor(gpu);
        }
    }

    private static unsafe ADLX_RESULT QueryImageSharpenDesktop(IADLX3DSettingsServices* services, IADLXGPU* gpu, IADLX3DImageSharpenDesktop** obj)
    {
        var result = ADLX_RESULT.ADLX_NOT_SUPPORTED;
        IADLX3DSettingsServices2* services2 = null;
        var qi2 = QueryInterface((IADLXInterface*)services, nameof(IADLX3DSettingsServices2), (void**)&services2);
        if (qi2 == ADLX_RESULT.ADLX_OK && services2 != null)
        {
            result = services2->GetImageSharpenDesktop(gpu, obj);
            ((IADLXInterface*)services2)->Release();
        }

        return result;
    }

    private static unsafe ADLX_RESULT QueryFluidMotionFrames(IADLX3DSettingsServices* services, IADLX3DAMDFluidMotionFrames** obj)
    {
        var result = ADLX_RESULT.ADLX_NOT_SUPPORTED;
        IADLX3DSettingsServices1* services1 = null;
        var qi1 = QueryInterface((IADLXInterface*)services, nameof(IADLX3DSettingsServices1), (void**)&services1);
        if (qi1 == ADLX_RESULT.ADLX_OK && services1 != null)
        {
            result = services1->GetAMDFluidMotionFrames(obj);
            ((IADLXInterface*)services1)->Release();
        }

        return result;
    }

    private sealed unsafe class Dummy3DSettingsChangedListener : IDisposable
    {
        private IntPtr _vtable;
        private IntPtr _instance;

        public IADLX3DSettingsChangedListener* Pointer => (IADLX3DSettingsChangedListener*)_instance;

        public Dummy3DSettingsChangedListener()
        {
            _vtable = Marshal.AllocHGlobal(IntPtr.Size);
            *((IntPtr*)_vtable) = (IntPtr)(delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedListener*, IADLX3DSettingsChangedEvent*, byte>)&OnChanged;

            _instance = Marshal.AllocHGlobal(sizeof(IADLX3DSettingsChangedListener));
            ((IADLX3DSettingsChangedListener*)_instance)->lpVtbl = (void**)_vtable;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static byte OnChanged(IADLX3DSettingsChangedListener* self, IADLX3DSettingsChangedEvent* evt) => 1;

        public void Dispose()
        {
            if (_instance != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_instance);
                _instance = IntPtr.Zero;
            }

            if (_vtable != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_vtable);
                _vtable = IntPtr.Zero;
            }
        }
    }
}
