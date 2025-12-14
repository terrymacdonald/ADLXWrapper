using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Round-trip profile apply for displays and desktops using the facade APIs.
    /// Skips on systems without AMD hardware or the ADLX DLL.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class ApplyProfileTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly ADLXSystemServices? _sys;
        private readonly string _skipReason = string.Empty;

        public ApplyProfileTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                _sys = _api.GetSystemServicesProfile();
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _sys?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void DisplayProfile_RoundTrips_WithSkipLogging()
        {
            Skip.If(_api == null || _sys == null, _skipReason);
            Skip.If(!ADLXHardwareDetection.HasAMDGPU(out var hwReason), hwReason);
            if (!ADLXApi.IsADLXDllAvailable(out var dllReason))
            {
                Skip.If(true, dllReason);
            }

            var display = _sys.EnumerateAllDisplays().FirstOrDefault();
            if (display == null)
            {
                Skip.If(true, "No displays found.");
            }

            using (display!)
            {
                var profile = display.GetProfile();
                var skips = new System.Collections.Generic.List<string>();
                display.ApplyProfile(profile, s => skips.Add(s));
                _output.WriteLine($"Applied profile to display {display.Name}; skips: {string.Join("; ", skips)}");
            }
        }

        [SkippableFact]
        public void DisplayProfile_SkipCallback_WhenDisplayServices3Missing()
        {
            Skip.If(_api == null || _sys == null, _skipReason);
            Skip.If(!ADLXHardwareDetection.HasAMDGPU(out var hwReason), hwReason);
            if (!ADLXApi.IsADLXDllAvailable(out var dllReason))
            {
                Skip.If(true, dllReason);
            }

            var display = _sys.EnumerateAllDisplays().FirstOrDefault();
            if (display == null)
            {
                Skip.If(true, "No displays found.");
            }

            using (display!)
            {
                var profile = display.GetProfile();
                using var displayServices = _sys.GetDisplayServicesHandle();
                var supportsDs3 = ADLXHelpers.TryQueryInterface((IntPtr)displayServices, nameof(IADLXDisplayServices3), out _);

                if (!supportsDs3)
                {
                    profile.Connectivity ??= new ConnectivityState();
                    profile.ColorDepth ??= new ColorDepthState();
                    profile.PixelFormat ??= new PixelFormatState();
                    profile.FreeSync ??= new FreeSyncState();
                    profile.Vsr ??= new VirtualSuperResolutionState();
                    profile.IntegerScaling ??= new IntegerScalingState();
                    profile.GpuScaling ??= new GPUScalingState();
                    profile.ScalingMode ??= new ScalingModeState();
                    profile.Hdcp ??= new HdcpState();
                    profile.VariBright ??= new VariBrightState();
                    profile.Blanking ??= new BlankingState();
                }

                var skips = new List<string>();
                display.ApplyProfile(profile, s => skips.Add(s));

                if (!supportsDs3)
                {
                    Assert.NotEmpty(skips);
                    _output.WriteLine($"Skip callbacks (DS3 unavailable): {string.Join("; ", skips)}");
                }
                else
                {
                    _output.WriteLine($"DS3 available; skip callbacks count: {skips.Count}");
                }
            }
        }

        [SkippableFact]
        public void DisplayProfile_SkipCallback_WhenDisplayServices2Or1Missing()
        {
            Skip.If(_api == null || _sys == null, _skipReason);
            Skip.If(!ADLXHardwareDetection.HasAMDGPU(out var hwReason), hwReason);
            if (!ADLXApi.IsADLXDllAvailable(out var dllReason))
            {
                Skip.If(true, dllReason);
            }

            var display = _sys.EnumerateAllDisplays().FirstOrDefault();
            if (display == null)
            {
                Skip.If(true, "No displays found.");
            }

            using (display!)
            {
                var profile = display.GetProfile();
                using var displayServices = _sys.GetDisplayServicesHandle();
                var supportsDs2 = ADLXHelpers.TryQueryInterface((IntPtr)displayServices, nameof(IADLXDisplayServices2), out _);
                var supportsDs1 = ADLXHelpers.TryQueryInterface((IntPtr)displayServices, nameof(IADLXDisplayServices1), out _);

                if (!supportsDs2)
                {
                    profile.Connectivity = new ConnectivityState
                    {
                        HdmiQualityDetectionSupported = true,
                        HdmiQualityDetectionEnabled = true,
                        DpLinkRateSupported = true,
                        DpLinkRate = ADLX_DP_LINK_RATE.DP_LINK_RATE_HBR2,
                        RelativePreEmphasisSupported = true,
                        RelativePreEmphasis = 0,
                        RelativeVoltageSwingSupported = true,
                        RelativeVoltageSwing = 0
                    };
                }

                if (!supportsDs1)
                {
                    profile.Blanking = new BlankingState { Supported = true, Blanked = true };
                }

                var skips = new List<string>();
                display.ApplyProfile(profile, s => skips.Add(s));

                if (!supportsDs2)
                {
                    Assert.Contains(skips, s => s.Contains("Connectivity"));
                    _output.WriteLine($"Connectivity skipped: {string.Join("; ", skips)}");
                }

                if (!supportsDs1)
                {
                    Assert.Contains(skips, s => s.Contains("Blanking"));
                    _output.WriteLine($"Blanking skipped: {string.Join("; ", skips)}");
                }

                if (supportsDs2 && supportsDs1)
                {
                    _output.WriteLine($"DisplayServices1/2 available; skip callbacks count: {skips.Count}");
                }
            }
        }

        [SkippableFact]
        public void DesktopProfile_RoundTrips()
        {
            Skip.If(_api == null || _sys == null, _skipReason);
            Skip.If(!ADLXHardwareDetection.HasAMDGPU(out var hwReason), hwReason);
            if (!ADLXApi.IsADLXDllAvailable(out var dllReason))
            {
                Skip.If(true, dllReason);
            }

            var desktop = _sys.EnumerateAllDesktops().FirstOrDefault();
            if (desktop == null)
            {
                Skip.If(true, "No desktops found.");
            }

            using (desktop!)
            {
                var profile = desktop.GetProfile();
                desktop.ApplyProfile(profile);
                _output.WriteLine($"Applied desktop profile {profile.Type} {profile.Width}x{profile.Height} at ({profile.TopLeftX},{profile.TopLeftY})");
            }
        }
    }
}
