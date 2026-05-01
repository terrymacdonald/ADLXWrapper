# ADLXWrapper Facade Guide

The facades are the recommended way to use ADLXWrapper: pointer-free, no `unsafe` code required, and safer. They wrap the generated ADLX vtable interfaces (`cs_generated/`) and handle AddRef/Release, capability gating, and disposal for you. All data is returned as `*Dto` objects — plain `readonly struct`s suitable for JSON serialisation or direct consumption.

## Initialization & disposal
```csharp
using ADLXWrapper;

// ADLXApiHelper loads the ADLX DLL, calls ADLXInitialize, and owns the system lifetime.
using var adlx = ADLXApiHelper.Initialize();
// Always dispose system services before disposing adlx
using var sys = adlx.GetSystemServices();
```
- Optional features throw `ADLX_NOT_SUPPORTED` via `ADLXException`. Many helpers also expose `Try*` patterns or capability DTOs.
- All facades/helpers are `IDisposable`. Dispose them (or use `using`) to release native references. Use-after-dispose throws `ObjectDisposedException`.

## DTO naming convention
All data objects follow the `*Dto` suffix (e.g. `GpuDto`, `DisplayDto`, `GammaDto`, `GpuMetricsSnapshotDto`, `ManualFanTuningDto`). DTOs are `readonly struct`s with `init` properties and `[JsonConstructor]` support for round-trip JSON.

## Display services
Enumerate displays, query identity, VSR, pixel format, gamma/gamut/3DLUT/custom color, etc.
```csharp
var displays = sys.EnumerateDisplays();
foreach (var display in displays)
using (display)
{
    Console.WriteLine($"{display.Name} [{display.NativeResolutionWidth}x{display.NativeResolutionHeight}] @ {display.RefreshRate:F2} Hz");

    // Virtual Super Resolution
    var vsr = display.GetVirtualSuperResolutionState();
    if (vsr.supported && !vsr.enabled)
        display.SetVirtualSuperResolution(true);

    // Pixel format (if supported)
    if (display.TryGetPixelFormat(out var pf))
        Console.WriteLine($"Pixel format: {pf.format}");

    // Color capabilities — all return *Dto objects
    GammaDto gamma = display.GetGamma();
    GamutDto gamut = display.GetGamut();
    ThreeDLUTDto lut = display.GetThreeDLut();
    CustomColorDto custom = display.GetCustomColor();
    Console.WriteLine($"Gamma supported: {gamma.IsSupported}; Custom color hue supported: {custom.IsHueSupported}");
}
```

## Desktop services
Enumerate desktops and their attached displays, Eyefinity groups, rotation, etc.
```csharp
using var desktop = sys.GetDesktopServices();
var desktops = desktop.EnumerateDesktops();   // returns IReadOnlyList<DesktopDto>
foreach (var desk in desktops)
    Console.WriteLine($"{desk.Type} {desk.Width}x{desk.Height} at ({desk.TopLeftX},{desk.TopLeftY})");

// For live ADLXDesktop objects (with per-display enumeration and event listeners):
var liveDesktops = sys.EnumerateADLXGPUs(); // example — see ADLXDesktop members
```

## GPU identity
`EnumerateGPUs()` returns `IEnumerable<GpuDto>` — plain DTOs, no disposal needed. `EnumerateADLXGPUs()` returns `IReadOnlyList<ADLXGPU>` live objects for event listeners and per-GPU feature access.
```csharp
// GpuDto — lightweight, no disposal required
foreach (GpuDto gpu in sys.EnumerateGPUs())
    Console.WriteLine($"{gpu.Name} ({gpu.VRAMType}, {gpu.TotalVRAM} MB, UniqueId={gpu.UniqueId})");

// ADLXGPU — live object with full property set, must be disposed
foreach (var gpu in sys.EnumerateADLXGPUs())
using (gpu)
    Console.WriteLine($"{gpu.Name} PCIe Gen {gpu.PciBusLaneWidth}, External={gpu.IsExternal}");
```

## GPU-specific helpers — using `gpuUniqueId`
All helpers that operate on a specific GPU take an `int gpuUniqueId` (from `GpuDto.UniqueId` or `ADLXGPU.UniqueId`). No pointers or handles are passed by callers.
```csharp
var gpus = sys.EnumerateGPUs().ToList();  // List<GpuDto>
int id = gpus[0].UniqueId;
```

## Performance monitoring
```csharp
using var perf = sys.GetPerformanceMonitoringServices();
int gpuId = sys.EnumerateGPUs().First().UniqueId;

if (perf.TryGetCurrentGpuMetrics(gpuId, out GpuMetricsSnapshotDto metrics))
    Console.WriteLine($"GPU Temp: {metrics.Temperature:F1}°C, Usage: {metrics.Usage:F1}%, Clock: {metrics.ClockSpeed} MHz");

SystemMetricsSnapshotDto systemMetrics = perf.GetCurrentSystemMetrics();
Console.WriteLine($"CPU: {systemMetrics.CpuUsage:F1}%, RAM: {systemMetrics.SystemRam} MB");

// Sampling interval range is an IntRangeDto
IntRangeDto range = perf.GetSamplingIntervalRange();
Console.WriteLine($"Sampling interval: {range.MinValue}–{range.MaxValue} ms (step {range.Step})");
```

