## Simplify ADLX system services acquisition

Goal: make `ADLXApi.Initialize()` return an object that directly exposes system services via `adlx.GetSystemServices()`, without extra helper plumbing, while still supporting multiple independent ADLX connections (multiple initialized instances).

Planned changes
- Update `ADLXApiHelper` (and any public `ADLXApi` facade if present) to cache `IADLXSystem` on initialization and expose:
  - `GetSystemServices()` returning `AdlxInterfaceHandle` (add-ref’d)
  - `GetSystemServicesNative()` returning `IADLXSystem*` (highest available interface, add-ref’d or owned per current pattern)
- Ensure ownership/dispose rules: each initialized instance holds its own `ComPtr<IADLXSystem>`; disposing an instance releases only its own resources; methods throw `ObjectDisposedException` after dispose. Multiple instances must coexist without shared static state.
- Remove or deprecate redundant system-accessor logic elsewhere so callers only need:
  ```csharp
  using var adlx = ADLXApiHelper.Initialize();
  using var sys = adlx.GetSystemServices();
  ```
- Adjust tests/samples to use `adlx.GetSystemServices()` for the managed handle and `GetSystemServicesNative()` where native pointers are required.
- Keep capability/error semantics unchanged: if system services are unavailable, throw `ADLX_NOT_SUPPORTED`; maintain `IsADLXDllAvailable` check and current initialization sequencing.
