using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

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
        private readonly IADLXSystem* _system; // optional system pointer to reacquire services
        private bool _disposed;

        /// <summary>
        /// Creates a desktop services helper from native desktop/display services.
        /// </summary>
        /// <param name="desktopServices">Native desktop services pointer.</param>
        /// <param name="displayServices">Optional display services pointer used to build display facades.</param>
        /// <param name="addRefDesktopServices">True to AddRef the desktop services pointer.</param>
        /// <param name="addRefDisplayServices">True to AddRef the display services pointer.</param>
        /// <param name="system">Optional system pointer used to reacquire desktop services per call.</param>
        public ADLXDesktopServicesHelper(IADLXDesktopServices* desktopServices, IADLXDisplayServices* displayServices = null, bool addRefDesktopServices = true, bool addRefDisplayServices = true, IADLXSystem* system = null)
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
            _system = system;
        }

        public IADLXDesktopServices* GetDesktopServicesNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return _desktopServices.Get();
        }

        /// <summary>
        /// Returns an AddRef'd handle to the desktop services interface for external ownership.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public ADLXInterfaceHandle GetDesktopServicesHandle()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return ADLXInterfaceHandle.From(GetDesktopServicesNative(), addRef: true);
        }

        /// <summary>
        /// Enumerates desktop DTOs (native data marshaled to managed structs).
        /// </summary>
        /// <returns>List of desktop info records.</returns>
        /// <exception cref="ADLXException">If enumeration is unsupported or fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IReadOnlyList<DesktopInfo> EnumerateDesktops()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            using var servicesOwner = AcquireDesktopServices(out var services);

            uint numDesktops = 0;
            var countResult = services->GetNumberOfDesktops(&numDesktops);
            if (countResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop count not supported by this ADLX system");
            if (countResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(countResult, "Failed to get desktop count");

            IADLXDesktopList* pDesktopList = null;
            var result = services->GetDesktops(&pDesktopList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate desktops");

            var desktops = new List<DesktopInfo>();
            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);
            if (desktopList.Get()->Size() == 0)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "No desktops returned by ADLX.");
            for (uint i = 0; i < desktopList.Get()->Size(); i++)
            {
                IADLXDesktop* pDesktop = null;
                desktopList.Get()->At(i, &pDesktop);
                using var desktop = new ComPtr<IADLXDesktop>(pDesktop);
                desktops.Add(new DesktopInfo(desktop.Get()));
            }

            return desktops;
        }

        public bool TryEnumerateDesktops(out IReadOnlyList<DesktopInfo> desktops)
        {
            try
            {
                desktops = EnumerateDesktops();
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                desktops = Array.Empty<DesktopInfo>();
                return false;
            }
        }

        /// <summary>
        /// Enumerates managed desktop facades. Callers must dispose each desktop.
        /// </summary>
        /// <returns>List of desktop facades.</returns>
        /// <exception cref="ADLXException">If enumeration is unsupported or fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IReadOnlyList<ADLXDesktop> EnumerateADLXDesktops()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            using var servicesOwner = AcquireDesktopServices(out var services);

            uint numDesktops = 0;
            var countResult = services->GetNumberOfDesktops(&numDesktops);
            if (countResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop count not supported by this ADLX system");
            if (countResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(countResult, "Failed to get desktop count");

            IADLXDesktopList* pDesktopList = null;
            var result = services->GetDesktops(&pDesktopList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate desktops");

            var desktops = new List<ADLXDesktop>();
            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);
            if (desktopList.Get()->Size() == 0)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "No desktops returned by ADLX.");
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

                desktops.Add(CreateADLXDesktop(pDesktop, addRef: false));
            }

            return desktops;
        }

        public bool TryEnumerateADLXDesktops(out IReadOnlyList<ADLXDesktop> desktops)
        {
            try
            {
                desktops = EnumerateADLXDesktops();
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                desktops = Array.Empty<ADLXDesktop>();
                return false;
            }
        }

        /// <summary>
        /// Enumerates managed desktop facades filtered by GPU unique ID. Callers must dispose each desktop.
        /// </summary>
        /// <param name="gpuUniqueId">GPU unique ID to filter on.</param>
        /// <returns>List of desktop facades for the GPU.</returns>
        /// <exception cref="ADLXException">If enumeration is unsupported or fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IReadOnlyList<ADLXDesktop> EnumerateADLXDesktopsForGpu(int gpuUniqueId)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            using var servicesOwner = AcquireDesktopServices(out var services);

            uint numDesktops = 0;
            var countResult = services->GetNumberOfDesktops(&numDesktops);
            if (countResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop count not supported by this ADLX system");
            if (countResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(countResult, "Failed to get desktop count");

            IADLXDesktopList* pDesktopList = null;
            var result = services->GetDesktops(&pDesktopList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate desktops");

            var desktops = new List<ADLXDesktop>();
            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);
            if (desktopList.Get()->Size() == 0)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "No desktops returned by ADLX.");
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
                var displayListPtr = GetDesktopDisplayListNative(pDesktop);
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
                    desktops.Add(CreateADLXDesktop(pDesktop, addRef: false));
                }
                else
                {
                    ADLXUtils.ReleaseInterface((IntPtr)pDesktop);
                }
            }

            return desktops;
        }

        public bool TryEnumerateADLXDesktopsForGpu(int gpuUniqueId, out IReadOnlyList<ADLXDesktop> desktops)
        {
            try
            {
                desktops = EnumerateADLXDesktopsForGpu(gpuUniqueId);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                desktops = Array.Empty<ADLXDesktop>();
                return false;
            }
        }

        /// <summary>
        /// Wraps a native desktop pointer in a managed facade.
        /// </summary>
        /// <param name="pDesktop">Native desktop pointer.</param>
        /// <param name="addRef">True to AddRef the pointer for this facade.</param>
        /// <returns>Managed desktop facade.</returns>
        /// <exception cref="ADLXException">If desktop services are unavailable.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="pDesktop"/> is null.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public ADLXDesktop CreateADLXDesktop(IADLXDesktop* pDesktop, bool addRef = true)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (pDesktop == null) throw new ArgumentNullException(nameof(pDesktop));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)pDesktop);
            }

            var services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            var displayServices = _displayServices.HasValue ? _displayServices.Value.Get() : null;
            return new ADLXDesktop(services, pDesktop, displayServices);
        }

        /// <summary>
        /// Creates a new Eyefinity desktop using the simple Eyefinity interface.
        /// </summary>
        /// <returns>Information about the created Eyefinity desktop.</returns>
        /// <exception cref="ADLXException">If Eyefinity is unsupported or creation fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public EyefinityDesktopInfo CreateEyefinityDesktop()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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
            return CreateEyefinityDesktop(simple.Get());
        }

        /// <summary>
        /// Destroys all Eyefinity desktops via the simple Eyefinity interface.
        /// </summary>
        /// <exception cref="ADLXException">If Eyefinity is unsupported or destruction fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public void DestroyAllEyefinityDesktops()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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
            DestroyAllEyefinityDesktops(simple.Get());
        }

        /// <summary>
        /// Adds a desktop list change listener.
        /// </summary>
        /// <param name="callback">Callback invoked when the desktop list changes.</param>
        /// <returns>Listener handle that must be disposed to unsubscribe.</returns>
        /// <exception cref="ADLXException">If registration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public DesktopListListenerHandle AddDesktopListEventListener(DesktopListListenerHandle.OnDesktopListChanged callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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

        /// <summary>
        /// Removes a desktop list change listener.
        /// </summary>
        /// <param name="handle">Handle returned by add.</param>
        /// <param name="disposeHandle">True to dispose the handle after removal.</param>
        public void RemoveDesktopListEventListener(DesktopListListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDesktopChangedHandlingNative();
            handling->RemoveDesktopListEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        /// <summary>
        /// Enumerates native desktop handles (AddRef'd) for advanced/native callers.
        /// </summary>
        /// <returns>Array of native desktop handles.</returns>
        /// <exception cref="ADLXException">If enumeration is unsupported or fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public ADLXInterfaceHandle[] EnumerateDesktopHandles()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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
            var handles = new ADLXInterfaceHandle[count];

            for (uint i = 0; i < count; i++)
            {
                IADLXDesktop* pDesktop = null;
                desktopList.Get()->At(i, &pDesktop);
                handles[i] = ADLXInterfaceHandle.From(pDesktop, addRef: false);
            }

            return handles;
        }

        /// <summary>
        /// Returns the native desktop list. Caller must dispose the returned list.
        /// </summary>
        /// <returns>Native desktop list pointer.</returns>
        /// <exception cref="ADLXException">If enumeration is unsupported or fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IADLXDesktopList* GetDesktopListNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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
            using var _sync = ADLXSync.EnterRead();
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

        public ADLXInterfaceHandle GetDesktopChangedHandling()
        {
            return ADLXInterfaceHandle.From(GetDesktopChangedHandlingNative(), addRef: true);
        }

        public SimpleEyefinityInfo GetSimpleEyefinity()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            IADLXSimpleEyefinity* pSimple = null;
            var result = _desktopServices.Get()->GetSimpleEyefinity(&pSimple);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pSimple == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Simple Eyefinity not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");

            using var simple = new ComPtr<IADLXSimpleEyefinity>(pSimple);
            return new SimpleEyefinityInfo(simple.Get());
        }

        public ADLXInterfaceHandle GetSimpleEyefinityHandle()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            IADLXSimpleEyefinity* pSimple = null;
            var result = _desktopServices.Get()->GetSimpleEyefinity(&pSimple);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pSimple == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Simple Eyefinity not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");

            return ADLXInterfaceHandle.From(pSimple, addRef: false);
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

        public IADLXDisplayList* GetDesktopDisplayListNative(IADLXDesktop* desktop)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (desktop == null) throw new ArgumentNullException(nameof(desktop));

            IADLXDisplayList* list = null;
            var result = desktop->GetDisplays(&list);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || list == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display enumeration for desktop not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get desktop display list");

            return list;
        }

        public IReadOnlyList<DisplayInfo> EnumerateDesktopDisplays(IADLXDesktop* desktop)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (desktop == null) return Array.Empty<DisplayInfo>();

            using var list = new ComPtr<IADLXDisplayList>(GetDesktopDisplayListNative(desktop));
            var count = list.Get()->Size();
            var displays = new List<DisplayInfo>((int)count);
            for (uint i = 0; i < count; i++)
            {
                IADLXDisplay* display = null;
                list.Get()->At(i, &display);
                using var d = new ComPtr<IADLXDisplay>(display);
                displays.Add(new DisplayInfo(d.Get()));
            }

            return displays;
        }

        public (uint rows, uint cols) GetEyefinityGridSize(IADLXEyefinityDesktop* eyefinityDesktop)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (eyefinityDesktop == null) throw new ArgumentNullException(nameof(eyefinityDesktop));

            uint rows = 0, cols = 0;
            var result = eyefinityDesktop->GridSize(&rows, &cols);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query Eyefinity grid size");
            return (rows, cols);
        }

        public IReadOnlyList<DisplayInfo> EnumerateEyefinityDisplays(IADLXEyefinityDesktop* eyefinityDesktop)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (eyefinityDesktop == null) return Array.Empty<DisplayInfo>();

            var displays = new List<DisplayInfo>();
            var (rows, cols) = GetEyefinityGridSize(eyefinityDesktop);
            for (uint r = 0; r < rows; r++)
            {
                for (uint c = 0; c < cols; c++)
                {
                    IADLXDisplay* pDisplay = null;
                    var getResult = eyefinityDesktop->GetDisplay(r, c, &pDisplay);
                    if (getResult != ADLX_RESULT.ADLX_OK)
                        throw new ADLXException(getResult, $"Failed to get Eyefinity display at {r},{c}");
                    using var display = new ComPtr<IADLXDisplay>(pDisplay);
                    displays.Add(new DisplayInfo(display.Get()));
                }
            }

            return displays;
        }

        /// <summary>
        /// Acquire a desktop services pointer for the current call. Uses system to reacquire when available to avoid stale interfaces.
        /// </summary>
        private ComPtr<IADLXDesktopServices> AcquireDesktopServices(out IADLXDesktopServices* services)
        {
            if (_system != null)
            {
                IADLXDesktopServices* fresh = null;
                var result = _system->GetDesktopsServices(&fresh);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || fresh == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get desktop services");

                services = fresh;
                return new ComPtr<IADLXDesktopServices>(fresh);
            }

            services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            ADLXUtils.AddRefInterface((IntPtr)services);
            return new ComPtr<IADLXDesktopServices>(services);
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

    #region Desktop DTOs and listener handles
    public readonly struct DesktopInfo
    {
        public ADLX_DESKTOP_TYPE Type { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public int TopLeftX { get; init; }
        public int TopLeftY { get; init; }
        public ADLX_ORIENTATION Orientation { get; init; }

        [JsonConstructor]
        public DesktopInfo(ADLX_DESKTOP_TYPE type, int width, int height, int topLeftX, int topLeftY, ADLX_ORIENTATION orientation)
        {
            Type = type;
            Width = width;
            Height = height;
            TopLeftX = topLeftX;
            TopLeftY = topLeftY;
            Orientation = orientation;
        }

        internal unsafe DesktopInfo(IADLXDesktop* pDesktop)
        {
            ADLX_DESKTOP_TYPE type = default;
            pDesktop->Type(&type);
            Type = type;

            int w = 0, h = 0;
            pDesktop->Size(&w, &h);
            Width = w;
            Height = h;

            ADLX_Point topLeft = default;
            pDesktop->TopLeft(&topLeft);
            TopLeftX = topLeft.x;
            TopLeftY = topLeft.y;

            ADLX_ORIENTATION orientation = default;
            pDesktop->Orientation(&orientation);
            Orientation = orientation;
        }
    }

    public readonly struct SimpleEyefinityInfo
    {
        public bool IsSupported { get; init; }

        internal unsafe SimpleEyefinityInfo(IADLXSimpleEyefinity* p)
        {
            IsSupported = p != null;
        }
    }

    public readonly struct EyefinityDesktopInfo
    {
        public bool IsValid { get; init; }

        internal unsafe EyefinityDesktopInfo(IADLXEyefinityDesktop* p)
        {
            IsValid = p != null;
        }
    }

    public sealed unsafe class DesktopListListenerHandle : SafeHandle
    {
        public delegate void OnDesktopListChanged(IADLXDesktopList* pNewDesktops);

        private static readonly ConcurrentDictionary<IntPtr, OnDesktopListChanged> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IADLXDesktopList*, byte>)&OnDesktopListChangedThunk;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private DesktopListListenerHandle(OnDesktopListChanged cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static DesktopListListenerHandle Create(OnDesktopListChanged cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new DesktopListListenerHandle(cb);
        }

        public IADLXDesktopListChangedListener* GetListener() => (IADLXDesktopListChangedListener*)handle;

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnDesktopListChangedThunk(IntPtr pThis, IADLXDesktopList* pNewDesktops)
        {
            if (_map.TryGetValue(pThis, out var cb)) { cb(pNewDesktops); return 1; }
            return 0;
        }
    }
    #endregion
}

