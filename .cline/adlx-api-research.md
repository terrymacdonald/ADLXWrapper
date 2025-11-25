# ADLX API Research Summary

## Overview

This document summarizes research on the AMD ADLX SDK API structure to guide the ClangSharp migration.

---

## ADLX Architecture

### Core Design Pattern
ADLX uses a **COM-like interface pattern**:
- Pure virtual interfaces (IADLX*)
- Reference counting (likely)
- Factory pattern for object creation
- Result codes for error handling

### Key Differences from IGCL
| Aspect | IGCL | ADLX |
|--------|------|------|
| Design | C-style functions | C++ COM-like interfaces |
| Handles | Opaque pointers | Interface pointers |
| Initialization | Function call | Factory + helper |
| Memory | Manual | Reference counting (likely) |
| Error Handling | Return codes | Return codes + interface methods |

---

## ADLX Type System

### Primitive Types (ADLXDefines.h)
```c
typedef int64_t        adlx_int64;
typedef int32_t        adlx_int32;
typedef uint64_t       adlx_uint64;
typedef uint32_t       adlx_uint32;
typedef void*          adlx_handle;
typedef bool           adlx_bool;
typedef double         adlx_double;
typedef float          adlx_float;
```

### Calling Conventions
```c
#define ADLX_STD_CALL    __stdcall
#define ADLX_CDECL_CALL  __cdecl
```

### Result Codes
```c
typedef enum ADLX_RESULT
{
    ADLX_OK = 0,
    ADLX_ALREADY_INITIALIZED = 2,
    ADLX_FAIL = -1,
    ADLX_INVALID_ARGS = -2,
    // ... more error codes
} ADLX_RESULT;
```

---

## Core Interfaces

### 1. IADLXSystem (Entry Point)
**Purpose:** Main system interface providing access to all services

**Key Methods:**
- `GetGPUs(IADLXGPUList** ppGPUs)` - Get GPU list
- `GetDisplays(IADLXDisplayList** ppDisplays)` - Get display list  
- `Get3DSettingsServices(IADLX3DSettingsServices** pp3DSettingsServices)` - Get 3D settings
- `GetDisplayServices(IADLXDisplayServices** ppDisplayServices)` - Get display services
- `GetGPUTuningServices(IADLXGPUTuningServices** ppGPUTuningServices)` - Get tuning services
- `GetPerformanceMonitoringServices(IADLXPerformanceMonitoringServices** ppPerfMonitoring)` - Get monitoring

**Initialization Pattern:**
```cpp
// From samples
ADLXHelper helper;
helper.Initialize();
IADLXSystem* system = helper.GetSystemServices();
```

### 2. IADLXGPU
**Purpose:** Represents a single GPU

**Key Methods:**
- `VendorId(const char** vendorId)` - Get vendor ID
- `Name(const char** name)` - Get GPU name
- `Type(ADLX_GPU_TYPE* type)` - Get GPU type
- `IsExternal(adlx_bool* isExternal)` - Check if external
- `ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)` - Get ASIC family
- `DriverPath(const char** driverPath)` - Get driver path
- `PNPString(const char** pnpString)` - Get PNP string
- `HasDesktops(adlx_bool* hasDesktops)` - Check if has desktops
- `TotalVRAM(adlx_uint* vramMB)` - Get total VRAM
- `UniqueId(adlx_int* uniqueId)` - Get unique ID

### 3. IADLXGPUList
**Purpose:** Collection of GPUs

**Key Methods:**
- `Size(adlx_uint* size)` - Get count
- `At(adlx_uint index, IADLXGPU** ppGPU)` - Get GPU at index
- `Empty(adlx_bool* empty)` - Check if empty

### 4. IADLXDisplay
**Purpose:** Represents a single display

**Key Methods:**
- `Name(const char** name)` - Get display name
- `DisplayType(ADLX_DISPLAY_TYPE* type)` - Get display type
- `ConnectorType(ADLX_DISPLAY_CONNECTOR_TYPE* connectorType)` - Get connector
- `NativeResolution(adlx_int* width, adlx_int* height)` - Get native resolution
- `RefreshRate(adlx_double* refreshRate)` - Get refresh rate
- `PixelClock(adlx_uint* pixelClock)` - Get pixel clock
- `UniqueId(adlx_int* uniqueId)` - Get unique ID

