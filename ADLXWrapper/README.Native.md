# ADLXWrapper Native Guide

Use the native path when you need direct ADLX vtable access that mirrors the AMD ADLX SDK samples. You manage pointers and interfaces explicitly; `ComPtr<T>` helps with lifetime.

## Initialization and teardown
Preferred pattern (matches Native tests and samples):
```csharp
using ADLXWrapper;

unsafe
{
    using var adlx = ADLXApi.Initialize();           // Loads ADLX DLL and calls ADLXInitialize
    using var system = adlx.GetSystemServices();     // Wraps IADLXSystem* in a ComPtr
    // ... use system->Get*Services(...) via pointers
} // Dispose order terminates ADLX and frees the DLL
```
- `ADLXApi.InitializeWithCallerAdl()` is available when you already have an ADL session.
- Dispose child service pointers before disposing the ADLX session; use `ComPtr<T>` or `ADLXUtils.ReleaseInterface` to avoid leaks.
- Optional features must be gated (`ADLX_NOT_SUPPORTED`) rather than assumed.

## Pointer safety helpers
- `ComPtr<T>`: RAII wrapper that calls `Release()` on dispose.
- `ADLXUtils.TryQueryInterface(IntPtr, string, out IntPtr)`: query extended interfaces (e.g., `IADLXSystem1`, `IADLXSystem2`).
- `ADLXUtils.AddRefInterface/ReleaseInterface`: manual ref-count helpers when you share pointers.

## Common native patterns

### Enumerate displays (native)
```csharp
unsafe
{
    using var adlx = ADLXApi.Initialize();
    using var system = adlx.GetSystemServices();

    IADLXDisplayServices* ds = null;
    var dsResult = system.Get()->GetDisplaysServices(&ds);
    if (dsResult != ADLX_RESULT.ADLX_OK || ds == null) return;
    using var dispServices = new ComPtr<IADLXDisplayServices>(ds);

    IADLXDisplayList* list = null;
    if (dispServices.Get()->GetDisplays(&list) != ADLX_RESULT.ADLX_OK || list == null) return;
    using var displays = new ComPtr<IADLXDisplayList>(list);

    var count = displays.Get()->Size();
    for (uint i = 0; i < count; i++)
    {
        IADLXDisplay* pDisp = null;
        if (displays.Get()->At(i, &pDisp) != ADLX_RESULT.ADLX_OK || pDisp == null) continue;
        using var disp = new ComPtr<IADLXDisplay>(pDisp);

        sbyte* namePtr = null;
        disp.Get()->Name(&namePtr);
        var name = Marshal.PtrToStringUTF8((IntPtr)namePtr) ?? string.Empty;
        int w = 0, h = 0;
        disp.Get()->NativeResolution(&w, &h);
        double rr = 0;
        disp.Get()->RefreshRate(&rr);
        Console.WriteLine($"{name} {w}x{h} @ {rr:F2} Hz");
    }
}
```

### Performance monitoring (native)
```csharp
IADLXPerformanceMonitoringServices* perf = null;
if (system.Get()->GetPerformanceMonitoringServices(&perf) == ADLX_RESULT.ADLX_OK && perf != null)
{
    using var perfServices = new ComPtr<IADLXPerformanceMonitoringServices>(perf);

    IADLXGPUList* gpuList = null;
    system.Get()->GetGPUs(&gpuList);
    using var gpus = new ComPtr<IADLXGPUList>(gpuList);

    IADLXGPU* pGpu = null;
    gpus.Get()->At(0, &pGpu);
    using var gpu = new ComPtr<IADLXGPU>(pGpu);

    IADLXGPUMetrics* pMetrics = null;
    if (perfServices.Get()->GetCurrentGPUMetrics(gpu.Get(), &pMetrics) == ADLX_RESULT.ADLX_OK && pMetrics != null)
    {
        using var metrics = new ComPtr<IADLXGPUMetrics>(pMetrics);
        double usage = 0, temp = 0;
        metrics.Get()->GPUUsage(&usage);
        metrics.Get()->GPUTemperature(&temp);
    }
}
```

### Power tuning (native, SmartShift Max)
```csharp
IntPtr pSystem1;
if (ADLXUtils.TryQueryInterface((IntPtr)system.Get(), nameof(IADLXSystem1), out pSystem1) && pSystem1 != IntPtr.Zero)
{
    using var system1 = new ComPtr<IADLXSystem1>((IADLXSystem1*)pSystem1);
    IADLXPowerTuningServices* pServices = null;
    if (system1.Get()->GetPowerTuningServices(&pServices) == ADLX_RESULT.ADLX_OK && pServices != null)
    {
        using var services = new ComPtr<IADLXPowerTuningServices>(pServices);
        IADLXSmartShiftMax* pMax = null;
        if (services.Get()->GetSmartShiftMax(&pMax) == ADLX_RESULT.ADLX_OK && pMax != null)
        {
            using var max = new ComPtr<IADLXSmartShiftMax>(pMax);
            bool supported = false;
            max.Get()->IsSupported(&supported);
        }
    }
}
```

## Interface versioning
- Core system services: `IADLXSystem` (displays, desktops, 3D, GPU tuning, perf).
- Extended services: `IADLXSystem1` (power tuning), `IADLXSystem2` (multimedia). Query them explicitly before calling extended methods.
- Service extensions follow the same pattern (e.g., `IADLXPowerTuningServices1`).

## Alignment with ADLX SDK samples
- Follow the ADLX SDK Samples for call ordering and capability checks; the Native tests (`ADLXWrapper.NativeTests`) mirror those patterns.
- Optional features should be gated with `ADLX_NOT_SUPPORTED` or feature-specific `IsSupported` queries.
- Dispose service pointers before terminating ADLX to avoid crashes after `ADLXTerminate`.

## Docs and references
- API surface: `APIDocs/_site/index.html` (generated XML docs for all interfaces).
- Tests: `ADLXWrapper.NativeTests` show end-to-end native usage of generated interfaces.
- Facade comparison: see `ADLXWrapper/README.md` for the pointer-free equivalents.
