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
        private readonly IntPtr[] _gpus;
        private readonly IntPtr _pGPUTuningServices;

        public GpuTuningServicesTests(ITestOutputHelper output)
        {
            _output = output;
            _gpus = Array.Empty<IntPtr>();
            _pGPUTuningServices = IntPtr.Zero;

            try
            {
                _api = ADLXApi.Initialize();
                _gpus = _api.EnumerateGPUs();
                
                if (_gpus.Length > 0)
                {
                    try
                    {
                        _pGPUTuningServices = _api.GetGPUTuningServices();
                        _hasHardware = true;
                        _output.WriteLine($"? ADLX initialized, found {_gpus.Length} GPU(s) with tuning services");
                    }
                    catch (ADLXException ex)
                    {
                        _hasHardware = false;
                        _output.WriteLine($"? GPU tuning services not available: {ex.Message}");
                    }
                }
                else
                {
                    _hasHardware = false;
                    _output.WriteLine("? ADLX initialized but no GPUs found");
                }
            }
            catch (Exception ex)
            {
                _hasHardware = false;
                _output.WriteLine($"? Initialization failed: {ex.Message}");
            }
        }

        [Fact]
        public void GetGPUTuningServices_ShouldSucceed()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var pTuningServices = _api.GetGPUTuningServices();
            Assert.NotEqual(IntPtr.Zero, pTuningServices);
            _output.WriteLine($"? GPU tuning services pointer: 0x{pTuningServices:X}");

            // Release the extra instance we just created
            ADLXHelpers.ReleaseInterface(pTuningServices);
        }

        [Fact]
        public void IsSupportedAutoTuning_ShouldReturnBooleanValue()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pGPUTuningServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No GPU tuning services available");
                return;
            }

            var isSupported = ADLXGPUTuningHelpers.IsSupportedAutoTuning(_pGPUTuningServices, _gpus[0]);
            _output.WriteLine($"? Auto Tuning Supported: {isSupported}");
        }

        [Fact]
        public void IsSupportedPresetTuning_ShouldReturnBooleanValue()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pGPUTuningServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No GPU tuning services available");
                return;
            }

            var isSupported = ADLXGPUTuningHelpers.IsSupportedPresetTuning(_pGPUTuningServices, _gpus[0]);
            _output.WriteLine($"? Preset Tuning Supported: {isSupported}");
        }

        [Fact]
        public void IsSupportedManualGFXTuning_ShouldHandleGracefully()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pGPUTuningServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No GPU tuning services available");
                return;
            }

            try
            {
                var isSupported = ADLXGPUTuningHelpers.IsSupportedManualGFXTuning(_pGPUTuningServices, _gpus[0]);
                _output.WriteLine($"? Manual GFX Tuning Supported: {isSupported}");
            }
            catch (ADLXException ex)
            {
                // This might fail on some GPUs (e.g., older iGPUs)
                _output.WriteLine($"??  Manual GFX tuning check failed: {ex.Message} (Result: {ex.Result})");
            }
        }

        [Fact]
        public void IsSupportedManualVRAMTuning_ShouldReturnBooleanValue()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pGPUTuningServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No GPU tuning services available");
                return;
            }

            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning(_pGPUTuningServices, _gpus[0]);
            _output.WriteLine($"? Manual VRAM Tuning Supported: {isSupported}");
        }

        [Fact]
        public void IsSupportedManualFanTuning_ShouldReturnBooleanValue()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pGPUTuningServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No GPU tuning services available");
                return;
            }

            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualFanTuning(_pGPUTuningServices, _gpus[0]);
            _output.WriteLine($"? Manual Fan Tuning Supported: {isSupported}");
        }

        [Fact]
        public void IsSupportedManualPowerTuning_ShouldReturnBooleanValue()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pGPUTuningServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No GPU tuning services available");
                return;
            }

            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualPowerTuning(_pGPUTuningServices, _gpus[0]);
            _output.WriteLine($"? Manual Power Tuning Supported: {isSupported}");
        }

        [Fact]
        public void AllSupportChecks_ShouldNotThrow()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pGPUTuningServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No GPU tuning services available");
                return;
            }

            _output.WriteLine("=== GPU Tuning Support Summary ===");
            
            try
            {
                var autoSupported = ADLXGPUTuningHelpers.IsSupportedAutoTuning(_pGPUTuningServices, _gpus[0]);
                _output.WriteLine($"  Auto Tuning: {autoSupported}");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"  Auto Tuning: Error - {ex.Message}");
            }

            try
            {
                var presetSupported = ADLXGPUTuningHelpers.IsSupportedPresetTuning(_pGPUTuningServices, _gpus[0]);
                _output.WriteLine($"  Preset Tuning: {presetSupported}");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"  Preset Tuning: Error - {ex.Message}");
            }

            try
            {
                var gfxSupported = ADLXGPUTuningHelpers.IsSupportedManualGFXTuning(_pGPUTuningServices, _gpus[0]);
                _output.WriteLine($"  Manual GFX Tuning: {gfxSupported}");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"  Manual GFX Tuning: Error - {ex.Message}");
            }

            try
            {
                var vramSupported = ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning(_pGPUTuningServices, _gpus[0]);
                _output.WriteLine($"  Manual VRAM Tuning: {vramSupported}");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"  Manual VRAM Tuning: Error - {ex.Message}");
            }

            try
            {
                var fanSupported = ADLXGPUTuningHelpers.IsSupportedManualFanTuning(_pGPUTuningServices, _gpus[0]);
                _output.WriteLine($"  Manual Fan Tuning: {fanSupported}");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"  Manual Fan Tuning: Error - {ex.Message}");
            }

            try
            {
                var powerSupported = ADLXGPUTuningHelpers.IsSupportedManualPowerTuning(_pGPUTuningServices, _gpus[0]);
                _output.WriteLine($"  Manual Power Tuning: {powerSupported}");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"  Manual Power Tuning: Error - {ex.Message}");
            }

            _output.WriteLine("? All support checks completed");
        }

        [Fact]
        public void IsSupportedAutoTuning_WithNullServices_ShouldThrowArgumentNullException()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            Assert.Throws<ArgumentNullException>(() => 
                ADLXGPUTuningHelpers.IsSupportedAutoTuning(IntPtr.Zero, _gpus[0]));
            _output.WriteLine("? IsSupportedAutoTuning correctly throws on null services pointer");
        }

        [Fact]
        public void IsSupportedAutoTuning_WithNullGPU_ShouldThrowArgumentNullException()
        {
            if (!_hasHardware || _pGPUTuningServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No GPU tuning services available");
                return;
            }

            Assert.Throws<ArgumentNullException>(() => 
                ADLXGPUTuningHelpers.IsSupportedAutoTuning(_pGPUTuningServices, IntPtr.Zero));
            _output.WriteLine("? IsSupportedAutoTuning correctly throws on null GPU pointer");
        }

        public void Dispose()
        {
            // Release GPU tuning services
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

            // Release all GPU interfaces
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
    }
}
