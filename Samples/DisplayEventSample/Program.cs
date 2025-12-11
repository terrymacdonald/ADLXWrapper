using System;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Event Sample (display-changed listener) ===");

    using var adlx = ADLXApi.Initialize();
    var sys = adlx.GetSystemServices();

    using var dispServices = AdlxInterfaceHandle.From(ADLXDisplayHelpers.GetDisplayServices(sys), addRef: false);

    IADLXDisplayChangedHandling* pHandling = null;
    dispServices.As<IADLXDisplayServices>()->GetDisplayChangedHandling(&pHandling);
    using var handling = AdlxInterfaceHandle.From(pHandling, addRef: false);

    var listener = DisplaySettingsListenerHandle.Create(evt =>
    {
        Console.WriteLine($"Display event: 0x{evt.ToInt64():X}");
        return true; // handled
    });

    try
    {
        handling.As<IADLXDisplayChangedHandling>()->AddDisplaySettingsEventListener((IADLXDisplaySettingsChangedListener*)listener.DangerousGetHandle());
        Console.WriteLine("Listener registered. Press Enter to exit...");
        Console.ReadLine();
    }
    finally
    {
        try { handling.As<IADLXDisplayChangedHandling>()->RemoveDisplaySettingsEventListener((IADLXDisplaySettingsChangedListener*)listener.DangerousGetHandle()); } catch { }
        listener.Dispose();
    }
}
