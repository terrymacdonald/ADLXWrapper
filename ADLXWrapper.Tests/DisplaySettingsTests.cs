using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Smoke tests for display settings slice (FreeSync, GPU scaling, scaling mode).
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
    }
}
