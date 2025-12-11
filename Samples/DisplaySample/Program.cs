using System.Collections.Generic;
using System.Linq;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Sample ===");

    using var adlx = ADLXApi.Initialize();
    var sys = adlx.GetSystemServices();
    var displays = ADLXDisplayHelpers.EnumerateAllDisplays(sys);
    var displayList = displays?.ToList() ?? new List<DisplayInfo>();
    Console.WriteLine($"Found {displayList.Count} displays");

    foreach (var info in displayList)
    {
        Console.WriteLine($"Name: {info.Name} | {info.Width}x{info.Height} @ {info.RefreshRate}Hz");
    }
}
