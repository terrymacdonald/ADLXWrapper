# ADLXWrapper

Facade-first C# access to the AMD ADLX SDK, with native bindings available for low-level control.

## Quick links
- Facade guide (recommended): `ADLXWrapper/README.md`
- Native guide: `ADLXWrapper/README.Native.md`
- Samples (each offers Facade and Native menu options): `Samples/`
- API docs (local HTML): `APIDocs/_site/index.html`
- Native tests: `ADLXWrapper.NativeTests/` (raw `cs_generated` APIs only)
- Facade tests: `ADLXWrapper.FacadeTests/` (helper/facade APIs)

## Why facade-first?
Facades remove pointer management and expose strongly-typed helpers (`ADLXSystemServicesHelper`, `ADLXDisplayServicesHelper`, etc.) that wrap the highest supported ADLX interface version, handle AddRef/Release, and surface DTOs for easy serialization. Use the native layer only when you need direct vtable access or to mirror ADLX SDK samples verbatim.

## Getting started
1) Prepare ADLX headers (once):
```powershell
./prepare_adlx.ps1
```
2) Build everything:
```powershell
./build_adlx.ps1
```
3) Run tests (hardware-aware; native first):
```powershell
./test_adlx.ps1
dotnet test ADLXWrapper.FacadeTests/ADLXWrapper.FacadeTests.csproj --verbosity normal
```
4) Explore samples: `dotnet run --project Samples/DisplaySample/DisplaySample.csproj` (menus show both Facade and Native flows).

## Facade quick start
```csharp
using ADLXWrapper;

using var adlx = ADLXApiHelper.Initialize();
using var sys = adlx.GetSystemServices();

// Displays (pointer-free)
var displays = sys.EnumerateDisplays();
foreach (var display in displays)
using (display)
{
    Console.WriteLine($"{display.Name} {display.NativeResolutionWidth}x{display.NativeResolutionHeight} @ {display.RefreshRate:F2} Hz");
    var vsr = display.GetVirtualSuperResolutionState();
    if (vsr.supported && !vsr.enabled) display.SetVirtualSuperResolution(true);
}

// GPU identity + metrics
var gpus = sys.EnumerateADLXGPUs();
foreach (var gpu in gpus)
using (gpu)
{
    var id = gpu.Identity;
    Console.WriteLine($"{id.Name} ({id.Brand}, {id.VRAMType}, {id.TotalVRAM} MB)");
}
```
More in `ADLXWrapper/README.md` (per-feature examples: Display, Desktop, GPU identity, Perf, 3D settings, Tuning, Power, Color, Multimedia).

## Using the release ZIP
- Build with `./create_adlx_release_zip.ps1` or download from GitHub Releases (artifact name `adlxwrapper-<version>-Release.zip`).
- Contents: `ADLXWrapper.dll`, `ADLXWrapper.pdb`, `ADLXWrapper.deps.json`, `ADLXWrapper.xml`, top-level README/license; optional sources if built with `-IncludeSources`.
- Reference in your project:
```xml
<ItemGroup>
  <Reference Include="ADLXWrapper">
    <HintPath>packages\\adlxwrapper\\ADLXWrapper.dll</HintPath>
  </Reference>
</ItemGroup>
```
The ADLX runtime ships with AMD drivers; no extra native payload is redistributed here.

## Native path
If you need vtable-level access, see `ADLXWrapper/README.Native.md` for initialization patterns, lifetime rules, and raw pointer examples that mirror the ADLX SDK samples. Native tests (`ADLXWrapper.NativeTests`) demonstrate direct usage of the generated interfaces in `ADLXWrapper/cs_generated/`.

## Project structure
```
ADLXWrapper/
|-- ADLX/                     # ADLX SDK headers (fetched by prepare script)
|-- ADLXWrapper/              # Library sources (helpers + cs_generated bindings)
|-- ADLXWrapper.NativeTests/  # Native xUnit tests
|-- ADLXWrapper.FacadeTests/  # Facade xUnit tests
|-- Samples/                  # Console samples (Facade and Native menus)
|-- APIDocs/_site/            # Generated API HTML docs
|-- scripts/                  # Prepare/build/test/release scripts
```

Need more? Start with `ADLXWrapper/README.md` for the Facade walkthrough, then branch to the Native guide if you need raw access. APIDocs in `APIDocs/_site/index.html` provide full API surface details.**
