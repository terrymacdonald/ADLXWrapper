using System;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for multimedia settings (video upscale and video super resolution).
    /// </summary>
    public static unsafe partial class ADLXMultimediaHelpers
    {
        /// <summary>
        /// Gets the IADLXMultimediaServices interface from the system services. Callers must dispose the returned pointer.
        /// </summary>
        public static IADLXMultimediaServices* GetMultimediaServices(IADLXSystem* pSystem)
        {
            if (pSystem == null) throw new ArgumentNullException(nameof(pSystem));

            var getFn = (delegate* unmanaged[Stdcall]<IADLXSystem*, out IADLXMultimediaServices*, ADLX_RESULT>)pSystem->Vtbl->GetMultiMediaServices;
            IntPtr pServices;
            var result = getFn(pSystem, &pServices);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get multimedia services");
            return (IADLXMultimediaServices*)pServices;
        }

        /// <summary>
        /// Gets the Video Upscale information for a specific GPU.
        /// </summary>
        public static VideoUpscaleInfo GetVideoUpscale(IADLXMultimediaServices* pMultimediaServices, IADLXGPU* pGPU)
        {
            if (pMultimediaServices == null) throw new ArgumentNullException(nameof(pMultimediaServices));
            if (pGPU == null) throw new ArgumentNullException(nameof(pGPU));

            var getFn = (delegate* unmanaged[Stdcall]<IADLXMultimediaServices*, IADLXGPU*, out IADLXVideoUpscale*, ADLX_RESULT>)pMultimediaServices->Vtbl->GetVideoUpscale;
            IntPtr pUpscale;
            var result = getFn(pMultimediaServices, pGPU, &pUpscale);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video upscale interface");

            using var upscale = new ComPtr<IADLXVideoUpscale>((IADLXVideoUpscale*)pUpscale);
            return new VideoUpscaleInfo(upscale.Get());
        }

        /// <summary>
        /// Gets the Video Super Resolution information for a specific GPU.
        /// </summary>
        public static VideoSuperResolutionInfo GetVideoSuperResolution(IADLXMultimediaServices* pMultimediaServices, IADLXGPU* pGPU)
        {
            if (pMultimediaServices == null) throw new ArgumentNullException(nameof(pMultimediaServices));
            if (pGPU == null) throw new ArgumentNullException(nameof(pGPU));

            var getFn = (delegate* unmanaged[Stdcall]<IADLXMultimediaServices*, IADLXGPU*, out IADLXVideoSuperResolution*, ADLX_RESULT>)pMultimediaServices->Vtbl->GetVideoSuperResolution;
            IntPtr pVsr;
            var result = getFn(pMultimediaServices, pGPU, &pVsr);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video super resolution interface");

            using var vsr = new ComPtr<IADLXVideoSuperResolution>((IADLXVideoSuperResolution*)pVsr);
            return new VideoSuperResolutionInfo(vsr.Get());
        }

        /// <summary>
        /// Sets the enabled state of Video Upscale.
        /// </summary>
        public static void SetVideoUpscaleEnabled(IADLXVideoUpscale* pVideoUpscale, bool enable)
        {
            if (pVideoUpscale == null) throw new ArgumentNullException(nameof(pVideoUpscale));

            var setFn = (delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, byte, ADLX_RESULT>)pVideoUpscale->Vtbl->SetEnabled;
            var result = setFn(pVideoUpscale, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale enabled");
        }

        /// <summary>
        /// Sets the minimum input resolution for Video Upscale.
        /// </summary>
        public static void SetVideoUpscaleMinInputResolution(IADLXVideoUpscale* pVideoUpscale, int minResolution)
        {
            if (pVideoUpscale == null) throw new ArgumentNullException(nameof(pVideoUpscale));

            var setFn = (delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, int, ADLX_RESULT>)pVideoUpscale->Vtbl->SetMinInputResolution;
            var result = setFn(pVideoUpscale, minResolution);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale minimum input resolution");
        }

        /// <summary>
        /// Sets the enabled state of Video Super Resolution.
        /// </summary>
        public static void SetVideoSuperResolutionEnabled(IADLXVideoSuperResolution* pVsr, bool enable)
        {
            if (pVsr == null) throw new ArgumentNullException(nameof(pVsr));

            var setFn = (delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, byte, ADLX_RESULT>)pVsr->Vtbl->SetEnabled;
            var result = setFn(pVsr, enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video super resolution enabled");
        }
    }

    /// <summary>
    /// Represents the collected information for Video Upscale.
    /// </summary>
    public readonly struct VideoUpscaleInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public ADLX_IntRange ScaleFactorRange { get; init; }
        public int MinInputResolution { get; init; }

        [JsonConstructor]
        public VideoUpscaleInfo(bool isSupported, bool isEnabled, ADLX_IntRange scaleFactorRange, int minInputResolution)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            ScaleFactorRange = scaleFactorRange;
            MinInputResolution = minInputResolution;
        }

        internal unsafe VideoUpscaleInfo(IADLXVideoUpscale* pUpscale)
        {
            byte supported = 0, enabled = 0;
            pUpscale->IsSupported(&supported);
            pUpscale->IsEnabled(&enabled);
            IsSupported = supported != 0;
            IsEnabled = enabled != 0;

            ADLX_IntRange range = default;
            pUpscale->GetScaleFactorRange(&range);
            ScaleFactorRange = range;

            int minRes = 0;
            pUpscale->GetMinInputResolution(&minRes);
            MinInputResolution = minRes;
        }
    }

    /// <summary>
    /// Represents the collected information for Video Super Resolution.
    /// </summary>
    public readonly struct VideoSuperResolutionInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public VideoSuperResolutionInfo(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }

        internal unsafe VideoSuperResolutionInfo(IADLXVideoSuperResolution* pVsr)
        {
            byte supported = 0, enabled = 0;
            pVsr->IsSupported(&supported);
            pVsr->IsEnabled(&enabled);
            IsSupported = supported != 0;
            IsEnabled = enabled != 0;
        }
    }
}