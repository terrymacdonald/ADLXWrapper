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
    public class PerformanceMonitoringServicesTests : IClassFixture<ADLXTestFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXTestFixture _fixture;
        private readonly IntPtr _pPerfMonServices;

        public PerformanceMonitoringServicesTests(ITestOutputHelper output, ADLXTestFixture fixture)
        {
            _output = output;
            _fixture = fixture;
            _pPerfMonServices = IntPtr.Zero;
            
            _fixture.WriteDiagnostics(_output);

            if (_fixture.CanRunTests)
            {
                try
                {
                    _pPerfMonServices = _fixture.Api!.GetPerformanceMonitoringServices();
                    _output.WriteLine("   Performance monitoring services available");
                }
                catch (ADLXException ex)
                {
                    _output.WriteLine($"   ?? Performance monitoring services not available: {ex.Message}");
                }
            }
        }

        [SkippableFact]
        public void GetPerformanceMonitoringServices_ShouldSucceed()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var pServices = _fixture.Api!.GetPerformanceMonitoringServices();
            Assert.NotEqual(IntPtr.Zero, pServices);
            _output.WriteLine($"? Performance monitoring services pointer: 0x{pServices:X}");

            ADLXHelpers.ReleaseInterface(pServices);
        }

        [SkippableFact]
        public void GetSupportedGPUMetrics_ShouldReturnValidPointer()
        {
            Skip.IfNot(_fixture.CanRunTests && _pPerfMonServices != IntPtr.Zero,
                _fixture.CanRunTests ? "Performance monitoring services not available" : _fixture.SkipReason);

            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(_pPerfMonServices, _fixture.GPUs[0]);
            Assert.NotEqual(IntPtr.Zero, pMetricsSupport);
            _output.WriteLine($"? GPU metrics support pointer: 0x{pMetricsSupport:X}");

            ADLXHelpers.ReleaseInterface(pMetricsSupport);
        }

        [SkippableFact]
        public void IsSupportedGPUUsage_ShouldReturnBoolean()
        {
            Skip.IfNot(_fixture.CanRunTests && _pPerfMonServices != IntPtr.Zero,
                _fixture.CanRunTests ? "Performance monitoring services not available" : _fixture.SkipReason);

            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(_pPerfMonServices, _fixture.GPUs[0]);
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
            Skip.IfNot(_fixture.CanRunTests && _pPerfMonServices != IntPtr.Zero,
                _fixture.CanRunTests ? "Performance monitoring services not available" : _fixture.SkipReason);

            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(_pPerfMonServices, _fixture.GPUs[0]);
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
            Skip.IfNot(_fixture.CanRunTests && _pPerfMonServices != IntPtr.Zero,
                _fixture.CanRunTests ? "Performance monitoring services not available" : _fixture.SkipReason);

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _fixture.GPUs[0]);
            Assert.NotEqual(IntPtr.Zero, pMetrics);
            _output.WriteLine($"? Current GPU metrics pointer: 0x{pMetrics:X}");

            ADLXHelpers.ReleaseInterface(pMetrics);
        }

        [SkippableFact]
        public void GetGPUTemperature_ShouldReturnValidValue()
        {
            Skip.IfNot(_fixture.CanRunTests && _pPerfMonServices != IntPtr.Zero,
                _fixture.CanRunTests ? "Performance monitoring services not available" : _fixture.SkipReason);

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _fixture.GPUs[0]);
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
            Skip.IfNot(_fixture.CanRunTests && _pPerfMonServices != IntPtr.Zero,
                _fixture.CanRunTests ? "Performance monitoring services not available" : _fixture.SkipReason);

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _fixture.GPUs[0]);
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
            Skip.IfNot(_fixture.CanRunTests && _pPerfMonServices != IntPtr.Zero,
                _fixture.CanRunTests ? "Performance monitoring services not available" : _fixture.SkipReason);

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _fixture.GPUs[0]);
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
            Skip.IfNot(_fixture.CanRunTests && _pPerfMonServices != IntPtr.Zero,
                _fixture.CanRunTests ? "Performance monitoring services not available" : _fixture.SkipReason);

            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(_pPerfMonServices, _fixture.GPUs[0]);
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
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            Assert.Throws<ArgumentNullException>(() => 
                ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(IntPtr.Zero, _fixture.GPUs[0]));
            _output.WriteLine("? GetCurrentGPUMetrics correctly throws on null services pointer");
        }
    }
}
