# ADLXWrapper

A modern C# wrapper for AMD's ADLX (AMD Display Library Extensions) using ClangSharp. It exposes ADLX via vtable-based interop for fast, type-safe access from .NET.

## Requirements
- Windows 10/11 x64 with AMD Adrenalin drivers (ADLX provided by `amdadlx64.dll`)
- AMD GPU recommended; APIs that need hardware will skip/fail gracefully if unavailable
- .NET SDK 10.0

## Getting Started

### Clone and prepare
```powershell
git clone https://github.com/terrymacdonald/ADLXWrapper.git
cd ADLXWrapper
.\prepare_adlx.ps1   # downloads/extracts the ADLX SDK to ADLX/
```

### Build
```powershell
.\build_adlx.ps1          # wrapper + tests
# or
dotnet build ADLXWrapper.sln
```

### Test
```powershell
.\test_adlx.ps1
# or
dotnet test ADLXWrapper.Tests/ADLXWrapper.Tests.csproj --verbosity normal
```

## Using ADLXWrapper in your app

Add a reference to the project or DLL, then import the namespace:
```powershell
dotnet add reference ..\ADLXWrapper\ADLXWrapper.csproj
# or add the built ADLXWrapper.dll as a reference
```

```csharp
using ADLXWrapper;
```

### Example 1: Enumerate GPUs and basic info (auto-disposed handles)
```csharp
using var adlx = ADLXApi.Initialize();
using var system = adlx.GetSystemServicesHandle(); // SafeHandle that auto-releases

foreach (var gpu in adlx.EnumerateGPUHandles())
{
    using (gpu)
    {
        var info = ADLXGPUInfo.GetBasicInfo(gpu);
        Console.WriteLine($"{info.Name} | VRAM: {info.TotalVRAM} MB | External: {info.IsExternal}");
    }
}
```

### Example 2: Query displays
```csharp
using var adlx = ADLXApi.Initialize();
using var system = adlx.GetSystemServicesHandle();
foreach (var display in ADLXDisplayHelpers.EnumerateAllDisplayHandles(system))
{
    using (display)
    {
        var d = ADLXDisplayInfo.GetBasicInfo(display);
        Console.WriteLine($"{d.Name} - {d.Width}x{d.Height} @ {d.RefreshRate} Hz");
    }
}
```

### Example 3: Read GPU metrics
```csharp
using var adlx = ADLXApi.Initialize();
var gpus = adlx.EnumerateGPUHandles();
if (gpus.Length > 0)
{
    using var perf = adlx.GetPerformanceMonitoringServicesHandle();
    using (gpus[0])
    {
        var snapshot = ADLXPerformanceMonitoringInfo.GetCurrentMetrics(perf, gpus[0]);
        Console.WriteLine($"Temp: {snapshot.Temperature:F1} C, Usage: {snapshot.Usage:F1}%, Clock: {snapshot.ClockSpeed} MHz");
    }
}
foreach (var gpu in gpus) gpu.Dispose();
```

### Example 4: Check GPU tuning capabilities (read-only)
```csharp
using var adlx = ADLXApi.Initialize();
var gpus = adlx.EnumerateGPUHandles();
if (gpus.Length > 0)
{
    using var tuning = adlx.GetGPUTuningServicesHandle();
    using (gpus[0])
    {
        var caps = ADLXGPUTuningInfo.GetTuningCapabilities(tuning, gpus[0]);
        Console.WriteLine($"Auto: {caps.AutoTuningSupported}, Preset: {caps.PresetTuningSupported}, Manual GFX: {caps.ManualGFXTuningSupported}");
    }
}
foreach (var gpu in gpus) gpu.Dispose();
```

### Example 5: List display names
```csharp
using var adlx = ADLXApi.Initialize();
using var sys = adlx.GetSystemServicesHandle();
foreach (var display in ADLXDisplayHelpers.EnumerateAllDisplayHandles(sys))
{
    using (display)
    {
        var info = ADLXDisplayInfo.GetBasicInfo(display);
        Console.WriteLine(info.Name);
    }
}
```

### Example 6: Save Eyefinity desktop settings (not yet implemented in helpers)
The current helper set does not expose Eyefinity save/restore APIs. If you need this, wrap the corresponding ADLX interfaces and vtable methods for Eyefinity desktop configuration, then follow the same SafeHandle pattern shown above to ensure automatic release.

## Scripts
- `prepare_adlx.ps1` — downloads/extracts the ADLX SDK into `ADLX/`
- `build_adlx.ps1` — builds wrapper + tests (net10.0)
- `test_adlx.ps1` — runs the full test suite; hardware-dependent tests skip when unsupported

## Project Layout
```
ADLXWrapper.sln
├─ ADLX/                    # Downloaded ADLX SDK (prepare_adlx.ps1)
├─ ADLXWrapper/             # Wrapper source
│  ├─ ADLXApi.cs            # Main API entry and lifetime
│  ├─ ADLXVTables.cs        # VTable definitions
│  ├─ ADLXExtensions.cs     # Helper and info utilities (GPU, display, tuning, metrics)
│  └─ cs_generated/         # (optional) ClangSharp-generated bindings
├─ ADLXWrapper.Tests/       # xUnit tests (includes generated struct/layout tests)
│  └─ generated_tests/         # ClangSharp-generated tests that are included in the test build
└─ scripts: prepare_adlx.ps1, build_adlx.ps1, test_adlx.ps1
```

## Notes
- Wrapper targets `net10.0`; ensure the .NET 10 SDK is installed.
- ADLX is provided by the AMD driver (`amdadlx64.dll`); no extra native install is required beyond drivers.
- Helper methods that query hardware will throw or skip if inputs are null; release COM-style interfaces with `ADLXHelpers.ReleaseInterface`.
