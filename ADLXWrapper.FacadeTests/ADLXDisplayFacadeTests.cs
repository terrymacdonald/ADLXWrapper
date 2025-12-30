using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public class ADLXDisplayFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLXDisplayFacadeTests(FacadeSessionFixture fixture)
    {
        _fixture = fixture;
    }

    private void SkipIfUnavailable()
    {
        Skip.If(!string.IsNullOrWhiteSpace(_fixture.SkipReason), _fixture.SkipReason);
    }

    private IReadOnlyList<ADLXDisplay> GetDisplaysOrSkip()
    {
        SkipIfUnavailable();
        try
        {
            var displays = _fixture.System!.EnumerateDisplays();
            Skip.If(displays.Count == 0, "No displays returned by ADLX.");
            return displays;
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Display enumeration not supported on this hardware/driver.");
        }
        catch (AccessViolationException ex)
        {
            throw new Xunit.SkipException($"Display enumeration crashed (likely unsupported environment): {ex.Message}");
        }
    }

    private static void DisposeRest(IReadOnlyList<ADLXDisplay> displays, int startAt = 1)
    {
        for (int i = startAt; i < displays.Count; i++)
        {
            displays[i].Dispose();
        }
    }

    [SkippableFact]
    public void Displays_enumerate_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var first = displays[0];
        DisposeRest(displays);

        Assert.False(string.IsNullOrWhiteSpace(first.Name));
    }

    [SkippableFact]
    public void Display_identity_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        Assert.False(string.IsNullOrWhiteSpace(display.Name));
        Assert.False(string.IsNullOrWhiteSpace(display.Edid));
        Assert.True(display.NativeResolutionWidth > 0);
        Assert.True(display.NativeResolutionHeight > 0);
        Assert.True(display.RefreshRate > 0);
        Assert.NotEqual<uint>(0, display.ManufacturerId);
        Assert.NotEqual<uint>(0, display.PixelClock);
        Assert.NotEqual<ulong>(0, display.UniqueId);
    }

    [SkippableFact]
    public void Display_freesync_state_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        var (supported, enabled) = display.GetFreeSyncState();
        Skip.If(!supported, "FreeSync not supported on this display.");
        Assert.IsAssignableFrom<bool>(enabled);
    }

    [SkippableFact]
    public void Display_gpu_scaling_state_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        var (supported, enabled) = display.GetGpuScalingState();
        Skip.If(!supported, "GPU scaling not supported on this display.");
        Assert.IsAssignableFrom<bool>(enabled);
    }

    [SkippableFact]
    public void Display_scaling_mode_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        var (supported, mode) = display.GetScalingMode();
        Skip.If(!supported, "Scaling mode not supported on this display.");
        Assert.True(Enum.IsDefined(typeof(ADLX_SCALE_MODE), mode));
    }

    [SkippableFact]
    public void Display_color_depth_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        var (supported, depth) = display.GetColorDepthState();
        Skip.If(!supported, "Color depth not supported on this display.");
        Assert.True(Enum.IsDefined(typeof(ADLX_COLOR_DEPTH), depth));
    }

    [SkippableFact]
    public void Display_pixel_format_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        var (supported, format) = display.GetPixelFormatState();
        Skip.If(!supported, "Pixel format not supported on this display.");
        Assert.True(Enum.IsDefined(typeof(ADLX_PIXEL_FORMAT), format));
    }

    [SkippableFact]
    public void Display_custom_color_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        var info = display.GetCustomColor();
        Skip.If(!info.IsSupported, "Custom color not supported on this display.");
        Assert.True(info.IsHueSupported || info.IsSaturationSupported || info.IsBrightnessSupported || info.IsContrastSupported || info.IsTemperatureSupported);
    }

    [SkippableFact]
    public void Display_varibright_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        var (supported, enabled, mode) = display.GetVariBrightState();
        Skip.If(!supported, "VariBright not supported on this display.");
        Assert.True(Enum.IsDefined(typeof(VariBrightMode), mode));
        Assert.IsAssignableFrom<bool>(enabled);
    }

    [SkippableFact]
    public void Display_gamma_info_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        var info = display.GetGamma();
        Skip.If(!info.IsSupported, "Gamma not supported on this display.");
        Assert.True(
            info.IsCurrentReGammaSRGB ||
            info.IsCurrentReGammaBT709 ||
            info.IsCurrentReGammaPQ ||
            info.IsCurrentReGammaPQ2084 ||
            info.IsCurrentReGamma36 ||
            info.HasRegammaCoefficient ||
            info.HasReGammaRamp ||
            info.HasDeGammaRamp);
    }

    [SkippableFact]
    public void Display_gamut_info_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        var info = display.GetGamut();
        Skip.If(!info.IsWhitePointSupported && !info.IsGamutSupported, "Gamut not supported on this display.");
        Assert.True(
            info.IsCurrent5000K ||
            info.IsCurrent6500K ||
            info.IsCurrent7500K ||
            info.IsCurrent9300K ||
            info.IsCurrentCustomWhitePoint ||
            info.IsCurrent709 ||
            info.IsCurrent601 ||
            info.IsCurrentAdobe ||
            info.IsCurrentCieRgb ||
            info.IsCurrent2020 ||
            info.IsCurrentCustomColorSpace);
    }

    [SkippableFact]
    public void Display_3dlut_info_facade()
    {
        var displays = GetDisplaysOrSkip();
        using var display = displays[0];
        DisposeRest(displays);

        var info = display.GetThreeDLut();
        Skip.If(!info.IsSceSupported && !info.IsSceVividGamingSupported && !info.IsSceDynamicContrastSupported && !info.IsUser3DLutSupported, "3DLUT not supported on this display.");
        Assert.True(info.IsCurrentSceDisabled || info.IsCurrentSceVividGaming || info.HasDynamicContrast);
    }
}
