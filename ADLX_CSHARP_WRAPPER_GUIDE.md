# ADLX C# Wrapper Guide

## Overview

The ADLXWrapper project provides a comprehensive C# wrapper for the AMD Display Library eXtended (ADLX) SDK using SWIG. This wrapper enables full access to ADLX functionality from C# applications, including GPU management, display control, performance monitoring, and tuning capabilities.

## Project Structure

```
ADLXWrapper/
├── ADLXWrapper/              # Native C++ wrapper DLL project
│   ├── ADLXWrapper.i         # SWIG interface definition
│   ├── ADLXWrapper.vcxproj   # Visual Studio project file
│   ├── ADLXQueryInterface.h/cpp # QueryInterface helper functions
│   └── x64/                     # Build output directory
├── IADLXGPU2Test/              # C# test project
│   ├── ComprehensiveTest.cs     # Full functionality validation
│   ├── SimpleExample.cs         # Basic usage example
│   └── *.cs                     # Generated SWIG wrapper classes
├── ADLX/                       # AMD ADLX SDK
└── test_csharp.bat             # VSCode-compatible build script
```

## Prerequisites

- Windows 10/11
- Visual Studio 2022 Community (or higher)
- AMD graphics drivers with ADLX support
- SWIG 4.3.1 (automatically installed via install_swig.ps1)
- .NET Framework 4.8

## Building the Wrapper

### Option 1: Using the Build Script (Recommended for VSCode)

```batch
.\test_csharp.bat
```

This script:
- Sets up the Visual Studio environment using vcvars64.bat
- Builds the C++ wrapper DLL
- Compiles the C# test project
- Runs the comprehensive functionality test

### Option 2: Using Visual Studio

1. Open `ADLXWrapper.sln` in Visual Studio
2. Build the solution (Ctrl+Shift+B)
3. Run the test project

## Key Features

### 1. Runtime Detection
```csharp
// Check if ADLX is available on the system
bool isAvailable = ADLX.IsADLXRuntimeAvailable();
```

### 2. Enhanced Helper Class
```csharp
var helper = new EnhancedADLXHelper();
var result = helper.Initialize();
if (result == ADLX_RESULT.ADLX_OK)
{
    // ADLX is ready to use
    var systemServices = helper.GetSystemServices();
}
```

### 3. GPU Enumeration and Information
```csharp
var gpuListPtr = ADLX.new_gpuListP_Ptr();
var result = systemServices.GetGPUs(gpuListPtr);
if (result == ADLX_RESULT.ADLX_OK)
{
    var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
    var count = gpuList.Size();
    
    for (uint i = 0; i < count; i++)
    {
        var gpuPtr = ADLX.new_gpuP_Ptr();
        if (gpuList.At(i, gpuPtr) == ADLX_RESULT.ADLX_OK)
        {
            var gpu = ADLX.gpuP_Ptr_value(gpuPtr);
            // Access GPU properties and methods
        }
    }
}
```

### 4. Display Management
```csharp
var displayServicesPtr = ADLX.new_displaySerP_Ptr();
systemServices.GetDisplaysServices(displayServicesPtr);
var displayServices = ADLX.displaySerP_Ptr_value(displayServicesPtr);

// Get display list, configure settings, etc.
```

### 5. Performance Monitoring
```csharp
var perfMonPtr = ADLX.new_performanceP_Ptr();
systemServices.GetPerformanceMonitoringServices(perfMonPtr);
var perfMon = ADLX.performanceP_Ptr_value(perfMonPtr);

// Access GPU metrics, system metrics, etc.
```

### 6. GPU Tuning
```csharp
var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
systemServices.GetGPUTuningServices(tuningServicesPtr);
var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);

// Access manual tuning, auto tuning, preset tuning, etc.
```

## Memory Management

The wrapper uses SWIG-generated pointer management functions:

```csharp
// Create pointers
var ptr = ADLX.new_gpuP_Ptr();

// Use the pointer
// ... operations ...

// Always clean up
ADLX.delete_gpuP_Ptr(ptr);
```

## QueryInterface Support

The wrapper includes helper functions for accessing extended interfaces:

```csharp
// Query for IADLXGPU1 interface
var gpu1 = ADLX.QueryGPU1Interface(gpu);

// Query for IADLXGPU2 interface
var gpu2 = ADLX.QueryGPU2Interface(gpu);

// Check interface support
bool supportsGPU2 = ADLX.SupportsGPU2Interface(gpu);
```

## Error Handling

All ADLX operations return `ADLX_RESULT` values:

```csharp
var result = someOperation();
if (result != ADLX_RESULT.ADLX_OK)
{
    Console.WriteLine($"Operation failed: {result}");
    // Handle error appropriately
}
```

## Callback Support (Director Pattern)

The wrapper supports ADLX event callbacks through SWIG directors:

```csharp
// Implement callback interfaces
public class MyDisplayListener : IADLXDisplayListChangedListener
{
    public override bool OnDisplayListChanged(IADLXDisplayList pNewDisplay)
    {
        // Handle display list changes
        return true;
    }
}
```

## Available Functionality

The wrapper provides access to all ADLX features:

### Display Management
- Display enumeration and properties
- Resolution and refresh rate control
- Color management (gamma, gamut, 3D LUT)
- FreeSync and VRR settings
- HDR and color depth configuration

### GPU Management
- GPU enumeration and information
- Performance monitoring and metrics
- Manual and automatic tuning
- Power management
- Fan control

### 3D Graphics Settings
- Anti-aliasing configuration
- Anisotropic filtering
- Tessellation settings
- Frame rate control
- Image enhancement features

### System Services
- Desktop management
- Application-specific settings
- Event handling and notifications
- I2C communication

## Testing

### Comprehensive Test
Run `ComprehensiveTest.cs` to validate all wrapper functionality:
```batch
.\test_csharp.bat
```

### Simple Example
The `SimpleExample.cs` provides a basic usage demonstration. To run it, call:
```csharp
SimpleExample.RunExample();
```

Or modify `ComprehensiveTest.cs` to call it:
```csharp
static void Main(string[] args)
{
    SimpleExample.RunExample();
}
```

## Troubleshooting

### Common Issues

1. **ADLX Runtime Not Available**
   - Ensure AMD graphics drivers are installed
   - Verify ADLX.dll is present in system PATH

2. **Build Errors**
   - Ensure Visual Studio 2022 is installed
   - Run from Developer Command Prompt or use test_csharp.bat

3. **Access Violations**
   - Always check return values before using pointers
   - Ensure proper cleanup of allocated resources

### Debug Information

The wrapper includes debug helpers:
```csharp
// Get detailed error descriptions
string errorDesc = ADLX.GetADLXErrorDescription(result);

// Validate ADLX installation
var validationResult = ADLX.ValidateADLXInstallation();
```

## Version Information

- SWIG Version: 4.3.1
- ADLX SDK Version: 1.4.0.110
- Target Framework: .NET Framework 4.8
- Platform: x64

## License

This wrapper follows the same licensing terms as the ADLX SDK. See the original ADLX documentation for details.
