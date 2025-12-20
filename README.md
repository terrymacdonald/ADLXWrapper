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
|-- ADLXWrapper.Tests/        # xUnit test suite
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

To verify the build and check hardware compatibility, run the test script.

```powershell
./test_adlx.ps1
```

## Detailed Usage and Examples

### Quick sample

```csharp
using var adlx = ADLXApiHelper.Initialize();
using var sys = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());

// Enumerate GPUs and displays
var gpuHandles = sys.EnumerateGPUHandles();
var displayHandles = sys.EnumerateDisplayHandles();

// Toggle a display feature if supported (e.g., Virtual Super Resolution)
using var displayHelper = new ADLXDisplayServicesHelper(sys.GetDisplayServicesNative());
var firstDisplay = displayHandles[0].As<IADLXDisplay>();
var vsr = displayHelper.GetVirtualSuperResolution(firstDisplay);
if (vsr.IsSupported)
{
    displayHelper.SetVirtualSuperResolutionEnabled(displayHelper.GetVirtualSuperResolutionNative(firstDisplay), !vsr.IsEnabled);
}

// Always dispose handles/helpers to release native refs
foreach (var h in gpuHandles) h.Dispose();
foreach (var h in displayHandles) h.Dispose();
```

### Support and disposal notes
- Optional features surface support via `IsSupported` or capability objects; unsupported operations throw `ADLX_NOT_SUPPORTED`.
- Helpers guard against use-after-dispose with `ObjectDisposedException`. Native pointers returned by helpers should be wrapped in `ComPtr` or disposed handles to avoid leaks.
- Tests and samples may skip on systems without the required AMD hardware/driver.

For additional API documentation and examples, see the **ADLXWrapper Project README** in the `ADLXWrapper/` folder.
