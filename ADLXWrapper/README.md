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

### Desktop profile snapshot
```csharp
using ADLXWrapper;
using System.Linq;

using var adlx = ADLXApi.Initialize();
var system = adlx.GetSystemServicesFacade();

var desktop = system.EnumerateAllDesktops().FirstOrDefault();
if (desktop != null)
{
    using (desktop)
    {
        var profile = desktop.GetProfile();
        desktop.ApplyProfile(profile); // re-applies current layout safely
    }
}
```

### Display events snapshot
```csharp
using ADLXWrapper;

unsafe
{
    using var adlx = ADLXApi.Initialize();
    var system = adlx.GetSystemServicesFacade();

    using var dispServices = system.GetDisplayServicesHandle();

    IADLXDisplayChangedHandling* handlingPtr = null;
    if (dispServices.As<IADLXDisplayServices>()->GetDisplayChangedHandling(&handlingPtr) != ADLX_RESULT.ADLX_OK || handlingPtr == null)
        return;

    using var handling = AdlxInterfaceHandle.From(handlingPtr, addRef: false);
    var listener = DisplaySettingsListenerHandle.Create(evt => { System.Console.WriteLine($"Display event: 0x{evt.ToInt64():X}"); return true; });

    try
    {
        handling.As<IADLXDisplayChangedHandling>()->AddDisplaySettingsEventListener((IADLXDisplaySettingsChangedListener*)listener.DangerousGetHandle());
        System.Console.ReadLine();
    }
    finally
    {
        handling.As<IADLXDisplayChangedHandling>()->RemoveDisplaySettingsEventListener((IADLXDisplaySettingsChangedListener*)listener.DangerousGetHandle());
        listener.Dispose();
    }
}
```

Key helpers:
- `ADLXSystemServices` – enumerates displays/desktops as `AdlxDisplay`/`AdlxDesktop` facades; exposes capabilities/version.
- `AdlxDisplay` – identity + per-feature getters/setters (gamma, gamut, 3DLUT, custom color, connectivity, resolutions, color depth, pixel format, FreeSync, VSR, integer scaling, GPU scaling, scaling mode, HDCP, VariBright, blanking, resolution). Profiles are JSON-serializable.
- `AdlxDesktop` – geometry/orientation, members, profile capture/apply.
- `ADLXPerformanceMonitoringHelpers` – metrics/support queries.
- `ADLXGPUTuningHelpers` – tuning capability inspection (read-only).

Legacy helpers:
- Static helper classes (e.g., `ADLXDisplayHelpers`, `ADLXDesktopHelpers`) remain for compatibility but are considered legacy; prefer the facade types. Samples/tests use facades exclusively.

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

### Capability matrix (DisplayServices3 = DS3)
| Area | Facade surface | Requires DS3 for apply? |
| --- | --- | --- |
| Connectivity (HDMI quality, DP link rate, voltage swing) | `AdlxDisplay.GetConnectivity()` / `ApplyProfile.Connectivity` | Yes |
| Color depth | `AdlxDisplay.GetColorDepth()` / `ApplyProfile.ColorDepth` | Yes |
| Pixel format | `AdlxDisplay.GetPixelFormat()` / `ApplyProfile.PixelFormat` | Yes |
| FreeSync | `AdlxDisplay.GetFreeSyncState()` / `ApplyProfile.FreeSync` | Yes |
| VSR | `AdlxDisplay.GetVsrState()` / `ApplyProfile.Vsr` | Yes |
| Integer scaling | `AdlxDisplay.GetIntegerScalingState()` / `ApplyProfile.IntegerScaling` | Yes |
| GPU scaling | `AdlxDisplay.GetGpuScalingState()` / `ApplyProfile.GpuScaling` | Yes |
| Scaling mode | `AdlxDisplay.GetScalingMode()` / `ApplyProfile.ScalingMode` | Yes |
| HDCP | `AdlxDisplay.GetHdcpState()` / `ApplyProfile.Hdcp` | Yes |
| VariBright | `AdlxDisplay.GetVariBright()` / `ApplyProfile.VariBright` | Yes |
| Blanking | `AdlxDisplay.GetBlanking()` / `ApplyProfile.Blanking` | Yes |
| Gamma, gamut, 3DLUT, custom color, custom resolution | `AdlxDisplay` getters/setters | No (DS2 sufficient) |
| Desktop geometry/profile | `AdlxDesktop` | No |

## Events
- For display events, obtain a display-services handle via `GetDisplayServicesHandle()` on the facade, then register listeners through `IADLXDisplayChangedHandling`.

## Testing on AMD hardware
- Run from repo root: `dotnet test ADLXWrapper.Tests/ADLXWrapper.Tests.csproj`.
- Tests auto-skip on non-AMD systems or when `amdadlx64.dll` is unavailable; a fully passing run requires an AMD GPU with ADLX installed.
- Samples can be built with `dotnet build Samples/ADLXWrapper.Samples.sln`; runtime behavior that touches hardware requires AMD drivers.
