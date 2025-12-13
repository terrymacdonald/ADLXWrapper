using System.Collections.Generic;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    public enum GammaPreset
    {
        Unknown = 0,
        SRGB,
        BT709,
        PQ,
        PQ2084,
        G36,
        CustomCoefficient,
        CustomRamp,
        Reset
    }

    public sealed class GammaState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public GammaPreset Preset { get; set; }
        [JsonProperty] public ADLX_RegammaCoeff? Coeff { get; set; }
        [JsonProperty] public ADLX_GammaRamp? Ramp { get; set; }
    }

    public sealed class GamutState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public ADLX_GAMUT_SPACE? ColorSpace { get; set; }
        [JsonProperty] public ADLX_WHITE_POINT? WhitePoint { get; set; }
        [JsonProperty] public ADLX_RGB? CustomWhitePoint { get; set; }
    }

    public enum ThreeDLUTMode
    {
        Disabled = 0,
        VividGaming
    }

    public sealed class ThreeDLUTState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public bool DynamicContrastSupported { get; set; }
        [JsonProperty] public ThreeDLUTMode Mode { get; set; }
        [JsonProperty] public int? DynamicContrast { get; set; }
    }

    public sealed class CustomColorState
    {
        [JsonProperty] public bool IsHueSupported { get; set; }
        [JsonProperty] public int? Hue { get; set; }
        [JsonProperty] public bool IsSaturationSupported { get; set; }
        [JsonProperty] public int? Saturation { get; set; }
        [JsonProperty] public bool IsBrightnessSupported { get; set; }
        [JsonProperty] public int? Brightness { get; set; }
        [JsonProperty] public bool IsContrastSupported { get; set; }
        [JsonProperty] public int? Contrast { get; set; }
        [JsonProperty] public bool IsTemperatureSupported { get; set; }
        [JsonProperty] public int? Temperature { get; set; }
    }

    public sealed class ConnectivityState
    {
        [JsonProperty] public bool HdmiQualityDetectionSupported { get; set; }
        [JsonProperty] public bool? HdmiQualityDetectionEnabled { get; set; }
        [JsonProperty] public bool DpLinkRateSupported { get; set; }
        [JsonProperty] public ADLX_DP_LINK_RATE? DpLinkRate { get; set; }
        [JsonProperty] public bool RelativePreEmphasisSupported { get; set; }
        [JsonProperty] public int? RelativePreEmphasis { get; set; }
        [JsonProperty] public bool RelativeVoltageSwingSupported { get; set; }
        [JsonProperty] public int? RelativeVoltageSwing { get; set; }
    }

    public sealed class DisplayResolutionState
    {
        [JsonProperty] public int Width { get; set; }
        [JsonProperty] public int Height { get; set; }
        [JsonProperty] public int RefreshRate { get; set; }
        [JsonProperty] public ADLX_DISPLAY_SCAN_TYPE? ScanType { get; set; }
        [JsonProperty] public ADLX_TIMING_STANDARD? TimingStandard { get; set; }
        [JsonProperty] public int? PixelClock { get; set; }
        [JsonProperty] public ADLX_TimingInfo? DetailedTiming { get; set; }
    }

    public sealed class CustomResolutionState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public List<DisplayResolutionState> Resolutions { get; set; } = new();
    }

    public sealed class ColorDepthState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public ADLX_COLOR_DEPTH? Value { get; set; }
    }

    public sealed class PixelFormatState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public ADLX_PIXEL_FORMAT? Value { get; set; }
    }

    public sealed class FreeSyncState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public bool? Enabled { get; set; }
    }

    public sealed class VirtualSuperResolutionState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public bool? Enabled { get; set; }
    }

    public sealed class IntegerScalingState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public bool? Enabled { get; set; }
    }

    public sealed class GPUScalingState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public bool? Enabled { get; set; }
    }

    public sealed class ScalingModeState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public ADLX_SCALE_MODE? Mode { get; set; }
    }

    public sealed class HdcpState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public bool? Enabled { get; set; }
    }

    public sealed class VariBrightState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public bool? Enabled { get; set; }
        [JsonProperty] public ADLXDisplaySettingsHelpers.VariBrightMode Mode { get; set; }
    }

    public sealed class BlankingState
    {
        [JsonProperty] public bool Supported { get; set; }
        [JsonProperty] public bool? Blanked { get; set; }
    }

    public sealed class DisplayProfile
    {
        [JsonProperty] public ulong UniqueId { get; set; }
        [JsonProperty] public int GpuUniqueId { get; set; }
        [JsonProperty] public string Name { get; set; } = string.Empty;
        [JsonProperty] public string? Edid { get; set; }
        [JsonProperty] public DisplayResolutionState? Resolution { get; set; }
        [JsonProperty] public GammaState? Gamma { get; set; }
        [JsonProperty] public GamutState? Gamut { get; set; }
        [JsonProperty] public ThreeDLUTState? ThreeDLUT { get; set; }
        [JsonProperty] public CustomColorState? CustomColor { get; set; }
        [JsonProperty] public ConnectivityState? Connectivity { get; set; }
        [JsonProperty] public CustomResolutionState? CustomResolution { get; set; }
        [JsonProperty] public ColorDepthState? ColorDepth { get; set; }
        [JsonProperty] public PixelFormatState? PixelFormat { get; set; }
        [JsonProperty] public FreeSyncState? FreeSync { get; set; }
        [JsonProperty] public VirtualSuperResolutionState? Vsr { get; set; }
        [JsonProperty] public IntegerScalingState? IntegerScaling { get; set; }
        [JsonProperty] public GPUScalingState? GpuScaling { get; set; }
        [JsonProperty] public ScalingModeState? ScalingMode { get; set; }
        [JsonProperty] public HdcpState? Hdcp { get; set; }
        [JsonProperty] public VariBrightState? VariBright { get; set; }
        [JsonProperty] public BlankingState? Blanking { get; set; }
    }

    public sealed class DesktopProfile
    {
        [JsonProperty] public ADLX_DESKTOP_TYPE Type { get; set; }
        [JsonProperty] public int Width { get; set; }
        [JsonProperty] public int Height { get; set; }
        [JsonProperty] public int TopLeftX { get; set; }
        [JsonProperty] public int TopLeftY { get; set; }
        [JsonProperty] public ADLX_ORIENTATION Orientation { get; set; }
        [JsonProperty] public List<DisplayProfile> Displays { get; set; } = new();
    }
}
