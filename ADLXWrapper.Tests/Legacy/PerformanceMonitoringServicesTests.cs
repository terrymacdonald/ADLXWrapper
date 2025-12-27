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
    public unsafe class PerformanceMonitoringServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly string _skipReason = string.Empty;
        private readonly ADLXPerformanceMonitoringServicesHelper? _perfHelper;
        private readonly IADLXGPU* _gpu;

        public PerformanceMonitoringServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                var system = _system.GetSystemServicesNative();
                if (!_system.TryGetPerformanceMonitoringServicesNative(out var perfServices))
                {
                    _skipReason = "Performance monitoring services not supported by this ADLX system.";
                    return;
                }
                _perfHelper = new ADLXPerformanceMonitoringServicesHelper(perfServices);

                IADLXGPUList* gpuList = null;
                var result = system->GetGPUs(&gpuList);
                if (result != ADLX_RESULT.ADLX_OK || gpuList == null || gpuList->Size() == 0)
                {
                    _skipReason = "No AMD GPUs found.";
                    return;
                }
                IADLXGPU* pGpu = null;
                gpuList->At(0, &pGpu);
                _gpu = pGpu;
                ((IADLXInterface*)gpuList)->Release();
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            if (_gpu != null) ((IUnknown*)_gpu)->Release();
            _perfHelper?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetPerformanceInfo()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _perfHelper == null, _skipReason);

            var settings = _perfHelper.GetPerformanceMonitoringSettings();
            Assert.True(settings.SamplingIntervalMs > 0);
            _output.WriteLine($"Sampling Interval: {settings.SamplingIntervalMs}ms");

            if (_perfHelper!.TryGetCurrentGpuMetrics(_gpu, out var metrics))
            {
                // Metrics is a struct; just ensure we read a plausible value
                Assert.True(metrics.Temperature >= 0);
                _output.WriteLine($"Current GPU Temp: {metrics.Temperature}C");
            }
            else
            {
                Skip.If(true, "GPU metrics not supported on this system.");
            }
        }

        [SkippableFact]
        public void CanEnumerateMetricsHistory()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _perfHelper == null, _skipReason);

            PerformanceMonitoringSettingsInfo settings;
            try
            {
                settings = _perfHelper.GetPerformanceMonitoringSettings();
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Performance monitoring not supported.");
                return;
            }

            if (settings.MaxHistorySizeSec <= 0)
            {
                Skip.If(true, "Performance metrics history unavailable (size <= 0).");
            }

            var stopMs = Math.Min(settings.MaxHistorySizeSec * 1000, settings.SamplingIntervalMs * 20);
            if (stopMs <= 0) stopMs = settings.SamplingIntervalMs * 2;

            if (_perfHelper.TryEnumerateGpuMetricsHistory(_gpu, 0, stopMs, out var gpuHistory))
            {
                foreach (var snap in gpuHistory)
                {
                    _output.WriteLine($"History sample: temp={snap.Temperature}C, usage={snap.Usage}%, clock={snap.ClockSpeed}MHz");
                    Assert.True(snap.Temperature >= 0);
                    break;
                }
            }
            else
            {
                Skip.If(true, "GPU metrics history not supported.");
                return;
            }

            if (_perfHelper.TryEnumerateAllMetricsHistory(0, stopMs, out var allHistory))
            {
                foreach (var snap in allHistory)
                {
                    _output.WriteLine($"All metrics snapshot at {snap.TimestampMs}ms; FPS={snap.FPS?.ToString() ?? "n/a"}");
                    break;
                }
            }
            else
            {
                Skip.If(true, "Combined metrics history not supported.");
            }
        }
    }
}
