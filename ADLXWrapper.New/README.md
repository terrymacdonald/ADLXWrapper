# ADLXWrapper - ClangSharp-based C# Wrapper

This is the new ClangSharp-based C# wrapper for the AMD ADLX SDK, replacing the previous SWIG-based implementation.

## Project Structure

```
ADLXWrapper.New/
??? ADLXWrapper.csproj          # .NET 9 project file
??? ClangSharpConfig.rsp        # ClangSharp generator configuration
??? ADLXNative.cs               # Manual P/Invoke declarations for DLL entry points
??? ADLXApi.cs                  # Main wrapper API (IDisposable)
??? ADLXVTables.cs              # VTable structure definitions
??? ADLXExtensions.cs           # Helper methods for GPU/Display operations
??? Generated/                  # ClangSharp auto-generated bindings (DO NOT EDIT)
?   ??? README.cs              # Placeholder
??? README.md                  # This file
```

## Build Status

? **Stage 1 Complete:** Project Setup and ClangSharp Configuration  
? **Stage 2 Complete:** Core Wrapper Layer (ADLXApi.cs)  
? **Stage 3 Complete:** Helper Extension Layer (ADLXExtensions.cs)  
? **Stage 4 Complete:** Basic Tests and Validation  
? **Stage 5 Complete:** Display Services Tests  
? **Stage 6 Complete:** GPU Tuning Services Tests  
? **Stage 7 Complete:** Performance Monitoring Tests  

- Created .NET 9 C# project
- Added ClangSharp NuGet packages (v18.1.0 / v20.1.2)
- Created ClangSharpConfig.rsp for ADLX header processing
- Added manual P/Invoke declarations for dynamic DLL loading
- Implemented complete ADLXApi wrapper with initialization and GPU enumeration
- Implemented VTable access for COM-like interfaces
- Created comprehensive helper methods for GPU properties
- Created comprehensive display enumeration and property access
- Created comprehensive GPU tuning capability checks
- Created comprehensive performance monitoring and metrics collection
- Project builds successfully

## How to Build

```powershell
cd ADLXWrapper.New
dotnet restore
dotnet build
```

## Usage Example

```csharp
using ADLXWrapper;

// Initialize ADLX
using (var adlx = ADLXApi.Initialize())
{
    // Get version info
    Console.WriteLine($"ADLX Version: {adlx.GetVersion()}");
    
    // Enumerate GPUs
    var gpus = adlx.EnumerateGPUs();
    Console.WriteLine($"Found {gpus.Length} GPU(s)");
    
    foreach (var gpu in gpus)
    {
        // Get basic GPU info
        var info = ADLXGPUInfo.GetBasicInfo(gpu);
        Console.WriteLine($"\nGPU: {info.Name}");
        Console.WriteLine($"  Vendor: {info.VendorId}");
        Console.WriteLine($"  VRAM: {info.TotalVRAM} MB ({info.VRAMType})");
        Console.WriteLine($"  External: {info.IsExternal}");
        Console.WriteLine($"  Has Desktops: {info.HasDesktops}");
        
        // Get individual properties
        var uniqueId = ADLXHelpers.GetGPUUniqueId(gpu);
        var deviceId = ADLXHelpers.GetGPUDeviceId(gpu);
        Console.WriteLine($"  Unique ID: {uniqueId}");
        Console.WriteLine($"  Device ID: {deviceId}");
        
        // Remember to release GPU interface when done
        ADLXHelpers.ReleaseInterface(gpu);
    }

    // Enumerate displays
    var pSystem = adlx.GetSystemServices();
    var displays = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);
    Console.WriteLine($"\nFound {displays.Length} display(s)");
    
    foreach (var display in displays)
    {
        // Get display info
        var displayInfo = ADLXDisplayInfo.GetBasicInfo(display);
        Console.WriteLine($"\nDisplay: {displayInfo.Name}");
        Console.WriteLine($"  Resolution: {displayInfo.Width}x{displayInfo.Height}");
        Console.WriteLine($"  Refresh Rate: {displayInfo.RefreshRate} Hz");
        Console.WriteLine($"  Manufacturer ID: {displayInfo.ManufacturerID}");
        Console.WriteLine($"  Pixel Clock: {displayInfo.PixelClock}");
        
        // Remember to release display interface when done
        ADLXHelpers.ReleaseInterface(display);
    }
} // Automatic cleanup via Dispose
```

