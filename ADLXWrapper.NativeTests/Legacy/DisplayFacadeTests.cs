using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Facade-level display tests that avoid native pointers.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public sealed class DisplayFacadeTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly ADLXDisplay? _display;
        private readonly string _skipReason = string.Empty;

        public DisplayFacadeTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = _api.GetSystemServices();

                var displays = _system.EnumerateDisplays();
                if (displays.Count == 0)
                {
                    _skipReason = "No displays found.";
                    return;
                }

                // Take the first display and dispose the rest.
                _display = displays[0];
                for (int i = 1; i < displays.Count; i++)
                {
                    displays[i].Dispose();
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _skipReason = "Display services are not supported on this system.";
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        [SkippableFact]
        public void Facade_Getters_DoNotRequirePointers()
        {
            Skip.If(_api == null || _system == null || _display == null, _skipReason);

            try
            {
                var vsr = _display.GetVirtualSuperResolutionState();
                _output.WriteLine($"VSR supported: {vsr.supported}, enabled: {vsr.enabled}");
            }
            catch (ADLXException ex)
            {
                Skip.If(true, $"Display services v3 (VSR) not available: {ex.Message}");
            }

            var scaling = _display.GetScalingMode();
            _output.WriteLine($"Scaling supported: {scaling.supported}, mode: {scaling.mode}");

            try
            {
                var freesync = _display.GetFreeSyncState();
                _output.WriteLine($"FreeSync supported: {freesync.supported}, enabled: {freesync.enabled}");
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, $"FreeSync not available: {ex.Message}");
            }
        }

        [SkippableFact]
        public void Facade_All_Getters_DoNotThrow()
        {
            Skip.If(_api == null || _system == null || _display == null, _skipReason);

            void Check(string name, Action action)
            {
                try { action(); }
                catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                {
                    _output.WriteLine($"{name} not supported: {ex.Message}");
                }
            }

            var d = _display;
            _output.WriteLine($"Display: {d.Name} ({d.NativeResolutionWidth}x{d.NativeResolutionHeight} @ {d.RefreshRate:F2}Hz)");
            _output.WriteLine($"EDID length: {d.Edid?.Length ?? 0}, Manufacturer: {d.ManufacturerId}, PixelClock: {d.PixelClock}, Connector: {d.ConnectorType}, Type: {d.Type}, Scan: {d.ScanType}, UID: {d.UniqueId}, GPU UID: {d.GpuUniqueId}");

            Check("DisplayInfo", () => { var info = d.GetDisplayInfo(); _output.WriteLine($"Info name: {info.Name}"); });
            Check("FreeSyncState", () => { var fs = d.GetFreeSyncState(); _output.WriteLine($"FreeSync: {fs.supported}/{fs.enabled}"); });
            Check("GPUScalingState", () => { var s = d.GetGpuScalingState(); _output.WriteLine($"GPU scaling: {s.supported}/{s.enabled}"); });
            Check("ScalingMode", () => { var m = d.GetScalingMode(); _output.WriteLine($"Scaling mode supported: {m.supported}, mode: {m.mode}"); });
            Check("CustomResolutions", () => { foreach (var cr in d.EnumerateCustomResolutions()) { _output.WriteLine($"Custom res: {cr.ResWidth}x{cr.ResHeight}@{cr.RefreshRate}"); } });
            Check("CustomResolutionListNative", () => { using var list = d.GetCustomResolutionListNative(); _output.WriteLine("Custom resolution list acquired."); });
            Check("VirtualSuperResolution", () => { var vsr = d.GetVirtualSuperResolutionState(); _output.WriteLine($"VSR: {vsr.supported}/{vsr.enabled}"); });
            Check("IntegerScaling", () => { var iscale = d.GetIntegerScalingState(); _output.WriteLine($"Integer scaling: {iscale.supported}/{iscale.enabled}"); });
            Check("HDCP", () => { var hdcp = d.GetHdcpState(); _output.WriteLine($"HDCP: {hdcp.supported}/{hdcp.enabled}"); });
            Check("VariBright", () => { var vb = d.GetVariBrightState(); _output.WriteLine($"VariBright: {vb.supported}/{vb.enabled}/{vb.mode}"); });
            Check("ColorDepth", () => { var cd = d.GetColorDepthState(); _output.WriteLine($"ColorDepth: {cd.supported}/{cd.current}"); });
            Check("PixelFormat", () => { var pf = d.GetPixelFormatState(); _output.WriteLine($"PixelFormat: {pf.supported}/{pf.current}"); });
            Check("CustomColor", () => { var cc = d.GetCustomColor(); _output.WriteLine($"CustomColor supported: {cc.IsSupported}"); });
            Check("Gamma", () => { var g = d.GetGamma(); _output.WriteLine($"Gamma supported: {g.IsSupported}"); });
            Check("Gamut", () => { var g = d.GetGamut(); _output.WriteLine($"Gamut supported: {g.IsGamutSupported}"); });
            Check("ThreeDLut", () => { var t = d.GetThreeDLut(); _output.WriteLine($"3DLUT user supported: {t.IsUser3DLutSupported}"); });
            Check("ConnectivityExperience", () => { var c = d.GetConnectivityExperience(); _output.WriteLine($"Connectivity HDMI quality supported: {c.IsHdmiQualityDetectionSupported}"); });
            Check("DisplayBlanking", () => { var b = d.GetDisplayBlankingState(); _output.WriteLine($"Display blanking: {b.supported}/{b.blanked}"); });
            Check("FreeSyncColorAccuracy", () => { var fsca = d.GetFreeSyncColorAccuracyState(); _output.WriteLine($"FSCA: {fsca.supported}/{fsca.enabled}"); });
            Check("DynamicRefreshRateControl", () => { var drrc = d.GetDynamicRefreshRateControlState(); _output.WriteLine($"DRR: {drrc.supported}/{drrc.enabled}"); });

            Check("GetGPU", () => { using var gpu = d.GetGPU(); _output.WriteLine($"GPU UID: {gpu.UniqueId}"); });
            Check("GetDesktop", () => { using var desk = d.GetDesktop(); _output.WriteLine($"Desktop size: {desk.Width}x{desk.Height}"); });
            Check("GetDesktopInfo", () => { var deskInfo = d.GetDesktopInfo(); _output.WriteLine($"Desktop type: {deskInfo.Type}"); });
        }

        public void Dispose()
        {
            _display?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }
    }
}
