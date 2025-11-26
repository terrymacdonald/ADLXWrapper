# ADLXWrapper.Tests - Comprehensive Test Suite

## Overview

This is a comprehensive test suite for the ADLXWrapper C# ClangSharp-based bindings for AMD's ADLX SDK. The tests are designed to work on any system (with or without AMD hardware) and will gracefully skip tests that cannot run on the current hardware.

## Test Organization

The test suite is organized into the following test files:

### Core Tests

#### 1. **BasicApiTests.cs**
Tests ADLX initialization, version queries, and cleanup.

**Tests:**
- `Initialize_ShouldSucceed` - Verifies ADLX initializes correctly
- `GetVersion_ShouldReturnValidVersion` - Checks version string retrieval
- `GetFullVersion_ShouldReturnNonZero` - Checks numeric version
- `GetSystemServices_ShouldReturnValidPointer` - Verifies system interface access
- `Dispose_ShouldNotThrow` - Tests proper cleanup
- `DisposeMultipleTimes_ShouldBeIdempotent` - Tests multiple dispose calls
- `AfterDispose_MethodsShouldThrowObjectDisposedException` - Validates disposed state
- `UsingStatement_ShouldAutomaticallyDispose` - Tests using pattern
- `InitializeMultipleTimes_ShouldReturnSeparateInstances` - Tests multiple instances

#### 2. **CoreApiTests.cs**
Tests GPU enumeration, properties, and helper methods.

**Tests:**
- GPU enumeration and basic properties
- GPU name, vendor ID, device ID retrieval
- VRAM information access
- GPU identification helpers
- Combined information struct tests
- List operations and array conversions
- Interface reference counting
- Null pointer handling

#### 3. **ArchitectureValidationTests.cs**
Validates the ClangSharp migration architecture.

**Tests:**
- `MigrationArchitecture_IsComplete` - Validates migration completeness
- `AllImplementedServices_AreAccessible` - Tests service accessibility
- `VTablePattern_IsProvenAndRepeatable` - Validates VTable pattern
- `TestCoverage_IsComprehensive` - Summarizes test coverage

### Display & Performance Tests

#### 4. **DisplayServicesTests.cs**
Tests display enumeration, properties, and configuration.

**Tests:**
- Display enumeration from system services
- Display name and resolution retrieval
- Refresh rate and pixel clock access
- Manufacturer ID retrieval
- Combined display information
- Null pointer handling
- Multiple display support

#### 5. **GpuTuningServicesTests.cs**
Tests GPU tuning support detection (read-only, no modifications).

**Tests:**
- GPU tuning services retrieval
- Auto-tuning support detection
- Preset tuning support detection
- Manual GFX tuning support
- Manual VRAM tuning support
- Manual fan tuning support
- Manual power tuning support
- Combined tuning capabilities
- Graceful handling of unsupported features

#### 6. **PerformanceMonitoringServicesTests.cs**
Tests GPU metrics and performance monitoring.

**Tests:**
- Performance monitoring services retrieval
- GPU metrics support detection
- Current metrics retrieval
- Temperature, usage, clock speed access
- VRAM, fan speed, power readings
- Combined metrics snapshots
- Null pointer handling
- Unsupported metrics handling

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
- **Multiple Displays**: 2+ for display enumeration tests
- Additional tests available: Advanced metrics, tuning capabilities

## Running the Tests

### From Visual Studio
1. Open Test Explorer (`Test` → `Test Explorer`)
2. Click `Run All Tests`
3. View detailed output for each test

### From Command Line
```bash
cd ADLXWrapper.Tests
dotnet test --verbosity detailed
```

### Run Specific Test Categories
```bash
# Basic API tests only
dotnet test --filter "FullyQualifiedName~BasicApiTests"

# Core API tests
dotnet test --filter "FullyQualifiedName~CoreApiTests"

# Display tests
dotnet test --filter "FullyQualifiedName~DisplayServicesTests"

# Performance tests
dotnet test --filter "FullyQualifiedName~PerformanceMonitoringServicesTests"

# Tuning tests
dotnet test --filter "FullyQualifiedName~GpuTuningServicesTests"
```

## Test Results Interpretation

### On System WITHOUT AMD Hardware
```
✅ Initialize_ShouldSucceed - SKIPPED
   ℹ️  Test skipped - No AMD hardware available
✅ All other tests - SKIPPED
```

### On System WITH AMD Hardware (Older GPU)
```
✅ Initialize_ShouldSucceed - PASSED
✅ GetVersion_ShouldReturnValidVersion - PASSED
✅ EnumerateGPUs tests - PASSED (1+ GPUs found)
✅ Basic GPU properties - PASSED
✅ Display enumeration - PASSED
ℹ️  Some advanced features may be unavailable (older GPU)
```

### On System WITH AMD Hardware (Newer GPU - RX 6000+)
```
✅ All initialization tests - PASSED
✅ All GPU enumeration tests - PASSED
✅ All display tests - PASSED
✅ All performance monitoring tests - PASSED
✅ All tuning tests - PASSED
✅ All architecture validation tests - PASSED
```

## Memory Management Pattern

All tests follow proper ADLX memory management:

```csharp
using (var adlx = ADLXApi.Initialize())
{
    var gpus = adlx.EnumerateGPUs();
    
    foreach (var gpu in gpus)
    {
        // Use GPU interface
        var info = ADLXGPUInfo.GetBasicInfo(gpu);
        // ...
        
        // Release when done
        ADLXHelpers.ReleaseInterface(gpu);
    }
} // Automatic cleanup via Dispose
```

## Key Testing Principles

1. **Graceful Degradation**: Tests skip cleanly on unsupported hardware
2. **Read-Only**: Tests never modify user settings
3. **Comprehensive Output**: Detailed WriteLine() statements for debugging
4. **Proper Cleanup**: All interfaces are released properly
5. **Hardware Detection**: Tests check capabilities before running
6. **Error Handling**: All operations include proper error checking

## Continuous Integration

These tests are CI/CD friendly:
- Build succeeds even without AMD hardware
- Tests skip automatically on incompatible systems
- No test failures due to hardware limitations
- Suitable for automated testing pipelines

## Contributing

When adding new tests:
1. Follow the existing test organization
2. Use graceful skipping when hardware is unavailable (see [SkippableFact] attribute
3. Provide detailed output with `_output.WriteLine()`
4. Clean up all ADLX resources with `ReleaseInterface()`
5. Document hardware requirements

## Support

For issues or questions:
1. Check test output for detailed error information
2. Verify AMD drivers are installed and up-to-date
3. Check hardware compatibility (GPU model, driver version)
4. Review ADLX SDK documentation at https://gpuopen.com/adlx/

## Summary Statistics

- **Total Test Files**: 6
- **Total Test Methods**: 66 (9 + 19 + 14 + 10 + 10 + 4)
- **Coverage Areas**: Initialization, GPUs, Displays, Performance Monitoring, GPU Tuning, Architecture Validation
- **Hardware Compatibility**: Gracefully handles all AMD GPU generations and non-AMD systems

---

**Last Updated**: 2025-11-26
**ADLX SDK Version**: Compatible with ADLX SDK 2.0+
**Test Framework**: xUnit 2.8.2
**.NET Target**: .NET 9.0
**Architecture**: ClangSharp-based VTable pattern
