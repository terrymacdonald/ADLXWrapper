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
        private ComPtr<IADLXDisplayServices>? _displayServices;
        private readonly DesktopInfo _identity;
        private bool _disposed;

        public AdlxDesktop(IADLXDesktopServices* pDesktopServices, IADLXDesktop* pDesktop, IADLXDisplayServices* pDisplayServices = null)
        {
            if (pDesktopServices == null) throw new ArgumentNullException(nameof(pDesktopServices));
            if (pDesktop == null) throw new ArgumentNullException(nameof(pDesktop));

            _desktopServices = new ComPtr<IADLXDesktopServices>(pDesktopServices);
            _desktop = new ComPtr<IADLXDesktop>(pDesktop);
            if (pDisplayServices != null)
            {
                ADLXUtils.AddRefInterface((IntPtr)pDisplayServices);
                _displayServices = new ComPtr<IADLXDisplayServices>(pDisplayServices);
            }
            _identity = new DesktopInfo(pDesktop);
        }

        public DesktopInfo Identity { get { ThrowIfDisposed(); return _identity; } }
        public ADLX_DESKTOP_TYPE Type { get { ThrowIfDisposed(); return _identity.Type; } }
        public int Width { get { ThrowIfDisposed(); return _identity.Width; } }
        public int Height { get { ThrowIfDisposed(); return _identity.Height; } }
        public int TopLeftX { get { ThrowIfDisposed(); return _identity.TopLeftX; } }
        public int TopLeftY { get { ThrowIfDisposed(); return _identity.TopLeftY; } }
        public ADLX_ORIENTATION Orientation { get { ThrowIfDisposed(); return _identity.Orientation; } }
        public bool IsEyefinity { get { ThrowIfDisposed(); return _identity.Type == ADLX_DESKTOP_TYPE.DESKTOP_EYEFINITY; } }

        /// <summary>
        /// Managed enumeration of displays on this desktop.
        /// </summary>
        public IEnumerable<DisplayInfo> EnumerateDisplays()
        {
            ThrowIfDisposed();
            using var helper = CreateDesktopServicesHelper();
            return helper.EnumerateDesktopDisplays(_desktop.Get());
        }

        /// <summary>
        /// Façade enumeration of displays on this desktop.
        /// </summary>
        public IEnumerable<AdlxDisplay> EnumerateAdlxDisplays(ADLXDisplayServicesHelper? displayHelper = null)
        {
            ThrowIfDisposed();
            var ownsHelper = false;
            using var desktopHelper = CreateDesktopServicesHelper();
            if (displayHelper == null)
            {
                if (!_displayServices.HasValue)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services were not provided for this desktop instance");
                displayHelper = new ADLXDisplayServicesHelper(_displayServices.Value.Get(), _desktopServices.Get());
                ownsHelper = true;
            }

            try
            {
                using var displayList = new ComPtr<IADLXDisplayList>(desktopHelper.GetDesktopDisplayListNative(_desktop.Get()));
                var count = displayList.Get()->Size();
                var displays = new List<AdlxDisplay>((int)count);
                for (uint i = 0; i < count; i++)
                {
                    IADLXDisplay* pDisplay = null;
                    var itemResult = displayList.Get()->At(i, &pDisplay);
                    if (itemResult != ADLX_RESULT.ADLX_OK || pDisplay == null)
                    {
                        if (pDisplay != null)
                            ADLXUtils.ReleaseInterface((IntPtr)pDisplay);
                        throw new ADLXException(itemResult, "Failed to access display from desktop list");
                    }

                    displays.Add(displayHelper.CreateAdlxDisplay(pDisplay, addRef: false));
                }

                return displays;
            }
            finally
            {
                if (ownsHelper)
                    displayHelper.Dispose();
            }
        }

        /// <summary>
        /// Subscribe to desktop list change events. Returns a handle the caller can dispose (or pass to remove) to unsubscribe.
        /// </summary>
        public DesktopListListenerHandle AddDesktopListEventListener(DesktopListListenerHandle.OnDesktopListChanged callback)
        {
            ThrowIfDisposed();
            using var helper = CreateDesktopServicesHelper();
            return helper.AddDesktopListEventListener(callback);
        }

        public void RemoveDesktopListEventListener(DesktopListListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid) return;
            using var helper = CreateDesktopServicesHelper();
            helper.RemoveDesktopListEventListener(handle, disposeHandle);
        }

        /// <summary>
        /// Eyefinity grid size (rows, columns) for Eyefinity desktops.
        /// </summary>
        public (uint rows, uint cols) GetEyefinityGridSize()
        {
            ThrowIfDisposed();
            using var eyefinity = GetEyefinityDesktop();
            using var helper = CreateDesktopServicesHelper();
            return helper.GetEyefinityGridSize(eyefinity.Get());
        }

        /// <summary>
        /// Enumerate displays that compose this Eyefinity desktop.
        /// </summary>
        public IEnumerable<DisplayInfo> EnumerateEyefinityDisplays()
        {
            ThrowIfDisposed();
            using var eyefinity = GetEyefinityDesktop();
            using var helper = CreateDesktopServicesHelper();
            return helper.EnumerateEyefinityDisplays(eyefinity.Get());
        }

        private ComPtr<IADLXEyefinityDesktop> GetEyefinityDesktop()
        {
            if (_identity.Type != ADLX_DESKTOP_TYPE.DESKTOP_EYEFINITY)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop is not an Eyefinity desktop");

            if (!ADLXUtils.TryQueryInterface((IntPtr)_desktop.Get(), nameof(IADLXEyefinityDesktop), out var ptr) || ptr == IntPtr.Zero)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Eyefinity interface not available for this desktop");

            return new ComPtr<IADLXEyefinityDesktop>((IADLXEyefinityDesktop*)ptr);
        }

        private ADLXDesktopServicesHelper CreateDesktopServicesHelper()
        {
            IADLXDisplayServices* displayServices = _displayServices.HasValue ? _displayServices.Value.Get() : null;
            return new ADLXDesktopServicesHelper(_desktopServices.Get(), displayServices);
        }

        /// <summary>
        /// Native display list accessor. Caller must dispose returned ComPtr and any retained items.
        /// </summary>
        public ComPtr<IADLXDisplayList> GetDisplayListNative()
        {
            ThrowIfDisposed();
            using var helper = CreateDesktopServicesHelper();
            return new ComPtr<IADLXDisplayList>(helper.GetDesktopDisplayListNative(_desktop.Get()));
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(AdlxDesktop));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _displayServices?.Dispose();
            _desktop.Dispose();
            _desktopServices.Dispose();
            _disposed = true;
        }
    }
}
