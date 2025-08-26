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
├── IADLXGPU2Test/              # C# test projects
│   ├── IADLXGPU2Test.csproj     # .NET Framework 4.8 project
│   ├── IADLXGPU2Test_Net8.csproj # .NET 8.0 project
│   ├── bin/Debug/               # Build outputs
│   │   ├── ADLXCSharpBind.dll   # .NET Framework 4.8 output
│   │   └── net8.0/              # .NET 8.0 outputs
│   │       └── IADLXGPU2Test_Net8.dll
│   └── *.cs                     # Generated SWIG wrapper classes
├── ADLXNativeTest/             # Native C test applications
│   ├── build.bat               # Direct compiler path method
│   ├── build_amd.bat           # AMD vcvars64.bat method
│   ├── build_fixed.bat         # Dynamic compiler detection
│   ├── build_simple.bat        # Simplified AMD official method
│   ├── build_vscode.bat        # VSCode-optimized method
│   └── main.c / main_amd_simple.c # Test source files
├── ADLX/                       # AMD ADLX SDK
├── rebuild_adlx.bat            # Main C++ wrapper build script
├── test_csharp_netframework48.bat # C# test build and run script (.NET Framework 4.8)
├── test_csharp_net8.bat        # C# test build and run script (.NET 8.0)
└── swigwin/                    # SWIG installation directory
```

## Prerequisites

- Windows 10/11
- Visual Studio 2022 Community (or higher)
- AMD graphics drivers with ADLX support
- Internet connection (for automatic dependency downloads)
- .NET Framework 4.8 and/or .NET 8.0

**Note:** SWIG 4.3.1 and ADLX SDK are automatically downloaded and installed during the build process.

## Build Scripts Reference

### Main Build Scripts

#### `rebuild_adlx.bat`
**Purpose:** Builds the main C++ wrapper DLL (ADLXWrapper.dll)
- Sets up Visual Studio environment using VsDevCmd.bat
- Builds ADLXWrapper.vcxproj using MSBuild
- Copies output DLL to test directory
- **Output:** `x64\Debug\ADLXWrapper.dll`

#### `test_csharp_netframework48.bat`
**Purpose:** Builds and runs the C# test application (.NET Framework 4.8)
- Sets up Visual Studio environment using vcvars64.bat
- Builds IADLXGPU2Test.csproj using MSBuild
- Runs the compiled test executable
- **Output:** `IADLXGPU2Test\bin\Debug\IADLXGPU2Test.exe`

#### `test_csharp_net8.bat`
**Purpose:** Builds and runs the C# test application (.NET 8.0)
- Uses dotnet CLI for .NET 8.0 build and execution
- Verifies .NET 8.0 SDK availability
- Builds IADLXGPU2Test_Net8.csproj using dotnet build
- Runs the compiled test executable using dotnet run
- **Output:** `IADLXGPU2Test\bin\Debug\net8.0\IADLXGPU2Test_Net8.exe`

### Native C Test Scripts (ADLXNativeTest/)

#### `build.bat`
**Purpose:** Direct compiler path method
- Uses hardcoded Visual Studio 2022 compiler path
- Compiles main.c with ADLX helper files
- **Best for:** Systems with known VS2022 installation paths

#### `build_amd.bat`
**Purpose:** AMD vcvars64.bat method
- Uses vcvars64.bat to set up build environment
- Follows AMD's recommended build approach
- **Best for:** Standard AMD development workflow

#### `build_fixed.bat`
**Purpose:** Dynamic compiler detection method
- Automatically finds latest MSVC compiler version
- Most robust method for different VS installations
- **Best for:** Systems with varying VS versions

#### `build_simple.bat`
**Purpose:** Simplified AMD official method
- Uses main_amd_simple.c for basic testing
- Minimal ADLX functionality test
- **Best for:** Diagnosing interface issues

#### `build_vscode.bat`
**Purpose:** VSCode-optimized method
- Designed for VSCode terminal compatibility
- Uses vcvars64.bat with enhanced error checking
- **Best for:** VSCode development environment

## Building the Wrapper

### Option 1: Quick Start (.NET Framework 4.8)

```batch
# Build the C++ wrapper DLL
.\rebuild_adlx.bat

