# Implementation Plan

## Overview
Create a comprehensive solution for serializing ADLX_GammaRamp objects to JSON format by adding helper functions to the existing SWIG interface and implementing C# extension methods.

The ADLX_GammaRamp object contains a gamma property that is a SWIG-generated wrapper (`SWIGTYPE_p_unsigned_short`) around a pointer to 768 unsigned 16-bit values (256 RGB triplets). The current issue is that JSON serializers cannot serialize these SWIG wrapper objects directly because they only contain pointer handles to unmanaged memory. This implementation will provide safe access to the underlying gamma data while maintaining compatibility with the existing SWIG-generated bindings and following the established architectural patterns in the codebase.

## Types
Define new data structures and extend existing SWIG interface declarations.

**New C# Classes:**
- `SerializableGammaRamp` - JSON-serializable structure containing ushort array for gamma data
- `GammaRampJsonConverter` - Custom JSON converter for ADLX_GammaRamp objects  
- `GammaRampExtensions` - Extension methods for ADLX_GammaRamp conversion

**SWIG Interface Extensions:**
- Helper function declarations for gamma data extraction and creation
- Error handling return types using existing ADLX_RESULT pattern

## Files
Modify existing SWIG interface and add new C# utility classes.

**Modified Files:**
- `ADLXCSharpBind/ADLXCSharpBind.i` - Add helper function declarations following existing pattern
- `ADLXCSharpBind/ADLXQueryInterface.h` - Add function prototypes for new helper functions
- `ADLXCSharpBind/ADLXQueryInterface.cpp` - Implement helper functions for gamma data access

**New Files:**
- `IADLXGPU2Test/SerializableGammaRamp.cs` - JSON-friendly data structure
- `IADLXGPU2Test/GammaRampExtensions.cs` - Extension methods for ADLX_GammaRamp
- `IADLXGPU2Test/GammaRampJsonConverter.cs` - Custom JSON converter implementation

## Functions
Add new helper functions and extension methods for gamma ramp data access.

**New Helper Functions (C++):**
- `ExtractGammaRampData(ADLX_GammaRamp* gammaRamp, adlx_uint16* buffer, adlx_size bufferSize)` - Safely extract gamma data to buffer
- `CreateGammaRampFromData(ADLX_GammaRamp* gammaRamp, const adlx_uint16* buffer, adlx_size bufferSize)` - Create gamma ramp from data array
- `ValidateGammaRampData(const adlx_uint16* buffer, adlx_size bufferSize)` - Validate gamma data format and ranges

**New Extension Methods (C#):**
- `ToSerializableGammaRamp()` - Convert ADLX_GammaRamp to JSON-serializable format
- `ToADLXGammaRamp()` - Convert SerializableGammaRamp back to ADLX_GammaRamp
- `ToJson()` - Direct JSON serialization method
- `FromJson(string json)` - Static method for JSON deserialization

## Classes
No existing classes require modification, only new utility classes will be added.

**New Classes:**
- `SerializableGammaRamp` - Contains ushort[768] array and metadata for JSON serialization
- `GammaRampExtensions` - Static class containing extension methods
- `GammaRampJsonConverter` - Implements JsonConverter<ADLX_GammaRamp> for direct serialization support

**Existing Classes:**
- No modifications to existing SWIG-generated classes
- ADLX_GammaRamp remains unchanged to maintain compatibility

## Dependencies
Add JSON serialization capability while maintaining existing SWIG dependencies.

**New Dependencies:**
- Newtonsoft.Json (if not already present) for JSON serialization support
- System.Runtime.InteropServices for P/Invoke marshalling of gamma data

**Existing Dependencies:**
- All current SWIG-generated dependencies remain unchanged
- ADLX SDK headers and libraries maintain current usage patterns

## Testing
Create comprehensive tests for gamma ramp serialization and data integrity.

**New Test Methods:**
- Test gamma data extraction accuracy and completeness
- Test JSON serialization/deserialization round-trip integrity
- Test error handling for invalid gamma data
- Test memory management and cleanup
- Test compatibility with existing GetGammaRamp functionality

**Test Files:**
- Add test methods to existing test classes or create new test file
- Include validation of 768-element array structure (256 RGB triplets)
- Test edge cases like null objects and invalid data ranges

## Implementation Order
Sequential implementation steps to minimize conflicts and ensure successful integration.

1. **Add SWIG Interface Declarations** - Extend ADLXCSharpBind.i with new helper function declarations
2. **Implement C++ Helper Functions** - Add implementations to ADLXQueryInterface.cpp and ADLXQueryInterface.h
3. **Rebuild SWIG Bindings** - Regenerate C# bindings to include new helper functions
4. **Create SerializableGammaRamp Class** - Implement JSON-friendly data structure
5. **Implement Extension Methods** - Create conversion methods between ADLX_GammaRamp and SerializableGammaRamp
6. **Add JSON Converter** - Implement custom JsonConverter for direct ADLX_GammaRamp serialization
7. **Create Test Implementation** - Add comprehensive tests for all new functionality
8. **Integration Testing** - Test with existing gamma ramp retrieval code to ensure compatibility
