using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Tests for Performance Monitoring services.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class PerformanceMonitoringServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly ADLXSystemServices? _system;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle _gpu;
        private readonly AdlxPerformanceMonitor? _perf;

        public PerformanceMonitoringServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                _system = _api.GetSystemServicesProfile();

                var gpus = _api.EnumerateGPUHandles();
                if (gpus.Length == 0)
                {
                    _skipReason = "No AMD GPUs found.";
                    return;
                }

                _gpu = gpus[0];
                for (int i = 1; i < gpus.Length; i++)
                {
                    gpus[i].Dispose();
                }

                _perf = _system.GetPerformanceMonitoringServices();
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _perf?.Dispose();
            _gpu.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetPerformanceInfo()
        {
            Skip.If(_api == null || _perf == null || _gpu.IsInvalid, _skipReason);

            var profile = _perf.GetProfile();
            _output.WriteLine($"Sampling Interval: {profile.SamplingIntervalMs}ms");
            Assert.True(profile.SamplingIntervalMs >= 0);

            var metrics = _perf.GetCurrentGpuMetrics(_gpu);
            Assert.True(metrics.Temperature >= 0);
            _output.WriteLine($"Current GPU Temp: {metrics.Temperature}C");
        }
    }
}