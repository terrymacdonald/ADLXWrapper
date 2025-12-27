using System;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Lightweight smoke tests that exercise the public facades only.
    /// </summary>
    [SupportedOSPlatform("windows")]
    [Collection("DisplaySerial")]
    public sealed unsafe class FacadeSmokeTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly string _skipReason = string.Empty;

        public FacadeSmokeTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = _api.GetSystemServices();
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _skipReason = "ADLX not supported on this system.";
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanEnumerateDisplaysViaFacade()
        {
            Skip.If(_api == null || _system == null, _skipReason);

            var displays = _system.EnumerateDisplays();
            Skip.If(displays.Count == 0, "No displays found.");

            var d = displays[0];
            _output.WriteLine($"Display: {d.Name}, UID: {d.UniqueId}, {d.NativeResolutionWidth}x{d.NativeResolutionHeight} @ {d.RefreshRate:F2}Hz");

            var info = d.GetDisplayInfo();
            Assert.Equal(d.Name, info.Name);
            Assert.True(info.NativeResolutionWidth > 0);
            Assert.True(info.NativeResolutionHeight > 0);

            foreach (var disp in displays.Skip(1)) disp.Dispose();
        }

        [SkippableFact]
        public void CanEnumerateGpusViaFacade()
        {
            Skip.If(_api == null || _system == null, _skipReason);

            var gpus = _system.EnumerateADLXGPUs();
            Skip.If(gpus.Count == 0, "No GPUs found.");

            using var gpu = gpus[0];
            _output.WriteLine($"GPU: {gpu.Name}, UID: {gpu.UniqueId}, VRAM: {gpu.TotalVRAM}, Vendor: {gpu.VendorId}");

            foreach (var g in gpus.Skip(1)) g.Dispose();
        }

        [SkippableFact]
        public void DisplaySettingsViaFacade_DoNotThrow()
        {
            Skip.If(_api == null || _system == null, _skipReason);

            var displays = _system.EnumerateDisplays();
            Skip.If(displays.Count == 0, "No displays found.");
            using var d = displays[0];

            void Check(string name, Action action)
            {
                try { action(); }
                catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                {
                    _output.WriteLine($"{name} not supported: {ex.Message}");
                }
            }

            Check("FreeSync", () => _output.WriteLine($"FreeSync: {_displayTuple(_=d.GetFreeSyncState())}"));
            Check("ScalingMode", () => _output.WriteLine($"Scaling: {_displayTuple(_=d.GetScalingMode())}"));
            Check("VirtualSuperResolution", () => _output.WriteLine($"VSR: {_displayTuple(_=d.GetVirtualSuperResolutionState())}"));
            Check("IntegerScaling", () => _output.WriteLine($"IntegerScaling: {_displayTuple(_=d.GetIntegerScalingState())}"));
            Check("Hdcp", () => _output.WriteLine($"HDCP: {_displayTuple(_=d.GetHdcpState())}"));
            Check("VariBright", () => _output.WriteLine($"VariBright: {_displayTuple(_=d.GetVariBrightState())}"));
            Check("ColorDepth", () => _output.WriteLine($"ColorDepth: {_displayTuple(_=d.GetColorDepthState())}"));
            Check("PixelFormat", () => _output.WriteLine($"PixelFormat: {_displayTuple(_=d.GetPixelFormatState())}"));
            Check("Gamma", () => _output.WriteLine($"Gamma supported: {d.GetGamma().IsSupported}"));
            Check("Gamut", () => _output.WriteLine($"Gamut supported: {d.GetGamut().IsGamutSupported}"));
            Check("CustomColor", () => _output.WriteLine($"CustomColor supported: {d.GetCustomColor().IsSupported}"));
            Check("FreeSyncColorAccuracy", () => _output.WriteLine($"FSCA: {_displayTuple(_=d.GetFreeSyncColorAccuracyState())}"));
            Check("DynamicRefreshRateControl", () => _output.WriteLine($"DRR: {_displayTuple(_=d.GetDynamicRefreshRateControlState())}"));
        }

        private static string _displayTuple((bool supported, bool enabled) t) => $"{t.supported}/{t.enabled}";
        private static string _displayTuple((bool supported, ADLX_SCALE_MODE mode) t) => $"{t.supported}/{t.mode}";
        private static string _displayTuple((bool supported, ADLX_COLOR_DEPTH current) t) => $"{t.supported}/{t.current}";
        private static string _displayTuple((bool supported, ADLX_PIXEL_FORMAT current) t) => $"{t.supported}/{t.current}";
        private static string _displayTuple((bool supported, bool enabled, VariBrightMode mode) t) => $"{t.supported}/{t.enabled}/{t.mode}";
    }
}
