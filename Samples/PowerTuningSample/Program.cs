using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Power Tuning Sample ===");

    using var adlx = ADLXApi.Initialize();
    var sys = adlx.GetSystemServices();

    // System2 is required for power tuning services on newer SDKs.
    if (!ADLXHelpers.TryQueryInterface((IntPtr)sys, "IADLXSystem2", out var sys2Ptr) || sys2Ptr == IntPtr.Zero)
    {
        Console.WriteLine("System2 not available; power tuning not supported on this driver/hardware.");
        return;
    }

    using var sys2 = AdlxInterfaceHandle.From((void*)sys2Ptr);

    try
    {
        using var powerServices = AdlxInterfaceHandle.From(ADLXPowerTuningHelpers.GetPowerTuningServices(sys2.As<IADLXSystem>()), addRef: false);
        Console.WriteLine("Power tuning services acquired.");

        var ssm = ADLXPowerTuningHelpers.GetSmartShiftMax(powerServices.As<IADLXPowerTuningServices>());
        Console.WriteLine($"SmartShift Max -> supported={ssm.IsSupported}, biasMode={ssm.BiasMode}, biasValue={ssm.BiasValue}, range=({ssm.BiasRange.minValue}-{ssm.BiasRange.maxValue})");

        var eco = ADLXPowerTuningHelpers.GetSmartShiftEco(powerServices.As<IADLXPowerTuningServices>());
        Console.WriteLine($"SmartShift Eco -> supported={eco.IsSupported}, enabled={eco.IsEnabled}");
    }
    catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
    {
        Console.WriteLine("Power tuning not supported on this hardware/driver.");
    }
}
