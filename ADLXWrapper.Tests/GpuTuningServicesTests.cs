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
        private readonly ADLXGPUTuningServicesHelper? _tuningHelper;
        private readonly IADLXGPU* _gpu;

        public GpuTuningServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                var system = _system.GetSystemServicesNative();
                _tuningHelper = new ADLXGPUTuningServicesHelper(_system.GetGPUTuningServicesNative());

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
            _tuningHelper?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetTuningInfo()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _tuningHelper == null, _skipReason);

            var caps = _tuningHelper.GetCapabilities(_gpu);
            _output.WriteLine($"Manual GFX Tuning Supported: {caps.ManualGFXTuningSupported}");

            var fanInfo = _tuningHelper.GetManualFanTuning(_gpu);
            _output.WriteLine($"Manual Fan Tuning Supported: {fanInfo.IsSupported}");

            var vramInfo = _tuningHelper.GetManualVramTuning(_gpu);
            _output.WriteLine($"Manual VRAM Tuning Supported: {vramInfo.IsSupported}");

            var presetInfo = _tuningHelper.GetPresetTuning(_gpu);
            _output.WriteLine($"Preset Tuning Supported: {presetInfo.IsSupported}");
        }

        [SkippableFact]
        public void ManualTuningSnapshots_ShouldExposeDetails()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _tuningHelper == null, _skipReason);

            var fanInfo = _tuningHelper.GetManualFanTuning(_gpu);
            if (fanInfo.IsSupported && fanInfo.FanPoints.Count > 0)
            {
                var point = fanInfo.FanPoints[0];
                _output.WriteLine($"Fan point: {point.FanSpeed} RPM at {point.Temperature}C (ZeroRPM supported={fanInfo.IsZeroRPMSupported})");
                Assert.True(point.FanSpeed >= 0);
                Assert.True(point.Temperature >= 0);
            }

            var vramInfo = _tuningHelper.GetManualVramTuning(_gpu);
            if (vramInfo.IsSupported && vramInfo.States.Count > 0)
            {
                var state = vramInfo.States[0];
                _output.WriteLine($"VRAM state: {state.Frequency} MHz @ {state.Voltage} mV");
                Assert.True(state.Frequency >= 0);
                Assert.True(state.Voltage >= 0);
            }

            var presetInfo = _tuningHelper.GetPresetTuning(_gpu);
            if (presetInfo.IsSupported && presetInfo.SupportedPresets.Count > 0)
            {
                _output.WriteLine($"Supported presets: {string.Join(", ", presetInfo.SupportedPresets)}; current={presetInfo.CurrentPreset}");
            }
        }
    }
}
