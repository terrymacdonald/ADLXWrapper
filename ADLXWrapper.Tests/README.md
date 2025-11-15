# ADLXWrapper.Tests - Comprehensive Test Suite

## Overview

This is a comprehensive test suite for the ADLXWrapper C# bindings for AMD's ADLX SDK. The tests are designed to work on any system (with or without AMD hardware) and will gracefully skip tests that cannot run on the current hardware.

## Test Organization

The test suite is organized into the following test files:

### Core Tests

#### 1. **InitializationTests.cs**
Tests ADLX runtime detection, initialization, versioning, and helper classes.

**Tests:**
- `Test_01_ADLX_Runtime_Availability` - Detects AMD hardware and ADLX runtime
- `Test_02_ADLX_Initialization` - Verifies ADLX initializes correctly
- `Test_03_System_Interface_Versions` - Checks IADLXSystem1/System2 support
- `Test_04_Hardware_Capabilities_Summary` - Displays all detected capabilities
- `Test_05_EnhancedADLXHelper_Initialization` - Tests high-level helper class
- `Test_06_ADLXLoader_LowLevel_Initialization` - Tests low-level DLL loading
- `Test_07_Error_Description_Helper` - Validates error description utility

#### 2. **GPUTests.cs**
Tests GPU enumeration, properties, and interface version detection.

**Tests:**
- `Test_01_Enumerate_GPUs` - Lists all AMD GPUs
- `Test_02_GPU_Basic_Properties` - Reads GPU name, vendor ID, device ID, VRAM, type
- `Test_03_GPU_Interface_Versions` - Checks IADLXGPU1/GPU2 support
- `Test_04_GPU_ASIC_Info` - Reads ASIC family, PCI bus type, lane width
- `Test_05_GPU_Driver_Info` - Reads driver path and PNP string

### Display & Desktop Tests

#### 3. **DisplayTests.cs**
Tests display enumeration, properties, and configuration.

**Tests:**
- `Test_01_Enumerate_Displays` - Lists all connected displays
- `Test_02_Display_Properties` - Reads display name, type, connector type, manufacturer ID
- `Test_03_Display_Resolution` - Reads native resolution and refresh rate
- `Test_04_Display_Pixel_Clock` - Reads pixel clock frequency
- `Test_05_Display_Scan_Type` - Reads progressive/interlaced scan type
- `Test_06_Display_EDID` - Reads EDID manufacturer and product IDs

#### 4. **DesktopTests.cs**
Tests desktop services and Eyefinity support detection.

**Tests:**
- `Test_01_Get_Desktop_Services` - Retrieves desktop services interface
- `Test_02_Enumerate_Desktops` - Lists all desktop configurations
- `Test_03_Desktop_GPU_Association` - Shows which GPU drives each desktop
- `Test_04_Eyefinity_Support_Detection` - Checks if Eyefinity is possible
- `Test_05_Eyefinity_Current_State` - Shows if Eyefinity is currently enabled

### Performance & Tuning Tests

#### 5. **PerformanceMonitoringTests.cs**
Tests GPU metrics and performance monitoring.

**Tests:**
- `Test_01_Get_Performance_Monitoring_Services` - Retrieves performance services
- `Test_02_GPU_Metrics_Support` - Checks which metrics are supported
- `Test_03_GPU_Current_Metrics` - Reads temperature, usage, clock speeds, VRAM, fan, power
- `Test_04_GPU_Metrics_Timestamp` - Validates metrics timestamp

#### 6. **GPUTuningTests.cs**
Tests GPU tuning support detection (read-only, no modifications).

**Tests:**
- `Test_01_Get_GPU_Tuning_Services` - Retrieves tuning services
- `Test_02_Manual_Fan_Tuning_Support` - Checks manual fan control support
- `Test_03_Manual_Power_Tuning_Support` - Checks power limit tuning support
- `Test_04_Manual_Graphics_Tuning_Support` - Checks clock/voltage tuning support
- `Test_05_Auto_Tuning_Support` - Checks auto-overclocking support
- `Test_06_Preset_Tuning_Support` - Checks preset profile support

### Advanced Features Tests

#### 7. **System2Tests.cs**
Tests IADLXSystem2-specific features (requires newer GPUs).

**Tests:**
- `Test_01_System2_Interface_Support` - Checks if IADLXSystem2 is available
- `Test_02_Multimedia_Services` - Tests multimedia services access
- `Test_03_GPU_Apps_List_Changed_Handling` - Tests app-GPU assignment monitoring
- `Test_04_System2_Capabilities_Summary` - Displays all System2 capabilities

## Hardware Requirements

### Minimum Requirements (All Tests Skip Gracefully)
- **No AMD GPU**: Tests will detect this and skip appropriately
- **Any OS**: Windows 10/11 (64-bit)

### For Basic Functionality Tests
- **AMD GPU**: Radeon RX 5000 series or newer
- **AMD Drivers**: Adrenalin 21.10.1 or newer
- Tests that run: Initialization, GPU enumeration, basic properties, display detection

### For Full Test Coverage
- **AMD GPU**: Radeon RX 6000 series or newer (RDNA 2)
- **AMD Drivers**: Adrenalin 23.2.1 or newer
- **Multiple Displays**: 2+ for Eyefinity tests
- Additional tests available: IADLXSystem2 features, advanced metrics, multimedia services

