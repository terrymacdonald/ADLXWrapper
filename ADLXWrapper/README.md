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

## Usage snapshot (facades)
```csharp
using ADLXWrapper;
using System.Linq;

// Initialize ADLX
using var adlx = ADLXApi.Initialize();
var system = adlx.GetSystemServicesFacade();

// Enumerate displays via facades
var displays = system.EnumerateAllDisplays();
foreach (var d in displays)
{
    using (d)
    {
        var profile = d.GetProfile();
        d.ApplyProfile(profile, skip => System.Console.WriteLine($"skip: {skip}")); // safe: re-applies current state
        System.Console.WriteLine($"Display {d.Name} {profile.Width}x{profile.Height} @ {profile.RefreshRate}Hz");
    }
}

// System-level apply (resolve by UniqueId)
// system.ApplyDisplayProfile(profile);
```

Key helpers:
- `ADLXSystemServices` – enumerates displays/desktops as `AdlxDisplay`/`AdlxDesktop` facades; exposes capabilities/version.
- `AdlxDisplay` – identity + per-feature getters/setters (gamma, gamut, 3DLUT, custom color, connectivity, resolutions, color depth, pixel format, FreeSync, VSR, integer scaling, GPU scaling, scaling mode, HDCP, VariBright, blanking, resolution). Profiles are JSON-serializable.
- `AdlxDesktop` – geometry/orientation, members, profile capture/apply.
- `ADLXPerformanceMonitoringHelpers` – metrics/support queries.
- `ADLXGPUTuningHelpers` – tuning capability inspection (read-only).

## Regenerating bindings (optional)
```powershell
cd ADLXWrapper
ClangSharpPInvokeGenerator @ClangSharpConfig.rsp
```
Generated files land in `cs_generated/` and are excluded from source control.

## Version gating and skips
- Capabilities are exposed via `ADLXSystemServices.Capabilities` (e.g., `SupportsDisplayServices3`).
- Display apply methods accept an optional skip callback to surface skipped features when newer interfaces are unavailable.
- Always guard test/sample runs with `ADLXHardwareDetection.HasAMDGPU` and `ADLXApi.IsADLXDllAvailable` to skip cleanly on non-AMD systems.

## Events
- For display events, obtain a display-services handle via `GetDisplayServicesHandle()` on the facade, then register listeners through `IADLXDisplayChangedHandling`.
