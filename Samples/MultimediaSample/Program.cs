using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Multimedia Sample (Video Upscale / VSR) ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    var gpus = sysHelper.EnumerateGPUsHandle();
    if (gpus.Length == 0)
    {
        Console.WriteLine("No AMD GPU found.");
        return;
    }

    using var mmHelper = new ADLXMultimediaServicesHelper(sysHelper.GetMultimediaServicesNative());
    var gpu = gpus[0];

    try
    {
        var upscale = mmHelper.GetVideoUpscale(gpu.As<IADLXGPU>());
        Console.WriteLine($"Video Upscale: supported={upscale.IsSupported}, enabled={upscale.IsEnabled}, sharpness={upscale.Sharpness}, range=({upscale.SharpnessRange.minValue}-{upscale.SharpnessRange.maxValue})");

        var vsr = mmHelper.GetVideoSuperResolution(gpu.As<IADLXGPU>());
        Console.WriteLine($"Video Super Resolution: supported={vsr.IsSupported}, enabled={vsr.IsEnabled}");

        try
        {
            using var listener = mmHelper.AddMultimediaEventListener(evtPtr =>
            {
                if (evtPtr == IntPtr.Zero) return true;
                var evt = (IADLXMultimediaChangedEvent*)evtPtr;
                var origin = evt->GetOrigin();
                var upsChanged = evt->IsVideoUpscaleChanged();
                var vsrChanged = evt->IsVideoSuperResolutionChanged();
                Console.WriteLine($"[Event] origin={origin}, upscaleChanged={upsChanged}, vsrChanged={vsrChanged}");
                return true; // keep listener active
            });

            Console.WriteLine("Registered multimedia change listener (no events expected in this sample run).");
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            Console.WriteLine("Multimedia change handling not supported; listener registration skipped.");
        }

        // Toggle example (commented to stay read-only)
        // using var upscaleNative = new ComPtr<IADLXVideoUpscale>(mmHelper.GetVideoUpscaleNative(gpu.As<IADLXGPU>()));
        // mmHelper.SetVideoUpscaleEnabled(upscaleNative.Get(), !upscale.IsEnabled);
        // using var vsrNative = new ComPtr<IADLXVideoSuperResolution>(mmHelper.GetVideoSuperResolutionNative(gpu.As<IADLXGPU>()));
        // mmHelper.SetVideoSuperResolutionEnabled(vsrNative.Get(), !vsr.IsEnabled);
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

