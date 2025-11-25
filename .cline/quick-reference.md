# ADLXWrapper ClangSharp Migration - Quick Reference

## Architecture Overview

```
???????????????????????????????????????
?  User Application Code              ?
?  - Uses IntPtr                       ?
?  - Clean, simple API                 ?
?  - No unsafe code needed             ?
???????????????????????????????????????
               ?
???????????????????????????????????????
?  ADLXWrapper Public API              ?
?  - ADLXApi.cs (IntPtr parameters)    ?
?  - ADLXExtensions.cs (helpers)       ?
?  - Automatic memory management       ?
???????????????????????????????????????
               ? Internal Casting
???????????????????????????????????????
?  ClangSharp Generated Code           ?
?  - ADLX.cs (P/Invoke methods)        ?
?  - Opaque pointer types              ?
?  - Auto-generated, never edit        ?
???????????????????????????????????????
               ?
???????????????????????????????????????
?  Native AMD ADLX DLL                 ?
?  - amdadlx64.dll                     ?
???????????????????????????????????????
```

## Key Patterns

### Pattern 1: Initialization
```csharp
// User code
using (var adlx = ADLXApi.Initialize())
{
    var gpus = adlx.EnumerateGPUs(); // Returns IntPtr[]
    // Use API...
} // Automatic cleanup
```

### Pattern 2: Wrapper Method (ADLXApi.cs)
```csharp
public unsafe IntPtr[] EnumerateGPUs()
{
    ThrowIfDisposed();
    
    // Get count
    uint count = 0;
    var result = ADLX.ADLXEnumerateGPUs(
        (IADLXSystem*)_hSystem,  // Cast IntPtr to opaque pointer
        &count,
        null
    );
    CheckResult(result);
    
    // Get GPU handles as opaque pointers
    var gpus = new IADLXGPU*[count];
    fixed (IADLXGPU** pGPUs = gpus)
    {
        result = ADLX.ADLXEnumerateGPUs(
            (IADLXSystem*)_hSystem,
            &count,
            pGPUs
        );
    }
    CheckResult(result);
    
    // Convert to IntPtr for public API
    var intPtrGPUs = new IntPtr[count];
    for (int i = 0; i < count; i++)
    {
        intPtrGPUs[i] = (IntPtr)gpus[i];
    }
    
    return intPtrGPUs;
}
```

### Pattern 3: Helper Method (ADLXExtensions.cs)
```csharp
public static unsafe string GetGPUName(IntPtr hGPU)
{
    // Cast IntPtr to opaque pointer
    byte* pName = null;
    var result = ADLX.ADLXGPUName(
        (IADLXGPU*)hGPU,
        &pName
    );
    
    if (result != ADLX_RESULT.ADLX_OK)
    {
        throw new ADLXException(result, "Failed to get GPU name");
    }
    
    return Marshal.PtrToStringAnsi((IntPtr)pName) ?? "";
}
```

### Pattern 4: Test Code
```csharp
[Fact]
public void EnumerateGPUs_ShouldReturnGPUs()
{
    if (!_hasHardware || _api == null)
    {
        Assert.True(true, "No AMD hardware - test skipped");
        return;
    }

    var gpus = _api.EnumerateGPUs(); // Returns IntPtr[]
    
    Assert.NotNull(gpus);
    Assert.NotEmpty(gpus);
    
    // Use helper method (recommended)
    var name = ADLXHelpers.GetGPUName(gpus[0]);
    Assert.NotEmpty(name);
}
```

## File Organization

### ADLXWrapper/ (Main Project)
- **ADLXWrapper.csproj** - Project file (.NET 9, unsafe blocks)
- **ClangSharpConfig.rsp** - ClangSharp configuration
- **ADLXApi.cs** - Main wrapper API (IntPtr-based)
- **ADLXExtensions.cs** - Helper methods
- **Generated/** - ClangSharp auto-generated files
  - ADLX.cs - Main P/Invoke methods
  - ADLX_*.cs - Types, structs, enums
  - IADLX*.cs - Interface handle types

### ADLXWrapper.Tests/ (Test Project)
- **BasicApiTests.cs** - Basic initialization and cleanup
- **CoreApiTests.cs** - System services tests
- **DisplayServicesTests.cs** - Display-related tests
- **GpuServicesTests.cs** - GPU-related tests
- **PerformanceMonitoringTests.cs** - Performance monitoring
- **PowerTuningTests.cs** - Power tuning tests
- **ThreeDSettingsTests.cs** - 3D graphics settings
- **DesktopServicesTests.cs** - Desktop services
- **MultimediaTests.cs** - Multimedia features

## Common ClangSharp Issues and Solutions

### Issue 1: Struct Size Mismatch
```csharp
// PROBLEM: Struct size doesn't match C++ header
// SOLUTION: Add explicit StructLayout
[StructLayout(LayoutKind.Sequential, Pack = 8)]
public struct MyStruct { }
```

### Issue 2: String Marshaling
```csharp
// PROBLEM: String parameters not marshaling correctly
// SOLUTION: Use proper MarshalAs attribute
[MarshalAs(UnmanagedType.LPStr)]
public string StringParam;
```

### Issue 3: Function Pointer Callbacks
```csharp
// PROBLEM: Need to pass C# callback to native code
// SOLUTION: Use delegate with UnmanagedFunctionPointer
[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public delegate void MyCallback(IntPtr context);
```

## ADLX-Specific Notes

### Initialization Pattern
ADLX uses a helper-based initialization:
1. Load amdadlx64.dll dynamically
2. Get function pointers (ADLXQueryFullVersion, etc.)
3. Initialize system
4. Get system services

### Interface Pointers
ADLX uses COM-like interface pointers:
- IADLXSystem*
- IADLXGPU*
- IADLXDisplay*
- etc.

These become opaque pointer types in ClangSharp.

### Error Codes
ADLX uses `ADLX_RESULT` enum:
- ADLX_OK = Success
- ADLX_FAIL = Generic failure
- ADLX_INVALID_ARGS = Invalid arguments
- etc.

### Reference Counting
Study ADLX samples to determine if reference counting is needed.

## Build and Test Commands

### Build Wrapper
```powershell
cd c:\vs-code\ADLXWrapper\ADLXWrapper
dotnet build
```

### Build Tests
```powershell
cd c:\vs-code\ADLXWrapper\ADLXWrapper.Tests
dotnet build
```

### Run Tests
```powershell
cd c:\vs-code\ADLXWrapper\ADLXWrapper.Tests
dotnet test
```

### Generate ClangSharp Bindings
```powershell
cd c:\vs-code\ADLXWrapper\ADLXWrapper
ClangSharpPInvokeGenerator @ClangSharpConfig.rsp
```

## Troubleshooting

### Access Violation
- Check struct layouts (Size field correct?)
- Check handle casts (correct opaque pointer type?)
- Check null pointers

### Memory Leak
- Verify Dispose is called
- Check for missing cleanup in Dispose
- Use memory profiler to identify leaks

### Wrong Data Returned
- Verify struct Size and Version fields
- Check struct field order matches C++ header
- Check packing/alignment

### Build Errors After ClangSharp Regeneration
- Clean and rebuild
- Update ADLXExtensions.cs if signatures changed
- Update ADLXApi.cs wrapper methods
- Update tests if API changed

## Next Steps After Planning

1. Get user approval for plan
2. Start Stage 1: Project Setup
3. Work through stages sequentially
4. Update progress tracker in migration-research-and-plan.md
5. Document any issues/solutions in .cline folder

