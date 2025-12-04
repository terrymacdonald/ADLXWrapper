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

using var adlx = ADLXApi.Initialize();
var gpus = adlx.EnumerateGPUs();

foreach (var gpu in gpus)
{
    var info = ADLXGPUInfo.GetBasicInfo(gpu);
    Console.WriteLine($"{info.Name} ({info.VRAMType}) - {info.TotalVRAM} MB");
    ADLXHelpers.ReleaseInterface(gpu);
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
