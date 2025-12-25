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
                if (!_system.TryGetMultimediaServicesNative(out var multimediaServices))
                {
                    _skipReason = "Multimedia services not supported by this ADLX system.";
                    return;
                }

                _multimediaHelper = new ADLXMultimediaServicesHelper(multimediaServices);
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

            if (_multimediaHelper!.TryGetVideoSuperResolution(_gpu, out var vsr))
            {
                _output.WriteLine($"Video Super Resolution supported: {vsr.IsSupported}");
            }
            else
            {
                Skip.If(true, "Video Super Resolution not supported on this system.");
            }

            if (_multimediaHelper.TryGetVideoUpscale(_gpu, out var upscale))
            {
                _output.WriteLine($"Video Upscale supported: {upscale.IsSupported}");
            }
            else
            {
                Skip.If(true, "Video Upscale not supported on this system.");
            }
        }

        [SkippableFact]
        public void CanRegisterMultimediaListener()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _multimediaServices == null, _skipReason);

            if (!_multimediaHelper!.TryGetMultimediaChangedHandlingNative(out var handling))
            {
                Skip.If(true, "Multimedia change handling not supported.");
                return;
            }

            using var listener = _multimediaHelper.AddMultimediaEventListener(pEvent =>
            {
                if (pEvent == IntPtr.Zero) return true;
                var evt = (IADLXMultimediaChangedEvent*)pEvent;
                var origin = evt->GetOrigin();
                var ups = evt->IsVideoUpscaleChanged();
                var vsr = evt->IsVideoSuperResolutionChanged();
                _output.WriteLine($"Multimedia event: origin={origin}, upscaleChanged={ups}, vsrChanged={vsr}");
                return true; // continue receiving events
            });

            _multimediaHelper.RemoveMultimediaEventListener(listener);
        }
    }
}
