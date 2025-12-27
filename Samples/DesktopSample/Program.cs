using System.Linq;
using System.Collections.Generic;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Desktop/Eyefinity Sample ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    var desktopServices = sysHelper.GetDesktopServicesNative();
    using var desktopHelper = new ADLXDesktopServicesHelper(desktopServices);
    var desktops = desktopHelper.EnumerateDesktops()?.ToList() ?? new List<DesktopInfo>();
    Console.WriteLine($"Found {desktops.Count} desktops");

    foreach (var desk in desktops)
    {
        Console.WriteLine($"Desktop {desk.Type} => {desk.Width}x{desk.Height}");
    }

    // Guard System2 for any create/destroy operations
    if (!ADLXUtils.TryQueryInterface((IntPtr)sysHelper.GetSystemServicesNative(), "IADLXSystem2", out var sys2Ptr) || sys2Ptr == IntPtr.Zero)
    {
        Console.WriteLine("System2 not available; skipping Eyefinity creation/destruction.");
        return;
    }

    using var sys2 = ADLXInterfaceHandle.From((void*)sys2Ptr);
    Console.WriteLine("System2 available. Eyefinity create/destroy helpers are accessible, but disabled in this sample by default.");
    Console.WriteLine("Uncomment the block below to experiment (changes user display configuration!).");

    /*
    using var dsk2 = ADLXInterfaceHandle.From(((IADLXSystem2*)sys2.As<IADLXSystem2>())->GetDesktopsServices(&var ds) == ADLX_RESULT.ADLX_OK ? ds : null, addRef: false);
    using var desktopHelper2 = new ADLXDesktopServicesHelper(dsk2.As<IADLXDesktopServices>());
    // Example: create/destroy a simple Eyefinity desktop
    // var simple = desktopHelper2.GetSimpleEyefinity();
    // var eyefinity = desktopHelper2.CreateEyefinityDesktop(simple);
    // desktopHelper2.DestroyAllEyefinityDesktops();
    */
}

