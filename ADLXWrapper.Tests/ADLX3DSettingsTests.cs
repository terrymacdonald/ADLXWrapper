using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Tests for 3D Settings services.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class ADLX3DSettingsTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly ADLXSystemServices? _system;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle _gpu;
        private readonly Adlx3DSettings? _settings;

        public ADLX3DSettingsTests(ITestOutputHelper output)
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

                _settings = _system.Get3DSettingsServices(_gpu);
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _settings?.Dispose();
            _gpu.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetAll3DSettings()
        {
            Skip.If(_api == null || _settings == null || _gpu.IsInvalid, _skipReason);
            var profile = _settings.GetProfile();
            _output.WriteLine($"Successfully retrieved 3D settings. Anti-Lag supported: {profile.AntiLag?.IsSupported ?? false}");
            Assert.NotNull(profile);
        }
    }
}