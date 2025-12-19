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
        private ComPtr<IADLXDisplayServices2>? _displayServices2;
        private ComPtr<IADLXDisplayServices3>? _displayServices3;
        private ComPtr<IADLXDisplayChangedHandling>? _displayChangedHandling;
        private bool _disposed;

        public ADLXDisplayServicesHelper(IADLXDisplayServices* displayServices, bool addRef = true)
        {
            if (displayServices == null) throw new ArgumentNullException(nameof(displayServices));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)displayServices);
            }
            _displayServices = new ComPtr<IADLXDisplayServices>(displayServices);
            TryUpgradeDisplayServices(displayServices);
        }

        public IADLXDisplayServices* GetDisplayServicesNative()
        {
            ThrowIfDisposed();
            return GetHighestDisplayServices();
        }

        public AdlxInterfaceHandle GetDisplayServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(GetDisplayServicesNative(), addRef: true);
        }

        public IEnumerable<DisplayInfo> EnumerateDisplays()
        {
            ThrowIfDisposed();
            var services = GetHighestDisplayServices();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported by this ADLX system");

            IADLXDisplayList* pDisplayList = null;
            var result = services->GetDisplays(&pDisplayList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDisplayList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
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

        public IADLXDisplayList* GetDisplayListNative()
        {
            ThrowIfDisposed();
            var services = GetHighestDisplayServices();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported by this ADLX system");

            IADLXDisplayList* pDisplayList = null;
            var result = services->GetDisplays(&pDisplayList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDisplayList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate displays");

            return pDisplayList; // caller must wrap/dispose
        }

        public AdlxInterfaceHandle[] EnumerateDisplayHandles()
        {
            ThrowIfDisposed();
            var services = GetHighestDisplayServices();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported by this ADLX system");

            IADLXDisplayList* pDisplayList = null;
            var result = services->GetDisplays(&pDisplayList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDisplayList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
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
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
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
            _displayServices3?.Dispose();
            _displayServices2?.Dispose();
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

        private void TryUpgradeDisplayServices(IADLXDisplayServices* services)
        {
            if (services == null)
                return;

            if (ADLXUtils.TryQueryInterface((IntPtr)services, nameof(IADLXDisplayServices3), out var pServices3))
            {
                _displayServices3 = new ComPtr<IADLXDisplayServices3>((IADLXDisplayServices3*)pServices3);
                return;
            }

            if (ADLXUtils.TryQueryInterface((IntPtr)services, nameof(IADLXDisplayServices2), out var pServices2))
            {
                _displayServices2 = new ComPtr<IADLXDisplayServices2>((IADLXDisplayServices2*)pServices2);
            }
        }

        private IADLXDisplayServices* GetHighestDisplayServices()
        {
            if (_displayServices3.HasValue)
                return (IADLXDisplayServices*)_displayServices3.Value.Get();
            if (_displayServices2.HasValue)
                return (IADLXDisplayServices*)_displayServices2.Value.Get();
            return _displayServices.Get();
        }
    }
}
