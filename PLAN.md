﻿## ADLXWrapper expansion plan

### Phase 1: Core Feature Implementation (Complete)

1.  [x] **Display Settings (get/set)**: Implemented color depth/format, scaling, FreeSync, etc.
2.  [x] **Desktop/Eyefinity**: Implemented grid/layout reading, change handling, and create/destroy wrappers.
3.  [x] **3D Settings**: Implemented full get/apply support for Anti-Lag, Boost, RIS, etc.
4.  [x] **Event Handling**: Added listeners for Display, Desktop, and GPU Tuning changes.
5.  [x] **API Polish**: Refactored into a clean, helper-based architecture.

### Phase 2: Next Steps

- [ ] Create comprehensive sample applications demonstrating save/load of full system state.
- [ ] Expand test coverage for `Apply` methods.
- [ ] Document advanced use cases like event handling.

### Phase 3: Interface version gating

- [x] Wire DisplayServices v1/v2 gating in helpers/facades (blanking via DS1; connectivity via DS2; retain DS3-only FSCA/DRRC).
- [x] Run regression tests after gating changes (current run skipped on non-AMD hardware; DLL missing on host).

### Phase 4: Facade expansion (Complete)

- [x] Stage 1 – Scope inventory: Catalog remaining non-facade helpers/raw entry points (GPU tuning, performance monitoring, multimedia, power tuning, 3D settings, logging/system init variants). Note call sites in samples/tests to quantify migration.
	- Raw helper surfaces to replace with facades: ADLXGPUTuningHelpers (manual/preset/auto tuning, fan/VRAM/power tuning; event listeners stubbed), ADLXPowerTuningHelpers (SmartShift Max/Eco, manual power/TDC), ADLXPerformanceMonitoringHelpers (metrics support/current/history, sampling config), ADLXMultimediaHelpers (video upscale/VSR), ADLX3DSettingsHelpers (anti-lag/boost/RIS/ESync/VSync/FRTC/AA/AF/Tessellation). Legacy DTO helpers still present: ADLXGpuHelpers, ADLXDisplayHelpers, ADLXDesktopHelpers. System logging/init facade absent.
	- Call sites using helpers: Samples (PerfMonitoringSample, MultimediaSample, PowerTuningSample) and tests (GpuTuningServicesTests, PerformanceMonitoringServicesTests, MultimediaServicesTests, PowerTuningServicesTests, ADLX3DSettingsTests, ResourceSafetyTests) rely on helpers + raw handles.
