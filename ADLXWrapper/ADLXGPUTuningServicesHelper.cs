using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLXGPUTuningServices selecting the highest available interface and exposing change handling helpers.
    /// </summary>
    public sealed unsafe class ADLXGPUTuningServicesHelper : IDisposable
    {
        private ComPtr<IADLXGPUTuningServices> _services;
        private ComPtr<IADLXGPUTuningServices1>? _services1;
        private ComPtr<IADLXGPUTuningChangedHandling>? _changedHandling;
        private bool _disposed;

        public ADLXGPUTuningServicesHelper(IADLXGPUTuningServices* services, bool addRef = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLXGPUTuningServices>(services);
            TryUpgradeServices(services);
        }

        public IADLXGPUTuningServices* GetGPUTuningServicesNative()
        {
            ThrowIfDisposed();
            return GetHighestServices();
        }

        public AdlxInterfaceHandle GetGPUTuningServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(GetGPUTuningServicesNative(), addRef: true);
        }

        public IADLXGPUTuningChangedHandling* GetGPUTuningChangedHandlingNative()
        {
            ThrowIfDisposed();
            if (_changedHandling.HasValue)
                return _changedHandling.Value.Get();

            IADLXGPUTuningChangedHandling* handling = null;
            var result = GetHighestServices()->GetGPUTuningChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU tuning change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU tuning change handling");

            _changedHandling = new ComPtr<IADLXGPUTuningChangedHandling>(handling);
            return handling;
        }

        public AdlxInterfaceHandle GetGPUTuningChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetGPUTuningChangedHandlingNative(), addRef: true);
        }

        public GpuTuningCapabilitiesInfo GetCapabilities(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));
            return new GpuTuningCapabilitiesInfo(GetHighestServices(), gpu);
        }

        public bool IsAutoTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedAutoTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Auto tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query auto tuning support");
            return supported;
        }

        public bool IsPresetTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedPresetTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Preset tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query preset tuning support");
            return supported;
        }

        public bool IsManualGfxTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedManualGFXTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual GFX tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query manual GFX tuning support");
            return supported;
        }

        public bool IsManualVramTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedManualVRAMTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual VRAM tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query manual VRAM tuning support");
            return supported;
        }

        public bool IsManualFanTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedManualFanTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual fan tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query manual fan tuning support");
            return supported;
        }

        public bool IsManualPowerTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedManualPowerTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual power tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query manual power tuning support");
            return supported;
        }

        public ManualFanTuningInfo GetManualFanTuning(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXInterface* manualFan = null;
            var result = GetHighestServices()->GetManualFanTuning(gpu, &manualFan);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || manualFan == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual fan tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get manual fan tuning interface");
            using var fan = new ComPtr<IADLXManualFanTuning>((IADLXManualFanTuning*)manualFan);
            return new ManualFanTuningInfo(fan.Get());
        }

        public ManualVramTuningInfo GetManualVramTuning(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXInterface* manualVram = null;
            var result = GetHighestServices()->GetManualVRAMTuning(gpu, &manualVram);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || manualVram == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual VRAM tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get manual VRAM tuning interface");
            using var vram = new ComPtr<IADLXManualVRAMTuning1>((IADLXManualVRAMTuning1*)manualVram);
            return new ManualVramTuningInfo(vram.Get());
        }

        public ManualGfxTuningInfo GetManualGfxTuning(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXInterface* manualGfx = null;
            var result = GetHighestServices()->GetManualGFXTuning(gpu, &manualGfx);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || manualGfx == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual GFX tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get manual GFX tuning interface");
            using var gfx = new ComPtr<IADLXManualGraphicsTuning2>((IADLXManualGraphicsTuning2*)manualGfx);
            return new ManualGfxTuningInfo(gfx.Get());
        }

        public PresetTuningInfo GetPresetTuning(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXInterface* preset = null;
            var result = GetHighestServices()->GetPresetTuning(gpu, &preset);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || preset == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Preset tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get preset tuning interface");
            using var tuning = new ComPtr<IADLXGPUPresetTuning>((IADLXGPUPresetTuning*)preset);
            return new PresetTuningInfo(tuning.Get());
        }

        public void Dispose()
        {
            if (_disposed) return;
            _changedHandling?.Dispose();
            _services1?.Dispose();
            _services.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ADLXGPUTuningServicesHelper));
        }

        ~ADLXGPUTuningServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }

        private void TryUpgradeServices(IADLXGPUTuningServices* services)
        {
            if (services == null) return;

            if (ADLXUtils.TryQueryInterface((IntPtr)services, nameof(IADLXGPUTuningServices1), out var p1))
            {
                _services1 = new ComPtr<IADLXGPUTuningServices1>((IADLXGPUTuningServices1*)p1);
            }
        }

        private IADLXGPUTuningServices* GetHighestServices()
        {
            if (_services1.HasValue)
                return (IADLXGPUTuningServices*)_services1.Value.Get();
            return _services.Get();
        }
    }

    public readonly struct GpuTuningCapabilitiesInfo
    {
        public bool AutoTuningSupported { get; init; }
        public bool PresetTuningSupported { get; init; }
        public bool ManualGFXTuningSupported { get; init; }
        public bool ManualVRAMTuningSupported { get; init; }
        public bool ManualFanTuningSupported { get; init; }
        public bool ManualPowerTuningSupported { get; init; }

        [JsonConstructor]
        public GpuTuningCapabilitiesInfo(bool autoTuningSupported, bool presetTuningSupported, bool manualGFXTuningSupported, bool manualVRAMTuningSupported, bool manualFanTuningSupported, bool manualPowerTuningSupported)
        {
            AutoTuningSupported = autoTuningSupported;
            PresetTuningSupported = presetTuningSupported;
            ManualGFXTuningSupported = manualGFXTuningSupported;
            ManualVRAMTuningSupported = manualVRAMTuningSupported;
            ManualFanTuningSupported = manualFanTuningSupported;
            ManualPowerTuningSupported = manualPowerTuningSupported;
        }

        public unsafe GpuTuningCapabilitiesInfo(IADLXGPUTuningServices* services, IADLXGPU* gpu)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported;
            services->IsSupportedAutoTuning(gpu, &supported);
            AutoTuningSupported = supported;
            services->IsSupportedPresetTuning(gpu, &supported);
            PresetTuningSupported = supported;
            services->IsSupportedManualGFXTuning(gpu, &supported);
            ManualGFXTuningSupported = supported;
            services->IsSupportedManualVRAMTuning(gpu, &supported);
            ManualVRAMTuningSupported = supported;
            services->IsSupportedManualFanTuning(gpu, &supported);
            ManualFanTuningSupported = supported;
            services->IsSupportedManualPowerTuning(gpu, &supported);
            ManualPowerTuningSupported = supported;
        }
    }

    public readonly struct ManualFanTuningInfo
    {
        public bool IsSupported { get; init; }
        public bool IsZeroRPMSupported { get; init; }
        public bool ZeroRPMEnabled { get; init; }
        public IReadOnlyList<FanPoint> FanPoints { get; init; }

        [JsonConstructor]
        public ManualFanTuningInfo(bool isSupported, bool isZeroRPMSupported, bool zeroRPMEnabled, IReadOnlyList<FanPoint> fanPoints)
        {
            IsSupported = isSupported;
            IsZeroRPMSupported = isZeroRPMSupported;
            ZeroRPMEnabled = zeroRPMEnabled;
            FanPoints = fanPoints;
        }

        internal unsafe ManualFanTuningInfo(IADLXManualFanTuning* fanTuning)
        {
            bool supported = false;
            fanTuning->IsSupportedZeroRPM(&supported);
            IsZeroRPMSupported = supported;

            bool enabled = false;
            if (IsZeroRPMSupported) fanTuning->GetZeroRPMState(&enabled);
            ZeroRPMEnabled = enabled;

            var points = new List<FanPoint>();
            IADLXManualFanTuningStateList* statesPtr;
            fanTuning->GetFanTuningStates(&statesPtr);
            using var states = new ComPtr<IADLXManualFanTuningStateList>(statesPtr);
            if (states.Get() != null)
            {
                for (uint i = 0; i < states.Get()->Size(); i++)
                {
                    IADLXManualFanTuningState* statePtr;
                    states.Get()->At(i, &statePtr);
                    using var state = new ComPtr<IADLXManualFanTuningState>(statePtr);
                    int speed = 0, temp = 0;
                    state.Get()->GetFanSpeed(&speed);
                    state.Get()->GetTemperature(&temp);
                    points.Add(new FanPoint { FanSpeed = speed, Temperature = temp });
                }
            }
            FanPoints = points;
            IsSupported = FanPoints.Count > 0 || IsZeroRPMSupported;
        }
    }

    public readonly struct FanPoint
    {
        public int FanSpeed { get; init; }
        public int Temperature { get; init; }
    }

    public readonly struct ManualVramTuningInfo
    {
        public bool IsSupported { get; init; }
        public IReadOnlyList<VramState> States { get; init; }

        [JsonConstructor]
        public ManualVramTuningInfo(bool isSupported, IReadOnlyList<VramState> states)
        {
            IsSupported = isSupported;
            States = states;
        }

        internal unsafe ManualVramTuningInfo(IADLXManualVRAMTuning1* vramTuning)
        {
            var states = new List<VramState>();
            IADLXManualTuningStateList* stateListPtr;
            vramTuning->GetVRAMTuningStates(&stateListPtr);
            using var stateList = new ComPtr<IADLXManualTuningStateList>(stateListPtr);
            if (stateList.Get() != null)
            {
                for (uint i = 0; i < stateList.Get()->Size(); i++)
                {
                    IADLXManualTuningState* statePtr;
                    stateList.Get()->At(i, &statePtr);
                    using var state = new ComPtr<IADLXManualTuningState>(statePtr);
                    int freq = 0, volt = 0;
                    state.Get()->GetFrequency(&freq);
                    state.Get()->GetVoltage(&volt);
                    states.Add(new VramState { Frequency = freq, Voltage = volt });
                }
            }
            States = states;
            IsSupported = States.Count > 0;
        }
    }

    public readonly struct VramState
    {
        public int Frequency { get; init; }
        public int Voltage { get; init; }
    }

    public readonly struct ManualGfxTuningInfo
    {
        public bool IsSupported { get; init; }
        public int? MinFrequency { get; init; }
        public int? MaxFrequency { get; init; }
        public int? Voltage { get; init; }

        [JsonConstructor]
        public ManualGfxTuningInfo(bool isSupported, int? minFrequency, int? maxFrequency, int? voltage)
        {
            IsSupported = isSupported;
            MinFrequency = minFrequency;
            MaxFrequency = maxFrequency;
            Voltage = voltage;
        }

        internal unsafe ManualGfxTuningInfo(IADLXManualGraphicsTuning2* gfxTuning)
        {
            int minFreq = 0, maxFreq = 0, volt = 0;
            gfxTuning->GetGPUMinFrequency(&minFreq);
            gfxTuning->GetGPUMaxFrequency(&maxFreq);
            gfxTuning->GetGPUVoltage(&volt);
            MinFrequency = minFreq;
            MaxFrequency = maxFreq;
            Voltage = volt;
            IsSupported = true;
        }
    }

    public readonly struct PresetTuningInfo
    {
        public bool IsSupported { get; init; }
        public PresetKind CurrentPreset { get; init; }
        public IReadOnlyList<PresetKind> SupportedPresets { get; init; }

        [JsonConstructor]
        public PresetTuningInfo(bool isSupported, PresetKind currentPreset, IReadOnlyList<PresetKind> supportedPresets)
        {
            IsSupported = isSupported;
            CurrentPreset = currentPreset;
            SupportedPresets = supportedPresets;
        }

        internal unsafe PresetTuningInfo(IADLXGPUPresetTuning* presetTuning)
        {
            var supported = new List<PresetKind>();
            bool flag;
            if (presetTuning->IsSupportedPowerSaver(&flag) == ADLX_RESULT.ADLX_OK && flag) supported.Add(PresetKind.PowerSaver);
            if (presetTuning->IsSupportedQuiet(&flag) == ADLX_RESULT.ADLX_OK && flag) supported.Add(PresetKind.Quiet);
            if (presetTuning->IsSupportedBalanced(&flag) == ADLX_RESULT.ADLX_OK && flag) supported.Add(PresetKind.Balanced);
            if (presetTuning->IsSupportedTurbo(&flag) == ADLX_RESULT.ADLX_OK && flag) supported.Add(PresetKind.Turbo);
            if (presetTuning->IsSupportedRage(&flag) == ADLX_RESULT.ADLX_OK && flag) supported.Add(PresetKind.Rage);
            SupportedPresets = supported;
            IsSupported = supported.Count > 0;

            bool curPower = false, curQuiet = false, curBalanced = false, curTurbo = false, curRage = false;
            if (presetTuning->IsCurrentPowerSaver(&curPower) == ADLX_RESULT.ADLX_OK && curPower) { CurrentPreset = PresetKind.PowerSaver; return; }
            if (presetTuning->IsCurrentQuiet(&curQuiet) == ADLX_RESULT.ADLX_OK && curQuiet) { CurrentPreset = PresetKind.Quiet; return; }
            if (presetTuning->IsCurrentBalanced(&curBalanced) == ADLX_RESULT.ADLX_OK && curBalanced) { CurrentPreset = PresetKind.Balanced; return; }
            if (presetTuning->IsCurrentTurbo(&curTurbo) == ADLX_RESULT.ADLX_OK && curTurbo) { CurrentPreset = PresetKind.Turbo; return; }
            if (presetTuning->IsCurrentRage(&curRage) == ADLX_RESULT.ADLX_OK && curRage) { CurrentPreset = PresetKind.Rage; return; }

            CurrentPreset = supported.Count > 0 ? supported[0] : PresetKind.Balanced;
        }
    }

    public enum PresetKind
    {
        PowerSaver,
        Quiet,
        Balanced,
        Turbo,
        Rage
    }

    public readonly struct AutoTuningInfo
    {
        public bool IsSupported { get; init; }

        [JsonConstructor]
        public AutoTuningInfo(bool isSupported)
        {
            IsSupported = isSupported;
        }

        internal unsafe AutoTuningInfo(IADLXGPUAutoTuning* autoTuning)
        {
            bool supUnder = false, supOcGpu = false, supOcVram = false;
            autoTuning->IsSupportedUndervoltGPU(&supUnder);
            autoTuning->IsSupportedOverclockGPU(&supOcGpu);
            autoTuning->IsSupportedOverclockVRAM(&supOcVram);
            IsSupported = supUnder || supOcGpu || supOcVram;
        }
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLXGPUTuningEventListener backed by a managed delegate.
    /// </summary>
    public sealed unsafe class GpuTuningListenerHandle : SafeHandle
    {
        private GpuTuningListenerHandle() : base(IntPtr.Zero, true) { handle = IntPtr.Zero; }
        public static GpuTuningListenerHandle Create(delegate* unmanaged<void> _ = null) => new();
        public IntPtr GetListener() => IntPtr.Zero;
        protected override bool ReleaseHandle() => true;
        public override bool IsInvalid => handle == IntPtr.Zero;
    }
}

