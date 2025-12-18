using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Multimedia Sample (Video Upscale / VSR) ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    var gpus = sysHelper.EnumerateGPUHandles();
    if (gpus.Length == 0)
    {
        Console.WriteLine("No AMD GPU found.");
        return;
    }

    var sys = sysHelper.GetSystemServicesNative();
    using var mm = AdlxInterfaceHandle.From(ADLXMultimediaHelpers.GetMultimediaServices(sys), addRef: false);
    var gpu = gpus[0];

    try
    {
        var upscale = ADLXMultimediaHelpers.GetVideoUpscale(mm.As<IADLXMultimediaServices>(), gpu.As<IADLXGPU>());
        Console.WriteLine($"Video Upscale: supported={upscale.IsSupported}, enabled={upscale.IsEnabled}, sharpness={upscale.Sharpness}, range=({upscale.SharpnessRange.minValue}-{upscale.SharpnessRange.maxValue})");

        var vsr = ADLXMultimediaHelpers.GetVideoSuperResolution(mm.As<IADLXMultimediaServices>(), gpu.As<IADLXGPU>());
        Console.WriteLine($"Video Super Resolution: supported={vsr.IsSupported}, enabled={vsr.IsEnabled}");

        // Toggle example (commented to stay read-only)
        // if (upscale.IsSupported) ADLXMultimediaHelpers.SetVideoUpscaleEnabled(mm.As<IADLXMultimediaServices>(), gpu.As<IADLXGPU>(), !upscale.IsEnabled);
        // if (vsr.IsSupported) ADLXMultimediaHelpers.SetVideoSuperResolutionEnabled(mm.As<IADLXMultimediaServices>(), gpu.As<IADLXGPU>(), !vsr.IsEnabled);
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
}
