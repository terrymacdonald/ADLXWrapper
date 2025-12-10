using System;
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
}