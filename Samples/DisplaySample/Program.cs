using ADLXWrapper;

Console.WriteLine("=== ADLX Display Sample ===");

using var adlx = ADLXApi.Initialize();
using var sys = adlx.GetSystemServicesHandle();

var displays = ADLXDisplayHelpers.EnumerateAllDisplayHandles(sys);
Console.WriteLine($"Found {displays.Length} displays");

foreach (var display in displays)
{
    using (display)
    {
        var info = ADLXDisplayInfo.GetBasicInfo(display);
        Console.WriteLine($"Name: {info.Name} | {info.Width}x{info.Height} @ {info.RefreshRate}Hz");
    }
}

foreach (var display in displays)
{
    try { display.Dispose(); } catch { }
}