- [x] Stage 2 – Surface design: Define facade types and naming aligned with existing conventions (Profile naming, legacy handle escape hatches). Decide gating/error propagation per area (QueryInterface, ADLX_NOT_SUPPORTED/BAD_VER pass-through).
	- GPU tuning: `AdlxGpuTuning` facade from `ADLXSystemServices.GetGpuTuningProfile(AdlxInterfaceHandle gpu)`; profiles via `GpuTuningProfile GetProfile()` / `ApplyProfile(GpuTuningProfile profile)`; feature-level profiles `ManualFanProfile`, `ManualVramProfile`, `ManualGfxProfile`, `ManualPowerProfile`, `AutoTuningProfile`, `PresetTuningProfile`; legacy escape hatch `GetGpuTuningServicesHandle()`. Gate via QueryInterface to v1/v2; skip unsupported, propagate ADLX errors when present interface fails.
	- Performance monitoring: `AdlxPerformanceMonitor` from `ADLXSystemServices.GetPerformanceMonitoringProfile()`; config profile `PerformanceMonitoringProfile` (sampling interval, history size) with `GetProfile`/`ApplyProfile`; metric APIs `GetGpuMetricsSupport`, `GetCurrentGpuMetrics`, `EnumerateGpuMetricsHistory`, `GetCurrentSystemMetrics`, `GetCurrentAllMetrics`; handle escape `GetPerformanceMonitoringServicesHandle()`.
	- Multimedia: `AdlxMultimedia` from `ADLXSystemServices.GetMultimediaProfile(AdlxInterfaceHandle gpu)`; `MultimediaProfile` contains `VideoUpscaleProfile` and `VideoSuperResolutionProfile`; `GetProfile`/`ApplyProfile`; handle escape `GetMultimediaServicesHandle()`.
	- Power tuning: `AdlxPowerTuning` from `ADLXSystemServices.GetPowerTuningProfile()`; `SmartShiftProfile` (Max/Eco) and per-GPU `ManualPowerProfile` (power/TDC) with `GetProfile`/`ApplyProfile`; QueryInterface to v1 for TDC/Eco; handle escape `GetPowerTuningServicesHandle()`.
	- 3D settings: `Adlx3DSettings` from `ADLXSystemServices.Get3DSettingsProfile(AdlxInterfaceHandle gpu)`; `ThreeDSettingsProfile` (AntiLag, Boost, RIS, EnhancedSync, WaitForVerticalRefresh, FrameRateTargetControl, AntiAliasing, AnisotropicFiltering, Tessellation) with `GetProfile`/`ApplyProfile`; handle escape `Get3DSettingsServicesHandle()`.
	- Logging/init: add `ADLXApi.EnableLog(LogProfile profile)` and `DisableLog()` to wrap existing logging entry points; ensure consistency with DLL load/terminate lifecycle.
	- DTOs: all profiles JSON-serializable; no unmanaged pointers; include support flags where relevant; apply methods skip unsupported features and can return/emit skip info later if needed.
 [x] Stage 3 – API shape draft: Sketch C# types and primary methods/properties per facade (profiles, support queries, apply patterns, JSON DTOs, ComPtr ownership rules).
	 - [x] Public facade entrypoints on ADLXSystemServices: `GetGpuTuningServices()`, `GetPerformanceMonitoringServices()`, `GetMultimediaServices()`, `GetPowerTuningServices()`, `Get3DSettingsServices()` returning facades; raw access via `Get...ServicesHandle()`.
	 - [x] Facade classes and lifetimes:
		  - `AdlxGpuTuning` (holds `ComPtr<IADLXGPUTuningServices>` and GPU handle `ComPtr<IADLXGPU>`; owner `ADLXApi`). Methods: `GpuTuningProfile GetProfile()`, `void ApplyProfile(GpuTuningProfile profile)`, feature accessors `GetManualFanProfile()/ApplyManualFanProfile`, `GetManualVramProfile()/ApplyManualVramProfile`, `GetManualGfxProfile()/ApplyManualGfxProfile`, `GetManualPowerProfile()/ApplyManualPowerProfile`, `GetAutoTuningProfile()/ApplyAutoTuningProfile`, `GetPresetTuningProfile()/ApplyPresetTuningProfile`. Escape: `GetGpuTuningServicesHandle()`.
		  - `AdlxPerformanceMonitor` (holds `ComPtr<IADLXPerformanceMonitoringServices>`). Methods: `PerformanceMonitoringProfile GetProfile()`, `void ApplyProfile(PerformanceMonitoringProfile profile)`, `GpuMetricsSupport GetGpuMetricsSupport(AdlxInterfaceHandle gpu)`, `GpuMetricsSnapshot GetCurrentGpuMetrics(AdlxInterfaceHandle gpu)`, `IReadOnlyList<GpuMetricsSnapshot> EnumerateGpuMetricsHistory(...)`, `SystemMetricsSnapshot GetCurrentSystemMetrics()`, `AllMetricsSnapshot GetCurrentAllMetrics()`. Escape: `GetPerformanceMonitoringServicesHandle()`.
		  - `AdlxMultimedia` (holds `ComPtr<IADLXMultimediaServices>` and GPU handle). Methods: `MultimediaProfile GetProfile()`, `void ApplyProfile(MultimediaProfile profile)`; subprofiles `VideoUpscaleProfile`, `VideoSuperResolutionProfile`. Escape: `GetMultimediaServicesHandle()`.
		  - `AdlxPowerTuning` (holds `ComPtr<IADLXPowerTuningServices>`; may QueryInterface to v1). Methods: `PowerTuningProfile GetProfile()`, `void ApplyProfile(PowerTuningProfile profile)`; subprofiles `SmartShiftProfile` (Max/Eco), optional per-GPU `ManualPowerProfile` (power/TDC) accessed via GPU argument or via composition with `AdlxGpuTuning`. Escape: `GetPowerTuningServicesHandle()`.
		  - `Adlx3DSettings` (holds `ComPtr<IADLX3DSettingsServices>` and GPU handle). Methods: `ThreeDSettingsProfile GetProfile()`, `void ApplyProfile(ThreeDSettingsProfile profile)`; fields: AntiLag, Boost, RIS, EnhancedSync, WaitForVerticalRefresh, FrameRateTargetControl, AntiAliasing, AnisotropicFiltering, Tessellation. Escape: `Get3DSettingsServicesHandle()`.
	 - [x] Profile DTOs: JSON-serializable shapes with support flags; include per-feature support booleans/ranges; no unmanaged pointers. Apply methods skip unsupported; when interface exists but call fails, propagate ADLXException. GPU tuning profile captures auto/preset/manual (gfx/vram/fan/power), SmartShift (if present), with ranges where needed. Performance monitoring profile captures sampling interval/history size. Multimedia profile captures enabled + sharpness for upscale, enabled for VSR. Power tuning captures bias mode/value and Eco enabled; optional per-GPU manual power/TDC. 3D settings profile captures the existing info structs.
	 - [x] Ownership/lifetime rules: facades keep owner `ADLXApi`; AddRef on service/GPU pointers; Dispose releases; throw ObjectDisposedException on use-after-dispose or when owner disposed.
	 - [x] Gating: attempt QueryInterface for newer interfaces/versions; if unavailable, mark unsupported and skip during apply; keep handle escape hatches for advanced use.
 - [ ] Stage 4 – Implementation: Build facades per area (GPU tuning, perf monitoring, multimedia, power tuning, 3D settings, logging). Implement profile capture/apply with skip-on-unsupported semantics and RAII/disposal safety.
	 - [x] Add facades and system entrypoints for performance monitoring, GPU tuning, multimedia, power tuning, and 3D settings (handles + acquisition helpers).
	 - [x] Wire ADLXApi convenience accessors if desired and ensure disposal guards across new types.
	 - [x] Add manual power/TDC profile flow (GPU-level) if kept separate from power tuning facade.
	 - [x] Logging facade:
		 - [x] Add LogProfile DTO (destination, severity, file path, optional managed sink shim) and keep it JSON-serializable.
		 - [x] Add ADLXApi.EnableLog(LogProfile) and DisableLog wrappers (mirrored on ADLXSystemServices); guard disposal and propagate ADLXException.
		 - [x] Test/verification notes: file logging happy path, disable path, dispose guard; optional callback sink if ADLX invokes it on this host.
