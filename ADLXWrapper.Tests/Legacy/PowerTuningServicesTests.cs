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
                if (!_system.TryGetPowerTuningServicesNative(out var powerServices))
                {
                    _skipReason = "Power tuning services not supported by this ADLX system.";
                    return;
                }

                if (!_system.TryGetGPUTuningServicesNative(out var tuningServices))
                {
                    _skipReason = "GPU tuning services not supported by this ADLX system.";
                    return;
                }

                _powerHelper = new ADLXPowerTuningServicesHelper(powerServices);
                _powerServices = _powerHelper.GetPowerTuningServicesNative();

                _tuningHelper = new ADLXGPUTuningServicesHelper(tuningServices);
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

            try
            {
                var ssm = _powerHelper!.GetSmartShiftMax();
                _output.WriteLine($"SmartShift Max supported: {ssm.IsSupported}");
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _output.WriteLine("SmartShift Max not supported.");
            }

            try
            {
                var sse = _powerHelper!.GetSmartShiftEco();
                _output.WriteLine($"SmartShift Eco supported: {sse.IsSupported}");
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _output.WriteLine("SmartShift Eco not supported.");
            }

            if (_powerHelper!.TryIsGPUConnectSupported(out _))
            {
                _output.WriteLine("GPUConnect supported by power tuning services.");
            }

            try
            {
                var manual = _powerHelper.GetManualPowerTuning(_tuningServices, _gpu);
                _output.WriteLine($"Manual Power Tuning supported: {manual.PowerLimitSupported}");
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _output.WriteLine("Manual Power Tuning not supported.");
            }
        }

        [SkippableFact]
        public void EnumerateGpuConnectHandles_ShouldProvideGpuIdentity()
        {
            Skip.If(_api == null || _system == null || _powerHelper == null, _skipReason);

            if (!_powerHelper.TryIsGPUConnectSupported(out var supported) || !supported)
            {
                Skip.If(true, "GPUConnect is not supported on this system.");
                return;
            }

            if (_powerHelper.TryEnumerateGPUConnectGpuHandles(out var handles) && handles.Length > 0)
            {
                using var first = handles[0];
                var info = _system!.GetGpuInfo(first.As<IADLXGPU>());
                _output.WriteLine($"GPUConnect GPU: {info.Name}, VRAM={info.TotalVRAM}MB, UniqueId={info.UniqueId}");
                Assert.False(string.IsNullOrWhiteSpace(info.Name));

                for (int i = 1; i < handles.Length; i++)
                {
                    handles[i].Dispose();
                }
            }
            else
            {
                Skip.If(true, "No GPUConnect GPUs returned.");
            }
        }
    }
}
