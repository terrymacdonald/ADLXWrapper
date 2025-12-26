using System;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Event Sample (display-changed listener) ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = adlx.GetSystemServices();
    using var displayHelper = sysHelper.GetDisplayServices();

    using var listener = displayHelper.AddDisplaySettingsEventListener(evt =>
    {
        Console.WriteLine("Display settings changed");
        return true; // keep listening
    });

    Console.WriteLine("Listener registered. Press Enter to exit...");
    Console.ReadLine();
}

