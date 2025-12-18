using System.Collections.Generic;
using System.Linq;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Sample ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    var displays = ADLXDisplayHelpers.EnumerateAllDisplays(sysHelper.GetSystemServicesNative());
    var displayList = displays?.ToList() ?? new List<DisplayInfo>();
    Console.WriteLine($"Found {displayList.Count} displays");

    foreach (var info in displayList)
    {
        Console.WriteLine($"Name: {info.Name} | {info.Width}x{info.Height} @ {info.RefreshRate}Hz");
    }
}
