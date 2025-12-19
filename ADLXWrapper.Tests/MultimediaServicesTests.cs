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
        private readonly ADLXMultimediaServicesHelper? _multimediaHelper;
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
                _multimediaHelper = new ADLXMultimediaServicesHelper(_system.GetMultimediaServicesNative());
                _multimediaServices = _multimediaHelper.GetMultimediaServicesNative();

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
            _multimediaHelper?.Dispose();
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

        [SkippableFact]
        public void CanRegisterMultimediaListener()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _multimediaServices == null, _skipReason);

            IADLXMultimediaChangedHandling* handling;
            try
            {
                handling = _multimediaHelper!.GetMultimediaChangedHandlingNative();
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Multimedia change handling not supported.");
                return;
            }

            using var listener = MultimediaEventListenerHandle.Create(pEvent =>
            {
                if (pEvent == IntPtr.Zero) return true;
                var evt = (IADLXMultimediaChangedEvent*)pEvent;
                var origin = evt->GetOrigin();
                var ups = evt->IsVideoUpscaleChanged();
                var vsr = evt->IsVideoSuperResolutionChanged();
                _output.WriteLine($"Multimedia event: origin={origin}, upscaleChanged={ups}, vsrChanged={vsr}");
                return true; // continue receiving events
            });

            ADLXMultimediaHelpers.AddMultimediaEventListener(handling, listener);
            ADLXMultimediaHelpers.RemoveMultimediaEventListener(handling, listener);
        }
    }
}