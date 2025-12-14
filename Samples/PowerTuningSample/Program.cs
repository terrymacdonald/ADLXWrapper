using ADLXWrapper;

Console.WriteLine("=== ADLX Power Tuning Sample ===");

using var adlx = ADLXApi.Initialize();

try
{
    using var sys = adlx.GetSystemServicesProfile();
    using var power = sys.GetPowerTuningServices();

    var profile = power.GetProfile();

    if (profile.SmartShiftMax.HasValue)
    {
        var ssm = profile.SmartShiftMax.Value;
        Console.WriteLine($"SmartShift Max -> supported={ssm.IsSupported}, biasMode={ssm.BiasMode}, biasValue={ssm.BiasValue}, range=({ssm.BiasRange.minValue}-{ssm.BiasRange.maxValue})");
    }
    else
    {
        Console.WriteLine("SmartShift Max not supported.");
    }

    if (profile.SmartShiftEco.HasValue)
    {
        var eco = profile.SmartShiftEco.Value;
        Console.WriteLine($"SmartShift Eco -> supported={eco.IsSupported}, enabled={eco.IsEnabled}");
    }
    else
    {
        Console.WriteLine("SmartShift Eco not supported.");
    }

    // Example apply (commented to remain read-only):
    // if (profile.SmartShiftMax?.IsSupported == true)
    // {
    //     var newMax = profile.SmartShiftMax.Value;
    //     power.ApplyProfile(new PowerTuningProfile(newMax with { BiasValue = newMax.BiasValue }, profile.SmartShiftEco));
    // }
}
catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
{
    Console.WriteLine("Power tuning not supported on this hardware/driver.");
}
