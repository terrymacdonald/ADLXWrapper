using System;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Event Sample (display-changed listener) ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    using var displayHelper = new ADLXDisplayServicesHelper(sysHelper.GetDisplayServicesNative());
    using var handling = displayHelper.GetDisplayChangedHandling();

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

