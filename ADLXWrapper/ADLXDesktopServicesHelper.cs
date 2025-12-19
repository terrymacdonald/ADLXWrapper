using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLXDesktopServices providing managed/native accessors and Eyefinity helpers.
    /// </summary>
    public sealed unsafe class ADLXDesktopServicesHelper : IDisposable
    {
        private ComPtr<IADLXDesktopServices> _desktopServices;
        private ComPtr<IADLXDisplayServices>? _displayServices;
        private ComPtr<IADLXDesktopChangedHandling>? _desktopChangedHandling;
        private bool _disposed;

        public ADLXDesktopServicesHelper(IADLXDesktopServices* desktopServices, IADLXDisplayServices* displayServices = null, bool addRefDesktopServices = true, bool addRefDisplayServices = true)
        {
            if (desktopServices == null) throw new ArgumentNullException(nameof(desktopServices));
            if (addRefDesktopServices)
            {
                ADLXUtils.AddRefInterface((IntPtr)desktopServices);
            }
            _desktopServices = new ComPtr<IADLXDesktopServices>(desktopServices);
            if (displayServices != null)
            {
                if (addRefDisplayServices)
                {
                    ADLXUtils.AddRefInterface((IntPtr)displayServices);
                }
                _displayServices = new ComPtr<IADLXDisplayServices>(displayServices);
            }
        }

        public IADLXDesktopServices* GetDesktopServicesNative()
        {
            ThrowIfDisposed();
            return _desktopServices.Get();
        }

        public AdlxInterfaceHandle GetDesktopServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(GetDesktopServicesNative(), addRef: true);
        }

        public IEnumerable<DesktopInfo> EnumerateDesktops()
        {
            ThrowIfDisposed();
            var services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            IADLXDesktopList* pDesktopList = null;
            var result = services->GetDesktops(&pDesktopList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate desktops");

            var desktops = new List<DesktopInfo>();
            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);
            for (uint i = 0; i < desktopList.Get()->Size(); i++)
            {
                IADLXDesktop* pDesktop = null;
                desktopList.Get()->At(i, &pDesktop);
                using var desktop = new ComPtr<IADLXDesktop>(pDesktop);
                desktops.Add(new DesktopInfo(desktop.Get()));
            }

            return desktops;
        }

        public IEnumerable<AdlxDesktop> EnumerateAdlxDesktops()
        {
            ThrowIfDisposed();
            var services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            IADLXDesktopList* pDesktopList = null;
            var result = services->GetDesktops(&pDesktopList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate desktops");

            var desktops = new List<AdlxDesktop>();
            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);
            for (uint i = 0; i < desktopList.Get()->Size(); i++)
            {
                IADLXDesktop* pDesktop = null;
                var itemResult = desktopList.Get()->At(i, &pDesktop);
                if (itemResult != ADLX_RESULT.ADLX_OK || pDesktop == null)
                {
                    if (pDesktop != null)
                        ADLXUtils.ReleaseInterface((IntPtr)pDesktop);
                    throw new ADLXException(itemResult, "Failed to access desktop from list");
                }

                desktops.Add(CreateAdlxDesktop(pDesktop, addRef: false));
            }

            return desktops;
        }

        public IEnumerable<AdlxDesktop> EnumerateAdlxDesktopsForGpu(int gpuUniqueId)
        {
            ThrowIfDisposed();
            var services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            IADLXDesktopList* pDesktopList = null;
            var result = services->GetDesktops(&pDesktopList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate desktops");

            var desktops = new List<AdlxDesktop>();
            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);
            for (uint i = 0; i < desktopList.Get()->Size(); i++)
            {
                IADLXDesktop* pDesktop = null;
                var itemResult = desktopList.Get()->At(i, &pDesktop);
                if (itemResult != ADLX_RESULT.ADLX_OK || pDesktop == null)
                {
                    if (pDesktop != null)
                        ADLXUtils.ReleaseInterface((IntPtr)pDesktop);
                    throw new ADLXException(itemResult, "Failed to access desktop from list");
                }

                var hasMatch = false;
                var displayListPtr = ADLXDesktopHelpers.GetDesktopDisplayListNative(pDesktop);
                using var displayList = new ComPtr<IADLXDisplayList>(displayListPtr);
                var displayCount = displayList.Get()->Size();
                for (uint d = 0; d < displayCount && !hasMatch; d++)
                {
                    IADLXDisplay* pDisplay = null;
                    displayList.Get()->At(d, &pDisplay);
                    using var display = new ComPtr<IADLXDisplay>(pDisplay);
                    var info = new DisplayInfo(display.Get());
                    if (info.GpuUniqueId == gpuUniqueId)
                    {
                        hasMatch = true;
                    }
                }

                if (hasMatch)
                {
                    desktops.Add(CreateAdlxDesktop(pDesktop, addRef: false));
                }
                else
                {
                    ADLXUtils.ReleaseInterface((IntPtr)pDesktop);
                }
            }

            return desktops;
        }

        public AdlxDesktop CreateAdlxDesktop(IADLXDesktop* pDesktop, bool addRef = true)
        {
            ThrowIfDisposed();
            if (pDesktop == null) throw new ArgumentNullException(nameof(pDesktop));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)pDesktop);
            }

            var services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            var displayServices = _displayServices.HasValue ? _displayServices.Value.Get() : null;
            return new AdlxDesktop(services, pDesktop, displayServices);
        }

        public EyefinityDesktopInfo CreateEyefinityDesktop()
        {
            ThrowIfDisposed();
            var services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            IADLXSimpleEyefinity* pSimple = null;
            var result = services->GetSimpleEyefinity(&pSimple);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pSimple == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Simple Eyefinity not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");

            using var simple = new ComPtr<IADLXSimpleEyefinity>(pSimple);
            return ADLXDesktopHelpers.CreateEyefinityDesktop(simple.Get());
        }

        public void DestroyAllEyefinityDesktops()
        {
            ThrowIfDisposed();
            var services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            IADLXSimpleEyefinity* pSimple = null;
            var result = services->GetSimpleEyefinity(&pSimple);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pSimple == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Simple Eyefinity not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");

            using var simple = new ComPtr<IADLXSimpleEyefinity>(pSimple);
            ADLXDesktopHelpers.DestroyAllEyefinityDesktops(simple.Get());
        }

        public DesktopListListenerHandle AddDesktopListEventListener(DesktopListListenerHandle.OnDesktopListChanged callback)
        {
            ThrowIfDisposed();
            var handling = GetDesktopChangedHandlingNative();
            var handle = DesktopListListenerHandle.Create(callback);
            var result = handling->AddDesktopListEventListener(handle.GetListener());
            if (result != ADLX_RESULT.ADLX_OK)
            {
                handle.Dispose();
                throw new ADLXException(result, "Failed to add desktop list event listener");
            }

            return handle;
        }

        public void RemoveDesktopListEventListener(DesktopListListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDesktopChangedHandlingNative();
            handling->RemoveDesktopListEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        public AdlxInterfaceHandle[] EnumerateDesktopHandles()
        {
            ThrowIfDisposed();
            var services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            IADLXDesktopList* pDesktopList = null;
            var result = services->GetDesktops(&pDesktopList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate desktops");

            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);
            var count = desktopList.Get()->Size();
            var handles = new AdlxInterfaceHandle[count];

            for (uint i = 0; i < count; i++)
            {
                IADLXDesktop* pDesktop = null;
                desktopList.Get()->At(i, &pDesktop);
                handles[i] = AdlxInterfaceHandle.From(pDesktop, addRef: false);
            }

            return handles;
        }

        public IADLXDesktopList* GetDesktopListNative()
        {
            ThrowIfDisposed();
            var services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            IADLXDesktopList* pDesktopList = null;
            var result = services->GetDesktops(&pDesktopList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate desktops");

            return pDesktopList; // caller must wrap/dispose
        }

        public IADLXDesktopChangedHandling* GetDesktopChangedHandlingNative()
        {
            ThrowIfDisposed();
            if (_desktopChangedHandling.HasValue)
                return _desktopChangedHandling.Value.Get();

            IADLXDesktopChangedHandling* handling = null;
            var result = _desktopServices.Get()->GetDesktopChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get desktop changed handling");

            _desktopChangedHandling = new ComPtr<IADLXDesktopChangedHandling>(handling);
            return handling;
        }

        public AdlxInterfaceHandle GetDesktopChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetDesktopChangedHandlingNative(), addRef: true);
        }

        public SimpleEyefinityInfo GetSimpleEyefinity()
        {
            ThrowIfDisposed();
            IADLXSimpleEyefinity* pSimple = null;
            var result = _desktopServices.Get()->GetSimpleEyefinity(&pSimple);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pSimple == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Simple Eyefinity not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");

            using var simple = new ComPtr<IADLXSimpleEyefinity>(pSimple);
            return new SimpleEyefinityInfo(simple.Get());
        }

        public AdlxInterfaceHandle GetSimpleEyefinityHandle()
        {
            ThrowIfDisposed();
            IADLXSimpleEyefinity* pSimple = null;
            var result = _desktopServices.Get()->GetSimpleEyefinity(&pSimple);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pSimple == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Simple Eyefinity not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");

            return AdlxInterfaceHandle.From(pSimple, addRef: false);
        }

        public EyefinityDesktopInfo CreateEyefinityDesktop(IADLXSimpleEyefinity* pSimpleEyefinity)
        {
            if (pSimpleEyefinity == null) throw new ArgumentNullException(nameof(pSimpleEyefinity));

            IADLXEyefinityDesktop* pDesktop = null;
            var result = pSimpleEyefinity->Create(&pDesktop);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktop == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Eyefinity desktop creation not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to create Eyefinity desktop");

            using var desktop = new ComPtr<IADLXEyefinityDesktop>(pDesktop);
            return new EyefinityDesktopInfo(desktop.Get());
        }

        public void DestroyEyefinityDesktop(IADLXSimpleEyefinity* pSimpleEyefinity, IADLXEyefinityDesktop* pEyefinityDesktop)
        {
            if (pSimpleEyefinity == null) throw new ArgumentNullException(nameof(pSimpleEyefinity));
            if (pEyefinityDesktop == null) throw new ArgumentNullException(nameof(pEyefinityDesktop));

            var result = pSimpleEyefinity->Destroy(pEyefinityDesktop);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to destroy Eyefinity desktop");
            }
        }

        public void DestroyAllEyefinityDesktops(IADLXSimpleEyefinity* pSimpleEyefinity)
        {
            if (pSimpleEyefinity == null) throw new ArgumentNullException(nameof(pSimpleEyefinity));

            var result = pSimpleEyefinity->DestroyAll();
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to destroy all Eyefinity desktops");
            }
        }

        public void Dispose()
        {
            if (_disposed) return;
            _desktopChangedHandling?.Dispose();
            _displayServices?.Dispose();
            _desktopServices.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ADLXDesktopServicesHelper));
        }

        ~ADLXDesktopServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }
    }
}