## API Reference

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

### ADLXHelpers (GPU Properties)

**String Properties:**
- `string GetGPUName(IntPtr pGPU)` - Get GPU name
- `string GetGPUVendorId(IntPtr pGPU)` - Get vendor ID
- `string GetGPUDriverPath(IntPtr pGPU)` - Get driver path
- `string GetGPUPNPString(IntPtr pGPU)` - Get PNP string
- `string GetGPUVRAMType(IntPtr pGPU)` - Get VRAM type
- `string GetGPUDeviceId(IntPtr pGPU)` - Get device ID

**Numeric Properties:**
- `uint GetGPUTotalVRAM(IntPtr pGPU)` - Get total VRAM in MB
- `int GetGPUUniqueId(IntPtr pGPU)` - Get unique identifier

**Boolean Properties:**
- `bool IsGPUExternal(IntPtr pGPU)` - Check if GPU is external
- `bool HasGPUDesktops(IntPtr pGPU)` - Check if GPU has desktops

**Interface Management:**
- `void ReleaseInterface(IntPtr pInterface)` - Release an interface (decrement ref count)
- `void AddRefInterface(IntPtr pInterface)` - Add reference to an interface (increment ref count)

### ADLXGPUInfo (Combined Information)

**Structs:**
- `GPUBasicInfo` - Name, VendorId, UniqueId, TotalVRAM, VRAMType, IsExternal, HasDesktops
- `GPUIdentification` - DeviceId, PNPString, DriverPath, UniqueId

**Methods:**
- `GPUBasicInfo GetBasicInfo(IntPtr pGPU)` - Get all basic info in one call
- `GPUIdentification GetIdentification(IntPtr pGPU)` - Get all identification info in one call

### ADLXListHelpers (List Operations)

- `uint GetListSize(IntPtr pList)` - Get number of items in list
- `bool IsListEmpty(IntPtr pList)` - Check if list is empty
- `IntPtr GetListItemAt(IntPtr pList, uint index)` - Get item at index
- `IntPtr[] ListToArray(IntPtr pList)` - Convert entire list to array

### ADLXDisplayHelpers (Display Operations)

- `IntPtr[] EnumerateAllDisplays(IntPtr pSystem)` - Enumerate all displays from system
- `string GetDisplayName(IntPtr pDisplay)` - Get display name
- `(int width, int height) GetDisplayNativeResolution(IntPtr pDisplay)` - Get native resolution
- `double GetDisplayRefreshRate(IntPtr pDisplay)` - Get refresh rate in Hz
- `uint GetDisplayManufacturerID(IntPtr pDisplay)` - Get manufacturer ID
- `uint GetDisplayPixelClock(IntPtr pDisplay)` - Get pixel clock

### ADLXDisplayInfo (Combined Display Information)

**Structs:**
- `DisplayBasicInfo` - Name, Width, Height, RefreshRate, ManufacturerID, PixelClock

**Methods:**
- `DisplayBasicInfo GetBasicInfo(IntPtr pDisplay)` - Get all display info in one call

### ADLXGPUTuningHelpers (GPU Tuning Operations)

- `bool IsSupportedAutoTuning(IntPtr pGPUTuningServices, IntPtr pGPU)` - Check if auto tuning is supported
- `bool IsSupportedPresetTuning(IntPtr pGPUTuningServices, IntPtr pGPU)` - Check if preset tuning is supported
- `bool IsSupportedManualGFXTuning(IntPtr pGPUTuningServices, IntPtr pGPU)` - Check if manual GFX tuning is supported
- `bool IsSupportedManualVRAMTuning(IntPtr pGPUTuningServices, IntPtr pGPU)` - Check if manual VRAM tuning is supported
- `bool IsSupportedManualFanTuning(IntPtr pGPUTuningServices, IntPtr pGPU)` - Check if manual fan tuning is supported
- `bool IsSupportedManualPowerTuning(IntPtr pGPUTuningServices, IntPtr pGPU)` - Check if manual power tuning is supported

### ADLXGPUTuningInfo (Combined GPU Tuning Information)

**Structs:**
- `GPUTuningCapabilities` - AutoTuningSupported, PresetTuningSupported, ManualGFXTuningSupported, ManualVRAMTuningSupported, ManualFanTuningSupported, ManualPowerTuningSupported

**Methods:**
- `GPUTuningCapabilities GetTuningCapabilities(IntPtr pGPUTuningServices, IntPtr pGPU)` - Get all tuning capabilities in one call

