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
    public void Gpu_metrics_support_and_current_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetPerfServicesComPtrOrSkip(out var services);

        IADLXGPUList* gpuList = null;
        var listResult = _session.System->GetGPUs(&gpuList);
        Skip.If(listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, listResult);
        using var gpuListPtr = new ComPtr<IADLXGPUList>(gpuList);

        var count = gpuList->Size();
        Skip.If(count == 0, "No GPUs returned by ADLX.");

        for (uint i = 0; i < count; i++)
        {
            IADLXGPU* gpu = null;
            var gpuResult = gpuList->At(i, &gpu);
            if (gpuResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                continue;
            Assert.Equal(ADLX_RESULT.ADLX_OK, gpuResult);
            using var gpuPtr = new ComPtr<IADLXGPU>(gpu);

            IADLXGPUMetricsSupport* support = null;
            var supportResult = services->GetSupportedGPUMetrics(gpu, &support);
            Skip.If(supportResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU metrics support not available on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, supportResult);
            using var supportPtr = new ComPtr<IADLXGPUMetricsSupport>(support);

            bool usageSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUUsage(&usageSupported));

            bool clockSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUClockSpeed(&clockSupported));

            bool vramClockSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUVRAMClockSpeed(&vramClockSupported));

            bool tempSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUTemperature(&tempSupported));

            bool hotspotSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUHotspotTemperature(&hotspotSupported));

            bool powerSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUPower(&powerSupported));

            bool totalBoardPowerSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUTotalBoardPower(&totalBoardPowerSupported));

            bool fanSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUFanSpeed(&fanSupported));

            bool vramSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUVRAM(&vramSupported));

            bool voltageSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUVoltage(&voltageSupported));

            bool intakeTempSupported = false;
            AssertResultOrContinue(support->IsSupportedGPUIntakeTemperature(&intakeTempSupported));

            IADLXGPUMetrics* metrics = null;
            var metricsResult = services->GetCurrentGPUMetrics(gpu, &metrics);
            Skip.If(metricsResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU metrics not available on this hardware/driver.");
            Assert.Equal(ADLX_RESULT.ADLX_OK, metricsResult);
            using var metricsPtr = new ComPtr<IADLXGPUMetrics>(metrics);

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

            if (intakeTempSupported)
            {
                double intakeTemp = 0;
                AssertResultOrContinue(metrics->GPUIntakeTemperature(&intakeTemp));
            }
        }
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

    private static bool AssertResultOrContinue(ADLX_RESULT result)
    {
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            return false;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return true;
    }
}
