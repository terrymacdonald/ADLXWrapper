using System;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Facade for 3D settings services bound to a GPU.
    /// </summary>
    public sealed unsafe class Adlx3DSettings : IDisposable
    {
        private readonly ADLXApi _owner;
        private ComPtr<IADLX3DSettingsServices> _services;
        private ComPtr<IADLXGPU> _gpu;
        private bool _disposed;

        internal Adlx3DSettings(ADLXApi owner, IADLX3DSettingsServices* services, IADLXGPU* gpu)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));
            _services = new ComPtr<IADLX3DSettingsServices>(services);
            _gpu = new ComPtr<IADLXGPU>(gpu);
        }

        public ThreeDSettingsProfile GetProfile()
        {
            ThrowIfDisposed();
            return ADLX3DSettingsHelpers.GetAll3DSettings(_services.Get(), _gpu.Get()).ToProfile();
        }

        public void ApplyProfile(ThreeDSettingsProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            ThrowIfDisposed();
            ADLX3DSettingsHelpers.ApplyAll3DSettings(_services.Get(), _gpu.Get(), profile.ToInfo());
        }

        public AdlxInterfaceHandle Get3DSettingsServicesHandle()
        {
            ThrowIfDisposed();
            ADLXHelpers.AddRefInterface((IntPtr)_services.Get());
            return AdlxInterfaceHandle.From(_services.Get(), addRef: false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            _services.Dispose();
            _gpu.Dispose();
            _services = default;
            _gpu = default;
            _disposed = true;
        }

        ~Adlx3DSettings()
        {
            Dispose(false);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(Adlx3DSettings));
        }
    }

    /// <summary>
    /// JSON-serializable 3D settings profile.
    /// </summary>
    public sealed class ThreeDSettingsProfile
    {
        public AntiLagInfo? AntiLag { get; set; }
        public BoostInfo? Boost { get; set; }
        public RadeonImageSharpeningInfo? ImageSharpening { get; set; }
        public EnhancedSyncInfo? EnhancedSync { get; set; }
        public WaitForVerticalRefreshInfo? WaitForVerticalRefresh { get; set; }
        public FrameRateTargetControlInfo? FrameRateTargetControl { get; set; }
        public AntiAliasingInfo? AntiAliasing { get; set; }
        public AnisotropicFilteringInfo? AnisotropicFiltering { get; set; }
        public TessellationInfo? Tessellation { get; set; }

        [JsonConstructor]
        public ThreeDSettingsProfile(
            AntiLagInfo? antiLag,
            BoostInfo? boost,
            RadeonImageSharpeningInfo? imageSharpening,
            EnhancedSyncInfo? enhancedSync,
            WaitForVerticalRefreshInfo? waitForVerticalRefresh,
            FrameRateTargetControlInfo? frameRateTargetControl,
            AntiAliasingInfo? antiAliasing,
            AnisotropicFilteringInfo? anisotropicFiltering,
            TessellationInfo? tessellation)
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

        internal ThreeDSettingsProfile(All3DSettingsInfo info)
        {
            AntiLag = info.AntiLag;
            Boost = info.Boost;
            ImageSharpening = info.ImageSharpening;
            EnhancedSync = info.EnhancedSync;
            WaitForVerticalRefresh = info.WaitForVerticalRefresh;
            FrameRateTargetControl = info.FrameRateTargetControl;
            AntiAliasing = info.AntiAliasing;
            AnisotropicFiltering = info.AnisotropicFiltering;
            Tessellation = info.Tessellation;
        }

        internal All3DSettingsInfo ToInfo()
        {
            return new All3DSettingsInfo(AntiLag, Boost, ImageSharpening, EnhancedSync, WaitForVerticalRefresh, FrameRateTargetControl, AntiAliasing, AnisotropicFiltering, Tessellation);
        }
    }

    internal static class ThreeDSettingsProfileExtensions
    {
        public static ThreeDSettingsProfile ToProfile(this All3DSettingsInfo info) => new ThreeDSettingsProfile(info);
    }
}
