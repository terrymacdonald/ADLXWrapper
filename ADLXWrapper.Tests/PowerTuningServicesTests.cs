using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Tests for Power Tuning services.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class PowerTuningServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly ADLXSystemServices? _system;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle _gpu;
        private readonly AdlxPowerTuning? _power;
        private readonly AdlxGpuTuning? _gpuTuning;

        public PowerTuningServicesTests(ITestOutputHelper output)
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

                _power = _system.GetPowerTuningServices();
                _gpuTuning = _system.GetGpuTuningServices(_gpu);
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _gpuTuning?.Dispose();
            _power?.Dispose();
            _gpu.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetPowerTuningInfo()
        {
            Skip.If(_api == null || _power == null || _gpuTuning == null || _gpu.IsInvalid, _skipReason);

            var profile = _power.GetProfile();
            _output.WriteLine($"SmartShift Max supported: {profile.SmartShiftMax?.IsSupported}");
            _output.WriteLine($"SmartShift Eco supported: {profile.SmartShiftEco?.IsSupported}");

            var manual = _gpuTuning.GetManualPowerTuning();
            _output.WriteLine($"Manual Power Tuning supported: {manual.PowerLimitSupported}");

            Assert.NotNull(profile.SmartShiftMax);
        }
    }
}