### 5. IADLXDisplayList
**Purpose:** Collection of displays

**Key Methods:**
- `Size(adlx_uint* size)` - Get count
- `At(adlx_uint index, IADLXDisplay** ppDisplay)` - Get display at index

---

## Service Interfaces

### Display Services (IADLXDisplayServices)
- Display enumeration
- Display settings (resolution, refresh rate, etc.)
- Gamma control (IADLXDisplayGamma)
- Gamut control (IADLXDisplayGamut)
- 3D LUT (IADLXDisplay3DLUT)

### GPU Tuning Services (IADLXGPUTuningServices)
- Manual tuning interfaces:
  - IGPUManualGFXTuning - Graphics tuning
  - IGPUManualVRAMTuning - VRAM tuning
  - IGPUManualFanTuning - Fan tuning
  - IGPUManualPowerTuning - Power tuning
- Auto tuning (IGPUAutoTuning)
- Preset tuning (IGPUPresetTuning)

### 3D Settings Services (IADLX3DSettingsServices)
- Anti-aliasing
- Anisotropic filtering  
- Tessellation
- Frame rate control
- Image enhancement

### Performance Monitoring Services (IADLXPerformanceMonitoringServices)
- FPS monitoring
- GPU metrics
- Power metrics
- Temperature metrics
- Memory metrics

### Power Tuning Services (IADLXPowerTuningServices)
- Power limits
- Power states

---

## Initialization Flow

### From ADLXHelper.cpp Analysis

**Option 1: Standard Initialization**
```cpp
ADLXHelper helper;
ADLX_RESULT res = helper.Initialize();
IADLXSystem* pSystem = helper.GetSystemServices();
```

**Option 2: With ADL Context (for ADL users)**
```cpp
ADLXHelper helper;
ADLX_RESULT res = helper.InitializeWithCallerAdl(adlContext, memoryFreeFn);
```

**Option 3: With Incompatible Driver**
```cpp
ADLXHelper helper;
ADLX_RESULT res = helper.InitializeWithIncompatibleDriver();
```

**Under the Hood:**
1. Load amdadlx64.dll dynamically
2. Get `ADLXQueryFullVersion` function pointer
3. Get `ADLXQueryVersion` function pointer  
4. Call initialization function
5. Obtain IADLXSystem* interface

---

## ClangSharp Mapping Strategy

### Challenge: COM-like Interfaces
ADLX uses pure virtual interfaces, which ClangSharp will see as:
```c
// C++ header
class IADLXGPU
{
    virtual ADLX_RESULT Name(const char** name) const = 0;
    // ...
};

// ClangSharp will generate
public unsafe partial struct IADLXGPU
{
    public void** lpVtbl;
}
```

### Solution: Wrapper Layer Pattern

**1. ClangSharp Generated (Read-Only)**
```csharp
// Generated/IADLXGPU.cs
public unsafe partial struct IADLXGPU
{
    public void** lpVtbl;
}

// Generated/ADLX.cs - Will have P/Invoke for C functions
// BUT: Interfaces are accessed via vtable!
```

**2. Wrapper Extension (Manual)**
```csharp
// ADLXExtensions.cs
public static unsafe class ADLXGPUExtensions
{
    // Manually define vtable layout based on header
    // Access interface methods via function pointers
    
    public static string GetName(IntPtr pGPU)
    {
        var gpu = (IADLXGPU*)pGPU;
        // Access vtable to call Name() method
        // This requires understanding vtable layout
    }
}
```

### Alternative: Look for C API
Check if ADLX provides C-style functions as well (some SDKs do).
If so, use those for ClangSharp instead of C++ interfaces.

**Action Item:** Investigate if ADLX has C API or if we need vtable approach.

---

## Reference Counting Strategy

ADLX likely uses reference counting (COM pattern):
- AddRef() increments reference count
- Release() decrements reference count
- Object destroyed when count reaches 0

**For Wrapper:**
```csharp
public sealed class ADLXApi : IDisposable
{
    private IntPtr _pSystem;
    
    public void Dispose()
    {
        if (_pSystem != IntPtr.Zero)
        {
            // Call Release() on _pSystem
            // via vtable or helper function
            _pSystem = IntPtr.Zero;
        }
    }
}
```

---

## Test Organization by ADLX Categories

