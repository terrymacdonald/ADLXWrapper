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
                _perfHelper = new ADLXPerformanceMonitoringServicesHelper(_system.GetPerformanceMonitoringServicesNative());

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

            var metrics = _perfHelper!.GetCurrentGpuMetrics(_gpu);
            // Metrics is a struct; just ensure we read a plausible value
            Assert.True(metrics.Temperature >= 0);
            _output.WriteLine($"Current GPU Temp: {metrics.Temperature}C");
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

            try
            {
                var gpuHistory = _perfHelper.EnumerateGpuMetricsHistory(_gpu, 0, stopMs);
                foreach (var snap in gpuHistory)
                {
                    _output.WriteLine($"History sample: temp={snap.Temperature}C, usage={snap.Usage}%, clock={snap.ClockSpeed}MHz");
                    Assert.True(snap.Temperature >= 0);
                    break;
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "GPU metrics history not supported.");
                return;
            }

            try
            {
                var allHistory = _perfHelper.EnumerateAllMetricsHistory(0, stopMs);
                foreach (var snap in allHistory)
                {
                    _output.WriteLine($"All metrics snapshot at {snap.TimestampMs}ms; FPS={snap.FPS?.ToString() ?? "n/a"}");
                    break;
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Combined metrics history not supported.");
            }
        }
    }
}
