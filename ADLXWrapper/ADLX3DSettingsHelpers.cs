using System;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for 3D Settings services (Anti-Lag, Boost, RIS, etc.).
    /// </summary>
    public static unsafe class ADLX3DSettingsHelpers
    {
        /// <summary>
        /// Gets the IADLX3DSettingsServices interface from the system services.
        /// </summary>
        public static IADLX3DSettingsServices* Get3DSettingsServices(IADLXSystem* pSystem)
        {
            if (pSystem == null) throw new ArgumentNullException(nameof(pSystem));

            IADLX3DSettingsServices* p3DSettingsServices;
            var result = pSystem->Get3DSettingsServices(&p3DSettingsServices);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get 3D Settings services");
            }
            return p3DSettingsServices;
        }

        /// <summary>
        /// Gets all 3D settings for a specific GPU.
        /// </summary>
        public static All3DSettingsInfo GetAll3DSettings(IADLX3DSettingsServices* p3DSettingsServices, IADLXGPU* pGpu)
        {
            return new All3DSettingsInfo(p3DSettingsServices, pGpu);
        }

        /// <summary>
        /// Applies a complete set of 3D settings to a specific GPU.
        /// </summary>
        public static void ApplyAll3DSettings(IADLX3DSettingsServices* p3DSettingsServices, IADLXGPU* pGpu, All3DSettingsInfo info)
        {
            if (info.AntiLag.HasValue) ApplyAntiLag(p3DSettingsServices, pGpu, info.AntiLag.Value);
            if (info.Boost.HasValue) ApplyBoost(p3DSettingsServices, pGpu, info.Boost.Value);
            if (info.ImageSharpening.HasValue) ApplyRadeonImageSharpening(p3DSettingsServices, pGpu, info.ImageSharpening.Value);
            if (info.EnhancedSync.HasValue) ApplyEnhancedSync(p3DSettingsServices, pGpu, info.EnhancedSync.Value);
            if (info.WaitForVerticalRefresh.HasValue) ApplyWaitForVerticalRefresh(p3DSettingsServices, pGpu, info.WaitForVerticalRefresh.Value);
            if (info.FrameRateTargetControl.HasValue) ApplyFrameRateTargetControl(p3DSettingsServices, pGpu, info.FrameRateTargetControl.Value);
            if (info.AntiAliasing.HasValue) ApplyAntiAliasing(p3DSettingsServices, pGpu, info.AntiAliasing.Value);
            if (info.AnisotropicFiltering.HasValue) ApplyAnisotropicFiltering(p3DSettingsServices, pGpu, info.AnisotropicFiltering.Value);
            if (info.Tessellation.HasValue) ApplyTessellation(p3DSettingsServices, pGpu, info.Tessellation.Value);
        }

        // Individual Get/Apply helpers for each feature

        private static void ApplyAntiLag(IADLX3DSettingsServices* s, IADLXGPU* g, AntiLagInfo i) { if (s->GetAntiLag(g, out var p) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiLag>(p); if (i.IsSupported) c.Get()->SetEnabled(i.IsEnabled); } }
        private static void ApplyBoost(IADLX3DSettingsServices* s, IADLXGPU* g, BoostInfo i) { if (s->GetBoost(g, out var p) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DBoost>(p); if (i.IsSupported) { c.Get()->SetEnabled(i.IsEnabled); if (i.IsMinResSupported) c.Get()->SetMinimumResolution(i.MinResolution); } } }
        private static void ApplyRadeonImageSharpening(IADLX3DSettingsServices* s, IADLXGPU* g, RadeonImageSharpeningInfo i) { if (s->GetImageSharpening(g, out var p) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DRadeonImageSharpening>(p); if (i.IsSupported) { c.Get()->SetEnabled(i.IsEnabled); c.Get()->SetSharpness(i.Sharpness); } } }
        private static void ApplyEnhancedSync(IADLX3DSettingsServices* s, IADLXGPU* g, EnhancedSyncInfo i) { if (s->GetEnhancedSync(g, out var p) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DEnhancedSync>(p); if (i.IsSupported) c.Get()->SetEnabled(i.IsEnabled); } }
        private static void ApplyWaitForVerticalRefresh(IADLX3DSettingsServices* s, IADLXGPU* g, WaitForVerticalRefreshInfo i) { if (s->GetWaitForVerticalRefresh(g, out var p) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DWaitForVerticalRefresh>(p); if (i.IsSupported) c.Get()->SetEnabled(i.IsEnabled); if (i.IsModeSupported) c.Get()->SetMode(i.Mode); } }
        private static void ApplyFrameRateTargetControl(IADLX3DSettingsServices* s, IADLXGPU* g, FrameRateTargetControlInfo i) { if (s->GetFrameRateTargetControl(g, out var p) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DFrameRateTargetControl>(p); if (i.IsSupported) { c.Get()->SetEnabled(i.IsEnabled); c.Get()->SetFPS(i.Fps); } } }
        private static void ApplyAntiAliasing(IADLX3DSettingsServices* s, IADLXGPU* g, AntiAliasingInfo i) { if (s->GetAntiAliasing(g, out var p) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiAliasing>(p); if (i.IsSupported) c.Get()->SetMode(i.Mode); } }
        private static void ApplyAnisotropicFiltering(IADLX3DSettingsServices* s, IADLXGPU* g, AnisotropicFilteringInfo i) { if (s->GetAnisotropicFiltering(g, out var p) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAnisotropicFiltering>(p); if (i.IsSupported) c.Get()->SetLevel(i.Level); } }
        private static void ApplyTessellation(IADLX3DSettingsServices* s, IADLXGPU* g, TessellationInfo i) { if (s->GetTessellation(g, out var p) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DTessellation>(p); if (i.IsSupported) c.Get()->SetMode(i.Mode); } }

        /// <summary>
        /// Resets all 3D settings for a GPU to their default values.
        /// </summary>
        public static void ResetAll3DSettings(IADLX3DSettingsServices* p3DSettingsServices, IADLXGPU* pGpu)
        {
            if (p3DSettingsServices == null) throw new ArgumentNullException(nameof(p3DSettingsServices));
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));

            var result = p3DSettingsServices->ResetAll3DSettings(pGpu);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to reset all 3D settings.");
            }
        }
    }

    //================================================================================================
    // Info Structs for 3D Settings
    //================================================================================================

    /// <summary>
    /// Represents a complete snapshot of all 3D settings for a GPU.
    /// </summary>
    public readonly struct All3DSettingsInfo
    {
        public AntiLagInfo? AntiLag { get; init; }
        public BoostInfo? Boost { get; init; }
        public RadeonImageSharpeningInfo? ImageSharpening { get; init; }
        public EnhancedSyncInfo? EnhancedSync { get; init; }
        public WaitForVerticalRefreshInfo? WaitForVerticalRefresh { get; init; }
        public FrameRateTargetControlInfo? FrameRateTargetControl { get; init; }
        public AntiAliasingInfo? AntiAliasing { get; init; }
        public AnisotropicFilteringInfo? AnisotropicFiltering { get; init; }
        public TessellationInfo? Tessellation { get; init; }

        [JsonConstructor]
        public All3DSettingsInfo(AntiLagInfo? antiLag, BoostInfo? boost, RadeonImageSharpeningInfo? imageSharpening, EnhancedSyncInfo? enhancedSync, WaitForVerticalRefreshInfo? waitForVerticalRefresh, FrameRateTargetControlInfo? frameRateTargetControl, AntiAliasingInfo? antiAliasing, AnisotropicFilteringInfo? anisotropicFiltering, TessellationInfo? tessellation)
        {
            AntiLag = antiLag;
            Boost = boost;
            ImageSharpening = imageSharpening;
            EnhancedSync = enhancedSync;
            WaitForVerticalRefresh = waitForVerticalRefresh;
            FrameRateTargetControl = frameRateTargetControl;
            AntiAliasing = antiAliasing;
            AnisotropicFiltering = anisotropicFiltering;
            Tessellation = tessellation;
        }

        internal unsafe All3DSettingsInfo(IADLX3DSettingsServices* s, IADLXGPU* g)
        {
            if (s->GetAntiLag(g, out var pAntiLag) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiLag>(pAntiLag); AntiLag = new AntiLagInfo(c.Get()); } else { AntiLag = null; }
            if (s->GetBoost(g, out var pBoost) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DBoost>(pBoost); Boost = new BoostInfo(c.Get()); } else { Boost = null; }
            if (s->GetImageSharpening(g, out var pRis) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DRadeonImageSharpening>(pRis); ImageSharpening = new RadeonImageSharpeningInfo(c.Get()); } else { ImageSharpening = null; }
            if (s->GetEnhancedSync(g, out var pEs) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DEnhancedSync>(pEs); EnhancedSync = new EnhancedSyncInfo(c.Get()); } else { EnhancedSync = null; }
            if (s->GetWaitForVerticalRefresh(g, out var pVsync) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DWaitForVerticalRefresh>(pVsync); WaitForVerticalRefresh = new WaitForVerticalRefreshInfo(c.Get()); } else { WaitForVerticalRefresh = null; }
            if (s->GetFrameRateTargetControl(g, out var pFrtc) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DFrameRateTargetControl>(pFrtc); FrameRateTargetControl = new FrameRateTargetControlInfo(c.Get()); } else { FrameRateTargetControl = null; }
            if (s->GetAntiAliasing(g, out var pAa) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiAliasing>(pAa); AntiAliasing = new AntiAliasingInfo(c.Get()); } else { AntiAliasing = null; }
            if (s->GetAnisotropicFiltering(g, out var pAf) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAnisotropicFiltering>(pAf); AnisotropicFiltering = new AnisotropicFilteringInfo(c.Get()); } else { AnisotropicFiltering = null; }
            if (s->GetTessellation(g, out var pTess) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DTessellation>(pTess); Tessellation = new TessellationInfo(c.Get()); } else { Tessellation = null; }
        }
    }

    public readonly struct AntiLagInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public AntiLagInfo(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }

        internal unsafe AntiLagInfo(IADLX3DAntiLag* p)
        {
            p->IsSupported(out var supported);
            IsSupported = supported;
            if (IsSupported) p->IsEnabled(out var enabled); else enabled = false;
            IsEnabled = enabled;
        }
    }

    public readonly struct BoostInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public bool IsMinResSupported { get; init; }
        public int MinResolution { get; init; }
        public ADLX_IntRange ResolutionRange { get; init; }

        [JsonConstructor]
        public BoostInfo(bool isSupported, bool isEnabled, bool isMinResSupported, int minResolution, ADLX_IntRange resolutionRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            IsMinResSupported = isMinResSupported;
            MinResolution = minResolution;
            ResolutionRange = resolutionRange;
        }

        internal unsafe BoostInfo(IADLX3DBoost* p)
        {
            p->IsSupported(out var supported);
            IsSupported = supported;

            if (IsSupported)
            {
                p->IsEnabled(out var enabled);
                IsEnabled = enabled;
                p->IsSupportedMinimumResolution(out var minResSupported);
                IsMinResSupported = minResSupported;
                if (IsMinResSupported)
                {
                    p->GetMinimumResolution(out var minRes);
                    MinResolution = minRes;
                    p->GetMinimumResolutionRange(out var range);
                    ResolutionRange = range;
                }
                else
                {
                    MinResolution = 0;
                    ResolutionRange = default;
                }
            }
            else
            {
                IsEnabled = false;
                IsMinResSupported = false;
                MinResolution = 0;
                ResolutionRange = default;
            }
        }
    }

    public readonly struct RadeonImageSharpeningInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public int Sharpness { get; init; }
        public ADLX_IntRange SharpnessRange { get; init; }

        [JsonConstructor]
        public RadeonImageSharpeningInfo(bool isSupported, bool isEnabled, int sharpness, ADLX_IntRange sharpnessRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            Sharpness = sharpness;
            SharpnessRange = sharpnessRange;
        }

        internal unsafe RadeonImageSharpeningInfo(IADLX3DRadeonImageSharpening* p)
        {
            p->IsSupported(out var supported);
            IsSupported = supported;

            if (IsSupported)
            {
                p->IsEnabled(out var enabled);
                IsEnabled = enabled;
                p->GetSharpness(out var sharpness);
                Sharpness = sharpness;
                p->GetSharpnessRange(out var range);
                SharpnessRange = range;
            }
            else
            {
                IsEnabled = false;
                Sharpness = 0;
                SharpnessRange = default;
            }
        }
    }

    public readonly struct EnhancedSyncInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public EnhancedSyncInfo(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }

        internal unsafe EnhancedSyncInfo(IADLX3DEnhancedSync* p)
        {
            p->IsSupported(out var supported);
            IsSupported = supported;
            if (IsSupported) p->IsEnabled(out var enabled); else enabled = false;
            IsEnabled = enabled;
        }
    }

    public readonly struct WaitForVerticalRefreshInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public bool IsModeSupported { get; init; }
        public ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE Mode { get; init; }

        [JsonConstructor]
        public WaitForVerticalRefreshInfo(bool isSupported, bool isEnabled, bool isModeSupported, ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE mode)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            IsModeSupported = isModeSupported;
            Mode = mode;
        }

        internal unsafe WaitForVerticalRefreshInfo(IADLX3DWaitForVerticalRefresh* p)
        {
            p->IsSupported(out var supported);
            IsSupported = supported;

            if (IsSupported)
            {
                p->IsEnabled(out var enabled);
                IsEnabled = enabled;
                p->IsModeSupported(out var modeSupported);
                IsModeSupported = modeSupported;
                if (IsModeSupported) p->GetMode(out var mode); else mode = default;
                Mode = mode;
            }
            else
            {
                IsEnabled = false;
                IsModeSupported = false;
                Mode = default;
            }
        }
    }

    public readonly struct FrameRateTargetControlInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public int Fps { get; init; }
        public ADLX_IntRange FpsRange { get; init; }

        [JsonConstructor]
        public FrameRateTargetControlInfo(bool isSupported, bool isEnabled, int fps, ADLX_IntRange fpsRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            Fps = fps;
            FpsRange = fpsRange;
        }

        internal unsafe FrameRateTargetControlInfo(IADLX3DFrameRateTargetControl* p)
        {
            p->IsSupported(out var supported);
            IsSupported = supported;

            if (IsSupported)
            {
                p->IsEnabled(out var enabled);
                IsEnabled = enabled;
                p->GetFPS(out var fps);
                Fps = fps;
                p->GetFPSRange(out var range);
                FpsRange = range;
            }
            else
            {
                IsEnabled = false;
                Fps = 0;
                FpsRange = default;
            }
        }
    }

    public readonly struct AntiAliasingInfo
    {
        public bool IsSupported { get; init; }
        public ADLX_ANTI_ALIASING_MODE Mode { get; init; }

        [JsonConstructor]
        public AntiAliasingInfo(bool isSupported, ADLX_ANTI_ALIASING_MODE mode)
        {
            IsSupported = isSupported;
            Mode = mode;
        }

        internal unsafe AntiAliasingInfo(IADLX3DAntiAliasing* p)
        {
            p->IsSupported(out var supported);
            IsSupported = supported;
            if (IsSupported) p->GetMode(out var mode); else mode = default;
            Mode = mode;
        }
    }

    public readonly struct AnisotropicFilteringInfo
    {
        public bool IsSupported { get; init; }
        public ADLX_ANISOTROPIC_FILTERING_LEVEL Level { get; init; }

        [JsonConstructor]
        public AnisotropicFilteringInfo(bool isSupported, ADLX_ANISOTROPIC_FILTERING_LEVEL level)
        {
            IsSupported = isSupported;
            Level = level;
        }

        internal unsafe AnisotropicFilteringInfo(IADLX3DAnisotropicFiltering* p)
        {
            p->IsSupported(out var supported);
            IsSupported = supported;
            if (IsSupported) p->GetLevel(out var level); else level = default;
            Level = level;
        }
    }

    public readonly struct TessellationInfo
    {
        public bool IsSupported { get; init; }
        public ADLX_TESSELLATION_MODE Mode { get; init; }
        public ADLX_TESSELLATION_LEVEL Level { get; init; }

        [JsonConstructor]
        public TessellationInfo(bool isSupported, ADLX_TESSELLATION_MODE mode, ADLX_TESSELLATION_LEVEL level)
        {
            IsSupported = isSupported;
            Mode = mode;
            Level = level;
        }

        internal unsafe TessellationInfo(IADLX3DTessellation* p)
        {
            p->IsSupported(out var supported);
            IsSupported = supported;

            if (IsSupported)
            {
                p->GetMode(out var mode);
                Mode = mode;
                p->GetLevel(out var level);
                Level = level;
            }
            else
            {
                Mode = default;
                Level = default;
            }
        }
    }
}