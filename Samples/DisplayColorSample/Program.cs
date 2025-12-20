using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Color Sample (gamma/gamut/3DLUT) ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    using var displayHelper = new ADLXDisplayServicesHelper(sysHelper.GetDisplayServicesNative());
    var displays = displayHelper.EnumerateDisplayHandles();

    if (displays.Length == 0)
    {
        Console.WriteLine("No displays found.");
        return;
    }

    try
    {
        using var displayHandle = displays[0];
        using var display = displayHelper.CreateAdlxDisplay(displayHandle.As<IADLXDisplay>());
        Console.WriteLine($"Using display: {display.Name}");

        var gamma = display.GetGamma();
        Console.WriteLine($"Gamma supported: {gamma.IsSupported}");

        var gamut = display.GetGamut();
        Console.WriteLine($"Gamut supported: {gamut.IsGamutSupported}, custom white point supported: {gamut.IsWhitePointSupported}");

        var lut = display.GetThreeDLut();
        Console.WriteLine($"3DLUT: SCE={lut.IsSceSupported}, VividGaming={lut.IsSceVividGamingSupported}, DynamicContrast={lut.IsSceDynamicContrastSupported}, User3DLUT={lut.IsUser3DLutSupported}");
        // This sample is read-only; production apps should supply LUT data before calling setters.
    }
    finally
    {
        for (int i = 1; i < displays.Length; i++)
        {
            try { displays[i].Dispose(); } catch { }
        }
    }
}

