using System.Linq;
using ADLXWrapper;

Console.WriteLine("=== ADLX Display Sample ===");

using var adlx = ADLXApi.Initialize();
var sys = adlx.GetSystemServicesFacade();
var displays = sys.EnumerateAllDisplays();
Console.WriteLine($"Found {displays.Count} display(s)");

foreach (var display in displays)
{
    using (display)
    {
        Console.WriteLine($"Name: {display.Name} | {display.Width}x{display.Height} @ {display.RefreshRate:F2}Hz");
    }
}
