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
    public unsafe class PowerTuningServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXGPU* _gpu;
        private readonly IADLXPowerTuningServices* _powerServices;
        private readonly IADLXGPUTuningServices* _tuningServices;

        public PowerTuningServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                var system = _api.GetSystemServices();
                _powerServices = ADLXPowerTuningHelpers.GetPowerTuningServices(system);
                _tuningServices = _api.GetGPUTuningServices();

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
            if (_powerServices != null) ((IUnknown*)_powerServices)->Release();
            if (_tuningServices != null) ((IUnknown*)_tuningServices)->Release();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetPowerTuningInfo()
        {
            Skip.If(_api == null || _gpu == null || _powerServices == null || _tuningServices == null, _skipReason);

            var ssm = ADLXPowerTuningHelpers.GetSmartShiftMax(_powerServices);
            Assert.NotNull(ssm);
            _output.WriteLine($"SmartShift Max supported: {ssm.IsSupported}");

            var sse = ADLXPowerTuningHelpers.GetSmartShiftEco(_powerServices);
            Assert.NotNull(sse);
            _output.WriteLine($"SmartShift Eco supported: {sse.IsSupported}");

            var manual = ADLXPowerTuningHelpers.GetManualPowerTuning(_tuningServices, _gpu);
            Assert.NotNull(manual);
            _output.WriteLine($"Manual Power Tuning supported: {manual.PowerLimitSupported}");
        }
    }
}