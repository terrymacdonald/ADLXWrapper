using System;
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

            var getFn = (delegate* unmanaged[Stdcall]<IADLXSystem*, out IADLXPowerTuningServices*, ADLX_RESULT>)pSystem->Vtbl->GetPowerTuningServices;
            IntPtr pServices;
            var result = getFn(pSystem, &pServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get power tuning services");
            return (IADLXPowerTuningServices*)pServices;
        }

        /// <summary>
        /// Gets the SmartShift Max information.
        /// </summary>
        public static SmartShiftMaxInfo GetSmartShiftMax(IADLXPowerTuningServices* pPowerServices)
        {
            if (pPowerServices == null) throw new ArgumentNullException(nameof(pPowerServices));

            var getFn = (delegate* unmanaged[Stdcall]<IADLXPowerTuningServices*, out IADLXSmartShiftMax*, ADLX_RESULT>)pPowerServices->Vtbl->GetSmartShiftMax;
            IntPtr pMax;
            var result = getFn(pPowerServices, &pMax);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get SmartShift Max interface");

            using var smartShiftMax = new ComPtr<IADLXSmartShiftMax>((IADLXSmartShiftMax*)pMax);
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
            if (pPowerServices->TryQueryInterface(out IADLXPowerTuningServices1* pPowerServices1) != ADLX_RESULT.ADLX_OK)
            {
                // Not supported, return default info
                return new SmartShiftEcoInfo(false, false);
            }
            using var powerServices1 = new ComPtr<IADLXPowerTuningServices1>(pPowerServices1);

            var getFn = (delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, out IADLXSmartShiftEco*, ADLX_RESULT>)powerServices1.Get()->Vtbl->GetSmartShiftEco;
            IntPtr pEco;
            var result = getFn(powerServices1.Get(), &pEco);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get SmartShift Eco interface");

            using var smartShiftEco = new ComPtr<IADLXSmartShiftEco>((IADLXSmartShiftEco*)pEco);
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

            var setModeFn = (delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, ADLX_SSM_BIAS_MODE, ADLX_RESULT>)pSmartShiftMax->Vtbl->SetBiasMode;
            var setBiasFn = (delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, int, ADLX_RESULT>)pSmartShiftMax->Vtbl->SetBias;

            var modeResult = setModeFn(pSmartShiftMax, mode);
            if (modeResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(modeResult, "Failed to set SmartShift Max bias mode");

            var biasResult = setBiasFn(pSmartShiftMax, bias);
            if (biasResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(biasResult, "Failed to set SmartShift Max bias");
        }

        /// <summary>
        /// Sets the enabled state of SmartShift Eco.
        /// </summary>
        public static void SetSmartShiftEcoEnabled(IADLXSmartShiftEco* pSmartShiftEco, bool enable)
        {
            if (pSmartShiftEco == null) throw new ArgumentNullException(nameof(pSmartShiftEco));

            var setFn = (delegate* unmanaged[Stdcall]<IADLXSmartShiftEco*, byte, ADLX_RESULT>)pSmartShiftEco->Vtbl->SetEnabled;
            var result = setFn(pSmartShiftEco, enable ? (byte)1 : (byte)0);
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

            var getFn = (delegate* unmanaged[Stdcall]<IADLXGPUTuningServices*, IADLXGPU*, out IADLXManualPowerTuning*, ADLX_RESULT>)pGpuTuningServices->Vtbl->GetManualPowerTuning;
            IntPtr pManual;
            var result = getFn(pGpuTuningServices, pGPU, &pManual);
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

            if (info.TdcLimitSupported && pManualPower->TryQueryInterface(out IADLXManualPowerTuning1* pManualPower1) == ADLX_RESULT.ADLX_OK)
            {
                using var manualPower1 = new ComPtr<IADLXManualPowerTuning1>(pManualPower1);
                SetManualTDCLimit(manualPower1.Get(), info.TdcLimitValue);
            }
        }

        /// <summary>
        /// Sets the manual power limit.
        /// </summary>
        public static void SetManualPowerLimit(IADLXManualPowerTuning* pManualPower, int value)
        {
            if (pManualPower == null) throw new ArgumentNullException(nameof(pManualPower));

            var setFn = (delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int, ADLX_RESULT>)pManualPower->Vtbl->SetPowerLimit;
            var result = setFn(pManualPower, value);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set manual power limit");
        }

        /// <summary>
        /// Sets the manual TDC limit.
        /// </summary>
        public static void SetManualTDCLimit(IADLXManualPowerTuning1* pManualPowerV1, int value)
        {
            if (pManualPowerV1 == null) throw new ArgumentNullException(nameof(pManualPowerV1));

            var setFn = (delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int, ADLX_RESULT>)pManualPowerV1->Vtbl->SetTDCLimit;
            var result = setFn(pManualPowerV1, value);
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

        internal unsafe SmartShiftMaxInfo(IADLXSmartShiftMax* pSmartShiftMax)
        {
            byte supported = 0;
            pSmartShiftMax->IsSupported(&supported);
            IsSupported = supported != 0;

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
            byte supported = 0, enabled = 0;
            pSmartShiftEco->IsSupported(&supported);
            pSmartShiftEco->IsEnabled(&enabled);
            IsSupported = supported != 0;
            IsEnabled = enabled != 0;
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

            // Try to query for IADLXManualPowerTuning1 for TDC limit
            if (pManualPower->TryQueryInterface(out IADLXManualPowerTuning1* pManualPower1) == ADLX_RESULT.ADLX_OK)
            {
                using var manualPower1 = new ComPtr<IADLXManualPowerTuning1>(pManualPower1);
                byte tdcSupported = 0;
                manualPower1.Get()->IsSupportedTDCLimit(&tdcSupported);
                TdcLimitSupported = tdcSupported != 0;

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
}