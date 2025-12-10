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
    public unsafe class MultimediaServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXGPU* _gpu;
        private readonly IADLXMultimediaServices* _multimediaServices;

        public MultimediaServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                var system = _api.GetSystemServices();
                _multimediaServices = ADLXMultimediaHelpers.GetMultimediaServices(system);

                system->GetGPUs(out var gpuList);
                if (gpuList->Size() == 0)
                {
                    _skipReason = "No AMD GPUs found.";
                    return;
                }
                gpuList->At(0, out _gpu);
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            if (_gpu != null) ((IUnknown*)_gpu)->Release();
            if (_multimediaServices != null) ((IUnknown*)_multimediaServices)->Release();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetMultimediaInfo()
        {
            Skip.If(_api == null || _gpu == null || _multimediaServices == null, _skipReason);

            var vsr = ADLXMultimediaHelpers.GetVideoSuperResolution(_multimediaServices, _gpu);
            Assert.NotNull(vsr);
            _output.WriteLine($"Video Super Resolution supported: {vsr.IsSupported}");

            var upscale = ADLXMultimediaHelpers.GetVideoUpscale(_multimediaServices, _gpu);
            Assert.NotNull(upscale);
            _output.WriteLine($"Video Upscale supported: {upscale.IsSupported}");
        }
    }
}