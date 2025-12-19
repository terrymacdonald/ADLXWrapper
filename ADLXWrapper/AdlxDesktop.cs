using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Flattened desktop façade with identity metadata and display enumeration helpers.
    /// </summary>
    public sealed unsafe class AdlxDesktop : IDisposable
    {
        private ComPtr<IADLXDesktopServices> _desktopServices;
        private ComPtr<IADLXDesktop> _desktop;
        private readonly DesktopInfo _identity;
        private bool _disposed;

        public AdlxDesktop(IADLXDesktopServices* pDesktopServices, IADLXDesktop* pDesktop)
        {
            if (pDesktopServices == null) throw new ArgumentNullException(nameof(pDesktopServices));
            if (pDesktop == null) throw new ArgumentNullException(nameof(pDesktop));

            _desktopServices = new ComPtr<IADLXDesktopServices>(pDesktopServices);
            _desktop = new ComPtr<IADLXDesktop>(pDesktop);
            _identity = new DesktopInfo(pDesktop);
        }

        public DesktopInfo Identity { get { ThrowIfDisposed(); return _identity; } }
        public ADLX_DESKTOP_TYPE Type { get { ThrowIfDisposed(); return _identity.Type; } }
        public int Width { get { ThrowIfDisposed(); return _identity.Width; } }
        public int Height { get { ThrowIfDisposed(); return _identity.Height; } }
        public int TopLeftX { get { ThrowIfDisposed(); return _identity.TopLeftX; } }
        public int TopLeftY { get { ThrowIfDisposed(); return _identity.TopLeftY; } }
        public ADLX_ORIENTATION Orientation { get { ThrowIfDisposed(); return _identity.Orientation; } }

        /// <summary>
        /// Managed enumeration of displays on this desktop.
        /// </summary>
        public IEnumerable<DisplayInfo> EnumerateDisplays()
        {
            ThrowIfDisposed();
            return ADLXDesktopHelpers.EnumerateDesktopDisplays(_desktop.Get());
        }

        /// <summary>
        /// Native display list accessor. Caller must dispose returned ComPtr and any retained items.
        /// </summary>
        public ComPtr<IADLXDisplayList> GetDisplayListNative()
        {
            ThrowIfDisposed();
            return new ComPtr<IADLXDisplayList>(ADLXDesktopHelpers.GetDesktopDisplayListNative(_desktop.Get()));
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(AdlxDesktop));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _desktop.Dispose();
            _desktopServices.Dispose();
            _disposed = true;
        }
    }
}
