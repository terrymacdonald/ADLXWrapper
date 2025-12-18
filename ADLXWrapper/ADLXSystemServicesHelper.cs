using System;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper wrapper over IADLXSystem exposing per-feature service accessors and GPU enumeration.
    /// </summary>
    public sealed unsafe class ADLXSystemServicesHelper : IDisposable
    {
        private ComPtr<IADLXSystem> _system;
        private ComPtr<IADLXSystem1>? _system1;
        private ComPtr<IADLXSystem2>? _system2;
        private ComPtr<IADLXDisplayServices>? _displayServices;
        private ComPtr<IADLXDesktopServices>? _desktopServices;
        private ComPtr<IADLX3DSettingsServices>? _threeDSettingsServices;
        private ComPtr<IADLXGPUTuningServices>? _gpuTuningServices;
        private ComPtr<IADLXPerformanceMonitoringServices>? _performanceMonitoringServices;
        private ComPtr<IADLXPowerTuningServices>? _powerTuningServices;
        private ComPtr<IADLXMultimediaServices>? _multimediaServices;
        private ComPtr<IADLXGPUsChangedHandling>? _gpusChangedHandling;
        private ComPtr<IADLXGPUAppsListChangedHandling>? _gpuAppsListChangedHandling;
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

            // Prefer the most capable interface available.
            if (TryGetSystem2(out var system2) && system2 != null)
            {
                return (IADLXSystem*)system2;
            }

            if (TryGetSystem1(out var system1) && system1 != null)
            {
                return (IADLXSystem*)system1;
            }

            return _system.Get();
        }

        public AdlxInterfaceHandle GetSystemServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(GetSystemServicesNative(), addRef: true);
        }

        public IADLXDisplayServices* GetDisplayServicesNative()
        {
            ThrowIfDisposed();
            return EnsureDisplayServices();
        }

        public AdlxInterfaceHandle GetDisplayServices()
        {
            return AdlxInterfaceHandle.From(GetDisplayServicesNative(), addRef: true);
        }

        public IADLXDesktopServices* GetDesktopServicesNative()
        {
            ThrowIfDisposed();
            return EnsureDesktopServices();
        }

        public AdlxInterfaceHandle GetDesktopServices()
        {
            return AdlxInterfaceHandle.From(GetDesktopServicesNative(), addRef: true);
        }

        public IADLX3DSettingsServices* Get3DSettingsServicesNative()
        {
            ThrowIfDisposed();
            return Ensure3DSettingsServices();
        }

        public AdlxInterfaceHandle Get3DSettingsServices()
        {
            return AdlxInterfaceHandle.From(Get3DSettingsServicesNative(), addRef: true);
        }

        public IADLXGPUTuningServices* GetGPUTuningServicesNative()
        {
            ThrowIfDisposed();
            return EnsureGPUTuningServices();
        }

        public AdlxInterfaceHandle GetGPUTuningServices()
        {
            return AdlxInterfaceHandle.From(GetGPUTuningServicesNative(), addRef: true);
        }

        public IADLXPerformanceMonitoringServices* GetPerformanceMonitoringServicesNative()
        {
            ThrowIfDisposed();
            return EnsurePerformanceMonitoringServices();
        }

        public AdlxInterfaceHandle GetPerformanceMonitoringServices()
        {
            return AdlxInterfaceHandle.From(GetPerformanceMonitoringServicesNative(), addRef: true);
        }

        public IADLXPowerTuningServices* GetPowerTuningServicesNative()
        {
            ThrowIfDisposed();
            return EnsurePowerTuningServices();
        }

        public AdlxInterfaceHandle GetPowerTuningServices()
        {
            return AdlxInterfaceHandle.From(GetPowerTuningServicesNative(), addRef: true);
        }

        public IADLXMultimediaServices* GetMultimediaServicesNative()
        {
            ThrowIfDisposed();
            return EnsureMultimediaServices();
        }

        public AdlxInterfaceHandle GetMultimediaServices()
        {
            return AdlxInterfaceHandle.From(GetMultimediaServicesNative(), addRef: true);
        }

        public IADLXGPUsChangedHandling* GetGPUsChangedHandlingNative()
        {
            ThrowIfDisposed();
            return EnsureGPUsChangedHandling();
        }

        public AdlxInterfaceHandle GetGPUsChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetGPUsChangedHandlingNative(), addRef: true);
        }

        public IADLXGPUAppsListChangedHandling* GetGPUAppsListChangedHandlingNative()
        {
            ThrowIfDisposed();
            return EnsureGPUAppsListChangedHandling();
        }

        public AdlxInterfaceHandle GetGPUAppsListChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetGPUAppsListChangedHandlingNative(), addRef: true);
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
            _gpuAppsListChangedHandling?.Dispose();
            _gpusChangedHandling?.Dispose();
            _multimediaServices?.Dispose();
            _powerTuningServices?.Dispose();
            _performanceMonitoringServices?.Dispose();
            _gpuTuningServices?.Dispose();
            _threeDSettingsServices?.Dispose();
            _desktopServices?.Dispose();
            _displayServices?.Dispose();
            _system2?.Dispose();
            _system1?.Dispose();
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

        private IADLXDisplayServices* EnsureDisplayServices()
        {
            if (_displayServices.HasValue)
                return _displayServices.Value.Get();

            IADLXDisplayServices* services = null;
            var result = _system.Get()->GetDisplaysServices(&services);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get display services");

            _displayServices = new ComPtr<IADLXDisplayServices>(services);
            return services;
        }

        private IADLXDesktopServices* EnsureDesktopServices()
        {
            if (_desktopServices.HasValue)
                return _desktopServices.Value.Get();

            IADLXDesktopServices* services = null;
            var result = _system.Get()->GetDesktopsServices(&services);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get desktop services");

            _desktopServices = new ComPtr<IADLXDesktopServices>(services);
            return services;
        }

        private IADLX3DSettingsServices* Ensure3DSettingsServices()
        {
            if (_threeDSettingsServices.HasValue)
                return _threeDSettingsServices.Value.Get();

            IADLX3DSettingsServices* services = null;
            var result = _system.Get()->Get3DSettingsServices(&services);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "3D settings services not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get 3D settings services");

            _threeDSettingsServices = new ComPtr<IADLX3DSettingsServices>(services);
            return services;
        }

        private IADLXGPUTuningServices* EnsureGPUTuningServices()
        {
            if (_gpuTuningServices.HasValue)
                return _gpuTuningServices.Value.Get();

            IADLXGPUTuningServices* services = null;
            var result = _system.Get()->GetGPUTuningServices(&services);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU tuning services not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU tuning services");

            _gpuTuningServices = new ComPtr<IADLXGPUTuningServices>(services);
            return services;
        }

        private IADLXPerformanceMonitoringServices* EnsurePerformanceMonitoringServices()
        {
            if (_performanceMonitoringServices.HasValue)
                return _performanceMonitoringServices.Value.Get();

            IADLXPerformanceMonitoringServices* services = null;
            var result = _system.Get()->GetPerformanceMonitoringServices(&services);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Performance monitoring services not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get performance monitoring services");

            _performanceMonitoringServices = new ComPtr<IADLXPerformanceMonitoringServices>(services);
            return services;
        }

        private IADLXPowerTuningServices* EnsurePowerTuningServices()
        {
            if (_powerTuningServices.HasValue)
                return _powerTuningServices.Value.Get();

            IADLXPowerTuningServices* services = null;
            ADLX_RESULT result;

            if (TryGetSystem2(out var system2))
            {
                result = system2->GetPowerTuningServices(&services);
            }
            else
            {
                var system1 = GetSystem1();
                result = system1->GetPowerTuningServices(&services);
            }

            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Power tuning services not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get power tuning services");

            _powerTuningServices = new ComPtr<IADLXPowerTuningServices>(services);
            return services;
        }

        private IADLXMultimediaServices* EnsureMultimediaServices()
        {
            if (_multimediaServices.HasValue)
                return _multimediaServices.Value.Get();

            var system2 = GetSystem2();
            IADLXMultimediaServices* services = null;
            var result = system2->GetMultimediaServices(&services);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Multimedia services not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get multimedia services");

            _multimediaServices = new ComPtr<IADLXMultimediaServices>(services);
            return services;
        }

        private IADLXGPUsChangedHandling* EnsureGPUsChangedHandling()
        {
            if (_gpusChangedHandling.HasValue)
                return _gpusChangedHandling.Value.Get();

            IADLXGPUsChangedHandling* handling = null;
            var result = _system.Get()->GetGPUsChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU change handling");

            _gpusChangedHandling = new ComPtr<IADLXGPUsChangedHandling>(handling);
            return handling;
        }

        private IADLXGPUAppsListChangedHandling* EnsureGPUAppsListChangedHandling()
        {
            if (_gpuAppsListChangedHandling.HasValue)
                return _gpuAppsListChangedHandling.Value.Get();

            var system2 = GetSystem2();
            IADLXGPUAppsListChangedHandling* handling = null;
            var result = system2->GetGPUAppsListChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU applications list handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get GPU applications list handling");

            _gpuAppsListChangedHandling = new ComPtr<IADLXGPUAppsListChangedHandling>(handling);
            return handling;
        }

        private IADLXSystem1* GetSystem1()
        {
            if (_system1.HasValue)
                return _system1.Value.Get();

            if (!ADLXHelpers.TryQueryInterface((IntPtr)_system.Get(), nameof(IADLXSystem1), out var pSystem1))
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Power tuning services not supported by this ADLX system");

            _system1 = new ComPtr<IADLXSystem1>((IADLXSystem1*)pSystem1);
            return _system1.Value.Get();
        }

        private bool TryGetSystem1(out IADLXSystem1* system1)
        {
            if (_system1.HasValue)
            {
                system1 = _system1.Value.Get();
                return system1 != null;
            }

            if (ADLXHelpers.TryQueryInterface((IntPtr)_system.Get(), nameof(IADLXSystem1), out var pSystem1))
            {
                _system1 = new ComPtr<IADLXSystem1>((IADLXSystem1*)pSystem1);
                system1 = _system1.Value.Get();
                return true;
            }

            system1 = null;
            return false;
        }

        private IADLXSystem2* GetSystem2()
        {
            if (_system2.HasValue)
                return _system2.Value.Get();

            if (!ADLXHelpers.TryQueryInterface((IntPtr)_system.Get(), nameof(IADLXSystem2), out var pSystem2))
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Extended system services not supported by this ADLX system");

            _system2 = new ComPtr<IADLXSystem2>((IADLXSystem2*)pSystem2);
            return _system2.Value.Get();
        }

        private bool TryGetSystem2(out IADLXSystem2* system2)
        {
            if (_system2.HasValue)
            {
                system2 = _system2.Value.Get();
                return system2 != null;
            }

            if (ADLXHelpers.TryQueryInterface((IntPtr)_system.Get(), nameof(IADLXSystem2), out var pSystem2))
            {
                _system2 = new ComPtr<IADLXSystem2>((IADLXSystem2*)pSystem2);
                system2 = _system2.Value.Get();
                return true;
            }

            system2 = null;
            return false;
        }
    }
}
