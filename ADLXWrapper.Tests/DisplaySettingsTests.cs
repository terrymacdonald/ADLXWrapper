using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Smoke tests for display settings slice (FreeSync, GPU scaling, scaling mode, color depth, pixel format).
    /// Uses SafeHandles and re-applies current values to avoid changing user config.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class DisplaySettingsTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle? _displayServices;
        private readonly AdlxInterfaceHandle[] _displays = Array.Empty<AdlxInterfaceHandle>();

        public DisplaySettingsTests(ITestOutputHelper output)
        {
            _output = output;

            if (!HardwareDetection.HasAMDGPU(out var hwError))
            {
                _hasHardware = false;
                _hasDll = false;
                _skipReason = hwError;
                return;
            }
            _hasHardware = true;

            if (!ADLXApi.IsADLXDllAvailable(out var dllError))
            {
                _hasDll = false;
                _skipReason = dllError;
                return;
            }
            _hasDll = true;

            try
            {
                _api = ADLXApi.Initialize();
                using var system = _api.GetSystemServicesHandle();
                var dispSvc = ADLXDisplayHelpers.GetDisplayServicesHandle(system);
                _displayServices = dispSvc;
                _displays = ADLXDisplayHelpers.EnumerateAllDisplayHandles(system);
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX init/display services failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            foreach (var d in _displays)
            {
                try { d.Dispose(); } catch { }
            }
            try { _displayServices?.Dispose(); } catch { }
            _api?.Dispose();
        }

        [SkippableFact]
        public void FreeSync_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var fs = ADLXDisplaySettingsHelpers.GetFreeSyncHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetFreeSyncState(fs);
            _output.WriteLine($"FreeSync supported={state.supported}, enabled={state.enabled}");
            if (state.supported)
            {
                // Re-apply current value to avoid changing user config
                ADLXDisplaySettingsHelpers.SetFreeSyncEnabled(fs, state.enabled);
            }
        }

        [SkippableFact]
        public void GPUScaling_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var scaling = ADLXDisplaySettingsHelpers.GetGPUScalingHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetGPUScalingState(scaling);
            _output.WriteLine($"GPU scaling supported={state.supported}, enabled={state.enabled}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetGPUScalingEnabled(scaling, state.enabled);
            }
        }

        [SkippableFact]
        public void ScalingMode_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var modeHandle = ADLXDisplaySettingsHelpers.GetScalingModeHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetScalingMode(modeHandle);
            _output.WriteLine($"Scaling mode supported={state.supported}, mode={state.mode}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetScalingMode(modeHandle, state.mode);
            }
        }

        [SkippableFact]
        public void FreeSyncColorAccuracy_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var fsca = ADLXDisplaySettingsHelpers.GetFreeSyncColorAccuracyHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetFreeSyncColorAccuracyState(fsca);
            _output.WriteLine($"FS Color Accuracy supported={state.supported}, enabled={state.enabled}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetFreeSyncColorAccuracyEnabled(fsca, state.enabled);
            }
        }

        [SkippableFact]
        public void DynamicRefreshRate_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var drr = ADLXDisplaySettingsHelpers.GetDynamicRefreshRateControlHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetDynamicRefreshRateControlState(drr);
            _output.WriteLine($"DRR supported={state.supported}, enabled={state.enabled}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetDynamicRefreshRateControlEnabled(drr, state.enabled);
            }
        }

        [SkippableFact]
        public void ColorDepth_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var depth = ADLXDisplaySettingsHelpers.GetColorDepthHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetColorDepthState(depth);
            _output.WriteLine($"ColorDepth supported={state.supported}, value={state.current}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetColorDepth(depth, state.current);
            }
        }

        [SkippableFact]
        public void PixelFormat_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var format = ADLXDisplaySettingsHelpers.GetPixelFormatHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetPixelFormatState(format);
            _output.WriteLine($"PixelFormat supported={state.supported}, value={state.current}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetPixelFormat(format, state.current);
            }
        }

        [SkippableFact]
        public void VirtualSuperResolution_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var vsr = ADLXDisplaySettingsHelpers.GetVirtualSuperResolutionHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetVirtualSuperResolutionState(vsr);
            _output.WriteLine($"VSR supported={state.supported}, enabled={state.enabled}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetVirtualSuperResolutionEnabled(vsr, state.enabled);
            }
        }

        [SkippableFact]
        public void IntegerScaling_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var intScale = ADLXDisplaySettingsHelpers.GetIntegerScalingHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetIntegerScalingState(intScale);
            _output.WriteLine($"IntegerScaling supported={state.supported}, enabled={state.enabled}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetIntegerScalingEnabled(intScale, state.enabled);
            }
        }

        [SkippableFact]
        public void HDCP_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var hdcp = ADLXDisplaySettingsHelpers.GetHDCPHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetHDCPState(hdcp);
            _output.WriteLine($"HDCP supported={state.supported}, enabled={state.enabled}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetHDCPEnabled(hdcp, state.enabled);
            }
        }

        [SkippableFact]
        public void VariBright_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var vb = ADLXDisplaySettingsHelpers.GetVariBrightHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetVariBrightState(vb);
            _output.WriteLine($"VariBright supported={state.supported}, enabled={state.enabled}, mode={state.mode}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetVariBright(vb, state.enabled, state.mode);
            }
        }

        [SkippableFact]
        public void DisplayBlanking_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var blanking = ADLXDisplaySettingsHelpers.GetDisplayBlankingHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetDisplayBlankingState(blanking);
            _output.WriteLine($"DisplayBlanking supported={state.supported}, blanked={state.blanked}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetDisplayBlanked(blanking, state.blanked);
            }
        }

        [SkippableFact]
        public void CustomColor_Hue_GetAndReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var cc = ADLXDisplaySettingsHelpers.GetCustomColorHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetCustomColorHue(cc);
            _output.WriteLine($"CustomColor Hue supported={state.supported}, value={state.value}, range=[{state.range.minValue},{state.range.maxValue}] step={state.range.step}");
            if (state.supported)
            {
                ADLXDisplaySettingsHelpers.SetCustomColorHue(cc, state.value);
            }
        }

        [SkippableFact]
        public void CustomResolution_GetCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);

            var display = _displays[0];
            using var cr = ADLXDisplaySettingsHelpers.GetCustomResolutionHandle(_displayServices, display);
            var state = ADLXDisplaySettingsHelpers.GetCustomResolutionState(cr);
            _output.WriteLine($"CustomResolution supported={state.supported}, current={state.current.resWidth}x{state.current.resHeight}@{state.current.refreshRate}");
        }

        [SkippableFact]
        public void DisplayConnectivityExperience_GetState()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);
            var display = _displays[0];
            try
            {
                using var conn = ADLXDisplaySettingsHelpers.GetDisplayConnectivityExperienceHandle(_displayServices, display);
                var state = ADLXDisplaySettingsHelpers.GetDisplayConnectivityExperienceState(conn);
                _output.WriteLine($"Connectivity: HDMI QD sup={state.hdmiQdSupported} en={state.hdmiQdEnabled}, DP sup={state.dpLinkSupported}, rate={state.dpLinkRate}, lanes {state.activeLanes}/{state.totalLanes}, pre={state.preEmphasis}, volt={state.voltageSwing}, linkProtect={state.linkProtectionEnabled}");
                if (state.hdmiQdSupported)
                {
                    ADLXDisplaySettingsHelpers.SetDisplayConnectivityHDMIQualityDetectionEnabled(conn, state.hdmiQdEnabled);
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Display connectivity experience not supported on this hardware.");
            }
        }

        [SkippableFact]
        public void VariBright1_BacklightAdaptive_GetAndReapply()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displayServices == null || _displays.Length == 0, _skipReason);
            var display = _displays[0];
            try
            {
                using var vb = ADLXDisplaySettingsHelpers.GetVariBrightHandle(_displayServices, display);
                var state = ADLXDisplaySettingsHelpers.GetVariBright1State(vb);
                _output.WriteLine($"VariBright1 sup={state.supported}, BA sup={state.backlightAdaptiveSupported} en={state.backlightAdaptiveEnabled}, batteryLife sup={state.batteryLifeSupported} en={state.batteryLifeEnabled}");
                if (state.backlightAdaptiveSupported)
                {
                    ADLXDisplaySettingsHelpers.SetVariBrightBacklightAdaptiveEnabled(vb, state.backlightAdaptiveEnabled);
                }
                if (state.batteryLifeSupported)
                {
                    ADLXDisplaySettingsHelpers.SetVariBrightBatteryLifeEnabled(vb, state.batteryLifeEnabled);
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "VariBright1 not supported on this hardware.");
            }
        }
    }
}
