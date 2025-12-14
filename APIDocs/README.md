# ADLXWrapper API Docs

## Overview
- Facade-first C# bindings over ADLX with vtable interop; generated types live in [ADLXWrapper/cs_generated](ADLXWrapper/cs_generated) and facades/RAII helpers in [ADLXWrapper](ADLXWrapper).
- Typical flow: initialize once, fetch system services, work through facades, dispose to terminate ADLX and unload the DLL.
- Profiles capture current state and apply it back safely; unsupported features are skipped rather than failing hard.
- Tests and samples are skip-safe on non-AMD systems or when `amdadlx64.dll` is missing.

## Initialization & Lifetime
- Prefer `ADLXApi.Initialize()` for standard bring-up; `InitializeWithCallerAdl` is available for ADL interop (see [ADLXWrapper/ADLXApi.cs](ADLXWrapper/ADLXApi.cs)).
- Objects throw `ObjectDisposedException` after disposal; `ADLXApi.Dispose` terminates ADLX then unloads the DLL.
- Check availability before use: `ADLXApi.IsADLXDllAvailable(out var reason)` and `ADLXHardwareDetection.HasAMDGPU` (see [ADLXWrapper/ADLXHardwareDetection.cs](ADLXWrapper/ADLXHardwareDetection.cs)).
- Use `GetSystemServicesProfile()` to acquire the primary facade; raw handles remain available via `Get...Handle()` escape hatches.

## Facades & Entry Points
- System: `ADLXSystemServices` (enumerates displays/desktops; access to performance monitoring, GPU tuning, multimedia, power tuning, and 3D settings). Defined in [ADLXWrapper/ADLXSystemServices.cs](ADLXWrapper/ADLXSystemServices.cs).
- Display: `AdlxDisplay` (identity, gamma/gamut/3DLUT, custom color, connectivity, resolutions, color depth, pixel format, FreeSync, VSR, integer scaling, GPU scaling, scaling mode, HDCP, VariBright, blanking, resolution). Profiles skip unsupported features. See [ADLXWrapper/AdlxDisplay.cs](ADLXWrapper/AdlxDisplay.cs).
- Desktop: `AdlxDesktop` (geometry/orientation, members, profile capture/apply). See [ADLXWrapper/AdlxDesktop.cs](ADLXWrapper/AdlxDesktop.cs).
- Performance monitoring: `AdlxPerformanceMonitor` (sampling config profile, current metrics, history, support). See [ADLXWrapper/ADLXPerformanceMonitoringHelpers.cs](ADLXWrapper/ADLXPerformanceMonitoringHelpers.cs).
- GPU tuning: `AdlxGpuTuning` (auto/preset/manual tuning profiles, per-feature apply). See [ADLXWrapper/ADLXGpuTuningHelpers.cs](ADLXWrapper/ADLXGpuTuningHelpers.cs).
- Multimedia: `AdlxMultimedia` (video upscale, video super resolution profiles). See [ADLXWrapper/ADLXMultimediaHelpers.cs](ADLXWrapper/ADLXMultimediaHelpers.cs).
- Power tuning: `AdlxPowerTuning` (SmartShift Max/Eco, manual power/TDC where supported). See [ADLXWrapper/ADLXPowerTuningHelpers.cs](ADLXWrapper/ADLXPowerTuningHelpers.cs).
- 3D settings: `Adlx3DSettings` (Anti-Lag, Boost, RIS, Enhanced Sync, VSync, FRTC, AA/AF, Tessellation). See [ADLXWrapper/ADLX3DSettingsHelpers.cs](ADLXWrapper/ADLX3DSettingsHelpers.cs).

## Profiles & Apply Semantics
- Each facade exposes `GetProfile()` / `ApplyProfile(profile)`; profiles are JSON-serializable DTOs (no unmanaged pointers).
- Apply is skip-safe: features missing or unsupported on the current interface are skipped; ADLX errors still raise `ADLXException`.
- Display apply accepts an optional skip callback to surface skipped features.
- Version gating is on-demand: attempts `QueryInterface` for newer interfaces; propagates `ADLX_NOT_SUPPORTED` / `ADLX_BAD_VER` as appropriate.

## Logging
- Configure via `ADLXApi.EnableLog(LogProfile)` with destinations: local file, debug view, or managed sink; disable via `DisableLog()` (see [ADLXWrapper/ADLXApi.cs](ADLXWrapper/ADLXApi.cs) and [ADLXWrapper/LogProfile.cs](ADLXWrapper/LogProfile.cs)).
- Local file requires `FilePath`; application sink requires a managed sink delegate; debug view requires neither.

## Events
- Display events: obtain display-services handle from the facade, then register listeners through `IADLXDisplayChangedHandling` (pattern mirrored in [ADLXWrapper/README.md](ADLXWrapper/README.md)).
- GPU tuning and multimedia change handling follow similar listener acquisition via their respective services.

## Build & Test
- Visual Studio 2026: open `ADLXWrapper.sln`, restore, build, and run tests in Test Explorer (tests skip on non-AMD or missing `amdadlx64.dll`).
- VS Code / dotnet CLI: `./prepare_adlx.ps1`, `./build_adlx.ps1`, optional `dotnet build Samples/ADLXWrapper.Samples.sln`, tests via `dotnet test ADLXWrapper.Tests/ADLXWrapper.Tests.csproj`.

## Samples & Skip Behavior
- Samples live under [Samples](Samples) and use facades exclusively; they are runtime skip-safe on non-AMD hardware or when the ADLX DLL is absent.
- Tests in [ADLXWrapper.Tests](ADLXWrapper.Tests) auto-skip in the same conditions; a full pass requires an AMD GPU with ADLX installed.
