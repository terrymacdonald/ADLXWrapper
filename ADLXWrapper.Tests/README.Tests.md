# ADLXWrapper Tests - ClangSharp-based Wrapper

This folder contains tests for the new ClangSharp-based ADLXWrapper.

## Test Structure

### New Tests (ClangSharp-based)

**BasicApiTests.cs** - 9 tests
- ? Initialize_ShouldSucceed
- ? GetVersion_ShouldReturnValidVersion
- ? GetFullVersion_ShouldReturnNonZero
- ? GetSystemServices_ShouldReturnValidPointer
- ? Dispose_ShouldNotThrow
- ? DisposeMultipleTimes_ShouldBeIdempotent
- ? AfterDispose_MethodsShouldThrowObjectDisposedException
- ? UsingStatement_ShouldAutomaticallyDispose
- ? InitializeMultipleTimes_ShouldReturnSeparateInstances

**CoreApiTests.cs** - 19 tests
- ? EnumerateGPUs_ShouldReturnArray
- ? EnumerateGPUs_ShouldReturnValidPointers
- ? GetGPUName_ShouldReturnValidName
- ? GetGPUVendorId_ShouldReturnAMD
- ? GetGPUTotalVRAM_ShouldReturnPositiveValue
- ? GetGPUVRAMType_ShouldReturnValidType
- ? GetGPUUniqueId_ShouldReturnValue
- ? GetGPUDeviceId_ShouldReturnValidId
- ? GetGPUDriverPath_ShouldReturnValidPath
- ? GetGPUPNPString_ShouldReturnValidString
- ? IsGPUExternal_ShouldReturnBooleanValue
- ? HasGPUDesktops_ShouldReturnBooleanValue
- ? GetBasicInfo_ShouldReturnCompleteInformation
- ? GetIdentification_ShouldReturnCompleteInformation
- ? AllGPUs_ShouldHaveUniqueIds
- ? GetGPUName_WithNullPointer_ShouldThrowArgumentNullException
- ? GetGPUTotalVRAM_WithNullPointer_ShouldThrowArgumentNullException
- ? ReleaseInterface_WithValidPointer_ShouldNotThrow
- ? ReleaseInterface_WithNullPointer_ShouldNotThrow

**DisplayServicesTests.cs** - 14 tests
- ? EnumerateAllDisplays_ShouldReturnArray
- ? EnumerateAllDisplays_ShouldReturnValidPointers
- ? GetDisplayName_ShouldReturnValidName
- ? GetDisplayNativeResolution_ShouldReturnValidResolution
- ? GetDisplayRefreshRate_ShouldReturnPositiveValue
- ? GetDisplayManufacturerID_ShouldReturnValue
- ? GetDisplayPixelClock_ShouldReturnPositiveValue
- ? GetDisplayBasicInfo_ShouldReturnCompleteInformation
- ? AllDisplays_ShouldHaveUniqueNames
- ? GetDisplayName_WithNullPointer_ShouldThrowArgumentNullException
- ? GetDisplayNativeResolution_WithNullPointer_ShouldThrowArgumentNullException
- ? GetDisplayRefreshRate_WithNullPointer_ShouldThrowArgumentNullException
- ? EnumerateAllDisplays_WithNullPointer_ShouldThrowArgumentNullException
- ? MultipleDisplayEnumeration_ShouldReturnConsistentResults

**GpuTuningServicesTests.cs** - 10 tests
- ? GetGPUTuningServices_ShouldSucceed
- ? IsSupportedAutoTuning_ShouldReturnBooleanValue
- ? IsSupportedPresetTuning_ShouldReturnBooleanValue
- ? IsSupportedManualGFXTuning_ShouldHandleGracefully
- ? IsSupportedManualVRAMTuning_ShouldReturnBooleanValue
- ? IsSupportedManualFanTuning_ShouldReturnBooleanValue
- ? IsSupportedManualPowerTuning_ShouldReturnBooleanValue
- ? AllSupportChecks_ShouldNotThrow
- ? IsSupportedAutoTuning_WithNullServices_ShouldThrowArgumentNullException
- ? IsSupportedAutoTuning_WithNullGPU_ShouldThrowArgumentNullException

**PerformanceMonitoringServicesTests.cs** - 10 tests
- ? GetPerformanceMonitoringServices_ShouldSucceed
- ? GetSupportedGPUMetrics_ShouldReturnValidPointer
- ? IsSupportedGPUUsage_ShouldReturnBoolean
- ? IsSupportedGPUTemperature_ShouldReturnBoolean
- ? GetCurrentGPUMetrics_ShouldReturnValidPointer
- ? GetGPUTemperature_ShouldReturnValidValue
- ? GetGPUUsage_ShouldReturnValidValue
- ? GetGPUClockSpeed_ShouldReturnValidValue
- ? AllMetrics_ShouldNotThrow
- ? GetCurrentGPUMetrics_WithNullServices_ShouldThrowArgumentNullException

