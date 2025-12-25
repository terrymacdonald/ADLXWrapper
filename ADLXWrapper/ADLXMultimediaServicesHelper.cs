using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

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

        /// <summary>
        /// Creates a multimedia services helper from the native services interface.
        /// </summary>
        /// <param name="services">Native multimedia services pointer.</param>
        /// <param name="addRef">True to AddRef the pointer for this helper.</param>
        public ADLXMultimediaServicesHelper(IADLXMultimediaServices* services, bool addRef = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLXMultimediaServices>(services);
        }

        /// <summary>
        /// Returns the native multimedia services interface owned by this helper.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IADLXMultimediaServices* GetMultimediaServicesNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return _services.Get();
        }

        /// <summary>
        /// Returns an AddRef'd handle to the multimedia services interface.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public AdlxInterfaceHandle GetMultimediaServicesHandle()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return AdlxInterfaceHandle.From(GetMultimediaServicesNative(), addRef: true);
        }

        /// <summary>
        /// Gets the multimedia change handling interface (native). Cached after first query.
        /// </summary>
        /// <returns>Native multimedia change handling pointer.</returns>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IADLXMultimediaChangedHandling* GetMultimediaChangedHandlingNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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

        /// <summary>
        /// Tries to get multimedia change handling; returns false when unsupported.
        /// </summary>
        public bool TryGetMultimediaChangedHandlingNative(out IADLXMultimediaChangedHandling* handling)
        {
            try
            {
                handling = GetMultimediaChangedHandlingNative();
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                handling = null;
                return false;
            }
        }

        public AdlxInterfaceHandle GetMultimediaChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetMultimediaChangedHandlingNative(), addRef: true);
        }

        public MultimediaListenerHandle AddMultimediaEventListener(MultimediaListenerHandle.MultimediaChangedCallback callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            var handling = GetMultimediaChangedHandlingNative();
            var handle = MultimediaListenerHandle.Create(callback);
            var result = handling->AddMultimediaEventListener(handle.GetListener());
            if (result != ADLX_RESULT.ADLX_OK)
            {
                handle.Dispose();
                throw new ADLXException(result, "Failed to add multimedia event listener");
            }
            return handle;
        }

        public void RemoveMultimediaEventListener(MultimediaListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXVideoUpscale* upscale = null;
            var result = _services.Get()->GetVideoUpscale(gpu, &upscale);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || upscale == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Video upscale not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video upscale");

            return upscale; // caller wraps/disposes
        }

        /// <summary>
        /// Tries to get the native video upscale interface; returns false when unsupported.
        /// </summary>
        public bool TryGetVideoUpscaleNative(IADLXGPU* gpu, out IADLXVideoUpscale* upscale)
        {
            try
            {
                upscale = GetVideoUpscaleNative(gpu);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                upscale = null;
                return false;
            }
        }

        public VideoUpscaleInfo GetVideoUpscale(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using var upscale = new ComPtr<IADLXVideoUpscale>(GetVideoUpscaleNative(gpu));
            return new VideoUpscaleInfo(upscale.Get());
        }

        /// <summary>
        /// Tries to query video upscale info; returns false when unsupported.
        /// </summary>
        public bool TryGetVideoUpscale(IADLXGPU* gpu, out VideoUpscaleInfo info)
        {
            try
            {
                info = GetVideoUpscale(gpu);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                info = default;
                return false;
            }
        }

        public void SetVideoUpscaleEnabled(IADLXVideoUpscale* upscale, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (upscale == null) throw new ArgumentNullException(nameof(upscale));

            var result = upscale->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale enabled");
        }

        public void SetVideoUpscaleSharpness(IADLXVideoUpscale* upscale, int sharpness)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (upscale == null) throw new ArgumentNullException(nameof(upscale));

            var result = upscale->SetSharpness(sharpness);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale sharpness");
        }

        public IADLXVideoSuperResolution* GetVideoSuperResolutionNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXVideoSuperResolution* vsr = null;
            var result = _services.Get()->GetVideoSuperResolution(gpu, &vsr);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || vsr == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Video super resolution not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video super resolution");

            return vsr; // caller wraps/disposes
        }

        /// <summary>
        /// Tries to get the native video super resolution interface; returns false when unsupported.
        /// </summary>
        public bool TryGetVideoSuperResolutionNative(IADLXGPU* gpu, out IADLXVideoSuperResolution* vsr)
        {
            try
            {
                vsr = GetVideoSuperResolutionNative(gpu);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                vsr = null;
                return false;
            }
        }

        public VideoSuperResolutionInfo GetVideoSuperResolution(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            using var vsr = new ComPtr<IADLXVideoSuperResolution>(GetVideoSuperResolutionNative(gpu));
            return new VideoSuperResolutionInfo(vsr.Get());
        }

        /// <summary>
        /// Tries to query video super resolution info; returns false when unsupported.
        /// </summary>
        public bool TryGetVideoSuperResolution(IADLXGPU* gpu, out VideoSuperResolutionInfo info)
        {
            try
            {
                info = GetVideoSuperResolution(gpu);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                info = default;
                return false;
            }
        }

        public void SetVideoSuperResolutionEnabled(IADLXVideoSuperResolution* vsr, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (vsr == null) throw new ArgumentNullException(nameof(vsr));

            var result = vsr->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video super resolution enabled");
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

    #region Multimedia DTOs and listener handle
    public readonly struct VideoUpscaleInfo
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public int Sharpness { get; init; }
        public ADLX_IntRange SharpnessRange { get; init; }

        [JsonConstructor]
        public VideoUpscaleInfo(bool isSupported, bool isEnabled, int sharpness, ADLX_IntRange sharpnessRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            Sharpness = sharpness;
            SharpnessRange = sharpnessRange;
        }

        internal unsafe VideoUpscaleInfo(IADLXVideoUpscale* pUpscale)
        {
            bool supported = false, enabled = false;
            pUpscale->IsSupported(&supported);
            pUpscale->IsEnabled(&enabled);
            IsSupported = supported;
            IsEnabled = enabled;

            int sharpness = 0;
            pUpscale->GetSharpness(&sharpness);
            Sharpness = sharpness;

            ADLX_IntRange range = default;
            pUpscale->GetSharpnessRange(&range);
            SharpnessRange = range;
        }
    }

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
            bool supported = false, enabled = false;
            pVsr->IsSupported(&supported);
            pVsr->IsEnabled(&enabled);
            IsSupported = supported;
            IsEnabled = enabled;
        }
    }
    #endregion

    public sealed unsafe class MultimediaListenerHandle : SafeHandle
    {
        public delegate bool MultimediaChangedCallback(IntPtr pEvent);

        private static readonly ConcurrentDictionary<IntPtr, MultimediaChangedCallback> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnMultimediaChanged;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private MultimediaListenerHandle(MultimediaChangedCallback cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static MultimediaListenerHandle Create(MultimediaChangedCallback cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new MultimediaListenerHandle(cb);
        }

        public IADLXMultimediaChangedEventListener* GetListener() => (IADLXMultimediaChangedEventListener*)handle;

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static byte OnMultimediaChanged(IntPtr pThis, IntPtr pEvent)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb(pEvent) ? (byte)1 : (byte)0;
            }
            return 0;
        }
    }

}
