using System;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Facade for multimedia services (video upscale, video super resolution) bound to a GPU.
    /// </summary>
    public sealed unsafe class AdlxMultimedia : IDisposable
    {
        private readonly ADLXApi _owner;
        private ComPtr<IADLXMultimediaServices> _services;
        private ComPtr<IADLXGPU> _gpu;
        private bool _disposed;

        internal AdlxMultimedia(ADLXApi owner, IADLXMultimediaServices* services, IADLXGPU* gpu)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));
            _services = new ComPtr<IADLXMultimediaServices>(services);
            _gpu = new ComPtr<IADLXGPU>(gpu);
        }

        public MultimediaProfile GetProfile()
        {
            ThrowIfDisposed();
            var upscale = ADLXMultimediaHelpers.GetVideoUpscale(_services.Get(), _gpu.Get());
            var vsr = ADLXMultimediaHelpers.GetVideoSuperResolution(_services.Get(), _gpu.Get());
            return new MultimediaProfile(upscale, vsr);
        }

        public void ApplyProfile(MultimediaProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            ThrowIfDisposed();

            if (profile.VideoUpscale != null)
            {
                IADLXVideoUpscale* pUpscale = null;
                var res = _services.Get()->GetVideoUpscale(_gpu.Get(), &pUpscale);
                if (res == ADLX_RESULT.ADLX_OK && pUpscale != null)
                {
                    using var upscale = new ComPtr<IADLXVideoUpscale>(pUpscale);
                    ADLXMultimediaHelpers.ApplyVideoUpscale(upscale.Get(), profile.VideoUpscale.Value);
                }
            }

            if (profile.VideoSuperResolution != null)
            {
                IADLXVideoSuperResolution* pVsr = null;
                var res = _services.Get()->GetVideoSuperResolution(_gpu.Get(), &pVsr);
                if (res == ADLX_RESULT.ADLX_OK && pVsr != null)
                {
                    using var vsr = new ComPtr<IADLXVideoSuperResolution>(pVsr);
                    ADLXMultimediaHelpers.ApplyVideoSuperResolution(vsr.Get(), profile.VideoSuperResolution.Value);
                }
            }
        }

        public AdlxInterfaceHandle GetMultimediaServicesHandle()
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

        ~AdlxMultimedia()
        {
            Dispose(false);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(AdlxMultimedia));
        }
    }

    /// <summary>
    /// JSON-serializable multimedia profile.
    /// </summary>
    public sealed class MultimediaProfile
    {
        public VideoUpscaleInfo? VideoUpscale { get; set; }
        public VideoSuperResolutionInfo? VideoSuperResolution { get; set; }

        [JsonConstructor]
        public MultimediaProfile(VideoUpscaleInfo? videoUpscale, VideoSuperResolutionInfo? videoSuperResolution)
        {
            VideoUpscale = videoUpscale;
            VideoSuperResolution = videoSuperResolution;
        }

        internal MultimediaProfile(VideoUpscaleInfo upscale, VideoSuperResolutionInfo vsr)
        {
            VideoUpscale = upscale;
            VideoSuperResolution = vsr;
        }
    }
}
