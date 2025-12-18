using System;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper wrapper over IADLXSystem exposing per-feature service accessors and GPU enumeration.
    /// </summary>
    public sealed unsafe class ADLXSystemServicesHelper : IDisposable
    {
        private ComPtr<IADLXSystem> _system;
        private bool _disposed;

        public ADLXSystemServicesHelper(IADLXSystem* system, bool addRef = true)
        {
            if (system == null) throw new ArgumentNullException(nameof(system));
            if (addRef)
            {
                ADLXHelpers.AddRefInterface((IntPtr)system);
            }
            _system = new ComPtr<IADLXSystem>(system);
        }

        public IADLXSystem* GetSystemServicesNative()
        {
            ThrowIfDisposed();
            return _system.Get();
        }

        public AdlxInterfaceHandle GetSystemServices()
        {
            ThrowIfDisposed();
            var ptr = _system.Get();
            ADLXHelpers.AddRefInterface((IntPtr)ptr);
            return AdlxInterfaceHandle.From(ptr, addRef: false);
        }

        public IADLXDisplayServices* GetDisplayServicesNative()
        {
            ThrowIfDisposed();
            IADLXDisplayServices* services = null;
            var result = _system.Get()->GetDisplaysServices(&services);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get display services");
            return services;
        }

        public AdlxInterfaceHandle GetDisplayServices()
        {
            return AdlxInterfaceHandle.From(GetDisplayServicesNative(), addRef: false);
        }

        public IADLXDesktopServices* GetDesktopServicesNative()
        {
            ThrowIfDisposed();
            IADLXDesktopServices* services = null;
            var result = _system.Get()->GetDesktopsServices(&services);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get desktop services");
            return services;
        }

        public AdlxInterfaceHandle GetDesktopServices()
        {
            return AdlxInterfaceHandle.From(GetDesktopServicesNative(), addRef: false);
        }

        public IADLX3DSettingsServices* Get3DSettingsServicesNative()
        {
            ThrowIfDisposed();
            IADLX3DSettingsServices* services = null;
            var result = _system.Get()->Get3DSettingsServices(&services);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get 3D settings services");
            return services;
        }

        public AdlxInterfaceHandle Get3DSettingsServices()
        {
            return AdlxInterfaceHandle.From(Get3DSettingsServicesNative(), addRef: false);
        }

        public IADLXGPUTuningServices* GetGPUTuningServicesNative()
        {
            ThrowIfDisposed();
            IADLXGPUTuningServices* services = null;
            var result = _system.Get()->GetGPUTuningServices(&services);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU tuning services");
            return services;
        }

        public AdlxInterfaceHandle GetGPUTuningServices()
        {
            return AdlxInterfaceHandle.From(GetGPUTuningServicesNative(), addRef: false);
        }

        public IADLXPerformanceMonitoringServices* GetPerformanceMonitoringServicesNative()
        {
            ThrowIfDisposed();
            IADLXPerformanceMonitoringServices* services = null;
            var result = _system.Get()->GetPerformanceMonitoringServices(&services);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get performance monitoring services");
            return services;
        }

        public AdlxInterfaceHandle GetPerformanceMonitoringServices()
        {
            return AdlxInterfaceHandle.From(GetPerformanceMonitoringServicesNative(), addRef: false);
        }

        public IADLXPowerTuningServices* GetPowerTuningServicesNative()
        {
            ThrowIfDisposed();
            if (!ADLXHelpers.TryQueryInterface((IntPtr)_system.Get(), nameof(IADLXSystem2), out var pSystem2))
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Power tuning services not supported by this ADLX system");

            using var system2 = new ComPtr<IADLXSystem2>((IADLXSystem2*)pSystem2);
            IADLXPowerTuningServices* services = null;
            var result = system2.Get()->GetPowerTuningServices(&services);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get power tuning services");
            return services;
        }

        public AdlxInterfaceHandle GetPowerTuningServices()
        {
            return AdlxInterfaceHandle.From(GetPowerTuningServicesNative(), addRef: false);
        }

        public IADLXMultimediaServices* GetMultimediaServicesNative()
        {
            ThrowIfDisposed();
            if (!ADLXHelpers.TryQueryInterface((IntPtr)_system.Get(), nameof(IADLXSystem2), out var pSystem2))
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Multimedia services not supported by this ADLX system");

            using var system2 = new ComPtr<IADLXSystem2>((IADLXSystem2*)pSystem2);
            IADLXMultimediaServices* services = null;
            var result = system2.Get()->GetMultimediaServices(&services);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get multimedia services");
            return services;
        }

        public AdlxInterfaceHandle GetMultimediaServices()
        {
            return AdlxInterfaceHandle.From(GetMultimediaServicesNative(), addRef: false);
        }

        public AdlxInterfaceHandle[] EnumerateGPUHandles()
        {
            ThrowIfDisposed();
            IADLXGPUList* pGpuList = null;
            var result = _system.Get()->GetGPUs(&pGpuList);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate GPUs");

            using var gpuList = new ComPtr<IADLXGPUList>(pGpuList);
            var count = gpuList.Get()->Size();
            var handles = new AdlxInterfaceHandle[count];

            for (uint i = 0; i < count; i++)
            {
                IADLXGPU* pGpu = null;
                gpuList.Get()->At(i, &pGpu);
                handles[i] = AdlxInterfaceHandle.From(pGpu, addRef: false);
            }

            return handles;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _system.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ADLXSystemServicesHelper));
            }
        }

        ~ADLXSystemServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }
    }
}