## Running the Tests

### From Visual Studio
1. Open Test Explorer (`Test` ? `Test Explorer`)
2. Click `Run All Tests`
3. View detailed output for each test

### From Command Line
```bash
cd ADLXWrapper.Tests
dotnet test --verbosity detailed
```

### Run Specific Test Categories
```bash
# GPU tests only
dotnet test --filter "FullyQualifiedName~GPUTests"

# Display and desktop tests
dotnet test --filter "FullyQualifiedName~DisplayTests|FullyQualifiedName~DesktopTests"

# Performance tests
dotnet test --filter "FullyQualifiedName~PerformanceMonitoringTests"
```

## Test Results Interpretation

### On System WITHOUT AMD Hardware
```
? Test_01_ADLX_Runtime_Availability - PASSED
  ??  No AMD hardware or drivers detected
? Test_02_ADLX_Initialization - SKIPPED (No AMD hardware)
? Test_03_System_Interface_Versions - SKIPPED (No AMD hardware)
? All other tests - SKIPPED
```

### On System WITH AMD Hardware (Older GPU)
```
? Test_01_ADLX_Runtime_Availability - PASSED
? Test_02_ADLX_Initialization - PASSED
? Test_03_System_Interface_Versions - PASSED
  ??  IADLXSystem2: ? Not supported (requires RX 6000+)
? Test_01_Enumerate_GPUs - PASSED (1 GPU found)
? Test_02_GPU_Basic_Properties - PASSED
? GPU Interface Tests - PASSED (IADLXGPU1: ?, IADLXGPU2: ?)
? Test_02_Multimedia_Services - SKIPPED (Requires IADLXSystem2)
```

### On System WITH AMD Hardware (Newer GPU - RX 6000+)
```
? Test_01_ADLX_Runtime_Availability - PASSED
? Test_02_ADLX_Initialization - PASSED
? Test_03_System_Interface_Versions - PASSED
  ? IADLXSystem2: Supported
? All GPU Tests - PASSED
? All Display Tests - PASSED
? All Performance Tests - PASSED
? All System2 Tests - PASSED
```

## Test Infrastructure

### ADLXTestFixture.cs
Shared test fixture that:
- Runs once per test collection
- Detects AMD hardware availability
- Initializes ADLX if possible
- Detects hardware capabilities
- Provides skip reasons for tests that can't run

### HardwareCapabilities
Tracks what features are available:
- `GPUCount` - Number of AMD GPUs detected
- `DisplayCount` - Number of displays connected
- `SupportsGPU1` - IADLXGPU1 interface support
- `SupportsGPU2` - IADLXGPU2 interface support
- `SupportsDesktopServices` - Desktop management support
- `SupportsPerformanceMonitoring` - Performance metrics support
- `SupportsGPUTuning` - GPU tuning support

## Memory Management Pattern

All tests follow proper ADLX memory management:

```csharp
var ptr = ADLX.new_someTypeP_Ptr();
try
{
    var result = SomeADLXCall(ptr);
    if (result == ADLX_RESULT.ADLX_OK)
    {
        var value = ADLX.someTypeP_Ptr_value(ptr);
        // Use value...
    }
}
finally
{
    ADLX.delete_someTypeP_Ptr(ptr);
}
```

## Key Testing Principles

1. **Graceful Degradation**: Tests skip cleanly on unsupported hardware
2. **Read-Only**: Tests never modify user settings
3. **Comprehensive Output**: Detailed WriteLine() statements for debugging
4. **Proper Cleanup**: All pointers and interfaces are cleaned up
5. **Hardware Detection**: Tests check capabilities before running
6. **Error Handling**: All ADLX_RESULT codes are checked

## Continuous Integration

These tests are CI/CD friendly:
- Build succeeds even without AMD hardware
- Tests skip automatically on incompatible systems
- No test failures due to hardware limitations
- Suitable for automated testing pipelines

## Future Enhancements

Potential additions:
1. Event listener tests (display/GPU hotplug)
2. 3D settings configuration tests
3. Display color/gamma adjustment tests (read & reapply)
4. Memory timing tests
5. I2C communication tests

## Contributing

When adding new tests:
1. Follow the existing test organization
2. Use `[SkippableFact]` attribute
3. Check capabilities before running
4. Provide detailed output with `_output.WriteLine()`
5. Clean up all ADLX resources
6. Document hardware requirements

## Support

For issues or questions:
1. Check test output for detailed error information
2. Verify AMD drivers are installed and up-to-date
3. Check hardware compatibility (GPU model, driver version)
4. Review ADLX SDK documentation at https://gpuopen.com/adlx/

## Summary Statistics

- **Total Test Files**: 7
- **Total Test Methods**: 37+
- **Coverage Areas**: Initialization, GPUs, Displays, Desktops, Performance, Tuning, Advanced Features
- **Hardware Compatibility**: Gracefully handles all AMD GPU generations and non-AMD systems

---

**Last Updated**: 2025-01-XX
**ADLX SDK Version**: Compatible with ADLX SDK 2.0+
**Test Framework**: xUnit 2.8.2
**.NET Target**: .NET 9.0
