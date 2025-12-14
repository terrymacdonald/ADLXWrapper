using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    public sealed unsafe class ADLXSystemServices : IDisposable
    {
        private readonly ADLXApi _owner;
        private ComPtr<IADLXSystem> _system;
        private bool _disposed;

        internal ADLXSystemServices(ADLXApi owner, IADLXSystem* system)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            if (system == null) throw new ArgumentNullException(nameof(system));
            _system = new ComPtr<IADLXSystem>(system);
        }

        public IReadOnlyList<AdlxDisplay> EnumerateAllDisplays()
        {
            ThrowIfDisposed();
            var results = new List<AdlxDisplay>();
            using var displayServices = AcquireDisplayServices();
            IADLXDisplayList* pDisplayList = null;
            var res = displayServices.Get()->GetDisplays(&pDisplayList);
            if (res != ADLX_RESULT.ADLX_OK || pDisplayList == null)
                throw new ADLXException(res, "Failed to get display list");
            using var displayList = new ComPtr<IADLXDisplayList>(pDisplayList);
            for (uint i = 0; i < displayList.Get()->Size(); i++)
            {
                IADLXDisplay* pDisplay = null;
                displayList.Get()->At(i, &pDisplay);
                using var disp = new ComPtr<IADLXDisplay>(pDisplay);
                results.Add(new AdlxDisplay(this, CloneDisplayPtr(disp.Get()), CloneDisplayServicesPtr(displayServices.Get()), null));
            }
            return results;
        }

        public IReadOnlyList<AdlxDesktop> EnumerateAllDesktops()
        {
            ThrowIfDisposed();
            var desktops = new List<AdlxDesktop>();
            using var desktopServices = AcquireDesktopServices();
            IADLXDesktopList* pDesktopList = null;
            var res = desktopServices.Get()->GetDesktops(&pDesktopList);
            if (res != ADLX_RESULT.ADLX_OK || pDesktopList == null)
                throw new ADLXException(res, "Failed to get desktop list");
            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);
            for (uint i = 0; i < desktopList.Get()->Size(); i++)
            {
                IADLXDesktop* pDesktop = null;
                desktopList.Get()->At(i, &pDesktop);
                using var desk = new ComPtr<IADLXDesktop>(pDesktop);
                desktops.Add(new AdlxDesktop(this, CloneDesktopPtr(desk.Get())));
            }
            return desktops;
        }

        public void ApplyDisplayProfile(DisplayProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            ThrowIfDisposed();
            var displays = EnumerateAllDisplays();
            foreach (var d in displays)
            {
                if (d.UniqueId == profile.UniqueId)
                {
                    d.ApplyProfile(profile);
                    return;
                }
            }
            throw new InvalidOperationException($"Display with UniqueId {profile.UniqueId} not found");
        }

        public void ApplyDisplayProfiles(IEnumerable<DisplayProfile> profiles, Action<string>? onSkip = null)
        {
            if (profiles == null) throw new ArgumentNullException(nameof(profiles));
            ThrowIfDisposed();

            var live = new Dictionary<ulong, AdlxDisplay>();
            foreach (var disp in EnumerateAllDisplays())
            {
                live[disp.UniqueId] = disp;
            }

            foreach (var profile in profiles)
            {
                if (profile == null) continue;
                if (!live.TryGetValue(profile.UniqueId, out var display))
                    throw new InvalidOperationException($"Display with UniqueId {profile.UniqueId} not found");

                using (display)
                {
                    display.ApplyProfile(profile, onSkip);
                }
            }
        }

        public IReadOnlyList<DisplayProfile> GetAllDisplayProfiles()
        {
            ThrowIfDisposed();
            var profiles = new List<DisplayProfile>();
            foreach (var display in EnumerateAllDisplays())
            {
                using (display)
                {
                    profiles.Add(display.GetProfile());
                }
            }
            return profiles;
        }

        public void ApplyDesktopProfile(DesktopProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            ThrowIfDisposed();
            var desktops = EnumerateAllDesktops();
            foreach (var desk in desktops)
            {
                if (desk.Type == profile.Type && desk.TopLeftX == profile.TopLeftX && desk.TopLeftY == profile.TopLeftY && desk.Width == profile.Width && desk.Height == profile.Height)
                {
                    desk.ApplyProfile(profile);
                    return;
                }
            }
            throw new InvalidOperationException("Matching desktop not found for supplied profile");
        }

        public void ApplyDesktopProfiles(IEnumerable<DesktopProfile> profiles)
        {
            if (profiles == null) throw new ArgumentNullException(nameof(profiles));
            ThrowIfDisposed();

            var live = EnumerateAllDesktops();

            foreach (var profile in profiles)
            {
                if (profile == null) continue;
                AdlxDesktop? match = null;
                foreach (var desk in live)
                {
                    if (desk.Type == profile.Type && desk.TopLeftX == profile.TopLeftX && desk.TopLeftY == profile.TopLeftY && desk.Width == profile.Width && desk.Height == profile.Height)
                    {
                        match = desk;
                        break;
                    }
                }

                if (match == null)
                    throw new InvalidOperationException("Matching desktop not found for supplied profile");

                using (match)
                {
                    match.ApplyProfile(profile);
                }
            }
        }

        public IReadOnlyList<DesktopProfile> GetAllDesktopProfiles()
        {
            ThrowIfDisposed();
            var profiles = new List<DesktopProfile>();
            foreach (var desktop in EnumerateAllDesktops())
            {
                using (desktop)
                {
                    profiles.Add(desktop.GetProfile());
                }
            }
            return profiles;
        }

        /// <summary>
        /// Get the GPU tuning services facade for a specific GPU handle.
        /// </summary>
        public AdlxGpuTuning GetGpuTuningServices(AdlxInterfaceHandle gpu)
        {
            ThrowIfDisposed();
            if (gpu.IsInvalid) throw new ArgumentException("GPU handle is invalid", nameof(gpu));
            var services = AcquireGpuTuningServices();
            ADLXHelpers.AddRefInterface((IntPtr)services.Get());
            ADLXHelpers.AddRefInterface((IntPtr)gpu.As<IADLXGPU>());
            return new AdlxGpuTuning(_owner, services.Get(), gpu.As<IADLXGPU>());
        }

        /// <summary>
        /// Advanced: get a disposable handle to the raw GPU tuning services.
        /// </summary>
        public AdlxInterfaceHandle GetGpuTuningServicesHandle()
        {
            ThrowIfDisposed();
            var services = AcquireGpuTuningServices();
            return AdlxInterfaceHandle.From(services.Get(), addRef: true);
        }

        /// <summary>
        /// Get the multimedia services facade for a specific GPU handle.
        /// </summary>
        public AdlxMultimedia GetMultimediaServices(AdlxInterfaceHandle gpu)
        {
            ThrowIfDisposed();
            if (gpu.IsInvalid) throw new ArgumentException("GPU handle is invalid", nameof(gpu));
            var services = AcquireMultimediaServices();
            ADLXHelpers.AddRefInterface((IntPtr)services.Get());
            ADLXHelpers.AddRefInterface((IntPtr)gpu.As<IADLXGPU>());
            return new AdlxMultimedia(_owner, services.Get(), gpu.As<IADLXGPU>());
        }

        /// <summary>
        /// Advanced: get a disposable handle to the raw multimedia services.
        /// </summary>
        public AdlxInterfaceHandle GetMultimediaServicesHandle()
        {
            ThrowIfDisposed();
            var services = AcquireMultimediaServices();
            return AdlxInterfaceHandle.From(services.Get(), addRef: true);
        }

        /// <summary>
        /// Get the power tuning services facade.
        /// </summary>
        public AdlxPowerTuning GetPowerTuningServices()
        {
            ThrowIfDisposed();
            var services = AcquirePowerTuningServices();
            ADLXHelpers.AddRefInterface((IntPtr)services.Get());
            return new AdlxPowerTuning(_owner, services.Get());
        }

        /// <summary>
        /// Advanced: get a disposable handle to the raw power tuning services.
        /// </summary>
        public AdlxInterfaceHandle GetPowerTuningServicesHandle()
        {
            ThrowIfDisposed();
            var services = AcquirePowerTuningServices();
            return AdlxInterfaceHandle.From(services.Get(), addRef: true);
        }

        /// <summary>
        /// Get the 3D settings services facade for a specific GPU handle.
        /// </summary>
        public Adlx3DSettings Get3DSettingsServices(AdlxInterfaceHandle gpu)
        {
            ThrowIfDisposed();
            if (gpu.IsInvalid) throw new ArgumentException("GPU handle is invalid", nameof(gpu));
            var services = Acquire3DSettingsServices();
            ADLXHelpers.AddRefInterface((IntPtr)services.Get());
            ADLXHelpers.AddRefInterface((IntPtr)gpu.As<IADLXGPU>());
            return new Adlx3DSettings(_owner, services.Get(), gpu.As<IADLXGPU>());
        }

        /// <summary>
        /// Advanced: get a disposable handle to the raw 3D settings services.
        /// </summary>
        public AdlxInterfaceHandle Get3DSettingsServicesHandle()
        {
            ThrowIfDisposed();
            var services = Acquire3DSettingsServices();
            return AdlxInterfaceHandle.From(services.Get(), addRef: true);
        }

        /// <summary>
        /// Get the performance monitoring services facade.
        /// </summary>
        public AdlxPerformanceMonitor GetPerformanceMonitoringServices()
        {
            ThrowIfDisposed();
            var services = AcquirePerformanceMonitoringServices();
            ADLXHelpers.AddRefInterface((IntPtr)services.Get());
            return new AdlxPerformanceMonitor(_owner, services.Get());
        }

        /// <summary>
        /// Advanced: get a disposable handle to the raw performance monitoring services.
        /// </summary>
        public AdlxInterfaceHandle GetPerformanceMonitoringServicesHandle()
        {
            ThrowIfDisposed();
            var services = AcquirePerformanceMonitoringServices();
            return AdlxInterfaceHandle.From(services.Get(), addRef: true);
        }

        /// <summary>
        /// Configure ADLX logging via the owner API.
        /// </summary>
        public void EnableLog(LogProfile profile)
        {
            ThrowIfDisposed();
            _owner.EnableLog(profile);
        }

        /// <summary>
        /// Reduce ADLX logging to a minimal setting.
        /// </summary>
        public void DisableLog()
        {
            ThrowIfDisposed();
            _owner.DisableLog();
        }

        /// <summary>
        /// Advanced: get a disposable handle to the raw display services for event/listener scenarios.
        /// </summary>
        public AdlxInterfaceHandle GetDisplayServicesHandle()
        {
            ThrowIfDisposed();
            var services = AcquireDisplayServices();
            return AdlxInterfaceHandle.From(services.Get(), addRef: false);
        }

        internal ComPtr<IADLXDisplayServices> AcquireDisplayServices()
        {
            IADLXDisplayServices* pDisplayServices = null;
            var res = _system.Get()->GetDisplaysServices(&pDisplayServices);
            if (res != ADLX_RESULT.ADLX_OK || pDisplayServices == null)
                throw new ADLXException(res, "Failed to get display services");
            return new ComPtr<IADLXDisplayServices>(pDisplayServices);
        }

        internal ComPtr<IADLXDisplayServices1> TryGetDisplayServices1(ComPtr<IADLXDisplayServices> baseServices)
        {
            if (baseServices.Get() == null)
                return default;

            if (ADLXHelpers.TryQueryInterface((IntPtr)baseServices.Get(), nameof(IADLXDisplayServices1), out var ptr) && ptr != IntPtr.Zero)
            {
                return new ComPtr<IADLXDisplayServices1>((IADLXDisplayServices1*)ptr);
            }

            return default;
        }

        internal ComPtr<IADLXDisplayServices1> RequireDisplayServices1(ComPtr<IADLXDisplayServices> baseServices)
        {
            var svc1 = TryGetDisplayServices1(baseServices);
            if (svc1.Get() == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "DisplayServices1 is not supported on this system");

            return svc1;
        }

        internal ComPtr<IADLXDisplayServices2> TryGetDisplayServices2(ComPtr<IADLXDisplayServices> baseServices)
        {
            if (baseServices.Get() == null)
                return default;

            if (ADLXHelpers.TryQueryInterface((IntPtr)baseServices.Get(), nameof(IADLXDisplayServices2), out var ptr) && ptr != IntPtr.Zero)
            {
                return new ComPtr<IADLXDisplayServices2>((IADLXDisplayServices2*)ptr);
            }

            return default;
        }

        internal ComPtr<IADLXDisplayServices2> RequireDisplayServices2(ComPtr<IADLXDisplayServices> baseServices)
        {
            var svc2 = TryGetDisplayServices2(baseServices);
            if (svc2.Get() == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "DisplayServices2 is not supported on this system");

            return svc2;
        }

        internal ComPtr<IADLXDisplayServices3> TryGetDisplayServices3(ComPtr<IADLXDisplayServices> baseServices)
        {
            if (baseServices.Get() == null)
                return default;

            if (ADLXHelpers.TryQueryInterface((IntPtr)baseServices.Get(), nameof(IADLXDisplayServices3), out var ptr) && ptr != IntPtr.Zero)
            {
                return new ComPtr<IADLXDisplayServices3>((IADLXDisplayServices3*)ptr);
            }

            return default;
        }

        internal ComPtr<IADLXDisplayServices3> RequireDisplayServices3(ComPtr<IADLXDisplayServices> baseServices)
        {
            var svc3 = TryGetDisplayServices3(baseServices);
            if (svc3.Get() == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "DisplayServices3 is not supported on this system");

            return svc3;
        }

        internal ComPtr<IADLXDesktopServices> AcquireDesktopServices()
        {
            IADLXDesktopServices* pDesktopServices = null;
            var res = _system.Get()->GetDesktopsServices(&pDesktopServices);
            if (res != ADLX_RESULT.ADLX_OK || pDesktopServices == null)
                throw new ADLXException(res, "Failed to get desktop services");
            return new ComPtr<IADLXDesktopServices>(pDesktopServices);
        }

        internal ComPtr<IADLXDisplay> CloneDisplayPtr(IADLXDisplay* ptr)
        {
            if (ptr == null) throw new ArgumentNullException(nameof(ptr));
            ADLXHelpers.AddRefInterface((IntPtr)ptr);
            return new ComPtr<IADLXDisplay>(ptr);
        }

        internal ComPtr<IADLXDisplayServices> CloneDisplayServicesPtr(IADLXDisplayServices* ptr)
        {
            if (ptr == null) throw new ArgumentNullException(nameof(ptr));
            ADLXHelpers.AddRefInterface((IntPtr)ptr);
            return new ComPtr<IADLXDisplayServices>(ptr);
        }

        internal ComPtr<IADLXGPUTuningServices> AcquireGpuTuningServices()
        {
            IADLXGPUTuningServices* pServices = null;
            var res = _system.Get()->GetGPUTuningServices(&pServices);
            if (res != ADLX_RESULT.ADLX_OK || pServices == null)
                throw new ADLXException(res, "Failed to get GPU tuning services");
            return new ComPtr<IADLXGPUTuningServices>(pServices);
        }

        internal ComPtr<IADLXMultimediaServices> AcquireMultimediaServices()
        {
            using var system2 = ADLXHelpers.RequireInterface<IADLXSystem2>((IntPtr)_system.Get(), nameof(IADLXSystem2));
            IADLXMultimediaServices* pServices = null;
            var res = system2.Get()->GetMultimediaServices(&pServices);
            if (res != ADLX_RESULT.ADLX_OK || pServices == null)
                throw new ADLXException(res, "Failed to get multimedia services");
            return new ComPtr<IADLXMultimediaServices>(pServices);
        }

        internal ComPtr<IADLXPowerTuningServices> AcquirePowerTuningServices()
        {
            using var system2 = ADLXHelpers.RequireInterface<IADLXSystem2>((IntPtr)_system.Get(), nameof(IADLXSystem2));
            IADLXPowerTuningServices* pServices = null;
            var res = system2.Get()->GetPowerTuningServices(&pServices);
            if (res != ADLX_RESULT.ADLX_OK || pServices == null)
                throw new ADLXException(res, "Failed to get power tuning services");
            return new ComPtr<IADLXPowerTuningServices>(pServices);
        }

        internal ComPtr<IADLX3DSettingsServices> Acquire3DSettingsServices()
        {
            IADLX3DSettingsServices* pServices = null;
            var res = _system.Get()->Get3DSettingsServices(&pServices);
            if (res != ADLX_RESULT.ADLX_OK || pServices == null)
                throw new ADLXException(res, "Failed to get 3D settings services");
            return new ComPtr<IADLX3DSettingsServices>(pServices);
        }

        internal ComPtr<IADLXPerformanceMonitoringServices> AcquirePerformanceMonitoringServices()
        {
            IADLXPerformanceMonitoringServices* pServices = null;
            var res = _system.Get()->GetPerformanceMonitoringServices(&pServices);
            if (res != ADLX_RESULT.ADLX_OK || pServices == null)
                throw new ADLXException(res, "Failed to get performance monitoring services");
            return new ComPtr<IADLXPerformanceMonitoringServices>(pServices);
        }

        internal ComPtr<IADLXDesktop> CloneDesktopPtr(IADLXDesktop* ptr)
        {
            if (ptr == null) throw new ArgumentNullException(nameof(ptr));
            ADLXHelpers.AddRefInterface((IntPtr)ptr);
            return new ComPtr<IADLXDesktop>(ptr);
        }

        internal void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ADLXSystemServices));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _system.Dispose();
            _system = default;
            _disposed = true;
        }
    }

}
