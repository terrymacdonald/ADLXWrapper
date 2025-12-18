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
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXGPU* _gpu;
        private readonly IADLXMultimediaServices* _multimediaServices;

        public MultimediaServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                var system = _system.GetSystemServicesNative();
                _multimediaServices = ADLXMultimediaHelpers.GetMultimediaServices(system);

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
            if (_multimediaServices != null) ((IUnknown*)_multimediaServices)->Release();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetMultimediaInfo()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _multimediaServices == null, _skipReason);

            var vsr = ADLXMultimediaHelpers.GetVideoSuperResolution(_multimediaServices, _gpu);
            _output.WriteLine($"Video Super Resolution supported: {vsr.IsSupported}");

            var upscale = ADLXMultimediaHelpers.GetVideoUpscale(_multimediaServices, _gpu);
            _output.WriteLine($"Video Upscale supported: {upscale.IsSupported}");
        }
    }
}