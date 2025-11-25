# ADLXWrapper

A comprehensive C# wrapper for AMD's ADLX (AMD Display Library Extensions), enabling .NET developers to interact with AMD GPU features, display settings, desktop configurations, and performance monitoring capabilities.

## 🎯 Migration Status: ClangSharp Implementation Complete!

**NEW**: This project now includes a modern **ClangSharp-based wrapper** (ADLXWrapper.New) alongside the original SWIG implementation!

### Why ClangSharp?

The new ClangSharp-based wrapper offers significant advantages:
- ✅ **Faster Performance** - Direct P/Invoke with struct-based bindings
- ✅ **Better Maintainability** - Simpler architecture, easier to debug
- ✅ **Native .NET Integration** - IDisposable, modern C# patterns
- ✅ **Single Compilation Step** - No multi-layer SWIG generation
- ✅ **Proven Architecture** - Successfully ported from IGCLWrapper project

### Current Status

**ClangSharp Wrapper (ADLXWrapper.New)** - ✅ **Production Ready (70% of ADLX API)**
- ✅ GPU Enumeration & Properties (19 methods)
- ✅ Display Services (6 methods + enumeration)
- ✅ GPU Tuning Services (6 capability checks)
- ✅ Performance Monitoring (15 metrics methods)
- ✅ 66 comprehensive tests
- ✅ Complete VTable pattern established
- ⊙ Additional services ready for incremental addition

**SWIG Wrapper (ADLXWrapper)** - ✅ **Stable, Legacy Support**
- Full ADLX API coverage
- Existing production codebases supported
- Will remain available for compatibility

## Overview

ADLXWrapper provides two implementations for working with ADLX:

### 1. ClangSharp-Based Wrapper (Recommended for New Projects)
- **Modern .NET architecture** with IDisposable pattern
- **Direct P/Invoke** for optimal performance
- **VTable-based COM interop** for interface access
- **Location**: `ADLXWrapper.New/` directory
- **Documentation**: See [ClangSharp README](ADLXWrapper.New/README.md)

### 2. SWIG-Based Wrapper (Legacy/Compatibility)
- **Full API coverage** of ADLX features
- **Automatic code generation** from C++ headers
- **Existing codebase support** maintained
- **Location**: `ADLXWrapper/` directory
- **Documentation**: See sections below

---

## ClangSharp Wrapper Quick Start

### Installation (ClangSharp)

```bash
# Build the ClangSharp wrapper
cd ADLXWrapper.New
dotnet build

# Add to your project
dotnet add reference path/to/ADLXWrapper.New/ADLXWrapper.csproj
```

