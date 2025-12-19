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
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly string _skipReason = string.Empty;
        private readonly ADLXPowerTuningServicesHelper? _powerHelper;
        private readonly ADLXGPUTuningServicesHelper? _tuningHelper;
        private readonly IADLXGPU* _gpu;
        private readonly IADLXPowerTuningServices* _powerServices;
        private readonly IADLXGPUTuningServices* _tuningServices;

        public PowerTuningServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                var system = _system.GetSystemServicesNative();
                _powerHelper = new ADLXPowerTuningServicesHelper(_system.GetPowerTuningServicesNative());
                _powerServices = _powerHelper.GetPowerTuningServicesNative();

                _tuningHelper = new ADLXGPUTuningServicesHelper(_system.GetGPUTuningServicesNative());
                _tuningServices = _tuningHelper.GetGPUTuningServicesNative();

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
            _powerHelper?.Dispose();
            _tuningHelper?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetPowerTuningInfo()
        {
            Skip.If(_api == null || _system == null || _gpu == null || _powerServices == null || _tuningServices == null, _skipReason);

            var ssm = ADLXPowerTuningHelpers.GetSmartShiftMax(_powerServices);
            _output.WriteLine($"SmartShift Max supported: {ssm.IsSupported}");

            var sse = ADLXPowerTuningHelpers.GetSmartShiftEco(_powerServices);
            _output.WriteLine($"SmartShift Eco supported: {sse.IsSupported}");

            var manual = ADLXPowerTuningHelpers.GetManualPowerTuning(_tuningServices, _gpu);
            _output.WriteLine($"Manual Power Tuning supported: {manual.PowerLimitSupported}");
        }
    }
}