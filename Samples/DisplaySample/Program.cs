using System.Collections.Generic;
using System.Linq;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Sample ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    using var displayHelper = new ADLXDisplayServicesHelper(sysHelper.GetDisplayServicesNative());
    var displays = displayHelper.EnumerateDisplays();
    var displayList = displays?.ToList() ?? new List<DisplayInfo>();
    Console.WriteLine($"Found {displayList.Count} displays");

    foreach (var info in displayList)
    {
        Console.WriteLine($"Name: {info.Name} | {info.Width}x{info.Height} @ {info.RefreshRate}Hz");
    }
}