### Basic Usage (ClangSharp)

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
    var perfMon = adlx.GetPerformanceMonitoringServices();
    var metrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(perfMon, gpus[0]);
    
    var temperature = ADLXPerformanceMonitoringHelpers.GetGPUTemperature(metrics);
    var usage = ADLXPerformanceMonitoringHelpers.GetGPUUsage(metrics);
    
    Console.WriteLine($"\nGPU Metrics:");
    Console.WriteLine($"  Temperature: {temperature:F1}°C");
    Console.WriteLine($"  Usage: {usage:F1}%");
    
    ADLXHelpers.ReleaseInterface(metrics);
    ADLXHelpers.ReleaseInterface(perfMon);
} // Automatic cleanup
```

### Key Features (ClangSharp)

**ADLXApi** - Main wrapper class
- `Initialize()` - Initialize ADLX
- `EnumerateGPUs()` - Get all AMD GPUs
- `GetSystemServices()` - Access system interface
- `GetGPUTuningServices()` - GPU tuning capabilities
- `GetPerformanceMonitoringServices()` - Performance metrics
- `Dispose()` - Cleanup (automatic with `using`)

**Helper Classes**
- `ADLXHelpers` - GPU property access (name, VRAM, vendor, etc.)
- `ADLXDisplayHelpers` - Display enumeration and properties
- `ADLXGPUTuningHelpers` - Tuning capability checks
- `ADLXPerformanceMonitoringHelpers` - Metrics collection
- `ADLXGPUInfo` - Combined information structs
- `ADLXDisplayInfo` - Display information structs

**For complete ClangSharp documentation**, see [ADLXWrapper.New/README.md](ADLXWrapper.New/README.md)

---

## SWIG Wrapper Documentation

[The original SWIG-based documentation remains below for legacy support...]

# ADLXWrapper

A comprehensive C# wrapper for AMD's ADLX (AMD Display Library Extensions), enabling .NET developers to interact with AMD GPU features, display settings, desktop configurations, and performance monitoring capabilities.

## Overview

ADLXWrapper provides two approaches to working with ADLX:
- **Easy Path**: Simplified high-level API using `EnhancedADLXHelper` (recommended for most users)
- **Advanced Path**: Low-level API using `ADLXLoader` for fine-grained control

This wrapper uses SWIG to generate C# bindings from the native ADLX C++ SDK, providing type-safe access to all ADLX functionality.

## Features

✅ **Display Management**
- Query display information (resolution, refresh rate, connector type)
- Configure display settings (color depth, pixel format, scaling)
- Manage desktop layouts and Eyefinity configurations
- Control FreeSync, HDR, and other display features

✅ **GPU Information & Control**
- Enumerate GPUs and query hardware details
- Monitor GPU metrics (temperature, clock speeds, utilization)
- GPU tuning (fan control, overclocking, power management)

✅ **3D Settings**
- Configure anti-aliasing, anisotropic filtering
- Manage tessellation and other 3D graphics settings

✅ **Event Notifications**
- Display list changed events
- GPU connection/disconnection events
- Application-specific GPU events

✅ **Automatic Versioning**
- SemVer versioning embedded in DLL and C# bindings
- Version information accessible at runtime
- Git commit tracking for patch versions

## Prerequisites

- **Hardware**: AMD GPU with ADLX support (Radeon RX 5000 series or newer recommended)
- **Operating System**: Windows 10/11 (64-bit)
- **AMD Drivers**: Latest Adrenalin drivers installed
- **Development**:
  - Visual Studio 2022 or later (for building)
  - .NET 9.0 or later (for C# projects)
  - PowerShell (for setup scripts)

## Quick Start

### 1. Build Instructions

#### Step 1: Prepare the Environment
Open PowerShell and navigate to the repository root:

```powershell
powershell.exe -ExecutionPolicy Bypass -File prepare_adlx.ps1
```

This script will:
- Install SWIG (if not already installed)
- Download the AMD ADLX SDK

#### Step 2: Update the Version (optional)
If you make changes to the ADLXWrapper DLL codebase, you can increment the version number for the ADLXWrapper DLL that is generated. To do this you should edit the `VERSION` file to set your desired MAJOR version number and MINOR version number.

The MAJOR and MINOR version numbers are used to generate the version information that is embedded into the ADLXWrapper DLL and also used to generate the C# binding files. The PATCH version number is automatically generated based on the current Git commit count.

#### Step 3: Build the Wrapper
Run the build script:

```powershell
.\rebuild_adlx.ps1
```

Or if you need to bypass execution policy:

```powershell
powershell -ExecutionPolicy Bypass -File rebuild_adlx.ps1
```

This generates:
- `ADLXWrapper.dll` (64-bit) in `x64/Debug/`
- C# binding files in `ADLXWrapper/cs_bindings/*.cs`

### 2. Integration into Your C# Project

#### Step 1: Copy Files
1. Copy `ADLXWrapper.dll` to your C# project root folder
2. Copy the entire `cs_bindings` folder to your project

#### Step 2: Configure DLL
- Right-click `ADLXWrapper.dll` in Visual Studio Solution Explorer
- Select **Properties**
- Set **Copy to Output Directory** to `Copy if newer`

#### Step 3: Add Using Statement
```csharp
using ADLXWrapper;
```

## Usage Examples

### Easy Path (Recommended)

The `EnhancedADLXHelper` class provides a simplified, high-level API that handles initialization, resource management, and cleanup automatically.

#### Basic Initialization

```csharp
using ADLXWrapper;

class Program
{
    static void Main()
    {
        // Create the helper
        var helper = new EnhancedADLXHelper();
        
        try
        {
            // Initialize ADLX
            ADLX_RESULT result = helper.Initialize();
            if (result != ADLX_RESULT.ADLX_OK)
            {
                Console.WriteLine($"Failed to initialize ADLX: {result}");
                return;
            }
            
            Console.WriteLine("ADLX initialized successfully!");
            
            // Get system services
            IADLXSystem system = helper.GetSystemServices();
            if (system == null)
            {
                Console.WriteLine("Failed to get system services");
                return;
            }
            
            // Use ADLX functionality here...
            
        }
        finally
        {
            // Clean up
            helper.Terminate();
            helper.Dispose();
        }
    }
}
```

#### Enumerate GPUs

```csharp
void EnumerateGPUs(IADLXSystem system)
{
    // Create pointer for GPU list
    var gpuListPtr = ADLX.new_gpuListP_Ptr();
    
    try
    {
        // Get the list of GPUs
        ADLX_RESULT result = system.GetGPUs(gpuListPtr);
        if (result != ADLX_RESULT.ADLX_OK)
        {
            Console.WriteLine($"Failed to get GPU list: {result}");
            return;
        }
        
        IADLXGPUList gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
        if (gpuList == null)
        {
            Console.WriteLine("GPU list is null");
            return;
        }
        
        // Get number of GPUs
        var sizePtr = ADLX.new_adlx_uintP();
        gpuList.Size(sizePtr);
        uint gpuCount = ADLX.adlx_uintP_value(sizePtr);
        ADLX.delete_adlx_uintP(sizePtr);
        
        Console.WriteLine($"Found {gpuCount} GPU(s)");
        
        // Iterate through GPUs
        for (uint i = 0; i < gpuCount; i++)
        {
            var gpuPtr = ADLX.new_gpuP_Ptr();
            gpuList.At(i, gpuPtr);
            IADLXGPU gpu = ADLX.gpuP_Ptr_value(gpuPtr);
            
            if (gpu != null)
            {
                // Get GPU name
                var namePtr = ADLX.new_charP_Ptr();
                gpu.Name(namePtr);
                string gpuName = ADLX.charP_Ptr_value(namePtr);
                ADLX.delete_charP_Ptr(namePtr);
                
                // Get VRAM size
                var vramPtr = ADLX.new_adlx_uintP();
                gpu.VRAMType(vramPtr);
                uint vramSizeMB = ADLX.adlx_uintP_value(vramPtr);
                ADLX.delete_adlx_uintP(vramPtr);
                
                Console.WriteLine($"  GPU {i}: {gpuName} (VRAM: {vramSizeMB} MB)");
                
                ADLX.delete_gpuP_Ptr(gpuPtr);
            }
        }
        
    }
    finally
    {
        ADLX.delete_gpuListP_Ptr(gpuListPtr);
    }
}
```

#### Access Desktop Layout

```csharp
void GetDesktopLayout(IADLXSystem system)
{
    // Get desktop services
    var desktopServicesPtr = ADLX.new_desktopSerP_Ptr();
    
    try
    {
        ADLX_RESULT result = system.GetDesktopsServices(desktopServicesPtr);
        if (result != ADLX_RESULT.ADLX_OK)
        {
            Console.WriteLine($"Failed to get desktop services: {result}");
            return;
        }
        
        IADLXDesktopServices desktopServices = ADLX.desktopSerP_Ptr_value(desktopServicesPtr);
        if (desktopServices == null)
        {
            Console.WriteLine("Desktop services is null");
            return;
        }
        
        // Get number of desktops
        var numDesktopsPtr = ADLX.new_adlx_uintP();
        desktopServices.GetNumberOfDesktops(numDesktopsPtr);
        uint numDesktops = ADLX.adlx_uintP_value(numDesktopsPtr);
        ADLX.delete_adlx_uintP(numDesktopsPtr);
        
        Console.WriteLine($"Number of desktops: {numDesktops}");
        
        // Get desktop list
        var desktopListPtr = ADLX.new_desktopListP_Ptr();
        result = desktopServices.GetDesktops(desktopListPtr);
        
        if (result == ADLX_RESULT.ADLX_OK)
        {
            IADLXDesktopList desktopList = ADLX.desktopListP_Ptr_value(desktopListPtr);
            
            if (desktopList != null)
            {
                var sizePtr = ADLX.new_adlx_uintP();
                desktopList.Size(sizePtr);
                uint desktopCount = ADLX.adlx_uintP_value(sizePtr);
                ADLX.delete_adlx_uintP(sizePtr);
                
                // Enumerate each desktop
                for (uint i = 0; i < desktopCount; i++)
                {
                    var desktopPtr = ADLX.new_desktopP_Ptr();
                    desktopList.At(i, desktopPtr);
                    IADLXDesktop desktop = ADLX.desktopP_Ptr_value(desktopPtr);
                    
                    if (desktop != null)
                    {
                        // Get desktop type
                        var typePtr = ADLX.new_adlx_desktopTypeP();
                        desktop.Type(typePtr);
                        ADLX_DESKTOP_TYPE desktopType = ADLX.adlx_desktopTypeP_value(typePtr);
                        ADLX.delete_adlx_desktopTypeP(typePtr);
                        
                        Console.WriteLine($"  Desktop {i}: Type = {desktopType}");
                    }
                    
                    ADLX.delete_desktopP_Ptr(desktopPtr);
                }
            }
        }
        
        ADLX.delete_desktopListP_Ptr(desktopListPtr);
    }
    finally
    {
        ADLX.delete_desktopSerP_Ptr(desktopServicesPtr);
    }
}
```

#### Query Display Information

```csharp
void GetDisplayInformation(IADLXSystem system)
{
    // Get display services
    var displayServicesPtr = ADLX.new_displaySerP_Ptr();
    
    try
    {
        ADLX_RESULT result = system.GetDisplaysServices(displayServicesPtr);
        if (result != ADLX_RESULT.ADLX_OK)
        {
            Console.WriteLine($"Failed to get display services: {result}");
            return;
        }
        
        IADLXDisplayServices displayServices = ADLX.displaySerP_Ptr_value(displayServicesPtr);
        if (displayServices == null)
        {
            Console.WriteLine("Display services is null");
            return;
        }
        
        // Get display list
        var displayListPtr = ADLX.new_displayListP_Ptr();
        result = displayServices.GetDisplays(displayListPtr);
        
        if (result != ADLX_RESULT.ADLX_OK)
        {
            Console.WriteLine($"Failed to get display list: {result}");
            ADLX.delete_displayListP_Ptr(displayListPtr);
            return;
        }
        
        IADLXDisplayList displayList = ADLX.displayListP_Ptr_value(displayListPtr);
        if (displayList == null)
        {
            Console.WriteLine("Display list is null");
            ADLX.delete_displayListP_Ptr(displayListPtr);
            return;
        }
        
        // Get number of displays
        var sizePtr = ADLX.new_adlx_uintP();
        displayList.Size(sizePtr);
        uint displayCount = ADLX.adlx_uintP_value(sizePtr);
        ADLX.delete_adlx_uintP(sizePtr);
        
        Console.WriteLine($"Found {displayCount} display(s)");
        
        // Enumerate displays
        for (uint i = 0; i < displayCount; i++)
        {
            var displayPtr = ADLX.new_displayP_Ptr();
            displayList.At(i, displayPtr);
            IADLXDisplay display = ADLX.displayP_Ptr_value(displayPtr);
            
            if (display != null)
            {
                // Get display name
                var namePtr = ADLX.new_charP_Ptr();
                display.Name(namePtr);
                string displayName = ADLX.charP_Ptr_value(namePtr);
                ADLX.delete_charP_Ptr(namePtr);
                
                // Get native resolution
                var widthPtr = ADLX.new_adlx_intP();
                var heightPtr = ADLX.new_adlx_intP();
                display.NativeResolution(widthPtr, heightPtr);
                int width = ADLX.adlx_intP_value(widthPtr);
                int height = ADLX.adlx_intP_value(heightPtr);
                ADLX.delete_adlx_intP(widthPtr);
                ADLX.delete_adlx_intP(heightPtr);
                
                // Get refresh rate
                var refreshRatePtr = ADLX.new_doubleP();
                display.RefreshRate(refreshRatePtr);
                double refreshRate = ADLX.doubleP_value(refreshRatePtr);
                ADLX.delete_doubleP(refreshRatePtr);
                
                Console.WriteLine($"  Display {i}: {displayName}");
                Console.WriteLine($"    Resolution: {width}x{height} @ {refreshRate}Hz");
            }
            
            ADLX.delete_displayP_Ptr(displayPtr);
        }
        
        ADLX.delete_displayListP_Ptr(displayListPtr);
    }
    finally
    {
        ADLX.delete_displaySerP_Ptr(displayServicesPtr);
    }
}
```

#### Monitor GPU Performance

```csharp
void MonitorGPUPerformance(IADLXSystem system, IADLXGPU gpu)
{
    // Get performance monitoring services
    var perfServicesPtr = ADLX.new_performanceP_Ptr();
    
    try
    {
        ADLX_RESULT result = system.GetPerformanceMonitoringServices(perfServicesPtr);
        if (result != ADLX_RESULT.ADLX_OK)
        {
            Console.WriteLine($"Failed to get performance monitoring: {result}");
            return;
        }
        
        IADLXPerformanceMonitoringServices perfServices = ADLX.performanceP_Ptr_value(perfServicesPtr);
        if (perfServices == null)
        {
            Console.WriteLine("Performance services is null");
            return;
        }
        
        // Get current metrics
        var metricsPtr = ADLX.new_metricsP_Ptr();
        result = perfServices.GetCurrentGPUMetrics(gpu, metricsPtr);
        
        if (result == ADLX_RESULT.ADLX_OK)
        {
            IADLXGPUMetrics metrics = ADLX.metricsP_Ptr_value(metricsPtr);
            
            if (metrics != null)
            {
                // Get GPU temperature
                var tempPtr = ADLX.new_doubleP();
                metrics.GPUTemperature(tempPtr);
                double temperature = ADLX.doubleP_value(tempPtr);
                ADLX.delete_doubleP(tempPtr);
                
                // Get GPU usage
                var usagePtr = ADLX.new_doubleP();
                metrics.GPUUsage(usagePtr);
                double usage = ADLX.doubleP_value(usagePtr);
                ADLX.delete_doubleP(usagePtr);
                
                // Get GPU clock speed
                var clockPtr = ADLX.new_adlx_intP();
                metrics.GPUClockSpeed(clockPtr);
                int clockSpeed = ADLX.adlx_intP_value(clockPtr);
                ADLX.delete_adlx_intP(clockPtr);
                
                Console.WriteLine($"GPU Performance:");
                Console.WriteLine($"  Temperature: {temperature:F1}°C");
                Console.WriteLine($"  Usage: {usage:F1}%");
                Console.WriteLine($"  Clock Speed: {clockSpeed} MHz");
            }
        }
        
        ADLX.delete_metricsP_Ptr(metricsPtr);
    }
    finally
    {
        ADLX.delete_performanceP_Ptr(perfServicesPtr);
    }
}
```

#### Complete Easy Path Example

```csharp
using System;
using ADLXWrapper;

namespace ADLXExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ADLX Wrapper Example (Easy Path) ===\n");
            
            var helper = new EnhancedADLXHelper();
            
            try
            {
                // Initialize
                Console.WriteLine("Initializing ADLX...");
                ADLX_RESULT result = helper.Initialize();
                
                if (result != ADLX_RESULT.ADLX_OK)
                {
                    Console.WriteLine($"Failed to initialize: {result}");
                    return;
                }
                
                if (!helper.IsInitialized())
                {
                    Console.WriteLine("ADLX not initialized properly");
                    return;
                }
                
                Console.WriteLine("ADLX initialized successfully!\n");
                
                // Get system services
                IADLXSystem system = helper.GetSystemServices();
                if (system == null)
                {
                    Console.WriteLine("Failed to get system services");
                    return;
                }
                
                // Query system information
                EnumerateGPUs(system);
                Console.WriteLine();
                
                GetDesktopLayout(system);
                Console.WriteLine();
                
                GetDisplayInformation(system);
                Console.WriteLine();
                
                // Get first GPU and monitor performance
                var gpuListPtr = ADLX.new_gpuListP_Ptr();
                system.GetGPUs(gpuListPtr);
                IADLXGPUList gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
                
                if (gpuList != null)
                {
                    var gpuPtr = ADLX.new_gpuP_Ptr();
                    gpuList.At(0, gpuPtr);
                    IADLXGPU gpu = ADLX.gpuP_Ptr_value(gpuPtr);
                    
                    if (gpu != null)
                    {
                        MonitorGPUPerformance(system, gpu);
                    }
                    
                    ADLX.delete_gpuP_Ptr(gpuPtr);
                }
                ADLX.delete_gpuListP_Ptr(gpuListPtr);
                
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
            finally
            {
                // Clean up
                Console.WriteLine("\nTerminating ADLX...");
                helper.Terminate();
                helper.Dispose();
                Console.WriteLine("Done!");
            }
        }
        
        // Include the helper methods shown above:
        // - EnumerateGPUs()
        // - GetDesktopLayout()
        // - GetDisplayInformation()
        // - MonitorGPUPerformance()
    }
}
```

### Advanced Path

The `ADLXLoader` class provides low-level control over DLL loading and ADLX initialization, suitable for advanced scenarios like ADL/ADLX interop or custom initialization.

#### Advanced Initialization with Full Control

```csharp
using System;
using ADLXWrapper;

class AdvancedExample
{
    static void Main()
    {
        Console.WriteLine("=== ADLX Wrapper Example (Advanced Path) ===\n");
        
        // Create loader
        var loader = new ADLXLoader();
        IADLXSystem system = null;
        
        try
        {
            // Load ADLX DLL
            Console.WriteLine("Loading ADLX DLL...");
            if (!loader.Load())
            {
                Console.WriteLine("Failed to load ADLX DLL");
                Console.WriteLine("Make sure AMD drivers and ADLX runtime are installed");
                return;
            }
            
            Console.WriteLine("ADLX DLL loaded successfully!");
            
            // Query version
            var versionStringPtr = ADLX.new_charP_Ptr();
            ADLX_RESULT result = loader.QueryVersion(versionStringPtr);
            
            if (result == ADLX_RESULT.ADLX_OK)
            {
                string version = ADLX.charP_Ptr_value(versionStringPtr);
                Console.WriteLine($"ADLX Version: {version}");
            }
            ADLX.delete_charP_Ptr(versionStringPtr);
            
            // Query full version (64-bit)
            var fullVersionPtr = ADLX.new_adlx_uint64P();
            result = loader.QueryFullVersion(fullVersionPtr);
            
            if (result == ADLX_RESULT.ADLX_OK)
            {
                ulong fullVersion = ADLX.adlx_uint64P_value(fullVersionPtr);
                Console.WriteLine($"ADLX Full Version: 0x{fullVersion:X16}\n");
            }
            ADLX.delete_adlx_uint64P(fullVersionPtr);
            
            // Initialize ADLX
            var systemPtr = ADLX.new_systemP_Ptr();
            ulong adlxVersion = ADLX.MAKE_FULL_VERSION(
                ADLX.ADLX_VER_MAJOR, 
                ADLX.ADLX_VER_MINOR
            );
            
            Console.WriteLine("Initializing ADLX...");
            result = loader.Initialize(adlxVersion, systemPtr);
            
            if (result != ADLX_RESULT.ADLX_OK)
            {
                Console.WriteLine($"Failed to initialize ADLX: {result}");
                ADLX.delete_systemP_Ptr(systemPtr);
                return;
            }
            
            system = ADLX.systemP_Ptr_value(systemPtr);
            if (system == null)
            {
                Console.WriteLine("System interface is null");
                ADLX.delete_systemP_Ptr(systemPtr);
                return;
            }
            
            Console.WriteLine("ADLX initialized successfully!\n");
            
            // Use ADLX functionality...
            // (Use the same helper methods as in Easy Path)
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
            
            // Clean up
            ADLX.delete_systemP_Ptr(systemPtr);
            
        }
        finally
        {
            // Terminate and unload
            Console.WriteLine("\nTerminating ADLX...");
            loader.Terminate();
            loader.Unload();
            loader.Dispose();
            Console.WriteLine("Done!");
        }
    }
}
```

#### Using IADLXSystem2 Features with Hardware/Driver Detection

The `IADLXSystem2` interface provides advanced features like multimedia services and GPU application list change handling. However, not all AMD GPUs or driver versions support these features. Here's how to check for support and gracefully handle unsupported hardware:

```csharp
void UseSystem2Features(IADLXSystem system)
{
    // Check if IADLXSystem2 is supported
    bool supportsSystem2 = ADLX.SupportsSystem2Interface(system);
    
    if (!supportsSystem2)
    {
        Console.WriteLine("IADLXSystem2 interface not supported on this hardware/driver");
        Console.WriteLine("Please update your AMD drivers or check GPU compatibility");
        return;
    }
    
    // Upgrade to System2 interface
    IADLXSystem2 system2 = ADLX.QuerySystem2Interface(system);
    if (system2 == null)
    {
        Console.WriteLine("Failed to query IADLXSystem2 interface");
        return;
    }
    
    try
    {
        Console.WriteLine("IADLXSystem2 interface is supported!");
        
        // Access Multimedia Services (System2 feature)
        AccessMultimediaServices(system2);
        
        // Setup GPU App List Change Notifications (System2 feature)
        SetupGPUAppListChangedHandling(system2);
    }
    finally
    {
        // Clean up if needed
        // Note: Don't dispose the original system interface
    }
}

void AccessMultimediaServices(IADLXSystem2 system2)
{
    Console.WriteLine("\n--- Accessing Multimedia Services (IADLXSystem2 feature) ---");
    
    // Create pointer for multimedia services
    var multimediaServicesPtr = ADLX.new_voidP_Ptr();
    
    try
    {
        // This is a System2-only feature
        ADLX_RESULT result = system2.GetMultimediaServices(multimediaServicesPtr);
        
        if (result == ADLX_RESULT.ADLX_OK)
        {
            Console.WriteLine("Multimedia services retrieved successfully!");
            // Use multimedia services here...
        }
        else if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            Console.WriteLine("Multimedia services not supported on this GPU");
            Console.WriteLine("This feature requires newer AMD GPUs (RDNA 2 or later)");
        }
        else
        {
            Console.WriteLine($"Failed to get multimedia services: {result}");
            string errorDesc = ADLX.GetADLXErrorDescription(result);
            Console.WriteLine($"Error: {errorDesc}");
        }
    }
    finally
    {
        ADLX.delete_voidP_Ptr(multimediaServicesPtr);
    }
}

void SetupGPUAppListChangedHandling(IADLXSystem2 system2)
{
    Console.WriteLine("\n--- Setting up GPU App List Changed Handling (IADLXSystem2 feature) ---");
    
    // Create pointer for GPU app list changed handling
    var appListHandlingPtr = ADLX.new_voidP_Ptr();
    
    try
    {
        // This is a System2-only feature
        ADLX_RESULT result = system2.GetGPUAppsListChangedHandling(appListHandlingPtr);
        
        if (result == ADLX_RESULT.ADLX_OK)
        {
            Console.WriteLine("GPU App List Changed Handling retrieved successfully!");
            // Register your custom event listener here...
        }
        else if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            Console.WriteLine("GPU App List Changed Handling not supported");
            Console.WriteLine("This feature requires specific driver versions");
        }
        else
        {
            Console.WriteLine($"Failed to get GPU app list handling: {result}");
            string errorDesc = ADLX.GetADLXErrorDescription(result);
            Console.WriteLine($"Error: {errorDesc}");
        }
    }
    finally
    {
        ADLX.delete_voidP_Ptr(appListHandlingPtr);
    }
}
```

#### Complete Example: Graceful Feature Detection

```csharp
using System;
using ADLXWrapper;

class FeatureDetectionExample
{
    static void Main()
    {
        Console.WriteLine("=== ADLX Feature Detection Example ===\n");
        
        var helper = new EnhancedADLXHelper();
        
        try
        {
            // Initialize
            ADLX_RESULT result = helper.Initialize();
            if (result != ADLX_RESULT.ADLX_OK)
            {
                Console.WriteLine($"Failed to initialize: {result}");
                return;
            }
            
            IADLXSystem system = helper.GetSystemServices();
            if (system == null)
            {
                Console.WriteLine("Failed to get system services");
                return;
            }
            
            // Detect interface versions
            DetectInterfaceVersions(system);
            
            // Try to use System2 features with graceful fallback
            Console.WriteLine("\n=== Attempting to use IADLXSystem2 Features ===");
            UseSystem2Features(system);
            
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
        finally
        {
            helper.Terminate();
            helper.Dispose();
        }
    }
    
    static void DetectInterfaceVersions(IADLXSystem system)
    {
        Console.WriteLine("=== Detecting ADLX Interface Support ===\n");
        
        // Check System interface versions
        bool supportsSystem1 = ADLX.SupportsSystem1Interface(system);
        bool supportsSystem2 = ADLX.SupportsSystem2Interface(system);
        
        Console.WriteLine($"IADLXSystem (base):  Supported");
        Console.WriteLine($"IADLXSystem1:        {(supportsSystem1 ? "Supported ✓" : "Not Supported ✗")}");
        Console.WriteLine($"IADLXSystem2:        {(supportsSystem2 ? "Supported ✓" : "Not Supported ✗")}");
        
        if (!supportsSystem2)
        {
            Console.WriteLine("\nℹ️  Note: IADLXSystem2 features require:");
            Console.WriteLine("   - AMD Radeon RX 6000 series or newer GPU");
            Console.WriteLine("   - AMD Adrenalin driver 23.2.1 or newer");
            Console.WriteLine("   - Some features may require specific GPU models");
        }
        
        // Check GPU interface versions
        var gpuListPtr = ADLX.new_gpuListP_Ptr();
        try
        {
            system.GetGPUs(gpuListPtr);
            IADLXGPUList gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
            
            if (gpuList != null)
            {
                var sizePtr = ADLX.new_adlx_uintP();
                gpuList.Size(sizePtr);
                uint gpuCount = ADLX.adlx_uintP_value(sizePtr);
                ADLX.delete_adlx_uintP(sizePtr);
                
                Console.WriteLine($"\n=== GPU Interface Support ({gpuCount} GPU(s)) ===");
                
                for (uint i = 0; i < gpuCount; i++)
                {
                    var gpuPtr = ADLX.new_gpuP_Ptr();
                    gpuList.At(i, gpuPtr);
                    IADLXGPU gpu = ADLX.gpuP_Ptr_value(gpuPtr);
                    
                    if (gpu != null)
                    {
                        // Get GPU name
                        var namePtr = ADLX.new_charP_Ptr();
                        gpu.Name(namePtr);
                        string gpuName = ADLX.charP_Ptr_value(namePtr);
                        ADLX.delete_charP_Ptr(namePtr);
                        
                        bool supportsGPU1 = ADLX.SupportsGPU1Interface(gpu);
                        bool supportsGPU2 = ADLX.SupportsGPU2Interface(gpu);
                        
                        Console.WriteLine($"\nGPU {i}: {gpuName}");
                        Console.WriteLine($"  IADLXGPU (base):  Supported");
                        Console.WriteLine($"  IADLXGPU1:        {(supportsGPU1 ? "Supported ✓" : "Not Supported ✗")}");
                        Console.WriteLine($"  IADLXGPU2:        {(supportsGPU2 ? "Supported ✓" : "Not Supported ✗")}");
                    }
                    
                    ADLX.delete_gpuP_Ptr(gpuPtr);
                }
            }
        }
        finally
        {
            ADLX.delete_gpuListP_Ptr(gpuListPtr);
        }
    }
    
    // Include UseSystem2Features, AccessMultimediaServices, 
    // and SetupGPUAppListChangedHandling methods from above
}
```

#### Safe Feature Usage Pattern

Here's a reusable pattern for safely accessing version-specific features:

```csharp
public class SafeADLXFeatures
{
    /// <summary>
    /// Safely executes an action that requires IADLXSystem2, with fallback handling
    /// </summary>
    public static bool TryUseSystem2Feature(
        IADLXSystem system,
        Action<IADLXSystem2> system2Action,
        Action fallbackAction = null)
    {
        // Check support first
        if (!ADLX.SupportsSystem2Interface(system))
        {
            Console.WriteLine("IADLXSystem2 not supported - using fallback");
            fallbackAction?.Invoke();
            return false;
        }
        
        // Query the interface
        IADLXSystem2 system2 = ADLX.QuerySystem2Interface(system);
        if (system2 == null)
        {
            Console.WriteLine("Failed to query IADLXSystem2 - using fallback");
            fallbackAction?.Invoke();
            return false;
        }
        
        try
        {
            // Execute the System2-specific action
            system2Action(system2);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error using System2 feature: {ex.Message}");
            fallbackAction?.Invoke();
            return false;
        }
    }
    
    /// <summary>
    /// Safely executes an action that requires IADLXGPU2, with fallback handling
    /// </summary>
    public static bool TryUseGPU2Feature(
        IADLXGPU gpu,
        Action<IADLXGPU2> gpu2Action,
        Action fallbackAction = null)
    {
        // Check support first
        if (!ADLX.SupportsGPU2Interface(gpu))
        {
            Console.WriteLine("IADLXGPU2 not supported - using fallback");
            fallbackAction?.Invoke();
            return false;
        }
        
        // Query the interface
        IADLXGPU2 gpu2 = ADLX.QueryGPU2Interface(gpu);
        if (gpu2 == null)
        {
            Console.WriteLine("Failed to query IADLXGPU2 - using fallback");
            fallbackAction?.Invoke();
            return false;
        }
        
        try
        {
            // Execute the GPU2-specific action
            gpu2Action(gpu2);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error using GPU2 feature: {ex.Message}");
            fallbackAction?.Invoke();
            return false;
        }
    }
}

// Usage example:
void UseFeatureSafely(IADLXSystem system)
{
    // Try to use System2 multimedia features
    bool success = SafeADLXFeatures.TryUseSystem2Feature(
        system,
        system2 =>
        {
            Console.WriteLine("Using System2 multimedia features");
            // Access multimedia services here...
        },
        () =>
        {
            Console.WriteLine("Multimedia features not available");
            Console.WriteLine("Continuing with basic functionality...");
        }
    );
    
    if (success)
    {
        Console.WriteLine("Successfully used System2 features!");
    }
}
```

#### Custom Listener for Display Changes

```csharp
using System;
using ADLXWrapper;

// Custom listener class for display list changes
public class MyDisplayListener : IADLXDisplayListChangedListener
{
    public override bool OnDisplayListChanged(IADLXDisplayList pNewDisplay)
    {
        Console.WriteLine("Display list changed!");
        
        if (pNewDisplay != null)
        {
            var sizePtr = ADLX.new_adlx_uintP();
            pNewDisplay.Size(sizePtr);
            uint displayCount = ADLX.adlx_uintP_value(sizePtr);
            ADLX.delete_adlx_uintP(sizePtr);
            
            Console.WriteLine($"New display count: {displayCount}");
        }
        
        return true; // Return true to continue receiving events
    }
}

// Usage
void RegisterDisplayChangeListener(IADLXSystem system)
{
    var displayServicesPtr = ADLX.new_displaySerP_Ptr();
    system.GetDisplaysServices(displayServicesPtr);
    IADLXDisplayServices displayServices = ADLX.displaySerP_Ptr_value(displayServicesPtr);
    
    if (displayServices != null)
    {
        var changeHandlingPtr = ADLX.new_displayChangeHandlP_Ptr();
        displayServices.GetDisplayChangedHandling(changeHandlingPtr);
        IADLXDisplayChangedHandling changeHandling = ADLX.displayChangeHandlP_Ptr_value(changeHandlingPtr);
        
        if (changeHandling != null)
        {
            var listener = new MyDisplayListener();
            changeHandling.AddDisplayListEventListener(listener);
            
            Console.WriteLine("Display change listener registered");
            Console.WriteLine("Waiting for display changes... (press any key to stop)");
            Console.ReadKey();
            
            // Unregister
            changeHandling.RemoveDisplayListEventListener(listener);
        }
        
        ADLX.delete_displayChangeHandlP_Ptr(changeHandlingPtr);
    }
    
    ADLX.delete_displaySerP_Ptr(displayServicesPtr);
}
```

## Memory Management

### Important Notes

1. **Always clean up pointers**: Use `delete_*` functions for pointers created with `new_*` functions
2. **Release interfaces**: Call `Release()` on ADLX interfaces when done (though C# GC usually handles this)
3. **Terminate before exit**: Always call `Terminate()` before application shutdown

### Pattern for Safe Memory Management

```csharp
// Create pointer
var ptr = ADLX.new_someTypeP_Ptr();

try
{
    // Use pointer
    // ...
}
finally
{
    // Always clean up
    ADLX.delete_someTypeP_Ptr(ptr);
}
```

## Testing

The ADLXWrapper includes a comprehensive test suite with over 37 tests covering all major ADLX features.

### Standard Tests (Default)

**IMPORTANT**: Unit tests require AMD GPU hardware to run successfully.

Run standard tests using:
```powershell
.\test_adlx.ps1
```

Or if you need to bypass execution policy:

```powershell
powershell -ExecutionPolicy Bypass -File test_adlx.ps1
```

Or from the test project directory:
```bash
cd ADLXWrapper.Tests
dotnet test --verbosity detailed
```

### Test Categories

The test suite includes several categories:

1. **Initialization Tests** - ADLX runtime detection and initialization
2. **GPU Tests** - GPU enumeration, properties, interface versions
3. **Display Tests** - Display enumeration and properties
4. **Desktop Tests** - Desktop services and Eyefinity detection
5. **Performance Tests** - GPU metrics and monitoring
6. **Tuning Tests** - GPU tuning support detection (read-only)
7. **System2 Tests** - Advanced features for newer GPUs

### Optional Tests That Modify Configuration

Some tests are **excluded by default** because they modify your system configuration. These tests are marked with special category traits.

#### CreateEyefinity Tests

Tests in the `CreateEyefinity` category will temporarily modify your display configuration by creating and destroying Eyefinity desktops.

**⚠️ These tests will:**
- Temporarily reconfigure your displays (may go black briefly)
- May reposition open windows
- Require 2+ compatible displays
- Restore original configuration when complete

**To run CreateEyefinity tests:**

```bash
# Run ONLY CreateEyefinity tests
cd ADLXWrapper.Tests
dotnet test --filter "Category=CreateEyefinity" --verbosity detailed

# View just the test names
dotnet test --filter "Category=CreateEyefinity" --list-tests
```

**From Visual Studio:**
1. Open Test Explorer (`Test` → `Test Explorer`)
2. Find: `DesktopTests` → `Optional_Test_Eyefinity_Create_And_Restore`
3. Right-click and select `Run` or `Debug`
4. Watch test output for status and warnings

**To exclude from normal runs:**
```bash
# Run all tests EXCEPT CreateEyefinity (default behavior)
dotnet test --filter "Category!=CreateEyefinity"
```

For more details on optional tests, see:
- [`ADLXWrapper.Tests/README.md`](ADLXWrapper.Tests/README.md) - Comprehensive test documentation
- [`ADLXWrapper.Tests/OPTIONAL_TESTS.md`](ADLXWrapper.Tests/OPTIONAL_TESTS.md) - Optional test category guide

## Troubleshooting

### ADLX Initialization Fails

**Problem**: `Initialize()` returns `ADLX_FAIL` or other error codes

**Solutions**:
1. Ensure AMD Adrenalin drivers are installed (version 21.10.1 or newer)
2. Verify you have a compatible AMD GPU (RX 5000 series or newer)
3. Check if ADLX runtime DLL (`amdadlx64.dll`) is in system PATH
4. Try running as Administrator
5. Use `ValidateADLXInstallation()` helper function

```csharp
if (!ADLX.IsADLXRuntimeAvailable())
{
    Console.WriteLine("ADLX runtime is not available");
    Console.WriteLine("Please install AMD Adrenalin drivers");
}

ADLX_RESULT validation = ADLX.ValidateADLXInstallation();
if (validation != ADLX_RESULT.ADLX_OK)
{
    string error = ADLX.GetADLXErrorDescription(validation);
    Console.WriteLine($"ADLX validation failed: {error}");
}
```

### DLL Not Found

**Problem**: `System.DllNotFoundException` when running application

**Solutions**:
1. Ensure `ADLXWrapper.dll` is in the same directory as your executable
2. Verify DLL is set to "Copy if newer" in project properties
3. Check platform target matches (x64 vs x86)
4. Install Visual C++ Redistributable 2022 (x64)

### Access Violation / Crashes

**Problem**: Application crashes with access violation

**Solutions**:
1. Always check for `null` before using interface pointers
2. Verify `ADLX_RESULT` is `ADLX_OK` before accessing output parameters
3. Don't use interfaces after calling `Terminate()`
4. Match pointer creation with deletion (every `new_*` needs `delete_*`)

## API Documentation

### Key Constants

```csharp
ADLX.ADLX_DLL_NAME_64          // "amdadlx64.dll"
ADLX.ADLX_DLL_NAME_32          // "amdadlx32.dll"
ADLX.ADLX_VER_MAJOR           // Major version number
ADLX.ADLX_VER_MINOR           // Minor version number
```

### Core Result Codes

```csharp
ADLX_RESULT.ADLX_OK                    // Success
ADLX_RESULT.ADLX_FAIL                  // General failure
ADLX_RESULT.ADLX_INVALID_ARGS          // Invalid arguments
ADLX_RESULT.ADLX_NOT_SUPPORTED         // Feature not supported
ADLX_RESULT.ADLX_ALREADY_INITIALIZED   // Already initialized
ADLX_RESULT.ADLX_ALREADY_ENABLED       // Already enabled
```

### Helper Functions

```csharp
// Runtime detection
bool IsADLXRuntimeAvailable();
ADLX_RESULT ValidateADLXInstallation();
string GetADLXErrorDescription(ADLX_RESULT result);

// Interface version detection
bool SupportsGPU1Interface(IADLXGPU gpu);
bool SupportsGPU2Interface(IADLXGPU gpu);
bool SupportsSystem1Interface(IADLXSystem system);
bool SupportsSystem2Interface(IADLXSystem system);

// Interface upgrades
IADLXGPU1 QueryGPU1Interface(IADLXGPU gpu);
IADLXGPU2 QueryGPU2Interface(IADLXGPU gpu);
IADLXSystem1 QuerySystem1Interface(IADLXSystem system);
IADLXSystem2 QuerySystem2Interface(IADLXSystem system);
```

## Project Structure

```
ADLXWrapper/
├── ADLX/                       # AMD ADLX SDK (downloaded by prepare_adlx.ps1)
├── ADLXWrapper/                # Native C++ wrapper project
│   ├── ADLXWrapper.i          # SWIG interface definition
│   ├── ADLXQueryInterface.h   # Query interface helpers
│   ├── cs_bindings/           # Generated C# bindings
│   └── x64/Debug/             # Built native DLL
├── ADLXWrapper.Tests/         # Unit tests
├── prepare_adlx.ps1           # Setup script
├── rebuild_adlx.ps1           # Build script
└── test_adlx.bat             # Test script
```

## Requirements

- **.NET**: 9.0 or later
- **C++ Standard**: C++14 (for building native wrapper)
- **SWIG**: 4.0 or later (installed by `prepare_adlx.ps1`)
- **AMD Drivers**: Adrenalin 21.10.1 or newer
- **Visual Studio**: 2022 or later (for building)

## Contributing

Contributions are welcome! Please feel free to submit issues or pull requests on the GitHub repository.

## License

This project wraps AMD's ADLX SDK. Please refer to AMD's ADLX SDK license for terms and conditions regarding the use of ADLX functionality.

## Resources

- [AMD ADLX SDK Documentation](https://gpuopen.com/adlx/)
- [AMD GPUOpen](https://gpuopen.com/)
- [GitHub Repository](https://github.com/terrymacdonald/ADLXWrapper)

## Support

For issues, questions, or feature requests:
1. Check the [Troubleshooting](#troubleshooting) section
2. Review existing GitHub issues
3. Create a new issue with detailed information about your environment and problem

---

**Note**: This wrapper is provided as-is. Always test thoroughly in your specific environment before production use.