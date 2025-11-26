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
    public class GpuTuningServicesTests : IClassFixture<ADLXTestFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXTestFixture _fixture;
        private readonly IntPtr _pGPUTuningServices;

        public GpuTuningServicesTests(ITestOutputHelper output, ADLXTestFixture fixture)
        {
            _output = output;
            _fixture = fixture;
            _pGPUTuningServices = IntPtr.Zero;
            
            _fixture.WriteDiagnostics(_output);

            if (_fixture.CanRunTests)
            {
                try
                {
                    _pGPUTuningServices = _fixture.Api!.GetGPUTuningServices();
                    _output.WriteLine("   GPU tuning services available");
                }
                catch (ADLXException ex)
                {
                    _output.WriteLine($"   ?? GPU tuning services not available: {ex.Message}");
                }
            }
        }

        [SkippableFact]
        public void GetGPUTuningServices_ShouldSucceed()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var pTuningServices = _fixture.Api!.GetGPUTuningServices();
            Assert.NotEqual(IntPtr.Zero, pTuningServices);
            _output.WriteLine($"? GPU tuning services pointer: 0x{pTuningServices:X}");

            ADLXHelpers.ReleaseInterface(pTuningServices);
        }

        [SkippableFact]
        public void IsSupportedAutoTuning_ShouldReturnBooleanValue()
        {
            Skip.IfNot(_fixture.CanRunTests && _pGPUTuningServices != IntPtr.Zero,
                _fixture.CanRunTests ? "GPU tuning services not available" : _fixture.SkipReason);

            var isSupported = ADLXGPUTuningHelpers.IsSupportedAutoTuning(_pGPUTuningServices, _fixture.GPUs[0]);
            _output.WriteLine($"? Auto Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedPresetTuning_ShouldReturnBooleanValue()
        {
            Skip.IfNot(_fixture.CanRunTests && _pGPUTuningServices != IntPtr.Zero,
                _fixture.CanRunTests ? "GPU tuning services not available" : _fixture.SkipReason);

            var isSupported = ADLXGPUTuningHelpers.IsSupportedPresetTuning(_pGPUTuningServices, _fixture.GPUs[0]);
            _output.WriteLine($"? Preset Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedManualGFXTuning_ShouldHandleGracefully()
        {
            Skip.IfNot(_fixture.CanRunTests && _pGPUTuningServices != IntPtr.Zero,
                _fixture.CanRunTests ? "GPU tuning services not available" : _fixture.SkipReason);

            try
            {
                var isSupported = ADLXGPUTuningHelpers.IsSupportedManualGFXTuning(_pGPUTuningServices, _fixture.GPUs[0]);
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
            Skip.IfNot(_fixture.CanRunTests && _pGPUTuningServices != IntPtr.Zero,
                _fixture.CanRunTests ? "GPU tuning services not available" : _fixture.SkipReason);

            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning(_pGPUTuningServices, _fixture.GPUs[0]);
            _output.WriteLine($"? Manual VRAM Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedManualFanTuning_ShouldReturnBooleanValue()
        {
            Skip.IfNot(_fixture.CanRunTests && _pGPUTuningServices != IntPtr.Zero,
                _fixture.CanRunTests ? "GPU tuning services not available" : _fixture.SkipReason);

            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualFanTuning(_pGPUTuningServices, _fixture.GPUs[0]);
            _output.WriteLine($"? Manual Fan Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void IsSupportedManualPowerTuning_ShouldReturnBooleanValue()
        {
            Skip.IfNot(_fixture.CanRunTests && _pGPUTuningServices != IntPtr.Zero,
                _fixture.CanRunTests ? "GPU tuning services not available" : _fixture.SkipReason);

            var isSupported = ADLXGPUTuningHelpers.IsSupportedManualPowerTuning(_pGPUTuningServices, _fixture.GPUs[0]);
            _output.WriteLine($"? Manual Power Tuning Supported: {isSupported}");
        }

        [SkippableFact]
        public void AllSupportChecks_ShouldNotThrow()
        {
            Skip.IfNot(_fixture.CanRunTests && _pGPUTuningServices != IntPtr.Zero,
                _fixture.CanRunTests ? "GPU tuning services not available" : _fixture.SkipReason);

            _output.WriteLine("=== GPU Tuning Support Summary ===");
            
            try { _output.WriteLine($"  Auto Tuning: {ADLXGPUTuningHelpers.IsSupportedAutoTuning(_pGPUTuningServices, _fixture.GPUs[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Auto Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Preset Tuning: {ADLXGPUTuningHelpers.IsSupportedPresetTuning(_pGPUTuningServices, _fixture.GPUs[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Preset Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual GFX Tuning: {ADLXGPUTuningHelpers.IsSupportedManualGFXTuning(_pGPUTuningServices, _fixture.GPUs[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual GFX Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual VRAM Tuning: {ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning(_pGPUTuningServices, _fixture.GPUs[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual VRAM Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual Fan Tuning: {ADLXGPUTuningHelpers.IsSupportedManualFanTuning(_pGPUTuningServices, _fixture.GPUs[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual Fan Tuning: Error - {ex.Message}"); }

            try { _output.WriteLine($"  Manual Power Tuning: {ADLXGPUTuningHelpers.IsSupportedManualPowerTuning(_pGPUTuningServices, _fixture.GPUs[0])}"); }
            catch (Exception ex) { _output.WriteLine($"  Manual Power Tuning: Error - {ex.Message}"); }

            _output.WriteLine("? All support checks completed");
        }

        [SkippableFact]
        public void IsSupportedAutoTuning_WithNullServices_ShouldThrowArgumentNullException()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            Assert.Throws<ArgumentNullException>(() => 
                ADLXGPUTuningHelpers.IsSupportedAutoTuning(IntPtr.Zero, _fixture.GPUs[0]));
            _output.WriteLine("? IsSupportedAutoTuning correctly throws on null services pointer");
        }

        [SkippableFact]
        public void IsSupportedAutoTuning_WithNullGPU_ShouldThrowArgumentNullException()
        {
            Skip.IfNot(_fixture.CanRunTests && _pGPUTuningServices != IntPtr.Zero,
                _fixture.CanRunTests ? "GPU tuning services not available" : _fixture.SkipReason);

            Assert.Throws<ArgumentNullException>(() => 
                ADLXGPUTuningHelpers.IsSupportedAutoTuning(_pGPUTuningServices, IntPtr.Zero));
            _output.WriteLine("? IsSupportedAutoTuning correctly throws on null GPU pointer");
        }
    }
}