- [ ] Stage 5 – Samples/tests: Update samples to facades; expand tests for new surfaces (support gating, profile round-trips, disposal guards). Ensure non-AMD skip logic remains intact.
	- [ ] Tests migration (current focus):
		- Targets: PerformanceMonitoringServicesTests, MultimediaServicesTests, PowerTuningServicesTests, GpuTuningServicesTests, ADLX3DSettingsTests, ResourceSafetyTests.
		- Success criteria: use facades/AdlxInterfaceHandle instead of raw pointers/helpers; preserve skip guards for AMD hardware and DLL presence; ensure using/Dispose on all handles; keep assertions meaningful (support/metrics values read, not modifying hardware).
		- Out of scope for now: sample refactors and helper deprecation—track separately after tests land.
- [ ] Stage 6 – Docs/verification: Refresh READMEs with new quick-starts; run `dotnet build ADLXWrapper/ADLXWrapper.csproj` and `dotnet test ADLXWrapper.Tests/ADLXWrapper.Tests.csproj` (skip-aware); ensure no generated file edits.

### Future things to fix

- Auto-tuning profile apply: auto-tuning is captured in `GpuTuningProfile` but not applied because no apply helper exists in current helpers. Add apply support when ADLX helper coverage is expanded.
- Align profile support flags and skip semantics across facades: add per-feature support markers to GPU tuning/power/multimedia/3D profiles to match display/desktop style and skip unsupported apply steps.
- ApplyProfile parity: ensure new facades gate via QueryInterface, skip unsupported features, and throw only on ADLX errors (mirror display/desktop behavior).
- Manual power/TDC flow: design and implement GPU-level manual power/TDC profile handling (or decide to drop it) and document the choice.
- Legacy helpers: mark legacy helper classes (ADLXGpuTuningHelpers, ADLXPerformanceMonitoringHelpers, ADLXMultimediaHelpers, ADLXPowerTuningHelpers, ADLX3DSettingsHelpers) as obsolete or retire them after migration to facades.

