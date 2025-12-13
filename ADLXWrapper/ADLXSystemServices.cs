using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    public sealed unsafe class ADLXSystemServices : IDisposable
    {
        private readonly ADLXApi _owner;
        private ComPtr<IADLXSystem> _system;
        private readonly AdlxCapabilities _capabilities;
        private bool _disposed;

        internal ADLXSystemServices(ADLXApi owner, IADLXSystem* system)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            if (system == null) throw new ArgumentNullException(nameof(system));
            _system = new ComPtr<IADLXSystem>(system);
            _capabilities = AdlxCapabilities.Create(owner, system);
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

        internal ComPtr<IADLXDisplayServices3> TryGetDisplayServices3(ComPtr<IADLXDisplayServices> baseServices)
        {
            if (!_capabilities.SupportsDisplayServices3 || baseServices.Get() == null)
                return default;

            if (ADLXHelpers.TryQueryInterface((IntPtr)baseServices.Get(), nameof(IADLXDisplayServices3), out var ptr) && ptr != IntPtr.Zero)
            {
                return new ComPtr<IADLXDisplayServices3>((IADLXDisplayServices3*)ptr);
            }

            return default;
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

        public AdlxCapabilities Capabilities => _capabilities;

        public void Dispose()
        {
            if (_disposed) return;
            _system.Dispose();
            _system = default;
            _disposed = true;
        }
    }

    public sealed class AdlxCapabilities
    {
        private AdlxCapabilities(ulong fullVersion, string versionString, int major, int minor, int patch, bool supportsDisplayServices2, bool supportsDisplayServices3)
        {
            FullVersion = fullVersion;
            VersionString = versionString;
            Major = major;
            Minor = minor;
            Patch = patch;
            SupportsDisplayServices2 = supportsDisplayServices2;
            SupportsDisplayServices3 = supportsDisplayServices3;
        }

        public ulong FullVersion { get; }
        public string VersionString { get; }
        public int Major { get; }
        public int Minor { get; }
        public int Patch { get; }
        public bool SupportsDisplayServices2 { get; }
        public bool SupportsDisplayServices3 { get; }

        public static unsafe AdlxCapabilities Create(ADLXApi owner, IADLXSystem* system)
        {
            var versionString = owner.GetVersion();
            var fullVersion = owner.GetFullVersion();
            var (major, minor, patch) = ParseVersion(versionString);

            bool hasDs3 = false, hasDs2 = false;
            if (system != null)
            {
                IADLXDisplayServices* pDisplayServices = null;
                var res = system->GetDisplaysServices(&pDisplayServices);
                if (res == ADLX_RESULT.ADLX_OK && pDisplayServices != null)
                {
                    using var services = new ComPtr<IADLXDisplayServices>(pDisplayServices);
                    hasDs3 = ADLXHelpers.TryQueryInterface((IntPtr)services.Get(), nameof(IADLXDisplayServices3), out _);
                    hasDs2 = hasDs3 || ADLXHelpers.TryQueryInterface((IntPtr)services.Get(), nameof(IADLXDisplayServices2), out _);
                }
            }

            return new AdlxCapabilities(fullVersion, versionString, major, minor, patch, hasDs2, hasDs3);
        }

        private static (int Major, int Minor, int Patch) ParseVersion(string version)
        {
            if (string.IsNullOrWhiteSpace(version)) return (0, 0, 0);

            var parts = version.Split('.', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var major = parts.Length > 0 && int.TryParse(parts[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out var m) ? m : 0;
            var minor = parts.Length > 1 && int.TryParse(parts[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out var n) ? n : 0;
            var patch = parts.Length > 2 && int.TryParse(parts[2], NumberStyles.Integer, CultureInfo.InvariantCulture, out var p) ? p : 0;
            return (major, minor, patch);
        }
    }
}
