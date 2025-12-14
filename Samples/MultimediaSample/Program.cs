using ADLXWrapper;

Console.WriteLine("=== ADLX Multimedia Sample (Video Upscale / VSR) ===");

using var adlx = ADLXApi.Initialize();
var gpus = adlx.EnumerateGPUHandles();
if (gpus.Length == 0)
{
    Console.WriteLine("No AMD GPU found.");
    return;
}

try
{
    using var sys = adlx.GetSystemServicesProfile();
    var gpu = gpus[0];
    using var mm = sys.GetMultimediaServices(gpu);

    var profile = mm.GetProfile();

    if (profile.VideoUpscale.HasValue)
    {
        var upscale = profile.VideoUpscale.Value;
        Console.WriteLine($"Video Upscale: supported={upscale.IsSupported}, enabled={upscale.IsEnabled}, sharpness={upscale.Sharpness}, range=({upscale.SharpnessRange.minValue}-{upscale.SharpnessRange.maxValue})");
    }
    else
    {
        Console.WriteLine("Video Upscale: not supported");
    }

    if (profile.VideoSuperResolution.HasValue)
    {
        var vsr = profile.VideoSuperResolution.Value;
        Console.WriteLine($"Video Super Resolution: supported={vsr.IsSupported}, enabled={vsr.IsEnabled}");
    }
    else
    {
        Console.WriteLine("Video Super Resolution: not supported");
    }

    // Toggle example (commented to stay read-only)
    // var newProfile = new MultimediaProfile(
    //     profile.VideoUpscale?.WithEnabled(!profile.VideoUpscale.Value.IsEnabled),
    //     profile.VideoSuperResolution?.WithEnabled(!profile.VideoSuperResolution.Value.IsEnabled));
    // mm.ApplyProfile(newProfile);
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
