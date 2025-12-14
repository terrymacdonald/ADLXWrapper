using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Tests for Multimedia services (VSR, Upscale).
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class MultimediaServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly ADLXSystemServices? _system;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle _gpu;
        private readonly AdlxMultimedia? _multimedia;

        public MultimediaServicesTests(ITestOutputHelper output)
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

                _multimedia = _system.GetMultimediaServices(_gpu);
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _multimedia?.Dispose();
            _gpu.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetMultimediaInfo()
        {
            Skip.If(_api == null || _multimedia == null || _gpu.IsInvalid, _skipReason);

            var profile = _multimedia.GetProfile();
            _output.WriteLine($"Video Super Resolution supported: {profile.VideoSuperResolution?.IsSupported}");
            _output.WriteLine($"Video Upscale supported: {profile.VideoUpscale?.IsSupported}");

            Assert.NotNull(profile.VideoSuperResolution);
            Assert.NotNull(profile.VideoUpscale);
        }
    }
}