using System.Collections.Generic;
using System.Linq;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Sample ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = adlx.GetSystemServices();
    var displays = sysHelper.EnumerateDisplays();
    var displayList = displays?.ToList() ?? new List<AdlxDisplay>();
    Console.WriteLine($"Found {displayList.Count} display(s)");

    foreach (var display in displayList)
    {
        using (display)
        {
            Console.WriteLine($"Name: {display.Name} | {display.Width}x{display.Height} @ {display.RefreshRate}Hz");
        }
    }
}

