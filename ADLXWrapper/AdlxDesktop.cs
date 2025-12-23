using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Flattened desktop façade with identity metadata and display enumeration helpers.
    /// </summary>
    public sealed unsafe class ADLXDesktop : IDisposable
    {
        private ComPtr<IADLXDesktopServices> _desktopServices;
        private ComPtr<IADLXDesktop> _desktop;
        private ComPtr<IADLXDisplayServices>? _displayServices;
        private readonly DesktopInfo _identity;
        private bool _disposed;

        public ADLXDesktop(IADLXDesktopServices* pDesktopServices, IADLXDesktop* pDesktop, IADLXDisplayServices* pDisplayServices = null)
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

        public ADLX_DESKTOP_TYPE Type { get { ThrowIfDisposed(); return _identity.Type; } }
        public int Width { get { ThrowIfDisposed(); return _identity.Width; } }
        public int Height { get { ThrowIfDisposed(); return _identity.Height; } }
        public int TopLeftX { get { ThrowIfDisposed(); return _identity.TopLeftX; } }
        public int TopLeftY { get { ThrowIfDisposed(); return _identity.TopLeftY; } }
        public ADLX_ORIENTATION Orientation { get { ThrowIfDisposed(); return _identity.Orientation; } }
        public bool IsEyefinity { get { ThrowIfDisposed(); return _identity.Type == ADLX_DESKTOP_TYPE.DESKTOP_EYEFINITY; } }
        public DesktopInfo Identity { get { ThrowIfDisposed(); return _identity; } }

        /// <summary>
        /// Managed enumeration of display identities on this desktop.
        /// </summary>
        public IReadOnlyList<DisplayInfo> EnumerateDisplayInfosForDesktop()
        {
            ThrowIfDisposed();
            using var helper = CreateDesktopServicesHelper();
            return helper.EnumerateDesktopDisplays(_desktop.Get());
        }

        /// <summary>
        /// Façade enumeration of displays on this desktop (pointer-free).
        /// </summary>
        public IReadOnlyList<ADLXDisplay> EnumerateDisplaysForDesktop()
        {
            ThrowIfDisposed();
            using var desktopHelper = CreateDesktopServicesHelper();
            if (!_displayServices.HasValue || _displayServices.Value.Get() == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services were not provided for this desktop instance");

            var displayServices = _displayServices.Value.Get();
            using var displayList = new ComPtr<IADLXDisplayList>(desktopHelper.GetDesktopDisplayListNative(_desktop.Get()));
            var count = displayList.Get()->Size();
            var displays = new List<ADLXDisplay>((int)count);
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

                displays.Add(new ADLXDisplay(displayServices, pDisplay, _desktopServices.Get()));
            }

            return displays;
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

        public DesktopInfo GetDesktopInfo()
        {   
            ThrowIfDisposed();
            return _identity;
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
        public IReadOnlyList<DisplayInfo> EnumerateEyefinityDisplayInfosForDesktop()
        {
            ThrowIfDisposed();
            using var eyefinity = GetEyefinityDesktop();
            using var helper = CreateDesktopServicesHelper();
            return helper.EnumerateEyefinityDisplays(eyefinity.Get());
        }

        /// <summary>
        /// GPU that drives this desktop (first GPU owning any display on the desktop).
        /// </summary>
        public ADLXGPU GetGPU()
        {
            ThrowIfDisposed();
            using var helper = CreateDesktopServicesHelper();
            using var displayList = new ComPtr<IADLXDisplayList>(helper.GetDesktopDisplayListNative(_desktop.Get()));
            if (displayList.Get()->Size() == 0)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop has no displays to resolve GPU");

            IADLXDisplay* pDisplay = null;
            var itemResult = displayList.Get()->At(0, &pDisplay);
            if (itemResult != ADLX_RESULT.ADLX_OK || pDisplay == null)
            {
                if (pDisplay != null) ADLXUtils.ReleaseInterface((IntPtr)pDisplay);
                throw new ADLXException(itemResult, "Failed to access display while resolving desktop GPU");
            }

            IADLXGPU* pGpu = null;
            var gpuResult = pDisplay->GetGPU(&pGpu);
            ADLXUtils.ReleaseInterface((IntPtr)pDisplay);
            if (gpuResult == ADLX_RESULT.ADLX_NOT_SUPPORTED || pGpu == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU lookup for desktop not supported by this ADLX system");
            if (gpuResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(gpuResult, "Failed to resolve GPU for desktop");

            var displayServices = _displayServices.HasValue ? _displayServices.Value.Get() : null;
            return new ADLXGPU(pGpu, displayServices, _desktopServices.Get());
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
            if (_disposed) throw new ObjectDisposedException(nameof(ADLXDesktop));
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
