using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

[Collection("NativeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLXSystemServicesNativeTests
{
    private readonly NativeSession _session;

    public ADLXSystemServicesNativeTests(NativeSessionFixture fixture)
    {
        _session = fixture.Session;
    }

    [SkippableFact]
    public void System_get_display_services_native()
    {
        SkipIfNoAdlxSupport();

        IADLXDisplayServices* displayServices = null;
        var result = _session.System->GetDisplaysServices(&displayServices);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLXDisplayServices>(displayServices);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)displayServices);
    }

    [SkippableFact]
    public void System_get_performance_monitoring_services_native()
    {
        SkipIfNoAdlxSupport();

        IADLXPerformanceMonitoringServices* services = null;
        var result = _session.System->GetPerformanceMonitoringServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Performance monitoring services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLXPerformanceMonitoringServices>(services);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)services);
    }

    [SkippableFact]
    public void System1_get_power_tuning_services_native()
    {
        SkipIfNoAdlxSupport();

        if (!TryGetSystem1(out var system1, out var skipReason))
        {
            Skip.If(true, skipReason);
        }

        IADLXPowerTuningServices* services = null;
        var result = system1->GetPowerTuningServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Power tuning services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLXPowerTuningServices>(services);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)services);
    }

    [SkippableFact]
    public void System2_get_multimedia_services_native()
    {
        SkipIfNoAdlxSupport();

        if (!TryGetSystem2(out var system2, out var skipReason))
        {
            Skip.If(true, skipReason);
        }

        IADLXMultimediaServices* services = null;
        var result = system2->GetMultimediaServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Multimedia services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLXMultimediaServices>(services);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)services);
    }

    [SkippableFact]
    public void System2_get_gpu_apps_list_changed_handling_native()
    {
        SkipIfNoAdlxSupport();

        if (!TryGetSystem2(out var system2, out var skipReason))
        {
            Skip.If(true, skipReason);
        }

        IADLXGPUAppsListChangedHandling* handling = null;
        var result = system2->GetGPUAppsListChangedHandling(&handling);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU apps list changed handling not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var handlingPtr = new ComPtr<IADLXGPUAppsListChangedHandling>(handling);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)handling);
    }

    [SkippableFact]
    public void System_get_desktop_services_native()
    {
        SkipIfNoAdlxSupport();

        IADLXDesktopServices* services = null;
        var result = _session.System->GetDesktopsServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLXDesktopServices>(services);
        IADLXDesktopList* list = null;
        var listResult = services->GetDesktops(&list);
        Skip.If(listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, listResult);

        using var listPtr = new ComPtr<IADLXDesktopList>(list);
        var count = list->Size();
        Skip.If(count == 0, "No desktops returned by ADLX.");
    }

    [SkippableFact]
    public void System_get_gpus_changed_handling_native()
    {
        SkipIfNoAdlxSupport();

        IADLXGPUsChangedHandling* handling = null;
        var result = _session.System->GetGPUsChangedHandling(&handling);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUs changed handling not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var handlingPtr = new ComPtr<IADLXGPUsChangedHandling>(handling);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)handling);
    }

    [SkippableFact]
    public void System_gpus_changed_add_remove_listener_native()
    {
        SkipIfNoAdlxSupport();

        IADLXGPUsChangedHandling* handling = null;
        var handlingResult = _session.System->GetGPUsChangedHandling(&handling);
        Skip.If(handlingResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUs changed handling not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, handlingResult);

        using var handlingPtr = new ComPtr<IADLXGPUsChangedHandling>(handling);
        using var listener = new DummyGpuEventListener();

        var addResult = handling->AddGPUsListEventListener(listener.Pointer);
        Skip.If(addResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU event listeners not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, addResult);

        var removeResult = handling->RemoveGPUsListEventListener(listener.Pointer);
        Assert.Equal(ADLX_RESULT.ADLX_OK, removeResult);
    }

    [SkippableFact]
    public void System_get_3d_settings_services_native()
    {
        SkipIfNoAdlxSupport();

        IADLX3DSettingsServices* services = null;
        var result = _session.System->Get3DSettingsServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "3D settings services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLX3DSettingsServices>(services);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)services);
    }

    [SkippableFact]
    public void System_get_gpu_tuning_services_native()
    {
        SkipIfNoAdlxSupport();

        IADLXGPUTuningServices* services = null;
        var result = _session.System->GetGPUTuningServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU tuning services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLXGPUTuningServices>(services);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)services);
    }

    [SkippableFact]
    public void System_total_system_ram_native()
    {
        SkipIfNoAdlxSupport();

        uint ramMb = 0;
        var result = _session.System->TotalSystemRAM(&ramMb);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Total system RAM not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        Assert.NotEqual<uint>(0, ramMb);
    }

    [SkippableFact]
    public unsafe void System_get_i2c_first_gpu_native()
    {
        SkipIfNoAdlxSupport();

        IADLXGPUList* gpuList = null;
        var listResult = _session.System->GetGPUs(&gpuList);
        Skip.If(listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, listResult);

        using var gpuListPtr = new ComPtr<IADLXGPUList>(gpuList);
        var count = gpuList->Size();
        Skip.If(count == 0, "No GPUs returned by ADLX.");

        IADLXGPU* gpu = null;
        var gpuResult = gpuList->At(0, &gpu);
        Skip.If(gpuResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU access not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, gpuResult);

        using var gpuPtr = new ComPtr<IADLXGPU>(gpu);
        IADLXI2C* i2c = null;
        var i2cResult = _session.System->GetI2C(gpu, &i2c);
        Skip.If(i2cResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "I2C not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, i2cResult);

        using var i2cPtr = new ComPtr<IADLXI2C>(i2c);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)i2c);
    }

    [SkippableFact]
    public void System_hybrid_graphics_type_native()
    {
        SkipIfNoAdlxSupport();

        ADLX_HG_TYPE hgType = 0;
        var result = _session.System->HybridGraphicsType(&hgType);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Hybrid graphics type not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
    }

    private sealed unsafe class DummyGpuEventListener : IDisposable
    {
        private IntPtr _vtable;
        private IntPtr _instance;

        public IADLXGPUsEventListener* Pointer => (IADLXGPUsEventListener*)_instance;

        public DummyGpuEventListener()
        {
            _vtable = Marshal.AllocHGlobal(IntPtr.Size);
            *((IntPtr*)_vtable) = (IntPtr)(delegate* unmanaged[Stdcall]<IADLXGPUsEventListener*, IADLXGPUList*, byte>)&OnGpuListChanged;

            _instance = Marshal.AllocHGlobal(sizeof(IADLXGPUsEventListener));
            ((IADLXGPUsEventListener*)_instance)->lpVtbl = (void**)_vtable;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static byte OnGpuListChanged(IADLXGPUsEventListener* self, IADLXGPUList* pNewGPUs) => 1;

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

    private void SkipIfNoAdlxSupport()
    {
        Skip.IfNot(ADLXApiHelper.IsADLXDllAvailable(out var dllError), $"ADLX DLL unavailable: {dllError}");
        Skip.IfNot(ADLXHardwareDetection.HasAMDGPU(out var gpuError), $"AMD GPU not detected: {gpuError}");
    }

    private unsafe bool TryGetSystem1(out IADLXSystem1* system1, out string skipReason)
    {
        system1 = null;
        skipReason = string.Empty;

        // Try friendly helper first to avoid hand-building IID strings
        if (ADLXUtils.TryQueryInterface((IntPtr)_session.System, nameof(IADLXSystem1), out var ifacePtr) && ifacePtr != IntPtr.Zero)
        {
            system1 = (IADLXSystem1*)ifacePtr;
            return true;
        }

        // Fall back to explicit QueryInterface to capture the ADLX_RESULT for skip vs fail handling
        ADLX_RESULT result;
        void* queried = null;
        var iidTerminated = nameof(IADLXSystem1) + "\0";
        fixed (char* iidChars = iidTerminated)
        {
            result = _session.System->QueryInterface((ushort*)iidChars, &queried);
        }

        if (result == ADLX_RESULT.ADLX_OK && queried != null)
        {
            system1 = (IADLXSystem1*)queried;
            return true;
        }

        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE)
        {
            skipReason = $"IADLXSystem1 not supported on this hardware/driver: {result}.";
            system1 = null;
            return false;
        }

        throw new ADLXException(result, "QueryInterface for IADLXSystem1 failed.");
    }

    private unsafe bool TryGetSystem2(out IADLXSystem2* system2, out string skipReason)
    {
        system2 = null;
        skipReason = string.Empty;

        // Try friendly helper first to avoid hand-building IID strings
        if (ADLXUtils.TryQueryInterface((IntPtr)_session.System, nameof(IADLXSystem2), out var ifacePtr) && ifacePtr != IntPtr.Zero)
        {
            system2 = (IADLXSystem2*)ifacePtr;
            return true;
        }

        // Fall back to explicit QueryInterface to capture the ADLX_RESULT for skip vs fail handling
        ADLX_RESULT result;
        void* queried = null;
        var iidTerminated = nameof(IADLXSystem2) + "\0";
        fixed (char* iidChars = iidTerminated)
        {
            result = _session.System->QueryInterface((ushort*)iidChars, &queried);
        }

        if (result == ADLX_RESULT.ADLX_OK && queried != null)
        {
            system2 = (IADLXSystem2*)queried;
            return true;
        }

        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE)
        {
            skipReason = $"IADLXSystem2 not supported on this hardware/driver: {result}.";
            system2 = null;
            return false;
        }

        throw new ADLXException(result, "QueryInterface for IADLXSystem2 failed.");
    }
}
