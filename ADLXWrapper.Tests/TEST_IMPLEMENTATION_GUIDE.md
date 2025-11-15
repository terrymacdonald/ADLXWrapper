# ADLXWrapper.Tests - Comprehensive Test Suite Implementation Guide

This document provides guidance for completing the ADLXWrapper.Tests comprehensive test suite based on your requirements.

## Current Status

? Created test infrastructure:
- `ADLXTestFixture.cs` - Shared fixture with hardware detection
- `InitializationTests.cs` - ADLX initialization tests (needs fixes)
- `GPUTests.cs` - GPU enumeration and properties (needs fixes)
- `ADLXWrapper.Tests.csproj` - Project file configured for xUnit

## Critical Issues to Fix

### 1. xUnit Skip Pattern
xUnit doesn't have `Skip.IfNot()`. Replace with attribute-based skipping:

```csharp
[SkippableFact]
public void Test_Name()
{
    Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
    // Test code...
}
```

Requires adding package reference:
```xml
<PackageReference Include="Xunit.SkippableFact" Version="1.4.13" />
```

### 2. ADLX API Method Signatures
Several methods have incorrect signatures. Correct patterns:

```csharp
// List.Size() returns uint directly
uint count = gpuList.Size();  // NOT: gpuList.Size(sizePtr)

// GPU properties use out parameters
var namePtr = ADLX.new_charP_Ptr();
gpu.Name(namePtr);
string name = ADLX.charP_Ptr_value(namePtr);

// Vendor ID - returns string not int
var vendorPtr = ADLX.new_charP_Ptr();
gpu.VendorId(vendorPtr);
string vendorId = ADLX.charP_Ptr_value(vendorPtr);

// VRAM - returns string 
var vramTypePtr = ADLX.new_charP_Ptr();
gpu.VRAMType(vramTypePtr);
string vramType = ADLX.charP_Ptr_value(vramTypePtr);
```

### 3. MAKE_FULL_VERSION Helper
Add this helper method to a static utility class:

```csharp
public static class ADLXHelpers
{
    public static ulong MakeFullVersion(int major, int minor)
    {
        return ((ulong)major << 32) | (ulong)minor;
    }
}
```

## Remaining Test Files to Create

### 1. DisplayTests.cs
Test display enumeration, properties, and settings:

```csharp
[Collection("ADLX Tests")]
public class DisplayTests
{
    [SkippableFact]
    public void Test_01_Enumerate_Displays()
    [SkippableFact]
    public void Test_02_Display_Properties()
    [SkippableFact]
    public void Test_03_Display_Resolution()
    [SkippableFact]
    public void Test_04_Display_Refresh_Rate()
    [SkippableFact]
    public void Test_05_Display_Color_Depth() // Read and re-apply current
    [SkippableFact]
    public void Test_06_Display_Pixel_Format() // Read and re-apply current
    [SkippableFact]
    public void Test_07_Display_FreeSync_Status()
}
```

### 2. DesktopTests.cs
Test desktop services and Eyefinity:

```csharp
[Collection("ADLX Tests")]
public class DesktopTests
{
    [SkippableFact]
    public void Test_01_Enumerate_Desktops()
    [SkippableFact]
    public void Test_02_Desktop_Properties()
    [SkippableFact]
    public void Test_03_Eyefinity_Detection() // Check if Eyefinity possible
    [SkippableFact]
    public void Test_04_Eyefinity_Create_And_Restore() // SPECIAL CASE
}
```

**Eyefinity Special Test Pattern**:
```csharp
public void Test_04_Eyefinity_Create_And_Restore()
{
    Skip.IfNot(_fixture.Capabilities.DisplayCount >= 2, "Need 2+ displays for Eyefinity");
    
    // 1. Get desktop services
    // 2. Check if displays can be combined (SimpleEyefinity.IsSupported)
    // 3. If supported:
    //    a. Save current desktop layout
    //    b. Create Eyefinity desktop
    //    c. Verify creation
    //    d. Destroy Eyefinity desktop
    //    e. Verify restoration to original layout
}
```

### 3. PerformanceMonitoringTests.cs
Test GPU metrics:

```csharp
[Collection("ADLX Tests")]
public class PerformanceMonitoringTests
{
    [SkippableFact]
    public void Test_01_Get_Performance_Services()
    [SkippableFact]
    public void Test_02_GPU_Temperature()
    [SkippableFact]
    public void Test_03_GPU_Usage()
    [SkippableFact]
    public void Test_04_GPU_Clock_Speed()
    [SkippableFact]
    public void Test_05_GPU_Fan_Speed()
    [SkippableFact]
    public void Test_06_GPU_Power()
    [SkippableFact]
    public void Test_07_VRAM_Usage()
}
```

### 4. GPUTuningTests.cs
Test GPU tuning (read-only unless specified):

```csharp
[Collection("ADLX Tests")]
public class GPUTuningTests
{
    [SkippableFact]
    public void Test_01_Get_Tuning_Services()
    [SkippableFact]
    public void Test_02_Manual_Fan_Tuning_Support()
    [SkippableFact]
    public void Test_03_Read_Current_Fan_Settings()
    [SkippableFact]
    public void Test_04_Reapply_Fan_Settings() // Read then re-apply same
    [SkippableFact]
    public void Test_05_Manual_Power_Tuning_Support()
    [SkippableFact]
    public void Test_06_Read_Current_Power_Limit()
    [SkippableFact]
    public void Test_07_Reapply_Power_Limit() // Read then re-apply same
}
```

