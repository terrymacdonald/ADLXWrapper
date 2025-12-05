using ADLXWrapper;
using static ADLXWrapper.ADLXDisplayHelpers;

Console.WriteLine("=== ADLX Display Event Sample (display-changed listener) ===");

using var adlx = ADLXApi.Initialize();
using var sys = adlx.GetSystemServicesHandle();
using var dispServices = ADLXDisplayHelpers.GetDisplayServicesHandle(sys);
using var handling = ADLXDisplayHelpers.GetDisplayChangedHandlingHandle(dispServices);

var listener = DisplaySettingsListenerHandle.Create(evt =>
{
    try
    {
        Console.WriteLine($"Display event: 0x{evt.ToInt64():X}");
        return true; // handled
    }
    catch
    {
        return false;
    }
});

try
{
    ADLXDisplayHelpers.AddDisplaySettingsChangedListener(handling, listener);
    Console.WriteLine("Listener registered. Press Enter to exit...");
    Console.ReadLine();
}
finally
{
    try { ADLXDisplayHelpers.RemoveDisplaySettingsChangedListener(handling, listener); } catch { }
    listener.Dispose();
}
