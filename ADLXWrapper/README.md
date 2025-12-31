# ADLXWrapper Facade Guide

The facades are the recommended way to use ADLXWrapper: pointer-free, version-aware, and safer. They wrap the generated ADLX vtable interfaces (`cs_generated/`) and handle AddRef/Release, capability gating, and disposal for you.

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

    // Color capabilities
    var gamma = display.GetGamma();
    var gamut = display.GetGamut();
    var lut = display.GetThreeDLut();
    var custom = display.GetCustomColor();
    Console.WriteLine($"Gamma supported: {gamma.IsSupported}; Custom color hue supported: {custom.IsHueSupported}");
}
```

## Desktop services
Enumerate desktops and their attached displays, Eyefinity groups, rotation, etc.
```csharp
var desktops = sys.GetDesktopServices().EnumerateDesktops();
foreach (var desk in desktops)
using (desk)
{
    var id = desk.Identity;
    Console.WriteLine($"{id.Type} {id.Width}x{id.Height} at ({id.TopLeftX},{id.TopLeftY})");
    foreach (var disp in desk.EnumerateDisplaysForDesktop())
    using (disp)
        Console.WriteLine($"  Display: {disp.Name} {disp.NativeResolutionWidth}x{disp.NativeResolutionHeight}");
}
```

## GPU identity
```csharp
foreach (var gpu in sys.EnumerateADLXGPUs())
using (gpu)
{
    var id = gpu.Identity;
    Console.WriteLine($"{id.Name} ({id.Brand}) VRAM: {id.TotalVRAM} MB, Type: {id.VRAMType}");
    Console.WriteLine($"PCIe Gen: {id.PcieGeneration}, Lane Width: {id.PcieLaneWidth}, External: {id.IsExternal}");
    Console.WriteLine($"Driver: {id.AMDSoftwareVersion}, Windows Driver: {id.AMDWindowsDriverVersion}, LUID: {id.Luid}");
}
```

## Performance monitoring
```csharp
using var perf = sys.GetPerformanceMonitoringServices();
var gpuHandle = sys.EnumerateGPUsHandle().FirstOrDefault();
if (gpuHandle != null)
{
    using var gpu = gpuHandle;
    var metrics = perf.GetCurrentGpuMetrics(gpu.As<IADLXGPU>());
    Console.WriteLine($"GPU Temp: {metrics.Temperature:F1}C, Usage: {metrics.Usage:F1}%, Clock: {metrics.ClockSpeed}MHz");
}
var systemMetrics = perf.GetCurrentSystemMetrics();
Console.WriteLine($"CPU: {systemMetrics.CpuUsage:F1}%, RAM: {systemMetrics.SystemRam}MB");
```

## 3D settings
Access Anti-Lag, Boost, RSR, Chill, Sharpening, and more. Optional features are safely gated.
```csharp
using var settings = sys.Get3DSettingsServices();
var gpus = sys.EnumerateGPUsHandle();
if (gpus.Length > 0)
{
    using var gpu = gpus[0];
    var boost = settings.GetRadeonBoost(gpu.As<IADLXGPU>());
    if (boost.IsSupported && !boost.IsEnabled)
        settings.SetRadeonBoost(gpu.As<IADLXGPU>(), true);
}
```

## GPU tuning (read-only patterns shown)
```csharp
using var tuning = sys.GetGPUTuningServices();
var gpuHandle = sys.EnumerateGPUsHandle().FirstOrDefault();
if (gpuHandle != null)
{
    using var gpu = gpuHandle;
    var preset = tuning.GetPresetTuning(gpu.As<IADLXGPU>());
    Console.WriteLine($"Preset supported: {preset.IsSupported}, current={preset.CurrentPreset}");

    var fan = tuning.GetManualFanTuning(gpu.As<IADLXGPU>());
    Console.WriteLine($"Fan manual supported: {fan.IsSupported}, RPM range {fan.MinFanSpeedRpm}-{fan.MaxFanSpeedRpm}");
}
```

## Power tuning (SmartShift Max/Eco, GPUConnect)
```csharp
using var power = sys.GetPowerTuningServices();
var ssm = power.GetSmartShiftMax();
Console.WriteLine($"SmartShift Max supported={ssm.IsSupported}, mode={ssm.BiasMode}, value={ssm.BiasValue}, range=({ssm.BiasRange.minValue}-{ssm.BiasRange.maxValue})");
```

## Multimedia (VSR, Video Upscale)
```csharp
using var mm = sys.GetMultimediaServices();
var gpuHandle = sys.EnumerateGPUsHandle().FirstOrDefault();
if (gpuHandle != null)
{
    using var gpu = gpuHandle;
    var vsr = mm.GetVideoSuperResolution(gpu.As<IADLXGPU>());
    Console.WriteLine($"VSR supported={vsr.IsSupported}, enabled={vsr.IsEnabled}");
}
```

## Initialization patterns and disposal rules
- Always scope `ADLXApiHelper` outermost, then system services, then per-feature helpers/facades.
- Dispose listeners/handles (event handles, display/GPU facades) when done.
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
