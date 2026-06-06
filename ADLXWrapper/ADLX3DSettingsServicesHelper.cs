using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLX3DSettingsServices selecting the highest available interface and exposing change handling.
    /// </summary>
    public sealed unsafe class ADLX3DSettingsServicesHelper : IDisposable
    {
        private ComPtr<IADLX3DSettingsServices> _services;
        private ComPtr<IADLX3DSettingsServices1>? _services1;
        private ComPtr<IADLX3DSettingsServices2>? _services2;
        private ComPtr<IADLX3DSettingsChangedHandling>? _changedHandling;
        private readonly IADLXSystem* _system;
        private bool _disposed;

        /// <summary>
        /// Creates a 3D settings helper from the native services interface, upgrading to v1/v2 when available.
        /// </summary>
        /// <param name="services">Native 3D settings services pointer.</param>
        /// <param name="addRef">True to AddRef the pointer for this helper.</param>
        /// <param name="system">Optional native system pointer used to upgrade to v1/v2 interfaces.</param>
        public ADLX3DSettingsServicesHelper(IADLX3DSettingsServices* services, bool addRef = true, IADLXSystem* system = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLX3DSettingsServices>(services);
            TryUpgradeServices(services);
            _system = system; // IADLXSystem is not ref-counted; safe to store as raw pointer
        }

        internal IADLX3DSettingsServices* Get3DSettingsServicesNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return GetHighestServices();
        }

        /// <summary>
        /// Returns an AddRef'd handle to the highest available 3D settings services interface.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal ADLXInterfaceHandle Get3DSettingsServicesHandle()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return ADLXInterfaceHandle.From(Get3DSettingsServicesNative(), addRef: true);
        }

        /// <summary>
        /// Gets the 3D settings change handling interface (native). Cached after first query.
        /// </summary>
        /// <returns>Native 3D settings change handling pointer.</returns>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal IADLX3DSettingsChangedHandling* Get3DSettingsChangedHandlingNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (_changedHandling.HasValue)
                return _changedHandling.Value.Get();

            IADLX3DSettingsChangedHandling* handling = null;
            var result = GetHighestServices()->Get3DSettingsChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                return null;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get 3D settings change handling");

            _changedHandling = new ComPtr<IADLX3DSettingsChangedHandling>(handling);
            return handling;
        }

        /// <summary>
        /// Tries to get 3D settings change handling; returns false when unsupported.
        /// </summary>
        internal bool TryGet3DSettingsChangedHandlingNative(out IADLX3DSettingsChangedHandling* handling)
        {
            handling = Get3DSettingsChangedHandlingNative();
            return handling != null;
        }

        internal ADLXInterfaceHandle Get3DSettingsChangedHandling()
        {
            var native = Get3DSettingsChangedHandlingNative();
            return native != null ? ADLXInterfaceHandle.From(native, addRef: true) : default;
        }

        /// <summary>
        /// Adds a 3D settings change listener.
        /// </summary>
        /// <param name="callback">Callback invoked on 3D settings changes.</param>
        /// <returns>Listener handle that must be disposed to unsubscribe.</returns>
        /// <exception cref="ADLXException">If registration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public ThreeDSettingsListenerHandle? Add3DSettingsEventListener(ThreeDSettingsListenerHandle.ThreeDSettingsChangedCallback callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (callback == null) throw new ArgumentNullException(nameof(callback));

            var handling = Get3DSettingsChangedHandlingNative();
            if (handling == null)
                return null;
            var handle = ThreeDSettingsListenerHandle.Create(callback);
            var result = handling->Add3DSettingsEventListener(handle.GetListener());
            if (result != ADLX_RESULT.ADLX_OK)
            {
                handle.Dispose();
                throw new ADLXException(result, "Failed to add 3D settings event listener");
            }

            return handle;
        }

        public void Remove3DSettingsEventListener(ThreeDSettingsListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (handle == null || handle.IsInvalid) return;

            var handling = Get3DSettingsChangedHandlingNative();
            if (handling == null) return;
            handling->Remove3DSettingsEventListener(handle.GetListener());

            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        internal All3DSettingsDto GetAll3DSettings(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));
            var services = GetHighestServices();

            AntiLagDto? antiLag = null;
            IADLX3DAntiLag* pAntiLag;
            if (services->GetAntiLag(gpu, &pAntiLag) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiLag>(pAntiLag); antiLag = new AntiLagDto(c.Get()); }

            BoostDto? boost = null;
            IADLX3DBoost* pBoost;
            if (services->GetBoost(gpu, &pBoost) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DBoost>(pBoost); boost = new BoostDto(c.Get()); }

            RadeonImageSharpeningDto? sharpening = null;
            IADLX3DImageSharpening* pRis;
            if (services->GetImageSharpening(gpu, &pRis) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DImageSharpening>(pRis); sharpening = new RadeonImageSharpeningDto(c.Get()); }

            EnhancedSyncDto? enhancedSync = null;
            IADLX3DEnhancedSync* pEs;
            if (services->GetEnhancedSync(gpu, &pEs) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DEnhancedSync>(pEs); enhancedSync = new EnhancedSyncDto(c.Get()); }

            WaitForVerticalRefreshDto? vsync = null;
            IADLX3DWaitForVerticalRefresh* pVsync;
            if (services->GetWaitForVerticalRefresh(gpu, &pVsync) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DWaitForVerticalRefresh>(pVsync); vsync = new WaitForVerticalRefreshDto(c.Get()); }

            FrameRateTargetControlDto? frtc = null;
            IADLX3DFrameRateTargetControl* pFrtc;
            if (services->GetFrameRateTargetControl(gpu, &pFrtc) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DFrameRateTargetControl>(pFrtc); frtc = new FrameRateTargetControlDto(c.Get()); }

            AntiAliasingDto? aa = null;
            IADLX3DAntiAliasing* pAa;
            if (services->GetAntiAliasing(gpu, &pAa) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiAliasing>(pAa); aa = new AntiAliasingDto(c.Get()); }

            AnisotropicFilteringDto? af = null;
            IADLX3DAnisotropicFiltering* pAf;
            if (services->GetAnisotropicFiltering(gpu, &pAf) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAnisotropicFiltering>(pAf); af = new AnisotropicFilteringDto(c.Get()); }

            TessellationDto? tess = null;
            IADLX3DTessellation* pTess;
            if (services->GetTessellation(gpu, &pTess) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DTessellation>(pTess); tess = new TessellationDto(c.Get()); }

            return new All3DSettingsDto(antiLag, boost, sharpening, enhancedSync, vsync, frtc, aa, af, tess, null, null);
        }

        /// <summary>
        /// Tries to get all 3D settings; returns false when unsupported.
        /// </summary>
        internal bool TryGetAll3DSettings(IADLXGPU* gpu, out All3DSettingsDto info)
        {
            info = GetAll3DSettings(gpu);
            return true;
        }

        /// <summary>
        /// Applies all provided 3D settings to a GPU (only non-null fields are applied).
        /// </summary>
        /// <param name="gpu">Native GPU pointer.</param>
        /// <param name="info">Settings to apply.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="gpu"/> is null.</exception>
        /// <exception cref="ADLXException">If any underlying call fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal void ApplyAll3DSettings(IADLXGPU* gpu, All3DSettingsDto info)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));
            var services = GetHighestServices();
            if (info.AntiLag.HasValue) ApplyAntiLag(services, gpu, info.AntiLag.Value);
            if (info.Boost.HasValue) ApplyBoost(services, gpu, info.Boost.Value);
            if (info.ImageSharpening.HasValue) ApplyRadeonImageSharpening(services, gpu, info.ImageSharpening.Value);
            if (info.EnhancedSync.HasValue) ApplyEnhancedSync(services, gpu, info.EnhancedSync.Value);
            if (info.WaitForVerticalRefresh.HasValue) ApplyWaitForVerticalRefresh(services, gpu, info.WaitForVerticalRefresh.Value);
            if (info.FrameRateTargetControl.HasValue) ApplyFrameRateTargetControl(services, gpu, info.FrameRateTargetControl.Value);
            if (info.AntiAliasing.HasValue) ApplyAntiAliasing(services, gpu, info.AntiAliasing.Value);
            if (info.AnisotropicFiltering.HasValue) ApplyAnisotropicFiltering(services, gpu, info.AnisotropicFiltering.Value);
            if (info.Tessellation.HasValue) ApplyTessellation(services, gpu, info.Tessellation.Value);
        }

        /// <summary>
        /// Tries to apply all provided 3D settings; returns false when the feature set is unsupported.
        /// </summary>
        internal bool TryApplyAll3DSettings(IADLXGPU* gpu, All3DSettingsDto info)
        {
            ApplyAll3DSettings(gpu, info);
            return true;
        }

        // =====================================================================
        // Public per-GPU overloads (by unique id)
        // =====================================================================

        /// <summary>Gets all 3D settings for the GPU with the specified unique id.</summary>
        public All3DSettingsDto GetAll3DSettings(int gpuUniqueId)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return WithGpuByUniqueId(gpuUniqueId, ptrGpu => GetAll3DSettings((IADLXGPU*)ptrGpu));
        }

        /// <summary>Tries to get all 3D settings for the GPU with the specified unique id.</summary>
        public bool TryGetAll3DSettings(int gpuUniqueId, out All3DSettingsDto info)
        {
            info = GetAll3DSettings(gpuUniqueId);
            return true;
        }

        /// <summary>Applies all 3D settings to the GPU with the specified unique id.</summary>
        public void ApplyAll3DSettings(int gpuUniqueId, All3DSettingsDto info)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            WithGpuByUniqueId(gpuUniqueId, ptrGpu => { ApplyAll3DSettings((IADLXGPU*)ptrGpu, info); return 0; });
        }

        /// <summary>Tries to apply all 3D settings to the GPU with the specified unique id.</summary>
        public bool TryApplyAll3DSettings(int gpuUniqueId, All3DSettingsDto info)
        {
            ApplyAll3DSettings(gpuUniqueId, info);
            return true;
        }

        /// <summary>Gets Fluid Motion Frames state for the GPU with the specified unique id.</summary>
        public FluidMotionFramesDto GetFluidMotionFrames(int gpuUniqueId)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return WithGpuByUniqueId(gpuUniqueId, ptrGpu => GetFluidMotionFrames((IADLXGPU*)ptrGpu));
        }

        /// <summary>Tries to get Fluid Motion Frames state for the GPU with the specified unique id.</summary>
        public bool TryGetFluidMotionFrames(int gpuUniqueId, out FluidMotionFramesDto info)
        {
            info = GetFluidMotionFrames(gpuUniqueId);
            return true;
        }

        private T WithGpuByUniqueId<T>(int gpuUniqueId, Func<IntPtr, T> action)
        {
            if (_system == null) throw new InvalidOperationException("System not available for GPU lookup by unique id. Ensure this helper was obtained via ADLXSystemServicesHelper.");
            IADLXGPUList* pList = null;
            var result = _system->GetGPUs(&pList);
            if (result != ADLX_RESULT.ADLX_OK || pList == null)
                throw new ADLXException(result != ADLX_RESULT.ADLX_OK ? result : ADLX_RESULT.ADLX_FAIL, "Failed to enumerate GPUs for unique id lookup");
            using var list = new ComPtr<IADLXGPUList>(pList);
            uint size = list.Get()->Size();
            for (uint i = 0; i < size; i++)
            {
                IADLXGPU* pGpu = null;
                if (list.Get()->At(i, &pGpu) != ADLX_RESULT.ADLX_OK || pGpu == null) continue;
                int uid = 0;
                pGpu->UniqueId(&uid);
                if (uid == gpuUniqueId)
                {
                    using var gpuOwner = new ComPtr<IADLXGPU>(pGpu);
                    return action((IntPtr)gpuOwner.Get());
                }
                ADLXUtils.ReleaseInterface((IntPtr)pGpu);
            }
            throw new ADLXException(ADLX_RESULT.ADLX_NOT_FOUND, $"GPU with unique id {gpuUniqueId} not found");
        }

        public void Dispose()
        {
            if (_disposed) return;
            _changedHandling?.Dispose();
            _services2?.Dispose();
            _services1?.Dispose();
            _services.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ADLX3DSettingsServicesHelper));
        }

        ~ADLX3DSettingsServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }

        private void TryUpgradeServices(IADLX3DSettingsServices* services)
        {
            if (services == null) return;

            if (ADLXUtils.TryQueryInterface((IntPtr)services, nameof(IADLX3DSettingsServices2), out var p2))
            {
                _services2 = new ComPtr<IADLX3DSettingsServices2>((IADLX3DSettingsServices2*)p2);
                return;
            }

            if (ADLXUtils.TryQueryInterface((IntPtr)services, nameof(IADLX3DSettingsServices1), out var p1))
            {
                _services1 = new ComPtr<IADLX3DSettingsServices1>((IADLX3DSettingsServices1*)p1);
            }
        }

        private IADLX3DSettingsServices* GetHighestServices()
        {
            if (_services2.HasValue)
                return (IADLX3DSettingsServices*)_services2.Value.Get();
            if (_services1.HasValue)
                return (IADLX3DSettingsServices*)_services1.Value.Get();
            return _services.Get();
        }

        private static void ApplyAntiLag(IADLX3DSettingsServices* services, IADLXGPU* gpu, AntiLagDto info)
        {
            IADLX3DAntiLag* p;
            if (services->GetAntiLag(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DAntiLag>(p);
                if (info.IsSupported) c.Get()->SetEnabled(info.IsEnabled ? (byte)1 : (byte)0);
            }
        }

        private static void ApplyBoost(IADLX3DSettingsServices* services, IADLXGPU* gpu, BoostDto info)
        {
            IADLX3DBoost* p;
            if (services->GetBoost(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DBoost>(p);
                if (info.IsSupported)
                {
                    c.Get()->SetEnabled(info.IsEnabled ? (byte)1 : (byte)0);
                    if (info.IsMinResSupported) c.Get()->SetResolution(info.MinResolution);
                }
            }
        }

        private static void ApplyRadeonImageSharpening(IADLX3DSettingsServices* services, IADLXGPU* gpu, RadeonImageSharpeningDto info)
        {
            IADLX3DImageSharpening* p;
            if (services->GetImageSharpening(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DImageSharpening>(p);
                if (info.IsSupported)
                {
                    c.Get()->SetEnabled(info.IsEnabled ? (byte)1 : (byte)0);
                    c.Get()->SetSharpness(info.Sharpness);
                }
            }
        }

        private static void ApplyEnhancedSync(IADLX3DSettingsServices* services, IADLXGPU* gpu, EnhancedSyncDto info)
        {
            IADLX3DEnhancedSync* p;
            if (services->GetEnhancedSync(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DEnhancedSync>(p);
                if (info.IsSupported) c.Get()->SetEnabled(info.IsEnabled ? (byte)1 : (byte)0);
            }
        }

        private static void ApplyWaitForVerticalRefresh(IADLX3DSettingsServices* services, IADLXGPU* gpu, WaitForVerticalRefreshDto info)
        {
            IADLX3DWaitForVerticalRefresh* p;
            if (services->GetWaitForVerticalRefresh(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DWaitForVerticalRefresh>(p);
                if (info.IsSupported) c.Get()->SetMode(info.Mode);
            }
        }

        private static void ApplyFrameRateTargetControl(IADLX3DSettingsServices* services, IADLXGPU* gpu, FrameRateTargetControlDto info)
        {
            IADLX3DFrameRateTargetControl* p;
            if (services->GetFrameRateTargetControl(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DFrameRateTargetControl>(p);
                if (info.IsSupported)
                {
                    c.Get()->SetEnabled(info.IsEnabled ? (byte)1 : (byte)0);
                    c.Get()->SetFPS(info.Fps);
                }
            }
        }

        private static void ApplyAntiAliasing(IADLX3DSettingsServices* services, IADLXGPU* gpu, AntiAliasingDto info)
        {
            IADLX3DAntiAliasing* p;
            if (services->GetAntiAliasing(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DAntiAliasing>(p);
                if (info.IsSupported) c.Get()->SetMode(info.Mode);
            }
        }

        private static void ApplyAnisotropicFiltering(IADLX3DSettingsServices* services, IADLXGPU* gpu, AnisotropicFilteringDto info)
        {
            IADLX3DAnisotropicFiltering* p;
            if (services->GetAnisotropicFiltering(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DAnisotropicFiltering>(p);
                if (info.IsSupported) c.Get()->SetLevel(info.Level);
            }
        }

        private static void ApplyTessellation(IADLX3DSettingsServices* services, IADLXGPU* gpu, TessellationDto info)
        {
            IADLX3DTessellation* p;
            if (services->GetTessellation(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DTessellation>(p);
                if (info.IsSupported)
                {
                    c.Get()->SetMode(info.Mode);
                    c.Get()->SetLevel(info.Level);
                }
            }
        }

        internal FluidMotionFramesDto GetFluidMotionFrames(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLX3DAMDFluidMotionFrames* fmf = null;
            // Prefer v2, then v1. If neither is present, treat as unsupported.
            IADLX3DSettingsServices2* s2 = null;
            IADLX3DSettingsServices1* s1 = null;
            var services = GetHighestServices();

            if (ADLXUtils.TryQueryInterface((IntPtr)services, nameof(IADLX3DSettingsServices2), out var p2))
            {
                s2 = (IADLX3DSettingsServices2*)p2;
            }
            else if (ADLXUtils.TryQueryInterface((IntPtr)services, nameof(IADLX3DSettingsServices1), out var p1))
            {
                s1 = (IADLX3DSettingsServices1*)p1;
            }
            else
            {
                return new FluidMotionFramesDto(false, false);
            }

            ADLX_RESULT result;
            IADLX3DAMDFluidMotionFrames* local = null;
            if (s2 != null)
            {
                using var s2Owner = new ComPtr<IADLX3DSettingsServices2>(s2);
                result = s2Owner.Get()->GetAMDFluidMotionFrames(&local);
            }
            else
            {
                using var s1Owner = new ComPtr<IADLX3DSettingsServices1>(s1);
                result = s1Owner.Get()->GetAMDFluidMotionFrames(&local);
            }
            fmf = local;
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || fmf == null)
                return new FluidMotionFramesDto(false, false);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get AMD Fluid Motion Frames interface");

            try
            {
                using var fmfPtr = new ComPtr<IADLX3DAMDFluidMotionFrames>(fmf);
                bool supported = false;
                var supportResult = fmfPtr.Get()->IsSupported(&supported);
                if (supportResult == ADLX_RESULT.ADLX_NOT_SUPPORTED || !supported)
                    return new FluidMotionFramesDto(false, false);
                if (supportResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(supportResult, "Failed to query AMD Fluid Motion Frames support");

                bool enabled = false;
                var enabledResult = fmfPtr.Get()->IsEnabled(&enabled);
                if (enabledResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(enabledResult, "Failed to query AMD Fluid Motion Frames state");

                return new FluidMotionFramesDto(true, enabled);
            }
            catch (System.Runtime.InteropServices.SEHException)
            {
                return new FluidMotionFramesDto(false, false);
            }
        }

        internal bool TryGetFluidMotionFrames(IADLXGPU* gpu, out FluidMotionFramesDto info)
        {
            info = GetFluidMotionFrames(gpu);
            return info.IsSupported;
        }

        public RadeonSuperResolutionDto GetRadeonSuperResolution()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();

            IADLX3DRadeonSuperResolution* rsr = null;
            var result = GetHighestServices()->GetRadeonSuperResolution(&rsr);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || rsr == null)
                return new RadeonSuperResolutionDto(false, false, 0, default);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Radeon Super Resolution interface");

            try
            {
                using var rsrPtr = new ComPtr<IADLX3DRadeonSuperResolution>(rsr);
                bool supported = false;
                var supportResult = rsrPtr.Get()->IsSupported(&supported);
                if (supportResult == ADLX_RESULT.ADLX_NOT_SUPPORTED || !supported)
                    return new RadeonSuperResolutionDto(false, false, 0, default);
                if (supportResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(supportResult, "Failed to query Radeon Super Resolution support");

                bool enabled = false;
                var enabledResult = rsrPtr.Get()->IsEnabled(&enabled);
                if (enabledResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(enabledResult, "Failed to query Radeon Super Resolution state");

                ADLX_IntRange range = default;
                var rangeResult = rsrPtr.Get()->GetSharpnessRange(&range);
                if (rangeResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(rangeResult, "Failed to query Radeon Super Resolution sharpness range");

                int sharpness = 0;
                var sharpnessResult = rsrPtr.Get()->GetSharpness(&sharpness);
                if (sharpnessResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(sharpnessResult, "Failed to query Radeon Super Resolution sharpness");

                return new RadeonSuperResolutionDto(true, enabled, sharpness, IntRangeDto.FromNative(range));
            }
            catch (System.Runtime.InteropServices.SEHException)
            {
                return new RadeonSuperResolutionDto(false, false, 0, default);
            }
        }

        public bool TryGetRadeonSuperResolution(out RadeonSuperResolutionDto info)
        {
            info = GetRadeonSuperResolution();
            return info.IsSupported;
        }

    }

    //================================================================================================
    // Info Structs for 3D Settings
    //================================================================================================

    /// <summary>
    /// Represents a complete snapshot of all 3D settings for a GPU.
    /// </summary>
    public readonly struct All3DSettingsDto
    {
        public AntiLagDto? AntiLag { get; init; }
        public BoostDto? Boost { get; init; }
        public RadeonImageSharpeningDto? ImageSharpening { get; init; }
        public EnhancedSyncDto? EnhancedSync { get; init; }
        public WaitForVerticalRefreshDto? WaitForVerticalRefresh { get; init; }
        public FrameRateTargetControlDto? FrameRateTargetControl { get; init; }
        public AntiAliasingDto? AntiAliasing { get; init; }
        public AnisotropicFilteringDto? AnisotropicFiltering { get; init; }
        public TessellationDto? Tessellation { get; init; }
        public FluidMotionFramesDto? FluidMotionFrames { get; init; }
        public RadeonSuperResolutionDto? RadeonSuperResolution { get; init; }

        [JsonConstructor]
        public All3DSettingsDto(AntiLagDto? antiLag, BoostDto? boost, RadeonImageSharpeningDto? imageSharpening, EnhancedSyncDto? enhancedSync, WaitForVerticalRefreshDto? waitForVerticalRefresh, FrameRateTargetControlDto? frameRateTargetControl, AntiAliasingDto? antiAliasing, AnisotropicFilteringDto? anisotropicFiltering, TessellationDto? tessellation, FluidMotionFramesDto? fluidMotionFrames, RadeonSuperResolutionDto? radeonSuperResolution)
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
            FluidMotionFrames = fluidMotionFrames;
            RadeonSuperResolution = radeonSuperResolution;
        }

        internal unsafe All3DSettingsDto(IADLX3DSettingsServices* services, IADLXGPU* gpu)
        {
            IADLX3DAntiLag* pAntiLag;
            if (services->GetAntiLag(gpu, &pAntiLag) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiLag>(pAntiLag); AntiLag = new AntiLagDto(c.Get()); } else { AntiLag = null; }

            IADLX3DBoost* pBoost;
            if (services->GetBoost(gpu, &pBoost) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DBoost>(pBoost); Boost = new BoostDto(c.Get()); } else { Boost = null; }

            IADLX3DImageSharpening* pRis;
            if (services->GetImageSharpening(gpu, &pRis) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DImageSharpening>(pRis); ImageSharpening = new RadeonImageSharpeningDto(c.Get()); } else { ImageSharpening = null; }

            IADLX3DEnhancedSync* pEs;
            if (services->GetEnhancedSync(gpu, &pEs) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DEnhancedSync>(pEs); EnhancedSync = new EnhancedSyncDto(c.Get()); } else { EnhancedSync = null; }

            IADLX3DWaitForVerticalRefresh* pVsync;
            if (services->GetWaitForVerticalRefresh(gpu, &pVsync) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DWaitForVerticalRefresh>(pVsync); WaitForVerticalRefresh = new WaitForVerticalRefreshDto(c.Get()); } else { WaitForVerticalRefresh = null; }

            IADLX3DFrameRateTargetControl* pFrtc;
            if (services->GetFrameRateTargetControl(gpu, &pFrtc) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DFrameRateTargetControl>(pFrtc); FrameRateTargetControl = new FrameRateTargetControlDto(c.Get()); } else { FrameRateTargetControl = null; }

            IADLX3DAntiAliasing* pAa;
            if (services->GetAntiAliasing(gpu, &pAa) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiAliasing>(pAa); AntiAliasing = new AntiAliasingDto(c.Get()); } else { AntiAliasing = null; }

            IADLX3DAnisotropicFiltering* pAf;
            if (services->GetAnisotropicFiltering(gpu, &pAf) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAnisotropicFiltering>(pAf); AnisotropicFiltering = new AnisotropicFilteringDto(c.Get()); } else { AnisotropicFiltering = null; }

            IADLX3DTessellation* pTess;
            if (services->GetTessellation(gpu, &pTess) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DTessellation>(pTess); Tessellation = new TessellationDto(c.Get()); } else { Tessellation = null; }

            // FMF/RSR not available without helper context; leave null in this constructor.
            FluidMotionFrames = null;
            RadeonSuperResolution = null;
        }
    }

    public readonly struct AntiLagDto
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public AntiLagDto(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }

        internal unsafe AntiLagDto(IADLX3DAntiLag* antiLag)
        {
            bool supported = false;
            antiLag->IsSupported(&supported);
            IsSupported = supported;

            bool enabled = false;
            if (IsSupported) antiLag->IsEnabled(&enabled);
            IsEnabled = enabled;
        }
    }

    public readonly struct BoostDto
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public bool IsMinResSupported { get; init; }
        public int MinResolution { get; init; }
        public IntRangeDto ResolutionRange { get; init; }

        [JsonConstructor]
        public BoostDto(bool isSupported, bool isEnabled, bool isMinResSupported, int minResolution, IntRangeDto resolutionRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            IsMinResSupported = isMinResSupported;
            MinResolution = minResolution;
            ResolutionRange = resolutionRange;
        }

        internal unsafe BoostDto(IADLX3DBoost* boost)
        {
            bool supported = false;
            boost->IsSupported(&supported);
            IsSupported = supported;

            if (IsSupported)
            {
                bool enabled = false;
                boost->IsEnabled(&enabled);
                IsEnabled = enabled;

                ADLX_IntRange range = default;
                boost->GetResolutionRange(&range);
                ResolutionRange = IntRangeDto.FromNative(range);

                int minRes = 0;
                boost->GetResolution(&minRes);
                MinResolution = minRes;
                IsMinResSupported = true;
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

    public readonly struct RadeonImageSharpeningDto
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public int Sharpness { get; init; }
        public IntRangeDto SharpnessRange { get; init; }

        [JsonConstructor]
        public RadeonImageSharpeningDto(bool isSupported, bool isEnabled, int sharpness, IntRangeDto sharpnessRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            Sharpness = sharpness;
            SharpnessRange = sharpnessRange;
        }

        internal unsafe RadeonImageSharpeningDto(IADLX3DImageSharpening* sharpening)
        {
            bool supported = false;
            sharpening->IsSupported(&supported);
            IsSupported = supported;

            if (IsSupported)
            {
                bool enabled = false;
                sharpening->IsEnabled(&enabled);
                IsEnabled = enabled;

                int sharpness = 0;
                sharpening->GetSharpness(&sharpness);
                Sharpness = sharpness;

                ADLX_IntRange range = default;
                sharpening->GetSharpnessRange(&range);
                SharpnessRange = IntRangeDto.FromNative(range);
            }
            else
            {
                IsEnabled = false;
                Sharpness = 0;
                SharpnessRange = default;
            }
        }
    }

    public readonly struct EnhancedSyncDto
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public EnhancedSyncDto(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }

        internal unsafe EnhancedSyncDto(IADLX3DEnhancedSync* enhancedSync)
        {
            bool supported = false;
            enhancedSync->IsSupported(&supported);
            IsSupported = supported;

            bool enabled = false;
            if (IsSupported) enhancedSync->IsEnabled(&enabled);
            IsEnabled = enabled;
        }
    }

    public readonly struct WaitForVerticalRefreshDto
    {
        public bool IsSupported { get; init; }
        public ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE Mode { get; init; }

        [JsonConstructor]
        public WaitForVerticalRefreshDto(bool isSupported, ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE mode)
        {
            IsSupported = isSupported;
            Mode = mode;
        }

        internal unsafe WaitForVerticalRefreshDto(IADLX3DWaitForVerticalRefresh* vsync)
        {
            bool supported = false;
            vsync->IsSupported(&supported);
            IsSupported = supported;

            if (IsSupported)
            {
                ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE mode = default;
                vsync->GetMode(&mode);
                Mode = mode;
            }
            else
            {
                Mode = default;
            }
        }
    }

    public readonly struct FrameRateTargetControlDto
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public int Fps { get; init; }
        public IntRangeDto FpsRange { get; init; }

        [JsonConstructor]
        public FrameRateTargetControlDto(bool isSupported, bool isEnabled, int fps, IntRangeDto fpsRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            Fps = fps;
            FpsRange = fpsRange;
        }

        internal unsafe FrameRateTargetControlDto(IADLX3DFrameRateTargetControl* frtc)
        {
            bool supported = false;
            frtc->IsSupported(&supported);
            IsSupported = supported;

            if (IsSupported)
            {
                bool enabled = false;
                frtc->IsEnabled(&enabled);
                IsEnabled = enabled;

                int fps = 0;
                frtc->GetFPS(&fps);
                Fps = fps;

                ADLX_IntRange range = default;
                frtc->GetFPSRange(&range);
                FpsRange = IntRangeDto.FromNative(range);
            }
            else
            {
                IsEnabled = false;
                Fps = 0;
                FpsRange = default;
            }
        }
    }

    public readonly struct AntiAliasingDto
    {
        public bool IsSupported { get; init; }
        public ADLX_ANTI_ALIASING_MODE Mode { get; init; }

        [JsonConstructor]
        public AntiAliasingDto(bool isSupported, ADLX_ANTI_ALIASING_MODE mode)
        {
            IsSupported = isSupported;
            Mode = mode;
        }

        internal unsafe AntiAliasingDto(IADLX3DAntiAliasing* antiAliasing)
        {
            bool supported = false;
            antiAliasing->IsSupported(&supported);
            IsSupported = supported;

            ADLX_ANTI_ALIASING_MODE mode = default;
            if (IsSupported) antiAliasing->GetMode(&mode);
            Mode = mode;
        }
    }

    public readonly struct AnisotropicFilteringDto
    {
        public bool IsSupported { get; init; }
        public ADLX_ANISOTROPIC_FILTERING_LEVEL Level { get; init; }

        [JsonConstructor]
        public AnisotropicFilteringDto(bool isSupported, ADLX_ANISOTROPIC_FILTERING_LEVEL level)
        {
            IsSupported = isSupported;
            Level = level;
        }

        internal unsafe AnisotropicFilteringDto(IADLX3DAnisotropicFiltering* anisotropicFiltering)
        {
            bool supported = false;
            anisotropicFiltering->IsSupported(&supported);
            IsSupported = supported;

            ADLX_ANISOTROPIC_FILTERING_LEVEL level = default;
            if (IsSupported) anisotropicFiltering->GetLevel(&level);
            Level = level;
        }
    }

    public readonly struct TessellationDto
    {
        public bool IsSupported { get; init; }
        public ADLX_TESSELLATION_MODE Mode { get; init; }
        public ADLX_TESSELLATION_LEVEL Level { get; init; }

        [JsonConstructor]
        public TessellationDto(bool isSupported, ADLX_TESSELLATION_MODE mode, ADLX_TESSELLATION_LEVEL level)
        {
            IsSupported = isSupported;
            Mode = mode;
            Level = level;
        }

        internal unsafe TessellationDto(IADLX3DTessellation* tessellation)
        {
            bool supported = false;
            tessellation->IsSupported(&supported);
            IsSupported = supported;

            if (IsSupported)
            {
                ADLX_TESSELLATION_MODE mode = default;
                ADLX_TESSELLATION_LEVEL level = default;
                tessellation->GetMode(&mode);
                tessellation->GetLevel(&level);
                Mode = mode;
                Level = level;
            }
            else
            {
                Mode = default;
                Level = default;
            }
        }
    }

    public readonly struct FluidMotionFramesDto
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public FluidMotionFramesDto(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }
    }

    public readonly struct RadeonSuperResolutionDto
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public int Sharpness { get; init; }
        public IntRangeDto SharpnessRange { get; init; }

        [JsonConstructor]
        public RadeonSuperResolutionDto(bool isSupported, bool isEnabled, int sharpness, IntRangeDto sharpnessRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            Sharpness = sharpness;
            SharpnessRange = sharpnessRange;
        }
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLX3DSettingsChangedListener backed by a managed delegate.
    /// Callbacks arrive on ADLX threads; the handle roots the delegate until disposed or explicitly removed.
    /// </summary>
    public sealed unsafe class ThreeDSettingsListenerHandle : SafeHandle
    {
        public delegate bool ThreeDSettingsChangedCallback(IntPtr pEvent);

        private static readonly ConcurrentDictionary<IntPtr, ThreeDSettingsChangedCallback> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&On3DSettingsChanged;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private ThreeDSettingsListenerHandle(ThreeDSettingsChangedCallback cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static ThreeDSettingsListenerHandle Create(ThreeDSettingsChangedCallback cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new ThreeDSettingsListenerHandle(cb);
        }

        public IADLX3DSettingsChangedListener* GetListener() => (IADLX3DSettingsChangedListener*)handle;

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
        private static byte On3DSettingsChanged(IntPtr pThis, IntPtr pEvent)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb(pEvent) ? (byte)1 : (byte)0;
            }
            return 0;
        }
    }
}

