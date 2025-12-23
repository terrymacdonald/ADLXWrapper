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
        private bool _disposed;

        public ADLX3DSettingsServicesHelper(IADLX3DSettingsServices* services, bool addRef = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLX3DSettingsServices>(services);
            TryUpgradeServices(services);
        }

        public IADLX3DSettingsServices* Get3DSettingsServicesNative()
        {
            ThrowIfDisposed();
            return GetHighestServices();
        }

        public AdlxInterfaceHandle Get3DSettingsServicesHandle()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(Get3DSettingsServicesNative(), addRef: true);
        }

        public IADLX3DSettingsChangedHandling* Get3DSettingsChangedHandlingNative()
        {
            ThrowIfDisposed();
            if (_changedHandling.HasValue)
                return _changedHandling.Value.Get();

            IADLX3DSettingsChangedHandling* handling = null;
            var result = GetHighestServices()->Get3DSettingsChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "3D settings change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get 3D settings change handling");

            _changedHandling = new ComPtr<IADLX3DSettingsChangedHandling>(handling);
            return handling;
        }

        public AdlxInterfaceHandle Get3DSettingsChangedHandling()
        {
            return AdlxInterfaceHandle.From(Get3DSettingsChangedHandlingNative(), addRef: true);
        }

        public ThreeDSettingsListenerHandle Add3DSettingsEventListener(ThreeDSettingsListenerHandle.ThreeDSettingsChangedCallback callback)
        {
            ThrowIfDisposed();
            if (callback == null) throw new ArgumentNullException(nameof(callback));

            var handling = Get3DSettingsChangedHandlingNative();
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
            if (handle == null || handle.IsInvalid) return;

            var handling = Get3DSettingsChangedHandlingNative();
            handling->Remove3DSettingsEventListener(handle.GetListener());

            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        public All3DSettingsInfo GetAll3DSettings(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));
            return new All3DSettingsInfo(GetHighestServices(), gpu);
        }

        public void ApplyAll3DSettings(IADLXGPU* gpu, All3DSettingsInfo info)
        {
            ThrowIfDisposed();
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

        private static void ApplyAntiLag(IADLX3DSettingsServices* services, IADLXGPU* gpu, AntiLagInfo info)
        {
            IADLX3DAntiLag* p;
            if (services->GetAntiLag(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DAntiLag>(p);
                if (info.IsSupported) c.Get()->SetEnabled(info.IsEnabled ? (byte)1 : (byte)0);
            }
        }

        private static void ApplyBoost(IADLX3DSettingsServices* services, IADLXGPU* gpu, BoostInfo info)
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

        private static void ApplyRadeonImageSharpening(IADLX3DSettingsServices* services, IADLXGPU* gpu, RadeonImageSharpeningInfo info)
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

        private static void ApplyEnhancedSync(IADLX3DSettingsServices* services, IADLXGPU* gpu, EnhancedSyncInfo info)
        {
            IADLX3DEnhancedSync* p;
            if (services->GetEnhancedSync(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DEnhancedSync>(p);
                if (info.IsSupported) c.Get()->SetEnabled(info.IsEnabled ? (byte)1 : (byte)0);
            }
        }

        private static void ApplyWaitForVerticalRefresh(IADLX3DSettingsServices* services, IADLXGPU* gpu, WaitForVerticalRefreshInfo info)
        {
            IADLX3DWaitForVerticalRefresh* p;
            if (services->GetWaitForVerticalRefresh(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DWaitForVerticalRefresh>(p);
                if (info.IsSupported) c.Get()->SetMode(info.Mode);
            }
        }

        private static void ApplyFrameRateTargetControl(IADLX3DSettingsServices* services, IADLXGPU* gpu, FrameRateTargetControlInfo info)
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

        private static void ApplyAntiAliasing(IADLX3DSettingsServices* services, IADLXGPU* gpu, AntiAliasingInfo info)
        {
            IADLX3DAntiAliasing* p;
            if (services->GetAntiAliasing(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DAntiAliasing>(p);
                if (info.IsSupported) c.Get()->SetMode(info.Mode);
            }
        }

        private static void ApplyAnisotropicFiltering(IADLX3DSettingsServices* services, IADLXGPU* gpu, AnisotropicFilteringInfo info)
        {
            IADLX3DAnisotropicFiltering* p;
            if (services->GetAnisotropicFiltering(gpu, &p) == ADLX_RESULT.ADLX_OK)
            {
                using var c = new ComPtr<IADLX3DAnisotropicFiltering>(p);
                if (info.IsSupported) c.Get()->SetLevel(info.Level);
            }
        }

        private static void ApplyTessellation(IADLX3DSettingsServices* services, IADLXGPU* gpu, TessellationInfo info)
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

        internal unsafe All3DSettingsInfo(IADLX3DSettingsServices* services, IADLXGPU* gpu)
        {
            IADLX3DAntiLag* pAntiLag;
            if (services->GetAntiLag(gpu, &pAntiLag) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiLag>(pAntiLag); AntiLag = new AntiLagInfo(c.Get()); } else { AntiLag = null; }

            IADLX3DBoost* pBoost;
            if (services->GetBoost(gpu, &pBoost) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DBoost>(pBoost); Boost = new BoostInfo(c.Get()); } else { Boost = null; }

            IADLX3DImageSharpening* pRis;
            if (services->GetImageSharpening(gpu, &pRis) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DImageSharpening>(pRis); ImageSharpening = new RadeonImageSharpeningInfo(c.Get()); } else { ImageSharpening = null; }

            IADLX3DEnhancedSync* pEs;
            if (services->GetEnhancedSync(gpu, &pEs) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DEnhancedSync>(pEs); EnhancedSync = new EnhancedSyncInfo(c.Get()); } else { EnhancedSync = null; }

            IADLX3DWaitForVerticalRefresh* pVsync;
            if (services->GetWaitForVerticalRefresh(gpu, &pVsync) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DWaitForVerticalRefresh>(pVsync); WaitForVerticalRefresh = new WaitForVerticalRefreshInfo(c.Get()); } else { WaitForVerticalRefresh = null; }

            IADLX3DFrameRateTargetControl* pFrtc;
            if (services->GetFrameRateTargetControl(gpu, &pFrtc) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DFrameRateTargetControl>(pFrtc); FrameRateTargetControl = new FrameRateTargetControlInfo(c.Get()); } else { FrameRateTargetControl = null; }

            IADLX3DAntiAliasing* pAa;
            if (services->GetAntiAliasing(gpu, &pAa) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAntiAliasing>(pAa); AntiAliasing = new AntiAliasingInfo(c.Get()); } else { AntiAliasing = null; }

            IADLX3DAnisotropicFiltering* pAf;
            if (services->GetAnisotropicFiltering(gpu, &pAf) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DAnisotropicFiltering>(pAf); AnisotropicFiltering = new AnisotropicFilteringInfo(c.Get()); } else { AnisotropicFiltering = null; }

            IADLX3DTessellation* pTess;
            if (services->GetTessellation(gpu, &pTess) == ADLX_RESULT.ADLX_OK) { using var c = new ComPtr<IADLX3DTessellation>(pTess); Tessellation = new TessellationInfo(c.Get()); } else { Tessellation = null; }
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

        internal unsafe AntiLagInfo(IADLX3DAntiLag* antiLag)
        {
            bool supported = false;
            antiLag->IsSupported(&supported);
            IsSupported = supported;

            bool enabled = false;
            if (IsSupported) antiLag->IsEnabled(&enabled);
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

        internal unsafe BoostInfo(IADLX3DBoost* boost)
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
                ResolutionRange = range;

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

        internal unsafe RadeonImageSharpeningInfo(IADLX3DImageSharpening* sharpening)
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

        internal unsafe EnhancedSyncInfo(IADLX3DEnhancedSync* enhancedSync)
        {
            bool supported = false;
            enhancedSync->IsSupported(&supported);
            IsSupported = supported;

            bool enabled = false;
            if (IsSupported) enhancedSync->IsEnabled(&enabled);
            IsEnabled = enabled;
        }
    }

    public readonly struct WaitForVerticalRefreshInfo
    {
        public bool IsSupported { get; init; }
        public ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE Mode { get; init; }

        [JsonConstructor]
        public WaitForVerticalRefreshInfo(bool isSupported, ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE mode)
        {
            IsSupported = isSupported;
            Mode = mode;
        }

        internal unsafe WaitForVerticalRefreshInfo(IADLX3DWaitForVerticalRefresh* vsync)
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

        internal unsafe FrameRateTargetControlInfo(IADLX3DFrameRateTargetControl* frtc)
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

        internal unsafe AntiAliasingInfo(IADLX3DAntiAliasing* antiAliasing)
        {
            bool supported = false;
            antiAliasing->IsSupported(&supported);
            IsSupported = supported;

            ADLX_ANTI_ALIASING_MODE mode = default;
            if (IsSupported) antiAliasing->GetMode(&mode);
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

        internal unsafe AnisotropicFilteringInfo(IADLX3DAnisotropicFiltering* anisotropicFiltering)
        {
            bool supported = false;
            anisotropicFiltering->IsSupported(&supported);
            IsSupported = supported;

            ADLX_ANISOTROPIC_FILTERING_LEVEL level = default;
            if (IsSupported) anisotropicFiltering->GetLevel(&level);
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

        internal unsafe TessellationInfo(IADLX3DTessellation* tessellation)
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

