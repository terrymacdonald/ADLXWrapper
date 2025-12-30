using System;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Display facade tests (info and settings).
    /// </summary>
    [SupportedOSPlatform("windows")]
    [Collection("DisplaySerial")]
    public sealed unsafe class ADLXDisplayFacadeTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly ADLXDisplay? _display;
        private readonly string _skipReason = string.Empty;

        public ADLXDisplayFacadeTests(ITestOutputHelper output)
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
                _display = displays[0];
                foreach (var d in displays.Skip(1)) d.Dispose();
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

        public void Dispose()
        {
            _display?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void DisplayInfo_ShouldBePopulated()
        {
            Skip.If(_api == null || _system == null || _display == null, _skipReason);
            var info = _display.GetDisplayInfo();
            _output.WriteLine($"Display: {info.Name}, {info.NativeResolutionWidth}x{info.NativeResolutionHeight} @ {info.RefreshRate}Hz");
            Assert.True(info.NativeResolutionWidth > 0);
            Assert.True(info.NativeResolutionHeight > 0);
        }

        [SkippableFact]
        public void DisplaySettings_Getters_DoNotThrow()
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

            Check("FreeSync", () => _display.GetFreeSyncState());
            Check("GPUScaling", () => _display.GetGpuScalingState());
            Check("ScalingMode", () => _display.GetScalingMode());
            Check("VirtualSuperResolution", () => _display.GetVirtualSuperResolutionState());
            Check("IntegerScaling", () => _display.GetIntegerScalingState());
            Check("HDCP", () => _display.GetHdcpState());
            Check("VariBright", () => _display.GetVariBrightState());
            Check("ColorDepth", () => _display.GetColorDepthState());
            Check("PixelFormat", () => _display.GetPixelFormatState());
            Check("Gamma", () => _display.GetGamma());
            Check("Gamut", () => _display.GetGamut());
            Check("CustomColor", () => _display.GetCustomColor());
            Check("FreeSyncColorAccuracy", () => _display.GetFreeSyncColorAccuracyState());
            Check("DynamicRefreshRateControl", () => _display.GetDynamicRefreshRateControlState());
            Check("DisplayBlanking", () => _display.GetDisplayBlankingState());
            Check("ConnectivityExperience", () => _display.GetConnectivityExperience());
            Check("CustomResolutions", () => _display.EnumerateCustomResolutions().ToList());
        }
    }
}
