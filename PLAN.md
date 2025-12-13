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
