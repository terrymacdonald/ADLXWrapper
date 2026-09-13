using ADLXWrapper;

Console.WriteLine("=== ADLX GPU Stress Test Sample ===");
Console.WriteLine("This sample intentionally loads the selected GPU.");

try
{
    using var adlx = ADLXApiHelper.Initialize();
    using var system = adlx.GetSystemServices();
    var gpus = system.EnumerateADLXGPUs();

    try
    {
        var supportedGpus = new List<ADLXGPU>();
        foreach (var gpu in gpus)
        {
            try
            {
                if (gpu.IsStressTestSupported())
                    supportedGpus.Add(gpu);
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
            }
        }

        if (supportedGpus.Count == 0)
        {
            Console.WriteLine("GPU stress testing is not supported on any detected GPU.");
            return;
        }

        Console.WriteLine("Supported GPUs:");
        for (var index = 0; index < supportedGpus.Count; index++)
        {
            var gpu = supportedGpus[index];
            Console.WriteLine($" {index + 1}) {gpu.Name} (Unique ID: {gpu.UniqueId})");
        }

        Console.Write("Select a GPU: ");
        if (!int.TryParse(Console.ReadLine(), out var selection) || selection < 1 || selection > supportedGpus.Count)
        {
            Console.WriteLine("Invalid GPU selection.");
            return;
        }

        Console.Write("Duration in seconds: ");
        if (!uint.TryParse(Console.ReadLine(), out var durationSeconds) || durationSeconds == 0)
        {
            Console.WriteLine("Duration must be a positive number of seconds.");
            return;
        }

        var selectedGpu = supportedGpus[selection - 1];
        Console.Write($"Type START to begin a {durationSeconds}-second stress test on {selectedGpu.Name}: ");
        if (!string.Equals(Console.ReadLine()?.Trim(), "START", StringComparison.Ordinal))
        {
            Console.WriteLine("Stress test cancelled.");
            return;
        }

        var operation = selectedGpu.StartStressTest(durationSeconds);
        try
        {
            Console.WriteLine("Stress test started. Waiting for ADLX completion notification...");
            var result = await operation.Completion;
            Console.WriteLine($"Stress test completed at {result.CompletedAt:O}.");
            Console.WriteLine($"GPU Unique ID: {result.GpuUniqueId}");
            Console.WriteLine($"Result: {(result.Succeeded ? "Passed" : "Failed")}");
        }
        finally
        {
            if (operation.IsCompleted)
                operation.Dispose();
        }
    }
    finally
    {
        foreach (var gpu in gpus)
            gpu.Dispose();
    }
}
catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
{
    Console.WriteLine($"Not supported: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
