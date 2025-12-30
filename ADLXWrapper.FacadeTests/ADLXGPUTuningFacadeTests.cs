using System.Collections.Generic;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public class ADLXGPUTuningFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLXGPUTuningFacadeTests(FacadeSessionFixture fixture)
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
        try
        {
            return _fixture.System!.GetGPUTuningServices();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("GPU tuning services not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void Gpu_tuning_capabilities_facade()
    {
        using var tuning = GetTuningHelperOrSkip();

        unsafe
        {
            var handles = _fixture.System!.EnumerateGPUsHandle();
            Skip.If(handles.Length == 0, "No GPUs returned by ADLX.");
            using var gpuHandle = handles[0];

            var caps = tuning.GetCapabilities(gpuHandle.As<IADLXGPU>());
            Assert.IsType<bool>(caps.AutoTuningSupported);
            Assert.IsType<bool>(caps.PresetTuningSupported);
            Assert.IsType<bool>(caps.ManualGFXTuningSupported);
            Assert.IsType<bool>(caps.ManualVRAMTuningSupported);
            Assert.IsType<bool>(caps.ManualFanTuningSupported);
            Assert.IsType<bool>(caps.ManualPowerTuningSupported);
        }
    }

    [SkippableFact]
    public void Gpu_tuning_preset_info_facade()
    {
        using var tuning = GetTuningHelperOrSkip();

        unsafe
        {
            var handles = _fixture.System!.EnumerateGPUsHandle();
            Skip.If(handles.Length == 0, "No GPUs returned by ADLX.");
            using var gpuHandle = handles[0];

            if (!tuning.TryGetPresetTuning(gpuHandle.As<IADLXGPU>(), out var info))
                throw new Xunit.SkipException("Preset tuning not supported on this GPU.");

            Assert.True(info.SupportedPresets is { Count: > 0 });
        }
    }

    [SkippableFact]
    public void Gpu_tuning_manual_fan_info_facade()
    {
        using var tuning = GetTuningHelperOrSkip();

        unsafe
        {
            var handles = _fixture.System!.EnumerateGPUsHandle();
            Skip.If(handles.Length == 0, "No GPUs returned by ADLX.");
            using var gpuHandle = handles[0];

            if (!tuning.TryGetManualFanTuning(gpuHandle.As<IADLXGPU>(), out var info))
                throw new Xunit.SkipException("Manual fan tuning not supported on this GPU.");

            Assert.True(info.IsSupported);
            if (info.FanPoints != null)
            {
                Assert.IsAssignableFrom<IReadOnlyList<FanPoint>>(info.FanPoints);
            }
        }
    }

    [SkippableFact]
    public void Gpu_tuning_manual_vram_info_facade()
    {
        using var tuning = GetTuningHelperOrSkip();

        unsafe
        {
            var handles = _fixture.System!.EnumerateGPUsHandle();
            Skip.If(handles.Length == 0, "No GPUs returned by ADLX.");
            using var gpuHandle = handles[0];

            if (!tuning.TryGetManualVramTuning(gpuHandle.As<IADLXGPU>(), out var info))
                throw new Xunit.SkipException("Manual VRAM tuning not supported on this GPU.");

            Assert.True(info.IsSupported);
        }
    }

    [SkippableFact]
    public void Gpu_tuning_manual_gfx_info_facade()
    {
        using var tuning = GetTuningHelperOrSkip();

        unsafe
        {
            var handles = _fixture.System!.EnumerateGPUsHandle();
            Skip.If(handles.Length == 0, "No GPUs returned by ADLX.");
            using var gpuHandle = handles[0];

            if (!tuning.TryGetManualGfxTuning(gpuHandle.As<IADLXGPU>(), out var info))
                throw new Xunit.SkipException("Manual GFX tuning not supported on this GPU.");

            Assert.True(info.IsSupported);
        }
    }
}
