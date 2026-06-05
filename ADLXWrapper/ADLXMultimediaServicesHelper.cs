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
        private readonly IADLXSystem* _system;
        private bool _disposed;

        /// <summary>
        /// Creates a multimedia services helper from the native services interface.
        /// </summary>
        /// <param name="services">Native multimedia services pointer.</param>
        /// <param name="addRef">True to AddRef the pointer for this helper.</param>
        public ADLXMultimediaServicesHelper(IADLXMultimediaServices* services, bool addRef = true, IADLXSystem* system = null)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLXMultimediaServices>(services);
            _system = system; // IADLXSystem is not ref-counted; safe to store as raw pointer
        }

        /// <summary>
        /// Returns the native multimedia services interface owned by this helper.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal IADLXMultimediaServices* GetMultimediaServicesNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return _services.Get();
        }

        /// <summary>
        /// Returns an AddRef'd handle to the multimedia services interface.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal ADLXInterfaceHandle GetMultimediaServicesHandle()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return ADLXInterfaceHandle.From(GetMultimediaServicesNative(), addRef: true);
        }

        /// <summary>
        /// Gets the multimedia change handling interface (native). Cached after first query.
        /// </summary>
        /// <returns>Native multimedia change handling pointer.</returns>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        internal IADLXMultimediaChangedHandling* GetMultimediaChangedHandlingNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (_changedHandling.HasValue)
                return _changedHandling.Value.Get();

            IADLXMultimediaChangedHandling* handling = null;
            var result = _services.Get()->GetMultimediaChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                return null;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get multimedia change handling");

            _changedHandling = new ComPtr<IADLXMultimediaChangedHandling>(handling);
            return handling;
        }

        /// <summary>
        /// Tries to get multimedia change handling; returns false when unsupported.
        /// </summary>
        internal bool TryGetMultimediaChangedHandlingNative(out IADLXMultimediaChangedHandling* handling)
        {
            handling = GetMultimediaChangedHandlingNative();
            return handling != null;
        }

        internal ADLXInterfaceHandle GetMultimediaChangedHandling()
        {
            var native = GetMultimediaChangedHandlingNative();
            return native != null ? ADLXInterfaceHandle.From(native, addRef: true) : default;
        }

        public MultimediaListenerHandle? AddMultimediaEventListener(MultimediaListenerHandle.MultimediaChangedCallback callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            var handling = GetMultimediaChangedHandlingNative();
            if (handling == null)
                return null;
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
            if (handling == null) return;
            handling->RemoveMultimediaEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        internal IADLXVideoUpscale* GetVideoUpscaleNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXVideoUpscale* upscale = null;
            var result = _services.Get()->GetVideoUpscale(gpu, &upscale);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || upscale == null)
                return null;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video upscale");

            return upscale; // caller wraps/disposes
        }

        /// <summary>
        /// Tries to get the native video upscale interface; returns false when unsupported.
        /// </summary>
        internal bool TryGetVideoUpscaleNative(IADLXGPU* gpu, out IADLXVideoUpscale* upscale)
        {
            upscale = GetVideoUpscaleNative(gpu);
            return upscale != null;
        }

        internal VideoUpscaleDto GetVideoUpscale(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            var native = GetVideoUpscaleNative(gpu);
            if (native == null) return default;
            using var upscale = new ComPtr<IADLXVideoUpscale>(native);
            return new VideoUpscaleDto(upscale.Get());
        }

        /// <summary>
        /// Tries to query video upscale info; returns false when unsupported.
        /// </summary>
        internal bool TryGetVideoUpscale(IADLXGPU* gpu, out VideoUpscaleDto info)
        {
            info = GetVideoUpscale(gpu);
            return true;
        }

        internal void SetVideoUpscaleEnabled(IADLXVideoUpscale* upscale, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (upscale == null) throw new ArgumentNullException(nameof(upscale));

            var result = upscale->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale enabled");
        }

        /// <summary>
        /// Tries to set video upscale enabled; returns false when the feature is unsupported.
        /// </summary>
        internal bool TrySetVideoUpscaleEnabled(IADLXVideoUpscale* upscale, bool enable)
        {
            SetVideoUpscaleEnabled(upscale, enable);
            return true;
        }

        internal void SetVideoUpscaleSharpness(IADLXVideoUpscale* upscale, int sharpness)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (upscale == null) throw new ArgumentNullException(nameof(upscale));

            var result = upscale->SetSharpness(sharpness);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale sharpness");
        }

        /// <summary>
        /// Tries to set video upscale sharpness; returns false when the feature is unsupported.
        /// </summary>
        internal bool TrySetVideoUpscaleSharpness(IADLXVideoUpscale* upscale, int sharpness)
        {
            SetVideoUpscaleSharpness(upscale, sharpness);
            return true;
        }

        internal IADLXVideoSuperResolution* GetVideoSuperResolutionNative(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            IADLXVideoSuperResolution* vsr = null;
            var result = _services.Get()->GetVideoSuperResolution(gpu, &vsr);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || vsr == null)
                return null;
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get video super resolution");

            return vsr; // caller wraps/disposes
        }

        /// <summary>
        /// Tries to get the native video super resolution interface; returns false when unsupported.
        /// </summary>
        internal bool TryGetVideoSuperResolutionNative(IADLXGPU* gpu, out IADLXVideoSuperResolution* vsr)
        {
            vsr = GetVideoSuperResolutionNative(gpu);
            return vsr != null;
        }

        internal VideoSuperResolutionDto GetVideoSuperResolution(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            var native = GetVideoSuperResolutionNative(gpu);
            if (native == null) return default;
            using var vsr = new ComPtr<IADLXVideoSuperResolution>(native);
            return new VideoSuperResolutionDto(vsr.Get());
        }

        /// <summary>
        /// Tries to query video super resolution info; returns false when unsupported.
        /// </summary>
        internal bool TryGetVideoSuperResolution(IADLXGPU* gpu, out VideoSuperResolutionDto info)
        {
            info = GetVideoSuperResolution(gpu);
            return true;
        }

        internal void SetVideoSuperResolutionEnabled(IADLXVideoSuperResolution* vsr, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (vsr == null) throw new ArgumentNullException(nameof(vsr));

            var result = vsr->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video super resolution enabled");
        }

        /// <summary>Tries to set video super resolution enabled; returns false when the feature is unsupported.</summary>
        internal bool TrySetVideoSuperResolutionEnabled(IADLXVideoSuperResolution* vsr, bool enable)
        {
            SetVideoSuperResolutionEnabled(vsr, enable);
            return true;
        }

        // =====================================================================
        // Public per-GPU overloads (by unique id)
        // =====================================================================

        /// <summary>Gets video upscale info for the GPU with the specified unique id.</summary>
        public VideoUpscaleDto GetVideoUpscale(int gpuUniqueId)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return WithGpuByUniqueId(gpuUniqueId, ptrGpu => GetVideoUpscale((IADLXGPU*)ptrGpu));
        }

        /// <summary>Tries to get video upscale info for the GPU with the specified unique id.</summary>
        public bool TryGetVideoUpscale(int gpuUniqueId, out VideoUpscaleDto info)
        {
            info = GetVideoUpscale(gpuUniqueId);
            return true;
        }

        /// <summary>Sets video upscale enabled state for the GPU with the specified unique id.</summary>
        public void SetVideoUpscaleEnabled(int gpuUniqueId, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            WithGpuByUniqueId(gpuUniqueId, ptrGpu =>
            {
                IADLXVideoUpscale* upscale = null;
                var r = _services.Get()->GetVideoUpscale((IADLXGPU*)ptrGpu, &upscale);
                if (r == ADLX_RESULT.ADLX_NOT_SUPPORTED || upscale == null) return 0;
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to get video upscale for GPU");
                using var u = new ComPtr<IADLXVideoUpscale>(upscale);
                var r2 = u.Get()->SetEnabled(enable ? (byte)1 : (byte)0);
                if (r2 != ADLX_RESULT.ADLX_OK) throw new ADLXException(r2, "Failed to set video upscale enabled");
                return 0;
            });
        }

        /// <summary>Tries to set video upscale enabled state for the GPU with the specified unique id.</summary>
        public bool TrySetVideoUpscaleEnabled(int gpuUniqueId, bool enable)
        {
            SetVideoUpscaleEnabled(gpuUniqueId, enable);
            return true;
        }

        /// <summary>Sets video upscale sharpness for the GPU with the specified unique id.</summary>
        public void SetVideoUpscaleSharpness(int gpuUniqueId, int sharpness)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            WithGpuByUniqueId(gpuUniqueId, ptrGpu =>
            {
                IADLXVideoUpscale* upscale = null;
                var r = _services.Get()->GetVideoUpscale((IADLXGPU*)ptrGpu, &upscale);
                if (r == ADLX_RESULT.ADLX_NOT_SUPPORTED || upscale == null) return 0;
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to get video upscale for GPU");
                using var u = new ComPtr<IADLXVideoUpscale>(upscale);
                var r2 = u.Get()->SetSharpness(sharpness);
                if (r2 != ADLX_RESULT.ADLX_OK) throw new ADLXException(r2, "Failed to set video upscale sharpness");
                return 0;
            });
        }

        /// <summary>Tries to set video upscale sharpness for the GPU with the specified unique id.</summary>
        public bool TrySetVideoUpscaleSharpness(int gpuUniqueId, int sharpness)
        {
            SetVideoUpscaleSharpness(gpuUniqueId, sharpness);
            return true;
        }

        /// <summary>Gets video super resolution info for the GPU with the specified unique id.</summary>
        public VideoSuperResolutionDto GetVideoSuperResolution(int gpuUniqueId)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return WithGpuByUniqueId(gpuUniqueId, ptrGpu => GetVideoSuperResolution((IADLXGPU*)ptrGpu));
        }

        /// <summary>Tries to get video super resolution info for the GPU with the specified unique id.</summary>
        public bool TryGetVideoSuperResolution(int gpuUniqueId, out VideoSuperResolutionDto info)
        {
            info = GetVideoSuperResolution(gpuUniqueId);
            return true;
        }

        /// <summary>Sets video super resolution enabled state for the GPU with the specified unique id.</summary>
        public void SetVideoSuperResolutionEnabled(int gpuUniqueId, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            WithGpuByUniqueId(gpuUniqueId, ptrGpu =>
            {
                IADLXVideoSuperResolution* vsr = null;
                var r = _services.Get()->GetVideoSuperResolution((IADLXGPU*)ptrGpu, &vsr);
                if (r == ADLX_RESULT.ADLX_NOT_SUPPORTED || vsr == null) return 0;
                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to get video super resolution for GPU");
                using var v = new ComPtr<IADLXVideoSuperResolution>(vsr);
                var r2 = v.Get()->SetEnabled(enable ? (byte)1 : (byte)0);
                if (r2 != ADLX_RESULT.ADLX_OK) throw new ADLXException(r2, "Failed to set video super resolution enabled");
                return 0;
            });
        }

        /// <summary>Tries to set video super resolution enabled state for the GPU with the specified unique id.</summary>
        public bool TrySetVideoSuperResolutionEnabled(int gpuUniqueId, bool enable)
        {
            SetVideoSuperResolutionEnabled(gpuUniqueId, enable);
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
    public readonly struct VideoUpscaleDto
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }
        public int Sharpness { get; init; }
        public IntRangeDto SharpnessRange { get; init; }

        [JsonConstructor]
        public VideoUpscaleDto(bool isSupported, bool isEnabled, int sharpness, IntRangeDto sharpnessRange)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
            Sharpness = sharpness;
            SharpnessRange = sharpnessRange;
        }

        internal unsafe VideoUpscaleDto(IADLXVideoUpscale* pUpscale)
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
            SharpnessRange = IntRangeDto.FromNative(range);
        }
    }

    public readonly struct VideoSuperResolutionDto
    {
        public bool IsSupported { get; init; }
        public bool IsEnabled { get; init; }

        [JsonConstructor]
        public VideoSuperResolutionDto(bool isSupported, bool isEnabled)
        {
            IsSupported = isSupported;
            IsEnabled = isEnabled;
        }

        internal unsafe VideoSuperResolutionDto(IADLXVideoSuperResolution* pVsr)
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
