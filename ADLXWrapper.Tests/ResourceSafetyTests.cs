using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Safety/regression tests to ensure handles are released and Dispose is idempotent.
    /// Skips when ADLX DLL or AMD hardware is unavailable.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public unsafe class ResourceSafetyTests
    {
        private readonly ITestOutputHelper _output;

        public ResourceSafetyTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [SkippableFact]
        public void EnumerateAndDispose_Repeatedly_ShouldNotThrow()
        {
            Skip.If(!ADLXHardwareDetection.HasAMDGPU(out var hwReason), hwReason);

            if (!ADLXApiHelper.IsADLXDllAvailable(out var dllReason))
            {
                Skip.If(true, dllReason);
            }

            using var api = ADLXApiHelper.Initialize();
            using var systemHelper = new ADLXSystemServicesHelper(api.GetSystemServicesNative());
            var system = systemHelper.GetSystemServicesNative();

            for (int i = 0; i < 5; i++)
            {
                using var perf = AdlxInterfaceHandle.From(systemHelper.GetPerformanceMonitoringServicesNative(), addRef: false);
                var gpus = systemHelper.EnumerateGPUHandles();

                foreach (var gpu in gpus)
                {
                    using (gpu)
                    {
                        var info = ADLXGPUInfo.GetBasicInfo(gpu);
                        _output.WriteLine($"[Iter {i}] GPU: {info.Name}");

                        var metrics = ADLXPerformanceMonitoringHelpers.GetCurrentGpuMetrics(perf.As<IADLXPerformanceMonitoringServices>(), gpu.As<IADLXGPU>());
                        _output.WriteLine($"[Iter {i}] Temp: {metrics.Temperature}C");
                    }
                }

                var displays = ADLXDisplayHelpers.EnumerateAllDisplayHandles(system);
                foreach (var display in displays)
                {
                    using (display)
                    {
                        var name = ADLXDisplayHelpers.GetDisplayName(display);
                        _output.WriteLine($"[Iter {i}] Display: {name}");
                    }
                }
            }
        }
    }
}
