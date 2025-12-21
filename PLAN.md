﻿## Facade-first display API cleanup plan

Goal: Make display usage pointer-free for typical callers. `sys.EnumerateDisplays()` should return fully managed `AdlxDisplay` facades, and callers should use methods like `display.GetVirtualSuperResolution()` / `display.SetVirtualSuperResolution(bool)` without touching native pointers or DTOs. Use this pattern to align all display settings, then reuse the approach for other objects (desktops, etc.).

 Guiding principles
- Facade-first: favor `AdlxDisplay` for user-facing operations. Native pointers and DTOs remain for advanced scenarios only.
- Managed collections: `EnumerateDisplays()` returns `IReadOnlyList<AdlxDisplay>` (disposable items); `EnumerateDisplayHandles()` stays for advanced users.
- Simple getters/setters: every display setting provides `Get<Setting>State()` returning a small managed state (support + current values) and `Set<Setting>(...)` for value updates. Naming aligns to the setting shape:
  - Mode-only: `Set<Setting>(ADLX_<MODE>)` (e.g., `SetScalingMode(ADLX_SCALE_MODE)`).
  - Enabled + mode: `Set<Setting>(bool enabled, <ModeEnum> mode)` (e.g., `SetVariBright(bool enabled, VariBrightMode mode)`).
  - Enabled-only: `Set<Setting>(bool enabled)` (e.g., `SetVirtualSuperResolution(bool enabled)`).
  No manual `ComPtr` in normal usage.

Execution steps
1) Reshape `ADLXSystemServicesHelper.EnumerateDisplays()`:
   - Change to return `IReadOnlyList<AdlxDisplay>` (each item constructed with display + display services + desktop services as needed).
   - Keep `EnumerateDisplayHandles()` for advanced/native consumers.
2) Expand `AdlxDisplay` to cover the common display settings with facade methods only (no native exposure required for normal use):
   - Identity remains as properties (Name, Width, Height, RefreshRate, PixelClock, ConnectorType, ScanType, Type, UniqueId, GpuUniqueId).
   - Optional features expose `Get<Setting>State()` (support + current state) and `Set<Setting>(...)`, e.g.:
     - Virtual Super Resolution: `GetVirtualSuperResolution()` -> {IsSupported, IsEnabled}; `SetVirtualSuperResolution(bool enable)`.
     - FreeSync, GPU scaling, scaling mode, integer scaling, pixel format, color depth, HDCP, Vari-Bright (with mode/booleans), custom color, gamma, gamut, 3DLUT, DRR, FreeSync color accuracy, connectivity/blanking, custom resolution apply/list helpers.
   - Remove pointer requirements inside facade: wrap native calls internally; ensure dispose safety.
3) Adjust samples and README to use the facade flow:
   ```csharp
   using var adlx = ADLXApiHelper.Initialize();
   using var sys = adlx.GetSystemServices();
   var displays = sys.EnumerateDisplays();
   foreach (var display in displays) using (display)
   {
       Console.WriteLine($"{display.Name} [{display.Width}x{display.Height}]");
       var vsr = display.GetVirtualSuperResolution();
       if (vsr.IsSupported) display.SetVirtualSuperResolution(true);
   }
   ```
4) Tests: add/update tests to validate facade getters/setters without requiring native handles in test bodies; keep native-path tests separately if needed.
5) Future application: apply the same facade pattern to desktops (e.g., `EnumerateDesktops()` -> `IReadOnlyList<AdlxDesktop>` with feature methods) after display work is complete.
