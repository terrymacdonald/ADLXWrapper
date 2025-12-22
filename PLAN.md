## Facade-first display API cleanup plan

Goal: Make display usage pointer-free for typical callers. `sys.EnumerateDisplays()` should return fully managed `ADLXDisplay` facades, and callers should use methods like `display.GetVirtualSuperResolution()` / `display.SetVirtualSuperResolution(bool)` without touching native pointers. Use this pattern to align all display settings, then reuse the approach for other objects (desktops, etc.).

Guiding principles
- Facade-first: favor `ADLXDisplay` for user-facing operations. Native pointers remain for advanced scenarios only; DTOs stay for richer data (e.g., `CustomColorInfo`, `GammaInfo`, `GamutInfo`, `ThreeDLUTInfo`, `ConnectivityExperienceInfo`, `DisplayResolutionInfo`).
- Managed collections: `EnumerateDisplays()` returns `IReadOnlyList<ADLXDisplay>` (disposable items); `EnumerateDisplayHandles()` stays for advanced users.
- Facade shape (current `ADLXDisplay`): identity props, state getters, simple setters, event helpers; pointer handling is internal.
- Simple getters/setters: every display setting provides `Get<Setting>State()` returning support + current values and `Set<Setting>(...)` for updates. Naming aligns to the setting shape and matches the current facade:
  - Mode-only: `Set<Setting>(ADLX_<MODE>)` (e.g., `SetScalingMode(ADLX_SCALE_MODE)`).
  - Enabled + mode: `Set<Setting>(bool enabled, <ModeEnum> mode)` (e.g., `SetVariBright(bool enabled, VariBrightMode mode)`).
  - Enabled-only: `Set<Setting>(bool enabled)` (e.g., `SetFreeSync`, `SetGpuScaling`, `SetVirtualSuperResolution`, `SetIntegerScaling`, `SetHdcp`, `SetFreeSyncColorAccuracy`, `SetDynamicRefreshRateControl`, `SetDisplayBlanked`).
  No manual `ComPtr` in normal usage.

Execution steps
1) Reshape `ADLXSystemServicesHelper.EnumerateDisplays()`:
   - Change to return `IReadOnlyList<ADLXDisplay>` (each item constructed with display + display services + desktop services as needed).
   - Keep `EnumerateDisplayHandles()` for advanced/native consumers.
2) Keep `ADLXDisplay` aligned to the facade shape:
   - Identity properties: Name, Width, Height, RefreshRate, PixelClock, ConnectorType, ScanType, Type, UniqueId, GpuUniqueId, Edid, ManufacturerId, Identity (DisplayInfo).
   - State getters + setters aligned to the three shapes above; enabled-only setters use `Set<Setting>(bool)` names already present.
   - DTO helpers remain for richer data: `GetCustomColor()/ApplyCustomColor(CustomColorInfo)`, `GetGamma()/ReapplyGamma()`, `GetGamut()/ReapplyGamut()`, `GetThreeDLut()/ReapplyThreeDLut()`, `GetConnectivityExperience()/ApplyConnectivityExperience(ConnectivityExperienceInfo)`, `EnumerateCustomResolutions()/GetCustomResolutionListNative()`.
   - Event helpers stay on the facade; native pointer handling remains internal.
3) Adjust samples and README to use the facade flow:
   ```csharp
   using var adlx = ADLXApiHelper.Initialize();
   using var sys = adlx.GetSystemServices();
   var displays = sys.EnumerateDisplays();
   foreach (var display in displays) using (display)
   {
       Console.WriteLine($"{display.Name} [{display.Width}x{display.Height}]");
       var vsr = display.GetVirtualSuperResolutionState();
       if (vsr.supported && !vsr.enabled) display.SetVirtualSuperResolution(true);
   }
   ```
4) Tests: add/update tests to validate facade getters/setters without requiring native handles in test bodies; keep native-path tests separately if needed.
5) Future application: apply the same facade pattern to desktops (e.g., `EnumerateDesktops()` -> `IReadOnlyList<ADLXDesktop>` with feature methods) after display work is complete.
