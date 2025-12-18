using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Tests for GPU Tuning services.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public unsafe class GpuTuningServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXGPU* _gpu;
        private readonly IADLXGPUTuningServices* _tuningServices;

        public GpuTuningServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                var system = _system.GetSystemServicesNative();
                _tuningServices = _system.GetGPUTuningServicesNative();

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
            if (_tuningServices != null) ((IUnknown*)_tuningServices)->Release();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetTuningInfo()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _tuningServices == null, _skipReason);

            var caps = new GpuTuningCapabilitiesInfo(_tuningServices, _gpu);
            _output.WriteLine($"Manual GFX Tuning Supported: {caps.ManualGFXTuningSupported}");

            var fanInfo = ADLXGPUTuningHelpers.GetManualFanTuning(_tuningServices, _gpu);
            _output.WriteLine($"Manual Fan Tuning Supported: {fanInfo.IsSupported}");

            var vramInfo = ADLXGPUTuningHelpers.GetManualVramTuning(_tuningServices, _gpu);
            _output.WriteLine($"Manual VRAM Tuning Supported: {vramInfo.IsSupported}");

            var presetInfo = ADLXGPUTuningHelpers.GetPresetTuning(_tuningServices, _gpu);
            _output.WriteLine($"Preset Tuning Supported: {presetInfo.IsSupported}");
        }
    }
}