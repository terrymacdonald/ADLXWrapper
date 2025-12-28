# ADLXWrapper

A modern, high-performance C# wrapper for the AMD Display Library Extensions (ADLX) SDK, generated using ClangSharp.

This wrapper provides a safe, idiomatic, and vtable-based interop layer, allowing .NET applications to interact directly and efficiently with AMD GPU features.

## Features

- **Full API Coverage**: Wraps the core ADLX services for System, GPU, Display, Desktop, 3D Settings, Performance Monitoring, Power Tuning, and Multimedia.
- **Service Helpers**: Per-service helper classes (e.g., `ADLXSystemServicesHelper`, `ADLXDisplayServicesHelper`, `ADLXGPUTuningServicesHelper`) wrap native interfaces with safe enumeration, capability checks, and managed DTOs.
- **Serializable Data Objects**: All hardware states are read into serializable `Info` structs, perfect for saving configurations to JSON.
- **Configuration Management**: Includes `Apply` methods to restore hardware states from deserialized `Info` objects.
- **Real-time Event Handling**: Provides listeners for display, desktop, and GPU tuning changes.

## Project Structure

```
ADLXWrapper/
|-- ADLX/                     # ADLX SDK headers (downloaded by script)
|-- ADLXWrapper/              # The main C# wrapper project
|   |-- ADLX*.cs              # Core helpers/services for each ADLX feature
|   \-- README.md             # Detailed API documentation and examples
|-- ADLXWrapper.NativeTests/  # xUnit native test suite
|-- Samples/                  # Sample console applications
\-- scripts/                  # Build, test, and preparation scripts
```

## Getting Started

### 1. Prepare the Environment

First, run the preparation script. This will download the required ADLX SDK headers into the `ADLX/` directory.

```powershell
./prepare_adlx.ps1
```

### 2. Build the Solution

Once the SDK is in place, you can build the entire solution, including the wrapper, tests, and samples.

```powershell
./build_adlx.ps1
```

### 3. Run Tests

To verify the build and check hardware compatibility, run the native test script.

```powershell
./test_adlx.ps1
```

## Using ADLXWrapper in another project

### Option A: add the project (preferred)
- Keep this repo as a sibling folder or git submodule, run `./prepare_adlx.ps1` once, then `dotnet add <your>.csproj reference ..\ADLXWrapper\ADLXWrapper.csproj`.
- Build normally; the `cs_generated` bindings are produced automatically, so you do not need to copy them manually.

### Option B: drop in the prebuilt DLL
- Build a Release copy (`dotnet build ADLXWrapper/ADLXWrapper.csproj -c Release`) or use the release ZIP built by `./create_adlx_release_zip.ps1`.
- Add a file reference to `ADLXWrapper.dll` (for example in your `.csproj`):

```xml
<ItemGroup>
  <Reference Include="ADLXWrapper">
    <HintPath>lib/ADLXWrapper.dll</HintPath>
  </Reference>
</ItemGroup>
```

- Only the managed DLL is required for consumers; all public helpers (`ADLXApiHelper`, service helpers, facades, DTOs) live in that assembly.
- The ADLX native runtime ships with AMD drivers, so you do not need to redistribute SDK headers or binaries.
- Do **not** cherry-pick files if you embed sources. Copy the entire `ADLXWrapper/` folder (all `ADLX*.cs` plus `cs_generated/` after running `prepare_adlx.ps1`) so the helper implementations and generated bindings stay in sync.

## Packaging a release ZIP
- Run `./prepare_adlx.ps1` once, then execute `./create_adlx_release_zip.ps1` (defaults to Release build). The script builds the wrapper and drops `artifacts/adlxwrapper-<version>-Release.zip`.
- Contents: `ADLXWrapper.dll`, `ADLXWrapper.pdb`, `ADLXWrapper.deps.json`, XML docs (`ADLXWrapper.xml`), top-level `README.md`/`LICENSE`, and (optionally) sources + `cs_generated` when run with `-IncludeSources`.

## Detailed Usage and Examples

### Quick sample (facade-first)

```csharp
using var adlx = ADLXApiHelper.Initialize();
using var sys = adlx.GetSystemServices();

// Enumerate GPUs and displays (pointer-free)
var gpus = sys.EnumerateADLXGPUs();
var displays = sys.EnumerateDisplays();

foreach (var display in displays)
using (display)
{
    Console.WriteLine($"Display {display.Name} [{display.Width}x{display.Height}] on GPU {display.GpuUniqueId}");

    // Toggle a display feature if supported (e.g., Virtual Super Resolution)
    var vsr = display.GetVirtualSuperResolutionState();
    if (vsr.supported && !vsr.enabled)
    {
        display.SetVirtualSuperResolution(true);
    }
}

// Listen for display settings changes (callbacks occur on ADLX threads)
using var displayServices = sys.GetDisplayServices();
using var settingsListener = displayServices.AddDisplaySettingsEventListener(evt =>
{
    Console.WriteLine("[Display settings changed]");
    return true; // keep listening
});

Console.WriteLine("Listener registered. Press Enter to exit...");
Console.ReadLine();
```

### Support and disposal notes
- Optional features surface support via `IsSupported` or capability objects; unsupported operations throw `ADLX_NOT_SUPPORTED`.
- Helpers guard against use-after-dispose with `ObjectDisposedException`. Native pointers returned by helpers should be wrapped in `ComPtr` or disposed handles to avoid leaks.
- Tests and samples may skip on systems without the required AMD hardware/driver.

For additional API documentation and examples, see the **ADLXWrapper Project README** in the `ADLXWrapper/` folder.
