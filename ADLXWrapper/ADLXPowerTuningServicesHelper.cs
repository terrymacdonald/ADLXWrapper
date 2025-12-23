using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLXPowerTuningServices selecting the highest available interface (if extended) and exposing change handling.
    /// </summary>
    public sealed unsafe class ADLXPowerTuningServicesHelper : IDisposable
    {
        private ComPtr<IADLXPowerTuningServices> _services;
        private ComPtr<IADLXPowerTuningServices1>? _services1;
        private ComPtr<IADLXPowerTuningChangedHandling>? _changedHandling;
        private bool _disposed;

        public ADLXPowerTuningServicesHelper(IADLXPowerTuningServices* services, bool addRef = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLXPowerTuningServices>(services);
            TryUpgradeServices(services);
        }

        public IADLXPowerTuningServices* GetPowerTuningServicesNative()
        {
            ThrowIfDisposed();
            return GetHighestServices();
        }

        public AdlxInterfaceHandle GetPowerTuningServicesHandle()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(GetPowerTuningServicesNative(), addRef: true);
        }

        public bool IsGPUConnectSupported()
        {
            ThrowIfDisposed();
            var services1 = GetPowerTuningServices1();
            bool supported = false;
            var result = services1->IsGPUConnectSupported(&supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUConnect not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query GPUConnect support");

            return supported;
        }

        public AdlxInterfaceHandle[] EnumerateGPUConnectGpuHandles()
        {
            ThrowIfDisposed();
            var services1 = GetPowerTuningServices1();

            IADLXGPU2List* pList = null;
            var result = services1->GetGPUConnectGPUs(&pList);
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

        public IADLXGPU2List* GetGPUConnectGpuListNative()
        {
            ThrowIfDisposed();
            var services1 = GetPowerTuningServices1();

            IADLXGPU2List* pList = null;
            var result = services1->GetGPUConnectGPUs(&pList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUConnect GPU enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate GPUConnect-capable GPUs");

            return pList; // caller must wrap in ComPtr and dispose
        }

        public IADLXPowerTuningChangedHandling* GetPowerTuningChangedHandlingNative()
        {
            ThrowIfDisposed();
            if (_changedHandling.HasValue)
                return _changedHandling.Value.Get();

            IADLXPowerTuningChangedHandling* handling = null;
            var result = GetHighestServices()->GetPowerTuningChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Power tuning change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get power tuning change handling");

            _changedHandling = new ComPtr<IADLXPowerTuningChangedHandling>(handling);
            return handling;
        }

        public AdlxInterfaceHandle GetPowerTuningChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetPowerTuningChangedHandlingNative(), addRef: true);
        }

        public PowerTuningListenerHandle AddPowerTuningEventListener(PowerTuningListenerHandle.PowerTuningChangedCallback callback)
        {
            ThrowIfDisposed();
            if (callback == null) throw new ArgumentNullException(nameof(callback));

            var handling = GetPowerTuningChangedHandlingNative();
            var handle = PowerTuningListenerHandle.Create(callback);
            var result = handling->AddPowerTuningEventListener(handle.GetListener());
            if (result != ADLX_RESULT.ADLX_OK)
            {
                handle.Dispose();
                throw new ADLXException(result, "Failed to add power tuning event listener");
            }

            return handle;
        }

        public void RemovePowerTuningEventListener(PowerTuningListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid) return;

            var handling = GetPowerTuningChangedHandlingNative();
            handling->RemovePowerTuningEventListener(handle.GetListener());

            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        public SmartShiftMaxInfo GetSmartShiftMax()
        {
            ThrowIfDisposed();
            using var ssm = new ComPtr<IADLXSmartShiftMax>(GetSmartShiftMaxNative());
            return new SmartShiftMaxInfo(ssm.Get());
        }

        public IADLXSmartShiftMax* GetSmartShiftMaxNative()
        {
            ThrowIfDisposed();
            IADLXSmartShiftMax* ssm = null;
            var result = GetHighestServices()->GetSmartShiftMax(&ssm);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || ssm == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "SmartShift Max not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get SmartShift Max interface");
            return ssm; // caller wraps/disposes
        }

        public void ApplySmartShiftMax(SmartShiftMaxInfo info)
        {
            ThrowIfDisposed();
            using var ssm = new ComPtr<IADLXSmartShiftMax>(GetSmartShiftMaxNative());
            if (!info.IsSupported) return;
            SetSmartShiftMaxBias(ssm.Get(), info.BiasMode, info.BiasValue);
        }

        public SmartShiftEcoInfo GetSmartShiftEco()
        {
            ThrowIfDisposed();
            var services1 = GetPowerTuningServices1();
            IADLXSmartShiftEco* eco = null;
            var result = services1->GetSmartShiftEco(&eco);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || eco == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "SmartShift Eco not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get SmartShift Eco interface");
            using var ecoPtr = new ComPtr<IADLXSmartShiftEco>(eco);
            return new SmartShiftEcoInfo(ecoPtr.Get());
        }

        public void ApplySmartShiftEco(SmartShiftEcoInfo info)
        {
            ThrowIfDisposed();
            var services1 = GetPowerTuningServices1();
            IADLXSmartShiftEco* eco = null;
            var result = services1->GetSmartShiftEco(&eco);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || eco == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "SmartShift Eco not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get SmartShift Eco interface");
            using var ecoPtr = new ComPtr<IADLXSmartShiftEco>(eco);
            if (!info.IsSupported) return;
            SetSmartShiftEcoEnabled(ecoPtr.Get(), info.IsEnabled);
        }

        public ManualPowerTuningInfo GetManualPowerTuning(IADLXGPUTuningServices* tuningServices, IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (tuningServices == null) throw new ArgumentNullException(nameof(tuningServices));
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXInterface* manual = null;
            var result = tuningServices->GetManualPowerTuning(gpu, &manual);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || manual == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual power tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get manual power tuning interface");
            using var manualPower = new ComPtr<IADLXManualPowerTuning>((IADLXManualPowerTuning*)manual);
            return new ManualPowerTuningInfo(manualPower.Get());
        }

        public void ApplyManualPowerTuning(IADLXManualPowerTuning* manualPower, ManualPowerTuningInfo info)
        {
            ThrowIfDisposed();
            if (manualPower == null) throw new ArgumentNullException(nameof(manualPower));

            if (info.PowerLimitSupported)
            {
                SetManualPowerLimit(manualPower, info.PowerLimitValue);
            }

            if (info.TdcLimitSupported && ADLXUtils.TryQueryInterface((IntPtr)manualPower, nameof(IADLXManualPowerTuning1), out var pManualPower1))
            {
                using var manualPower1 = new ComPtr<IADLXManualPowerTuning1>((IADLXManualPowerTuning1*)pManualPower1);
                SetManualTDCLimit(manualPower1.Get(), info.TdcLimitValue);
            }
        }

        public void ApplyManualPowerTuning(IADLXGPUTuningServices* tuningServices, IADLXGPU* gpu, ManualPowerTuningInfo info)
        {
            ThrowIfDisposed();
            if (tuningServices == null) throw new ArgumentNullException(nameof(tuningServices));
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXInterface* manual = null;
            var result = tuningServices->GetManualPowerTuning(gpu, &manual);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || manual == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual power tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get manual power tuning interface");
            using var manualPower = new ComPtr<IADLXManualPowerTuning>((IADLXManualPowerTuning*)manual);
            ApplyManualPowerTuning(manualPower.Get(), info);
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
                throw new ObjectDisposedException(nameof(ADLXPowerTuningServicesHelper));
        }

        ~ADLXPowerTuningServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }

        private void TryUpgradeServices(IADLXPowerTuningServices* services)
        {
            if (services == null) return;

            if (ADLXUtils.TryQueryInterface((IntPtr)services, nameof(IADLXPowerTuningServices1), out var p1))
            {
                _services1 = new ComPtr<IADLXPowerTuningServices1>((IADLXPowerTuningServices1*)p1);
            }
        }

        private IADLXPowerTuningServices1* GetPowerTuningServices1()
        {
            if (_services1.HasValue)
                return _services1.Value.Get();

            if (ADLXUtils.TryQueryInterface((IntPtr)_services.Get(), nameof(IADLXPowerTuningServices1), out var p1))
            {
                _services1 = new ComPtr<IADLXPowerTuningServices1>((IADLXPowerTuningServices1*)p1);
                return _services1.Value.Get();
            }

            throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Extended power tuning services not supported by this ADLX system");
        }

        private IADLXPowerTuningServices* GetHighestServices()
        {
            if (_services1.HasValue)
                return (IADLXPowerTuningServices*)_services1.Value.Get();
            return _services.Get();
        }

        private static void SetSmartShiftMaxBias(IADLXSmartShiftMax* smartShiftMax, ADLX_SSM_BIAS_MODE mode, int bias)
        {
            if (smartShiftMax == null) throw new ArgumentNullException(nameof(smartShiftMax));

            var modeResult = smartShiftMax->SetBiasMode(mode);
            if (modeResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(modeResult, "Failed to set SmartShift Max bias mode");

            var biasResult = smartShiftMax->SetBias(bias);
            if (biasResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(biasResult, "Failed to set SmartShift Max bias");
        }

        private static void SetSmartShiftEcoEnabled(IADLXSmartShiftEco* smartShiftEco, bool enable)
        {
            if (smartShiftEco == null) throw new ArgumentNullException(nameof(smartShiftEco));

            var result = smartShiftEco->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set SmartShift Eco");
        }

        private static void SetManualPowerLimit(IADLXManualPowerTuning* manualPower, int value)
        {
            if (manualPower == null) throw new ArgumentNullException(nameof(manualPower));

            var result = manualPower->SetPowerLimit(value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set manual power limit");
        }

        private static void SetManualTDCLimit(IADLXManualPowerTuning1* manualPowerV1, int value)
        {
            if (manualPowerV1 == null) throw new ArgumentNullException(nameof(manualPowerV1));

            var result = manualPowerV1->SetTDCLimit(value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set TDC limit");
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

        internal unsafe SmartShiftMaxInfo(IADLXSmartShiftMax* smartShiftMax)
        {
            bool supported = false;
            smartShiftMax->IsSupported(&supported);
            IsSupported = supported;

            ADLX_SSM_BIAS_MODE mode = default;
            smartShiftMax->GetBiasMode(&mode);
            BiasMode = mode;

            ADLX_IntRange range = default;
            smartShiftMax->GetBiasRange(&range);
            BiasRange = range;

            int bias = 0;
            smartShiftMax->GetBias(&bias);
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

        internal unsafe SmartShiftEcoInfo(IADLXSmartShiftEco* smartShiftEco)
        {
            bool supported = false, enabled = false;
            smartShiftEco->IsSupported(&supported);
            smartShiftEco->IsEnabled(&enabled);
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

        internal unsafe ManualPowerTuningInfo(IADLXManualPowerTuning* manualPower)
        {
            ADLX_IntRange powerRange = default;
            int powerValue = 0;
            var r1 = manualPower->GetPowerLimitRange(&powerRange);
            var r2 = manualPower->GetPowerLimit(&powerValue);
            PowerLimitSupported = r1 == ADLX_RESULT.ADLX_OK && r2 == ADLX_RESULT.ADLX_OK;
            PowerLimitRange = powerRange;
            PowerLimitValue = powerValue;

            if (ADLXUtils.TryQueryInterface((IntPtr)manualPower, nameof(IADLXManualPowerTuning1), out var pManualPower1))
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

