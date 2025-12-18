using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Color Sample (gamma/gamut/3DLUT) ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    var sys = sysHelper.GetSystemServicesNative();
    var displays = ADLXDisplayHelpers.EnumerateAllDisplayHandles(sys);

    if (displays.Length == 0)
    {
        Console.WriteLine("No displays found.");
        return;
    }

    var display = displays[0];
    using (display)
    {
        Console.WriteLine($"Using display: {ADLXDisplayHelpers.GetDisplayName(display)}");
        using var dispServices = AdlxInterfaceHandle.From(ADLXDisplayHelpers.GetDisplayServices(sys), addRef: false);

        var gamma = ADLXDisplaySettingsHelpers.GetGamma(dispServices.As<IADLXDisplayServices>(), display.As<IADLXDisplay>());
        Console.WriteLine($"Gamma supported: {gamma.IsSupported}");

        var gamut = ADLXDisplaySettingsHelpers.GetGamut(dispServices.As<IADLXDisplayServices>(), display.As<IADLXDisplay>());
        Console.WriteLine($"Gamut supported: {gamut.IsGamutSupported}, custom white point supported: {gamut.IsWhitePointSupported}");

        var lut = ADLXDisplaySettingsHelpers.Get3DLUT(dispServices.As<IADLXDisplayServices>(), display.As<IADLXDisplay>());
        Console.WriteLine($"3DLUT: SCE={lut.IsSceSupported}, VividGaming={lut.IsSceVividGamingSupported}, DynamicContrast={lut.IsSceDynamicContrastSupported}, User3DLUT={lut.IsUser3DLutSupported}");
        // This sample is read-only; production apps should supply LUT data before calling setters.
    }

    foreach (var d in displays)
    {
        try { d.Dispose(); } catch { }
    }
}
