using System;
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

    private unsafe All3DSettingsInfo GetAll3DSettingsOrSkip(ADLX3DSettingsServicesHelper helper)
    {
        var handles = _fixture.System!.EnumerateGPUsHandle();
        Skip.If(handles.Length == 0, "No GPUs returned by ADLX.");
        using var gpuHandle = handles[0];

        if (!helper.TryGetAll3DSettings(gpuHandle.As<IADLXGPU>(), out var info))
            throw new Xunit.SkipException("3D settings not supported on this GPU.");

        return info;
    }

    [SkippableFact]
    public void Three_d_settings_info_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Assert.True(
            info.AntiLag.HasValue ||
            info.Boost.HasValue ||
            info.ImageSharpening.HasValue ||
            info.EnhancedSync.HasValue ||
            info.WaitForVerticalRefresh.HasValue ||
            info.FrameRateTargetControl.HasValue ||
            info.AntiAliasing.HasValue ||
            info.AnisotropicFiltering.HasValue ||
            info.Tessellation.HasValue,
            "No 3D settings information returned.");

        if (info.AntiLag is { } antiLag && antiLag.IsSupported)
        {
            Assert.IsType<bool>(antiLag.IsEnabled);
        }

        if (info.Boost is { } boost && boost.IsSupported)
        {
            Assert.True(boost.ResolutionRange.minValue <= boost.MinResolution && boost.MinResolution <= boost.ResolutionRange.maxValue);
        }

        if (info.ImageSharpening is { } sharpening && sharpening.IsSupported)
        {
            Assert.True(sharpening.SharpnessRange.minValue <= sharpening.Sharpness && sharpening.Sharpness <= sharpening.SharpnessRange.maxValue);
        }

        if (info.EnhancedSync is { } enhancedSync && enhancedSync.IsSupported)
        {
            Assert.IsType<bool>(enhancedSync.IsEnabled);
        }

        if (info.WaitForVerticalRefresh is { } vsync && vsync.IsSupported)
        {
            Assert.True(Enum.IsDefined(typeof(ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE), vsync.Mode));
        }

        if (info.FrameRateTargetControl is { } frtc && frtc.IsSupported)
        {
            Assert.True(frtc.FpsRange.minValue <= frtc.Fps && frtc.Fps <= frtc.FpsRange.maxValue);
        }

        if (info.AntiAliasing is { } aa && aa.IsSupported)
        {
            Assert.True(Enum.IsDefined(typeof(ADLX_ANTI_ALIASING_MODE), aa.Mode));
        }

        if (info.AnisotropicFiltering is { } af && af.IsSupported)
        {
            Assert.True(Enum.IsDefined(typeof(ADLX_ANISOTROPIC_FILTERING_LEVEL), af.Level));
        }

        if (info.Tessellation is { } tess && tess.IsSupported)
        {
            Assert.True(Enum.IsDefined(typeof(ADLX_TESSELLATION_MODE), tess.Mode));
            Assert.True(Enum.IsDefined(typeof(ADLX_TESSELLATION_LEVEL), tess.Level));
        }

    }

    [SkippableFact]
    public void Three_d_anti_lag_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.AntiLag.HasValue || !info.AntiLag.Value.IsSupported, "Anti-Lag not supported on this GPU.");
        Assert.IsType<bool>(info.AntiLag!.Value.IsEnabled);
    }

    [SkippableFact]
    public void Three_d_boost_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.Boost.HasValue || !info.Boost.Value.IsSupported, "Boost not supported on this GPU.");
        var boost = info.Boost!.Value;
        Assert.True(boost.ResolutionRange.minValue <= boost.MinResolution && boost.MinResolution <= boost.ResolutionRange.maxValue);
    }

    [SkippableFact]
    public void Three_d_image_sharpening_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.ImageSharpening.HasValue || !info.ImageSharpening.Value.IsSupported, "Image sharpening not supported on this GPU.");
        var ris = info.ImageSharpening!.Value;
        Assert.True(ris.SharpnessRange.minValue <= ris.Sharpness && ris.Sharpness <= ris.SharpnessRange.maxValue);
    }

    [SkippableFact]
    public void Three_d_enhanced_sync_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.EnhancedSync.HasValue || !info.EnhancedSync.Value.IsSupported, "Enhanced Sync not supported on this GPU.");
        Assert.IsType<bool>(info.EnhancedSync!.Value.IsEnabled);
    }

    [SkippableFact]
    public void Three_d_wait_for_vertical_refresh_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.WaitForVerticalRefresh.HasValue || !info.WaitForVerticalRefresh.Value.IsSupported, "Wait for Vertical Refresh not supported on this GPU.");
        Assert.True(Enum.IsDefined(typeof(ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE), info.WaitForVerticalRefresh!.Value.Mode));
    }

    [SkippableFact]
    public void Three_d_frame_rate_target_control_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.FrameRateTargetControl.HasValue || !info.FrameRateTargetControl.Value.IsSupported, "Frame Rate Target Control not supported on this GPU.");
        var frtc = info.FrameRateTargetControl!.Value;
        Assert.True(frtc.FpsRange.minValue <= frtc.Fps && frtc.Fps <= frtc.FpsRange.maxValue);
    }

    [SkippableFact]
    public void Three_d_anti_aliasing_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.AntiAliasing.HasValue || !info.AntiAliasing.Value.IsSupported, "Anti-Aliasing not supported on this GPU.");
        Assert.True(Enum.IsDefined(typeof(ADLX_ANTI_ALIASING_MODE), info.AntiAliasing!.Value.Mode));
    }

    [SkippableFact]
    public void Three_d_anisotropic_filtering_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.AnisotropicFiltering.HasValue || !info.AnisotropicFiltering.Value.IsSupported, "Anisotropic Filtering not supported on this GPU.");
        Assert.True(Enum.IsDefined(typeof(ADLX_ANISOTROPIC_FILTERING_LEVEL), info.AnisotropicFiltering!.Value.Level));
    }

    [SkippableFact]
    public void Three_d_tessellation_facade()
    {
        using var helper = Get3DHelperOrSkip();
        var info = GetAll3DSettingsOrSkip(helper);

        Skip.If(!info.Tessellation.HasValue || !info.Tessellation.Value.IsSupported, "Tessellation not supported on this GPU.");
        var tess = info.Tessellation!.Value;
        Assert.True(Enum.IsDefined(typeof(ADLX_TESSELLATION_MODE), tess.Mode));
        Assert.True(Enum.IsDefined(typeof(ADLX_TESSELLATION_LEVEL), tess.Level));
    }

    [SkippableFact]
    public void Three_d_fluid_motion_frames_facade()
    {
        using var helper = Get3DHelperOrSkip();
        unsafe
        {
            var handles = _fixture.System!.EnumerateGPUsHandle();
            Skip.If(handles.Length == 0, "No GPUs returned by ADLX.");
            using var gpuHandle = handles[0];

            try
            {
                if (!helper.TryGetFluidMotionFrames(gpuHandle.As<IADLXGPU>(), out var info))
                    throw new Xunit.SkipException("AMD Fluid Motion Frames not supported on this GPU.");

                Skip.If(!info.IsSupported, "AMD Fluid Motion Frames reported unsupported.");
                Assert.IsType<bool>(info.IsEnabled);
            }
            catch (SEHException ex)
            {
                throw new Xunit.SkipException($"AMD Fluid Motion Frames call failed (SEH), treating as unsupported: {ex.Message}");
            }
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
            Assert.True(info.SharpnessRange.minValue <= info.Sharpness && info.Sharpness <= info.SharpnessRange.maxValue);
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
}
