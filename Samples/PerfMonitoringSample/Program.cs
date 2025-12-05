using ADLXWrapper;

Console.WriteLine("=== ADLX Perf Monitoring Sample ===");

using var adlx = ADLXApi.Initialize();
var gpus = adlx.EnumerateGPUHandles();
if (gpus.Length == 0)
{
    Console.WriteLine("No AMD GPU found; exiting.");
    return;
}

using var perf = adlx.GetPerformanceMonitoringServicesHandle();
var gpu = gpus[0];

try
{
    Console.WriteLine("Reading current GPU metrics...");
    var current = ADLXPerformanceMonitoringInfo.GetCurrentMetrics(perf, gpu);
    Console.WriteLine($"Temp={current.Temperature:F1}C, Usage={current.Usage:F1}%, Clock={current.ClockSpeed}MHz, VRAM={current.VRAMUsage}MB");

    Console.WriteLine("Starting/stopping tracking and fetching history (0,0 => driver default window)...");
    ADLXPerformanceMonitoringHelpers.StartPerformanceMetricsTracking(perf);
    ADLXPerformanceMonitoringHelpers.StopPerformanceMetricsTracking(perf);

    using var gpuHistory = ADLXPerformanceMonitoringHelpers.GetGPUMetricsHistory(perf, gpu, 0, 0);
    var snapshots = ADLXPerformanceMonitoringHelpers.EnumerateGPUMetricsList(gpuHistory);
    Console.WriteLine($"GPU history count: {snapshots.Length}");

    using var sysHistory = ADLXPerformanceMonitoringHelpers.GetSystemMetricsHistory(perf, 0, 0);
    var sysSnapshots = ADLXPerformanceMonitoringHelpers.EnumerateSystemMetricsList(sysHistory);
    Console.WriteLine($"System history count: {sysSnapshots.Length}");

    using var allHistory = ADLXPerformanceMonitoringHelpers.GetAllMetricsHistory(perf, 0, 0);
    var allSnapshots = ADLXPerformanceMonitoringHelpers.EnumerateAllMetricsList(allHistory, new[] { (IntPtr)gpu });
    Console.WriteLine($"All-metrics history count: {allSnapshots.Length}");
    if (allSnapshots.Length > 0)
    {
        var first = allSnapshots[^1];
        if (first.System is { } sys)
            Console.WriteLine($"System CPU={sys.CpuUsage:F1}% RAM={sys.SystemRam}MB");
        if (first.GPU.Length > 0)
            Console.WriteLine($"First GPU snapshot temp={first.GPU[0].Metrics.Temperature:F1}C");
    }
}
catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
{
    Console.WriteLine("Perf monitoring not supported on this hardware/driver.");
}
finally
{
    foreach (var handle in gpus)
    {
        handle.Dispose();
    }
}
