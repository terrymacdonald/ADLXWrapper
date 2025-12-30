using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public class ADLXPowerTuningServicesFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLXPowerTuningServicesFacadeTests(FacadeSessionFixture fixture)
    {
        _fixture = fixture;
    }

    private void SkipIfUnavailable()
    {
        Skip.If(!string.IsNullOrWhiteSpace(_fixture.SkipReason), _fixture.SkipReason);
    }

    private ADLXPowerTuningServicesHelper GetPowerOrSkip()
    {
        SkipIfUnavailable();
        try
        {
            return _fixture.System!.GetPowerTuningServices();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Power tuning services not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void Power_tuning_gpu_connect_facade()
    {
        using var power = GetPowerOrSkip();
        if (!power.TryIsGPUConnectSupported(out var supported))
            throw new Xunit.SkipException("GPUConnect support query failed.");

        if (!supported)
            return;

        if (!power.TryEnumerateGPUConnectGpuHandles(out var handles))
            throw new Xunit.SkipException("GPUConnect handle enumeration failed.");

        Skip.If(handles.Length == 0, "GPUConnect reported supported but returned no GPU handles.");
        foreach (var handle in handles) handle.Dispose();
    }

    [SkippableFact]
    public void Power_tuning_smart_shift_max_facade()
    {
        using var power = GetPowerOrSkip();
        SmartShiftMaxInfo info;
        try
        {
            info = power.GetSmartShiftMax();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("SmartShift Max not supported on this hardware/driver.");
        }

        Skip.If(!info.IsSupported, "SmartShift Max reported unsupported.");
        Assert.True(info.BiasRange.minValue <= info.BiasValue && info.BiasValue <= info.BiasRange.maxValue);
    }

    [SkippableFact]
    public void Power_tuning_smart_shift_eco_facade()
    {
        using var power = GetPowerOrSkip();
        SmartShiftEcoInfo info;
        try
        {
            info = power.GetSmartShiftEco();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("SmartShift Eco not supported on this hardware/driver.");
        }

        Skip.If(!info.IsSupported, "SmartShift Eco reported unsupported.");
        Assert.IsType<bool>(info.IsEnabled);
    }

    [SkippableFact]
    public void Power_tuning_manual_power_info_facade()
    {
        using var power = GetPowerOrSkip();
        using var tuning = _fixture.System!.GetGPUTuningServices();

        unsafe
        {
            var gpuHandles = _fixture.System.EnumerateGPUsHandle();
            Skip.If(gpuHandles.Length == 0, "No GPUs returned by ADLX.");
            using var gpuHandle = gpuHandles[0];

            if (!power.TryGetManualPowerTuning(tuning, gpuHandle, out var info))
                throw new Xunit.SkipException("Manual power tuning not supported on this GPU.");

            Skip.If(!info.PowerLimitSupported && !info.TdcLimitSupported, "Manual power tuning reported unsupported.");

            if (info.PowerLimitSupported)
            {
                Assert.True(info.PowerLimitRange.minValue <= info.PowerLimitValue && info.PowerLimitValue <= info.PowerLimitRange.maxValue);
            }

            if (info.TdcLimitSupported)
            {
                Assert.True(info.TdcLimitRange.minValue <= info.TdcLimitValue && info.TdcLimitValue <= info.TdcLimitRange.maxValue);
            }
        }
    }
}
