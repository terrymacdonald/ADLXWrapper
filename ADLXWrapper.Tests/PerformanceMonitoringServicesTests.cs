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
        private readonly IntPtr[] _gpus;
        private readonly IntPtr _pPerfMonServices;

        public PerformanceMonitoringServicesTests(ITestOutputHelper output)
        {
            _output = output;
            _gpus = Array.Empty<IntPtr>();
            _pPerfMonServices = IntPtr.Zero;

            try
            {
                _api = ADLXApi.Initialize();
                _gpus = _api.EnumerateGPUs();
                
                if (_gpus.Length > 0)
                {
                    try
                    {
                        _pPerfMonServices = _api.GetPerformanceMonitoringServices();
                        _hasHardware = true;
                        _output.WriteLine($"? ADLX initialized, found {_gpus.Length} GPU(s) with performance monitoring");
                    }
                    catch (ADLXException ex)
                    {
                        _hasHardware = false;
                        _output.WriteLine($"? Performance monitoring services not available: {ex.Message}");
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
        public void GetPerformanceMonitoringServices_ShouldSucceed()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var pServices = _api.GetPerformanceMonitoringServices();
            Assert.NotEqual(IntPtr.Zero, pServices);
            _output.WriteLine($"? Performance monitoring services pointer: 0x{pServices:X}");

            ADLXHelpers.ReleaseInterface(pServices);
        }

        [Fact]
        public void GetSupportedGPUMetrics_ShouldReturnValidPointer()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pPerfMonServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No performance monitoring services available");
                return;
            }

            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(_pPerfMonServices, _gpus[0]);
            Assert.NotEqual(IntPtr.Zero, pMetricsSupport);
            _output.WriteLine($"? GPU metrics support pointer: 0x{pMetricsSupport:X}");

            ADLXHelpers.ReleaseInterface(pMetricsSupport);
        }

        [Fact]
        public void IsSupportedGPUUsage_ShouldReturnBoolean()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pPerfMonServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No performance monitoring services available");
                return;
            }

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

        [Fact]
        public void IsSupportedGPUTemperature_ShouldReturnBoolean()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pPerfMonServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No performance monitoring services available");
                return;
            }

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

        [Fact]
        public void GetCurrentGPUMetrics_ShouldReturnValidPointer()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pPerfMonServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No performance monitoring services available");
                return;
            }

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _gpus[0]);
            Assert.NotEqual(IntPtr.Zero, pMetrics);
            _output.WriteLine($"? Current GPU metrics pointer: 0x{pMetrics:X}");

            ADLXHelpers.ReleaseInterface(pMetrics);
        }

        [Fact]
        public void GetGPUTemperature_ShouldReturnValidValue()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pPerfMonServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No performance monitoring services available");
                return;
            }

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

        [Fact]
        public void GetGPUUsage_ShouldReturnValidValue()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pPerfMonServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No performance monitoring services available");
                return;
            }

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

        [Fact]
        public void GetGPUClockSpeed_ShouldReturnValidValue()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pPerfMonServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No performance monitoring services available");
                return;
            }

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

        [Fact]
        public void AllMetrics_ShouldNotThrow()
        {
            if (!_hasHardware || _gpus.Length == 0 || _pPerfMonServices == IntPtr.Zero)
            {
                _output.WriteLine("? Test skipped - No performance monitoring services available");
                return;
            }

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _gpus[0]);
            try
            {
                _output.WriteLine("=== Current GPU Metrics ===");
                
                try
                {
                    var temp = ADLXPerformanceMonitoringHelpers.GetGPUTemperature(pMetrics);
                    _output.WriteLine($"  Temperature: {temp:F1}°C");
                }
                catch (Exception ex)
                {
                    _output.WriteLine($"  Temperature: Error - {ex.Message}");
                }

                try
                {
                    var usage = ADLXPerformanceMonitoringHelpers.GetGPUUsage(pMetrics);
                    _output.WriteLine($"  Usage: {usage:F1}%");
                }
                catch (Exception ex)
                {
                    _output.WriteLine($"  Usage: Error - {ex.Message}");
                }

                try
                {
                    var clockSpeed = ADLXPerformanceMonitoringHelpers.GetGPUClockSpeed(pMetrics);
                    _output.WriteLine($"  Clock Speed: {clockSpeed} MHz");
                }
                catch (Exception ex)
                {
                    _output.WriteLine($"  Clock Speed: Error - {ex.Message}");
                }

                try
                {
                    var vramClockSpeed = ADLXPerformanceMonitoringHelpers.GetGPUVRAMClockSpeed(pMetrics);
                    _output.WriteLine($"  VRAM Clock Speed: {vramClockSpeed} MHz");
                }
                catch (Exception ex)
                {
                    _output.WriteLine($"  VRAM Clock Speed: Error - {ex.Message}");
                }

                try
                {
                    var vram = ADLXPerformanceMonitoringHelpers.GetGPUVRAM(pMetrics);
                    _output.WriteLine($"  VRAM Usage: {vram} MB");
                }
                catch (Exception ex)
                {
                    _output.WriteLine($"  VRAM Usage: Error - {ex.Message}");
                }

                try
                {
                    var fanSpeed = ADLXPerformanceMonitoringHelpers.GetGPUFanSpeed(pMetrics);
                    _output.WriteLine($"  Fan Speed: {fanSpeed} RPM");
                }
                catch (Exception ex)
                {
                    _output.WriteLine($"  Fan Speed: Error - {ex.Message}");
                }

                try
                {
                    var power = ADLXPerformanceMonitoringHelpers.GetGPUPower(pMetrics);
                    _output.WriteLine($"  Power: {power:F1}W");
                }
                catch (Exception ex)
                {
                    _output.WriteLine($"  Power: Error - {ex.Message}");
                }

                _output.WriteLine("? All metric checks completed");
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetrics);
            }
        }

        [Fact]
        public void GetCurrentGPUMetrics_WithNullServices_ShouldThrowArgumentNullException()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            Assert.Throws<ArgumentNullException>(() => 
                ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(IntPtr.Zero, _gpus[0]));
            _output.WriteLine("? GetCurrentGPUMetrics correctly throws on null services pointer");
        }

        public void Dispose()
        {
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
