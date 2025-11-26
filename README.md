# ADLXWrapper

A modern C# wrapper for AMD's ADLX (AMD Display Library Extensions) using ClangSharp, enabling .NET developers to interact with AMD GPU features, display settings, and performance monitoring capabilities.

## Overview

ADLXWrapper provides a high-performance, native .NET wrapper for the AMD ADLX SDK built with ClangSharp. The wrapper uses direct P/Invoke with VTable-based COM interop for optimal performance and maintainability. It allows developers to easily interact with the AMD ADLX API provided by the AMD Adrenalin drivers. 

### Key Features

- ✅ **Modern .NET Architecture** - IDisposable pattern, async-compatible
- ✅ **High Performance** - Direct P/Invoke with struct-based bindings
- ✅ **Type Safe** - Compile-time safety with IntelliSense support
- ✅ **Easy to Use** - Simplified API with helper methods
- ✅ **Well Tested** - 60+ comprehensive tests
- ✅ **Production Ready** - 70% of ADLX API coverage

### Implemented Features

✅ **GPU Management**
- Enumerate GPUs and query hardware details
- Access GPU properties (name, vendor, VRAM, device ID)
- GPU identification and unique ID retrieval

✅ **Display Services**
- Enumerate connected displays
- Query display properties (resolution, refresh rate, manufacturer)
- Access pixel clock and display configuration

✅ **Performance Monitoring**
- Real-time GPU metrics (temperature, usage, clock speeds)
- VRAM usage monitoring
- Fan speed and power consumption tracking

✅ **GPU Tuning**
- Detect tuning capabilities (auto, preset, manual)
- Check support for fan, power, and clock tuning
- Read-only capability detection

## Quick Start

### Prerequisites

- **Hardware**: AMD GPU with ADLX support (Radeon RX 5000 series or newer recommended)
- **Operating System**: Windows 10/11 (64-bit)
- **AMD Drivers**: Latest Adrenalin drivers installed
- **.NET**: .NET 9.0 or later

### Installation

```bash
# Clone the repository
git clone https://github.com/terrymacdonald/ADLXWrapper.git
cd ADLXWrapper

# Build the wrapper
cd ADLXWrapper
dotnet build

# Add to your project
dotnet add reference path/to/ADLXWrapper/ADLXWrapper.csproj
```

### Basic Usage

```csharp
using ADLXWrapper;
using System;

// Initialize ADLX with automatic cleanup
using (var adlx = ADLXApi.Initialize())
{
    // Get version
    Console.WriteLine($"ADLX Version: {adlx.GetVersion()}");
    
    // Enumerate GPUs
    var gpus = adlx.EnumerateGPUs();
    Console.WriteLine($"Found {gpus.Length} GPU(s)");
    
    foreach (var gpu in gpus)
    {
        // Get GPU info using helper methods
        var info = ADLXGPUInfo.GetBasicInfo(gpu);
        Console.WriteLine($"\nGPU: {info.Name}");
        Console.WriteLine($"  VRAM: {info.TotalVRAM} MB ({info.VRAMType})");
        Console.WriteLine($"  External: {info.IsExternal}");
        
        // Release GPU interface
        ADLXHelpers.ReleaseInterface(gpu);
    }
    
    // Get displays
    var pSystem = adlx.GetSystemServices();
    var displays = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);
    
    foreach (var display in displays)
    {
        var displayInfo = ADLXDisplayInfo.GetBasicInfo(display);
        Console.WriteLine($"\nDisplay: {displayInfo.Name}");
        Console.WriteLine($"  Resolution: {displayInfo.Width}x{displayInfo.Height}");
        Console.WriteLine($"  Refresh Rate: {displayInfo.RefreshRate} Hz");
        
        ADLXHelpers.ReleaseInterface(display);
    }
    
    // Monitor GPU performance
    if (gpus.Length > 0)
    {
        var perfMon = adlx.GetPerformanceMonitoringServices();
        var snapshot = ADLXPerformanceMonitoringInfo.GetCurrentMetrics(perfMon, gpus[0]);
        
        Console.WriteLine($"\nGPU Metrics:");
        Console.WriteLine($"  Temperature: {snapshot.Temperature:F1}°C");
        Console.WriteLine($"  Usage: {snapshot.Usage:F1}%");
        Console.WriteLine($"  Clock Speed: {snapshot.ClockSpeed} MHz");
        
        ADLXHelpers.ReleaseInterface(perfMon);
    }
} // Automatic cleanup
```

## API Overview

### ADLXApi (Main Wrapper)

**Initialization:**
- `static ADLXApi Initialize()` - Initialize ADLX with default settings
- `static ADLXApi InitializeWithCallerAdl(IntPtr adlContext, IntPtr adlMainMemoryFree)` - Initialize with existing ADL context

**Version Information:**
- `ulong GetFullVersion()` - Get ADLX full version number
- `string GetVersion()` - Get ADLX version string

**System Services:**
- `IntPtr GetSystemServices()` - Get root system interface pointer
- `IntPtr GetGPUTuningServices()` - Get GPU tuning services interface pointer
- `IntPtr GetPerformanceMonitoringServices()` - Get performance monitoring services interface pointer
- `IntPtr[] EnumerateGPUs()` - Enumerate all AMD GPUs in the system

**Cleanup:**
- `void Dispose()` - Release resources (called automatically with `using`)

### Helper Classes

