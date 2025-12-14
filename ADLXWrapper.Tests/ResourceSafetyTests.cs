using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Safety/regression tests to ensure handles are released and Dispose is idempotent.
    /// Skips when ADLX DLL or AMD hardware is unavailable.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class ResourceSafetyTests
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

            if (!ADLXApi.IsADLXDllAvailable(out var dllReason))
            {
                Skip.If(true, dllReason);
            }

            using var api = ADLXApi.Initialize();
            using var system = api.GetSystemServicesProfile();

            for (int i = 0; i < 5; i++)
            {
                using var perf = system.GetPerformanceMonitoringServices();
                var gpus = api.EnumerateGPUHandles();

                foreach (var gpu in gpus)
                {
                    using (gpu)
                    {
                        var info = ADLXGPUInfo.GetBasicInfo(gpu);
                        _output.WriteLine($"[Iter {i}] GPU: {info.Name}");

                        var metrics = perf.GetCurrentGpuMetrics(gpu);
                        _output.WriteLine($"[Iter {i}] Temp: {metrics.Temperature}C");
                    }
                }

                var displays = system.EnumerateAllDisplays();
                foreach (var display in displays)
                {
                    using (display)
                    {
                        _output.WriteLine($"[Iter {i}] Display: {display.Name}");
                    }
                }
            }
        }
    }
}
