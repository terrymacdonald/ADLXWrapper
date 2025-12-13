using System.Linq;
using ADLXWrapper;

Console.WriteLine("=== ADLX Desktop/Eyefinity Sample ===");

using var adlx = ADLXApi.Initialize();
var sys = adlx.GetSystemServicesFacade();
var desktops = sys.EnumerateAllDesktops().ToList();
Console.WriteLine($"Found {desktops.Count} desktop(s)");

foreach (var desk in desktops)
{
    using (desk)
    {
        var profile = desk.GetProfile();
        Console.WriteLine($"Desktop {profile.Type} => {profile.Width}x{profile.Height} at ({profile.TopLeftX},{profile.TopLeftY}) displays={profile.Displays.Count}");
    }
}

Console.WriteLine("Eyefinity create/destroy is intentionally disabled in this sample to avoid reconfiguring user displays.");
