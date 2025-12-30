using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public class ADLXPerformanceMonitoringServicesFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLXPerformanceMonitoringServicesFacadeTests(FacadeSessionFixture fixture)
    {
        _fixture = fixture;
    }

    private void SkipIfUnavailable()
    {
        Skip.If(!string.IsNullOrWhiteSpace(_fixture.SkipReason), _fixture.SkipReason);
    }

    private ADLXPerformanceMonitoringServicesHelper GetPerfOrSkip()
    {
        SkipIfUnavailable();
        try
        {
            return _fixture.System!.GetPerformanceMonitoringServices();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Performance monitoring services not supported on this hardware/driver.");
        }
    }

    private unsafe ADLXInterfaceHandle GetFirstGpuHandleOrSkip()
    {
        var handles = _fixture.System!.EnumerateGPUsHandle();
        Skip.If(handles.Length == 0, "No GPUs returned by ADLX.");
        return handles[0];
    }

    [SkippableFact]
    public void Performance_monitoring_gpu_support_facade()
    {
        using var perf = GetPerfOrSkip();
        unsafe
        {
            using var gpuHandle = GetFirstGpuHandleOrSkip();
            if (!perf.TryGetGpuMetricsSupport(gpuHandle.As<IADLXGPU>(), out var support))
                throw new Xunit.SkipException("GPU metrics support query failed or unsupported on this GPU.");

            Assert.IsType<bool>(support.UsageSupported);
            Assert.IsType<bool>(support.ClockSpeedSupported);
            Assert.IsType<bool>(support.TemperatureSupported);
            Assert.IsType<bool>(support.PowerSupported);
        }
    }

    [SkippableFact]
    public void Performance_monitoring_gpu_metrics_snapshot_facade()
    {
        using var perf = GetPerfOrSkip();
        unsafe
        {
            using var gpuHandle = GetFirstGpuHandleOrSkip();
            if (!perf.TryGetGpuMetricsSupport(gpuHandle.As<IADLXGPU>(), out var support))
                throw new Xunit.SkipException("GPU metrics support query failed or unsupported on this GPU.");

            if (!perf.TryGetCurrentGpuMetrics(gpuHandle.As<IADLXGPU>(), out var metrics))
                throw new Xunit.SkipException("GPU metrics not available on this GPU.");

            Assert.True(metrics.TimestampMs >= 0);
            if (support.UsageSupported)
                Assert.True(metrics.Usage >= 0);
            if (support.TemperatureSupported)
                Assert.True(metrics.Temperature >= 0);
            if (support.HotspotTemperatureSupported)
                Assert.True(metrics.HotspotTemperature >= 0);
            if (support.ClockSpeedSupported)
                Assert.True(metrics.ClockSpeed >= 0);
            if (support.VRAMClockSpeedSupported)
                Assert.True(metrics.VRAMClockSpeed >= 0);
            if (support.VRAMSupported)
                Assert.True(metrics.VRAMUsage >= 0);
            if (support.FanSpeedSupported)
                Assert.True(metrics.FanSpeed >= 0);
            if (support.PowerSupported)
                Assert.True(metrics.Power >= 0);
            if (support.TotalBoardPowerSupported)
                Assert.True(metrics.TotalBoardPower >= 0);
            if (support.VoltageSupported)
                Assert.True(metrics.Voltage >= 0);
        }
    }

    [SkippableFact]
    public void Performance_monitoring_system_metrics_snapshot_facade()
    {
        using var perf = GetPerfOrSkip();
        try
        {
            var metrics = perf.GetCurrentSystemMetrics();
            Assert.True(metrics.TimestampMs >= 0);
            Assert.True(metrics.CpuUsage >= 0);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("System metrics not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void Performance_monitoring_sampling_interval_facade()
    {
        using var perf = GetPerfOrSkip();
        ADLX_IntRange range;
        try
        {
            range = perf.GetSamplingIntervalRange();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Sampling interval not supported on this hardware/driver.");
        }

        int current;
        try
        {
            current = perf.GetSamplingInterval();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Sampling interval not supported on this hardware/driver.");
        }

        // Re-apply the current interval (safe, no behavioral change)
        var target = current;
        if (target < range.minValue) target = range.minValue;
        if (target > range.maxValue) target = range.maxValue;

        Assert.True(perf.TrySetSamplingInterval(target));
        var after = perf.GetSamplingInterval();
        Assert.Equal(target, after);
    }

    [SkippableFact]
    public void Performance_monitoring_history_size_facade()
    {
        using var perf = GetPerfOrSkip();
        int currentSize;
        try
        {
            currentSize = perf.GetMaxPerformanceMetricsHistorySize();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Performance metrics history sizing not supported on this hardware/driver.");
        }

        // Re-apply the current size (safe, no behavioral change)
        Assert.True(perf.TrySetMaxPerformanceMetricsHistorySize(currentSize));
        var after = perf.GetMaxPerformanceMetricsHistorySize();
        Assert.Equal(currentSize, after);
    }
}
