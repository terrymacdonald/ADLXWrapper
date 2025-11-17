using Xunit;
using Xunit.Abstractions;
using ADLXWrapper;

namespace ADLXWrapper.Tests;

/// <summary>
/// Tests for GPU performance monitoring and metrics
/// </summary>
[Collection("ADLX Tests")]
public class PerformanceMonitoringTests
{
    private readonly ADLXTestFixture _fixture;
    private readonly ITestOutputHelper _output;

    public PerformanceMonitoringTests(ADLXTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }

    [SkippableFact]
    public void Test_01_Get_Performance_Monitoring_Services()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");

        var perfServicesPtr = ADLX.new_performanceP_Ptr();
        try
        {
            var result = _fixture.System!.GetPerformanceMonitoringServices(perfServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);

            var perfServices = ADLX.performanceP_Ptr_value(perfServicesPtr);
            Assert.NotNull(perfServices);

            _output.WriteLine("✓ Performance monitoring services retrieved successfully");
        }
        finally
        {
            ADLX.delete_performanceP_Ptr(perfServicesPtr);
        }
    }

    [SkippableFact]
    public void Test_02_GPU_Metrics_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsPerformanceMonitoring, "Performance monitoring not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        Skip.IfNot(_fixture.FirstGPU != null, "First GPU not available");

        var gpu = _fixture.FirstGPU;

        var perfServicesPtr = ADLX.new_performanceP_Ptr();
        try
        {
            var result = _fixture.System!.GetPerformanceMonitoringServices(perfServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);

            var perfServices = ADLX.performanceP_Ptr_value(perfServicesPtr);
            Assert.NotNull(perfServices);

            _output.WriteLine("=== GPU Metrics Support ===");

            // Get metrics support
            var metricsSupportPtr = ADLX.new_metricsSupportP_Ptr();
            try
            {
                result = perfServices.GetSupportedGPUMetrics(gpu, metricsSupportPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    var metricsSupport = ADLX.metricsSupportP_Ptr_value(metricsSupportPtr);
                    if (metricsSupport != null)
                    {
                        // Check temperature support
                        var tempSupportedPtr = ADLX.new_adlx_boolP();
                        try
                        {
                            metricsSupport.IsSupportedGPUTemperature(tempSupportedPtr);
                            bool tempSupported = ADLX.adlx_boolP_value(tempSupportedPtr);
                            _output.WriteLine($"GPU Temperature: {(tempSupported ? "✓ Supported" : "✗ Not supported")}");
                        }
                        finally
                        {
                            ADLX.delete_adlx_boolP(tempSupportedPtr);
                        }

                        // Check usage support
                        var usageSupportedPtr = ADLX.new_adlx_boolP();
                        try
                        {
                            metricsSupport.IsSupportedGPUUsage(usageSupportedPtr);
                            bool usageSupported = ADLX.adlx_boolP_value(usageSupportedPtr);
                            _output.WriteLine($"GPU Usage: {(usageSupported ? "✓ Supported" : "✗ Not supported")}");
                        }
                        finally
                        {
                            ADLX.delete_adlx_boolP(usageSupportedPtr);
                        }

                        // Check clock speed support
                        var clockSupportedPtr = ADLX.new_adlx_boolP();
                        try
                        {
                            metricsSupport.IsSupportedGPUClockSpeed(clockSupportedPtr);
                            bool clockSupported = ADLX.adlx_boolP_value(clockSupportedPtr);
                            _output.WriteLine($"GPU Clock Speed: {(clockSupported ? "✓ Supported" : "✗ Not supported")}");
                        }
                        finally
                        {
                            ADLX.delete_adlx_boolP(clockSupportedPtr);
                        }

                        // Check VRAM usage support
                        var vramSupportedPtr = ADLX.new_adlx_boolP();
                        try
                        {
                            metricsSupport.IsSupportedGPUVRAM(vramSupportedPtr);
                            bool vramSupported = ADLX.adlx_boolP_value(vramSupportedPtr);
                            _output.WriteLine($"VRAM Usage: {(vramSupported ? "✓ Supported" : "✗ Not supported")}");
                        }
                        finally
                        {
                            ADLX.delete_adlx_boolP(vramSupportedPtr);
                        }

                        // Check fan speed support
                        var fanSupportedPtr = ADLX.new_adlx_boolP();
                        try
                        {
                            metricsSupport.IsSupportedGPUFanSpeed(fanSupportedPtr);
                            bool fanSupported = ADLX.adlx_boolP_value(fanSupportedPtr);
                            _output.WriteLine($"Fan Speed: {(fanSupported ? "✓ Supported" : "✗ Not supported")}");
                        }
                        finally
                        {
                            ADLX.delete_adlx_boolP(fanSupportedPtr);
                        }

                        // Check power support
                        var powerSupportedPtr = ADLX.new_adlx_boolP();
                        try
                        {
                            metricsSupport.IsSupportedGPUPower(powerSupportedPtr);
                            bool powerSupported = ADLX.adlx_boolP_value(powerSupportedPtr);
                            _output.WriteLine($"GPU Power: {(powerSupported ? "✓ Supported" : "✗ Not supported")}");
                        }
                        finally
                        {
                            ADLX.delete_adlx_boolP(powerSupportedPtr);
                        }
                    }
                }
            }
            finally
            {
                ADLX.delete_metricsSupportP_Ptr(metricsSupportPtr);
            }
        }
        finally
        {
            ADLX.delete_performanceP_Ptr(perfServicesPtr);
        }
    }

    [SkippableFact]
    public void Test_03_GPU_Current_Metrics()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsPerformanceMonitoring, "Performance monitoring not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        Skip.IfNot(_fixture.FirstGPU != null, "First GPU not available");

        var gpu = _fixture.FirstGPU;

        var perfServicesPtr = ADLX.new_performanceP_Ptr();
        try
        {
            var result = _fixture.System!.GetPerformanceMonitoringServices(perfServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);

            var perfServices = ADLX.performanceP_Ptr_value(perfServicesPtr);
            Assert.NotNull(perfServices);

            _output.WriteLine("=== Current GPU Metrics ===");

            // Get current metrics
            var metricsPtr = ADLX.new_metricsP_Ptr();
            try
            {
                result = perfServices.GetCurrentGPUMetrics(gpu, metricsPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    var metrics = ADLX.metricsP_Ptr_value(metricsPtr);
                    if (metrics != null)
                    {
                        // Temperature
                        var tempPtr = ADLX.new_doubleP();
                        try
                        {
                            var tempResult = metrics.GPUTemperature(tempPtr);
                            if (tempResult == ADLX_RESULT.ADLX_OK)
                            {
                                double temp = ADLX.doubleP_value(tempPtr);
                                _output.WriteLine($"GPU Temperature: {temp:F1}°C");
                                Assert.True(temp >= 0 && temp <= 150, "Temperature should be in valid range");
                            }
                        }
                        finally
                        {
                            ADLX.delete_doubleP(tempPtr);
                        }

                        // Usage
                        var usagePtr = ADLX.new_doubleP();
                        try
                        {
                            var usageResult = metrics.GPUUsage(usagePtr);
                            if (usageResult == ADLX_RESULT.ADLX_OK)
                            {
                                double usage = ADLX.doubleP_value(usagePtr);
                                _output.WriteLine($"GPU Usage: {usage:F1}%");
                                Assert.True(usage >= 0 && usage <= 100, "Usage should be 0-100%");
                            }
                        }
                        finally
                        {
                            ADLX.delete_doubleP(usagePtr);
                        }

                        // Clock Speed
                        var clockPtr = ADLX.new_adlx_intP();
                        try
                        {
                            var clockResult = metrics.GPUClockSpeed(clockPtr);
                            if (clockResult == ADLX_RESULT.ADLX_OK)
                            {
                                int clockSpeed = ADLX.adlx_intP_value(clockPtr);
                                _output.WriteLine($"GPU Clock Speed: {clockSpeed} MHz");
                                Assert.True(clockSpeed > 0, "Clock speed should be positive");
                            }
                        }
                        finally
                        {
                            ADLX.delete_adlx_intP(clockPtr);
                        }

                        // VRAM Clock Speed
                        var vramClockPtr = ADLX.new_adlx_intP();
                        try
                        {
                            var vramClockResult = metrics.GPUVRAMClockSpeed(vramClockPtr);
                            if (vramClockResult == ADLX_RESULT.ADLX_OK)
                            {
                                int vramClock = ADLX.adlx_intP_value(vramClockPtr);
                                _output.WriteLine($"VRAM Clock Speed: {vramClock} MHz");
                            }
                        }
                        finally
                        {
                            ADLX.delete_adlx_intP(vramClockPtr);
                        }

                        // VRAM Usage
                        var vramPtr = ADLX.new_adlx_intP();
                        try
                        {
                            var vramResult = metrics.GPUVRAM(vramPtr);
                            if (vramResult == ADLX_RESULT.ADLX_OK)
                            {
                                int vram = ADLX.adlx_intP_value(vramPtr);
                                _output.WriteLine($"VRAM Usage: {vram} MB");
                                Assert.True(vram >= 0, "VRAM usage should be non-negative");
                            }
                        }
                        finally
                        {
                            ADLX.delete_adlx_intP(vramPtr);
                        }

                        // Fan Speed
                        var fanPtr = ADLX.new_adlx_intP();
                        try
                        {
                            var fanResult = metrics.GPUFanSpeed(fanPtr);
                            if (fanResult == ADLX_RESULT.ADLX_OK)
                            {
                                int fanSpeed = ADLX.adlx_intP_value(fanPtr);
                                _output.WriteLine($"Fan Speed: {fanSpeed} RPM");
                            }
                        }
                        finally
                        {
                            ADLX.delete_adlx_intP(fanPtr);
                        }

                        // Power
                        var powerPtr = ADLX.new_doubleP();
                        try
                        {
                            var powerResult = metrics.GPUPower(powerPtr);
                            if (powerResult == ADLX_RESULT.ADLX_OK)
                            {
                                double power = ADLX.doubleP_value(powerPtr);
                                _output.WriteLine($"GPU Power: {power:F1}W");
                                Assert.True(power >= 0, "Power should be non-negative");
                            }
                        }
                        finally
                        {
                            ADLX.delete_doubleP(powerPtr);
                        }
                    }
                }
                else
                {
                    _output.WriteLine($"Could not get current metrics: {result}");
                }
            }
            finally
            {
                ADLX.delete_metricsP_Ptr(metricsPtr);
            }
        }
        finally
        {
            ADLX.delete_performanceP_Ptr(perfServicesPtr);
        }
    }

    [SkippableFact]
    public void Test_04_GPU_Metrics_Timestamp()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsPerformanceMonitoring, "Performance monitoring not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        Skip.IfNot(_fixture.FirstGPU != null, "First GPU not available");

        var gpu = _fixture.FirstGPU;

        _output.WriteLine("=== GPU Metrics Timestamp ===");
        _output.WriteLine("ℹ️  Note: TimeStamp() method has a SWIG binding limitation:");
        _output.WriteLine("   The C++ API uses adlx_int64 (signed long long) but SWIG doesn't generate");
        _output.WriteLine("   wrapper functions for signed long long, only unsigned (adlx_uint64P).");
        _output.WriteLine("   This causes a type mismatch. The timestamp functionality works in C++");
        _output.WriteLine("   but cannot be easily tested through the C# SWIG bindings.");
        _output.WriteLine("   This is a known limitation of the SWIG code generation.");

        var perfServicesPtr = ADLX.new_performanceP_Ptr();
        try
        {
            var result = _fixture.System!.GetPerformanceMonitoringServices(perfServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);

            var perfServices = ADLX.performanceP_Ptr_value(perfServicesPtr);
            Assert.NotNull(perfServices);

            var metricsPtr = ADLX.new_metricsP_Ptr();
            try
            {
                result = perfServices.GetCurrentGPUMetrics(gpu, metricsPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    var metrics = ADLX.metricsP_Ptr_value(metricsPtr);
                    Assert.NotNull(metrics);
                    _output.WriteLine("✓ Successfully retrieved GPU metrics (timestamp test skipped due to binding limitation)");
                }
                else
                {
                    _output.WriteLine($"Could not get metrics: {result}");
                }
            }
            finally
            {
                ADLX.delete_metricsP_Ptr(metricsPtr);
            }
        }
        finally
        {
            ADLX.delete_performanceP_Ptr(perfServicesPtr);
        }
    }
}