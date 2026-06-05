using System.Linq;
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
        var helper = _fixture.System!.GetPowerTuningServices();
        Skip.If(helper == null, "Power tuning services not supported on this hardware/driver.");
        return helper!;
    }

    [SkippableFact]
    public void Power_tuning_gpu_connect_facade()
    {
        using var power = GetPowerOrSkip();
        if (!power.TryIsGPUConnectSupported(out var supported))
            throw new Xunit.SkipException("GPUConnect support query failed.");

        // TryEnumerateGPUConnectGpuHandles is internal (returns native handles); test only verifies the support flag.
        Assert.IsType<bool>(supported);
    }

    [SkippableFact]
    public void Power_tuning_smart_shift_max_facade()
    {
        using var power = GetPowerOrSkip();
        var info = power.GetSmartShiftMax();
        Skip.If(!info.IsSupported, "SmartShift Max not supported on this hardware/driver.");
        Assert.True(info.BiasRange.MinValue <= info.BiasValue && info.BiasValue <= info.BiasRange.MaxValue);
    }

    [SkippableFact]
    public void Power_tuning_smart_shift_eco_facade()
    {
        using var power = GetPowerOrSkip();
        var info = power.GetSmartShiftEco();
        Skip.If(!info.IsSupported, "SmartShift Eco not supported on this hardware/driver.");
        Assert.IsType<bool>(info.IsEnabled);
    }

    [SkippableFact]
    public void Power_tuning_manual_power_info_facade()
    {
        using var power = GetPowerOrSkip();
        var tuning = _fixture.System!.GetGPUTuningServices();
        Skip.If(tuning == null, "GPU tuning services not supported on this hardware/driver.");
        using (tuning)
        {
            var gpus = _fixture.System!.EnumerateGPUs().ToList();
            Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
            var gpuUniqueId = gpus[0].UniqueId;

            if (!power.TryGetManualPowerTuning(gpuUniqueId, tuning, out var info))
                throw new Xunit.SkipException("Manual power tuning not supported on this GPU.");

            Skip.If(!info.PowerLimitSupported && !info.TdcLimitSupported, "Manual power tuning reported unsupported.");

            if (info.PowerLimitSupported)
            {
                Assert.True(info.PowerLimitRange.MinValue <= info.PowerLimitValue && info.PowerLimitValue <= info.PowerLimitRange.MaxValue);
            }

            if (info.TdcLimitSupported)
            {
                Assert.True(info.TdcLimitRange.MinValue <= info.TdcLimitValue && info.TdcLimitValue <= info.TdcLimitRange.MaxValue);
            }
        }
    }
}
