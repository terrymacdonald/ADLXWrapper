using ADLXWrapper;

Console.WriteLine("=== ADLX Display Color Sample (gamma/gamut/3DLUT) ===");

using var adlx = ADLXApi.Initialize();
using var sys = adlx.GetSystemServicesHandle();
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

    using var dispServices = ADLXDisplayHelpers.GetDisplayServicesHandle(sys);

    // Gamma read/reapply current
    using var gamma = ADLXDisplaySettingsHelpers.GetGammaHandle(dispServices, display);
    var gammaState = ADLXDisplaySettingsHelpers.GetGammaState(gamma);
    Console.WriteLine($"Gamma state: ReGammaRamp={gammaState.reGammaRamp}, DeGammaRamp={gammaState.deGammaRamp}, ReGammaCoeff={gammaState.reGammaCoeff}");

    // Gamut read
    using var gamut = ADLXDisplaySettingsHelpers.GetGamutHandle(dispServices, display);
    var gamutState = ADLXDisplaySettingsHelpers.GetGamutState(gamut);
    Console.WriteLine($"Gamut space: {gamutState.gamut}, white points: 5000K={gamutState.whitePoint5000K}, 6500K={gamutState.whitePoint6500K}, 7500K={gamutState.whitePoint7500K}, 9300K={gamutState.whitePoint9300K}");

    // 3DLUT presence (read-only)
    using var lut = ADLXDisplaySettingsHelpers.Get3DLUTHandle(dispServices, display);
    var lutState = ADLXDisplaySettingsHelpers.Get3DLUTState(lut);
    Console.WriteLine($"3DLUT: SCE supported={lutState.sceSupported}, VividGaming supported={lutState.vividGamingSupported}");
    // This sample is read-only; production apps should supply LUT data before calling setters.
}

foreach (var d in displays)
{
    try { d.Dispose(); } catch { }
}
