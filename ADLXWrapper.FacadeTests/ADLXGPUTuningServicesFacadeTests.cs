using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public class ADLXGPUTuningServicesFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLXGPUTuningServicesFacadeTests(FacadeSessionFixture fixture)
    {
        _fixture = fixture;
    }

    private void SkipIfUnavailable()
    {
        Skip.If(!string.IsNullOrWhiteSpace(_fixture.SkipReason), _fixture.SkipReason);
    }

    private ADLXGPUTuningServicesHelper GetTuningHelperOrSkip()
    {
        SkipIfUnavailable();
        var helper = _fixture.System!.GetGPUTuningServices();
        Skip.If(helper == null, "GPU tuning services not supported on this hardware/driver.");
        return helper!;
    }

    [SkippableFact]
    public void Gpu_tuning_capabilities_facade()
    {
        using var tuning = GetTuningHelperOrSkip();

        var gpus = _fixture.System!.EnumerateGPUs().ToList();
        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        var gpuUniqueId = gpus[0].UniqueId;

        var caps = tuning.GetCapabilities(gpuUniqueId);
        Assert.IsType<bool>(caps.AutoTuningSupported);
        Assert.IsType<bool>(caps.PresetTuningSupported);
        Assert.IsType<bool>(caps.ManualGFXTuningSupported);
        Assert.IsType<bool>(caps.ManualVRAMTuningSupported);
        Assert.IsType<bool>(caps.ManualFanTuningSupported);
        Assert.IsType<bool>(caps.ManualPowerTuningSupported);
    }

    [SkippableFact]
    public void Gpu_tuning_preset_info_facade()
    {
        using var tuning = GetTuningHelperOrSkip();

        var gpus = _fixture.System!.EnumerateGPUs().ToList();
        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        var gpuUniqueId = gpus[0].UniqueId;

        if (!tuning.TryGetPresetTuning(gpuUniqueId, out var info))
            throw new Xunit.SkipException("Preset tuning not supported on this GPU.");

        Skip.If(!info.IsSupported || info.SupportedPresets.Count == 0, "Preset tuning reported unsupported or returned no presets.");
        Assert.True(info.SupportedPresets.Count > 0);
    }

    [SkippableFact]
    public void Gpu_tuning_manual_fan_info_facade()
    {
        using var tuning = GetTuningHelperOrSkip();

        var gpus = _fixture.System!.EnumerateGPUs().ToList();
        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        var gpuUniqueId = gpus[0].UniqueId;

        if (!tuning.TryGetManualFanTuning(gpuUniqueId, out var info))
            throw new Xunit.SkipException("Manual fan tuning not supported on this GPU.");

        Skip.If(!info.IsSupported, "Manual fan tuning reported unsupported.");
        if (info.FanPoints != null)
        {
            Assert.IsAssignableFrom<IReadOnlyList<FanPoint>>(info.FanPoints);
        }
    }

    [SkippableFact]
    public void Gpu_tuning_manual_vram_info_facade()
    {
        using var tuning = GetTuningHelperOrSkip();

        var gpus = _fixture.System!.EnumerateGPUs().ToList();
        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        var gpuUniqueId = gpus[0].UniqueId;

        if (!tuning.TryGetManualVramTuning(gpuUniqueId, out var info))
            throw new Xunit.SkipException("Manual VRAM tuning not supported on this GPU.");

        Skip.If(!info.IsSupported, "Manual VRAM tuning reported unsupported.");
    }

    [SkippableFact]
    public void Gpu_tuning_manual_gfx_info_facade()
    {
        using var tuning = GetTuningHelperOrSkip();

        var gpus = _fixture.System!.EnumerateGPUs().ToList();
        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        var gpuUniqueId = gpus[0].UniqueId;

        if (!tuning.TryGetManualGfxTuning(gpuUniqueId, out var info))
            throw new Xunit.SkipException("Manual GFX tuning not supported on this GPU.");

        Skip.If(!info.IsSupported, "Manual GFX tuning reported unsupported.");
    }
}
