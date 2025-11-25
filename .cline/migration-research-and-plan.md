# ADLXWrapper SWIG to ClangSharp Migration Plan

## Executive Summary

This document outlines the complete plan to migrate ADLXWrapper from SWIG-based bindings to ClangSharp-based bindings, following the proven architecture established in IGCLWrapper.

**Current Status:** Planning Phase - No modifications started
**Target:** ClangSharp-based P/Invoke bindings with managed wrapper layer

---

## Background Research

### What We're Moving FROM (SWIG):
- **Current Approach:** SWIG generates C++ wrapper layer + C# bindings
- **Files:** ADLXWrapper.i (SWIG interface), ADLXWrapper_wrap.cxx (generated)
- **Issues:** 
  - Multiple compilation layers (C++ ? SWIG ? C#)
  - Class-based bindings (slower, more complex)
  - Hard to maintain and debug
  - Requires SWIG toolchain

### What We're Moving TO (ClangSharp):
- **New Approach:** Direct P/Invoke from C# to native DLL
- **Architecture:** ClangSharp-generated structs + managed wrapper layer
- **Benefits:**
  - Single compilation step
  - Struct-based bindings (faster)
  - Better type safety
  - Native .NET integration
  - Easier to maintain

### Proven Pattern (IGCLWrapper):
The IGCLWrapper project successfully completed this exact migration and established these patterns:

1. **ClangSharp Generated Layer** (`Generated/` folder)
   - Auto-generated from C headers
   - Multi-file output (one file per type)
   - Opaque pointer types for handles
   - Never manually edited

2. **Wrapper Layer** (`IGCLApi.cs`, `IGCLExtensions.cs`)
   - Public API uses `IntPtr` for all handles
   - Internally casts to ClangSharp opaque pointers
   - Implements IDisposable for memory management
   - Helper methods for common operations

3. **Test Layer** (multiple test files)
   - Organized by IGCL API categories
   - Uses wrapper layer (IntPtr-based)
   - Safe, managed code

---

## ADLX API Structure Analysis

### Main SDK Components (from ADLX\SDK\Include):

1. **Core System** (`ISystem.h`, `ISystem1.h`, `ISystem2.h`)
   - API initialization/termination
   - GPU enumeration
   - System services access

2. **Display Services** (IDisplays*.h, IDisplay*.h)
   - `IDisplays.h` - Display enumeration
   - `IDisplaySettings.h` - Display configuration
   - `IDisplayGamma.h` - Gamma control
   - `IDisplayGamut.h` - Color gamut
   - `IDisplay3DLUT.h` - 3D LUT control

3. **GPU Services** (IGPU*.h)
   - GPU properties and info
   - Manual tuning (GFX, VRAM, Fan, Power)
   - Auto tuning
   - Preset tuning

4. **3D Graphics Settings** (I3DSettings*.h)
   - Anti-aliasing
   - Anisotropic filtering
   - Tessellation
   - Other 3D features

5. **Performance Monitoring** (IPerformanceMonitoring*.h)
   - FPS monitoring
   - Metrics collection
   - Performance telemetry

6. **Power Tuning** (IPowerTuning*.h)
   - Power limits
   - Power states

7. **Desktop Management** (IDesktops.h)
   - Desktop enumeration
   - Desktop properties

8. **Applications** (IApplications.h)
   - Application-specific settings

9. **Multimedia** (IMultiMedia.h)
   - Multimedia features

10. **I2C** (II2C.h)
    - I2C communication

11. **Events** (IChangedEvent.h)
    - Change notifications

### Key Helper Files:
- **ADLX\SDK\ADLXHelper\Windows\Cpp\ADLXHelper.h/cpp**
  - Initialization helpers
  - Common patterns
  - Memory management

- **ADLX\SDK\Platform\Windows\WinAPI.cpp**
  - Platform-specific initialization

### Entry Point:
- **Native DLL:** `amdadlx64.dll` (64-bit) or `amdadlx32.dll` (32-bit)
- **Main Header:** `ADLX.h`
- **Key Functions:**
  - `ADLXQueryFullVersion_Fn`
  - `ADLXQueryVersion_Fn`
  - Initialization via helper

---

## Folder Structure Comparison

### IGCLWrapper Structure (Target):
```
IGCLWrapper/
??? IGCLWrapper/
?   ??? ClangSharpConfig.rsp          # ClangSharp configuration
?   ??? IGCLApi.cs                     # Main wrapper API
?   ??? IGCLExtensions.cs              # Helper methods
?   ??? IGCLWrapper.csproj             # .NET project
?   ??? Generated/                     # ClangSharp auto-generated
?       ??? IGCL.cs                    # Main P/Invoke methods
?       ??? _ctl_*_handle_t.cs        # Handle types
?       ??? _ctl_*_properties_t.cs    # Struct types
?       ??? ... (100+ files)
??? IGCLWrapper.Tests/
?   ??? BasicApiTests.cs
?   ??? CoreApiTests.cs
?   ??? DisplayServicesTests.cs
?   ??? GpuServicesTests.cs
?   ??? SystemServicesTests.cs
??? drivers.gpu.control-library/       # IGCL SDK
    ??? include/igcl_api.h
```

### ADLXWrapper Structure (Current):
```
ADLXWrapper/
??? ADLXWrapper/                       # C++ SWIG project
?   ??? ADLXWrapper.vcxproj
?   ??? ADLXWrapper.i                  # SWIG interface
?   ??? ADLXWrapper_wrap.cxx           # SWIG generated
??? ADLXWrapper.Tests/                 # C# Tests
?   ??? InitializationTests.cs
?   ??? GPUTests.cs
?   ??? DisplayTests.cs
?   ??? ... (9 test files)
??? ADLX/SDK/                          # AMD ADLX SDK
    ??? Include/                       # Header files
    ??? ADLXHelper/
```

### ADLXWrapper Structure (Target):
```
ADLXWrapper/
??? ADLXWrapper/                       # NEW: C# wrapper project
?   ??? ClangSharpConfig.rsp          # ClangSharp configuration
?   ??? ADLXApi.cs                     # Main wrapper API
?   ??? ADLXExtensions.cs              # Helper methods
?   ??? ADLXWrapper.csproj             # .NET 9 project
?   ??? Generated/                     # ClangSharp auto-generated
?       ??? ADLX.cs                    # Main P/Invoke methods
?       ??? ADLX_*_handle_t.cs        # Handle types
?       ??? ADLX_*_properties_t.cs    # Struct types
?       ??? ... (auto-generated)
??? ADLXWrapper.Tests/
?   ??? BasicApiTests.cs               # NEW: Basic API tests
?   ??? CoreApiTests.cs                # NEW: Core/System tests
?   ??? DisplayServicesTests.cs        # CONVERTED: Display tests
?   ??? GpuServicesTests.cs            # CONVERTED: GPU tests
?   ??? PerformanceMonitoringTests.cs  # CONVERTED: Perf tests
?   ??? PowerTuningTests.cs            # CONVERTED: Power tests
?   ??? ThreeDSettingsTests.cs         # NEW: 3D settings tests
?   ??? DesktopServicesTests.cs        # CONVERTED: Desktop tests
?   ??? MultimediaTests.cs             # NEW: Multimedia tests
??? ADLX/SDK/                          # AMD ADLX SDK (unchanged)
```

---

## Implementation Plan

### STAGE 1: Project Setup and ClangSharp Configuration
**Goal:** Create new C# wrapper project and configure ClangSharp

**Tasks:**
1. Create new ADLXWrapper C# project (.NET 9)
   - SDK-style csproj
   - Enable unsafe blocks
   - Add ClangSharp NuGet packages
   
2. Create ClangSharpConfig.rsp
   - Configure input: ADLX\SDK\Include\ADLX.h
   - Configure output: ADLXWrapper/Generated/
   - Configure namespace: ADLXWrapper
   - Configure library: amdadlx64.dll
   - Enable multi-file generation
   
3. Research ADLX header dependencies
   - Identify which headers to include
   - Check for platform-specific defines
   - Determine if preprocessing needed

4. Generate initial bindings
   - Run ClangSharp
   - Verify generated files
   - Check for errors/warnings

**Deliverables:**
- ADLXWrapper/ADLXWrapper.csproj
- ADLXWrapper/ClangSharpConfig.rsp
- ADLXWrapper/Generated/ (populated)

**Decision Points:**
- Which ADLX headers to process?
- Need custom preprocessor defines?
- Any ClangSharp exclusions needed?

---

### STAGE 2: Core Wrapper Layer (ADLXApi.cs)
**Goal:** Create main API wrapper with initialization and cleanup

**Tasks:**
1. Create ADLXException class
   - Map ADLX_RESULT to exception
   - Provide error context

2. Create ADLXApi class
   - IDisposable implementation
   - Private constructor
   - Initialize() static method
   
3. Implement initialization
   - Load amdadlx64.dll
   - Call initialization functions
   - Handle ADL context (if needed)
   - Return ADLXApi instance

4. Implement GPU enumeration
   - EnumerateGPUs() method
   - Return IntPtr[] for GPU handles
   - Internal opaque pointer conversion

5. Implement cleanup
   - Dispose pattern
   - Call termination functions
   - Release resources

**Deliverables:**
- ADLXWrapper/ADLXApi.cs (core structure)

**Pattern (based on IGCLApi.cs):**
```csharp
public sealed class ADLXApi : IDisposable
{
    private IntPtr _hSystem;
    private bool _disposed;

    private ADLXApi(IntPtr hSystem) { }
    
    public static ADLXApi Initialize() { }
    public IntPtr[] EnumerateGPUs() { }
    public void Dispose() { }
}
```

---

### STAGE 3: Helper Extension Layer (ADLXExtensions.cs)
**Goal:** Create helper methods for common ADLX operations

**Tasks:**
1. Create static helper classes
   - ADLXHelpers (main helpers)
   - ADLXStructHelper (struct initialization)

2. Implement GPU helpers
   - GetGPUProperties(IntPtr hGPU)
   - GetGPUName(IntPtr hGPU)
   - GetGPUVendorId(IntPtr hGPU)
   - etc.

3. Implement Display helpers
   - GetDisplayProperties(IntPtr hDisplay)
   - EnumerateDisplays(IntPtr hGPU)
   - etc.

4. Implement struct initialization helpers
   - Proper Size and Version fields
   - Safe defaults

**Deliverables:**
- ADLXWrapper/ADLXExtensions.cs

**Pattern:**
```csharp
public static class ADLXHelpers
{
    public static unsafe ADLX_GPU_Properties GetGPUProperties(IntPtr hGPU)
    {
        // Create struct with proper size/version
        // Cast IntPtr to opaque pointer
        // Call ADLX P/Invoke method
        // Return struct
    }
}
```

---

### STAGE 4: Basic Tests and Validation
**Goal:** Verify wrapper layer works correctly

**Tasks:**
1. Create BasicApiTests.cs
   - Test initialization
   - Test GPU enumeration
   - Test basic properties
   - Test cleanup

2. Create CoreApiTests.cs
   - Test system services
   - Test version queries
   - Test error handling

3. Run tests on AMD hardware
   - Verify P/Invoke works
   - Verify struct layouts
   - Verify handle management

4. Fix any issues found
   - Struct alignment problems
   - Handle type mismatches
   - Memory leaks

**Deliverables:**
- ADLXWrapper.Tests/BasicApiTests.cs
- ADLXWrapper.Tests/CoreApiTests.cs
- Verified working wrapper

**Success Criteria:**
- Tests pass on AMD hardware
- No access violations
- Proper cleanup verified

---

### STAGE 5: Display Services Tests
**Goal:** Convert existing display tests to new wrapper

**Tasks:**
1. Review existing DisplayTests.cs
   - Understand current test coverage
   - Map to ADLX display APIs

2. Create DisplayServicesTests.cs
   - Test display enumeration
   - Test display properties
   - Test gamma/gamut control
   - Test 3D LUT
   - Test display settings

3. Extend ADLXApi/ADLXExtensions if needed
   - Add display enumeration methods
   - Add display helper methods

4. Validate on AMD hardware

**Deliverables:**
- ADLXWrapper.Tests/DisplayServicesTests.cs
- Updated ADLXApi.cs (if needed)
- Updated ADLXExtensions.cs (if needed)

---

### STAGE 6: GPU Services Tests
**Goal:** Convert existing GPU tests to new wrapper

**Tasks:**
1. Review existing GPUTests.cs and GPUTuningTests.cs
   - Understand current test coverage
   - Map to ADLX GPU APIs

2. Create GpuServicesTests.cs
   - Test GPU properties
   - Test GPU enumeration
   - Test manual tuning (GFX, VRAM, Fan, Power)
   - Test auto tuning
   - Test preset tuning

3. Extend ADLXApi/ADLXExtensions if needed
   - Add GPU tuning methods
   - Add GPU helper methods

4. Validate on AMD hardware

**Deliverables:**
- ADLXWrapper.Tests/GpuServicesTests.cs
- Updated wrapper layer

---

### STAGE 7: Performance Monitoring Tests
**Goal:** Convert existing performance monitoring tests

**Tasks:**
1. Review existing PerformanceMonitoringTests.cs
   - Understand current test coverage
   - Map to ADLX performance APIs

2. Create PerformanceMonitoringTests.cs
   - Test FPS monitoring
   - Test metrics collection
   - Test telemetry

3. Extend wrapper layer if needed

**Deliverables:**
- ADLXWrapper.Tests/PerformanceMonitoringTests.cs

---

### STAGE 8: Additional Services Tests
**Goal:** Create tests for remaining ADLX services

**Tasks:**
1. Create PowerTuningTests.cs
   - Map from existing tests if available
   - Test power limits
   - Test power states

2. Create ThreeDSettingsTests.cs
   - Test anti-aliasing
   - Test anisotropic filtering
   - Test tessellation
   - Test other 3D features

3. Create DesktopServicesTests.cs
   - Test desktop enumeration
   - Test desktop properties

4. Create MultimediaTests.cs (if needed)
   - Test multimedia features

**Deliverables:**
- Complete test coverage for all ADLX services

---

### STAGE 9: Documentation and Cleanup
**Goal:** Document the new wrapper and clean up old SWIG files

**Tasks:**
1. Create comprehensive README.md
   - Usage examples
   - API reference
   - Build instructions
   - Troubleshooting

2. Create migration guide
   - For users of old SWIG wrapper
   - API changes
   - Code examples

3. Clean up old SWIG files
   - Move to archive folder
   - Or delete if not needed
   - Update .gitignore

4. Create .cline instruction files
   - Similar to IGCLWrapper
   - Document patterns
   - Troubleshooting guide

**Deliverables:**
- README.md
- Migration guide
- Clean workspace
- Complete documentation

---

### STAGE 10: Final Validation and Release
**Goal:** Final testing and prepare for release

**Tasks:**
1. Run all tests on AMD hardware
   - Verify no regressions
   - Test on multiple GPU types if possible

2. Performance testing
   - Compare to old SWIG wrapper
   - Verify performance improvements

3. Memory leak testing
   - Long-running tests
   - Verify proper cleanup

4. Build NuGet package (if applicable)

5. Tag release in Git

**Deliverables:**
- Fully tested wrapper
- Release notes
- NuGet package (optional)

---

## Key Technical Details

### ClangSharp Configuration
Based on IGCLWrapper pattern:
```
--file
ADLX/SDK/Include/ADLX.h

--namespace
ADLXWrapper

--output
ADLXWrapper/Generated

--libraryPath
amdadlx64.dll

--methodClassName
ADLX

--config
generate-helper-types
multi-file
generate-macro-bindings
compatible-codegen
```

### Handle Type Pattern
ADLX uses interface pointers (IADLXSystem*, IADLXGPU*, etc.)
ClangSharp will generate opaque pointer types similar to IGCL.

Example:
```csharp
// ClangSharp generates (in Generated/)
public struct IADLXSystem { }
public struct IADLXGPU { }

// Wrapper uses IntPtr (in ADLXApi.cs)
public IntPtr GetSystemServices()
{
    IADLXSystem* pSystem;
    // ... get system ...
    return (IntPtr)pSystem;
}
```

### Memory Management Pattern
```csharp
// User code
using (var adlx = ADLXApi.Initialize())
{
    var gpus = adlx.EnumerateGPUs();
    // Use gpus...
} // Automatic cleanup
```

### Error Handling Pattern
```csharp
var result = ADLX.SomeFunction(...);
if (result != ADLX_RESULT.ADLX_OK)
{
    throw new ADLXException(result, "Operation failed");
}
```

---

## Risk Analysis and Mitigation

### Risk 1: ADLX uses COM-like interface pointers
**Mitigation:** Study ADLXHelper to understand initialization pattern. ClangSharp should handle opaque pointers correctly.

### Risk 2: Complex struct layouts
**Mitigation:** Verify struct sizes match C++ headers. Add explicit StructLayout attributes if needed.

### Risk 3: Reference counting (if used)
**Mitigation:** Study ADLX samples to understand memory management. Implement proper cleanup in wrapper.

### Risk 4: Callback functions
**Mitigation:** ClangSharp supports function pointers. Implement delegate marshaling if needed.

### Risk 5: String marshaling
**Mitigation:** ADLX uses const char*. Use proper MarshalAs attributes for string conversion.

---

## Success Criteria

1. ? All tests pass on AMD hardware
2. ? No memory leaks detected
3. ? Performance equal or better than SWIG version
4. ? Clean, maintainable code
5. ? Comprehensive documentation
6. ? Easy to update when ADLX headers change

---

## Current Progress Tracker

**STATUS: PLANNING PHASE**

- [ ] Stage 1: Project Setup and ClangSharp Configuration
- [ ] Stage 2: Core Wrapper Layer (ADLXApi.cs)
- [ ] Stage 3: Helper Extension Layer (ADLXExtensions.cs)
- [ ] Stage 4: Basic Tests and Validation
- [ ] Stage 5: Display Services Tests
- [ ] Stage 6: GPU Services Tests
- [ ] Stage 7: Performance Monitoring Tests
- [ ] Stage 8: Additional Services Tests
- [ ] Stage 9: Documentation and Cleanup
- [ ] Stage 10: Final Validation and Release

**NEXT STEP:** Present plan to user for approval

---

## References

1. IGCLWrapper project: `c:\vs-code\IGCLWrapper`
2. ADLX SDK: `c:\vs-code\ADLXWrapper\ADLX\SDK`
3. ClangSharp documentation: https://github.com/dotnet/ClangSharp
4. IGCLWrapper instructions: `c:\vs-code\IGCLWrapper\.cline\.instructions.md`
5. IGCLWrapper implementation guide: `c:\vs-code\IGCLWrapper\.cline\option-a-implementation-guide.md`

