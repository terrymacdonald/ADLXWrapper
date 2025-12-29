using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

[Collection("NativeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLXPerformanceMonitoringServicesNativeTests
{
    private readonly NativeSession _session;

    public ADLXPerformanceMonitoringServicesNativeTests(NativeSessionFixture fixture)
    {
        _session = fixture.Session;
    }

    [SkippableFact]
    public void Perf_monitoring_services_acquire_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetPerfServicesComPtrOrSkip(out _);
    }

    [SkippableFact]
    public void Perf_monitoring_services_query_interface_v1_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetPerfServicesComPtrOrSkip(out var services);
        QueryInterfaceOrSkip((IADLXInterface*)services, "IADLXPerformanceMonitoringServices1");
    }

    [SkippableFact]
    public void Perf_monitoring_services_query_interface_v2_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetPerfServicesComPtrOrSkip(out var services);
        QueryInterfaceOrSkip((IADLXInterface*)services, "IADLXPerformanceMonitoringServices2");
    }

    [SkippableFact]
    public void System_metrics_support_and_current_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetPerfServicesComPtrOrSkip(out var services);

        IADLXSystemMetricsSupport* support = null;
        var supportResult = services->GetSupportedSystemMetrics(&support);
        Skip.If(supportResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "System metrics support not available on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, supportResult);
        using var supportPtr = new ComPtr<IADLXSystemMetricsSupport>(support);

        bool cpuSupported = false;
        AssertResultOrContinue(support->IsSupportedCPUUsage(&cpuSupported));

        bool ramSupported = false;
        AssertResultOrContinue(support->IsSupportedSystemRAM(&ramSupported));

        IADLXSystemMetrics* metrics = null;
        var metricsResult = services->GetCurrentSystemMetrics(&metrics);
        Skip.If(metricsResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "System metrics not available on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, metricsResult);
        using var metricsPtr = new ComPtr<IADLXSystemMetrics>(metrics);

        if (cpuSupported)
        {
            double cpuUsage = 0;
            AssertResultOrContinue(metrics->CPUUsage(&cpuUsage));
        }

        if (ramSupported)
        {
            int ramUsage = 0;
            AssertResultOrContinue(metrics->SystemRAM(&ramUsage));
        }
    }

    [SkippableFact]
    public void Gpu_metrics_usage_and_clocks_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetPerfServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            if (!GetGpuSupport(services, gpu, out var support))
                return;
            using var supportPtr = new ComPtr<IADLXGPUMetricsSupport>(support);

            bool usageSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUUsage(&usageSupported));

            bool clockSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUClockSpeed(&clockSupported));

            bool vramClockSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUVRAMClockSpeed(&vramClockSupported));

            using var metricsPtr = GetGpuMetricsOrSkip(services, gpu, out var metrics);

            if (usageSupported)
            {
                double gpuUsage = 0;
                AssertResultOrContinue(metrics->GPUUsage(&gpuUsage));
            }

            if (clockSupported)
            {
                int gpuClock = 0;
                AssertResultOrContinue(metrics->GPUClockSpeed(&gpuClock));
            }

            if (vramClockSupported)
            {
                int vramClock = 0;
                AssertResultOrContinue(metrics->GPUVRAMClockSpeed(&vramClock));
            }
        });
    }

    [SkippableFact]
    public void Gpu_metrics_temperatures_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetPerfServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            if (!GetGpuSupport(services, gpu, out var support))
                return;
            using var supportPtr = new ComPtr<IADLXGPUMetricsSupport>(support);

            bool tempSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUTemperature(&tempSupported));

            bool hotspotSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUHotspotTemperature(&hotspotSupported));

            bool intakeTempSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUIntakeTemperature(&intakeTempSupported));

            var memTempSupport = TrySupport1Or2(support);

            using var metricsPtr = GetGpuMetricsOrSkip(services, gpu, out var metrics);

            if (tempSupported)
            {
                double gpuTemp = 0;
                AssertResultOrContinue(metrics->GPUTemperature(&gpuTemp));
            }

            if (hotspotSupported)
            {
                double hotspotTemp = 0;
                AssertResultOrContinue(metrics->GPUHotspotTemperature(&hotspotTemp));
            }

            if (intakeTempSupported)
            {
                double intakeTemp = 0;
                AssertResultOrContinue(metrics->GPUIntakeTemperature(&intakeTemp));
            }

            IADLXGPUMetrics1* metrics1 = null;
            var qi1 = QueryInterface((IADLXInterface*)metrics, nameof(IADLXGPUMetrics1), (void**)&metrics1);
            if (qi1 == ADLX_RESULT.ADLX_OK && metrics1 != null)
            {
                using var m1Ptr = new ComPtr<IADLXGPUMetrics1>(metrics1);
                if (memTempSupport.HasValue && memTempSupport.Value)
                {
                    IADLXGPUMetrics2* metrics2 = null;
                    var qi2 = QueryInterface((IADLXInterface*)metrics1, nameof(IADLXGPUMetrics2), (void**)&metrics2);
                    if (qi2 == ADLX_RESULT.ADLX_OK && metrics2 != null)
                    {
                        using var m2Ptr = new ComPtr<IADLXGPUMetrics2>(metrics2);
                        double memTemp = 0;
                        AssertResultOrContinue(metrics2->GPUMemoryTemperature(&memTemp));
                    }
                }
            }
        });
    }

    [SkippableFact]
    public void Gpu_metrics_power_and_fan_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetPerfServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            if (!GetGpuSupport(services, gpu, out var support))
                return;
            using var supportPtr = new ComPtr<IADLXGPUMetricsSupport>(support);

            bool powerSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUPower(&powerSupported));

            bool totalBoardPowerSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUTotalBoardPower(&totalBoardPowerSupported));

            bool fanSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUFanSpeed(&fanSupported));

            using var metricsPtr = GetGpuMetricsOrSkip(services, gpu, out var metrics);

            if (powerSupported)
            {
                double gpuPower = 0;
                AssertResultOrContinue(metrics->GPUPower(&gpuPower));
            }

            if (totalBoardPowerSupported)
            {
                double boardPower = 0;
                AssertResultOrContinue(metrics->GPUTotalBoardPower(&boardPower));
            }

            if (fanSupported)
            {
                int fanSpeed = 0;
                AssertResultOrContinue(metrics->GPUFanSpeed(&fanSpeed));
            }
        });
    }

    [SkippableFact]
    public void Gpu_metrics_memory_and_voltage_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetPerfServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            if (!GetGpuSupport(services, gpu, out var support))
                return;
            using var supportPtr = new ComPtr<IADLXGPUMetricsSupport>(support);

            bool vramSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUVRAM(&vramSupported));

            bool voltageSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUVoltage(&voltageSupported));

            using var metricsPtr = GetGpuMetricsOrSkip(services, gpu, out var metrics);

            if (vramSupported)
            {
                int vram = 0;
                AssertResultOrContinue(metrics->GPUVRAM(&vram));
            }

            if (voltageSupported)
            {
                int voltage = 0;
                AssertResultOrContinue(metrics->GPUVoltage(&voltage));
            }
        });
    }

    [SkippableFact]
    public void Gpu_metrics_npu_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetPerfServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            using var metricsPtr = GetGpuMetricsOrSkip(services, gpu, out var metrics);

            IADLXGPUMetrics1* metrics1 = null;
            var qi1 = QueryInterface((IADLXInterface*)metrics, nameof(IADLXGPUMetrics1), (void**)&metrics1);
            if (qi1 == ADLX_RESULT.ADLX_OK && metrics1 != null)
            {
                using var m1Ptr = new ComPtr<IADLXGPUMetrics1>(metrics1);

                int npuFreq = 0;
                AssertResultOrContinue(metrics1->NPUFrequency(&npuFreq));

                int npuActivity = 0;
                AssertResultOrContinue(metrics1->NPUActivityLevel(&npuActivity));
            }
        });
    }

    private void SkipIfNoAdlxSupport()
    {
        Skip.IfNot(ADLXApiHelper.IsADLXDllAvailable(out var dllError), $"ADLX DLL unavailable: {dllError}");
        Skip.IfNot(ADLXHardwareDetection.HasAMDGPU(out var gpuError), $"AMD GPU not detected: {gpuError}");
    }

    private unsafe ComPtr<IADLXPerformanceMonitoringServices> GetPerfServicesComPtrOrSkip(out IADLXPerformanceMonitoringServices* services)
    {
        services = null;
        ADLX_RESULT result;
        var system = _session.System;
        fixed (IADLXPerformanceMonitoringServices** pServices = &services)
        {
            result = system->GetPerformanceMonitoringServices(pServices);
        }

        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Performance monitoring services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return new ComPtr<IADLXPerformanceMonitoringServices>(services);
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
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            return false;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return true;
    }

    private static unsafe void QueryInterfaceOrSkip(IADLXInterface* iface, string name)
    {
        void* queried = null;
        var terminated = name + "\0";
        fixed (char* chars = terminated)
        {
            var result = iface->QueryInterface((ushort*)chars, &queried);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE)
            {
                return;
            }

            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)queried);
            ((IADLXInterface*)queried)->Release();
        }
    }

    private static unsafe ADLX_RESULT QueryInterface(IADLXInterface* iface, string name, void** obj)
    {
        var terminated = name + "\0";
        fixed (char* chars = terminated)
        {
            return iface->QueryInterface((ushort*)chars, obj);
        }
    }

    private unsafe delegate void GpuVisitor(IADLXGPU* gpu);

    private unsafe void ForEachGpu(IADLXGPUList* list, GpuVisitor action)
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
            action(gpu);
        }
    }

    private unsafe bool GetGpuSupport(IADLXPerformanceMonitoringServices* services, IADLXGPU* gpu, out IADLXGPUMetricsSupport* support)
    {
        IADLXGPUMetricsSupport* local = null;
        var supportResult = services->GetSupportedGPUMetrics(gpu, &local);
        if (supportResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            support = null;
            return false;
        }

        Assert.Equal(ADLX_RESULT.ADLX_OK, supportResult);
        support = local;
        return true;
    }

    private unsafe ComPtr<IADLXGPUMetrics> GetGpuMetricsOrSkip(IADLXPerformanceMonitoringServices* services, IADLXGPU* gpu, out IADLXGPUMetrics* metrics)
    {
        IADLXGPUMetrics* local = null;
        var metricsResult = services->GetCurrentGPUMetrics(gpu, &local);
        Skip.If(metricsResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU metrics not available on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, metricsResult);
        metrics = local;
        return new ComPtr<IADLXGPUMetrics>(local);
    }

    private unsafe bool? TrySupport1Or2(IADLXGPUMetricsSupport* support)
    {
        IADLXGPUMetricsSupport1* s1 = null;
        fixed (char* chars = (nameof(IADLXGPUMetricsSupport1) + "\0"))
        {
            var qi1 = support->QueryInterface((ushort*)chars, (void**)&s1);
            if (qi1 == ADLX_RESULT.ADLX_OK && s1 != null)
            {
                using var s1Ptr = new ComPtr<IADLXGPUMetricsSupport1>(s1);
                bool supported = false;
                AssertResultOrContinue(s1->IsSupportedGPUMemoryTemperature(&supported));
                return supported;
            }
        }

        IADLXGPUMetricsSupport2* s2 = null;
        fixed (char* chars = (nameof(IADLXGPUMetricsSupport2) + "\0"))
        {
            var qi2 = support->QueryInterface((ushort*)chars, (void**)&s2);
            if (qi2 == ADLX_RESULT.ADLX_OK && s2 != null)
            {
                using var s2Ptr = new ComPtr<IADLXGPUMetricsSupport2>(s2);
                bool supported = false;
                AssertResultOrContinue(s2->IsSupportedGPUMemoryTemperature(&supported));
                return supported;
            }
        }

        return null;
    }
}
