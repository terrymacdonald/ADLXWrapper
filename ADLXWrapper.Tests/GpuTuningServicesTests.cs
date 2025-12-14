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
    public class GpuTuningServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly ADLXSystemServices? _system;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle _gpu;
        private readonly AdlxGpuTuning? _tuning;

        public GpuTuningServicesTests(ITestOutputHelper output)
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

                _tuning = _system.GetGpuTuningServices(_gpu);
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _tuning?.Dispose();
            _gpu.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetTuningInfo()
        {
            Skip.If(_api == null || _tuning == null || _gpu.IsInvalid, _skipReason);

            var profile = _tuning.GetProfile();
            _output.WriteLine($"Manual GFX Tuning Supported: {profile.Capabilities.ManualGFXTuningSupported}");
            _output.WriteLine($"Manual VRAM Tuning Supported: {profile.ManualVram?.IsSupported}");
            _output.WriteLine($"Preset Tuning Supported: {profile.PresetTuning?.IsSupported}");
        }
    }
}