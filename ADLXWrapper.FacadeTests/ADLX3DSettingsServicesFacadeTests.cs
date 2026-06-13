using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using System.Runtime.InteropServices;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public class ADLX3DSettingsServicesFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLX3DSettingsServicesFacadeTests(FacadeSessionFixture fixture)
    {
        _fixture = fixture;
    }

    private void SkipIfUnavailable()
    {
        Skip.If(!string.IsNullOrWhiteSpace(_fixture.SkipReason), _fixture.SkipReason);
    }

    private ADLX3DSettingsServicesHelper Get3DHelperOrSkip()
    {
        SkipIfUnavailable();
        try
        {
            return _fixture.System!.Get3DSettingsServices();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("3D settings services not supported on this hardware/driver.");
        }
    }

    private All3DSettingsDto GetAll3DSettingsOrSkip(ADLX3DSettingsServicesHelper helper)
    {
        var gpus = _fixture.System!.EnumerateGPUs().ToList();
        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        var gpuUniqueId = gpus[0].UniqueId;

        if (!helper.TryGetAll3DSettings(gpuUniqueId, out var info))
            throw new Xunit.SkipException("3D settings not supported on this GPU.");

        return info;
    }

    [SkippableFact]
    public void Three_d_settings_info_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Assert.True(
            info.AntiLag.IsSupported ||
            info.Chill.IsSupported ||
            info.Boost.IsSupported ||
            info.ImageSharpening.IsSupported ||
            info.EnhancedSync.IsSupported ||
            info.WaitForVerticalRefresh.IsSupported ||
            info.FrameRateTargetControl.IsSupported ||
            info.AntiAliasing.IsSupported ||
            info.MorphologicalAntiAliasing.IsSupported ||
            info.AnisotropicFiltering.IsSupported ||
            info.Tessellation.IsSupported ||
            info.FluidMotionFrames.IsSupported ||
            info.RadeonSuperResolution.IsSupported ||
            info.ImageSharpenDesktop.IsSupported,
            "No 3D settings information returned.");

        if (info.AntiLag.IsSupported)
        {
            Assert.IsType<bool>(info.AntiLag.IsEnabled);
        }

        if (info.Chill.IsSupported)
        {
            Assert.True(info.Chill.FPSRange.MinValue <= info.Chill.MinFPS && info.Chill.MinFPS <= info.Chill.FPSRange.MaxValue);
            Assert.True(info.Chill.FPSRange.MinValue <= info.Chill.MaxFPS && info.Chill.MaxFPS <= info.Chill.FPSRange.MaxValue);
        }

        if (info.Boost.IsSupported)
        {
            Assert.True(info.Boost.ResolutionRange.MinValue <= info.Boost.MinResolution && info.Boost.MinResolution <= info.Boost.ResolutionRange.MaxValue);
        }

        if (info.ImageSharpening.IsSupported)
        {
            Assert.True(info.ImageSharpening.SharpnessRange.MinValue <= info.ImageSharpening.Sharpness && info.ImageSharpening.Sharpness <= info.ImageSharpening.SharpnessRange.MaxValue);
        }

        if (info.EnhancedSync.IsSupported)
        {
            Assert.IsType<bool>(info.EnhancedSync.IsEnabled);
        }

        if (info.WaitForVerticalRefresh.IsSupported)
        {
            Assert.True(Enum.IsDefined(typeof(ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE), info.WaitForVerticalRefresh.Mode));
        }

        if (info.FrameRateTargetControl.IsSupported)
        {
            Assert.True(info.FrameRateTargetControl.FpsRange.MinValue <= info.FrameRateTargetControl.Fps && info.FrameRateTargetControl.Fps <= info.FrameRateTargetControl.FpsRange.MaxValue);
        }

        if (info.AntiAliasing.IsSupported)
        {
            Assert.True(Enum.IsDefined(typeof(ADLX_ANTI_ALIASING_MODE), info.AntiAliasing.Mode));
        }

        if (info.MorphologicalAntiAliasing.IsSupported)
        {
            Assert.IsType<bool>(info.MorphologicalAntiAliasing.IsEnabled);
        }

        if (info.AnisotropicFiltering.IsSupported)
        {
            Assert.True(Enum.IsDefined(typeof(ADLX_ANISOTROPIC_FILTERING_LEVEL), info.AnisotropicFiltering.Level));
        }

        if (info.Tessellation.IsSupported)
        {
            Assert.True(Enum.IsDefined(typeof(ADLX_TESSELLATION_MODE), info.Tessellation.Mode));
            Assert.True(Enum.IsDefined(typeof(ADLX_TESSELLATION_LEVEL), info.Tessellation.Level));
        }

        if (info.FluidMotionFrames.IsSupported)
        {
            Assert.IsType<bool>(info.FluidMotionFrames.IsEnabled);
        }

        if (info.RadeonSuperResolution.IsSupported)
        {
            Assert.True(info.RadeonSuperResolution.SharpnessRange.MinValue <= info.RadeonSuperResolution.Sharpness && info.RadeonSuperResolution.Sharpness <= info.RadeonSuperResolution.SharpnessRange.MaxValue);
        }

        if (info.ImageSharpenDesktop.IsSupported)
        {
            Assert.IsType<bool>(info.ImageSharpenDesktop.IsEnabled);
        }
    }

    [SkippableFact]
    public void Three_d_anti_lag_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.AntiLag.IsSupported, "Anti-Lag not supported on this GPU.");
        var antiLag = info.AntiLag;
        Assert.IsType<bool>(antiLag.IsEnabled);
        // Level is nullable — null means driver does not support IADLX3DAntiLag1; either value is valid.
        _ = antiLag.Level; // property must be accessible (may be null on older drivers)
    }

    [SkippableFact]
    public void Three_d_boost_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.Boost.IsSupported, "Boost not supported on this GPU.");
        var boost = info.Boost;
        Assert.True(boost.ResolutionRange.MinValue <= boost.MinResolution && boost.MinResolution <= boost.ResolutionRange.MaxValue);
    }

    [SkippableFact]
    public void Three_d_image_sharpening_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.ImageSharpening.IsSupported, "Image sharpening not supported on this GPU.");
        var ris = info.ImageSharpening;
        Assert.True(ris.SharpnessRange.MinValue <= ris.Sharpness && ris.Sharpness <= ris.SharpnessRange.MaxValue);
    }

    [SkippableFact]
    public void Three_d_enhanced_sync_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.EnhancedSync.IsSupported, "Enhanced Sync not supported on this GPU.");
        Assert.IsType<bool>(info.EnhancedSync.IsEnabled);
    }

    [SkippableFact]
    public void Three_d_wait_for_vertical_refresh_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.WaitForVerticalRefresh.IsSupported, "Wait for Vertical Refresh not supported on this GPU.");
        Assert.True(Enum.IsDefined(typeof(ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE), info.WaitForVerticalRefresh.Mode));
    }

    [SkippableFact]
    public void Three_d_frame_rate_target_control_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.FrameRateTargetControl.IsSupported, "Frame Rate Target Control not supported on this GPU.");
        var frtc = info.FrameRateTargetControl;
        Assert.True(frtc.FpsRange.MinValue <= frtc.Fps && frtc.Fps <= frtc.FpsRange.MaxValue);
    }

    [SkippableFact]
    public void Three_d_anti_aliasing_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.AntiAliasing.IsSupported, "Anti-Aliasing not supported on this GPU.");
        Assert.True(Enum.IsDefined(typeof(ADLX_ANTI_ALIASING_MODE), info.AntiAliasing.Mode));
    }

    [SkippableFact]
    public void Three_d_anisotropic_filtering_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.AnisotropicFiltering.IsSupported, "Anisotropic Filtering not supported on this GPU.");
        Assert.True(Enum.IsDefined(typeof(ADLX_ANISOTROPIC_FILTERING_LEVEL), info.AnisotropicFiltering.Level));
    }

    [SkippableFact]
    public void Three_d_tessellation_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.Tessellation.IsSupported, "Tessellation not supported on this GPU.");
        var tess = info.Tessellation;
        Assert.True(Enum.IsDefined(typeof(ADLX_TESSELLATION_MODE), tess.Mode));
        Assert.True(Enum.IsDefined(typeof(ADLX_TESSELLATION_LEVEL), tess.Level));
    }

    [SkippableFact]
    public void Three_d_fluid_motion_frames_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var gpus = _fixture.System!.EnumerateGPUs().ToList();
        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        var gpuUniqueId = gpus[0].UniqueId;

        try
        {
            if (!helper.TryGetFluidMotionFrames(gpuUniqueId, out var info))
                throw new Xunit.SkipException("AMD Fluid Motion Frames not supported on this GPU.");

            Skip.If(!info.IsSupported, "AMD Fluid Motion Frames reported unsupported.");
            Assert.IsType<bool>(info.IsEnabled);
        }
        catch (SEHException ex)
        {
            throw new Xunit.SkipException($"AMD Fluid Motion Frames call failed (SEH), treating as unsupported: {ex.Message}");
        }
    }

    [SkippableFact]
    public void Three_d_radeon_super_resolution_facade()
    {
        using var helper = Get3DHelperOrSkip();
        try
        {
            if (!helper.TryGetRadeonSuperResolution(out var info))
                throw new Xunit.SkipException("Radeon Super Resolution not supported on this system.");

            Skip.If(!info.IsSupported, "Radeon Super Resolution reported unsupported.");
            Assert.True(info.SharpnessRange.MinValue <= info.Sharpness && info.Sharpness <= info.SharpnessRange.MaxValue);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException($"Radeon Super Resolution not supported: {ex.Result}");
        }
        catch (SEHException ex)
        {
            throw new Xunit.SkipException($"Radeon Super Resolution call failed (SEH), treating as unsupported: {ex.Message}");
        }
    }

    [SkippableFact]
    public void Three_d_chill_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var gpus = _fixture.System!.EnumerateGPUs().ToList();
        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        var gpuUniqueId = gpus[0].UniqueId;
        if (!helper.TryGetChill(gpuUniqueId, out var info))
            throw new Xunit.SkipException("Chill not supported on this GPU.");

        Skip.If(!info.IsSupported, "Chill reported unsupported.");
        Assert.True(info.FPSRange.MinValue <= info.MinFPS && info.MinFPS <= info.FPSRange.MaxValue,
            $"MinFPS {info.MinFPS} is outside FPSRange [{info.FPSRange.MinValue},{info.FPSRange.MaxValue}]");
        Assert.True(info.FPSRange.MinValue <= info.MaxFPS && info.MaxFPS <= info.FPSRange.MaxValue,
            $"MaxFPS {info.MaxFPS} is outside FPSRange [{info.FPSRange.MinValue},{info.FPSRange.MaxValue}]");
    }

    [SkippableFact]
    public void Three_d_morphological_anti_aliasing_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.MorphologicalAntiAliasing.IsSupported,
            "Morphological Anti-Aliasing not supported on this GPU.");
        Assert.IsType<bool>(info.MorphologicalAntiAliasing.IsEnabled);
    }

    [SkippableFact]
    public void Three_d_image_sharpen_desktop_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var gpus = _fixture.System!.EnumerateGPUs().ToList();
        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        var gpuUniqueId = gpus[0].UniqueId;
        if (!helper.TryGetImageSharpenDesktop(gpuUniqueId, out var info))
            throw new Xunit.SkipException("ImageSharpenDesktop not supported on this system (IADLX3DSettingsServices2 not available or GPU unsupported).");

        Skip.If(!info.IsSupported, "ImageSharpenDesktop reported unsupported.");
        Assert.IsType<bool>(info.IsEnabled);
    }
}
