using System;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLXMultimediaServices exposing managed/native accessors and change handling.
    /// </summary>
    public sealed unsafe class ADLXMultimediaServicesHelper : IDisposable
    {
        private ComPtr<IADLXMultimediaServices> _services;
        private ComPtr<IADLXMultimediaChangedHandling>? _changedHandling;
        private bool _disposed;

        public ADLXMultimediaServicesHelper(IADLXMultimediaServices* services, bool addRef = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLXMultimediaServices>(services);
        }

        public IADLXMultimediaServices* GetMultimediaServicesNative()
        {
            ThrowIfDisposed();
            return _services.Get();
        }

        public AdlxInterfaceHandle GetMultimediaServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(GetMultimediaServicesNative(), addRef: true);
        }

        public IADLXMultimediaChangedHandling* GetMultimediaChangedHandlingNative()
        {
            ThrowIfDisposed();
            if (_changedHandling.HasValue)
                return _changedHandling.Value.Get();

            IADLXMultimediaChangedHandling* handling = null;
            var result = _services.Get()->GetMultimediaChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Multimedia change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get multimedia change handling");

            _changedHandling = new ComPtr<IADLXMultimediaChangedHandling>(handling);
            return handling;
        }

        public AdlxInterfaceHandle GetMultimediaChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetMultimediaChangedHandlingNative(), addRef: true);
        }

        public MultimediaEventListenerHandle AddMultimediaEventListener(MultimediaEventListenerHandle.MultimediaChangedCallback callback)
        {
            ThrowIfDisposed();
            var handling = GetMultimediaChangedHandlingNative();
            var handle = MultimediaEventListenerHandle.Create(callback);
            var result = handling->AddMultimediaEventListener(handle.GetListener());
            if (result != ADLX_RESULT.ADLX_OK)
            {
                handle.Dispose();
                throw new ADLXException(result, "Failed to add multimedia event listener");
            }
            return handle;
        }

        public void RemoveMultimediaEventListener(MultimediaEventListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetMultimediaChangedHandlingNative();
            handling->RemoveMultimediaEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        public IADLXVideoUpscale* GetVideoUpscaleNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXVideoUpscale* upscale = null;
            var result = _services.Get()->GetVideoUpscale(gpu, &upscale);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || upscale == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Video upscale not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video upscale");

            return upscale; // caller wraps/disposes
        }

        public VideoUpscaleInfo GetVideoUpscale(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using var upscale = new ComPtr<IADLXVideoUpscale>(GetVideoUpscaleNative(gpu));
            return new VideoUpscaleInfo(upscale.Get());
        }

        public IADLXVideoSuperResolution* GetVideoSuperResolutionNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXVideoSuperResolution* vsr = null;
            var result = _services.Get()->GetVideoSuperResolution(gpu, &vsr);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || vsr == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Video super resolution not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video super resolution");

            return vsr; // caller wraps/disposes
        }

        public VideoSuperResolutionInfo GetVideoSuperResolution(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using var vsr = new ComPtr<IADLXVideoSuperResolution>(GetVideoSuperResolutionNative(gpu));
            return new VideoSuperResolutionInfo(vsr.Get());
        }

        public void Dispose()
        {
            if (_disposed) return;
            _changedHandling?.Dispose();
            _services.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ADLXMultimediaServicesHelper));
        }

        ~ADLXMultimediaServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }
    }
}

