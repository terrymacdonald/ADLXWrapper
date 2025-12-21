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

        public AdlxInterfaceHandle GetMultimediaServicesHandle()
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

        public void SetVideoUpscaleEnabled(IADLXVideoUpscale* upscale, bool enable)
        {
            ThrowIfDisposed();
            if (upscale == null) throw new ArgumentNullException(nameof(upscale));

            var result = upscale->SetEnabled(enable ? (byte)1 : (byte)0);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale enabled");
        }

        public void SetVideoUpscaleSharpness(IADLXVideoUpscale* upscale, int sharpness)
        {
            ThrowIfDisposed();
            if (upscale == null) throw new ArgumentNullException(nameof(upscale));

            var result = upscale->SetSharpness(sharpness);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to set video upscale sharpness");
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

        public void SetVideoSuperResolutionEnabled(IADLXVideoSuperResolution* vsr, bool enable)
        {
            ThrowIfDisposed();
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

        public sealed unsafe class MultimediaEventListenerHandle : SafeHandle
        {
            public delegate bool MultimediaChangedCallback(IntPtr pEvent);

            private static readonly ConcurrentDictionary<IntPtr, MultimediaChangedCallback> _map = new();
            private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnMultimediaChanged;
            private readonly GCHandle _gcHandle;
            private readonly IntPtr _vtbl;

            private MultimediaEventListenerHandle(MultimediaChangedCallback cb) : base(IntPtr.Zero, true)
            {
                _gcHandle = GCHandle.Alloc(cb);
                _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
                Marshal.WriteIntPtr(_vtbl, _thunkPtr);

                var inst = Marshal.AllocHGlobal(IntPtr.Size);
                Marshal.WriteIntPtr(inst, _vtbl);
                handle = inst;
                _map[inst] = cb;
            }

            public static MultimediaEventListenerHandle Create(MultimediaChangedCallback cb)
            {
                if (cb == null) throw new ArgumentNullException(nameof(cb));
                return new MultimediaEventListenerHandle(cb);
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

            [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
            private static byte OnMultimediaChanged(IntPtr pThis, IntPtr pEvent)
            {
                if (_map.TryGetValue(pThis, out var cb))
                {
                    return cb(pEvent) ? (byte)1 : (byte)0;
                }
                return 0;
            }
        }
        #endregion

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

