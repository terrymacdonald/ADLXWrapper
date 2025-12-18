using System.Linq;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Perf Monitoring Sample ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    var gpus = sysHelper.EnumerateGPUHandles();
    if (gpus.Length == 0)
    {
        Console.WriteLine("No AMD GPU found; exiting.");
        return;
    }

    var sys = sysHelper.GetSystemServicesNative();
    using var perf = AdlxInterfaceHandle.From(ADLXPerformanceMonitoringHelpers.GetPerformanceMonitoringServices(sys), addRef: false);
    var gpu = gpus[0];

    try
    {
        Console.WriteLine("Reading current GPU metrics...");
        var current = ADLXPerformanceMonitoringHelpers.GetCurrentGpuMetrics(perf.As<IADLXPerformanceMonitoringServices>(), gpu.As<IADLXGPU>());
        Console.WriteLine($"Temp={current.Temperature:F1}C, Usage={current.Usage:F1}%, Clock={current.ClockSpeed}MHz, VRAM={current.VRAMUsage}MB");

        Console.WriteLine("Starting/stopping tracking and fetching history (0,0 => driver default window)...");
        ADLXPerformanceMonitoringHelpers.StartPerformanceMetricsTracking(perf.As<IADLXPerformanceMonitoringServices>());
        ADLXPerformanceMonitoringHelpers.StopPerformanceMetricsTracking(perf.As<IADLXPerformanceMonitoringServices>());

        var snapshots = ADLXPerformanceMonitoringHelpers.EnumerateGpuMetricsHistory(perf.As<IADLXPerformanceMonitoringServices>(), gpu.As<IADLXGPU>(), 0, 0).ToList();
        Console.WriteLine($"GPU history count: {snapshots.Count}");

        var sysSnapshots = ADLXPerformanceMonitoringHelpers.EnumerateSystemMetricsHistory(perf.As<IADLXPerformanceMonitoringServices>(), 0, 0).ToList();
        Console.WriteLine($"System history count: {sysSnapshots.Count}");

        var allSnapshots = ADLXPerformanceMonitoringHelpers.EnumerateAllMetricsHistory(perf.As<IADLXPerformanceMonitoringServices>(), 0, 0).ToList();
        Console.WriteLine($"All-metrics history count: {allSnapshots.Count}");
        if (allSnapshots.Count > 0)
        {
            var last = allSnapshots[^1];
            var sysSnap = last.System;
            if (sysSnap.HasValue)
                Console.WriteLine($"System CPU={sysSnap.Value.CpuUsage:F1}% RAM={sysSnap.Value.SystemRam}MB");

            if (last.GpuMetrics.Length > 0)
                Console.WriteLine($"First GPU snapshot temp={last.GpuMetrics[0].Metrics.Temperature:F1}C");
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
}
