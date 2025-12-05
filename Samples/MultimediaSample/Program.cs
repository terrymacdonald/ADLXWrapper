using ADLXWrapper;

Console.WriteLine("=== ADLX Multimedia Sample (Video Upscale / VSR) ===");

using var adlx = ADLXApi.Initialize();
var gpus = adlx.EnumerateGPUHandles();
if (gpus.Length == 0)
{
    Console.WriteLine("No AMD GPU found.");
    return;
}

using var sys = adlx.GetSystemServicesHandle();
using var mm = ADLXMultimediaHelpers.GetMultimediaServices(sys);
var gpu = gpus[0];

try
{
    using var upscale = ADLXMultimediaHelpers.GetVideoUpscale(mm, gpu);
    var (sup, en, range, minRes) = ADLXMultimediaHelpers.GetVideoUpscaleState(upscale);
    Console.WriteLine($"Video Upscale: supported={sup}, enabled={en}, scaleRange=({range.minValue}-{range.maxValue}), minInputRes={minRes}");

    using var vsr = ADLXMultimediaHelpers.GetVideoSuperResolution(mm, gpu);
    var (vsrSup, vsrEn) = ADLXMultimediaHelpers.GetVideoSuperResolutionState(vsr);
    Console.WriteLine($"Video Super Resolution: supported={vsrSup}, enabled={vsrEn}");

    // Toggle example (commented to stay read-only)
    // if (sup) ADLXMultimediaHelpers.SetVideoUpscaleEnabled(upscale, !en);
    // if (vsrSup) ADLXMultimediaHelpers.SetVideoSuperResolutionEnabled(vsr, !vsrEn);
}
catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
{
    Console.WriteLine("Multimedia features not supported on this hardware/driver.");
}
finally
{
    foreach (var h in gpus)
    {
        try { h.Dispose(); } catch { }
    }
}