### Display/Desktop facade & profile plan

- Remove/replace legacy DTOs: `DisplayInfo`, `DesktopInfo`, and their enumeration helpers will be removed or marked obsolete; callers should migrate to facades/profiles. Files to update: `ADLXDisplayHelpers.cs`, `ADLXDesktopHelpers.cs`, samples, tests.
- Public API names (lock-in):
	- `ADLXApi.GetSystemServices()` → `ADLXSystemServices`
	- `ADLXSystemServices.EnumerateAllDisplays()` → `IReadOnlyList<AdlxDisplay>`
	- `ADLXSystemServices.EnumerateAllDesktops()` → `IReadOnlyList<AdlxDesktop>`
	- `ADLXSystemServices.ApplyDisplayProfile(DisplayProfile)` / `ApplyDesktopProfile(DesktopProfile)`
	- `AdlxDisplay`: identity properties, per-feature getters/setters, `GetProfile()`, `ApplyProfile(DisplayProfile)`, `GetDesktop()` (lazy resolve)
	- `AdlxDesktop`: geometry/orientation, `GetDisplays()`, `GetProfile()`, `ApplyProfile(DesktopProfile)`
- Feature coverage to capture/apply in profiles (no pointers, JSON-ready): gamma, gamut, 3DLUT, custom color, connectivity, custom resolutions, color depth, pixel format, FreeSync, VSR, integer scaling, GPU scaling, scaling mode, HDCP, VariBright, blanking, resolution (native/current). Omit others only if unsupported by ADLX APIs.
- Lazy desktop resolution: `AdlxDisplay.GetDesktop()` enumerates desktops via desktop services, traverses member displays, matches by `UniqueId`; cache result, refresh if stale.
- Interface versioning: acquire v1 services, attempt cast to v2/v3 for newer features; if cast fails, mark feature unsupported and skip. Only throw when interface exists but ADLX returns an error.
- Lifecycle/disposal: facades own `ComPtr`s, track parent `ADLXApi` disposal; throw `ObjectDisposedException` on use-after-dispose.
- Profiles JSON contract: Newtonsoft.Json; profiles contain identity (UniqueId, GpuUniqueId, Name, optional EDID) + per-feature state structs; no unmanaged pointers.
- System-level apply: `ApplyDisplayProfile`/`ApplyDesktopProfile` resolve live objects by `UniqueId`, apply supported features, skip unsupported ones; consider returning per-feature results or logging skips.
- Tests/samples: migrate DTO-based samples/tests to facades/profiles; add save/load/apply tests and version-fallback coverage.
- Backward compatibility: decide whether to `[Obsolete]` legacy helpers or remove; current plan favors removal to reduce confusion.

#### Detailed implementation blueprint (for a human programmer)

Public types and methods (names are final):

- `ADLXApi`
	- `ADLXSystemServices GetSystemServices()`

- `ADLXSystemServices`
	- `IReadOnlyList<AdlxDisplay> EnumerateAllDisplays()` — returns live facades, owns `ComPtr` copies.
	- `IReadOnlyList<AdlxDesktop> EnumerateAllDesktops()` — returns live facades, owns `ComPtr` copies.
	- `void ApplyDisplayProfile(DisplayProfile profile)` — resolves display by `UniqueId`, applies supported features, skips unsupported.
	- `void ApplyDesktopProfile(DesktopProfile profile)` — resolves desktop, applies desktop geometry/orientation if applicable, then applies member display profiles by `UniqueId`.

