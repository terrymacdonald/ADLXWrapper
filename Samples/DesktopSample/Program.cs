using System.Linq;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Desktop/Eyefinity Sample ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    var sys = sysHelper.GetSystemServicesNative();
    var desktops = ADLXDesktopHelpers.EnumerateAllDesktops(sys)?.ToList() ?? new List<DesktopInfo>();
    Console.WriteLine($"Found {desktops.Count} desktops");

    foreach (var desk in desktops)
    {
        Console.WriteLine($"Desktop {desk.Type} => {desk.Width}x{desk.Height}");
    }

    // Guard System2 for any create/destroy operations
    if (!ADLXUtils.TryQueryInterface((IntPtr)sys, "IADLXSystem2", out var sys2Ptr) || sys2Ptr == IntPtr.Zero)
    {
        Console.WriteLine("System2 not available; skipping Eyefinity creation/destruction.");
        return;
    }

    using var sys2 = AdlxInterfaceHandle.From((void*)sys2Ptr);
    Console.WriteLine("System2 available. Eyefinity create/destroy helpers are accessible, but disabled in this sample by default.");
    Console.WriteLine("Uncomment the block below to experiment (changes user display configuration!).");

    /*
    using var dsk2 = AdlxInterfaceHandle.From(ADLXDesktopHelpers.GetDesktopServices(sys2.As<IADLXSystem>()), addRef: false);
    // Example: create/destroy a simple Eyefinity desktop
    // var simple = ADLXDesktopHelpers.GetSimpleEyefinity(dsk2.As<IADLXDesktopServices>());
    // ADLXDesktopHelpers.CreateEyefinityDesktop(simpleHandle);
    // ADLXDesktopHelpers.DestroyEyefinityDesktop(simpleHandle);
    */
}

