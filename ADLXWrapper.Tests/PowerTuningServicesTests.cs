using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Power tuning services tests (SmartShift Max/Eco).
    /// Reads and reapplies current values to avoid altering user config.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class PowerTuningServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle[] _gpus = Array.Empty<AdlxInterfaceHandle>();
        private readonly AdlxInterfaceHandle? _pPowerServices;

        public PowerTuningServicesTests(ITestOutputHelper output)
        {
            _output = output;
            _pPowerServices = null;

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
                _pPowerServices = ADLXPowerTuningHelpers.GetPowerTuningServices(system);
                _gpus = _api.EnumerateGPUHandles();
            }
            catch (Exception ex)
            {
                _skipReason = $"Power tuning init failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            try { _pPowerServices?.Dispose(); } catch { }
            foreach (var g in _gpus)
            {
                try { g.Dispose(); } catch { }
            }
            _api?.Dispose();
        }

        [SkippableFact]
        public void SmartShiftMax_ReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPowerServices == null, _skipReason);
            try
            {
                using var max = ADLXPowerTuningHelpers.GetSmartShiftMax((IntPtr)_pPowerServices!);
                var state = ADLXPowerTuningHelpers.GetSmartShiftMaxState(max);
                _output.WriteLine($"SmartShiftMax sup={state.supported}, mode={state.biasMode}, range=({state.biasRange.minValue},{state.biasRange.maxValue}), bias={state.biasValue}");
                if (state.supported)
                {
                    var clamped = Math.Clamp(state.biasValue, state.biasRange.minValue, state.biasRange.maxValue);
                    ADLXPowerTuningHelpers.SetSmartShiftMaxBias(max, state.biasMode, clamped);
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "SmartShift Max not supported on this hardware.");
            }
        }

        [SkippableFact]
        public void SmartShiftEco_ReapplyCurrent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _pPowerServices == null, _skipReason);
            try
            {
                using var eco = ADLXPowerTuningHelpers.GetSmartShiftEco((IntPtr)_pPowerServices!);
                var state = ADLXPowerTuningHelpers.GetSmartShiftEcoState(eco);
                _output.WriteLine($"SmartShiftEco sup={state.supported}, en={state.enabled}");
                if (state.supported)
                {
                    ADLXPowerTuningHelpers.SetSmartShiftEcoEnabled(eco, state.enabled);
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "SmartShift Eco not supported on this hardware.");
            }
        }
    }
}