- `AdlxDisplay` (fields: `ComPtr<IADLXDisplay> _display; ComPtr<IADLXDisplayServices> _services; ADLXApi _owner; AdlxDesktop? _cachedDesktop`)
	- Identity properties: `string Name`, `int Width`, `int Height`, `double RefreshRate`, `uint ManufacturerId`, `uint PixelClock`, `ADLX_DISPLAY_TYPE Type`, `ADLX_DISPLAY_CONNECTOR_TYPE ConnectorType`, `ADLX_DISPLAY_SCAN_TYPE ScanType`, `ulong UniqueId`, `int GpuUniqueId`, `string? Edid`.
	- Profile: `DisplayProfile GetProfile()`, `string ToJson()`, `void ApplyProfile(DisplayProfile profile)`.
	- Desktop linkage: `AdlxDesktop GetDesktop()` — lazy resolve via desktop services: enumerate desktops, traverse displays, match `UniqueId`, cache result; re-resolve if cached is disposed.
	- Per-feature getters/setters (all check support, throw `ADLXException` on ADLX failures; return tuples or state structs): `GetGamma/SetGamma(GammaState)`, `GetGamut/SetGamut(GamutState)`, `GetThreeDLUT/SetThreeDLUT(ThreeDLUTState)`, `GetCustomColor/SetCustomColor(CustomColorState)`, `GetConnectivity/SetConnectivity(ConnectivityState)`, `GetCustomResolution/SetCustomResolution(CustomResolutionState)`, `GetColorDepth/SetColorDepth(ColorDepthState)`, `GetPixelFormat/SetPixelFormat(PixelFormatState)`, `GetFreeSyncState()/SetFreeSyncEnabled(bool)`, `GetVsrState()/SetVsrEnabled(bool)`, `GetIntegerScalingState()/SetIntegerScalingEnabled(bool)`, `GetGpuScalingState()/SetGpuScalingEnabled(bool)`, `GetScalingMode()/SetScalingMode(ADLX_SCALE_MODE)`, `GetHdcpState()/SetHdcpEnabled(bool)`, `GetVariBright()/SetVariBright(VariBrightState)`, `GetBlanking()/SetBlanked(bool)`, `GetResolution()/SetResolution(DisplayResolutionState)` where feasible.
	- Disposal: implements `IDisposable`; throws `ObjectDisposedException` if used after dispose or after `_owner` disposed.

- `AdlxDesktop` (fields: `ComPtr<IADLXDesktop> _desktop; ComPtr<IADLXDesktopServices> _desktopServices; ComPtr<IADLXDisplayServices> _displayServices; ADLXApi _owner`)
	- Properties: `ADLX_DESKTOP_TYPE Type`, `int Width`, `int Height`, `int TopLeftX`, `int TopLeftY`, `ADLX_ORIENTATION Orientation`.
	- Composition: `IReadOnlyList<AdlxDisplay> GetDisplays()` — constructs displays with back-reference to this desktop.
	- Profile: `DesktopProfile GetProfile()`, `string ToJson()`, `void ApplyProfile(DesktopProfile profile)` — rebind member displays by `UniqueId` and call their `ApplyProfile`.
	- Disposal: implements `IDisposable`; same object-lifetime rules as display.

Profile DTOs (Newtonsoft.Json, no unmanaged pointers):

- `DesktopProfile`
	- `ADLX_DESKTOP_TYPE Type`, `int Width`, `int Height`, `int TopLeftX`, `int TopLeftY`, `ADLX_ORIENTATION Orientation`.
	- `List<DisplayProfile> Displays` — member display profiles.