## 3D settings
Access Anti-Lag, Boost, RSR, Chill, Sharpening, and more. Optional features are safely gated.
```csharp
using var settings = sys.Get3DSettingsServices();
int gpuId = sys.EnumerateGPUs().First().UniqueId;

All3DSettingsDto all = settings.GetAll3DSettings(gpuId);
Console.WriteLine($"Anti-Lag enabled: {all.AntiLag.IsEnabled}, Boost enabled: {all.Boost.IsEnabled}");

if (settings.TryGetFluidMotionFrames(gpuId, out FluidMotionFramesDto fmf) && fmf.IsSupported)
    Console.WriteLine($"Fluid Motion Frames: {fmf.IsEnabled}");
```

## GPU tuning (read-only patterns shown)
```csharp
using var tuning = sys.GetGPUTuningServices();
int gpuId = sys.EnumerateGPUs().First().UniqueId;

GpuTuningCapabilitiesDto caps = tuning.GetCapabilities(gpuId);
Console.WriteLine($"Manual fan: {caps.IsManualFanTuningSupported}, Manual GFX: {caps.IsManualGfxTuningSupported}");

if (tuning.TryGetPresetTuning(gpuId, out PresetTuningDto preset))
    Console.WriteLine($"Preset supported: {preset.IsSupported}, current={preset.CurrentPreset}");

if (tuning.TryGetManualFanTuning(gpuId, out ManualFanTuningDto fan))
    Console.WriteLine($"Fan manual supported: {fan.IsSupported}, RPM range {fan.MinFanSpeedRpm}–{fan.MaxFanSpeedRpm}");
```

## Power tuning (SmartShift Max/Eco, GPUConnect)
```csharp
using var power = sys.GetPowerTuningServices();

SmartShiftMaxDto ssm = power.GetSmartShiftMax();
Console.WriteLine($"SmartShift Max supported={ssm.IsSupported}, mode={ssm.BiasMode}, value={ssm.BiasValue}, range=({ssm.BiasRange.MinValue}–{ssm.BiasRange.MaxValue})");

SmartShiftEcoDto eco = power.GetSmartShiftEco();
Console.WriteLine($"SmartShift Eco supported={eco.IsSupported}, enabled={eco.IsEnabled}");

// Manual power tuning requires both the power helper and a GPU tuning helper
using var tuning = sys.GetGPUTuningServices();
int gpuId = sys.EnumerateGPUs().First().UniqueId;
if (power.TryGetManualPowerTuning(gpuId, tuning, out ManualPowerTuningDto mpt))
    Console.WriteLine($"Power limit: {mpt.PowerLimitValue} (range {mpt.PowerLimitRange.MinValue}–{mpt.PowerLimitRange.MaxValue})");
```

## Multimedia (Video Upscale, Video Super Resolution)
```csharp
using var mm = sys.GetMultimediaServices();
int gpuId = sys.EnumerateGPUs().First().UniqueId;

if (mm.TryGetVideoUpscale(gpuId, out VideoUpscaleDto upscale) && upscale.IsSupported)
    Console.WriteLine($"Video Upscale enabled={upscale.IsEnabled}, sharpness={upscale.Sharpness} (range {upscale.SharpnessRange.MinValue}–{upscale.SharpnessRange.MaxValue})");

if (mm.TryGetVideoSuperResolution(gpuId, out VideoSuperResolutionDto vsr) && vsr.IsSupported)
    Console.WriteLine($"Video Super Resolution enabled={vsr.IsEnabled}");
```

## Initialization patterns and disposal rules
- Always scope `ADLXApiHelper` outermost, then system services, then per-feature helpers/facades.
- Dispose `ADLXDisplay`, `ADLXGPU`, and `ADLXDesktop` live objects when done. DTOs (`GpuDto`, `DisplayDto`, etc.) are plain structs — no disposal.
- Optional features are expected to be missing on some hardware; prefer capability queries or `Try*` methods.

## API docs
Browse `APIDocs/_site/index.html` for the full public surface, including DTOs and helper methods.

## Samples
Each sample is a console menu with both Facade and Native flows:
- `Samples/DisplaySample`
- `Samples/DesktopSample`
- `Samples/DisplayColorSample`
- `Samples/PerfMonitoringSample`
- `Samples/MultimediaSample`
- `Samples/PowerTuningSample`

Run with `dotnet run --project Samples/<SampleName>/<SampleName>.csproj`.

## Native path
If you need raw vtable calls, see `ADLXWrapper/README.Native.md` for initialization/teardown, `ComPtr` usage, and patterns aligned with the ADLX SDK samples and `ADLXWrapper.NativeTests`.
