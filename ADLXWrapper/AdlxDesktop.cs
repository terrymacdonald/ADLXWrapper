using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    public sealed unsafe class AdlxDesktop : IDisposable
    {
        private readonly ADLXSystemServices _system;
        private ComPtr<IADLXDesktop> _desktop;
        private bool _disposed;

        internal AdlxDesktop(ADLXSystemServices system, ComPtr<IADLXDesktop> desktop)
        {
            _system = system ?? throw new ArgumentNullException(nameof(system));
            _desktop = desktop;
        }

        public ADLX_DESKTOP_TYPE Type
        {
            get
            {
                ThrowIfDisposed();
                ADLX_DESKTOP_TYPE type = default;
                _desktop.Get()->Type(&type);
                return type;
            }
        }

        public int Width
        {
            get
            {
                ThrowIfDisposed();
                int w = 0, h = 0;
                _desktop.Get()->Size(&w, &h);
                return w;
            }
        }

        public int Height
        {
            get
            {
                ThrowIfDisposed();
                int w = 0, h = 0;
                _desktop.Get()->Size(&w, &h);
                return h;
            }
        }

        public int TopLeftX
        {
            get
            {
                ThrowIfDisposed();
                ADLX_Point tl = default;
                _desktop.Get()->TopLeft(&tl);
                return tl.x;
            }
        }

        public int TopLeftY
        {
            get
            {
                ThrowIfDisposed();
                ADLX_Point tl = default;
                _desktop.Get()->TopLeft(&tl);
                return tl.y;
            }
        }

        public ADLX_ORIENTATION Orientation
        {
            get
            {
                ThrowIfDisposed();
                ADLX_ORIENTATION o = default;
                _desktop.Get()->Orientation(&o);
                return o;
            }
        }

        public IReadOnlyList<AdlxDisplay> GetDisplays()
        {
            ThrowIfDisposed();
            var displays = new List<AdlxDisplay>();
            IADLXDisplayList* pList = null;
            var res = _desktop.Get()->GetDisplays(&pList);
            if (res != ADLX_RESULT.ADLX_OK || pList == null)
                throw new ADLXException(res, "Failed to get desktop displays");
            using var list = new ComPtr<IADLXDisplayList>(pList);
            using var displayServices = _system.AcquireDisplayServices();
            for (uint i = 0; i < list.Get()->Size(); i++)
            {
                IADLXDisplay* pDisplay = null;
                list.Get()->At(i, &pDisplay);
                using var disp = new ComPtr<IADLXDisplay>(pDisplay);
                displays.Add(new AdlxDisplay(_system, _system.CloneDisplayPtr(disp.Get()), _system.CloneDisplayServicesPtr(displayServices.Get()), this));
            }
            return displays;
        }

        public DesktopProfile GetProfile()
        {
            ThrowIfDisposed();
            var profile = new DesktopProfile
            {
                Type = Type,
                Width = Width,
                Height = Height,
                TopLeftX = TopLeftX,
                TopLeftY = TopLeftY,
                Orientation = Orientation
            };
            foreach (var display in GetDisplays())
            {
                profile.Displays.Add(display.GetProfile());
            }
            return profile;
        }

        public string ToJson() => JsonConvert.SerializeObject(GetProfile(), Formatting.Indented);

        public void ApplyProfile(DesktopProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            ThrowIfDisposed();
            var currentDisplays = GetDisplays();
            foreach (var target in profile.Displays)
            {
                foreach (var live in currentDisplays)
                {
                    if (live.UniqueId == target.UniqueId)
                    {
                        live.ApplyProfile(target);
                        break;
                    }
                }
            }
        }

        internal IADLXDesktop* GetRaw() => _desktop.Get();

        private void ThrowIfDisposed()
        {
            _system.ThrowIfDisposed();
            if (_disposed) throw new ObjectDisposedException(nameof(AdlxDesktop));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _desktop.Dispose();
            _desktop = default;
            _disposed = true;
        }
    }
}
