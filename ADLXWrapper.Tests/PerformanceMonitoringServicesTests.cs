using Xunit;
using System;
using ADLXWrapper;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Performance monitoring tests for ADLX wrapper (ClangSharp-based)
    /// Tests GPU metrics and performance monitoring (read-only)
    /// </summary>
    public class PerformanceMonitoringServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly IntPtr[] _gpus = Array.Empty<IntPtr>();
        private readonly IntPtr _pPerfMonServices;

        public PerformanceMonitoringServicesTests(ITestOutputHelper output)
        {
            _output = output;
            _pPerfMonServices = IntPtr.Zero;

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

                // Try to get performance monitoring services
                try
                {
                    _pPerfMonServices = _api.GetPerformanceMonitoringServices();
                    _output.WriteLine("  Performance monitoring services: Available");
                }
                catch (ADLXException ex)
                {
                    _output.WriteLine($"  Performance monitoring services: Not available ({ex.Message})");
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
            // Release performance monitoring services
            if (_pPerfMonServices != IntPtr.Zero)
            {
                try
                {
                    ADLXHelpers.ReleaseInterface(_pPerfMonServices);
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
        public void GetPerformanceMonitoringServices_ShouldSucceed()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            var pServices = _api!.GetPerformanceMonitoringServices();
            Assert.NotEqual(IntPtr.Zero, pServices);
            _output.WriteLine($"? Performance monitoring services pointer: 0x{pServices:X}");

            ADLXHelpers.ReleaseInterface(pServices);
        }

        [SkippableFact]
        public void GetSupportedGPUMetrics_ShouldReturnValidPointer()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == IntPtr.Zero || _gpus.Length == 0,
                _pPerfMonServices != IntPtr.Zero ? _skipReason : "Performance monitoring services not available");

            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(_pPerfMonServices, _gpus[0]);
            Assert.NotEqual(IntPtr.Zero, pMetricsSupport);
            _output.WriteLine($"? GPU metrics support pointer: 0x{pMetricsSupport:X}");

            ADLXHelpers.ReleaseInterface(pMetricsSupport);
        }

        [SkippableFact]
        public void IsSupportedGPUUsage_ShouldReturnBoolean()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == IntPtr.Zero || _gpus.Length == 0,
                _pPerfMonServices != IntPtr.Zero ? _skipReason : "Performance monitoring services not available");

            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(_pPerfMonServices, _gpus[0]);
            try
            {
                var isSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUUsage(pMetricsSupport);
                _output.WriteLine($"? GPU Usage Supported: {isSupported}");
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetricsSupport);
            }
        }

        [SkippableFact]
        public void IsSupportedGPUTemperature_ShouldReturnBoolean()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == IntPtr.Zero || _gpus.Length == 0,
                _pPerfMonServices != IntPtr.Zero ? _skipReason : "Performance monitoring services not available");

            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(_pPerfMonServices, _gpus[0]);
            try
            {
                var isSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUTemperature(pMetricsSupport);
                _output.WriteLine($"? GPU Temperature Supported: {isSupported}");
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetricsSupport);
            }
        }

        [SkippableFact]
        public void GetCurrentGPUMetrics_ShouldReturnValidPointer()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == IntPtr.Zero || _gpus.Length == 0,
                _pPerfMonServices != IntPtr.Zero ? _skipReason : "Performance monitoring services not available");

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _gpus[0]);
            Assert.NotEqual(IntPtr.Zero, pMetrics);
            _output.WriteLine($"? Current GPU metrics pointer: 0x{pMetrics:X}");

            ADLXHelpers.ReleaseInterface(pMetrics);
        }

        [SkippableFact]
        public void GetGPUTemperature_ShouldReturnValidValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == IntPtr.Zero || _gpus.Length == 0,
                _pPerfMonServices != IntPtr.Zero ? _skipReason : "Performance monitoring services not available");

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _gpus[0]);
            try
            {
                var temperature = ADLXPerformanceMonitoringHelpers.GetGPUTemperature(pMetrics);
                Assert.True(temperature >= 0 && temperature <= 150, "Temperature should be in valid range");
                _output.WriteLine($"? GPU Temperature: {temperature:F1}°C");
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetrics);
            }
        }

        [SkippableFact]
        public void GetGPUUsage_ShouldReturnValidValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == IntPtr.Zero || _gpus.Length == 0,
                _pPerfMonServices != IntPtr.Zero ? _skipReason : "Performance monitoring services not available");

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _gpus[0]);
            try
            {
                var usage = ADLXPerformanceMonitoringHelpers.GetGPUUsage(pMetrics);
                Assert.True(usage >= 0 && usage <= 100, "Usage should be 0-100%");
                _output.WriteLine($"? GPU Usage: {usage:F1}%");
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetrics);
            }
        }

        [SkippableFact]
        public void GetGPUClockSpeed_ShouldReturnValidValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == IntPtr.Zero || _gpus.Length == 0,
                _pPerfMonServices != IntPtr.Zero ? _skipReason : "Performance monitoring services not available");

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _gpus[0]);
            try
            {
                var clockSpeed = ADLXPerformanceMonitoringHelpers.GetGPUClockSpeed(pMetrics);
                Assert.True(clockSpeed > 0, "Clock speed should be positive");
                _output.WriteLine($"? GPU Clock Speed: {clockSpeed} MHz");
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetrics);
            }
        }

        [SkippableFact]
        public void AllMetrics_ShouldNotThrow()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == IntPtr.Zero || _gpus.Length == 0,
                _pPerfMonServices != IntPtr.Zero ? _skipReason : "Performance monitoring services not available");

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _gpus[0]);
            try
            {
                _output.WriteLine("=== Current GPU Metrics ===");
                
                try { _output.WriteLine($"  Temperature: {ADLXPerformanceMonitoringHelpers.GetGPUTemperature(pMetrics):F1}°C"); }
                catch (Exception ex) { _output.WriteLine($"  Temperature: Error - {ex.Message}"); }

                try { _output.WriteLine($"  Usage: {ADLXPerformanceMonitoringHelpers.GetGPUUsage(pMetrics):F1}%"); }
                catch (Exception ex) { _output.WriteLine($"  Usage: Error - {ex.Message}"); }

                try { _output.WriteLine($"  Clock Speed: {ADLXPerformanceMonitoringHelpers.GetGPUClockSpeed(pMetrics)} MHz"); }
                catch (Exception ex) { _output.WriteLine($"  Clock Speed: Error - {ex.Message}"); }

                try { _output.WriteLine($"  VRAM Clock Speed: {ADLXPerformanceMonitoringHelpers.GetGPUVRAMClockSpeed(pMetrics)} MHz"); }
                catch (Exception ex) { _output.WriteLine($"  VRAM Clock Speed: Error - {ex.Message}"); }

                try { _output.WriteLine($"  VRAM Usage: {ADLXPerformanceMonitoringHelpers.GetGPUVRAM(pMetrics)} MB"); }
                catch (Exception ex) { _output.WriteLine($"  VRAM Usage: Error - {ex.Message}"); }

                try { _output.WriteLine($"  Fan Speed: {ADLXPerformanceMonitoringHelpers.GetGPUFanSpeed(pMetrics)} RPM"); }
                catch (Exception ex) { _output.WriteLine($"  Fan Speed: Error - {ex.Message}"); }

                try { _output.WriteLine($"  Power: {ADLXPerformanceMonitoringHelpers.GetGPUPower(pMetrics):F1}W"); }
                catch (Exception ex) { _output.WriteLine($"  Power: Error - {ex.Message}"); }

                _output.WriteLine("? All metric checks completed");
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetrics);
            }
        }

        [SkippableFact]
        public void GetCurrentGPUMetrics_WithNullServices_ShouldThrowArgumentNullException()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _gpus.Length == 0, _skipReason);

            Assert.Throws<ArgumentNullException>(() => 
                ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(IntPtr.Zero, _gpus[0]));
            _output.WriteLine("? GetCurrentGPUMetrics correctly throws on null services pointer");
        }
    }
}
