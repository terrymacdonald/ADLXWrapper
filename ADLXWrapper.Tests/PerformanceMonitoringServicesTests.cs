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
        private readonly ADLXApi? _api;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXGPU* _gpu;
        private readonly IADLXPerformanceMonitoringServices* _perfServices;

        public PerformanceMonitoringServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                var system = _api.GetSystemServices();
                _perfServices = ADLXPerformanceMonitoringHelpers.GetPerformanceMonitoringServices(system);

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
            if (_perfServices != null) ((IUnknown*)_perfServices)->Release();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetPerformanceInfo()
        {
            Skip.If(_api == null || _gpu == null || _perfServices == null, _skipReason);

            var settings = ADLXPerformanceMonitoringHelpers.GetPerformanceMonitoringSettings(_perfServices);
            Assert.True(settings.SamplingIntervalMs > 0);
            _output.WriteLine($"Sampling Interval: {settings.SamplingIntervalMs}ms");

            var metrics = ADLXPerformanceMonitoringHelpers.GetCurrentGpuMetrics(_perfServices, _gpu);
            // Metrics is a struct; just ensure we read a plausible value
            Assert.True(metrics.Temperature >= 0);
            _output.WriteLine($"Current GPU Temp: {metrics.Temperature}C");
        }
    }
}