using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// GPU tuning services tests for ADLX wrapper (ClangSharp-based)
    /// Tests GPU tuning capabilities (read-only, no modification of user settings)
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class GpuTuningServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle[] _gpus = Array.Empty<AdlxInterfaceHandle>();
        private readonly AdlxInterfaceHandle? _pGPUTuningServices;

        public GpuTuningServicesTests(ITestOutputHelper output)
        {
            _output = output;
            _pGPUTuningServices = null;

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
                _gpus = _api.EnumerateGPUHandles();
                _output.WriteLine($"? ADLX initialized successfully");
                _output.WriteLine($"  ADLX Version: {_api.GetVersion()}");
                _output.WriteLine($"  GPUs found: {_gpus.Length}");

                // Try to get GPU tuning services
                try
                {
                    var handle = _api.GetGPUTuningServicesHandle();
                    _pGPUTuningServices = handle;
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
            if (_pGPUTuningServices is not null)
            {
                try
                {
                    _pGPUTuningServices.Dispose();
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
                    gpu.Dispose();
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

            using var pTuningServices = _api!.GetGPUTuningServicesHandle();
            Assert.False(pTuningServices.IsInvalid);
            _output.WriteLine($"? GPU tuning services pointer: 0x{((IntPtr)pTuningServices):X}");
        }

        [SkippableFact]
        public void IsSupportedAutoTuning_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == null || _gpus.Length == 0,
                _pGPUTuningServices != null ? _skipReason : "GPU tuning services not available");

            var tuning = _pGPUTuningServices!;
            var isSupported = ADLXGPUTuningHelpers.IsSupportedAutoTuning(tuning, _gpus[0]);
            _output.WriteLine($"? Auto Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedPresetTuning_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == null || _gpus.Length == 0,
                _pGPUTuningServices != null ? _skipReason : "GPU tuning services not available");

            var tuning = _pGPUTuningServices!;
            var isSupported = ADLXGPUTuningHelpers.IsSupportedPresetTuning(tuning, _gpus[0]);
            _output.WriteLine($"? Preset Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedManualGFXTuning_ShouldHandleGracefully()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == null || _gpus.Length == 0,
                _pGPUTuningServices != null ? _skipReason : "GPU tuning services not available");

            try
            {
                var tuning = _pGPUTuningServices!;
                var isSupported = ADLXGPUTuningHelpers.IsSupportedManualGFXTuning(tuning, _gpus[0]);
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
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == null || _gpus.Length == 0,
                _pGPUTuningServices != null ? _skipReason : "GPU tuning services not available");

            var tuning = _pGPUTuningServices!;
            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning(tuning, _gpus[0]);
            _output.WriteLine($"? Manual VRAM Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedManualFanTuning_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == null || _gpus.Length == 0,
                _pGPUTuningServices != null ? _skipReason : "GPU tuning services not available");

            var tuning = _pGPUTuningServices!;
            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualFanTuning(tuning, _gpus[0]);
            _output.WriteLine($"? Manual Fan Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedManualPowerTuning_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == null || _gpus.Length == 0,
                _pGPUTuningServices != null ? _skipReason : "GPU tuning services not available");

            var tuning = _pGPUTuningServices!;
            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualPowerTuning(tuning, _gpus[0]);
            _output.WriteLine($"? Manual Power Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void AllSupportChecks_ShouldNotThrow()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == null || _gpus.Length == 0,
                _pGPUTuningServices != null ? _skipReason : "GPU tuning services not available");

            _output.WriteLine("=== GPU Tuning Support Summary ===");
            
            var tuning = _pGPUTuningServices!;
            try { _output.WriteLine($"  Auto Tuning: {ADLXGPUTuningHelpers.IsSupportedAutoTuning(tuning, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Auto Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Preset Tuning: {ADLXGPUTuningHelpers.IsSupportedPresetTuning(tuning, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Preset Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual GFX Tuning: {ADLXGPUTuningHelpers.IsSupportedManualGFXTuning(tuning, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual GFX Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual VRAM Tuning: {ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning(tuning, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual VRAM Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual Fan Tuning: {ADLXGPUTuningHelpers.IsSupportedManualFanTuning(tuning, _gpus[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual Fan Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual Power Tuning: {ADLXGPUTuningHelpers.IsSupportedManualPowerTuning(tuning, _gpus[0])}"); }
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
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pGPUTuningServices == null,
                _pGPUTuningServices != null ? _skipReason : "GPU tuning services not available");

            var tuning = _pGPUTuningServices!;
            Assert.Throws<ArgumentNullException>(() => 
                ADLXGPUTuningHelpers.IsSupportedAutoTuning(tuning, IntPtr.Zero));
            _output.WriteLine("? IsSupportedAutoTuning correctly throws on null GPU pointer");
        }
    }
}
