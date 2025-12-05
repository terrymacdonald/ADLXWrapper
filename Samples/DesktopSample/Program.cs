using ADLXWrapper;

Console.WriteLine("=== ADLX Desktop/Eyefinity Sample ===");

using var adlx = ADLXApi.Initialize();
using var sys = adlx.GetSystemServicesHandle();
using var desktops = ADLXDesktopHelpers.GetDesktopServicesHandle(sys);

var desktopHandles = ADLXDesktopHelpers.EnumerateAllDesktopHandles(desktops);
Console.WriteLine($"Found {desktopHandles.Length} desktops");

foreach (var desk in desktopHandles)
{
    using (desk)
    {
        var type = ADLXDesktopHelpers.GetDesktopType(desk);
        var size = ADLXDesktopHelpers.GetDesktopSize(desk);
        Console.WriteLine($"Desktop {type} => {size.Width}x{size.Height}");
    }
}

// Guard System2 for any create/destroy operations
if (!ADLXHelpers.TryQueryInterface((IntPtr)sys, "IADLXSystem2", out var sys2Ptr) || sys2Ptr == IntPtr.Zero)
{
    Console.WriteLine("System2 not available; skipping Eyefinity creation/destruction.");
    return;
}

Console.WriteLine("System2 available. Eyefinity create/destroy helpers are accessible, but disabled in this sample by default.");
Console.WriteLine("Uncomment the block below to experiment (changes user display configuration!).");

/*
using var sys2 = AdlxInterfaceHandle.From(sys2Ptr);
using var dsk2 = ADLXDesktopHelpers.GetDesktopServicesHandle(sys2);
// Example: create/destroy a simple Eyefinity desktop
// using var simple = ADLXDesktopHelpers.GetSimpleEyefinityHandle(dsk2);
// ADLXDesktopHelpers.CreateEyefinityDesktop(simple, desiredDisplaysArray);
// ADLXDesktopHelpers.DestroyEyefinityDesktop(simple);
*/

foreach (var handle in desktopHandles)
{
    try { handle.Dispose(); } catch { }
}
