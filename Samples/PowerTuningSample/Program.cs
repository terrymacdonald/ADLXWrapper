using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Power Tuning Sample ===");

    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    var sys = sysHelper.GetSystemServicesNative();

    // System2 is required for power tuning services on newer SDKs.
    if (!ADLXUtils.TryQueryInterface((IntPtr)sys, "IADLXSystem2", out var sys2Ptr) || sys2Ptr == IntPtr.Zero)
    {
        Console.WriteLine("System2 not available; power tuning not supported on this driver/hardware.");
        return;
    }

    try
    {
        // Acquire power tuning services via helper (System2 required)
        using var powerServicesHelper = new ADLXPowerTuningServicesHelper(sysHelper.GetPowerTuningServicesNative());
        Console.WriteLine("Power tuning services acquired.");

        var ssm = ADLXPowerTuningHelpers.GetSmartShiftMax(powerServicesHelper.GetPowerTuningServicesNative());
        Console.WriteLine($"SmartShift Max -> supported={ssm.IsSupported}, biasMode={ssm.BiasMode}, biasValue={ssm.BiasValue}, range=({ssm.BiasRange.minValue}-{ssm.BiasRange.maxValue})");

        var eco = ADLXPowerTuningHelpers.GetSmartShiftEco(powerServicesHelper.GetPowerTuningServicesNative());
        Console.WriteLine($"SmartShift Eco -> supported={eco.IsSupported}, enabled={eco.IsEnabled}");
    }
    catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
    {
        Console.WriteLine("Power tuning not supported on this hardware/driver.");
    }
}

