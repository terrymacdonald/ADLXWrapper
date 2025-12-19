using System;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLXGPUTuningServices selecting the highest available interface and exposing change handling helpers.
    /// </summary>
    public sealed unsafe class ADLXGPUTuningServicesHelper : IDisposable
    {
        private ComPtr<IADLXGPUTuningServices> _services;
        private ComPtr<IADLXGPUTuningServices1>? _services1;
        private ComPtr<IADLXGPUTuningChangedHandling>? _changedHandling;
        private bool _disposed;

        public ADLXGPUTuningServicesHelper(IADLXGPUTuningServices* services, bool addRef = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLXGPUTuningServices>(services);
            TryUpgradeServices(services);
        }

        public IADLXGPUTuningServices* GetGPUTuningServicesNative()
        {
            ThrowIfDisposed();
            return GetHighestServices();
        }

        public AdlxInterfaceHandle GetGPUTuningServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(GetGPUTuningServicesNative(), addRef: true);
        }

        public IADLXGPUTuningChangedHandling* GetGPUTuningChangedHandlingNative()
        {
            ThrowIfDisposed();
            if (_changedHandling.HasValue)
                return _changedHandling.Value.Get();

            IADLXGPUTuningChangedHandling* handling = null;
            var result = GetHighestServices()->GetGPUTuningChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU tuning change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU tuning change handling");

            _changedHandling = new ComPtr<IADLXGPUTuningChangedHandling>(handling);
            return handling;
        }

        public AdlxInterfaceHandle GetGPUTuningChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetGPUTuningChangedHandlingNative(), addRef: true);
        }

        public bool IsAutoTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedAutoTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Auto tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query auto tuning support");
            return supported;
        }

        public bool IsPresetTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedPresetTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Preset tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query preset tuning support");
            return supported;
        }

        public bool IsManualGfxTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedManualGFXTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual GFX tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query manual GFX tuning support");
            return supported;
        }

        public bool IsManualVramTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedManualVRAMTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual VRAM tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query manual VRAM tuning support");
            return supported;
        }

        public bool IsManualFanTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedManualFanTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual fan tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query manual fan tuning support");
            return supported;
        }

        public bool IsManualPowerTuningSupported(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));

            bool supported = false;
            var result = GetHighestServices()->IsSupportedManualPowerTuning(gpu, &supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Manual power tuning not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query manual power tuning support");
            return supported;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _changedHandling?.Dispose();
            _services1?.Dispose();
            _services.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ADLXGPUTuningServicesHelper));
        }

        ~ADLXGPUTuningServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }

        private void TryUpgradeServices(IADLXGPUTuningServices* services)
        {
            if (services == null) return;

            if (ADLXUtils.TryQueryInterface((IntPtr)services, nameof(IADLXGPUTuningServices1), out var p1))
            {
                _services1 = new ComPtr<IADLXGPUTuningServices1>((IADLXGPUTuningServices1*)p1);
            }
        }

        private IADLXGPUTuningServices* GetHighestServices()
        {
            if (_services1.HasValue)
                return (IADLXGPUTuningServices*)_services1.Value.Get();
            return _services.Get();
        }
    }
}