**ArchitectureValidationTests.cs** - 4 tests
- ? MigrationArchitecture_IsComplete
- ? AllImplementedServices_AreAccessible
- ? VTablePattern_IsProvenAndRepeatable
- ? TestCoverage_IsComprehensive

**Total: 66 tests**

### Legacy Tests (SWIG-based)

The following test files are for the old SWIG wrapper and will remain until the migration is complete:
- ADLXTestFixture.cs
- DesktopTests.cs
- DisplayTests.cs
- GPUTests.cs
- GPUTuningTests.cs
- InitializationTests.cs
- PerformanceMonitoringTests.cs
- SimpleTest.cs
- System2Tests.cs

## Running Tests

### Run All Tests
```powershell
cd ADLXWrapper.Tests
dotnet test
```

### Run Only New ClangSharp Tests
```powershell
dotnet test --filter "FullyQualifiedName~BasicApiTests|FullyQualifiedName~CoreApiTests"
```

### Run Specific Test Class
```powershell
dotnet test --filter "FullyQualifiedName~BasicApiTests"
dotnet test --filter "FullyQualifiedName~CoreApiTests"
```

### Run With Detailed Output
```powershell
dotnet test --logger "console;verbosity=detailed"
```

## Test Behavior

### Hardware Detection
Tests automatically detect if AMD hardware is available:
- ? **With AMD GPU:** Tests run and validate functionality
- ?? **Without AMD GPU:** Tests skip gracefully with informative messages
- ? **DLL Not Found:** Tests skip with clear error message

### Test Output
Tests use `ITestOutputHelper` to provide detailed information:
- ? Success indicators for passing tests
- ? Skip indicators for skipped tests
- Property values displayed for verification
- Error messages for failures

### Example Output
```
? ADLX initialized successfully
? ADLX Version: 1.2.3.4
? Found 1 GPU(s)
? GPU Name: AMD Radeon RX 7900 XTX
? Vendor ID: AMD
? Total VRAM: 24576 MB
? VRAM Type: GDDR6
```

## Test Coverage

### Initialization & Cleanup
- [x] Basic initialization
- [x] Version queries
- [x] Disposal patterns
- [x] Using statement support
- [x] Multiple instances
- [x] Error handling after disposal

### GPU Enumeration
- [x] Enumerate GPUs
- [x] Validate GPU pointers
- [x] Handle no GPUs scenario

### GPU Properties (String)
- [x] Name
- [x] Vendor ID
- [x] Driver Path
- [x] PNP String
- [x] VRAM Type
- [x] Device ID

### GPU Properties (Numeric)
- [x] Total VRAM
- [x] Unique ID

### GPU Properties (Boolean)
- [x] Is External
- [x] Has Desktops

### Combined Information
- [x] GPUBasicInfo struct
- [x] GPUIdentification struct

### Error Handling
- [x] Null pointer validation
- [x] ObjectDisposedException
- [x] ArgumentNullException
- [x] Graceful skipping without hardware

### Interface Management
- [x] Release interface
- [x] AddRef interface
- [x] Null pointer handling

## Requirements

- .NET 9.0 SDK
- xUnit test framework
- AMD GPU with ADLX support (for full test coverage)
- AMD GPU drivers installed

## Notes

### Type Conflicts
The project shows warnings about `ADLX_RESULT` type conflicts between old SWIG bindings and new wrapper. This is expected and doesn't affect functionality. The warnings will disappear when old SWIG files are removed in Stage 9.

### Test Isolation
Each test class manages its own ADLX instance:
- BasicApiTests: Creates new instance per test class
- CoreApiTests: Shares GPU list across tests in the class
- Both properly clean up resources in Dispose

### Memory Management
Tests properly release GPU interface pointers using `ADLXHelpers.ReleaseInterface()` to verify reference counting works correctly.

## Future Tests

Planned test files for upcoming stages:
- [ ] DisplayServicesTests.cs (Stage 5)
- [ ] GpuTuningTests.cs (Stage 6)
- [ ] PerformanceMonitoringTests.cs (Stage 7)
- [ ] PowerTuningTests.cs (Stage 8)
- [ ] ThreeDSettingsTests.cs (Stage 8)
- [ ] DesktopServicesTests.cs (Stage 8)

## Troubleshooting

### Tests Skipped
If all tests are skipped:
1. Check if AMD GPU is present
2. Verify AMD GPU drivers are installed
3. Check if amdadlx64.dll is in system PATH
4. Run test with `--logger "console;verbosity=detailed"` for diagnostics

### Access Violations
If tests crash with access violations:
1. Check struct layouts in ADLXVTables.cs
2. Verify VTable method order matches ADLX headers
3. Ensure proper pointer type casting

### Memory Leaks
Monitor test runs with Task Manager to check for memory leaks. All tests properly dispose ADLX instances and release GPU interfaces.

