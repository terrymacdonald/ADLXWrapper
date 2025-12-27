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
                if (!_system.TryGetGPUTuningServicesNative(out var tuningServices))
                {
                    _skipReason = "GPU tuning services not supported by this ADLX system.";
                    return;
                }
                _tuningHelper = new ADLXGPUTuningServicesHelper(tuningServices);

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

            if (_tuningHelper.TryGetManualFanTuning(_gpu, out var fanInfo))
            {
                _output.WriteLine($"Manual Fan Tuning Supported: {fanInfo.IsSupported}");
            }
            else
            {
                Skip.If(true, "Manual Fan Tuning not supported.");
            }

            if (_tuningHelper.TryGetManualVramTuning(_gpu, out var vramInfo))
            {
                _output.WriteLine($"Manual VRAM Tuning Supported: {vramInfo.IsSupported}");
            }
            else
            {
                Skip.If(true, "Manual VRAM Tuning not supported.");
            }

            if (_tuningHelper.TryGetPresetTuning(_gpu, out var presetInfo))
            {
                _output.WriteLine($"Preset Tuning Supported: {presetInfo.IsSupported}");
            }
            else
            {
                Skip.If(true, "Preset Tuning not supported.");
            }
        }

        [SkippableFact]
        public void ManualTuningSnapshots_ShouldExposeDetails()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _tuningHelper == null, _skipReason);

            if (_tuningHelper.TryGetManualFanTuning(_gpu, out var fanInfo) && fanInfo.IsSupported && fanInfo.FanPoints.Count > 0)
            {
                var point = fanInfo.FanPoints[0];
                _output.WriteLine($"Fan point: {point.FanSpeed} RPM at {point.Temperature}C (ZeroRPM supported={fanInfo.IsZeroRPMSupported})");
                Assert.True(point.FanSpeed >= 0);
                Assert.True(point.Temperature >= 0);
            }

            if (_tuningHelper.TryGetManualVramTuning(_gpu, out var vramInfo) && vramInfo.IsSupported && vramInfo.States.Count > 0)
            {
                var state = vramInfo.States[0];
                _output.WriteLine($"VRAM state: {state.Frequency} MHz @ {state.Voltage} mV");
                Assert.True(state.Frequency >= 0);
                Assert.True(state.Voltage >= 0);
            }

            if (_tuningHelper.TryGetPresetTuning(_gpu, out var presetInfo) && presetInfo.IsSupported && presetInfo.SupportedPresets.Count > 0)
            {
                _output.WriteLine($"Supported presets: {string.Join(", ", presetInfo.SupportedPresets)}; current={presetInfo.CurrentPreset}");
            }
        }
    }
}