# Build and test C# wrapper (.NET Framework 4.8)
.\test_csharp_netframework48.bat
```

### Option 2: .NET 8.0 Build

```batch
# Build the C++ wrapper DLL first
.\rebuild_adlx.bat

# Build and test C# wrapper (.NET 8.0)
.\test_csharp_net8.bat
```

### Option 3: Using Visual Studio

1. Open `ADLXWrapper.sln` in Visual Studio
2. Build the solution (Ctrl+Shift+B)
3. Run the desired test project

### Option 4: Native C Testing

```batch
# Test different build methods
cd ADLXNativeTest

# Try the most robust method first
.\build_fixed.bat

# Or use VSCode-optimized method
.\build_vscode.bat

# For AMD standard approach
.\build_amd.bat
```

## .NET Version Support

### .NET Framework 4.8
- **Project:** `IADLXGPU2Test.csproj`
- **Output:** `IADLXGPU2Test.exe`
- **Build Script:** `test_csharp_netframework48.bat`
- **DLL Name:** `ADLXCSharpBind.dll`

### .NET 8.0
- **Project:** `IADLXGPU2Test_Net8.csproj`
- **Output:** `IADLXGPU2Test_Net8.dll` / `IADLXGPU2Test_Net8.exe`
- **Build Method:** `dotnet build` / `dotnet run`
- **Dependencies:** Newtonsoft.Json 13.0.3

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

## Advanced Features

### Gamma Ramp JSON Serialization (.NET 8.0)
For .NET 8.0 projects, comprehensive gamma ramp JSON serialization is available. See [ADLX_GAMMA_RAMP_JSON_GUIDE.md](ADLX_GAMMA_RAMP_JSON_GUIDE.md) for detailed usage instructions.

```csharp
// Convert ADLX gamma ramp to JSON
var serializable = gammaRamp.ToSerializable("My Profile");
string json = NewtonsoftJsonUtility.ToJson(serializable);

// Load from JSON
var loaded = NewtonsoftJsonUtility.FromJson(jsonString);
var restored = loaded.ToADLX();
```

## Testing

### Comprehensive Test (.NET Framework 4.8)
```batch
.\test_csharp_netframework48.bat
```

### .NET 8.0 Test
```batch
.\test_csharp_net8.bat
```

### Native C Tests
```batch
cd ADLXNativeTest
.\build_fixed.bat    # Recommended method
```

## Troubleshooting

### Common Issues

1. **ADLX Runtime Not Available**
   - Ensure AMD graphics drivers are installed
   - Verify ADLX.dll is present in system PATH

2. **Build Errors**
   - Ensure Visual Studio 2022 is installed
   - Try different build scripts in ADLXNativeTest/
   - Use `build_fixed.bat` for most robust compilation

3. **Access Violations**
   - Always check return values before using pointers
   - Ensure proper cleanup of allocated resources

4. **Wrong DLL Referenced**
   - .NET Framework 4.8 uses `ADLXCSharpBind.dll`
   - .NET 8.0 uses `ADLXWrapper.dll`
   - Ensure correct DLL is in output directory

### Build Script Selection Guide

- **Most Systems:** Use `build_fixed.bat` (dynamic compiler detection)
- **VSCode Users:** Use `build_vscode.bat` (terminal optimized)
- **AMD Standard:** Use `build_amd.bat` (official AMD method)
- **Known VS Path:** Use `build.bat` (direct compiler path)
- **Troubleshooting:** Use `build_simple.bat` (minimal test)

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
- Target Frameworks: .NET Framework 4.8, .NET 8.0
- Platform: x64
- Visual Studio: 2022 Community (recommended)

## Related Documentation

- [ADLX_GAMMA_RAMP_JSON_GUIDE.md](ADLX_GAMMA_RAMP_JSON_GUIDE.md) - Comprehensive gamma ramp JSON serialization for .NET 8.0
- [README.md](README.md) - Project overview and quick start guide
- [AMD ADLX Documentation](https://gpuopen.com/manuals/adlx/) - Official ADLX SDK documentation
- [AMD ADLX C# Samples](https://gpuopen.com/manuals/adlx/adlx-page_sample_cs/) - Official C# usage examples

## License

This wrapper follows the same licensing terms as the ADLX SDK. See the original ADLX documentation for details.