### Proposed Test Files

1. **BasicApiTests.cs**
   - Initialization
   - Termination
   - Version queries

2. **CoreApiTests.cs** (System Services)
   - System interface access
   - GPU enumeration
   - Display enumeration

3. **GpuServicesTests.cs** (GPU Info & Properties)
   - GPU properties
   - GPU info
   - GPU type detection

4. **DisplayServicesTests.cs** (Display Management)
   - Display enumeration
   - Display properties
   - Display settings

5. **DisplayGammaTests.cs** (Display Color - Gamma)
   - Gamma control
   - Gamma ramps

6. **DisplayGamutTests.cs** (Display Color - Gamut)
   - Gamut control
   - Color space

7. **Display3DLUTTests.cs** (Display Color - 3D LUT)
   - 3D LUT management
   - LUT application

8. **GpuTuningManualTests.cs** (GPU Manual Tuning)
   - Manual GFX tuning
   - Manual VRAM tuning
   - Manual fan tuning
   - Manual power tuning

9. **GpuTuningAutoTests.cs** (GPU Auto Tuning)
   - Auto tuning features

10. **GpuTuningPresetTests.cs** (GPU Preset Tuning)
    - Preset management

11. **ThreeDSettingsTests.cs** (3D Graphics Settings)
    - Anti-aliasing
    - Anisotropic filtering
    - Tessellation

12. **PerformanceMonitoringTests.cs** (Performance)
    - FPS monitoring
    - Metrics collection

13. **PowerTuningTests.cs** (Power Management)
    - Power limits
    - Power states

14. **DesktopServicesTests.cs** (Desktop)
    - Desktop enumeration
    - Desktop properties

15. **MultimediaTests.cs** (Multimedia)
    - Multimedia features (if any)

16. **I2CTests.cs** (I2C Communication)
    - I2C read/write (if supported)

---

## Critical Questions to Answer

### Q1: Does ADLX provide a C API?
**How to check:** Look for non-class functions in headers
**Impact:** If yes, use C API for ClangSharp (much easier)

### Q2: How is the vtable laid out?
**How to check:** Examine header file method order
**Impact:** Needed for manual vtable access in wrapper

### Q3: Does ADLX use reference counting?
**How to check:** Look for AddRef/Release in interfaces
**Impact:** Affects memory management in wrapper

### Q4: What's the exact initialization sequence?
**How to check:** Study ADLXHelper.cpp
**Impact:** Critical for wrapper initialization

### Q5: Are there any special marshaling requirements?
**How to check:** Test with simple P/Invoke
**Impact:** May need custom marshaling code

---

## Next Investigation Steps

1. **Examine ADLXHelper.cpp in detail**
   - Understand exact initialization
   - Identify DLL loading mechanism
   - Find entry point functions

2. **Check for C API**
   - Search headers for non-class functions
   - Look in WinAPI.cpp

3. **Analyze interface layouts**
   - Document vtable method order
   - Check for multiple inheritance

4. **Study sample code**
   - Understand typical usage patterns
   - Identify common operations

5. **Test basic P/Invoke**
   - Try loading amdadlx64.dll
   - Try calling initialization functions
   - Verify struct sizes

---

## Files to Study Further

1. **ADLX\SDK\ADLXHelper\Windows\Cpp\ADLXHelper.cpp** - Initialization logic
2. **ADLX\SDK\Platform\Windows\WinAPI.cpp** - Platform-specific code
3. **ADLX\SDK\Include\ADLX.h** - Main entry point
4. **ADLX\Samples\CPP\Generic\GPUs\mainGPUs.cpp** - Usage example
5. **All interface headers in ADLX\SDK\Include\I*.h** - API reference

---

## Recommended Approach

### Phase 1: Investigation (Before Stage 1)
- Deep dive into ADLXHelper.cpp
- Determine if C API exists
- Map out vtable structures if needed

### Phase 2: Proof of Concept
- Create minimal P/Invoke test
- Load DLL and initialize
- Get one GPU property
- Verify approach works

### Phase 3: Full Implementation
- Proceed with 10-stage plan if POC succeeds
- Adjust strategy based on findings

---

## Status

**Current Phase:** Planning Complete
**Next Action:** Present plan to user, await approval
**Blockers:** None identified yet
**Risks:** COM-like interface may require vtable access

