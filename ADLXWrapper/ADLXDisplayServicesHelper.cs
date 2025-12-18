using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLXDisplayServices that provides cached accessors and managed/native enumeration helpers.
    /// </summary>
    public sealed unsafe class ADLXDisplayServicesHelper : IDisposable
    {
        private ComPtr<IADLXDisplayServices> _displayServices;
        private ComPtr<IADLXDisplayChangedHandling>? _displayChangedHandling;
        private bool _disposed;

        public ADLXDisplayServicesHelper(IADLXDisplayServices* displayServices, bool addRef = true)
        {
            if (displayServices == null) throw new ArgumentNullException(nameof(displayServices));
            if (addRef)
            {
                ADLXHelpers.AddRefInterface((IntPtr)displayServices);
            }
            _displayServices = new ComPtr<IADLXDisplayServices>(displayServices);
        }

        public IADLXDisplayServices* GetDisplayServicesNative()
        {
            ThrowIfDisposed();
            return _displayServices.Get();
        }

        public AdlxInterfaceHandle GetDisplayServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(_displayServices.Get(), addRef: true);
        }

        public IEnumerable<DisplayInfo> EnumerateDisplays()
        {
            ThrowIfDisposed();
            var services = _displayServices.Get();
            if (services == null) return Array.Empty<DisplayInfo>();

            IADLXDisplayList* pDisplayList = null;
            var result = services->GetDisplays(&pDisplayList);
            if (result != ADLX_RESULT.ADLX_OK || pDisplayList == null)
                throw new ADLXException(result, "Failed to enumerate displays");

            var displays = new List<DisplayInfo>();
            using var displayList = new ComPtr<IADLXDisplayList>(pDisplayList);
            for (uint i = 0; i < displayList.Get()->Size(); i++)
            {
                IADLXDisplay* pDisplay = null;
                displayList.Get()->At(i, &pDisplay);
                using var display = new ComPtr<IADLXDisplay>(pDisplay);
                displays.Add(new DisplayInfo(display.Get()));
            }

            return displays;
        }

        public AdlxInterfaceHandle[] EnumerateDisplayHandles()
        {
            ThrowIfDisposed();
            var services = _displayServices.Get();
            if (services == null) return Array.Empty<AdlxInterfaceHandle>();

            IADLXDisplayList* pDisplayList = null;
            var result = services->GetDisplays(&pDisplayList);
            if (result != ADLX_RESULT.ADLX_OK || pDisplayList == null)
                throw new ADLXException(result, "Failed to enumerate displays");

            using var displayList = new ComPtr<IADLXDisplayList>(pDisplayList);
            var count = displayList.Get()->Size();
            var handles = new AdlxInterfaceHandle[count];

            for (uint i = 0; i < count; i++)
            {
                IADLXDisplay* pDisplay = null;
                displayList.Get()->At(i, &pDisplay);
                handles[i] = AdlxInterfaceHandle.From(pDisplay, addRef: false);
            }

            return handles;
        }

        public IADLXDisplayChangedHandling* GetDisplayChangedHandlingNative()
        {
            ThrowIfDisposed();
            if (_displayChangedHandling.HasValue)
                return _displayChangedHandling.Value.Get();

            IADLXDisplayChangedHandling* handling = null;
            var result = _displayServices.Get()->GetDisplayChangedHandling(&handling);
            if (result != ADLX_RESULT.ADLX_OK || handling == null)
                throw new ADLXException(result, "Failed to get display changed handling");

            _displayChangedHandling = new ComPtr<IADLXDisplayChangedHandling>(handling);
            return handling;
        }

        public AdlxInterfaceHandle GetDisplayChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetDisplayChangedHandlingNative(), addRef: true);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _displayChangedHandling?.Dispose();
            _displayServices.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ADLXDisplayServicesHelper));
            }
        }

        ~ADLXDisplayServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }
    }
}