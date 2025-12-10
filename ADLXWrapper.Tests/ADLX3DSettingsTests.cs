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
        private readonly ADLXApi? _api;
        private readonly string _skipReason;
        private readonly IADLXGPU* _gpu;
        private readonly IADLX3DSettingsServices* _settingsServices;

        public ADLX3DSettingsTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                var system = _api.GetSystemServices();
                system->GetGPUs(out var gpuList);
                if (gpuList->Size() == 0)
                {
                    _skipReason = "No AMD GPUs found.";
                    return;
                }
                gpuList->At(0, out _gpu);
                _settingsServices = ADLX3DSettingsHelpers.Get3DSettingsServices(system);
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            if (_gpu != null) ((IUnknown*)_gpu)->Release();
            if (_settingsServices != null) ((IUnknown*)_settingsServices)->Release();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetAll3DSettings()
        {
            Skip.If(_api == null || _gpu == null || _settingsServices == null, _skipReason);
            var settings = ADLX3DSettingsHelpers.GetAll3DSettings(_settingsServices, _gpu);
            Assert.NotNull(settings);
            _output.WriteLine($"Successfully retrieved 3D settings. Anti-Lag supported: {settings.AntiLag?.IsSupported ?? false}");
        }
    }
}