### 5. System2Tests.cs
Test IADLXSystem2-specific features:

```csharp
[Collection("ADLX Tests")]
public class System2Tests
{
    [SkippableFact]
    public void Test_01_System2_Interface_Support()
    [SkippableFact]
    public void Test_02_Multimedia_Services()
    [SkippableFact]
    public void Test_03_GPU_Apps_List_Changed_Handling()
}
```

### 6. EventListenerTests.cs
Test event listeners:

```csharp
[Collection("ADLX Tests")]
public class EventListenerTests
{
    [SkippableFact]
    public void Test_01_Display_List_Changed_Listener()
    [SkippableFact]
    public void Test_02_GPU_Connect_Changed_Listener()
}
```

## Test Execution Strategy

### Hardware Detection Flow
```
1. Fixture initialization runs once per test collection
2. Detects AMD hardware availability
3. Attempts ADLX initialization
4. Captures hardware capabilities
5. Each test checks capabilities and skips gracefully if unsupported
```

### Skipping Tests Properly
```csharp
[SkippableFact]
public void Test_Name()
{
    // Skip if no AMD hardware
    Skip.IfNot(_fixture.IsAMDHardwareAvailable, "No AMD hardware");
    
    // Skip if ADLX not supported
    Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason);
    
    // Skip if specific feature not supported
    Skip.IfNot(_fixture.Capabilities.SupportsPerformanceMonitoring, 
               "Performance monitoring not supported");
    
    // Test code here...
}
```

### Configuration Test Pattern (Read & Reapply)
```csharp
[SkippableFact]
public void Test_Display_Setting_Reapply()
{
    Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason);
    
    // Get display services and first display
    var display = GetFirstDisplay();
    
    // Get current setting
    var currentValue = GetCurrentColorDepth(display);
    _output.WriteLine($"Current color depth: {currentValue}");
    
    // Re-apply the same setting
    var result = SetColorDepth(display, currentValue);
    
    // Verify it succeeded
    Assert.Equal(ADLX_RESULT.ADLX_OK, result);
    _output.WriteLine("? Successfully re-applied same setting");
}
```

## Expected Test Output Examples

### On AMD System with Full Support:
```
? Test_01_ADLX_Runtime_Availability - PASSED
? Test_02_ADLX_Initialization - PASSED  
? Test_03_System_Interface_Versions - PASSED (System2: ? Supported)
? Test_04_Hardware_Capabilities_Summary - PASSED
  GPUs: 1, Displays: 2, GPU2: ?, System2: ?
? Test_01_Enumerate_GPUs - PASSED (Found 1 GPU)
? Test_02_GPU_Basic_Properties - PASSED
? Test_03_GPU_Interface_Versions - PASSED
...
```

### On AMD System with Limited Support:
```
? Test_01_ADLX_Runtime_Availability - PASSED
? Test_02_ADLX_Initialization - PASSED
? Test_03_System_Interface_Versions - PASSED (System2: ? Not supported)
  ??  IADLXSystem2 requires RX 6000 series or newer
? Test_01_System2_Interface_Support - SKIPPED (IADLXSystem2 not supported)
? Test_02_Multimedia_Services - SKIPPED (Requires IADLXSystem2)
...
```

### On Non-AMD System:
```
? Test_01_ADLX_Runtime_Availability - PASSED
  ??  No AMD hardware or drivers detected
? Test_02_ADLX_Initialization - SKIPPED (No AMD hardware)
? Test_03_System_Interface_Versions - SKIPPED (No AMD hardware)
...
(All tests skipped gracefully)
```

## Running Tests

### Visual Studio Test Explorer
```
1. Build solution
2. Open Test Explorer (Test > Test Explorer)
3. Click "Run All Tests"
4. View detailed output for each test
```

### Command Line
```cmd
cd ADLXWrapper.Tests
dotnet test --verbosity detailed
```

### With Filtering
```cmd
# Run only GPU tests
dotnet test --filter "FullyQualifiedName~GPUTests"

# Run only tests that pass (exclude skipped)
dotnet test --filter "TestCategory!=RequiresAMDHardware"
```

## Next Steps

1. Fix compilation errors in existing test files:
   - Add Xunit.SkippableFact package
   - Fix API method calls
   - Add MAKE_FULL_VERSION helper
   
2. Create remaining test files:
   - DisplayTests.cs
   - DesktopTests.cs (with Eyefinity special case)
   - PerformanceMonitoringTests.cs
   - GPUTuningTests.cs
   - System2Tests.cs
   - EventListenerTests.cs

3. Test on AMD hardware and iterate

4. Add README in test project explaining hardware requirements

## Tips for Implementation

1. **Always check result codes**: Every ADLX call can fail, check ADLX_RESULT
2. **Clean up pointers**: Use try/finally or using statements
3. **Detailed output**: Use _output.WriteLine() liberally for debugging
4. **Test independence**: Each test should be self-contained
5. **Hardware variations**: Test on different AMD GPU generations if possible

## Memory Management Pattern

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

## Continuous Integration Considerations

For CI/CD pipelines without AMD hardware:
1. Tests will skip gracefully
2. Build will still succeed
3. Consider adding AMD hardware for full test coverage
4. Or run tests manually on AMD systems before releases
