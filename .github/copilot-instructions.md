# Copilot Instructions for ADLXWrapper

- **Goal & scope**: Safe C# wrapper over AMD ADLX native DLLs (vtable COM style) targeting Windows x64 / net10.0. Generated bindings live in [ADLXWrapper/cs_generated](../ADLXWrapper/cs_generated); hand-written helpers/facades live in [ADLXWrapper](../ADLXWrapper).
- **Initialization & lifetimes**: Create the API with `using var adlx = ADLXApi.Initialize();` (or `InitializeWithCallerAdl`). Retrieve services via `GetSystemServices()` or `GetSystemServicesFacade()`; wrap raw pointers in `ComPtr<T>` for RAII. `ADLXApi.Dispose` terminates ADLX then unloads the DLL—calls after disposal should throw `ObjectDisposedException`.
- **DLL loading**: [ADLXWrapper/ADLXApi.cs](../ADLXWrapper/ADLXApi.cs) loads `amdadlx64.dll` through `LoadLibraryEx` (see [ADLXWrapper/ADLXNative.cs](../ADLXWrapper/ADLXNative.cs)). Keep `GetDllName()` and `LoadADLXDll()` consistent if names/paths change; surface load failures via `ADLXException`.
- **Interop layout**: Manual vtables live in [ADLXWrapper/ADLXVTables.cs](../ADLXWrapper/ADLXVTables.cs); generated interfaces/enums in [ADLXWrapper/cs_generated](../ADLXWrapper/cs_generated). When calling vtable functions, use `Marshal.GetDelegateForFunctionPointer` with `StdCall`.
- **Facade-first**: Prefer facade types (`AdlxDisplay`, `AdlxDesktop`, `ADLXSystemServices` in [ADLXWrapper](../ADLXWrapper)) over legacy static helpers. Legacy helpers (e.g., `ADLXDisplayHelpers`, `ADLXDesktopHelpers`) remain for compatibility; new work should mirror facade style and immutability.
- **Naming for facade methods**: When adding facade-surface methods, name them with `Profile` instead of `Facade` (e.g., `GetSystemServicesProfile`, display/desktop `GetProfile()/ApplyProfile` style). Keep this convention for new or versioned APIs.
- **Profiles & data shapes**: Facades expose `GetProfile()/ApplyProfile` for displays/desktops; display apply methods accept a skip callback for unsupported DS3 features. `GpuInfo`/`DisplayInfo` are JSON-friendly structs (see [ADLXWrapper/ADLXGpuHelpers.cs](../ADLXWrapper/ADLXGpuHelpers.cs), [ADLXWrapper/ADLXDisplayHelpers.cs](../ADLXWrapper/ADLXDisplayHelpers.cs)). `ADLXListHelpers` requires disposing returned items—prefer `ComPtr`.
- **Capabilities & gating**: Capability checks surface via `ADLXSystemServices.Capabilities`; display apply paths gate newer features (DS3) and may skip unsupported items. Always guard hardware access with `ADLXHardwareDetection.HasAMDGPU` and `ADLXApi.IsADLXDllAvailable` for clean skips.
- **Events**: For display events, obtain a display-services handle from the facade and register through `IADLXDisplayChangedHandling` (pattern shown in [ADLXWrapper/README.md](../ADLXWrapper/README.md)).
- **Build workflow**: From repo root on Windows PowerShell: `./prepare_adlx.ps1` (downloads/validates ADLX SDK into `ADLX/`), `./build_adlx.ps1` (restores, cleans, builds `ADLXWrapper.sln`, sets version from `VERSION` + git commit count). Direct build: `dotnet build ADLXWrapper/ADLXWrapper.csproj` (ClangSharp generation runs as an MSBuild target).
- **Tests**: xUnit suite in [ADLXWrapper.Tests](../ADLXWrapper.Tests); run `./test_adlx.ps1` or `dotnet test ADLXWrapper.Tests/ADLXWrapper.Tests.csproj`. Tests auto-skip on non-AMD systems or missing `amdadlx64.dll` and do not mutate hardware state.
- **Generated bindings**: `GenerateBindings` target runs `ClangSharpPInvokeGenerator @ClangSharpConfig.rsp` (see [ADLXWrapper/ClangSharpConfig.rsp](../ADLXWrapper/ClangSharpConfig.rsp)); `dotnet clean` deletes [cs_generated](../ADLXWrapper/cs_generated). Never hand-edit generated files—adjust headers/config instead.
- **Versioning**: `build_adlx.ps1` and the `SetVersionFromGit` MSBuild target derive `MAJOR.MINOR` from [VERSION](../VERSION) and `PATCH` from git commit count. Update [VERSION](../VERSION) when bumping major/minor.
- **Samples & usage**: Minimal usage snippets plus facade guidance live in [ADLXWrapper/README.md](../ADLXWrapper/README.md) and samples under [Samples](../Samples) (build with `dotnet build Samples/ADLXWrapper.Samples.sln`).

Quick-start pattern:
```csharp
using var adlx = ADLXApi.Initialize();
var system = adlx.GetSystemServicesFacade();
var displays = system.EnumerateAllDisplays();
foreach (var d in displays)
{
    using (d)
    {
        var profile = d.GetProfile();
        d.ApplyProfile(profile, skip => Console.WriteLine($"skip: {skip}"));
    }
}
```
