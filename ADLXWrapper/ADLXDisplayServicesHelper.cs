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
        private ComPtr<IADLXDesktopServices>? _desktopServices;
        private ComPtr<IADLXDisplayChangedHandling>? _displayChangedHandling;
        private bool _disposed;

        public ADLXDisplayServicesHelper(IADLXDisplayServices* displayServices, IADLXDesktopServices* desktopServices = null, bool addRefDisplayServices = true, bool addRefDesktopServices = true)
        {
            if (displayServices == null) throw new ArgumentNullException(nameof(displayServices));
            if (addRefDisplayServices)
            {
                ADLXUtils.AddRefInterface((IntPtr)displayServices);
            }
            _displayServices = new ComPtr<IADLXDisplayServices>(displayServices);
            if (desktopServices != null)
            {
                if (addRefDesktopServices)
                {
                    ADLXUtils.AddRefInterface((IntPtr)desktopServices);
                }
                _desktopServices = new ComPtr<IADLXDesktopServices>(desktopServices);
            }
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

        public IEnumerable<AdlxDisplay> EnumerateAdlxDisplays()
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

            var displays = new List<AdlxDisplay>();
            using var displayList = new ComPtr<IADLXDisplayList>(pDisplayList);
            for (uint i = 0; i < displayList.Get()->Size(); i++)
            {
                IADLXDisplay* pDisplay = null;
                var itemResult = displayList.Get()->At(i, &pDisplay);
                if (itemResult != ADLX_RESULT.ADLX_OK || pDisplay == null)
                {
                    if (pDisplay != null)
                        ADLXUtils.ReleaseInterface((IntPtr)pDisplay);
                    throw new ADLXException(itemResult, "Failed to access display from list");
                }

                displays.Add(CreateAdlxDisplay(pDisplay, addRef: false));
            }

            return displays;
        }

        public IEnumerable<AdlxDisplay> EnumerateAdlxDisplaysForGpu(int gpuUniqueId)
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

            var displays = new List<AdlxDisplay>();
            using var displayList = new ComPtr<IADLXDisplayList>(pDisplayList);
            for (uint i = 0; i < displayList.Get()->Size(); i++)
            {
                IADLXDisplay* pDisplay = null;
                var itemResult = displayList.Get()->At(i, &pDisplay);
                if (itemResult != ADLX_RESULT.ADLX_OK || pDisplay == null)
                {
                    if (pDisplay != null)
                        ADLXUtils.ReleaseInterface((IntPtr)pDisplay);
                    throw new ADLXException(itemResult, "Failed to access display from list");
                }

                var identity = new DisplayInfo(pDisplay);
                if (identity.GpuUniqueId == gpuUniqueId)
                {
                    displays.Add(CreateAdlxDisplay(pDisplay, addRef: false));
                }
                else
                {
                    ADLXUtils.ReleaseInterface((IntPtr)pDisplay);
                }
            }

            return displays;
        }

        public AdlxDisplay CreateAdlxDisplay(IADLXDisplay* pDisplay, bool addRef = true)
        {
            ThrowIfDisposed();
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)pDisplay);
            }

            var services = GetHighestDisplayServices();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported by this ADLX system");

            var desktopServices = _desktopServices.HasValue ? _desktopServices.Value.Get() : null;
            return new AdlxDisplay(services, pDisplay, desktopServices);
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

        public DisplayListListenerHandle AddDisplayListEventListener(DisplayListListenerHandle.OnDisplayListChanged callback)
        {
            ThrowIfDisposed();
            var handling = GetDisplayChangedHandlingNative();
            var handle = DisplayListListenerHandle.Create(callback);
            var result = handling->AddDisplayListEventListener(handle.GetListener());
            if (result != ADLX_RESULT.ADLX_OK)
            {
                handle.Dispose();
                throw new ADLXException(result, "Failed to add display list event listener");
            }
            return handle;
        }

        public void RemoveDisplayListEventListener(DisplayListListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDisplayChangedHandlingNative();
            handling->RemoveDisplayListEventListener(handle.GetListener());

            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        public DisplayGamutListenerHandle AddDisplayGamutEventListener(DisplayGamutListenerHandle.OnDisplayGamutChanged callback)
        {
            ThrowIfDisposed();
            var handling = GetDisplayChangedHandlingNative();
            var handle = DisplayGamutListenerHandle.Create(callback);
            var result = handling->AddDisplayGamutEventListener(handle.GetListener());
            if (result != ADLX_RESULT.ADLX_OK)
            {
                handle.Dispose();
                throw new ADLXException(result, "Failed to add display gamut event listener");
            }
            return handle;
        }

        public void RemoveDisplayGamutEventListener(DisplayGamutListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDisplayChangedHandlingNative();
            handling->RemoveDisplayGamutEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        public DisplayGammaListenerHandle AddDisplayGammaEventListener(DisplayGammaListenerHandle.OnDisplayGammaChanged callback)
        {
            ThrowIfDisposed();
            var handling = GetDisplayChangedHandlingNative();
            var handle = DisplayGammaListenerHandle.Create(callback);
            var result = handling->AddDisplayGammaEventListener(handle.GetListener());
            if (result != ADLX_RESULT.ADLX_OK)
            {
                handle.Dispose();
                throw new ADLXException(result, "Failed to add display gamma event listener");
            }
            return handle;
        }

        public void RemoveDisplayGammaEventListener(DisplayGammaListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDisplayChangedHandlingNative();
            handling->RemoveDisplayGammaEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        public Display3DLutListenerHandle AddDisplay3DLutEventListener(Display3DLutListenerHandle.OnDisplay3DLutChanged callback)
        {
            ThrowIfDisposed();
            var handling = GetDisplayChangedHandlingNative();
            var handle = Display3DLutListenerHandle.Create(callback);
            var result = handling->AddDisplay3DLUTEventListener(handle.GetListener());
            if (result != ADLX_RESULT.ADLX_OK)
            {
                handle.Dispose();
                throw new ADLXException(result, "Failed to add display 3DLUT event listener");
            }
            return handle;
        }

        public void RemoveDisplay3DLutEventListener(Display3DLutListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDisplayChangedHandlingNative();
            handling->RemoveDisplay3DLUTEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        public DisplaySettingsListenerHandle AddDisplaySettingsEventListener(DisplaySettingsListenerHandle.DisplaySettingsChangedCallback callback)
        {
            ThrowIfDisposed();
            var handling = GetDisplayChangedHandlingNative();
            var handle = DisplaySettingsListenerHandle.Create(callback);
            var result = handling->AddDisplaySettingsEventListener(handle.GetListener());
            if (result != ADLX_RESULT.ADLX_OK)
            {
                handle.Dispose();
                throw new ADLXException(result, "Failed to add display settings event listener");
            }
            return handle;
        }

        public void RemoveDisplaySettingsEventListener(DisplaySettingsListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDisplayChangedHandlingNative();
            handling->RemoveDisplaySettingsEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _displayChangedHandling?.Dispose();
            _desktopServices?.Dispose();
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
