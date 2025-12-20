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

## Usage snapshot (current helper surface)
```csharp
using ADLXWrapper;
using System;

// Initialize ADLX (ADLXApiHelper owns DLL + system lifetime)
using var adlx = ADLXApiHelper.Initialize();

// Wrap system services (AddRef'd; dispose before disposing adlx)
using var system = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());

// Display helpers / façades
using var displayServices = new ADLXDisplayServicesHelper(
    system.GetDisplayServicesNative(),
    system.GetDesktopServicesNative());

foreach (var display in displayServices.EnumerateAdlxDisplays())
using (display) // AdlxDisplay is IDisposable
{
    Console.WriteLine($"Display {display.Name} on GPU {display.GpuUniqueId}: " +
                      $"{display.Width}x{display.Height} @ {display.RefreshRate:F2} Hz");

    // Grab the owning GPU façade if needed
    using var gpu = display.GetGpu();
    Console.WriteLine($"  GPU: {gpu.Name} ({gpu.VRAMType}, {gpu.TotalVRAM} MB)");
}
```

Notes and contracts
- Error handling: non-OK ADLX calls throw `ADLXException`; capability gaps surface as `ADLX_NOT_SUPPORTED`. Post-dispose calls throw `ObjectDisposedException`.
- Interface selection: helpers pick the highest available ADLX interface version before failing with `ADLX_NOT_SUPPORTED`.
- Ownership: helpers/façades AddRef native pointers; always `Dispose()` them. Do not manually Release pointers owned by a `ComPtr`.
- Events: listener callbacks are invoked on ADLX threads. Keep the returned listener handle alive and dispose (or call the corresponding remove helper) to unsubscribe.

## Regenerating bindings (optional)
```powershell
cd ADLXWrapper
ClangSharpPInvokeGenerator @ClangSharpConfig.rsp
```
Generated files land in `cs_generated/` and are excluded from source control.