### ADLXPerformanceMonitoringHelpers (Performance Monitoring Operations)

- `IntPtr GetSupportedGPUMetrics(IntPtr pPerfMonServices, IntPtr pGPU)` - Get GPU metrics support interface
- `IntPtr GetCurrentGPUMetrics(IntPtr pPerfMonServices, IntPtr pGPU)` - Get current GPU metrics interface
- `bool IsSupportedGPUUsage(IntPtr pMetricsSupport)` - Check if GPU usage metric is supported
- `bool IsSupportedGPUClockSpeed(IntPtr pMetricsSupport)` - Check if GPU clock speed metric is supported
- `bool IsSupportedGPUTemperature(IntPtr pMetricsSupport)` - Check if GPU temperature metric is supported
- `bool IsSupportedGPUPower(IntPtr pMetricsSupport)` - Check if GPU power metric is supported
- `bool IsSupportedGPUFanSpeed(IntPtr pMetricsSupport)` - Check if GPU fan speed metric is supported
- `bool IsSupportedGPUVRAM(IntPtr pMetricsSupport)` - Check if GPU VRAM metric is supported
- `double GetGPUTemperature(IntPtr pMetrics)` - Get GPU temperature in °C
- `double GetGPUUsage(IntPtr pMetrics)` - Get GPU usage percentage
- `int GetGPUClockSpeed(IntPtr pMetrics)` - Get GPU clock speed in MHz
- `int GetGPUVRAMClockSpeed(IntPtr pMetrics)` - Get VRAM clock speed in MHz
- `int GetGPUVRAM(IntPtr pMetrics)` - Get VRAM usage in MB
- `int GetGPUFanSpeed(IntPtr pMetrics)` - Get fan speed in RPM
- `double GetGPUPower(IntPtr pMetrics)` - Get GPU power in Watts

### ADLXPerformanceMonitoringInfo (Combined Performance Monitoring Information)

**Structs:**
- `GPUMetricsSupport` - UsageSupported, ClockSpeedSupported, TemperatureSupported, PowerSupported, FanSpeedSupported, VRAMSupported
- `GPUMetricsSnapshot` - Temperature, Usage, ClockSpeed, VRAMClockSpeed, VRAMUsage, FanSpeed, Power

**Methods:**
- `GPUMetricsSupport GetMetricsSupport(IntPtr pPerfMonServices, IntPtr pGPU)` - Get all metrics support in one call
- `GPUMetricsSnapshot GetCurrentMetrics(IntPtr pPerfMonServices, IntPtr pGPU)` - Get all current metrics in one call

## ClangSharp Code Generation

To generate P/Invoke bindings from ADLX headers:

```powershell
cd ADLXWrapper.New
ClangSharpPInvokeGenerator @ClangSharpConfig.rsp
```

Note: ClangSharp generation will be set up in later stages once the configuration is finalized.

## Architecture

This wrapper uses a layered approach:

1. **Native Layer (ADLXNative.cs):** Manual P/Invoke for DLL loading and entry points
2. **Generated Layer (Generated/):** ClangSharp auto-generated types and structures (future)
3. **VTable Layer (ADLXVTables.cs):** COM-like interface vtable definitions
4. **Wrapper Layer (ADLXApi.cs):** Managed API with IntPtr handles and IDisposable
5. **Helper Layer (ADLXExtensions.cs):** Convenience methods for common operations

## Dependencies

- .NET 9.0
- ClangSharp 18.1.0
- ClangSharp.Interop 20.1.2
- AMD ADLX SDK (in ../ADLX/SDK/)
- AMD GPU drivers with ADLX support (amdadlx64.dll)

## Memory Management

- All ADLX interfaces use COM-like reference counting
- GPU handles returned by `EnumerateGPUs()` should be released with `ADLXHelpers.ReleaseInterface()` when done
- The `ADLXApi` class implements IDisposable for automatic cleanup
- Always use `using` statement or call `Dispose()` explicitly

## Next Steps

- **Stage 4:** Basic tests and validation
- **Stage 5:** Display services tests
- **Stage 6:** GPU services tests
- And more...

## References

- Planning docs: `../.cline/`
- ADLX SDK: `../ADLX/SDK/`
- C samples: `../ADLX/Samples/C/`
- IGCLWrapper reference: `C:\vs-code\IGCLWrapper\`
