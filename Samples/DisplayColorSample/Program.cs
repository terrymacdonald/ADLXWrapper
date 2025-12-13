using System.Linq;
using ADLXWrapper;

Console.WriteLine("=== ADLX Display Color Sample (gamma/gamut/3DLUT) ===");

using var adlx = ADLXApi.Initialize();
var sys = adlx.GetSystemServicesProfile();
var display = sys.EnumerateAllDisplays().FirstOrDefault();

if (display == null)
{
    Console.WriteLine("No displays found.");
    return;
}

using (display)
{
    Console.WriteLine($"Using display: {display.Name}");

    var gamma = display.GetGamma();
    Console.WriteLine($"Gamma supported: {gamma.Supported} (preset: {gamma.Preset})");

    var gamut = display.GetGamut();
    Console.WriteLine($"Gamut supported: {gamut.Supported}, space: {gamut.ColorSpace?.ToString() ?? "<none>"}, white point: {gamut.WhitePoint?.ToString() ?? "<none>"}");

    var lut = display.GetThreeDLUT();
    Console.WriteLine($"3DLUT: mode={lut.Mode}, dynamic contrast supported={lut.DynamicContrastSupported}");
    Console.WriteLine("(This sample is read-only; supply LUT data before setting in production.)");
}
