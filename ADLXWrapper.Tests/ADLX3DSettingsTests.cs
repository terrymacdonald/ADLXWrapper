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
        private readonly IADLX3DSettingsServices* _settingsServices;

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
                _settingsHelper = new ADLX3DSettingsServicesHelper(_system.Get3DSettingsServicesNative());
                _settingsServices = _settingsHelper.Get3DSettingsServicesNative();
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
            Skip.If(_api == null || _system == null || _gpu == null || _settingsServices == null, _skipReason);
            var settings = ADLX3DSettingsHelpers.GetAll3DSettings(_settingsServices, _gpu);
            _output.WriteLine($"Successfully retrieved 3D settings. Anti-Lag supported: {settings.AntiLag?.IsSupported ?? false}");
        }
    }
}
