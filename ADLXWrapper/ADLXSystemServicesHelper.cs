using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper wrapper over IADLXSystem exposing per-feature service accessors and GPU enumeration.
    /// </summary>
    public sealed unsafe class ADLXSystemServicesHelper : IDisposable
    {
        private readonly IADLXSystem* _system;
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

        /// <summary>
        /// Creates a system-services helper from an ADLX system pointer.
        /// </summary>
        /// <param name="system">Native ADLX system pointer.</param>
        /// <param name="addRef">Ignored; kept for API compatibility. IADLXSystem is not ref-counted.</param>
        public ADLXSystemServicesHelper(IADLXSystem* system, bool addRef = false)
        {
            if (system == null) throw new ArgumentNullException(nameof(system));
            // IADLXSystem is a singleton and not ref-counted; do not AddRef/Release.
            _system = system;
        }

        /// <summary>
        /// Returns the most capable available system interface (IADLXSystem2, then 1, else base).
        /// Caller must not Release the returned pointer.
        /// </summary>
        /// <returns>Native system interface pointer owned by this helper.</returns>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
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

            return _system;
        }

        /// <summary>
        /// Returns an AddRef'd handle to the system services interface for external ownership.
        /// </summary>
        /// <returns>Managed handle to the native system interface.</returns>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle GetSystemServicesHandle()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.FromNonRefCounted(GetSystemServicesNative());
        }

        /// <summary>
        /// Gets the native display services interface, querying lazily if needed.
        /// </summary>
        /// <returns>Native display services pointer owned by this helper.</returns>
        /// <exception cref="ADLXException">If display services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IADLXDisplayServices* GetDisplayServicesNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return EnsureDisplayServices();
            }
        }

        /// <summary>
        /// Returns an AddRef'd handle to the display services interface for external ownership.
        /// </summary>
        /// <returns>Managed handle to the native display services interface.</returns>
        /// <exception cref="ADLXException">If display services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle GetDisplayServicesHandle()
        {
            return AdlxInterfaceHandle.From(GetDisplayServicesNative(), addRef: true);
        }

        /// <summary>
        /// Creates a managed display-services helper with shared system lifetime.
        /// </summary>
        /// <returns>Display services helper wrapping native interfaces.</returns>
        /// <exception cref="ADLXException">If display/desktop services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public ADLXDisplayServicesHelper GetDisplayServices()
        {
            ThrowIfDisposed();
            var displayServices = GetDisplayServicesNative();
            var desktopServices = GetDesktopServicesNative();
            ADLXUtils.AddRefInterface((IntPtr)displayServices);
            ADLXUtils.AddRefInterface((IntPtr)desktopServices);
            return new ADLXDisplayServicesHelper(displayServices, desktopServices, addRefDisplayServices: false, addRefDesktopServices: false);
        }

        /// <summary>
        /// Gets the native desktop services interface, querying lazily if needed.
        /// </summary>
        /// <returns>Native desktop services pointer owned by this helper.</returns>
        /// <exception cref="ADLXException">If desktop services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IADLXDesktopServices* GetDesktopServicesNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return EnsureDesktopServices();
            }
        }

        /// <summary>
        /// Returns an AddRef'd handle to the desktop services interface for external ownership.
        /// </summary>
        /// <returns>Managed handle to the native desktop services interface.</returns>
        /// <exception cref="ADLXException">If desktop services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle GetDesktopServicesHandle()
        {
            return AdlxInterfaceHandle.From(GetDesktopServicesNative(), addRef: true);
        }

        /// <summary>
        /// Creates a managed desktop-services helper with shared system lifetime.
        /// </summary>
        /// <returns>Desktop services helper wrapping native interfaces.</returns>
        /// <exception cref="ADLXException">If desktop/display services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public ADLXDesktopServicesHelper GetDesktopServices()
        {
            ThrowIfDisposed();
            var desktopServices = GetDesktopServicesNative();
            var displayServices = GetDisplayServicesNative();
            ADLXUtils.AddRefInterface((IntPtr)desktopServices);
            ADLXUtils.AddRefInterface((IntPtr)displayServices);
            return new ADLXDesktopServicesHelper(desktopServices, displayServices, addRefDesktopServices: false);
        }

        /// <summary>
        /// Gets the native 3D settings services interface, querying lazily if needed.
        /// </summary>
        /// <returns>Native 3D settings services pointer owned by this helper.</returns>
        /// <exception cref="ADLXException">If 3D settings services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IADLX3DSettingsServices* Get3DSettingsServicesNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return Ensure3DSettingsServices();
            }
        }

        /// <summary>
        /// Returns an AddRef'd handle to the 3D settings services interface for external ownership.
        /// </summary>
        /// <returns>Managed handle to the native 3D settings services interface.</returns>
        /// <exception cref="ADLXException">If 3D settings services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle Get3DSettingsServicesHandle()
        {
            return AdlxInterfaceHandle.From(Get3DSettingsServicesNative(), addRef: true);
        }

        /// <summary>
        /// Creates a managed 3D settings helper with shared system lifetime.
        /// </summary>
        /// <returns>3D settings services helper wrapping native interfaces.</returns>
        /// <exception cref="ADLXException">If 3D settings services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public ADLX3DSettingsServicesHelper Get3DSettingsServices()
        {
            ThrowIfDisposed();
            var services = Get3DSettingsServicesNative();
            ADLXUtils.AddRefInterface((IntPtr)services);
            return new ADLX3DSettingsServicesHelper(services, addRef: false);
        }

        /// <summary>
        /// Gets the native GPU tuning services interface, querying lazily if needed.
        /// </summary>
        /// <returns>Native GPU tuning services pointer owned by this helper.</returns>
        /// <exception cref="ADLXException">If GPU tuning services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IADLXGPUTuningServices* GetGPUTuningServicesNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return EnsureGPUTuningServices();
            }
        }

        /// <summary>
        /// Returns an AddRef'd handle to the GPU tuning services interface for external ownership.
        /// </summary>
        /// <returns>Managed handle to the native GPU tuning services interface.</returns>
        /// <exception cref="ADLXException">If GPU tuning services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle GetGPUTuningServicesHandle()
        {
            return AdlxInterfaceHandle.From(GetGPUTuningServicesNative(), addRef: true);
        }

        /// <summary>
        /// Creates a managed GPU tuning helper with shared system lifetime.
        /// </summary>
        /// <returns>GPU tuning services helper wrapping native interfaces.</returns>
        /// <exception cref="ADLXException">If GPU tuning services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public ADLXGPUTuningServicesHelper GetGPUTuningServices()
        {
            ThrowIfDisposed();
            var services = GetGPUTuningServicesNative();
            ADLXUtils.AddRefInterface((IntPtr)services);
            return new ADLXGPUTuningServicesHelper(services, addRef: false);
        }

        /// <summary>
        /// Gets the native performance monitoring services interface, querying lazily if needed.
        /// </summary>
        /// <returns>Native performance monitoring services pointer owned by this helper.</returns>
        /// <exception cref="ADLXException">If performance monitoring services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IADLXPerformanceMonitoringServices* GetPerformanceMonitoringServicesNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return EnsurePerformanceMonitoringServices();
            }
        }

        /// <summary>
        /// Returns an AddRef'd handle to the performance monitoring services interface for external ownership.
        /// </summary>
        /// <returns>Managed handle to the native performance monitoring services interface.</returns>
        /// <exception cref="ADLXException">If performance monitoring services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle GetPerformanceMonitoringServicesHandle()
        {
            return AdlxInterfaceHandle.From(GetPerformanceMonitoringServicesNative(), addRef: true);
        }

        /// <summary>
        /// Creates a managed performance monitoring helper with shared system lifetime.
        /// </summary>
        /// <returns>Performance monitoring services helper wrapping native interfaces.</returns>
        /// <exception cref="ADLXException">If performance monitoring services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public ADLXPerformanceMonitoringServicesHelper GetPerformanceMonitoringServices()
        {
            ThrowIfDisposed();
            var services = GetPerformanceMonitoringServicesNative();
            ADLXUtils.AddRefInterface((IntPtr)services);
            return new ADLXPerformanceMonitoringServicesHelper(services, addRef: false);
        }

        /// <summary>
        /// Gets the native power tuning services interface, querying lazily if needed.
        /// </summary>
        /// <returns>Native power tuning services pointer owned by this helper.</returns>
        /// <exception cref="ADLXException">If power tuning services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IADLXPowerTuningServices* GetPowerTuningServicesNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return EnsurePowerTuningServices();
            }
        }

        /// <summary>
        /// Returns an AddRef'd handle to the power tuning services interface for external ownership.
        /// </summary>
        /// <returns>Managed handle to the native power tuning services interface.</returns>
        /// <exception cref="ADLXException">If power tuning services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle GetPowerTuningServicesHandle()
        {
            return AdlxInterfaceHandle.From(GetPowerTuningServicesNative(), addRef: true);
        }

        /// <summary>
        /// Creates a managed power tuning helper with shared system lifetime.
        /// </summary>
        /// <returns>Power tuning services helper wrapping native interfaces.</returns>
        /// <exception cref="ADLXException">If power tuning services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public ADLXPowerTuningServicesHelper GetPowerTuningServices()
        {
            ThrowIfDisposed();
            var services = GetPowerTuningServicesNative();
            ADLXUtils.AddRefInterface((IntPtr)services);
            return new ADLXPowerTuningServicesHelper(services, addRef: false);
        }

        /// <summary>
        /// Gets the native multimedia services interface, querying lazily if needed.
        /// </summary>
        /// <returns>Native multimedia services pointer owned by this helper.</returns>
        /// <exception cref="ADLXException">If multimedia services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IADLXMultimediaServices* GetMultimediaServicesNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return EnsureMultimediaServices();
            }
        }

        /// <summary>
        /// Returns an AddRef'd handle to the multimedia services interface for external ownership.
        /// </summary>
        /// <returns>Managed handle to the native multimedia services interface.</returns>
        /// <exception cref="ADLXException">If multimedia services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle GetMultimediaServicesHandle()
        {
            return AdlxInterfaceHandle.From(GetMultimediaServicesNative(), addRef: true);
        }

        /// <summary>
        /// Creates a managed multimedia services helper with shared system lifetime.
        /// </summary>
        /// <returns>Multimedia services helper wrapping native interfaces.</returns>
        /// <exception cref="ADLXException">If multimedia services are unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public ADLXMultimediaServicesHelper GetMultimediaServices()
        {
            ThrowIfDisposed();
            var services = GetMultimediaServicesNative();
            ADLXUtils.AddRefInterface((IntPtr)services);
            return new ADLXMultimediaServicesHelper(services, addRef: false);
        }

        /// <summary>
        /// Gets the native GPU change handling interface (for GPU list/display change events).
        /// </summary>
        /// <returns>Native GPU change handling pointer owned by this helper.</returns>
        /// <exception cref="ADLXException">If change handling is unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IADLXGPUsChangedHandling* GetGPUsChangedHandlingNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return EnsureGPUsChangedHandling();
            }
        }

        /// <summary>
        /// Returns an AddRef'd handle to the GPU change handling interface for external ownership.
        /// </summary>
        /// <returns>Managed handle to the native GPU change handling interface.</returns>
        /// <exception cref="ADLXException">If change handling is unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle GetGPUsChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetGPUsChangedHandlingNative(), addRef: true);
        }

        /// <summary>
        /// Gets the native GPU applications list change handling interface (IADLXSystem2).
        /// </summary>
        /// <returns>Native GPU apps list change handling pointer owned by this helper.</returns>
        /// <exception cref="ADLXException">If the extended interface is unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IADLXGPUAppsListChangedHandling* GetGPUAppsListChangedHandlingNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
                return EnsureGPUAppsListChangedHandling();
            }
        }

        /// <summary>
        /// Returns an AddRef'd handle to the GPU applications list change handling interface for external ownership.
        /// </summary>
        /// <returns>Managed handle to the native GPU apps list change handling interface.</returns>
        /// <exception cref="ADLXException">If the extended interface is unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle GetGPUAppsListChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetGPUAppsListChangedHandlingNative(), addRef: true);
        }

        /// <summary>
        /// Enumerates GPU facades with display/desktop helpers wired in.
        /// </summary>
        /// <returns>List of managed GPU facades. Callers must Dispose each GPU when finished.</returns>
        /// <exception cref="ADLXException">If enumeration is unsupported or fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IReadOnlyList<ADLXGPU> EnumerateADLXGPUs()
        {
            ThrowIfDisposed();
            var displayServices = GetDisplayServicesNative();
            var desktopServices = GetDesktopServicesNative();

            IADLXGPUList* pGpuList = null;
            var result = _system->GetGPUs(&pGpuList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pGpuList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate GPUs");

            var facades = new List<ADLXGPU>();
            using var gpuList = new ComPtr<IADLXGPUList>(pGpuList);
            var count = gpuList.Get()->Size();
            for (uint i = 0; i < count; i++)
            {
                IADLXGPU* pGpu = null;
                gpuList.Get()->At(i, &pGpu);
                facades.Add(new ADLXGPU(pGpu, displayServices, desktopServices));
            }

            return facades;
        }

        /// <summary>
        /// Enumerates display facades via the display services helper.
        /// </summary>
        /// <returns>List of managed displays.</returns>
        /// <exception cref="ADLXException">If enumeration fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IReadOnlyList<ADLXDisplay> EnumerateDisplays()
        {
            ThrowIfDisposed();
            using var displayHelper = GetDisplayServices();
            return displayHelper.EnumerateDisplays();
        }

        /// <summary>
        /// Enumerates desktop facades via the desktop services helper.
        /// </summary>
        /// <returns>List of managed desktops.</returns>
        /// <exception cref="ADLXException">If enumeration fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IReadOnlyList<ADLXDesktop> EnumerateDesktops()
        {
            ThrowIfDisposed();
            using var desktopHelper = GetDesktopServices();
            return desktopHelper.EnumerateADLXDesktops();
        }

        /// <summary>
        /// Enumerates GPU native handles (AddRef'd for caller ownership).
        /// </summary>
        /// <returns>Array of native GPU interface handles.</returns>
        /// <exception cref="ADLXException">If enumeration fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public AdlxInterfaceHandle[] EnumerateGPUsHandle()
        {
            ThrowIfDisposed();
            IADLXGPUList* pGpuList = null;
            var result = _system->GetGPUs(&pGpuList);
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

        /// <summary>
        /// Enumerates GPU DTOs with identity and capability information.
        /// </summary>
        /// <returns>Sequence of GPU info records.</returns>
        /// <exception cref="ADLXException">If enumeration is unsupported or fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IEnumerable<GpuInfo> EnumerateGPUs()
        {
            ThrowIfDisposed();
            using var gpuList = new ComPtr<IADLXGPUList>(EnumerateGPUsNative());
            var count = gpuList.Get()->Size();
            var results = new List<GpuInfo>((int)count);
            for (uint i = 0; i < count; i++)
            {
                IADLXGPU* pGpu = null;
                gpuList.Get()->At(i, &pGpu);
                using var gpu = new ComPtr<IADLXGPU>(pGpu);
                results.Add(new GpuInfo(gpu.Get()));
            }

            return results;
        }

        /// <summary>
        /// Builds a GPU info DTO from a native GPU pointer.
        /// </summary>
        /// <param name="gpu">Native GPU pointer.</param>
        /// <returns>GPU info record.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="gpu"/> is null.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public GpuInfo GetGpuInfo(IADLXGPU* gpu)
        {
            ThrowIfDisposed();
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));
            return new GpuInfo(gpu);
        }

        /// <summary>
        /// Enumerates native GPU interfaces. Caller must dispose the returned list/entries.
        /// </summary>
        /// <returns>Native GPU list pointer.</returns>
        /// <exception cref="ADLXException">If enumeration is unsupported or fails.</exception>
        /// <exception cref="ObjectDisposedException">If the helper has been disposed.</exception>
        public IADLXGPUList* EnumerateGPUsNative()
        {
            ThrowIfDisposed();
            using (ADLXSync.EnterRead())
            {
            IADLXGPUList* pGpuList = null;
            var result = _system->GetGPUs(&pGpuList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pGpuList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate GPUs");

            return pGpuList; // caller must wrap/dispose
            }
        }

        /// <summary>
        /// Disposes cached COM pointers and releases references.
        /// </summary>
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
            var result = _system->GetDisplaysServices(&services);
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
            var result = _system->GetDesktopsServices(&services);
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
            var result = _system->Get3DSettingsServices(&services);
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
            var result = _system->GetGPUTuningServices(&services);
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
            var result = _system->GetPerformanceMonitoringServices(&services);
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
            var result = _system->GetGPUsChangedHandling(&handling);
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

            if (!ADLXUtils.TryQueryInterface((IntPtr)_system, nameof(IADLXSystem1), out var pSystem1))
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "IADLXSystem1 is not supported by this ADLX system");

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

            if (ADLXUtils.TryQueryInterface((IntPtr)_system, nameof(IADLXSystem1), out var pSystem1))
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

            if (!ADLXUtils.TryQueryInterface((IntPtr)_system, nameof(IADLXSystem2), out var pSystem2))
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

            if (ADLXUtils.TryQueryInterface((IntPtr)_system, nameof(IADLXSystem2), out var pSystem2))
            {
                _system2 = new ComPtr<IADLXSystem2>((IADLXSystem2*)pSystem2);
                system2 = _system2.Value.Get();
                return true;
            }

            system2 = null;
            return false;
        }
    }

    #region GPU DTO
    public readonly struct GpuInfo
    {
        public string Name { get; init; }
        public string VendorId { get; init; }
        public int UniqueId { get; init; }
        public uint TotalVRAM { get; init; }
        public string VRAMType { get; init; }
        public bool IsExternal { get; init; }
        public bool HasDesktops { get; init; }
        public string DeviceId { get; init; }
        public string PNPString { get; init; }
        public string DriverPath { get; init; }

        [JsonConstructor]
        public GpuInfo(string name, string vendorId, int uniqueId, uint totalVRAM, string vramType, bool isExternal, bool hasDesktops, string deviceId, string pnpString, string driverPath)
        {
            Name = name;
            VendorId = vendorId;
            UniqueId = uniqueId;
            TotalVRAM = totalVRAM;
            VRAMType = vramType;
            IsExternal = isExternal;
            HasDesktops = hasDesktops;
            DeviceId = deviceId;
            PNPString = pnpString;
            DriverPath = driverPath;
        }

        internal unsafe GpuInfo(IADLXGPU* pGpu)
        {
            sbyte* namePtr = null;
            pGpu->Name(&namePtr);
            Name = ADLXUtils.MarshalString(&namePtr);

            sbyte* vendorIdPtr = null;
            pGpu->VendorId(&vendorIdPtr);
            VendorId = ADLXUtils.MarshalString(&vendorIdPtr);

            int uid = 0;
            pGpu->UniqueId(&uid);
            UniqueId = uid;

            uint vram = 0;
            pGpu->TotalVRAM(&vram);
            TotalVRAM = vram;

            sbyte* vramTypePtr = null;
            pGpu->VRAMType(&vramTypePtr);
            VRAMType = ADLXUtils.MarshalString(&vramTypePtr);

            bool isExt = false;
            pGpu->IsExternal(&isExt);
            IsExternal = isExt;

            bool hasDesk = false;
            pGpu->HasDesktops(&hasDesk);
            HasDesktops = hasDesk;

            sbyte* devIdPtr = null;
            pGpu->DeviceId(&devIdPtr);
            DeviceId = ADLXUtils.MarshalString(&devIdPtr);

            sbyte* pnpPtr = null;
            pGpu->PNPString(&pnpPtr);
            PNPString = ADLXUtils.MarshalString(&pnpPtr);

            sbyte* driverPathPtr = null;
            pGpu->DriverPath(&driverPathPtr);
            DriverPath = ADLXUtils.MarshalString(&driverPathPtr);
        }
    }
    #endregion
}

