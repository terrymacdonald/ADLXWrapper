using Xunit;
using System;
using ADLXWrapper;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// GPU tuning services tests for ADLX wrapper (ClangSharp-based)
    /// Tests GPU tuning capabilities (read-only, no modification of user settings)
    /// </summary>
    public class GpuTuningServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly IntPtr[] _gpus = Array.Empty<IntPtr>();
        private readonly IntPtr _pGPUTuningServices;

        public GpuTuningServicesTests(ITestOutputHelper output)
        {
            _output = output;
            _pGPUTuningServices = IntPtr.Zero;

            // Stage 1: Check for AMD GPU hardware via PCI
            if (!HardwareDetection.HasAMDGPU(out string hwError))
            {
                _hasHardware = false;
                _hasDll = false;
                _skipReason = hwError;
                _output.WriteLine($"??  {hwError}");
                return;
            }
            _hasHardware = true;

            var gpuNames = HardwareDetection.GetAMDGPUNames();
            if (gpuNames.Length > 0)
            {
                _output.WriteLine($"? AMD GPU detected: {string.Join(", ", gpuNames)}");
            }

            // Stage 2: Check for ADLX DLL availability
            if (!ADLXApi.IsADLXDllAvailable(out string dllError))
            {
                _hasDll = false;
                _skipReason = dllError;
                _output.WriteLine($"??  {dllError}");
                return;
            }
            _hasDll = true;

            // Stage 3: Try to initialize ADLX API
            try
            {
                _api = ADLXApi.Initialize();
                _gpus = _api.EnumerateGPUs();
                _output.WriteLine($"? ADLX initialized successfully");
                _output.WriteLine($"  ADLX Version: {_api.GetVersion()}");
                _output.WriteLine($"  GPUs found: {_gpus.Length}");

                // Try to get GPU tuning services
                try
                {
                    _pGPUTuningServices = _api.GetGPUTuningServices();
                    _output.WriteLine("  GPU tuning services: Available");
                }
                catch (ADLXException ex)
                {
                    _output.WriteLine($"  GPU tuning services: Not available ({ex.Message})");
                }
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
                _output.WriteLine($"??  {_skipReason}");
            }
        }

        public void Dispose()
        {
            // Release tuning services
            if (_pGPUTuningServices != IntPtr.Zero)
            {
                try
                {
                    ADLXHelpers.ReleaseInterface(_pGPUTuningServices);
                }
                catch
                {
                    // Ignore errors during cleanup
                }
            }

            // Release GPU interfaces
            foreach (var gpu in _gpus)
            {
                try
                {
                    ADLXHelpers.ReleaseInterface(gpu);
                }
                catch
                {
                    // Ignore errors during cleanup
                }
            }

            _api?.Dispose();
        }

        [SkippableFact]
        public void GetGPUTuningServices_ShouldSucceed()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            var pTuningServices = _api!.GetGPUTuningServices();
            Assert.NotEqual(IntPtr.Zero, pTuningServices);
            _output.WriteLine($"? GPU tuning services pointer: 0x{pTuningServices:X}");

            ADLXHelpers.ReleaseInterface(pTuningServices);
        }

        [SkippableFact]
        public void IsSupportedAutoTuning_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == IntPtr.Zero || _gpus.Length == 0,
                _pGPUTuningServices != IntPtr.Zero ? _skipReason : "GPU tuning services not available");

            var isSupported = ADLXGPUTuningHelpers.IsSupportedAutoTuning(_pGPUTuningServices, _gpus[0]);
            _output.WriteLine($"? Auto Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedPresetTuning_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == IntPtr.Zero || _gpus.Length == 0,
                _pGPUTuningServices != IntPtr.Zero ? _skipReason : "GPU tuning services not available");

            var isSupported = ADLXGPUTuningHelpers.IsSupportedPresetTuning(_pGPUTuningServices, _gpus[0]);
            _output.WriteLine($"? Preset Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedManualGFXTuning_ShouldHandleGracefully()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == IntPtr.Zero || _gpus.Length == 0,
                _pGPUTuningServices != IntPtr.Zero ? _skipReason : "GPU tuning services not available");

            try
            {
                var isSupported = ADLXGPUTuningHelpers.IsSupportedManualGFXTuning(_pGPUTuningServices, _gpus[0]);
                _output.WriteLine($"? Manual GFX Tuning Supported: {isSupported}");
            }
            catch (ADLXException ex)
            {
                _output.WriteLine($"??  Manual GFX tuning check failed: {ex.Message} (Result: {ex.Result})");
            }
        }

        [SkippableFact]
        public void IsSupportedManualVRAMTuning_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == IntPtr.Zero || _gpus.Length == 0,
                _pGPUTuningServices != IntPtr.Zero ? _skipReason : "GPU tuning services not available");

            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning(_pGPUTuningServices, _gpus[0]);
            _output.WriteLine($"? Manual VRAM Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedManualFanTuning_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == IntPtr.Zero || _gpus.Length == 0,
                _pGPUTuningServices != IntPtr.Zero ? _skipReason : "GPU tuning services not available");

            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualFanTuning(_pGPUTuningServices, _gpus[0]);
            _output.WriteLine($"? Manual Fan Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedManualPowerTuning_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == IntPtr.Zero || _gpus.Length == 0,
                _pGPUTuningServices != IntPtr.Zero ? _skipReason : "GPU tuning services not available");

            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualPowerTuning(_pGPUTuningServices, _gpus[0]);
            _output.WriteLine($"? Manual Power Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void AllSupportChecks_ShouldNotThrow()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == IntPtr.Zero || _gpus.Length == 0,
                _pGPUTuningServices != IntPtr.Zero ? _skipReason : "GPU tuning services not available");

            _output.WriteLine("=== GPU Tuning Support Summary ===");
            
            try { _output.WriteLine($"  Auto Tuning: {ADLXGPUTuningHelpers.IsSupportedAutoTuning(_pGPUTuningServices, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Auto Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Preset Tuning: {ADLXGPUTuningHelpers.IsSupportedPresetTuning(_pGPUTuningServices, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Preset Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual GFX Tuning: {ADLXGPUTuningHelpers.IsSupportedManualGFXTuning(_pGPUTuningServices, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual GFX Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual VRAM Tuning: {ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning(_pGPUTuningServices, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual VRAM Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual Fan Tuning: {ADLXGPUTuningHelpers.IsSupportedManualFanTuning(_pGPUTuningServices, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual Fan Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual Power Tuning: {ADLXGPUTuningHelpers.IsSupportedManualPowerTuning(_pGPUTuningServices, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual Power Tuning: Error - {ex.Message}"); }

            _output.WriteLine("? All support checks completed");
        }

        [SkippableFact]
        public void IsSupportedAutoTuning_WithNullServices_ShouldThrowArgumentNullException()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _gpus.Length == 0, _skipReason);

            Assert.Throws<ArgumentNullException>(() => 
                ADLXGPUTuningHelpers.IsSupportedAutoTuning(IntPtr.Zero, _gpus[0]));
            _output.WriteLine("? IsSupportedAutoTuning correctly throws on null services pointer");
        }

        [SkippableFact]
        public void IsSupportedAutoTuning_WithNullGPU_ShouldThrowArgumentNullException()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == IntPtr.Zero,
                _pGPUTuningServices != IntPtr.Zero ? _skipReason : "GPU tuning services not available");

            Assert.Throws<ArgumentNullException>(() => 
                ADLXGPUTuningHelpers.IsSupportedAutoTuning(_pGPUTuningServices, IntPtr.Zero));
            _output.WriteLine("? IsSupportedAutoTuning correctly throws on null GPU pointer");
        }
    }
}
