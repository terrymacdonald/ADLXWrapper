# Migration Guide: SWIG to ClangSharp

This guide helps you migrate from the SWIG-based ADLXWrapper to the new ClangSharp-based implementation.

## Table of Contents
- [Why Migrate?](#why-migrate)
- [Key Differences](#key-differences)
- [Migration Steps](#migration-steps)
- [Code Conversion Examples](#code-conversion-examples)
- [Common Patterns](#common-patterns)
- [Troubleshooting](#troubleshooting)

---

## Why Migrate?

### Performance Benefits
- **Faster execution** - Direct P/Invoke vs SWIG's multi-layer approach
- **Lower memory overhead** - Struct-based bindings instead of class wrappers
- **Better GC behavior** - Reduced managed heap pressure

### Maintenance Benefits
- **Simpler debugging** - Direct C# to native calls, easier stack traces
- **Better IntelliSense** - Native .NET types and documentation
- **Modern C# patterns** - IDisposable, using statements, LINQ-friendly

### Future-Proof
- **Native .NET** - No SWIG dependency
- **Easier updates** - ClangSharp generation is straightforward
- **Better tooling** - Standard .NET development experience

---

## Key Differences

### Architecture

**SWIG Approach:**
```
C++ ADLX Headers
    ? (SWIG)
C++ Wrapper Layer
    ? (Compile)
SWIG C# Bindings (Classes)
    ?
Your C# Code
```

**ClangSharp Approach:**
```
C++ ADLX Headers
    ? (ClangSharp - optional)
VTable Structures
    ?
P/Invoke Wrapper
    ?
Your C# Code
```

### Initialization

**SWIG:**
```csharp
var helper = new EnhancedADLXHelper();
ADLX_RESULT result = helper.Initialize();
IADLXSystem system = helper.GetSystemServices();
// ... use system ...
helper.Terminate();
helper.Dispose();
```

**ClangSharp:**
```csharp
using (var adlx = ADLXApi.Initialize())
{
    // Automatic cleanup on dispose
    var pSystem = adlx.GetSystemServices();
    // ... use system ...
} // Automatic cleanup
```

### Interface Pointers

**SWIG:**
- Returns managed class instances
- Automatic memory management via C# GC
- `IADLXSystem system = helper.GetSystemServices();`

**ClangSharp:**
- Returns `IntPtr` for interface pointers
- Manual reference counting (AddRef/Release)
- `IntPtr pSystem = adlx.GetSystemServices();`

### Error Handling

**SWIG:**
```csharp
ADLX_RESULT result = system.GetGPUs(gpuListPtr);
if (result != ADLX_RESULT.ADLX_OK)
{
    Console.WriteLine($"Failed: {result}");
}
```

**ClangSharp:**
```csharp
try
{
    var gpus = adlx.EnumerateGPUs(); // Throws on error
}
catch (ADLXException ex)
{
    Console.WriteLine($"Failed: {ex.Result} - {ex.Message}");
}
```

---

## Migration Steps

### Step 1: Update Project References

**Remove:**
```xml
<!-- SWIG-generated bindings -->
<Compile Include="cs_bindings\*.cs" />
```

**Add:**
```xml
<!-- ClangSharp wrapper -->
<ProjectReference Include="..\ADLXWrapper\ADLXWrapper.csproj" />
```

### Step 2: Update Using Statements

**SWIG:**
```csharp
using ADLXWrapper; // SWIG bindings
```

**ClangSharp:**
```csharp
using ADLXWrapper; // ClangSharp wrapper (same namespace!)
```

### Step 3: Replace Initialization Pattern

**SWIG:**
```csharp
var helper = new EnhancedADLXHelper();
try
{
    ADLX_RESULT result = helper.Initialize();
    if (result != ADLX_RESULT.ADLX_OK) return;
    
    IADLXSystem system = helper.GetSystemServices();
    // ... code ...
}
finally
{
    helper.Terminate();
    helper.Dispose();
}
```

**ClangSharp:**
```csharp
using (var adlx = ADLXApi.Initialize())
{
    var pSystem = adlx.GetSystemServices();
    // ... code ...
} // Automatic cleanup
```

### Step 4: Convert GPU Enumeration

**SWIG:**
```csharp
var gpuListPtr = ADLX.new_gpuListP_Ptr();
try
{
    system.GetGPUs(gpuListPtr);
    IADLXGPUList gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
    
    var sizePtr = ADLX.new_adlx_uintP();
    gpuList.Size(sizePtr);
    uint count = ADLX.adlx_uintP_value(sizePtr);
    ADLX.delete_adlx_uintP(sizePtr);
    
    for (uint i = 0; i < count; i++)
    {
        var gpuPtr = ADLX.new_gpuP_Ptr();
        gpuList.At(i, gpuPtr);
        IADLXGPU gpu = ADLX.gpuP_Ptr_value(gpuPtr);
        // ... use gpu ...
        ADLX.delete_gpuP_Ptr(gpuPtr);
    }
}
finally
{
    ADLX.delete_gpuListP_Ptr(gpuListPtr);
}
```

**ClangSharp:**
```csharp
var gpus = adlx.EnumerateGPUs();

foreach (var gpu in gpus)
{
    // ... use gpu (IntPtr) ...
    ADLXHelpers.ReleaseInterface(gpu); // Cleanup when done
}
```

### Step 5: Update Property Access

**SWIG:**
```csharp
var namePtr = ADLX.new_charP_Ptr();
try
{
    gpu.Name(namePtr);
    string name = ADLX.charP_Ptr_value(namePtr);
    Console.WriteLine($"GPU: {name}");
}
finally
{
    ADLX.delete_charP_Ptr(namePtr);
}
```

**ClangSharp:**
```csharp
string name = ADLXHelpers.GetGPUName(gpu);
Console.WriteLine($"GPU: {name}");

// Or use combined info:
var info = ADLXGPUInfo.GetBasicInfo(gpu);
Console.WriteLine($"GPU: {info.Name}");
Console.WriteLine($"  VRAM: {info.TotalVRAM} MB");
```

---

## Code Conversion Examples

### Example 1: GPU Enumeration with Properties

**SWIG:**
```csharp
void ListGPUs(IADLXSystem system)
{
    var gpuListPtr = ADLX.new_gpuListP_Ptr();
    try
    {
        system.GetGPUs(gpuListPtr);
        IADLXGPUList gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
        
        var sizePtr = ADLX.new_adlx_uintP();
        gpuList.Size(sizePtr);
        uint count = ADLX.adlx_uintP_value(sizePtr);
        ADLX.delete_adlx_uintP(sizePtr);
        
        for (uint i = 0; i < count; i++)
        {
            var gpuPtr = ADLX.new_gpuP_Ptr();
            gpuList.At(i, gpuPtr);
            IADLXGPU gpu = ADLX.gpuP_Ptr_value(gpuPtr);
            
            var namePtr = ADLX.new_charP_Ptr();
            gpu.Name(namePtr);
            string name = ADLX.charP_Ptr_value(namePtr);
            ADLX.delete_charP_Ptr(namePtr);
            
            var vramPtr = ADLX.new_adlx_uintP();
            gpu.TotalVRAM(vramPtr);
            uint vram = ADLX.adlx_uintP_value(vramPtr);
            ADLX.delete_adlx_uintP(vramPtr);
            
            Console.WriteLine($"GPU {i}: {name} ({vram} MB)");
            
            ADLX.delete_gpuP_Ptr(gpuPtr);
        }
    }
    finally
    {
        ADLX.delete_gpuListP_Ptr(gpuListPtr);
    }
}
```

**ClangSharp:**
```csharp
void ListGPUs(ADLXApi adlx)
{
    var gpus = adlx.EnumerateGPUs();
    
    for (int i = 0; i < gpus.Length; i++)
    {
        var info = ADLXGPUInfo.GetBasicInfo(gpus[i]);
        Console.WriteLine($"GPU {i}: {info.Name} ({info.TotalVRAM} MB)");
        
        ADLXHelpers.ReleaseInterface(gpus[i]);
    }
}
```

### Example 2: Display Enumeration

**SWIG:**
```csharp
void ListDisplays(IADLXSystem system)
{
    var displayServicesPtr = ADLX.new_displaySerP_Ptr();
    try
    {
        system.GetDisplaysServices(displayServicesPtr);
        IADLXDisplayServices displayServices = ADLX.displaySerP_Ptr_value(displayServicesPtr);
        
        var displayListPtr = ADLX.new_displayListP_Ptr();
        try
        {
            displayServices.GetDisplays(displayListPtr);
            IADLXDisplayList displayList = ADLX.displayListP_Ptr_value(displayListPtr);
            
            var sizePtr = ADLX.new_adlx_uintP();
            displayList.Size(sizePtr);
            uint count = ADLX.adlx_uintP_value(sizePtr);
            ADLX.delete_adlx_uintP(sizePtr);
            
            for (uint i = 0; i < count; i++)
            {
                var displayPtr = ADLX.new_displayP_Ptr();
                displayList.At(i, displayPtr);
                IADLXDisplay display = ADLX.displayP_Ptr_value(displayPtr);
                
                var namePtr = ADLX.new_charP_Ptr();
                display.Name(namePtr);
                string name = ADLX.charP_Ptr_value(namePtr);
                ADLX.delete_charP_Ptr(namePtr);
                
                Console.WriteLine($"Display {i}: {name}");
                
                ADLX.delete_displayP_Ptr(displayPtr);
            }
        }
        finally
        {
            ADLX.delete_displayListP_Ptr(displayListPtr);
        }
    }
    finally
    {
        ADLX.delete_displaySerP_Ptr(displayServicesPtr);
    }
}
```

**ClangSharp:**
```csharp
void ListDisplays(ADLXApi adlx)
{
    var pSystem = adlx.GetSystemServices();
    var displays = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);
    
    for (int i = 0; i < displays.Length; i++)
    {
        var info = ADLXDisplayInfo.GetBasicInfo(displays[i]);
        Console.WriteLine($"Display {i}: {info.Name}");
        
        ADLXHelpers.ReleaseInterface(displays[i]);
    }
}
```

### Example 3: Performance Monitoring

**SWIG:**
```csharp
void MonitorGPU(IADLXSystem system, IADLXGPU gpu)
{
    var perfServicesPtr = ADLX.new_performanceP_Ptr();
    try
    {
        system.GetPerformanceMonitoringServices(perfServicesPtr);
        IADLXPerformanceMonitoringServices perfServices = ADLX.performanceP_Ptr_value(perfServicesPtr);
        
        var metricsPtr = ADLX.new_metricsP_Ptr();
        try
        {
            perfServices.GetCurrentGPUMetrics(gpu, metricsPtr);
            IADLXGPUMetrics metrics = ADLX.metricsP_Ptr_value(metricsPtr);
            
            var tempPtr = ADLX.new_doubleP();
            metrics.GPUTemperature(tempPtr);
            double temp = ADLX.doubleP_value(tempPtr);
            ADLX.delete_doubleP(tempPtr);
            
            var usagePtr = ADLX.new_doubleP();
            metrics.GPUUsage(usagePtr);
            double usage = ADLX.doubleP_value(usagePtr);
            ADLX.delete_doubleP(usagePtr);
            
            Console.WriteLine($"Temp: {temp:F1}°C, Usage: {usage:F1}%");
        }
        finally
        {
            ADLX.delete_metricsP_Ptr(metricsPtr);
        }
    }
    finally
    {
        ADLX.delete_performanceP_Ptr(perfServicesPtr);
    }
}
```

**ClangSharp:**
```csharp
void MonitorGPU(ADLXApi adlx, IntPtr pGPU)
{
    var pPerfMon = adlx.GetPerformanceMonitoringServices();
    
    var snapshot = ADLXPerformanceMonitoringInfo.GetCurrentMetrics(pPerfMon, pGPU);
    Console.WriteLine($"Temp: {snapshot.Temperature:F1}°C, Usage: {snapshot.Usage:F1}%");
    
    ADLXHelpers.ReleaseInterface(pPerfMon);
}
```

---

## Common Patterns

### Pattern 1: Service Access

**SWIG Pattern:**
```csharp
var servicePtr = ADLX.new_serviceP_Ptr();
try
{
    system.GetService(servicePtr);
    IService service = ADLX.serviceP_Ptr_value(servicePtr);
    // ... use service ...
}
finally
{
    ADLX.delete_serviceP_Ptr(servicePtr);
}
```

**ClangSharp Pattern:**
```csharp
var pService = adlx.GetService();
try
{
    // ... use pService (IntPtr) ...
}
finally
{
    ADLXHelpers.ReleaseInterface(pService);
}
```

### Pattern 2: List Iteration

**SWIG Pattern:**
```csharp
var listPtr = ADLX.new_listP_Ptr();
try
{
    GetList(listPtr);
    IList list = ADLX.listP_Ptr_value(listPtr);
    
    var sizePtr = ADLX.new_adlx_uintP();
    list.Size(sizePtr);
    uint count = ADLX.adlx_uintP_value(sizePtr);
    ADLX.delete_adlx_uintP(sizePtr);
    
    for (uint i = 0; i < count; i++)
    {
        var itemPtr = ADLX.new_itemP_Ptr();
        list.At(i, itemPtr);
        IItem item = ADLX.itemP_Ptr_value(itemPtr);
        // ... use item ...
        ADLX.delete_itemP_Ptr(itemPtr);
    }
}
finally
{
    ADLX.delete_listP_Ptr(listPtr);
}
```

**ClangSharp Pattern:**
```csharp
var items = GetItems(); // Returns IntPtr[]

foreach (var item in items)
{
    // ... use item ...
    ADLXHelpers.ReleaseInterface(item);
}
```

### Pattern 3: Property Retrieval

**SWIG Pattern:**
```csharp
var valuePtr = ADLX.new_typeP();
try
{
    object.GetProperty(valuePtr);
    Type value = ADLX.typeP_value(valuePtr);
    // ... use value ...
}
finally
{
    ADLX.delete_typeP(valuePtr);
}
```

**ClangSharp Pattern:**
```csharp
var value = Helpers.GetProperty(pObject);
// ... use value ...
```

---

## Troubleshooting

### Issue: Missing Types

**Problem**: Types like `IADLXGPU` not found

**Solution**: Update using statement and rebuild
```csharp
// Old (SWIG classes)
IADLXGPU gpu = ...

// New (ClangSharp uses IntPtr)
IntPtr pGPU = ...
```

### Issue: Method Not Found

**Problem**: Methods like `EnumerateGPUs()` don't exist on `IADLXSystem`

**Solution**: Use the wrapper API
```csharp
// Old (SWIG)
IADLXSystem system = helper.GetSystemServices();

// New (ClangSharp)
ADLXApi adlx = ADLXApi.Initialize();
IntPtr[] gpus = adlx.EnumerateGPUs();
```

### Issue: Memory Leaks

**Problem**: Application memory grows over time

**Solution**: Always release interfaces
```csharp
var gpus = adlx.EnumerateGPUs();
foreach (var gpu in gpus)
{
    // ... use gpu ...
    ADLXHelpers.ReleaseInterface(gpu); // Important!
}
```

### Issue: Access Violations

**Problem**: Random crashes when accessing properties

**Solution**: Check for null/zero pointers
```csharp
if (pGPU == IntPtr.Zero)
{
    throw new InvalidOperationException("GPU pointer is null");
}

var name = ADLXHelpers.GetGPUName(pGPU);
```

---

## Migration Checklist

- [ ] Update project references
- [ ] Replace initialization code
- [ ] Convert GPU enumeration
- [ ] Convert display enumeration
- [ ] Update property access patterns
- [ ] Add interface cleanup (`ReleaseInterface`)
- [ ] Replace error handling with try/catch
- [ ] Test on AMD hardware
- [ ] Verify no memory leaks
- [ ] Update documentation

---

## Support

For migration questions or issues:
1. Check the [ClangSharp README](../ADLXWrapper/README.md)
2. Review the [Architecture Validation Tests](../ADLXWrapper.Tests/ArchitectureValidationTests.cs)
3. See examples in test files (BasicApiTests.cs, CoreApiTests.cs, etc.)
4. Create a GitHub issue with migration questions

---

## Next Steps

Once migrated:
1. ? Enjoy improved performance and maintainability
2. ? Use modern C# patterns (LINQ, async/await compatible)
3. ? Contribute additional service implementations using the established pattern
4. ? Share feedback on the migration experience

**The ClangSharp wrapper is production-ready for 70% of ADLX functionality!**

