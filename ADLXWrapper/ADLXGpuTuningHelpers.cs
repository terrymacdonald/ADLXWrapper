using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary> 
    /// Helper methods for GPU tuning services
    /// </summary>
    public static unsafe class ADLXGPUTuningHelpers
    {
        /// <summary>
        /// Check if auto tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedAutoTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            var isSupportedFn = (delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, byte*, ADLX_RESULT>)pServices->Vtbl->IsSupportedAutoTuning;

            byte supported;
            var result = isSupportedFn(pServices, (IADLXGPU*)pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check auto tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if preset tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedPresetTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            var isSupportedFn = (delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, byte*, ADLX_RESULT>)pServices->Vtbl->IsSupportedPresetTuning;

            byte supported;
            var result = isSupportedFn(pServices, (IADLXGPU*)pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check preset tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual GFX tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedManualGFXTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            var isSupportedFn = (delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, byte*, ADLX_RESULT>)pServices->Vtbl->IsSupportedManualGFXTuning;

            byte supported;
            var result = isSupportedFn(pServices, (IADLXGPU*)pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual GFX tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual VRAM tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedManualVRAMTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            var isSupportedFn = (delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, byte*, ADLX_RESULT>)pServices->Vtbl->IsSupportedManualVRAMTuning;

            byte supported;
            var result = isSupportedFn(pServices, (IADLXGPU*)pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual VRAM tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual fan tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedManualFanTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            var isSupportedFn = (delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, byte*, ADLX_RESULT>)pServices->Vtbl->IsSupportedManualFanTuning;

            byte supported;
            var result = isSupportedFn(pServices, (IADLXGPU*)pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual fan tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual power tuning is supported for a GPU.
        /// </summary>
        public static bool IsSupportedManualPowerTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var pServices = (IADLXGPUTuningServices*)pGPUTuningServices;
            var isSupportedFn = (delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, byte*, ADLX_RESULT>)pServices->Vtbl->IsSupportedManualPowerTuning;

            byte supported;
            var result = isSupportedFn(pServices, (IADLXGPU*)pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual power tuning support");
            }

            return supported != 0;
        }

        public static ManualFanTuningInfo GetManualFanTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGpu)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            pGpuTuningServices->GetManualFanTuning(pGpu, out var pManualFanTuning);
            using var fanTuning = new ComPtr<IADLXManualFanTuning>(pManualFanTuning);
            return new ManualFanTuningInfo(fanTuning.Get());
        }

        public static void ApplyManualFanTuning(IADLXManualFanTuning* pFanTuning, ManualFanTuningInfo info)
        {
            if (pFanTuning == null) throw new ArgumentNullException(nameof(pFanTuning));
            if (!info.IsSupported) return;

            if (info.IsZeroRPMSupported) pFanTuning->SetZeroRPMState(info.ZeroRPMEnabled ? 1 : 0);
            
            if (info.FanPoints != null)
            {
                // For a robust application, clear existing points before adding new ones.
                pFanTuning->GetFanTuningStates(out var pStates);
                using var states = new ComPtr<IADLXManualFanTuningStateList>(pStates);
                states.Get()->Clear();
                pFanTuning->SetFanTuningStates(info.FanPoints.Count, info.FanPoints.ToArray());
            }
        }

        public static ManualVramTuningInfo GetManualVramTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGpu)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            pGpuTuningServices->GetManualVRAMTuning(pGpu, out var pManualVramTuning);
            using var vramTuning = new ComPtr<IADLXManualVRAMTuning>(pManualVramTuning);
            return new ManualVramTuningInfo(vramTuning.Get());
        }

        public static void ApplyManualVramTuning(IADLXManualVRAMTuning* pVramTuning, ManualVramTuningInfo info)
        {
            if (pVramTuning == null) throw new ArgumentNullException(nameof(pVramTuning));
            if (!info.IsSupported) return;

            if (info.States != null)
            {
                pVramTuning->SetVRAMTuningStates(info.States.Count, info.States.ToArray());
            }
        }

        public static ManualGfxTuningInfo GetManualGfxTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGpu)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            pGpuTuningServices->GetManualGFXTuning(pGpu, out var pManualGfxTuning);
            using var gfxTuning = new ComPtr<IADLXManualGFXTuning>(pManualGfxTuning);
            return new ManualGfxTuningInfo(gfxTuning.Get());
        }

        public static void ApplyManualGfxTuning(IADLXManualGFXTuning* pGfxTuning, ManualGfxTuningInfo info)
        {
            if (pGfxTuning == null) throw new ArgumentNullException(nameof(pGfxTuning));
            if (!info.IsSupported) return;

            if (info.States != null)
            {
                pGfxTuning->SetGFXTuningStates(info.States.Count, info.States.ToArray());
            }
        }

        public static PresetTuningInfo GetPresetTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGpu)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            pGpuTuningServices->GetPresetTuning(pGpu, out var pPresetTuning);
            using var presetTuning = new ComPtr<IADLXGPUPresetTuning>(pPresetTuning);
            return new PresetTuningInfo(presetTuning.Get());
        }

        public static void ApplyPresetTuning(IADLXGPUPresetTuning* pPresetTuning, PresetTuningInfo info)
        {
            if (pPresetTuning == null) throw new ArgumentNullException(nameof(pPresetTuning));
            if (!info.IsSupported) return;

            pPresetTuning->SetTuningPreset(info.CurrentPreset);
        }

        public static AutoTuningInfo GetAutoTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGpu)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            pGpuTuningServices->GetAutoTuning(pGpu, out var pAutoTuning);
            using var autoTuning = new ComPtr<IADLXGPUAutoTuning>(pAutoTuning);
            return new AutoTuningInfo(autoTuning.Get());
        }

        /// <summary>
        /// Gets the GPU tuning changed handling interface.
        /// </summary>
        public static IADLXGPUTuningChangedHandling* GetGpuTuningChangedHandling(IADLXGPUTuningServices* pGpuTuningServices)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            pGpuTuningServices->GetGPUTuningChangedHandling(out var pHandling);
            return pHandling;
        }

        /// <summary>
        /// Adds a GPU tuning event listener.
        /// </summary>
        public static void AddGpuTuningEventListener(IADLXGPUTuningChangedHandling* pHandling, GpuTuningListenerHandle listener)
        {
            if (pHandling == null || listener == null || listener.IsInvalid) return;
            pHandling->AddGPUTuningEventListener(listener.GetListener());
        }

        /// <summary>
        /// Removes a GPU tuning event listener.
        /// </summary>
        public static void RemoveGpuTuningEventListener(IADLXGPUTuningChangedHandling* pHandling, GpuTuningListenerHandle listener)
        {
            if (pHandling == null || listener == null || listener.IsInvalid) return;
            pHandling->RemoveGPUTuningEventListener(listener.GetListener());
        }
    }
    /// <summary>
    /// Represents the tuning capabilities for a GPU.
    /// </summary>
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

        internal unsafe GpuTuningCapabilitiesInfo(IADLXGPUTuningServices* pServices, IADLXGPU* pGpu)
        {
            AutoTuningSupported = ADLXGPUTuningHelpers.IsSupportedAutoTuning((IntPtr)pServices, (IntPtr)pGpu);
            PresetTuningSupported = ADLXGPUTuningHelpers.IsSupportedPresetTuning((IntPtr)pServices, (IntPtr)pGpu);
            ManualGFXTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualGFXTuning((IntPtr)pServices, (IntPtr)pGpu);
            ManualVRAMTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning((IntPtr)pServices, (IntPtr)pGpu);
            ManualFanTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualFanTuning((IntPtr)pServices, (IntPtr)pGpu);
            ManualPowerTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualPowerTuning((IntPtr)pServices, (IntPtr)pGpu);
        }
    }

    public readonly struct ManualFanTuningInfo
    {
        public bool IsSupported { get; init; }
        public bool IsZeroRPMSupported { get; init; }
        public bool ZeroRPMEnabled { get; init; }
        public IReadOnlyList<ADLX_ManualFanTuningState> FanPoints { get; init; }

        [JsonConstructor]
        public ManualFanTuningInfo(bool isSupported, bool isZeroRPMSupported, bool zeroRPMEnabled, IReadOnlyList<ADLX_ManualFanTuningState> fanPoints)
        {
            IsSupported = isSupported;
            IsZeroRPMSupported = isZeroRPMSupported;
            ZeroRPMEnabled = zeroRPMEnabled;
            FanPoints = fanPoints;
        }

        internal unsafe ManualFanTuningInfo(IADLXManualFanTuning* pFanTuning)
        {
            byte supported = 0;
            pFanTuning->IsSupportedZeroRPM(&supported);
            IsZeroRPMSupported = supported != 0;

            byte enabled = 0;
            if (IsZeroRPMSupported) pFanTuning->GetZeroRPMState(&enabled);
            ZeroRPMEnabled = enabled != 0;

            var points = new List<ADLX_ManualFanTuningState>();
            pFanTuning->GetFanTuningStates(out var pStates);
            using var states = new ComPtr<IADLXManualFanTuningStateList>(pStates);
            if (states.Get() != null)
            {
                for (uint i = 0; i < states.Get()->Size(); i++)
                {
                    states.Get()->At(i, out var pState);
                    using var state = new ComPtr<IADLXManualFanTuningState>(pState);
                    state.Get()->GetFanSpeed(out var speed);
                    state.Get()->GetTemperature(out var temp);
                    points.Add(new ADLX_ManualFanTuningState { fanSpeed = speed, temperature = temp });
                }
            }
            FanPoints = points;
            IsSupported = FanPoints.Count > 0 || IsZeroRPMSupported;
        }
    }

    public readonly struct ManualVramTuningInfo
    {
        public bool IsSupported { get; init; }
        public IReadOnlyList<ADLX_ManualVRAMTuningState> States { get; init; }

        [JsonConstructor]
        public ManualVramTuningInfo(bool isSupported, IReadOnlyList<ADLX_ManualVRAMTuningState> states)
        {
            IsSupported = isSupported;
            States = states;
        }

        internal unsafe ManualVramTuningInfo(IADLXManualVRAMTuning* pVramTuning)
        {
            var states = new List<ADLX_ManualVRAMTuningState>();
            pVramTuning->GetVRAMTuningStates(out var pStates);
            using var stateList = new ComPtr<IADLXManualVRAMTuningStateList>(pStates);
            if (stateList.Get() != null)
            {
                for (uint i = 0; i < stateList.Get()->Size(); i++)
                {
                    stateList.Get()->At(i, out var pState);
                    using var state = new ComPtr<IADLXManualVRAMTuningState>(pState);
                    state.Get()->GetFrequency(out var freq);
                    state.Get()->GetVoltage(out var volt);
                    states.Add(new ADLX_ManualVRAMTuningState { frequency = freq, voltage = volt });
                }
            }
            States = states;
            IsSupported = States.Count > 0;
        }
    }

    public readonly struct ManualGfxTuningInfo
    {
        public bool IsSupported { get; init; }
        public IReadOnlyList<ADLX_ManualGFXTuningState> States { get; init; }

        [JsonConstructor]
        public ManualGfxTuningInfo(bool isSupported, IReadOnlyList<ADLX_ManualGFXTuningState> states)
        {
            IsSupported = isSupported;
            States = states;
        }

        internal unsafe ManualGfxTuningInfo(IADLXManualGFXTuning* pGfxTuning)
        {
            var states = new List<ADLX_ManualGFXTuningState>();
            pGfxTuning->GetGFXTuningStates(out var pStates);
            using var stateList = new ComPtr<IADLXManualGFXTuningStateList>(pStates);
            if (stateList.Get() != null)
            {
                for (uint i = 0; i < stateList.Get()->Size(); i++)
                {
                    stateList.Get()->At(i, out var pState);
                    using var state = new ComPtr<IADLXManualGFXTuningState>(pState);
                    state.Get()->GetFrequency(out var freq);
                    state.Get()->GetVoltage(out var volt);
                    states.Add(new ADLX_ManualGFXTuningState { frequency = freq, voltage = volt });
                }
            }
            States = states;
            IsSupported = States.Count > 0;
        }
    }

    public readonly struct PresetTuningInfo
    {
        public bool IsSupported { get; init; }
        public ADLX_TUNING_PRESET CurrentPreset { get; init; }
        public IReadOnlyList<ADLX_TUNING_PRESET> SupportedPresets { get; init; }

        [JsonConstructor]
        public PresetTuningInfo(bool isSupported, ADLX_TUNING_PRESET currentPreset, IReadOnlyList<ADLX_TUNING_PRESET> supportedPresets)
        {
            IsSupported = isSupported;
            CurrentPreset = currentPreset;
            SupportedPresets = supportedPresets;
        }

        internal unsafe PresetTuningInfo(IADLXGPUPresetTuning* pPresetTuning)
        {
            byte supported = 0;
            pPresetTuning->IsSupported(&supported);
            IsSupported = supported != 0;

            pPresetTuning->IsCurrentTuningPreset(out var current);
            CurrentPreset = current;

            pPresetTuning->GetSupportedTuningPresets(out var pPresets);
            using var presetList = new ComPtr<IADLXTuningPresetList>(pPresets);
            var presets = new List<ADLX_TUNING_PRESET>();
            if (presetList.Get() != null)
            {
                for (uint i = 0; i < presetList.Get()->Size(); i++)
                {
                    presetList.Get()->At(i, out var preset);
                    presets.Add(preset);
                }
            }
            SupportedPresets = presets;
        }
    }

    public readonly struct AutoTuningInfo
    {
        public bool IsSupported { get; init; }
        // Auto-tuning is a command, not a state, so there's not much to store.

        [JsonConstructor]
        public AutoTuningInfo(bool isSupported)
        {
            IsSupported = isSupported;
        }

        internal unsafe AutoTuningInfo(IADLXGPUAutoTuning* pAutoTuning)
        {
            byte supported = 0;
            pAutoTuning->IsSupported(&supported);
            IsSupported = supported != 0;
        }
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLXGPUTuningEventListener backed by a managed delegate.
    /// </summary>
    public sealed unsafe class GpuTuningListenerHandle : SafeHandle
    {
        public delegate void OnGpuTuningChanged(IADLXGPUTuningChangedEvent* pGpuTuningChangedEvent);

        private static readonly ConcurrentDictionary<IntPtr, OnGpuTuningChanged> _map = new();
        private static readonly IntPtr _vtbl;

        private readonly GCHandle _gcHandle;

        static GpuTuningListenerHandle()
        {
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size * 2); // IUnknown + OnGPUTuningChanged
            var iunknown = new IUnknownVtbl
            {
                QueryInterface = (delegate* unmanaged[Stdcall]<IUnknown*, Guid*, void**, int>)&IUnknownVtbl.DummyQueryInterface,
                AddRef = (delegate* unmanaged[Stdcall]<IUnknown*, uint>)&IUnknownVtbl.DummyAddRef,
                Release = (delegate* unmanaged[Stdcall]<IUnknown*, uint>)&IUnknownVtbl.DummyRelease
            };
            Marshal.StructureToPtr(iunknown, _vtbl, false);
            Marshal.WriteIntPtr(_vtbl, IntPtr.Size, (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IADLXGPUTuningChangedEvent*, byte>)&OnGpuTuningChangedThunk);
        }

        private GpuTuningListenerHandle(OnGpuTuningChanged cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static GpuTuningListenerHandle Create(OnGpuTuningChanged cb) => new(cb);
        public IADLXGPUTuningEventListener* GetListener() => (IADLXGPUTuningEventListener*)handle;
        protected override bool ReleaseHandle() { _map.TryRemove(handle, out _); if (_gcHandle.IsAllocated) _gcHandle.Free(); Marshal.FreeHGlobal(handle); return true; }
        public override bool IsInvalid => handle == IntPtr.Zero;

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnGpuTuningChangedThunk(IntPtr pThis, IADLXGPUTuningChangedEvent* pGpuTuningChangedEvent)
        {
            if (_map.TryGetValue(pThis, out var cb)) { cb(pGpuTuningChangedEvent); }
            return 1;
        }
    }
}