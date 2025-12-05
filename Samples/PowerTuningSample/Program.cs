using ADLXWrapper;

Console.WriteLine("=== ADLX Power Tuning Sample ===");

using var adlx = ADLXApi.Initialize();
using var sys = adlx.GetSystemServicesHandle();

// System2 is required for power tuning services on newer SDKs.
if (!ADLXHelpers.TryQueryInterface((IntPtr)sys, "IADLXSystem2", out var sys2Ptr) || sys2Ptr == IntPtr.Zero)
{
    Console.WriteLine("System2 not available; power tuning not supported on this driver/hardware.");
    return;
}

using var sys2 = AdlxInterfaceHandle.From(sys2Ptr);

try
{
    using var powerServices = ADLXPowerTuningHelpers.GetPowerTuningServices(sys2);
    Console.WriteLine("Power tuning services acquired.");

    // SmartShift Max
    using var ssm = ADLXPowerTuningHelpers.GetSmartShiftMax(powerServices);
    var (ssmSupported, ssmEnabled, ssmBias, _) = ADLXPowerTuningHelpers.GetSmartShiftMaxState(ssm);
    Console.WriteLine($"SmartShift Max -> supported={ssmSupported}, enabled={ssmEnabled}, biasMode={ssmBias}");

    // SmartShift Eco
    using var eco = ADLXPowerTuningHelpers.GetSmartShiftEco(powerServices);
    var (ecoSupported, ecoEnabled) = ADLXPowerTuningHelpers.GetSmartShiftEcoState(eco);
    Console.WriteLine($"SmartShift Eco -> supported={ecoSupported}, enabled={ecoEnabled}");
}
catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
{
    Console.WriteLine("Power tuning not supported on this hardware/driver.");
}