- `DisplayProfile`
	- Identity: `ulong UniqueId`, `int GpuUniqueId`, `string Name`, `string? Edid`.
	- Optional: `DisplayResolutionState Resolution` (current/native captured).
	- Feature states: `GammaState`, `GamutState`, `ThreeDLUTState`, `CustomColorState`, `ConnectivityState`, `CustomResolutionState`, `ColorDepthState`, `PixelFormatState`, `FreeSyncState`, `VirtualSuperResolutionState`, `IntegerScalingState`, `GPUScalingState`, `ScalingModeState`, `HdcpState`, `VariBrightState`, `BlankingState`.

State structs (all serializable):
- `GammaState`: preset enum (sRGB/BT709/PQ/PQ2084/G36/CustomCoeff/CustomRamp/Reset), optional `ADLX_RegammaCoeff? Coeff`, optional `ADLX_GammaRamp? Ramp`, `bool Supported`.
- `GamutState`: `ADLX_GAMUT_SPACE? ColorSpace`, `ADLX_WHITE_POINT? WhitePoint`, optional custom white `ADLX_RGB?`, `bool Supported`.
- `ThreeDLUTState`: `ThreeDLUTMode Mode` (Disabled/VividGaming), `int? DynamicContrast`, support flags.
- `CustomColorState`: `int Hue`, `int Saturation`, `int Brightness`, `int Contrast`, `int Temperature`, per-property support flags.
- `ConnectivityState`: `bool? HdmiQualityDetectionEnabled`, `ADLX_DP_LINK_RATE? DpLinkRate`, `int? RelativePreEmphasis`, `int? RelativeVoltageSwing`, support flags.
- `CustomResolutionState`: `List<DisplayResolutionState> Resolutions`, `bool Supported`.
- `DisplayResolutionState`: `int Width`, `int Height`, `int RefreshRate`.
- `ColorDepthState`: `ADLX_COLOR_DEPTH? Value`, `bool Supported`.
- `PixelFormatState`: `ADLX_PIXEL_FORMAT? Value`, `bool Supported`.
- `FreeSyncState`: `bool? Enabled`, `bool Supported`.
- `VirtualSuperResolutionState`: `bool? Enabled`, `bool Supported`.
- `IntegerScalingState`: `bool? Enabled`, `bool Supported`.
- `GPUScalingState`: `bool? Enabled`, `bool Supported`.
- `ScalingModeState`: `ADLX_SCALE_MODE? Mode`, `bool Supported`.
- `HdcpState`: `bool? Enabled`, `bool Supported`.
- `VariBrightState`: `bool? Enabled`, `VariBrightMode Mode`, `bool Supported`.
- `BlankingState`: `bool? Blanked`, `bool Supported`.

Lazy desktop resolution algorithm (`AdlxDisplay.GetDesktop()`): enumerate desktops via desktop services; for each desktop enumerate displays; compare `UniqueId`; if match, return an `AdlxDesktop` facade; cache for future; if cache is disposed, re-resolve.

Interface versioning pattern: always acquire base `IADLXDisplayServices`; try cast to v2/v3 where needed (FreeSync color accuracy, DRR, etc.). If cast fails, mark feature unsupported and skip during profile apply; setters throw only when the required interface is present but ADLX returns an error.

Error/validation rules: use support queries/ranges before set; skip unsupported fields in `ApplyProfile`; log/collect per-feature skips (optional return structure) — decide minimal behavior (skip silently vs. optional callback/log).

Lifecycle: facades own `ComPtr`s; disposing a facade releases the handle; using after dispose or after owning `ADLXApi` disposal throws `ObjectDisposedException`.

Migration steps:
- Remove or obsolete `DisplayInfo`/`DesktopInfo` and related enumeration methods in `ADLXDisplayHelpers.cs` and `ADLXDesktopHelpers.cs`.
- Refactor samples to use `AdlxDisplay`/`AdlxDesktop` and profiles (`GetProfile()/ApplyProfile/ToJson`).
- Update tests to cover: enumeration returns facades, save/load/apply profile, unsupported feature skip, lazy desktop resolution, disposal guards, version fallback.

Backward compatibility decision: default stance is removal of legacy DTO helpers to avoid dual APIs; if kept, mark `[Obsolete]` and document they do not support settings apply.
