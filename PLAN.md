﻿## ADLXWrapper refactor plan

Goal: redesign the helper surface to align with the new helper naming, full ADLX coverage, hardware/driver capability checks, flattened feature exposure, and clear testing/doc updates. This plan is staged to allow review before code changes.

### Stage 0: Baseline and assumptions (context only)
- Source of truth: ADLX SDK headers under ADLX/SDK/Include and ClangSharp-generated bindings in ADLXWrapper/cs_generated.
- Versioning: ADLXHelper.h exports packed version (MAJOR=1, MINOR=4, RELEASE=0, BUILD=110 at last import) and initialization entry points (ADLXInitialize/ADLXInitializeWithCallerAdl/ADLXTerminate/ADLXQueryFullVersion).
- Always-available primitives: system services acquisition, GPU/display/desktop identity enumeration, basic lists (IADLXGPUList, IADLXDisplayList, IADLXDesktopList), basic GPU info (name/IDs/VRAM/product), display identity (EDID, native mode, connector, scan type, pixel clock, refresh, uniqueId), desktop geometry/type, system version strings.
- Optional/feature-gated knobs (require IsSupported or capability query):
	- Display: FreeSync, VSR, GPU scaling, scaling mode, integer scaling, color depth, pixel format, custom color, HDCP, custom resolution, Vari-Bright, gamma, gamut, 3DLUT, connectivity, blanking, DRR, FreeSync color accuracy, video sharpness/upscale (multimedia), display events.
	- Desktop: Eyefinity creation/destruction, desktop change listeners.
	- 3D: AntiLag, Chill, Boost, RSR, RIS/ImageSharpening, Enhanced Sync, WaitForVerticalRefresh/VSync, FRTC, AntiAliasing (level/method/mode), Tessellation, ResetShaderCache; 3D change events.
	- GPU tuning: auto tuning (UV/OC GPU/VRAM), manual GFX/VRAM/fan/power/TDC tuning, presets, SmartAccess Memory, memory timing controls, validation helpers, tuning events.
	- Power tuning: SmartShift Max (bias range/set), SmartShift Eco, power on/off control, GPUConnect change flags.
	- Performance monitoring: GPU/system metrics (per capability), history and sampling configuration, NPU/shared memory metrics, change listeners where applicable.
	- Multimedia: VSR/video sharpness, change events.
	- Applications: application list/query via system services; power-off safety checks.

#### Optional vs always-on matrix (draft, refine during implementation)
- Always-on: ADLXInitialize/Terminate, system services, GPU/display/desktop lists and identity getters, version strings, basic GPU info (name/IDs/VRAM/product), display EDID/native timing/connector/scan, desktop geometry/type.
- Likely-optional (guard with IsSupported/capability):
	- Display: FreeSync, VSR, GPU scaling, scaling mode, integer scaling, color depth, pixel format, custom color, HDCP, custom resolution, Vari-Bright, gamma/gamut/3DLUT, connectivity/blanking, DRR, FreeSync color accuracy, video sharpness/upscale.
	- Desktop: Eyefinity grid create/destroy; desktop changed listener registration.
	- 3D: per-feature toggles listed above; 3D change events.
	- GPU tuning: auto tuning modes; manual GFX/VRAM/fan/power/TDC; presets; SAM; memory timings; validation helpers; tuning events.
	- Power tuning: SmartShift Max/Eco; power on/off; GPUConnect change flags.
	- Perf monitoring: each metric group (power, clocks, temps, fan, VRAM, voltage, NPU, shared GPU memory, system power distribution) gated per support object.
	- Multimedia: VSR/video sharpness; multimedia change events.
	- Applications/System: application list presence may vary; power-off controls may be unsupported on some hardware.

### Stage 1: ADLXApi → ADLXApiHelper reshape
- Rename class and restrict public surface to: Dispose, finalizer, GetFullVersion, GetVersion, GetSystemServicesNative, GetSystemServices (AdlxInterfaceHandle), Initialize, InitializeWithCallerAdl, IsADLXDllAvailable.
- Preserve DLL load semantics (LoadLibraryEx search flags, ADLXNative.GetDllName consistency), delegate binding, ADLXInitialize/Terminate invocation order, and ObjectDisposedException guarding after dispose.
- Remove all per-feature service getters from this type; document where those move (system services helper).
- Acceptance: compiles without callers needing feature-specific getters; initialization/dispose tests remain green; version queries still work.

