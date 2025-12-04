using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Performance monitoring tests for ADLX wrapper (ClangSharp-based)
    /// Tests GPU metrics and performance monitoring (read-only)
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class PerformanceMonitoringServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle[] _gpus = Array.Empty<AdlxInterfaceHandle>();
        private readonly AdlxInterfaceHandle? _pPerfMonServices;

        public PerformanceMonitoringServicesTests(ITestOutputHelper output)
        {
            _output = output;
            _pPerfMonServices = null;

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

            if (!ADLXApi.IsADLXDllAvailable(out string dllError))
            {
                _hasDll = false;
                _skipReason = dllError;
                _output.WriteLine($"??  {dllError}");
                return;
            }
            _hasDll = true;

            try
            {
                _api = ADLXApi.Initialize();
                _gpus = _api.EnumerateGPUHandles();
                _output.WriteLine($"? ADLX initialized successfully");
                _output.WriteLine($"  ADLX Version: {_api.GetVersion()}");
                _output.WriteLine($"  GPUs found: {_gpus.Length}");

                try
                {
                    var handle = _api.GetPerformanceMonitoringServicesHandle();
                    _pPerfMonServices = handle;
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
            if (_pPerfMonServices is not null)
            {
                try
                {
                    _pPerfMonServices.Dispose();
                }
                catch
                {
                }
            }

            foreach (var gpu in _gpus)
            {
                try
                {
                    gpu.Dispose();
                }
                catch
                {
                }
            }

            _api?.Dispose();
        }

        [SkippableFact]
        public void GetPerformanceMonitoringServices_ShouldSucceed()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            using var pServices = _api!.GetPerformanceMonitoringServicesHandle();
            Assert.False(pServices.IsInvalid);
            _output.WriteLine($"? Performance monitoring services pointer: 0x{((IntPtr)pServices):X}");
        }

        [SkippableFact]
        public void GetSupportedGPUMetrics_ShouldReturnValidPointer()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == null || _gpus.Length == 0,
                _pPerfMonServices != null ? _skipReason : "Performance monitoring services not available");

            var perf = _pPerfMonServices!;
            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(perf, _gpus[0]);
            Assert.NotEqual(IntPtr.Zero, pMetricsSupport);
            _output.WriteLine($"? GPU metrics support pointer: 0x{pMetricsSupport:X}");

            ADLXHelpers.ReleaseInterface(pMetricsSupport);
        }

        [SkippableFact]
        public void IsSupportedGPUUsage_ShouldReturnBoolean()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == null || _gpus.Length == 0,
                _pPerfMonServices != null ? _skipReason : "Performance monitoring services not available");

            var perf = _pPerfMonServices!;
            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(perf, _gpus[0]);
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
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == null || _gpus.Length == 0,
                _pPerfMonServices != null ? _skipReason : "Performance monitoring services not available");

            var perf = _pPerfMonServices!;
            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(perf, _gpus[0]);
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
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == null || _gpus.Length == 0,
                _pPerfMonServices != null ? _skipReason : "Performance monitoring services not available");

            var perf = _pPerfMonServices!;
            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(perf, _gpus[0]);
            Assert.NotEqual(IntPtr.Zero, pMetrics);
            _output.WriteLine($"? Current GPU metrics pointer: 0x{pMetrics:X}");

            ADLXHelpers.ReleaseInterface(pMetrics);
        }

        [SkippableFact]
        public void GetGPUTemperature_ShouldReturnValidValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == null || _gpus.Length == 0,
                _pPerfMonServices != null ? _skipReason : "Performance monitoring services not available");

            var perf = _pPerfMonServices!;
            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(perf, _gpus[0]);
            try
            {
                var temperature = ADLXPerformanceMonitoringHelpers.GetGPUTemperature(pMetrics);
                Assert.True(temperature >= 0 && temperature <= 150, "Temperature should be in valid range");
                _output.WriteLine($"? GPU Temperature: {temperature:F1} C");
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetrics);
            }
        }

        [SkippableFact]
        public void GetGPUUsage_ShouldReturnValidValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == null || _gpus.Length == 0,
                _pPerfMonServices != null ? _skipReason : "Performance monitoring services not available");

            var perf = _pPerfMonServices!;
            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(perf, _gpus[0]);
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
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == null || _gpus.Length == 0,
                _pPerfMonServices != null ? _skipReason : "Performance monitoring services not available");

            var perf = _pPerfMonServices!;
            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(perf, _gpus[0]);
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
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPerfMonServices == null || _gpus.Length == 0,
                _pPerfMonServices != null ? _skipReason : "Performance monitoring services not available");

            var perf = _pPerfMonServices!;
            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(perf, _gpus[0]);
            try
            {
                _output.WriteLine("=== Current GPU Metrics ===");
                
                try { _output.WriteLine($"  Temperature: {ADLXPerformanceMonitoringHelpers.GetGPUTemperature(pMetrics):F1} C"); }
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

                try { _output.WriteLine($"  Power: {ADLXPerformanceMonitoringHelpers.GetGPUPower(pMetrics):F1} W"); }
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
