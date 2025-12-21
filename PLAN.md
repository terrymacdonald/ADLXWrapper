## Simplify ADLX system services acquisition

Goal: make `ADLXApi.Initialize()` return an object that directly exposes system services via `adlx.GetSystemServices()`, without extra helper plumbing, while still supporting multiple independent ADLX connections (multiple initialized instances).

Planned changes
- Update `ADLXApiHelper` (and any public `ADLXApi` facade if present) to cache `IADLXSystem` on initialization and expose:
  - Rename current handle accessor to `GetSystemServicesHandle()` (returns `AdlxInterfaceHandle`, add-ref’d).
  - Keep `GetSystemServicesNative()` returning `IADLXSystem*` (highest available interface, add-ref’d or owned per current pattern).
  - Rename helper accessor to `GetSystemServices()` returning `ADLXSystemServicesHelper` so callers avoid native pointers.
- Ensure ownership/dispose rules: each initialized instance holds its own `ComPtr<IADLXSystem>`; disposing an instance releases only its own resources; methods throw `ObjectDisposedException` after dispose. Multiple instances must coexist without shared static state.
- Remove or deprecate redundant system-accessor logic elsewhere so callers only need:
  ```csharp
  using var adlx = ADLXApiHelper.Initialize();
  using var sysHelper = adlx.GetSystemServices();       // helper wrapper
  var sysHandle = adlx.GetSystemServicesHandle();       // managed handle
  var sysNative = adlx.GetSystemServicesNative();       // native pointer
  ```
- Adjust tests/samples to use the new names: helper via `GetSystemServices()`, handle via `GetSystemServicesHandle()`, native via `GetSystemServicesNative()`.
- Keep capability/error semantics unchanged: if system services are unavailable, throw `ADLX_NOT_SUPPORTED`; maintain `IsADLXDllAvailable` check and current initialization sequencing.

Refactor steps (sensible chunks)
1) ADLXApiHelper rename pass:
   - Rename handle accessor to `GetSystemServicesHandle()`.
   - Rename helper accessor to `GetSystemServices()` (returning `ADLXSystemServicesHelper`).
   - Ensure native accessor stays `GetSystemServicesNative()`.
   - Update XML docs/comments accordingly.
2) Apply the Handle/Native/Helper naming pattern to all feature services helpers:
   - For each helper (`ADLXDisplayServicesHelper`, `ADLXDesktopServicesHelper`, `ADLX3DSettingsServicesHelper`, `ADLXGPUTuningServicesHelper`, `ADLXPerformanceMonitoringServicesHelper`, `ADLXPowerTuningServicesHelper`, `ADLXMultimediaServicesHelper`, etc.), expose:
     - `Get<Feature>ServicesHandle()` returning `AdlxInterfaceHandle` (add-ref’d)
     - `Get<Feature>ServicesNative()` returning the highest available native pointer
     - `Get<Feature>Services()` returning the helper itself or a helper wrapper as appropriate
   - Ensure consistency in docs and parameter names across helpers.
3) API surface and call site updates:
   - Update tests, samples, and any helper call sites to the new method names (helper vs handle vs native).
   - Verify there are no lingering references to old names via `rg`.
4) Validation:
   - Run `dotnet test` to confirm the rename ripple is clean.
   - Spot-check README/usage snippets to reflect the simplified helper call.