### Stage 2: ADLXSystemServices → ADLXSystemServicesHelper reshape
- Combine existing system services responsibilities with per-feature dual accessors: Get<Feature>() → AdlxInterfaceHandle façade; Get<Feature>Native() → IADLX<Feature>Services*.
- Cover all SDK services: 3DSettings, GPUTuning, Desktop, Display, Multimedia, PerformanceMonitoring, PowerTuning, Applications/System power control (as exposed), and any future service exposed by IADLXSystem.
- Define caching/ownership rules: keep underlying ComPtr<T> for system services; return handles that do not outlive the parent; no double-release when wrapped.
- Error contract: every accessor exists; if unsupported on hardware/driver, throw ADLX_NOT_SUPPORTED (never null/empty). Guard disposed instances with ObjectDisposedException; map other ADLX errors to ADLXException.
- System native access: GetSystemServicesNative() returns the highest available interface (System2 if present, else System1, else base). Add version-specific helpers only if a caller needs an explicit interface version; otherwise prefer the highest level.
- Acceptance: all feature helpers obtainable; unsupported surfaces as ADLX_NOT_SUPPORTED; disposal releases ComPtrs and throws after dispose.

### Stage 3: Per-feature service helpers (ADLX<Feature>ServicesHelper)
- Rename each service helper to ADLX<Feature>ServicesHelper and merge any split helper/service logic.
- Common surface per service (where applicable): Dispose; EnumerateAll<Thing>() returning managed façade list; EnumerateAll<Thing>Native() returning native list pointer (ComPtr-managed); event subscription helpers where SDK defines change events.
- Enforce capability checks only for optional knobs (see Stage 0 matrix). Always-on primitives skip redundant IsSupported.
- Error contract: methods exist for all features; when unsupported, throw ADLX_NOT_SUPPORTED (managed and native). Native getters should choose the highest available interface version (e.g., System2 power tuning else System1) before throwing.
- Per-feature notes:
	- Display: enumerate displays; managed/native display lists; access to sub-features (color depth, pixel format, scaling, FreeSync, VSR, HDR-related where present) via flattened façade methods. Handle display change events.
	- Desktop: enumerate desktops; manage Eyefinity grid create/destroy; desktop change listeners; mappings between displays/desktops; expose GetGPU()/GetGPUNative() from a desktop façade.
	- 3D: expose AntiLag/Chill/Boost/RSR/RIS/EnhancedSync/VSync/FRTC/AA/Tessellation/ResetShaderCache with support checks; 3D change events.
	- GPU Tuning: manual tuning (GFX/VRAM/fan/power/TDC), auto tuning (start/listen), presets, validation helpers, SAM support check, memory timing; GPU tuning change events.
	- Power Tuning: SmartShift Max/Eco, power state controls, GPUConnect change flags.
	- Performance Monitoring: GPU/system metrics snapshots and history; sampling/history configuration; optional NPU/shared-memory metrics; support gating per metric group.
	- Multimedia: VSR/video sharpness range/get/set; multimedia change events.
	- Applications/System: application list access; power-off controls with safety checks.
- Acceptance: each helper exposes both managed and native enumeration, throws ADLX_NOT_SUPPORTED when the underlying capability is absent, and relies solely on generated bindings for native shapes.

#### Stage 3 detailed deliverables per feature
- Display: API map for each optional feature (IsSupported/IsEnabled/Get/Set names), event subscription pattern, and list/ComPtr disposal flow for display lists/items.
- Desktop: Eyefinity capability detection, creation/destroy flows with validation, listener lifecycle (register/unregister), mapping helper between displays and desktops, and GPU accessors (GetGPU()/GetGPUNative()) from desktops/displays.
- 3D: matrix of feature → support check → state/get/set/apply methods; handling of ResetShaderCache; event flag mapping.
- GPU Tuning: capability discovery (per tuning block), state/range/get/set/validate shapes, async auto-tune start/complete listener contract, preset availability map, SAM support surface, memory timing list handling.
- Power Tuning: SmartShift Max bias range/state/set; Eco support/IsActive reason; power control IsSupported; GPUConnect change flags.
- Performance Monitoring: per-metric support flags, snapshot vs history retrieval, history size/sampling setters, optional NPU/shared-mem metrics, disposal of history lists.
- Multimedia: support check and range/state/set for sharpness/VSR, event flags.
- Applications/System: application list availability check, iteration helpers, power-off safety flow (IsPowerOffSupported/StartPowerOff/Abort/IsPowerOffCompleted if provided by API version). GPUs expose EnumerateAllDisplays/EnumerateAllDesktops accessors to relate topology.

