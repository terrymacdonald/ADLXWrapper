# ADLXWrapper (ClangSharp-based)

High-performance C# bindings for AMD ADLX using vtable-based interop.

## Build
- Requires .NET SDK 10.0 and the ADLX SDK (downloaded into `../ADLX/` via `prepare_adlx.ps1`).

```powershell
# from repo root
.\prepare_adlx.ps1
.\build_adlx.ps1        # builds wrapper + tests
# or
dotnet build ADLXWrapper/ADLXWrapper.csproj
```

## Usage snapshot
```csharp
using ADLXWrapper;
using System;
using System.Linq;

// Initialize ADLX
using var adlx = ADLXApi.Initialize();
var systemServices = adlx.GetSystemServices();

// Enumerate GPUs
var gpus = ADLXGpuHelpers.EnumerateAllGpus(systemServices).ToList();
Console.WriteLine($"Found {gpus.Count} GPU(s).");

foreach (var gpuInfo in gpus)
{
    Console.WriteLine($"- GPU: {gpuInfo.Name} (ID: {gpuInfo.UniqueId})");
    Console.WriteLine($"  VRAM: {gpuInfo.TotalVRAM} MB ({gpuInfo.VRAMType})");
}

// Enumerate Displays
var displays = ADLXDisplayHelpers.EnumerateAllDisplays(systemServices).ToList();
Console.WriteLine($"\nFound {displays.Count} display(s).");

foreach (var displayInfo in displays)
{
    Console.WriteLine($"- Display: {displayInfo.Name} on GPU {displayInfo.GpuUniqueId}");
    Console.WriteLine($"  Resolution: {displayInfo.Width}x{displayInfo.Height} @ {displayInfo.RefreshRate:F2} Hz");
}
```

More helpers:
- `ADLXDisplayHelpers` / `ADLXDisplayInfo` – enumerate displays and read properties
- `ADLXPerformanceMonitoringHelpers` / `ADLXPerformanceMonitoringInfo` – metrics/support queries
- `ADLXGPUTuningHelpers` / `ADLXGPUTuningInfo` – check tuning capabilities (read-only)
- `ADLXListHelpers` – convert ADLX list interfaces to arrays

## Regenerating bindings (optional)
```powershell
cd ADLXWrapper
ClangSharpPInvokeGenerator @ClangSharpConfig.rsp
```
Generated files land in `cs_generated/` and are excluded from source control.