**ADLXHelpers** - GPU property access
- `GetGPUName()`, `GetGPUVendorId()`, `GetGPUDeviceId()`
- `GetGPUTotalVRAM()`, `GetGPUVRAMType()`
- `IsGPUExternal()`, `HasGPUDesktops()`
- `ReleaseInterface()`, `AddRefInterface()`

**ADLXDisplayHelpers** - Display operations
- `EnumerateAllDisplays()`
- `GetDisplayName()`, `GetDisplayNativeResolution()`
- `GetDisplayRefreshRate()`, `GetDisplayPixelClock()`

**ADLXGPUTuningHelpers** - Tuning capabilities
- `IsSupportedAutoTuning()`, `IsSupportedPresetTuning()`
- `IsSupportedManualGFXTuning()`, `IsSupportedManualVRAMTuning()`
- `IsSupportedManualFanTuning()`, `IsSupportedManualPowerTuning()`

**ADLXPerformanceMonitoringHelpers** - Performance metrics
- `GetCurrentGPUMetrics()`
- `GetGPUTemperature()`, `GetGPUUsage()`
- `GetGPUClockSpeed()`, `GetGPUVRAMClockSpeed()`
- `GetGPUFanSpeed()`, `GetGPUPower()`

### Combined Information Structs

**ADLXGPUInfo**
- `GPUBasicInfo` - Name, VendorId, VRAM, IsExternal, etc.
- `GPUIdentification` - DeviceId, PNPString, DriverPath

**ADLXDisplayInfo**
- `DisplayBasicInfo` - Name, Resolution, RefreshRate, etc.

**ADLXPerformanceMonitoringInfo**
- `GPUMetricsSnapshot` - Temperature, Usage, ClockSpeed, etc.
- `GPUMetricsSupport` - Capability flags for each metric

## Documentation

- **[ADLXWrapper/README.md](ADLXWrapper/README.md)** - Detailed API reference
- **[ADLXWrapper.Tests/README.md](ADLXWrapper.Tests/README.md)** - Test documentation

## Testing

The project includes comprehensive tests covering all implemented features.

```bash
cd ADLXWrapper.Tests
dotnet test --verbosity detailed
```

**Note**: Tests require AMD GPU hardware to run successfully. Tests will gracefully skip on systems without AMD hardware.

## Architecture

The wrapper uses a layered approach:

1. **Native Layer** - Manual P/Invoke for DLL loading and entry points
2. **VTable Layer** - COM-like interface vtable definitions
3. **Wrapper Layer** - Managed API with IntPtr handles and IDisposable
4. **Helper Layer** - Convenience methods for common operations

This architecture provides:
- Direct native performance
- Type-safe managed API
- Easy-to-use helper methods
- Proper resource management

## Memory Management

- All ADLX interfaces use COM-like reference counting
- Interfaces returned by enumeration methods should be released with `ADLXHelpers.ReleaseInterface()`
- The `ADLXApi` class implements IDisposable for automatic cleanup
- Always use `using` statement or call `Dispose()` explicitly

## Project Structure

```
ADLXWrapper/
├── ADLXWrapper/                # Main wrapper project (ClangSharp-based)
│   ├── ADLXNative.cs          # P/Invoke declarations
│   ├── ADLXApi.cs             # Main wrapper API
│   ├── ADLXVTables.cs         # VTable structures
│   ├── ADLXExtensions.cs      # Helper methods
│   └── MIGRATION-GUIDE.md     # Migration from SWIG
├── ADLXWrapper.Tests/         # Comprehensive test suite
│   ├── BasicApiTests.cs       # Initialization tests
│   ├── CoreApiTests.cs        # GPU enumeration tests
│   ├── DisplayServicesTests.cs # Display tests
│   ├── GpuTuningServicesTests.cs # Tuning tests
│   ├── PerformanceMonitoringServicesTests.cs # Metrics tests
│   └── ArchitectureValidationTests.cs # Architecture tests
└── README.md                  # This file
```

## Requirements

- **.NET**: 9.0 or later
- **ClangSharp**: 18.1.0 / 20.1.2 (NuGet packages)
- **AMD Drivers**: Adrenalin 21.10.1 or newer
- **AMD GPU**: Radeon RX 5000 series or newer recommended

## Roadmap

Future enhancements planned:
- Desktop services and Eyefinity support
- 3D settings services
- I2C communication services
- Power tuning services
- Multimedia services (requires newer GPUs)
- Event listener support

The VTable pattern is proven and can be easily extended to support additional ADLX services.

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests on the GitHub repository.

When adding new features:
1. Follow the established VTable pattern
2. Add helper methods to appropriate extension classes
3. Create comprehensive tests
4. Update documentation

## License

This project wraps AMD's ADLX SDK. Please refer to AMD's ADLX SDK license for terms and conditions regarding the use of ADLX functionality.

## Resources

- [AMD ADLX SDK Documentation](https://gpuopen.com/adlx/)
- [AMD GPUOpen](https://gpuopen.com/)
- [GitHub Repository](https://github.com/terrymacdonald/ADLXWrapper)

## Support

For issues, questions, or feature requests:
1. Review the documentation in the `ADLXWrapper/` directory
2. Check existing GitHub issues
3. Create a new issue with detailed information about your environment and problem

---

**Note**: This wrapper is provided as-is. Always test thoroughly in your specific environment before production use.