### Stage 4: Object flattening and façade shapes
- For each settings-bearing object (Display, Desktop, GPU, Application, System power control, tuning state holders), define flattened methods/properties that inline linked sub-interfaces while keeping disposals safe.
- Naming rules: Get*/Set*/IsSupported*/IsEnabled* aligned to SDK feature names; properties for identity/metadata (always-on) without support checks. Optional features must call IsSupported (or capability object) first and throw ADLX_NOT_SUPPORTED on unsupported hardware/driver.
- Caller contract: callers need not pre-check support; unsupported paths raise ADLX_NOT_SUPPORTED.
- Display façade example set: identity properties (Name, ManufacturerId, UniqueId, Width/Height, RefreshRate, PixelClock, ConnectorType, ScanType, Type, Edid, GpuUniqueId); methods and IsSupported/IsEnabled for FreeSync, VSR, GPU scaling, scaling mode, integer scaling, color depth, pixel format, custom color, HDCP, custom resolution, Vari-Bright, gamma, gamut, 3DLUT, connectivity, blanking, DRR, FreeSync color accuracy, video sharpness/upscale.
- Desktop façade: identity/geometry; Eyefinity grid info; IsSupported/Apply create/destroy; desktop change events hook.
- GPU façade: identity/VRAM/product/LUID/BDF; MGPU mode; SAM support flag; links to applications if exposed; EnumerateAllDisplays/EnumerateAllDesktops to traverse topology; GetDisplayListNative/GetDesktopListNative variants.
- Tuning façades: state/range objects for GFX/VRAM/fan/power/TDC with Validate/IsSupported; auto-tuning status; preset availability; SmartShift bias ranges and eco state with support checks.
- Performance monitoring façades: metric snapshots and history with support flags for each metric group (power, clocks, temps, fan, VRAM, voltage, NPU, shared memory, system metrics/power distribution).
- Acceptance: façade APIs map one-to-one to SDK capabilities, optional features guarded, no duplicate unmanaged releases, and serialization-friendly shapes preserved.

#### Stage 4 per-object checklists
- Display: map each sub-interface (color depth, pixel format, custom color, HDCP, FreeSync, VSR, GPU scaling, scaling mode, integer scaling, custom resolution, Vari-Bright, gamma, gamut, 3DLUT, connectivity, blanking, DRR, FreeSync color accuracy, video sharpness) to façade methods/properties with support guards; ensure GetGPU()/GetDesktop() return façades; handle event origin references.
- Desktop: grid geometry, linked displays, Eyefinity group info; create/destroy flows; surface unique ID; ensure dispose behavior if backed by ComPtr list items.
- GPU: identity, VRAM totals/type, product name, PNP string, MGPU mode, SAM support, LUID/BDF; link to application list when available; optional power control hooks.
- Application: PID/name/path, GPU dependency; list enumeration disposal rules.
- Tuning states: define immutable info/state structs for current, range, and valid sets; include support flags; no direct unmanaged lifetime exposure.
- Metrics snapshots/history: snapshot objects with support markers; history list items disposed after use; conversions to managed types only (no raw pointer leakage).

### Stage 5: Cross-cutting concerns and safeguards
- Error handling: map non-OK ADLX results to ADLXException; translate capability failures to ADLX_NOT_SUPPORTED; guard disposed instances with ObjectDisposedException; keep highest-available-interface selection for native paths.
- ComPtr ownership: every native pointer returned to callers must be wrapped or documented; no manual Release on ComPtr-owned pointers; helpers dispose of lists/items they create.
- Generated bindings: do not hand-edit cs_generated; adjust only helpers and headers/config if generation is needed.
- Threading/events: document that callbacks originate from ADLX threads; ensure delegates are rooted; provide unsubscribe patterns.
- Documentation: update README/usage samples to new helper names and acquisition pattern (Initialize → GetSystemServicesHelper → per-feature helpers).
- Acceptance: clear guidance in docs/tests; build scripts unaffected (prepare/build/test scripts stay valid).

### Stage 6: Validation and rollout
- Tests: extend xUnit to cover new helper names, support-guarded paths (ADLX_NOT_SUPPORTED), dispose-after-use behavior, and representative feature toggles per service. Skip gracefully when hardware/driver support is absent; assert ADLX_NOT_SUPPORTED on unsupported feature calls.
- Samples: update existing Samples/* to use new helper names and flattened façades; add small snippets demonstrating support gating.
- Migration notes: brief section summarizing breaking changes (renames, removed service getters from API) and the new acquisition pattern.

#### Stage 6 validation matrix
- ADLXApiHelper: init/terminate, version queries, dispose guard; IsADLXDllAvailable behavior.
- SystemServicesHelper: dual accessor correctness, disposed guard, ADLX_NOT_SUPPORTED when service missing.
- Per-feature helpers: support-guarded setters return ADLX_NOT_SUPPORTED when unsupported; enumerate APIs yield expected counts; event subscribe/unsubscribe behaves and does not leak delegates.
- Façades: optional feature methods throw ADLX_NOT_SUPPORTED on unsupported hardware; identity properties always accessible; no double-release of ComPtr-wrapped items.
- Performance monitoring: history retrieval/disposal; sampling configuration; support gating per metric.
- Samples: build and run (where hardware allows) using new APIs; documentation references updated names.

### Open points to confirm before implementation
- Optional vs always-on matrix per feature is based on SDK headers; adjust if ADLX version updates change defaults.
- Decide list return types (IList vs IReadOnlyList) for managed enumerations and keep AdlxInterfaceHandle usage consistent across helpers.

### Contract guardrails (applies to all stages)
- Every helper entry point exists; unsupported hardware/driver surfaces as ADLX_NOT_SUPPORTED (via ADLXException) rather than returning null.
- Native accessors pick the highest available ADLX interface version before failing as unsupported.
- After disposal (ComPtr or helper), subsequent calls throw ObjectDisposedException; no manual Release on ComPtr-owned pointers.
