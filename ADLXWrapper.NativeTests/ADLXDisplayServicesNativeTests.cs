using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

[Collection("NativeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLXDisplayServicesNativeTests
{
    private readonly NativeSession _session;

    public ADLXDisplayServicesNativeTests(NativeSessionFixture fixture)
    {
        _session = fixture.Session;
    }

    [SkippableFact]
    public void Display_services_acquire_native()
    {
        SkipIfNoAdlxSupport();

        IADLXDisplayServices* services = null;
        var result = _session.System->GetDisplaysServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        using var servicesPtr = new ComPtr<IADLXDisplayServices>(services);
    }

    [SkippableFact]
    public void Display_services_query_interface_v1_native()
    {
        SkipIfNoAdlxSupport();

        using var servicesPtr = GetDisplayServicesComPtrOrSkip(out var services);
        QueryInterfaceOrSkip(services, nameof(IADLXDisplayServices1));
    }

    [SkippableFact]
    public void Display_services_query_interface_v2_native()
    {
        SkipIfNoAdlxSupport();

        using var servicesPtr = GetDisplayServicesComPtrOrSkip(out var services);
        QueryInterfaceOrSkip(services, nameof(IADLXDisplayServices2));
    }

    [SkippableFact]
    public void Display_services_query_interface_v3_native()
    {
        SkipIfNoAdlxSupport();

        using var servicesPtr = GetDisplayServicesComPtrOrSkip(out var services);
        QueryInterfaceOrSkip(services, nameof(IADLXDisplayServices3));
    }

    [SkippableFact]
    public void Display_list_enumeration_native()
    {
        SkipIfNoAdlxSupport();

        var services = GetDisplayServicesOrSkip();
        uint numDisplays = 0;
        var countResult = services->GetNumberOfDisplays(&numDisplays);
        Skip.If(countResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display count not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, countResult);

        IADLXDisplayList* list = null;
        var listResult = services->GetDisplays(&list);
        Skip.If(listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, listResult);

        using var listPtr = new ComPtr<IADLXDisplayList>(list);
        var size = list->Size();
        Skip.If(size == 0, "No displays returned by ADLX.");
        Assert.Equal(numDisplays, size);
    }

    [SkippableFact]
    public void Display_identity_properties_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            uint manufacturerId = 0;
            AssertResultOrContinue(display->ManufacturerID(&manufacturerId));

            ADLX_DISPLAY_TYPE displayType = 0;
            AssertResultOrContinue(display->DisplayType(&displayType));

            ADLX_DISPLAY_CONNECTOR_TYPE connectorType = 0;
            AssertResultOrContinue(display->ConnectorType(&connectorType));

            nuint uniqueId = 0;
            if (AssertResultOrContinue(display->UniqueId(&uniqueId)))
            {
                Assert.NotEqual<nuint>(0, uniqueId);
            }
        });
    }

    [SkippableFact]
    public void Display_strings_and_edid_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            sbyte* namePtr = null;
            if (AssertResultOrContinue(display->Name(&namePtr)))
            {
                var name = Marshal.PtrToStringAnsi((IntPtr)namePtr) ?? string.Empty;
                Assert.False(string.IsNullOrWhiteSpace(name));
            }

            sbyte* edidPtr = null;
            if (AssertResultOrContinue(display->EDID(&edidPtr)))
            {
                var edid = Marshal.PtrToStringAnsi((IntPtr)edidPtr) ?? string.Empty;
                Assert.False(string.IsNullOrWhiteSpace(edid));
            }
        });
    }

    [SkippableFact]
    public void Display_timings_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            int width = 0, height = 0;
            if (AssertResultOrContinue(display->NativeResolution(&width, &height)))
            {
                Assert.True(width > 0 && height > 0);
            }

            double refreshRate = 0;
            if (AssertResultOrContinue(display->RefreshRate(&refreshRate)))
            {
                Assert.True(refreshRate > 0);
            }

            uint pixelClock = 0;
            if (AssertResultOrContinue(display->PixelClock(&pixelClock)))
            {
                Assert.True(pixelClock > 0);
            }

            ADLX_DISPLAY_SCAN_TYPE scanType = 0;
            AssertResultOrContinue(display->ScanType(&scanType));
        });
    }

    [SkippableFact]
    public void Display_gpu_link_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXGPU* gpu = null;
            if (AssertResultOrContinue(display->GetGPU(&gpu)))
            {
                using var gpuPtr = new ComPtr<IADLXGPU>(gpu);
                Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)gpu);
            }
        });
    }

    [SkippableFact]
    public void Display_feature_freesync_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplayFreeSync* freeSync = null;
            if (AcquireDisplayFeatureOrSkip(services->GetFreeSync(display, &freeSync), freeSync))
            {
                bool supported = false;
                AssertResultOrContinue(freeSync->IsSupported(&supported));
            }
        });
    }

    [SkippableFact]
    public void Display_feature_vsr_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplayVSR* vsr = null;
            if (AcquireDisplayFeatureOrSkip(services->GetVirtualSuperResolution(display, &vsr), vsr))
            {
                bool supported = false;
                AssertResultOrContinue(vsr->IsSupported(&supported));
            }
        });
    }

    [SkippableFact]
    public void Display_feature_scaling_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplayGPUScaling* gpuScaling = null;
            AcquireDisplayFeatureOrSkip(services->GetGPUScaling(display, &gpuScaling), gpuScaling);

            IADLXDisplayScalingMode* scalingMode = null;
            AcquireDisplayFeatureOrSkip(services->GetScalingMode(display, &scalingMode), scalingMode);

            IADLXDisplayIntegerScaling* integerScaling = null;
            AcquireDisplayFeatureOrSkip(services->GetIntegerScaling(display, &integerScaling), integerScaling);
        });
    }

    [SkippableFact]
    public void Display_feature_color_depth_and_pixel_format_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplayColorDepth* colorDepth = null;
            AcquireDisplayFeatureOrSkip(services->GetColorDepth(display, &colorDepth), colorDepth);

            IADLXDisplayPixelFormat* pixelFormat = null;
            AcquireDisplayFeatureOrSkip(services->GetPixelFormat(display, &pixelFormat), pixelFormat);
        });
    }

    [SkippableFact]
    public void Display_feature_custom_color_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplayCustomColor* customColor = null;
            AcquireDisplayFeatureOrSkip(services->GetCustomColor(display, &customColor), customColor);
        });
    }

    [SkippableFact]
    public void Display_feature_hdcp_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplayHDCP* hdcp = null;
            if (AcquireDisplayFeatureOrSkip(services->GetHDCP(display, &hdcp), hdcp))
            {
                bool supported = false;
                AssertResultOrContinue(hdcp->IsSupported(&supported));
            }
        });
    }

    [SkippableFact]
    public void Display_feature_custom_resolution_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplayCustomResolution* customResolution = null;
            AcquireDisplayFeatureOrSkip(services->GetCustomResolution(display, &customResolution), customResolution);
        });
    }

    [SkippableFact]
    public void Display_feature_varibright_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplayVariBright* variBright = null;
            AcquireDisplayFeatureOrSkip(services->GetVariBright(display, &variBright), variBright);
        });
    }

    [SkippableFact]
    public void Display_feature_3dlut_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplay3DLUT* lut = null;
            AcquireDisplayFeatureOrSkip(services->Get3DLUT(display, &lut), lut);
        });
    }

    [SkippableFact]
    public void Display_feature_gamma_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplayGamma* gamma = null;
            AcquireDisplayFeatureOrSkip(services->GetGamma(display, &gamma), gamma);
        });
    }

    [SkippableFact]
    public void Display_feature_gamut_all_displays_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDisplay((services, display, index) =>
        {
            IADLXDisplayGamut* gamut = null;
            AcquireDisplayFeatureOrSkip(services->GetGamut(display, &gamut), gamut);
        });
    }

    [SkippableFact]
    public void Display_events_add_remove_listener_native()
    {
        SkipIfNoAdlxSupport();
        var services = GetDisplayServicesOrSkip();

        IADLXDisplayChangedHandling* handling = null;
        var result = services->GetDisplayChangedHandling(&handling);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display changed handling not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var handlingPtr = new ComPtr<IADLXDisplayChangedHandling>(handling);
        using var listener = new DummyDisplayListChangedListener();

        var addResult = handling->AddDisplayListEventListener(listener.Pointer);
        Skip.If(addResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display list event listeners not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, addResult);

        var removeResult = handling->RemoveDisplayListEventListener(listener.Pointer);
        Assert.Equal(ADLX_RESULT.ADLX_OK, removeResult);
    }

    private void SkipIfNoAdlxSupport()
    {
        Skip.IfNot(ADLXApiHelper.IsADLXDllAvailable(out var dllError), $"ADLX DLL unavailable: {dllError}");
        Skip.IfNot(ADLXHardwareDetection.HasAMDGPU(out var gpuError), $"AMD GPU not detected: {gpuError}");
    }

    private unsafe ComPtr<IADLXDisplayServices> GetDisplayServicesComPtrOrSkip(out IADLXDisplayServices* services)
    {
        services = null;
        var system = _session.System;
        ADLX_RESULT result;
        fixed (IADLXDisplayServices** pServices = &services)
        {
            result = system->GetDisplaysServices(pServices);
        }
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return new ComPtr<IADLXDisplayServices>(services);
    }

    private unsafe IADLXDisplayServices* GetDisplayServicesOrSkip()
    {
        IADLXDisplayServices* services = null;
        var result = _session.System->GetDisplaysServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return services;
    }

    private unsafe ComPtr<IADLXDisplayList> GetDisplayListOrSkip(IADLXDisplayServices* services, out IADLXDisplayList* list)
    {
        IADLXDisplayList* localList = null;
        var listResult = services->GetDisplays(&localList);
        Skip.If(listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, listResult);
        list = localList;
        return new ComPtr<IADLXDisplayList>(localList);
    }

    private unsafe void ForEachDisplay(DisplayAction action)
    {
        using var servicesPtr = GetDisplayServicesComPtrOrSkip(out var services);
        using var listPtr = GetDisplayListOrSkip(services, out var list);

        var count = list->Size();
        Skip.If(count == 0, "No displays returned by ADLX.");

        for (uint i = 0; i < count; i++)
        {
            IADLXDisplay* display = null;
            var atResult = list->At(i, &display);
            if (atResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                continue;
            Assert.Equal(ADLX_RESULT.ADLX_OK, atResult);

            using var displayPtr = new ComPtr<IADLXDisplay>(display);
            action(services, display, i);
        }
    }

    private unsafe void QueryInterfaceOrSkip<T>(T* iface, string name) where T : unmanaged
    {
        void* queried = null;
        var terminated = name + "\0";
        fixed (char* chars = terminated)
        {
            var result = ((IADLXInterface*)iface)->QueryInterface((ushort*)chars, &queried);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE)
            {
                return;
            }

            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)queried);
            ((IADLXInterface*)queried)->Release();
        }
    }

    private static bool AssertResultOrContinue(ADLX_RESULT result)
    {
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            return false;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return true;
    }

    private unsafe bool AcquireDisplayFeatureOrSkip<TInterface>(ADLX_RESULT result, TInterface* feature)
        where TInterface : unmanaged
    {
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            return false;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        using var featurePtr = new ComPtr<TInterface>(feature);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)feature);
        return true;
    }

    private sealed unsafe class DummyDisplayListChangedListener : IDisposable
    {
        private IntPtr _vtable;
        private IntPtr _instance;

        public IADLXDisplayListChangedListener* Pointer => (IADLXDisplayListChangedListener*)_instance;

        public DummyDisplayListChangedListener()
        {
            _vtable = Marshal.AllocHGlobal(IntPtr.Size);
            *((IntPtr*)_vtable) = (IntPtr)(delegate* unmanaged[Stdcall]<IADLXDisplayListChangedListener*, IADLXDisplayList*, byte>)&OnDisplayListChanged;

            _instance = Marshal.AllocHGlobal(sizeof(IADLXDisplayListChangedListener));
            ((IADLXDisplayListChangedListener*)_instance)->lpVtbl = (void**)_vtable;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static byte OnDisplayListChanged(IADLXDisplayListChangedListener* self, IADLXDisplayList* pNewDisplay) => 1;

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

    private unsafe delegate void DisplayAction(IADLXDisplayServices* services, IADLXDisplay* display, uint index);
}
