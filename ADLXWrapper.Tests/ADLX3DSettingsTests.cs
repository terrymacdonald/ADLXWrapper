using System;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Tests for 3D Settings services.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public unsafe class ADLX3DSettingsTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly string _skipReason = string.Empty;
        private readonly ADLX3DSettingsServicesHelper? _settingsHelper;
        private readonly IADLXGPU* _gpu;

        public ADLX3DSettingsTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                var system = _system.GetSystemServicesNative();
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
                if (!_system.TryGet3DSettingsServicesNative(out var settingsServices))
                {
                    _skipReason = "3D settings services not supported by this ADLX system.";
                    return;
                }

                _settingsHelper = new ADLX3DSettingsServicesHelper(settingsServices);
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            if (_gpu != null) ((IUnknown*)_gpu)->Release();
            _settingsHelper?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetAll3DSettings()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _settingsHelper == null, _skipReason);
            if (_settingsHelper.TryGetAll3DSettings(_gpu, out var settings))
            {
                _output.WriteLine($"Successfully retrieved 3D settings. Anti-Lag supported: {settings.AntiLag?.IsSupported ?? false}");
            }
            else
            {
                Skip.If(true, "3D settings not supported on this system.");
            }
        }

        [SkippableFact]
        public void All3DSettings_ShouldExposeCommonFields()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _settingsHelper == null, _skipReason);

            if (!_settingsHelper.TryGetAll3DSettings(_gpu, out var settings))
            {
                Skip.If(true, "3D settings not supported on this system.");
                return;
            }

            if (settings.FrameRateTargetControl.HasValue)
            {
                var frtc = settings.FrameRateTargetControl.Value;
                _output.WriteLine($"FRTC supported={frtc.IsSupported}, enabled={frtc.IsEnabled}, fps={frtc.Fps}");
                Assert.True(frtc.FpsRange.maxValue >= frtc.FpsRange.minValue);
            }

            if (settings.Tessellation.HasValue)
            {
                var tess = settings.Tessellation.Value;
                _output.WriteLine($"Tessellation supported={tess.IsSupported}, mode={tess.Mode}, level={tess.Level}");
            }

            if (settings.ImageSharpening.HasValue)
            {
                var ris = settings.ImageSharpening.Value;
                _output.WriteLine($"RIS supported={ris.IsSupported}, enabled={ris.IsEnabled}, sharpness={ris.Sharpness}");
            }

            Assert.True(settings.AntiLag.HasValue || settings.Boost.HasValue || settings.ImageSharpening.HasValue || settings.EnhancedSync.HasValue || settings.Tessellation.HasValue);
        }
    }
}
