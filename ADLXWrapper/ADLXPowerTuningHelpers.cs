using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for power tuning (manual power limit, SmartShift Max/Eco).
    /// </summary>
    public static unsafe partial class ADLXPowerTuningHelpers
    {
        /// <summary>
        /// Gets the IADLXPowerTuningServices interface from the system services. Callers must dispose the returned pointer.
        /// </summary>
        public static IADLXPowerTuningServices* GetPowerTuningServices(IADLXSystem* pSystem)
        {
            if (pSystem == null) throw new ArgumentNullException(nameof(pSystem));

            if (!ADLXUtils.TryQueryInterface((IntPtr)pSystem, nameof(IADLXSystem2), out var pSystem2))
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Power tuning services require IADLXSystem2");

            using var system2 = new ComPtr<IADLXSystem2>((IADLXSystem2*)pSystem2);
            IADLXPowerTuningServices* pServices = null;
            var result = system2.Get()->GetPowerTuningServices(&pServices);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pServices == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Power tuning services not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get power tuning services");
            return pServices;
        }

        /// <summary>
        /// Gets the SmartShift Max information.
        /// </summary>
        public static SmartShiftMaxInfo GetSmartShiftMax(IADLXPowerTuningServices* pPowerServices)
        {
            if (pPowerServices == null) throw new ArgumentNullException(nameof(pPowerServices));

            IADLXSmartShiftMax* pMax;
            var result = pPowerServices->GetSmartShiftMax(&pMax);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pMax == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "SmartShift Max not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get SmartShift Max interface");

            using var smartShiftMax = new ComPtr<IADLXSmartShiftMax>(pMax);
            return new SmartShiftMaxInfo(smartShiftMax.Get());
        }

        /// <summary>
        /// Applies the settings from a SmartShiftMaxInfo object to the hardware.
        /// </summary>
        public static void ApplySmartShiftMax(IADLXSmartShiftMax* pSmartShiftMax, SmartShiftMaxInfo info)
        {
            if (pSmartShiftMax == null) throw new ArgumentNullException(nameof(pSmartShiftMax));
            if (!info.IsSupported) return;

            // Set mode first, then bias value
            SetSmartShiftMaxBias(pSmartShiftMax, info.BiasMode, info.BiasValue);
        }

        /// <summary>
        /// Gets the SmartShift Eco information.
        /// </summary>
        public static SmartShiftEcoInfo GetSmartShiftEco(IADLXPowerTuningServices* pPowerServices)
        {
            if (pPowerServices == null) throw new ArgumentNullException(nameof(pPowerServices));

            // IADLXPowerTuningServices1 is required for GetSmartShiftEco
            if (!ADLXUtils.TryQueryInterface((IntPtr)pPowerServices, nameof(IADLXPowerTuningServices1), out var pPowerServices1))
            {
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "SmartShift Eco not supported by this ADLX system");
            }

            using var powerServices1 = new ComPtr<IADLXPowerTuningServices1>((IADLXPowerTuningServices1*)pPowerServices1);
            IADLXSmartShiftEco* pEco;
            var result = powerServices1.Get()->GetSmartShiftEco(&pEco);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pEco == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "SmartShift Eco not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get SmartShift Eco interface");

            using var smartShiftEco = new ComPtr<IADLXSmartShiftEco>(pEco);
            return new SmartShiftEcoInfo(smartShiftEco.Get());
        }

        /// <summary>
        /// Applies the settings from a SmartShiftEcoInfo object to the hardware.
        /// </summary>
        public static void ApplySmartShiftEco(IADLXSmartShiftEco* pSmartShiftEco, SmartShiftEcoInfo info)
        {
            if (pSmartShiftEco == null) throw new ArgumentNullException(nameof(pSmartShiftEco));
            if (!info.IsSupported) return;

            SetSmartShiftEcoEnabled(pSmartShiftEco, info.IsEnabled);
        }

        /// <summary>
        /// Sets the SmartShift Max bias mode and value.
        /// </summary>
        public static void SetSmartShiftMaxBias(IADLXSmartShiftMax* pSmartShiftMax, ADLX_SSM_BIAS_MODE mode, int bias)
        {
            if (pSmartShiftMax == null) throw new ArgumentNullException(nameof(pSmartShiftMax));

            var modeResult = pSmartShiftMax->SetBiasMode(mode);
            if (modeResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(modeResult, "Failed to set SmartShift Max bias mode");

            var biasResult = pSmartShiftMax->SetBias(bias);
            if (biasResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(biasResult, "Failed to set SmartShift Max bias");
        }

        /// <summary>
        /// Sets the enabled state of SmartShift Eco.
        /// </summary>
        public static void SetSmartShiftEcoEnabled(IADLXSmartShiftEco* pSmartShiftEco, bool enable)
        {
            if (pSmartShiftEco == null) throw new ArgumentNullException(nameof(pSmartShiftEco));

            var result = pSmartShiftEco->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set SmartShift Eco");
        }

        /// <summary>
        /// Gets the Manual Power Tuning information for a specific GPU.
        /// </summary>
        public static ManualPowerTuningInfo GetManualPowerTuning(IADLXGPUTuningServices* pGpuTuningServices, IADLXGPU* pGPU)
        {
            if (pGpuTuningServices == null) throw new ArgumentNullException(nameof(pGpuTuningServices));
            if (pGPU == null) throw new ArgumentNullException(nameof(pGPU));

            IADLXInterface* pManual = null;
            var result = pGpuTuningServices->GetManualPowerTuning(pGPU, &pManual);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pManual == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual power tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get manual power tuning interface");

            using var manualPowerTuning = new ComPtr<IADLXManualPowerTuning>((IADLXManualPowerTuning*)pManual);
            return new ManualPowerTuningInfo(manualPowerTuning.Get());
        }

        /// <summary>
        /// Applies the settings from a ManualPowerTuningInfo object to the hardware.
        /// </summary>
        public static void ApplyManualPowerTuning(IADLXManualPowerTuning* pManualPower, ManualPowerTuningInfo info)
        {
            if (pManualPower == null) throw new ArgumentNullException(nameof(pManualPower));

            if (info.PowerLimitSupported)
            {
                SetManualPowerLimit(pManualPower, info.PowerLimitValue);
            }

            if (info.TdcLimitSupported)
            {
                if (ADLXUtils.TryQueryInterface((IntPtr)pManualPower, nameof(IADLXManualPowerTuning1), out var pManualPower1))
                {
                    using var manualPower1 = new ComPtr<IADLXManualPowerTuning1>((IADLXManualPowerTuning1*)pManualPower1);
                    SetManualTDCLimit(manualPower1.Get(), info.TdcLimitValue);
                }
            }
        }

        /// <summary>
        /// Sets the manual power limit.
        /// </summary>
        public static void SetManualPowerLimit(IADLXManualPowerTuning* pManualPower, int value)
        {
            if (pManualPower == null) throw new ArgumentNullException(nameof(pManualPower));

            var result = pManualPower->SetPowerLimit(value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set manual power limit");
        }

        /// <summary>
        /// Sets the manual TDC limit.
        /// </summary>
        public static void SetManualTDCLimit(IADLXManualPowerTuning1* pManualPowerV1, int value)
        {
            if (pManualPowerV1 == null) throw new ArgumentNullException(nameof(pManualPowerV1));

            var result = pManualPowerV1->SetTDCLimit(value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set TDC limit");
        }

        public static IADLXPowerTuningChangedHandling* GetPowerTuningChangedHandling(IADLXPowerTuningServices* pPowerServices)
        {
            if (pPowerServices == null) throw new ArgumentNullException(nameof(pPowerServices));

            IADLXPowerTuningChangedHandling* pHandling = null;
            var result = pPowerServices->GetPowerTuningChangedHandling(&pHandling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pHandling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Power tuning change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get power tuning change handling");

            return pHandling;
        }

        public static void AddPowerTuningEventListener(IADLXPowerTuningChangedHandling* pHandling, PowerTuningListenerHandle listener)
        {
            if (pHandling == null || listener == null || listener.IsInvalid) return;
            pHandling->AddPowerTuningEventListener(listener.GetListener());
        }

        public static void RemovePowerTuningEventListener(IADLXPowerTuningChangedHandling* pHandling, PowerTuningListenerHandle listener)
        {
            if (pHandling == null || listener == null || listener.IsInvalid) return;
            pHandling->RemovePowerTuningEventListener(listener.GetListener());
        }

        /// <summary>
        /// Checks whether GPUConnect controls are supported via power tuning services.
        /// </summary>
        public static bool IsGPUConnectSupported(IADLXPowerTuningServices* pPowerServices)
        {
            if (pPowerServices == null) throw new ArgumentNullException(nameof(pPowerServices));

            if (!ADLXUtils.TryQueryInterface((IntPtr)pPowerServices, nameof(IADLXPowerTuningServices1), out var pServices1))
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUConnect not supported by this ADLX system");

            using var services1 = new ComPtr<IADLXPowerTuningServices1>((IADLXPowerTuningServices1*)pServices1);
            bool supported = false;
            var result = services1.Get()->IsGPUConnectSupported(&supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUConnect not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query GPUConnect support");

            return supported;
        }

        /// <summary>
        /// Enumerates GPUConnect-capable GPUs, returning AddRef'd handles.
        /// </summary>
        public static AdlxInterfaceHandle[] EnumerateGPUConnectGpuHandles(IADLXPowerTuningServices* pPowerServices)
        {
            if (pPowerServices == null) throw new ArgumentNullException(nameof(pPowerServices));

            if (!ADLXUtils.TryQueryInterface((IntPtr)pPowerServices, nameof(IADLXPowerTuningServices1), out var pServices1))
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUConnect not supported by this ADLX system");

            using var services1 = new ComPtr<IADLXPowerTuningServices1>((IADLXPowerTuningServices1*)pServices1);
            IADLXGPU2List* pList = null;
            var result = services1.Get()->GetGPUConnectGPUs(&pList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUConnect GPU enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate GPUConnect-capable GPUs");

            using var list = new ComPtr<IADLXGPU2List>(pList);
            var count = list.Get()->Size();
            var handles = new AdlxInterfaceHandle[count];
            for (uint i = 0; i < count; i++)
            {
                IADLXGPU2* gpu = null;
                list.Get()->At(i, &gpu);
                handles[i] = AdlxInterfaceHandle.From(gpu, addRef: false);
            }

            return handles;
        }
    }

    /// <summary>
    /// Represents the collected information for SmartShift Max.
    /// </summary>
    public readonly struct SmartShiftMaxInfo
    {
        public bool IsSupported { get; init; }
        public ADLX_SSM_BIAS_MODE BiasMode { get; init; }
        public ADLX_IntRange BiasRange { get; init; }
        public int BiasValue { get; init; }

        [JsonConstructor]
        public SmartShiftMaxInfo(bool isSupported, ADLX_SSM_BIAS_MODE biasMode, ADLX_IntRange biasRange, int biasValue)
        {
            IsSupported = isSupported;
            BiasMode = biasMode;
            BiasRange = biasRange;
            BiasValue = biasValue;
        }

        internal unsafe SmartShiftMaxInfo(IADLXSmartShiftMax* pSmartShiftMax)
        {
            bool supported = false;
            pSmartShiftMax->IsSupported(&supported);
            IsSupported = supported;

            ADLX_SSM_BIAS_MODE mode = default;
            pSmartShiftMax->GetBiasMode(&mode);
            BiasMode = mode;

            ADLX_IntRange range = default;
            pSmartShiftMax->GetBiasRange(&range);
            BiasRange = range;

            int bias = 0;
            pSmartShiftMax->GetBias(&bias);
            BiasValue = bias;
        }
    }

    /// <summary>
    /// Represents the collected information for SmartShift Eco.
    /// </summary>
    public readonly struct SmartShiftEcoInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public SmartShiftEcoInfo(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }

        internal unsafe SmartShiftEcoInfo(IADLXSmartShiftEco* pSmartShiftEco)
        {
            bool supported = false, enabled = false;
            pSmartShiftEco->IsSupported(&supported);
            pSmartShiftEco->IsEnabled(&enabled);
            IsSupported = supported;
            IsEnabled = enabled;
        }
    }

    /// <summary>
    /// Represents the collected information for Manual Power Tuning.
    /// </summary>
    public readonly struct ManualPowerTuningInfo
    {
        public bool PowerLimitSupported { get; init; }
        public ADLX_IntRange PowerLimitRange { get; init; }
        public int PowerLimitValue { get; init; }
        public bool TdcLimitSupported { get; init; }
        public ADLX_IntRange TdcLimitRange { get; init; }
        public int TdcLimitValue { get; init; }
        public int TdcLimitDefaultValue { get; init; }

        [JsonConstructor]
        public ManualPowerTuningInfo(bool powerLimitSupported, ADLX_IntRange powerLimitRange, int powerLimitValue, bool tdcLimitSupported, ADLX_IntRange tdcLimitRange, int tdcLimitValue, int tdcLimitDefaultValue)
        {
            PowerLimitSupported = powerLimitSupported;
            PowerLimitRange = powerLimitRange;
            PowerLimitValue = powerLimitValue;
            TdcLimitSupported = tdcLimitSupported;
            TdcLimitRange = tdcLimitRange;
            TdcLimitValue = tdcLimitValue;
            TdcLimitDefaultValue = tdcLimitDefaultValue;
        }

        internal unsafe ManualPowerTuningInfo(IADLXManualPowerTuning* pManualPower)
        {
            ADLX_IntRange powerRange = default;
            int powerValue = 0;
            var r1 = pManualPower->GetPowerLimitRange(&powerRange);
            var r2 = pManualPower->GetPowerLimit(&powerValue);
            PowerLimitSupported = r1 == ADLX_RESULT.ADLX_OK && r2 == ADLX_RESULT.ADLX_OK;
            PowerLimitRange = powerRange;
            PowerLimitValue = powerValue;

            if (ADLXUtils.TryQueryInterface((IntPtr)pManualPower, nameof(IADLXManualPowerTuning1), out var pManualPower1))
            {
                using var manualPower1 = new ComPtr<IADLXManualPowerTuning1>((IADLXManualPowerTuning1*)pManualPower1);
                bool tdcSupported = false;
                manualPower1.Get()->IsSupportedTDCLimit(&tdcSupported);
                TdcLimitSupported = tdcSupported;

                ADLX_IntRange tdcRange = default;
                int tdcValue = 0, tdcDefault = 0;
                manualPower1.Get()->GetTDCLimitRange(&tdcRange);
                manualPower1.Get()->GetTDCLimit(&tdcValue);
                manualPower1.Get()->GetTDCLimitDefault(&tdcDefault);
                TdcLimitRange = tdcRange;
                TdcLimitValue = tdcValue;
                TdcLimitDefaultValue = tdcDefault;
            }
            else
            {
                TdcLimitSupported = false;
                TdcLimitRange = default;
                TdcLimitValue = 0;
                TdcLimitDefaultValue = 0;
            }
        }
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLXPowerTuningChangedListener backed by a managed delegate.
    /// Callbacks arrive on ADLX threads; the handle roots the delegate until disposed or explicitly removed.
    /// </summary>
    public sealed unsafe class PowerTuningListenerHandle : SafeHandle
    {
        public delegate bool PowerTuningChangedCallback(IntPtr pEvent);

        private static readonly ConcurrentDictionary<IntPtr, PowerTuningChangedCallback> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnPowerTuningChanged;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private PowerTuningListenerHandle(PowerTuningChangedCallback cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static PowerTuningListenerHandle Create(PowerTuningChangedCallback cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new PowerTuningListenerHandle(cb);
        }

        public IADLXPowerTuningChangedListener* GetListener() => (IADLXPowerTuningChangedListener*)handle;

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnPowerTuningChanged(IntPtr pThis, IntPtr pEvent)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb(pEvent) ? (byte)1 : (byte)0;
            }
            return 0;
        }
    }
}
