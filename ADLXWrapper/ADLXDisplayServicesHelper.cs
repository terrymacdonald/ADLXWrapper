using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    public enum VariBrightMode
    {
        Unknown = 0,
        MaximizeBrightness,
        OptimizeBrightness,
        Balanced,
        OptimizeBattery,
        MaximizeBattery
    }

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

        /// <summary>
        /// Creates a display services helper from native display/desktop services.
        /// </summary>
        /// <param name="displayServices">Native display services pointer.</param>
        /// <param name="desktopServices">Optional desktop services pointer used to build display facades.</param>
        /// <param name="addRefDisplayServices">True to AddRef the display services pointer for this helper.</param>
        /// <param name="addRefDesktopServices">True to AddRef the desktop services pointer for this helper.</param>
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

        /// <summary>
        /// Returns the highest available display services interface (3, then 2, then base). Caller must not release it.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IADLXDisplayServices* GetDisplayServicesNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return GetHighestDisplayServices();
        }

        /// <summary>
        /// Returns an AddRef'd handle to the current display services interface.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public ADLXInterfaceHandle GetDisplayServicesHandle()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return ADLXInterfaceHandle.From(GetDisplayServicesNative(), addRef: true);
        }

        /// <summary>
        /// Enumerates managed display facades. Caller must dispose each display.
        /// </summary>
        /// <exception cref="ADLXException">If display services are unsupported or enumeration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IReadOnlyList<ADLXDisplay> EnumerateDisplays()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var displays = new List<ADLXDisplay>();
            using var displayList = new ComPtr<IADLXDisplayList>(GetDisplayListSafe());
            var count = displayList.Get()->Size();
            if (count == 0)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "No displays returned by ADLX.");
            for (uint i = 0; i < count; i++)
            {
                IADLXDisplay* pDisplay = null;
                var itemResult = displayList.Get()->At(i, &pDisplay);
                if (itemResult != ADLX_RESULT.ADLX_OK || pDisplay == null)
                {
                    if (pDisplay != null)
                        ADLXUtils.ReleaseInterface((IntPtr)pDisplay);
                    throw new ADLXException(itemResult, "Failed to access display from list");
                }

                displays.Add(CreateADLXDisplay(pDisplay, addRef: false));
            }

            return displays;
        }

        /// <summary>
        /// Enumerates displays that are part of an active desktop (in use). Callers must dispose each display.
        /// </summary>
        /// <exception cref="ADLXException">If display/desktop services are unsupported or enumeration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IReadOnlyList<ADLXDisplay> EnumerateDisplaysInUse()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetHighestDisplayServices();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported by this ADLX system");
            if (!_desktopServices.HasValue || _desktopServices.Value.Get() == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services were not provided for this display helper");

            var desktopServices = _desktopServices.Value.Get();
            var activeIds = new HashSet<ulong>();

            IADLXDesktopList* pDesktopList = null;
            var desktopsResult = desktopServices->GetDesktops(&pDesktopList);
            if (desktopsResult == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (desktopsResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(desktopsResult, "Failed to enumerate desktops while filtering displays in use");

            using (var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList))
            {
                for (uint i = 0; i < desktopList.Get()->Size(); i++)
                {
                    IADLXDesktop* pDesktop = null;
                    var itemResult = desktopList.Get()->At(i, &pDesktop);
                    if (itemResult != ADLX_RESULT.ADLX_OK || pDesktop == null)
                    {
                        if (pDesktop != null)
                            ADLXUtils.ReleaseInterface((IntPtr)pDesktop);
                        throw new ADLXException(itemResult, "Failed to access desktop while filtering displays in use");
                    }

                    IADLXDisplayList* pDisplayList = null;
                    var desktopDisplaysResult = pDesktop->GetDisplays(&pDisplayList);
                    if (desktopDisplaysResult == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDisplayList == null)
                    {
                        ADLXUtils.ReleaseInterface((IntPtr)pDesktop);
                        throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display enumeration for desktop not supported by this ADLX system");
                    }
                    if (desktopDisplaysResult != ADLX_RESULT.ADLX_OK)
                    {
                        ADLXUtils.ReleaseInterface((IntPtr)pDesktop);
                        throw new ADLXException(desktopDisplaysResult, "Failed to enumerate displays for desktop");
                    }

                    using var displayList = new ComPtr<IADLXDisplayList>(pDisplayList);
                    var displayCount = displayList.Get()->Size();
                    for (uint d = 0; d < displayCount; d++)
                    {
                        IADLXDisplay* pDisplay = null;
                        displayList.Get()->At(d, &pDisplay);
                        using var display = new ComPtr<IADLXDisplay>(pDisplay);
                        nuint uid = 0;
                        display.Get()->UniqueId(&uid);
                        activeIds.Add((ulong)uid);
                    }

                    ADLXUtils.ReleaseInterface((IntPtr)pDesktop);
                }
            }

            IADLXDisplayList* pAllDisplays = null;
            var displaysInUse = new List<ADLXDisplay>();
            using var allDisplays = new ComPtr<IADLXDisplayList>(GetDisplayListSafe());
            for (uint i = 0; i < allDisplays.Get()->Size(); i++)
            {
                IADLXDisplay* pDisplay = null;
                var itemResult = allDisplays.Get()->At(i, &pDisplay);
                if (itemResult != ADLX_RESULT.ADLX_OK || pDisplay == null)
                {
                    if (pDisplay != null)
                        ADLXUtils.ReleaseInterface((IntPtr)pDisplay);
                    throw new ADLXException(itemResult, "Failed to access display from list");
                }

                nuint uid = 0;
                pDisplay->UniqueId(&uid);
                if (activeIds.Contains((ulong)uid))
                {
                    displaysInUse.Add(CreateADLXDisplay(pDisplay, addRef: false));
                }
                else
                {
                    ADLXUtils.ReleaseInterface((IntPtr)pDisplay);
                }
            }

            return displaysInUse;
        }

        /// <summary>
        /// Tries to enumerate display facades, returning false if the feature is not supported.
        /// </summary>
        public bool TryEnumerateDisplays(out IReadOnlyList<ADLXDisplay> displays)
        {
            try
            {
                displays = EnumerateDisplays();
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                displays = Array.Empty<ADLXDisplay>();
                return false;
            }
        }

        public IReadOnlyList<ADLXDisplay> EnumerateADLXDisplays()
        {
            return EnumerateDisplays();
        }

        public bool TryEnumerateADLXDisplays(out IReadOnlyList<ADLXDisplay> displays)
        {
            return TryEnumerateDisplays(out displays);
        }

        /// <summary>
        /// Builds a display DTO from a native display pointer.
        /// </summary>
        /// <param name="display">Native display pointer.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="display"/> is null.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public DisplayInfo GetDisplayInfo(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (display == null) throw new ArgumentNullException(nameof(display));
            return new DisplayInfo(display);
        }

        /// <summary>
        /// Enumerates managed displays filtered by owning GPU unique ID. Disposes non-matching displays.
        /// </summary>
        /// <param name="gpuUniqueId">GPU unique ID to filter on.</param>
        /// <exception cref="ADLXException">If enumeration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IReadOnlyList<ADLXDisplay> EnumerateADLXDisplaysForGpu(int gpuUniqueId)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var allDisplays = EnumerateDisplays();
            var filtered = new List<ADLXDisplay>();
            foreach (var display in allDisplays)
            {
                if (display.GpuUniqueId == gpuUniqueId)
                {
                    filtered.Add(display);
                }
                else
                {
                    display.Dispose();
                }
            }
            return filtered;
        }

        /// <summary>
        /// Wraps a native display pointer in a managed facade.
        /// </summary>
        /// <param name="pDisplay">Native display pointer.</param>
        /// <param name="addRef">True to AddRef the pointer for this facade.</param>
        /// <exception cref="ADLXException">If display services are unavailable.</exception>
        /// <exception cref="ArgumentNullException">If <paramref name="pDisplay"/> is null.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public ADLXDisplay CreateADLXDisplay(IADLXDisplay* pDisplay, bool addRef = true)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)pDisplay);
            }

            var services = GetHighestDisplayServices();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported by this ADLX system");

            var desktopServices = _desktopServices.HasValue ? _desktopServices.Value.Get() : null;
            return new ADLXDisplay(services, pDisplay, desktopServices);
        }

        /// <summary>
        /// Returns the native display list. Caller must dispose the returned list.
        /// </summary>
        /// <exception cref="ADLXException">If display services are unsupported or enumeration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IADLXDisplayList* GetDisplayListNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return GetDisplayListSafe(); // caller must wrap/dispose
        }

        /// <summary>
        /// Enumerates display handles (AddRef'd) for native consumption.
        /// </summary>
        /// <exception cref="ADLXException">If display services are unsupported or enumeration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public ADLXInterfaceHandle[] EnumerateDisplayHandles()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            using var displayList = new ComPtr<IADLXDisplayList>(GetDisplayListSafe());
            var count = displayList.Get()->Size();
            var handles = new ADLXInterfaceHandle[count];

            for (uint i = 0; i < count; i++)
            {
                IADLXDisplay* pDisplay = null;
                displayList.Get()->At(i, &pDisplay);
                handles[i] = ADLXInterfaceHandle.From(pDisplay, addRef: false);
            }

            return handles;
        }

        /// <summary>
        /// Tries to enumerate display handles; returns false if not supported.
        /// </summary>
        public bool TryEnumerateDisplayHandles(out ADLXInterfaceHandle[] handles)
        {
            try
            {
                handles = EnumerateDisplayHandles();
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                handles = Array.Empty<ADLXInterfaceHandle>();
                return false;
            }
        }

        /// <summary>
        /// Enumerates a native display list using the base display services interface with the same call order as the native tests.
        /// </summary>
        /// <returns>Raw pointer to an IADLXDisplayList. Caller must wrap/dispose.</returns>
        /// <exception cref="ADLXException">If enumeration is unsupported or fails.</exception>
        private IADLXDisplayList* GetDisplayListSafe()
        {
            var baseServices = _displayServices.Get();
            if (baseServices == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported by this ADLX system");

            uint numDisplays = 0;
            ADLX_RESULT countResult;
            try
            {
                countResult = baseServices->GetNumberOfDisplays(&numDisplays);
            }
            catch (AccessViolationException ex)
            {
                throw new ADLXException(ADLX_RESULT.ADLX_FAIL, $"ADLX display count crashed: {ex.Message}");
            }

            if (countResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display count not supported by this ADLX system");
            if (countResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(countResult, "Failed to get display count");
            if (numDisplays == 0)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "No displays returned by ADLX.");

            IADLXDisplayList* pDisplayList = null;
            ADLX_RESULT listResult;
            try
            {
                listResult = baseServices->GetDisplays(&pDisplayList);
            }
            catch (AccessViolationException ex)
            {
                throw new ADLXException(ADLX_RESULT.ADLX_FAIL, $"ADLX display enumeration crashed: {ex.Message}");
            }

            if (listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDisplayList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display enumeration not supported by this ADLX system");
            if (listResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(listResult, "Failed to enumerate displays");

            return pDisplayList;
        }

        /// <summary>
        /// Gets the display change handling interface, caching the result.
        /// </summary>
        /// <exception cref="ADLXException">If unsupported or retrieval fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public IADLXDisplayChangedHandling* GetDisplayChangedHandlingNative()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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

        /// <summary>
        /// Returns an AddRef'd handle to the display change handling interface.
        /// </summary>
        public ADLXInterfaceHandle GetDisplayChangedHandling()
        {
            return ADLXInterfaceHandle.From(GetDisplayChangedHandlingNative(), addRef: true);
        }

        /// <summary>
        /// Adds a display list change listener.
        /// </summary>
        /// <param name="callback">Callback invoked when the display list changes.</param>
        /// <returns>Listener handle that must be disposed to unsubscribe.</returns>
        /// <exception cref="ADLXException">If registration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public DisplayListListenerHandle AddDisplayListEventListener(DisplayListListenerHandle.OnDisplayListChanged callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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

        /// <summary>
        /// Removes a display list change listener.
        /// </summary>
        /// <param name="handle">Handle returned by add.</param>
        /// <param name="disposeHandle">True to dispose the handle after removal.</param>
        public void RemoveDisplayListEventListener(DisplayListListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDisplayChangedHandlingNative();
            handling->RemoveDisplayListEventListener(handle.GetListener());

            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        /// <summary>
        /// Adds a display gamut change listener.
        /// </summary>
        /// <param name="callback">Callback invoked when gamut changes.</param>
        /// <returns>Listener handle that must be disposed to unsubscribe.</returns>
        /// <exception cref="ADLXException">If registration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public DisplayGamutListenerHandle AddDisplayGamutEventListener(DisplayGamutListenerHandle.OnDisplayGamutChanged callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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

        /// <summary>
        /// Removes a display gamut change listener.
        /// </summary>
        /// <param name="handle">Handle returned by add.</param>
        /// <param name="disposeHandle">True to dispose the handle after removal.</param>
        public void RemoveDisplayGamutEventListener(DisplayGamutListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDisplayChangedHandlingNative();
            handling->RemoveDisplayGamutEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        /// <summary>
        /// Adds a display gamma change listener.
        /// </summary>
        /// <param name="callback">Callback invoked when gamma changes.</param>
        /// <returns>Listener handle that must be disposed to unsubscribe.</returns>
        /// <exception cref="ADLXException">If registration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public DisplayGammaListenerHandle AddDisplayGammaEventListener(DisplayGammaListenerHandle.OnDisplayGammaChanged callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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

        /// <summary>
        /// Removes a display gamma change listener.
        /// </summary>
        /// <param name="handle">Handle returned by add.</param>
        /// <param name="disposeHandle">True to dispose the handle after removal.</param>
        public void RemoveDisplayGammaEventListener(DisplayGammaListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDisplayChangedHandlingNative();
            handling->RemoveDisplayGammaEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        /// <summary>
        /// Adds a display 3D LUT change listener.
        /// </summary>
        /// <param name="callback">Callback invoked when 3D LUT changes.</param>
        /// <returns>Listener handle that must be disposed to unsubscribe.</returns>
        /// <exception cref="ADLXException">If registration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public Display3DLutListenerHandle AddDisplay3DLutEventListener(Display3DLutListenerHandle.OnDisplay3DLutChanged callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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

        /// <summary>
        /// Removes a display 3D LUT change listener.
        /// </summary>
        /// <param name="handle">Handle returned by add.</param>
        /// <param name="disposeHandle">True to dispose the handle after removal.</param>
        public void RemoveDisplay3DLutEventListener(Display3DLutListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (handle == null || handle.IsInvalid)
                return;

            var handling = GetDisplayChangedHandlingNative();
            handling->RemoveDisplay3DLUTEventListener(handle.GetListener());
            if (disposeHandle)
            {
                handle.Dispose();
            }
        }

        /// <summary>
        /// Adds a display settings change listener.
        /// </summary>
        /// <param name="callback">Callback invoked when display settings change.</param>
        /// <returns>Listener handle that must be disposed to unsubscribe.</returns>
        /// <exception cref="ADLXException">If registration fails.</exception>
        /// <exception cref="ObjectDisposedException">If disposed.</exception>
        public DisplaySettingsListenerHandle AddDisplaySettingsEventListener(DisplaySettingsListenerHandle.OnDisplaySettingsChanged callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            if (callback == null) throw new ArgumentNullException(nameof(callback));
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

        /// <summary>
        /// Removes a display settings change listener.
        /// </summary>
        /// <param name="handle">Handle returned by add.</param>
        /// <param name="disposeHandle">True to dispose the handle after removal.</param>
        public void RemoveDisplaySettingsEventListener(DisplaySettingsListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
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

        #region Display settings (migrated from DisplaySettingsOps)
        public (bool supported, bool enabled) GetFreeSyncState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var fs = new ComPtr<IADLXDisplayFreeSync>((IADLXDisplayFreeSync*)DisplaySettingsOps.GetFreeSyncHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetFreeSyncState((IntPtr)fs.Get());
        }

        public void SetFreeSyncEnabled(IADLXDisplay* display, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var fs = new ComPtr<IADLXDisplayFreeSync>((IADLXDisplayFreeSync*)DisplaySettingsOps.GetFreeSyncHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetFreeSyncEnabled((IntPtr)fs.Get(), enable);
        }

        public bool TrySetFreeSyncEnabled(IADLXDisplay* display, bool enable)
        {
            try
            {
                SetFreeSyncEnabled(display, enable);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, bool enabled) GetGPUScalingState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var scaling = new ComPtr<IADLXDisplayGPUScaling>((IADLXDisplayGPUScaling*)DisplaySettingsOps.GetGPUScalingHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetGPUScalingState((IntPtr)scaling.Get());
        }

        public void SetGPUScalingEnabled(IADLXDisplay* display, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var scaling = new ComPtr<IADLXDisplayGPUScaling>((IADLXDisplayGPUScaling*)DisplaySettingsOps.GetGPUScalingHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetGPUScalingEnabled((IntPtr)scaling.Get(), enable);
        }

        public bool TrySetGPUScalingEnabled(IADLXDisplay* display, bool enable)
        {
            try
            {
                SetGPUScalingEnabled(display, enable);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, ADLX_SCALE_MODE mode) GetScalingMode(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var scalingMode = new ComPtr<IADLXDisplayScalingMode>((IADLXDisplayScalingMode*)DisplaySettingsOps.GetScalingModeHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetScalingMode((IntPtr)scalingMode.Get());
        }

        public void SetScalingMode(IADLXDisplay* display, ADLX_SCALE_MODE mode)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var scalingMode = new ComPtr<IADLXDisplayScalingMode>((IADLXDisplayScalingMode*)DisplaySettingsOps.GetScalingModeHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetScalingMode((IntPtr)scalingMode.Get(), mode);
        }

        public bool TrySetScalingMode(IADLXDisplay* display, ADLX_SCALE_MODE mode)
        {
            try
            {
                SetScalingMode(display, mode);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, bool enabled) GetVirtualSuperResolutionState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var vsr = new ComPtr<IADLXDisplayVSR>((IADLXDisplayVSR*)DisplaySettingsOps.GetVirtualSuperResolutionHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetVirtualSuperResolutionState((IntPtr)vsr.Get());
        }

        public void SetVirtualSuperResolutionEnabled(IADLXDisplay* display, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var vsr = new ComPtr<IADLXDisplayVSR>((IADLXDisplayVSR*)DisplaySettingsOps.GetVirtualSuperResolutionHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetVirtualSuperResolutionEnabled((IntPtr)vsr.Get(), enable);
        }

        public bool TrySetVirtualSuperResolutionEnabled(IADLXDisplay* display, bool enable)
        {
            try
            {
                SetVirtualSuperResolutionEnabled(display, enable);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, bool enabled) GetIntegerScalingState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var integerScaling = new ComPtr<IADLXDisplayIntegerScaling>((IADLXDisplayIntegerScaling*)DisplaySettingsOps.GetIntegerScalingHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetIntegerScalingState((IntPtr)integerScaling.Get());
        }

        public void SetIntegerScalingEnabled(IADLXDisplay* display, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var integerScaling = new ComPtr<IADLXDisplayIntegerScaling>((IADLXDisplayIntegerScaling*)DisplaySettingsOps.GetIntegerScalingHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetIntegerScalingEnabled((IntPtr)integerScaling.Get(), enable);
        }

        public bool TrySetIntegerScalingEnabled(IADLXDisplay* display, bool enable)
        {
            try
            {
                SetIntegerScalingEnabled(display, enable);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, bool enabled) GetHDCPState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            using var hdcp = new ComPtr<IADLXDisplayHDCP>((IADLXDisplayHDCP*)DisplaySettingsOps.GetHDCPHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetHDCPState((IntPtr)hdcp.Get());
        }

        public void SetHDCPEnabled(IADLXDisplay* display, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            using var hdcp = new ComPtr<IADLXDisplayHDCP>((IADLXDisplayHDCP*)DisplaySettingsOps.GetHDCPHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetHDCPEnabled((IntPtr)hdcp.Get(), enable);
        }

        public bool TrySetHDCPEnabled(IADLXDisplay* display, bool enable)
        {
            try
            {
                SetHDCPEnabled(display, enable);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, bool enabled, VariBrightMode mode) GetVariBrightState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            using var vb = new ComPtr<IADLXDisplayVariBright>((IADLXDisplayVariBright*)DisplaySettingsOps.GetVariBrightHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetVariBrightState((IntPtr)vb.Get());
        }

        public void SetVariBright(IADLXDisplay* display, bool enable, VariBrightMode mode)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            using var vb = new ComPtr<IADLXDisplayVariBright>((IADLXDisplayVariBright*)DisplaySettingsOps.GetVariBrightHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetVariBright((IntPtr)vb.Get(), enable, mode);
        }

        public bool TrySetVariBright(IADLXDisplay* display, bool enable, VariBrightMode mode)
        {
            try
            {
                SetVariBright(display, enable, mode);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public void SetVariBrightBacklightAdaptiveEnabled(IADLXDisplayVariBright1* variBright, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            DisplaySettingsOps.SetVariBrightBacklightAdaptiveEnabled(variBright, enable);
        }

        public bool TrySetVariBrightBacklightAdaptiveEnabled(IADLXDisplayVariBright1* variBright, bool enable)
        {
            try
            {
                SetVariBrightBacklightAdaptiveEnabled(variBright, enable);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public void SetVariBrightBatteryLifeEnabled(IADLXDisplayVariBright1* variBright, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            DisplaySettingsOps.SetVariBrightBatteryLifeEnabled(variBright, enable);
        }

        public bool TrySetVariBrightBatteryLifeEnabled(IADLXDisplayVariBright1* variBright, bool enable)
        {
            try
            {
                SetVariBrightBatteryLifeEnabled(variBright, enable);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, ADLX_COLOR_DEPTH current) GetColorDepthState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var cd = new ComPtr<IADLXDisplayColorDepth>((IADLXDisplayColorDepth*)DisplaySettingsOps.GetColorDepthHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetColorDepthState((IntPtr)cd.Get());
        }

        public void SetColorDepth(IADLXDisplay* display, ADLX_COLOR_DEPTH depth)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var cd = new ComPtr<IADLXDisplayColorDepth>((IADLXDisplayColorDepth*)DisplaySettingsOps.GetColorDepthHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetColorDepth((IntPtr)cd.Get(), depth);
        }

        public bool TrySetColorDepth(IADLXDisplay* display, ADLX_COLOR_DEPTH depth)
        {
            try
            {
                SetColorDepth(display, depth);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, ADLX_PIXEL_FORMAT current) GetPixelFormatState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var pf = new ComPtr<IADLXDisplayPixelFormat>((IADLXDisplayPixelFormat*)DisplaySettingsOps.GetPixelFormatHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetPixelFormatState((IntPtr)pf.Get());
        }

        public void SetPixelFormat(IADLXDisplay* display, ADLX_PIXEL_FORMAT format)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServicesForSettings();
            using var pf = new ComPtr<IADLXDisplayPixelFormat>((IADLXDisplayPixelFormat*)DisplaySettingsOps.GetPixelFormatHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetPixelFormat((IntPtr)pf.Get(), format);
        }

        public bool TrySetPixelFormat(IADLXDisplay* display, ADLX_PIXEL_FORMAT format)
        {
            try
            {
                SetPixelFormat(display, format);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public CustomColorInfo GetCustomColor(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return DisplaySettingsOps.GetCustomColor(GetHighestDisplayServices(), display);
        }

        public void ApplyCustomColor(IADLXDisplay* display, CustomColorInfo info)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            IADLXDisplayCustomColor* pCustomColor;
            var result = GetHighestDisplayServices()->GetCustomColor(display, &pCustomColor);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pCustomColor == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Custom Color not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Custom Color interface");
            using var customColor = new ComPtr<IADLXDisplayCustomColor>(pCustomColor);
            DisplaySettingsOps.ApplyCustomColor(customColor.Get(), info);
        }

        public bool TryApplyCustomColor(IADLXDisplay* display, CustomColorInfo info)
        {
            try
            {
                ApplyCustomColor(display, info);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public GammaInfo GetGamma(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return DisplaySettingsOps.GetGamma(GetHighestDisplayServices(), display);
        }

        public void ReapplyGamma(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            IADLXDisplayGamma* pGamma;
            var result = GetHighestDisplayServices()->GetGamma(display, &pGamma);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pGamma == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Gamma not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Gamma interface");
            using var gamma = new ComPtr<IADLXDisplayGamma>(pGamma);
            DisplaySettingsOps.ReapplyGamma(gamma.Get());
        }

        public bool TryReapplyGamma(IADLXDisplay* display)
        {
            try
            {
                ReapplyGamma(display);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public GamutInfo GetGamut(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return DisplaySettingsOps.GetGamut(GetHighestDisplayServices(), display);
        }

        public void ReapplyGamut(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            IADLXDisplayGamut* pGamut;
            var result = GetHighestDisplayServices()->GetGamut(display, &pGamut);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pGamut == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Gamut not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Gamut interface");
            using var gamut = new ComPtr<IADLXDisplayGamut>(pGamut);
            DisplaySettingsOps.ReapplyGamut(gamut.Get());
        }

        public bool TryReapplyGamut(IADLXDisplay* display)
        {
            try
            {
                ReapplyGamut(display);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public ThreeDLUTInfo GetThreeDLut(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return DisplaySettingsOps.Get3DLUT(GetHighestDisplayServices(), display);
        }

        public void ReapplyThreeDLut(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            IADLXDisplay3DLUT* pLut;
            var result = GetHighestDisplayServices()->Get3DLUT(display, &pLut);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pLut == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "3DLUT not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get 3DLUT interface");
            using var lut = new ComPtr<IADLXDisplay3DLUT>(pLut);
            DisplaySettingsOps.Reapply3DLUT(lut.Get());
        }

        public bool TryReapplyThreeDLut(IADLXDisplay* display)
        {
            try
            {
                ReapplyThreeDLut(display);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public ConnectivityExperienceInfo GetConnectivityExperience(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            return DisplaySettingsOps.GetDisplayConnectivityExperience((IADLXDisplayServices*)services, display);
        }

        public void ApplyConnectivityExperience(IADLXDisplay* display, ConnectivityExperienceInfo info)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            IADLXDisplayConnectivityExperience* pConn;
            var services = GetDisplayServices3OrThrow();
            var result = services->GetDisplayConnectivityExperience(display, &pConn);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pConn == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display Connectivity Experience not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Display Connectivity Experience interface");
            using var conn = new ComPtr<IADLXDisplayConnectivityExperience>(pConn);
            DisplaySettingsOps.ApplyDisplayConnectivityExperience(conn.Get(), info);
        }

        public bool TryApplyConnectivityExperience(IADLXDisplay* display, ConnectivityExperienceInfo info)
        {
            try
            {
                ApplyConnectivityExperience(display, info);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, bool blanked) GetDisplayBlankingState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            using var blanking = new ComPtr<IADLXDisplayBlanking>((IADLXDisplayBlanking*)DisplaySettingsOps.GetDisplayBlankingHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetDisplayBlankingState((IntPtr)blanking.Get());
        }

        public void SetDisplayBlanked(IADLXDisplay* display, bool blank)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            using var blanking = new ComPtr<IADLXDisplayBlanking>((IADLXDisplayBlanking*)DisplaySettingsOps.GetDisplayBlankingHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetDisplayBlanked((IntPtr)blanking.Get(), blank);
        }

        public bool TrySetDisplayBlanked(IADLXDisplay* display, bool blank)
        {
            try
            {
                SetDisplayBlanked(display, blank);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, bool enabled) GetFreeSyncColorAccuracyState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            using var fsca = new ComPtr<IADLXDisplayFreeSyncColorAccuracy>((IADLXDisplayFreeSyncColorAccuracy*)DisplaySettingsOps.GetFreeSyncColorAccuracyHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetFreeSyncColorAccuracyState((IntPtr)fsca.Get());
        }

        public void SetFreeSyncColorAccuracyEnabled(IADLXDisplay* display, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            using var fsca = new ComPtr<IADLXDisplayFreeSyncColorAccuracy>((IADLXDisplayFreeSyncColorAccuracy*)DisplaySettingsOps.GetFreeSyncColorAccuracyHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetFreeSyncColorAccuracyEnabled((IntPtr)fsca.Get(), enable);
        }

        public bool TrySetFreeSyncColorAccuracyEnabled(IADLXDisplay* display, bool enable)
        {
            try
            {
                SetFreeSyncColorAccuracyEnabled(display, enable);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public (bool supported, bool enabled) GetDynamicRefreshRateControlState(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            using var drr = new ComPtr<IADLXDisplayDynamicRefreshRateControl>((IADLXDisplayDynamicRefreshRateControl*)DisplaySettingsOps.GetDynamicRefreshRateControlHandle((IntPtr)services, (IntPtr)display));
            return DisplaySettingsOps.GetDynamicRefreshRateControlState((IntPtr)drr.Get());
        }

        public void SetDynamicRefreshRateControlEnabled(IADLXDisplay* display, bool enable)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var services = GetDisplayServices3OrThrow();
            using var drr = new ComPtr<IADLXDisplayDynamicRefreshRateControl>((IADLXDisplayDynamicRefreshRateControl*)DisplaySettingsOps.GetDynamicRefreshRateControlHandle((IntPtr)services, (IntPtr)display));
            DisplaySettingsOps.SetDynamicRefreshRateControlEnabled((IntPtr)drr.Get(), enable);
        }

        public bool TrySetDynamicRefreshRateControlEnabled(IADLXDisplay* display, bool enable)
        {
            try
            {
                SetDynamicRefreshRateControlEnabled(display, enable);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }

        public IEnumerable<DisplayResolutionInfo> EnumerateCustomResolutions(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return DisplaySettingsOps.EnumerateCustomResolutions(GetHighestDisplayServices(), display);
        }

        public IADLXDisplayResolutionList* GetCustomResolutionListNative(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return DisplaySettingsOps.GetCustomResolutionListNative(GetHighestDisplayServices(), display);
        }

        public CustomResolutionInfo GetCustomResolution(IADLXDisplay* display)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            return DisplaySettingsOps.GetCustomResolution(GetHighestDisplayServices(), display);
        }

        public void ApplyCustomResolution(IADLXDisplayCustomResolution* customRes, DisplayResolutionInfo info)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            DisplaySettingsOps.ApplyCustomResolution(customRes, info);
        }

        public bool TryApplyCustomResolution(IADLXDisplayCustomResolution* customRes, DisplayResolutionInfo info)
        {
            try
            {
                ApplyCustomResolution(customRes, info);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                return false;
            }
        }
        #endregion
        private static unsafe class DisplaySettingsOps
        {
            /// <summary>
            /// Gets the Gamma settings for a specific display.
            /// </summary>
            public static GammaInfo GetGamma(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
            {
                if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));
    
                IADLXDisplayGamma* pGamma;
                var result = pDisplayServices->GetGamma(pDisplay, &pGamma);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pGamma == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Gamma not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK) 
                    throw new ADLXException(result, "Failed to get Gamma interface");
                using var gamma = new ComPtr<IADLXDisplayGamma>(pGamma);
                return new GammaInfo(gamma.Get());
            }
    
            /// <summary>
            /// Applies the settings from a GammaInfo object to the hardware.
            /// </summary>
            public static void ApplyGamma(IADLXDisplayGamma* pGamma, GammaInfo info)
            {
                if (pGamma == null) throw new ArgumentNullException(nameof(pGamma));
                if (info.IsSupported == false) return;

                ADLX_RESULT r = ADLX_RESULT.ADLX_OK;

                if (info.IsCurrentReGammaSRGB) { r = pGamma->SetReGammaSRGB(); }
                else if (info.IsCurrentReGammaBT709) { r = pGamma->SetReGammaBT709(); }
                else if (info.IsCurrentReGammaPQ) { r = pGamma->SetReGammaPQ(); }
                else if (info.IsCurrentReGammaPQ2084) { r = pGamma->SetReGammaPQ2084Interim(); }
                else if (info.IsCurrentReGamma36) { r = pGamma->SetReGamma36(); }
                else if (info.HasRegammaCoefficient) { r = pGamma->SetReGammaCoefficient(info.RegammaCoefficient); }
                else if (info.HasReGammaRamp) { r = pGamma->SetReGammaRamp(info.ReGammaRamp); }
                else if (info.HasDeGammaRamp) { r = pGamma->SetDeGammaRamp(info.DeGammaRamp); }
                else { r = pGamma->ResetGammaRamp(); }

                if (r != ADLX_RESULT.ADLX_OK && r != ADLX_RESULT.ADLX_NOT_SUPPORTED)
                    throw new ADLXException(r, "Failed to apply gamma");
            }

            /// <summary>
            /// Reapplies the current Gamma settings.
            /// </summary>
            public static void ReapplyGamma(IADLXDisplayGamma* pGamma)
            {
                if (pGamma == null) throw new ArgumentNullException(nameof(pGamma));
                ApplyGamma(pGamma, new GammaInfo(pGamma));
            }
    
            /// <summary>
            /// Gets the Gamut settings for a specific display.
            /// </summary>
            public static GamutInfo GetGamut(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
            {
                if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));
    
                IADLXDisplayGamut* pGamut;
                var result = pDisplayServices->GetGamut(pDisplay, &pGamut);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pGamut == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Gamut not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Gamut interface");
                using var gamut = new ComPtr<IADLXDisplayGamut>(pGamut);
                return new GamutInfo(gamut.Get());
            }
    
            /// <summary>
            /// Applies the settings from a GamutInfo object to the hardware.
            /// </summary>
            public static void ApplyGamut(IADLXDisplayGamut* pGamut, GamutInfo info)
            {
                if (pGamut == null) throw new ArgumentNullException(nameof(pGamut));
                if (info.IsGamutSupported == false && info.IsWhitePointSupported == false) return;

                ADLX_RESULT r = ADLX_RESULT.ADLX_OK;

                ADLX_WHITE_POINT? wp = info.IsCurrent5000K ? ADLX_WHITE_POINT.WHITE_POINT_5000K :
                                       info.IsCurrent6500K ? ADLX_WHITE_POINT.WHITE_POINT_6500K :
                                       info.IsCurrent7500K ? ADLX_WHITE_POINT.WHITE_POINT_7500K :
                                       info.IsCurrent9300K ? ADLX_WHITE_POINT.WHITE_POINT_9300K :
                                       (ADLX_WHITE_POINT?)null;

                ADLX_GAMUT_SPACE? gamutSpace = info.IsCurrent709 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_709 :
                                             info.IsCurrent601 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_601 :
                                             info.IsCurrentAdobe ? ADLX_GAMUT_SPACE.GAMUT_SPACE_ADOBE_RGB :
                                             info.IsCurrentCieRgb ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CIE_RGB :
                                             info.IsCurrent2020 ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CCIR_2020 :
                                             info.IsCurrentCustomColorSpace ? ADLX_GAMUT_SPACE.GAMUT_SPACE_CUSTOM :
                                             (ADLX_GAMUT_SPACE?)null;

                bool hasCustomWhitePoint = info.HasWhitePoint || info.IsCurrentCustomWhitePoint;

                if (gamutSpace.HasValue)
                {
                    if (wp.HasValue)
                    {
                        r = pGamut->SetGamut(wp.Value, gamutSpace.Value);
                    }
                    else if (hasCustomWhitePoint)
                    {
                        ADLX_RGB white = new ADLX_RGB { gamutR = info.WhitePoint.x, gamutG = info.WhitePoint.y, gamutB = 0 };
                        r = pGamut->SetGamut(white, gamutSpace.Value);
                    }
                    else
                    {
                        return;
                    }
                }
                else if (wp.HasValue)
                {
                    r = pGamut->SetGamut(wp.Value, info.CurrentGamutSpace);
                }
                else if (hasCustomWhitePoint)
                {
                    ADLX_RGB white = new ADLX_RGB { gamutR = info.WhitePoint.x, gamutG = info.WhitePoint.y, gamutB = 0 };
                    r = pGamut->SetGamut(white, info.CurrentGamutSpace);
                }
                else
                {
                    return; // Nothing to reapply
                }

                if (r != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r, "Failed to reapply gamut configuration");
            }

            /// <summary>
            /// Reapplies the current Gamut settings.
            /// </summary>
            public static void ReapplyGamut(IADLXDisplayGamut* pGamut)
            {
                if (pGamut == null) throw new ArgumentNullException(nameof(pGamut));

                ApplyGamut(pGamut, new GamutInfo(pGamut));
            }
    
            /// <summary>
            /// Gets the 3DLUT settings for a specific display.
            /// </summary>
            public static ThreeDLUTInfo Get3DLUT(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
            {
                if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));
    
                IADLXDisplay3DLUT* pLut;
                var result = pDisplayServices->Get3DLUT(pDisplay, &pLut);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pLut == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "3DLUT not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get 3DLUT interface");
                using var lut = new ComPtr<IADLXDisplay3DLUT>(pLut);
                return new ThreeDLUTInfo(lut.Get());
            }
    
            /// <summary>
            /// Applies the settings from a ThreeDLUTInfo object to the hardware.
            /// </summary>
            public static void Apply3DLUT(IADLXDisplay3DLUT* p3dLut, ThreeDLUTInfo info)
            {
                if (p3dLut == null) throw new ArgumentNullException(nameof(p3dLut));
                if (info.IsSceSupported == false && info.IsSceVividGamingSupported == false && info.IsSceDynamicContrastSupported == false) return;

                if (info.IsSceSupported || info.IsSceVividGamingSupported)
                {
                    ADLX_RESULT sceResult = ADLX_RESULT.ADLX_OK;
                    if (info.IsCurrentSceVividGaming && info.IsSceVividGamingSupported)
                    {
                        sceResult = p3dLut->SetSCEVividGaming();
                    }
                    else
                    {
                        sceResult = p3dLut->SetSCEDisabled();
                    }

                    if (sceResult != ADLX_RESULT.ADLX_OK && sceResult != ADLX_RESULT.ADLX_NOT_SUPPORTED)
                        throw new ADLXException(sceResult, "Failed to apply SCE state");
                }

                if (info.IsSceDynamicContrastSupported && info.HasDynamicContrast)
                {
                    int clamped = Math.Clamp(info.CurrentDynamicContrast, info.DynamicContrastRange.minValue, info.DynamicContrastRange.maxValue);
                    var r = p3dLut->SetSCEDynamicContrast(clamped);
                    if (r != ADLX_RESULT.ADLX_OK && r != ADLX_RESULT.ADLX_NOT_SUPPORTED)
                        throw new ADLXException(r, "Failed to apply SCE dynamic contrast");
                }
            }

            /// <summary>
            /// Reapplies the current 3DLUT settings.
            /// </summary>
            public static void Reapply3DLUT(IADLXDisplay3DLUT* p3dLut)
            {
                if (p3dLut == null) throw new ArgumentNullException(nameof(p3dLut));
                Apply3DLUT(p3dLut, new ThreeDLUTInfo(p3dLut));
            }
    
            /// <summary>
            /// Gets the Display Connectivity Experience settings for a specific display.
            /// </summary>
            public static ConnectivityExperienceInfo GetDisplayConnectivityExperience(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
            {
                if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));
    
                var services3 = (IADLXDisplayServices3*)pDisplayServices;
                IADLXDisplayConnectivityExperience* pConn;
                var result = services3->GetDisplayConnectivityExperience(pDisplay, &pConn);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pConn == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display Connectivity Experience not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Display Connectivity Experience interface");
    
                using var connectivity = new ComPtr<IADLXDisplayConnectivityExperience>(pConn);
                return new ConnectivityExperienceInfo(connectivity.Get());
            }
    
            /// <summary>
            /// Applies the settings from a ConnectivityExperienceInfo object to the hardware.
            /// </summary>
            public static void ApplyDisplayConnectivityExperience(IADLXDisplayConnectivityExperience* pConnectivity, ConnectivityExperienceInfo info)
            {
                if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));
    
                if (info.IsHdmiQualityDetectionSupported) SetDisplayConnectivityHDMIQualityDetectionEnabled(pConnectivity, info.IsHdmiQualityDetectionEnabled);
                if (info.IsRelativePreEmphasisSupported) SetDisplayConnectivityRelativePreEmphasis(pConnectivity, info.RelativePreEmphasis);
                if (info.IsRelativeVoltageSwingSupported) SetDisplayConnectivityRelativeVoltageSwing(pConnectivity, info.RelativeVoltageSwing);
            }
    
            /// <summary>
            /// Sets the enabled state of HDMI Quality Detection.
            /// </summary>
            public static void SetDisplayConnectivityHDMIQualityDetectionEnabled(IADLXDisplayConnectivityExperience* pConnectivity, bool enable)
            {
                if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));
    
                var result = pConnectivity->SetEnabledHDMIQualityDetection(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set HDMI quality detection");
            }
    
            /// <summary>
            /// Sets the relative pre-emphasis for display connectivity.
            /// </summary>
            public static void SetDisplayConnectivityRelativePreEmphasis(IADLXDisplayConnectivityExperience* pConnectivity, int value)
            {
                if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));
                var result = pConnectivity->SetRelativePreEmphasis(value);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set relative pre-emphasis");
            }
    
            /// <summary>
            /// Sets the relative voltage swing for display connectivity.
            /// </summary>
            public static void SetDisplayConnectivityRelativeVoltageSwing(IADLXDisplayConnectivityExperience* pConnectivity, int value)
            {
                if (pConnectivity == null) throw new ArgumentNullException(nameof(pConnectivity));
                var result = pConnectivity->SetRelativeVoltageSwing(value);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set relative voltage swing");
            }
    
            /// <summary>
            /// Gets the Custom Resolution settings for a specific display.
            /// </summary>
            public static CustomResolutionInfo GetCustomResolution(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
            {
                if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));
    
                IADLXDisplayCustomResolution* pCustomRes;
                var result = pDisplayServices->GetCustomResolution(pDisplay, &pCustomRes);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pCustomRes == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Custom Resolution not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Custom Resolution interface");
    
                using var customRes = new ComPtr<IADLXDisplayCustomResolution>(pCustomRes);
                return new CustomResolutionInfo(customRes.Get());
            }
    
            /// <summary>
            /// Returns the native custom resolution list for a display. Caller must wrap/dispose.
            /// </summary>
            public static IADLXDisplayResolutionList* GetCustomResolutionListNative(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
            {
                if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));
    
                IADLXDisplayCustomResolution* pCustomRes;
                var result = pDisplayServices->GetCustomResolution(pDisplay, &pCustomRes);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pCustomRes == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Custom Resolution not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Custom Resolution interface");
    
                using var customRes = new ComPtr<IADLXDisplayCustomResolution>(pCustomRes);
                IADLXDisplayResolutionList* list = null;
                var listResult = customRes.Get()->GetResolutionList(&list);
                if (listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED || list == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Custom Resolution list not supported by this ADLX system");
                if (listResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(listResult, "Failed to get Custom Resolution list");
    
                return list;
            }
    
            /// <summary>
            /// Enumerates custom resolutions for a display.
            /// </summary>
            public static IEnumerable<DisplayResolutionInfo> EnumerateCustomResolutions(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
            {
                if (pDisplayServices == null || pDisplay == null) return Array.Empty<DisplayResolutionInfo>();
    
                using var list = new ComPtr<IADLXDisplayResolutionList>(GetCustomResolutionListNative(pDisplayServices, pDisplay));
                var count = list.Get()->Size();
                var resolutions = new List<DisplayResolutionInfo>((int)count);
                for (uint i = 0; i < count; i++)
                {
                    IADLXDisplayResolution* pRes;
                    list.Get()->At(i, &pRes);
                    using var res = new ComPtr<IADLXDisplayResolution>(pRes);
                    resolutions.Add(new DisplayResolutionInfo(res.Get()));
                }
    
                return resolutions;
            }
    
            /// <summary>
            /// Applies a custom resolution.
            /// </summary>
            public static void ApplyCustomResolution(IADLXDisplayCustomResolution* pCustomRes, DisplayResolutionInfo info)
            {
                if (pCustomRes == null) throw new ArgumentNullException(nameof(pCustomRes));
    
                // Creating a new resolution is a complex operation that requires a valid IADLXDisplayResolution object.
                // For now, we assume the user would handle creating the IADLXDisplayResolution object themselves.
            }
    
            /// <summary>
            /// Sets the enabled state of Vari-Bright Backlight Adaptive.
            /// </summary>
            public static void SetVariBrightBacklightAdaptiveEnabled(IADLXDisplayVariBright1* pVariBright, bool enable)
            {
                if (pVariBright == null) throw new ArgumentNullException(nameof(pVariBright));
                var result = pVariBright->SetBacklightAdaptiveEnabled(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set backlight adaptive mode");
            }
    
            /// <summary>
            /// Sets the enabled state of Vari-Bright Battery Life.
            /// </summary>
            public static void SetVariBrightBatteryLifeEnabled(IADLXDisplayVariBright1* pVariBright, bool enable)
            {
                if (pVariBright == null) throw new ArgumentNullException(nameof(pVariBright));
                var result = pVariBright->SetBatteryLifeEnabled(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set battery life mode");
            }
    
            /// <summary>
            /// Gets the Custom Color settings for a specific display.
            /// </summary>
            public static CustomColorInfo GetCustomColor(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
            {
                if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));
    
                IADLXDisplayCustomColor* pCustomColor;
                var result = pDisplayServices->GetCustomColor(pDisplay, &pCustomColor);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pCustomColor == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Custom Color not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Custom Color interface");
    
                using var customColor = new ComPtr<IADLXDisplayCustomColor>(pCustomColor);
                return new CustomColorInfo(customColor.Get());
            }
    
            /// <summary>
            /// Applies the settings from a CustomColorInfo object to the hardware.
            /// </summary>
            public static void ApplyCustomColor(IADLXDisplayCustomColor* pCustomColor, CustomColorInfo info)
            {
                if (pCustomColor == null) throw new ArgumentNullException(nameof(pCustomColor));
                if (info.IsSupported == false) return;
    
                if (info.IsHueSupported) { var r = pCustomColor->SetHue(info.Hue); if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to set Custom Color hue"); }
                if (info.IsSaturationSupported) { var r = pCustomColor->SetSaturation(info.Saturation); if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to set Custom Color saturation"); }
                if (info.IsBrightnessSupported) { var r = pCustomColor->SetBrightness(info.Brightness); if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to set Custom Color brightness"); }
                if (info.IsContrastSupported) { var r = pCustomColor->SetContrast(info.Contrast); if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to set Custom Color contrast"); }
                if (info.IsTemperatureSupported) { var r = pCustomColor->SetTemperature(info.Temperature); if (r != ADLX_RESULT.ADLX_OK) throw new ADLXException(r, "Failed to set Custom Color temperature"); }
            }
    
            public static unsafe IntPtr GetDisplayBlankingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));

                if (!ADLXUtils.TryQueryInterface(pDisplayServices, nameof(IADLXDisplayServices3), out var pServices3) || pServices3 == IntPtr.Zero)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display Blanking requires Display Services v3.");

                using var services = new ComPtr<IADLXDisplayServices3>((IADLXDisplayServices3*)pServices3);
                IADLXDisplayBlanking* pBlanking;
                var result = services.Get()->GetDisplayBlanking((IADLXDisplay*)pDisplay, &pBlanking);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pBlanking == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display Blanking not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Display Blanking interface");

                return (IntPtr)pBlanking;
            }
    
            public static unsafe (bool supported, bool blanked) GetDisplayBlankingState(IntPtr pBlanking)
            {
                if (pBlanking == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pBlanking));
    
                var blanking = (IADLXDisplayBlanking*)pBlanking;
                bool supported = false;
                var r1 = blanking->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query Display Blanking support");
    
                bool blanked = false;
                var r2 = blanking->IsCurrentBlanked(&blanked);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query current blanking state");
    
                return (supported, blanked);
            }
    
            public static unsafe void SetDisplayBlanked(IntPtr pBlanking, bool blank)
            {
                if (pBlanking == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pBlanking));
    
                var blanking = (IADLXDisplayBlanking*)pBlanking;
                var result = blank ? blanking->SetBlanked() : blanking->SetUnblanked();
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, blank ? "Failed to set display blanked" : "Failed to set display unblanked");
            }
    
            public static unsafe IntPtr GetVirtualSuperResolutionHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));

                var services = (IADLXDisplayServices*)pDisplayServices;
                IADLXDisplayVSR* pVsr;
                var result = services->GetVirtualSuperResolution((IADLXDisplay*)pDisplay, &pVsr);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pVsr == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Virtual Super Resolution not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Virtual Super Resolution interface");
    
                return (IntPtr)pVsr;
            }
    
            public static unsafe (bool supported, bool enabled) GetVirtualSuperResolutionState(IntPtr pVsr)
            {
                if (pVsr == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pVsr));
    
                var vsr = (IADLXDisplayVSR*)pVsr;
                bool supported = false;
                bool enabled = false;
    
                var r1 = vsr->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query VSR support");
    
                var r2 = vsr->IsEnabled(&enabled);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query VSR enabled");
    
                return (supported, enabled);
            }
    
            public static unsafe void SetVirtualSuperResolutionEnabled(IntPtr pVsr, bool enable)
            {
                if (pVsr == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pVsr));
    
                var vsr = (IADLXDisplayVSR*)pVsr;
                var result = vsr->SetEnabled(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set VSR state");
            }
    
            public static unsafe IntPtr GetIntegerScalingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));

                var services = (IADLXDisplayServices*)pDisplayServices;
                IADLXDisplayIntegerScaling* pIntegerScaling;
                var result = services->GetIntegerScaling((IADLXDisplay*)pDisplay, &pIntegerScaling);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pIntegerScaling == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Integer Scaling not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Integer Scaling interface");
    
                return (IntPtr)pIntegerScaling;
            }
    
            public static unsafe (bool supported, bool enabled) GetIntegerScalingState(IntPtr pIntegerScaling)
            {
                if (pIntegerScaling == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pIntegerScaling));
    
                var integerScaling = (IADLXDisplayIntegerScaling*)pIntegerScaling;
                bool supported = false;
                bool enabled = false;
    
                var r1 = integerScaling->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query Integer Scaling support");
    
                var r2 = integerScaling->IsEnabled(&enabled);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query Integer Scaling enabled");
    
                return (supported, enabled);
            }
    
            public static unsafe void SetIntegerScalingEnabled(IntPtr pIntegerScaling, bool enable)
            {
                if (pIntegerScaling == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pIntegerScaling));
    
                var integerScaling = (IADLXDisplayIntegerScaling*)pIntegerScaling;
                var result = integerScaling->SetEnabled(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set Integer Scaling state");
            }
    
            public static unsafe IntPtr GetHDCPHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));

                var services = (IADLXDisplayServices*)pDisplayServices;
                IADLXDisplayHDCP* pHdcp;
                var result = services->GetHDCP((IADLXDisplay*)pDisplay, &pHdcp);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pHdcp == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "HDCP not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get HDCP interface");
    
                return (IntPtr)pHdcp;
            }
    
            public static unsafe (bool supported, bool enabled) GetHDCPState(IntPtr pHdcp)
            {
                if (pHdcp == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pHdcp));
    
                var hdcp = (IADLXDisplayHDCP*)pHdcp;
                bool supported = false;
                bool enabled = false;
    
                var r1 = hdcp->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query HDCP support");
    
                var r2 = hdcp->IsEnabled(&enabled);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query HDCP enabled");
    
                return (supported, enabled);
            }
    
            public static unsafe void SetHDCPEnabled(IntPtr pHdcp, bool enable)
            {
                if (pHdcp == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pHdcp));
    
                var hdcp = (IADLXDisplayHDCP*)pHdcp;
                var result = hdcp->SetEnabled(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set HDCP state");
            }
    
    
            public static unsafe IntPtr GetVariBrightHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));

                var services = (IADLXDisplayServices*)pDisplayServices;
                IADLXDisplayVariBright* pVariBright;
                var result = services->GetVariBright((IADLXDisplay*)pDisplay, &pVariBright);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pVariBright == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "VariBright not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get VariBright interface");
    
                return (IntPtr)pVariBright;
            }
    
            public static unsafe (bool supported, bool enabled, VariBrightMode mode) GetVariBrightState(IntPtr pVariBright)
            {
                if (pVariBright == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pVariBright));
    
                var variBright = (IADLXDisplayVariBright*)pVariBright;
                bool supported = false;
                bool enabled = false;
                var r1 = variBright->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query VariBright support");
    
                var r2 = variBright->IsEnabled(&enabled);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query VariBright enabled");
    
                var mode = supported ? DetectVariBrightMode(variBright) : VariBrightMode.Unknown;
    
                return (supported, enabled, mode);
            }
    
            private static unsafe VariBrightMode DetectVariBrightMode(IADLXDisplayVariBright* pVariBright)
            {
                bool flag = false;
    
                if (pVariBright->IsCurrentMaximizeBrightness(&flag) == ADLX_RESULT.ADLX_OK && flag) return VariBrightMode.MaximizeBrightness;
                if (pVariBright->IsCurrentOptimizeBrightness(&flag) == ADLX_RESULT.ADLX_OK && flag) return VariBrightMode.OptimizeBrightness;
                if (pVariBright->IsCurrentBalanced(&flag) == ADLX_RESULT.ADLX_OK && flag) return VariBrightMode.Balanced;
                if (pVariBright->IsCurrentOptimizeBattery(&flag) == ADLX_RESULT.ADLX_OK && flag) return VariBrightMode.OptimizeBattery;
                if (pVariBright->IsCurrentMaximizeBattery(&flag) == ADLX_RESULT.ADLX_OK && flag) return VariBrightMode.MaximizeBattery;
    
                return VariBrightMode.Unknown;
            }
    
            public static unsafe void SetVariBright(IntPtr pVariBright, bool enable, VariBrightMode mode)
            {
                if (pVariBright == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pVariBright));
    
                var variBright = (IADLXDisplayVariBright*)pVariBright;
                var result = variBright->SetEnabled(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set VariBright enabled state");
    
                if (!enable)
                    return;
    
                ADLX_RESULT modeResult;
                switch (mode)
                {
                    case VariBrightMode.MaximizeBrightness:
                        modeResult = variBright->SetMaximizeBrightness();
                        break;
                    case VariBrightMode.OptimizeBrightness:
                        modeResult = variBright->SetOptimizeBrightness();
                        break;
                    case VariBrightMode.Balanced:
                        modeResult = variBright->SetBalanced();
                        break;
                    case VariBrightMode.OptimizeBattery:
                        modeResult = variBright->SetOptimizeBattery();
                        break;
                    case VariBrightMode.MaximizeBattery:
                        modeResult = variBright->SetMaximizeBattery();
                        break;
                    default:
                        modeResult = ADLX_RESULT.ADLX_OK;
                        break;
                }
    
                if (modeResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(modeResult, "Failed to set VariBright mode");
            }
    
            public static unsafe IntPtr GetColorDepthHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));

                var services = (IADLXDisplayServices*)pDisplayServices;
                IADLXDisplayColorDepth* pColorDepth;
                var result = services->GetColorDepth((IADLXDisplay*)pDisplay, &pColorDepth);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pColorDepth == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Color Depth not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Color Depth interface");
    
                return (IntPtr)pColorDepth;
            }
    
            public static unsafe (bool supported, ADLX_COLOR_DEPTH current) GetColorDepthState(IntPtr pColorDepth)
            {
                if (pColorDepth == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pColorDepth));
    
                var colorDepth = (IADLXDisplayColorDepth*)pColorDepth;
                bool supported = false;
                var r1 = colorDepth->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query Color Depth support");
    
                ADLX_COLOR_DEPTH depth = default;
                var r2 = colorDepth->GetValue(&depth);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query Color Depth value");
    
                return (supported, depth);
            }
    
            public static unsafe void SetColorDepth(IntPtr pColorDepth, ADLX_COLOR_DEPTH depth)
            {
                if (pColorDepth == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pColorDepth));
    
                var colorDepth = (IADLXDisplayColorDepth*)pColorDepth;
                var result = colorDepth->SetValue(depth);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set Color Depth");
            }
    
            public static unsafe IntPtr GetPixelFormatHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));

                var services = (IADLXDisplayServices*)pDisplayServices;
                IADLXDisplayPixelFormat* pPixelFormat;
                var result = services->GetPixelFormat((IADLXDisplay*)pDisplay, &pPixelFormat);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pPixelFormat == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Pixel Format not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Pixel Format interface");
    
                return (IntPtr)pPixelFormat;
            }
    
            public static unsafe (bool supported, ADLX_PIXEL_FORMAT current) GetPixelFormatState(IntPtr pPixelFormat)
            {
                if (pPixelFormat == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pPixelFormat));
    
                var pixelFormat = (IADLXDisplayPixelFormat*)pPixelFormat;
                bool supported = false;
                var r1 = pixelFormat->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query Pixel Format support");
    
                ADLX_PIXEL_FORMAT format = default;
                var r2 = pixelFormat->GetValue(&format);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query Pixel Format value");
    
                return (supported, format);
            }
    
            public static unsafe void SetPixelFormat(IntPtr pPixelFormat, ADLX_PIXEL_FORMAT format)
            {
                if (pPixelFormat == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pPixelFormat));
    
                var pixelFormat = (IADLXDisplayPixelFormat*)pPixelFormat;
                var result = pixelFormat->SetValue(format);
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to set Pixel Format");
            }
    
            public static unsafe IntPtr GetFreeSyncHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));

                var services = (IADLXDisplayServices*)pDisplayServices;
                IADLXDisplayFreeSync* pFS;
                var result = services->GetFreeSync((IADLXDisplay*)pDisplay, &pFS);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pFS == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "FreeSync not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get FreeSync interface");
    
                return (IntPtr)pFS;
            }
    
            public static unsafe IntPtr GetGPUScalingHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));

                var services = (IADLXDisplayServices*)pDisplayServices;
                IADLXDisplayGPUScaling* pScaling;
                var result = services->GetGPUScaling((IADLXDisplay*)pDisplay, &pScaling);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pScaling == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU scaling not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get GPU scaling interface");
    
                return (IntPtr)pScaling;
            }
    
            public static unsafe IntPtr GetScalingModeHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));

                var services = (IADLXDisplayServices*)pDisplayServices;
                IADLXDisplayScalingMode* pMode;
                var result = services->GetScalingMode((IADLXDisplay*)pDisplay, &pMode);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pMode == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Scaling mode not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get scaling mode interface");
    
                return (IntPtr)pMode;
            }
    
            public static unsafe (bool supported, bool enabled) GetFreeSyncState(IntPtr pFreeSync)
            {
                if (pFreeSync == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pFreeSync));
    
                var freeSync = (IADLXDisplayFreeSync*)pFreeSync;
                bool supported = false;
                bool enabled = false;
                var r1 = freeSync->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query FreeSync support");
    
                var r2 = freeSync->IsEnabled(&enabled);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query FreeSync enabled");
    
                return (supported, enabled);
            }
    
            public static unsafe void SetFreeSyncEnabled(IntPtr pFreeSync, bool enable)
            {
                if (pFreeSync == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pFreeSync));
    
                var freeSync = (IADLXDisplayFreeSync*)pFreeSync;
                var result = freeSync->SetEnabled(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                {
                    throw new ADLXException(result, "Failed to set FreeSync enabled state");
                }
            }
    
            public static unsafe (bool supported, bool enabled) GetGPUScalingState(IntPtr pGPUScaling)
            {
                if (pGPUScaling == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pGPUScaling));
    
                var scaling = (IADLXDisplayGPUScaling*)pGPUScaling;
                bool supported = false;
                bool enabled = false;
                var r1 = scaling->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query GPU scaling support");
    
                var r2 = scaling->IsEnabled(&enabled);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query GPU scaling enabled");
    
                return (supported, enabled);
            }
    
            public static unsafe void SetGPUScalingEnabled(IntPtr pGPUScaling, bool enable)
            {
                if (pGPUScaling == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pGPUScaling));
    
                var scaling = (IADLXDisplayGPUScaling*)pGPUScaling;
                var result = scaling->SetEnabled(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                {
                    throw new ADLXException(result, "Failed to set GPU scaling enabled state");
                }
            }
    
            public static unsafe (bool supported, ADLX_SCALE_MODE mode) GetScalingMode(IntPtr pScalingMode)
            {
                if (pScalingMode == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pScalingMode));
    
                var scaling = (IADLXDisplayScalingMode*)pScalingMode;
                bool supported = false;
                var r1 = scaling->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query scaling mode support");
    
                ADLX_SCALE_MODE mode;
                var r2 = scaling->GetMode(&mode);
                if (r2 != ADLX_RESULT.ADLX_OK)
                {
                    throw new ADLXException(r2, "Failed to get scaling mode");
                }
    
                return (supported, mode);
            }
    
            public static unsafe void SetScalingMode(IntPtr pScalingMode, ADLX_SCALE_MODE mode)
            {
                if (pScalingMode == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pScalingMode));
    
                var scaling = (IADLXDisplayScalingMode*)pScalingMode;
                var result = scaling->SetMode(mode);
                if (result != ADLX_RESULT.ADLX_OK)
                {
                    throw new ADLXException(result, "Failed to set scaling mode");
                }
            }
    
            public static unsafe IntPtr GetFreeSyncColorAccuracyHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));
    
                var services = (IADLXDisplayServices3*)pDisplayServices;
                IADLXDisplayFreeSyncColorAccuracy* pFSCA;
                var result = services->GetFreeSyncColorAccuracy((IADLXDisplay*)pDisplay, &pFSCA);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pFSCA == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "FreeSync Color Accuracy not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get FreeSync Color Accuracy interface");
    
                return (IntPtr)pFSCA;
            }
    
            public static unsafe (bool supported, bool enabled) GetFreeSyncColorAccuracyState(IntPtr pFSCA)
            {
                if (pFSCA == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pFSCA));
    
                var fsca = (IADLXDisplayFreeSyncColorAccuracy*)pFSCA;
                bool supported = false;
                bool enabled = false;
                var r1 = fsca->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query FreeSync Color Accuracy support");
    
                var r2 = fsca->IsEnabled(&enabled);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query FreeSync Color Accuracy enabled");
    
                return (supported, enabled);
            }
    
            public static unsafe void SetFreeSyncColorAccuracyEnabled(IntPtr pFSCA, bool enable)
            {
                if (pFSCA == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pFSCA));
    
                var fsca = (IADLXDisplayFreeSyncColorAccuracy*)pFSCA;
                var result = fsca->SetEnabled(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                {
                    throw new ADLXException(result, "Failed to set FreeSync Color Accuracy state");
                }
            }
    
            public static unsafe IntPtr GetDynamicRefreshRateControlHandle(IntPtr pDisplayServices, IntPtr pDisplay)
            {
                if (pDisplayServices == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplayServices));
                if (pDisplay == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDisplay));
    
                var services = (IADLXDisplayServices3*)pDisplayServices;
                IADLXDisplayDynamicRefreshRateControl* pDRR;
                var result = services->GetDynamicRefreshRateControl((IADLXDisplay*)pDisplay, &pDRR);
                if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDRR == null)
                    throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Dynamic Refresh Rate Control not supported by this ADLX system");
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, "Failed to get Dynamic Refresh Rate Control interface");
    
                return (IntPtr)pDRR;
            }
    
            public static unsafe (bool supported, bool enabled) GetDynamicRefreshRateControlState(IntPtr pDRR)
            {
                if (pDRR == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDRR));
    
                var drr = (IADLXDisplayDynamicRefreshRateControl*)pDRR;
                bool supported = false;
                bool enabled = false;
                var r1 = drr->IsSupported(&supported);
                if (r1 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r1, "Failed to query DRR support");
    
                var r2 = drr->IsEnabled(&enabled);
                if (r2 != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(r2, "Failed to query DRR enabled");
    
                return (supported, enabled);
            }
    
            public static unsafe void SetDynamicRefreshRateControlEnabled(IntPtr pDRR, bool enable)
            {
                if (pDRR == IntPtr.Zero)
                    throw new ArgumentNullException(nameof(pDRR));
    
                var drr = (IADLXDisplayDynamicRefreshRateControl*)pDRR;
                var result = drr->SetEnabled(enable ? (byte)1 : (byte)0);
                if (result != ADLX_RESULT.ADLX_OK)
                {
                    throw new ADLXException(result, "Failed to set DRR state");
                }
            }
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

        private IADLXDisplayServices* GetDisplayServicesForSettings()
        {
            var services = GetHighestDisplayServices();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported by this ADLX system");
            return services;
        }

        private IADLXDisplayServices3* GetDisplayServices3OrThrow()
        {
            if (_displayServices3.HasValue)
                return _displayServices3.Value.Get();

            throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services v3 not supported by this ADLX system");
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

    #region Display DTOs and listener handles (relocated from DisplaySettingsOps)
    /// <summary>
    /// Represents the collected information for a display.
    /// </summary>
    public readonly struct DisplayInfo
    {
        public string Name { get; init; }
        public int NativeResolutionWidth { get; init; }
        public int NativeResolutionHeight { get; init; }
        public double RefreshRate { get; init; }
        public uint ManufacturerID { get; init; }
        public uint PixelClock { get; init; }
        public ADLX_DISPLAY_TYPE Type { get; init; }
        public ADLX_DISPLAY_CONNECTOR_TYPE ConnectorType { get; init; }
        public ADLX_DISPLAY_SCAN_TYPE ScanType { get; init; }
        public ulong UniqueId { get; init; }
        public string Edid { get; init; }
        public int GpuUniqueId { get; init; }

        [JsonConstructor]
        public DisplayInfo(string name, int nativeResolutionWidth, int nativeResolutionHeight, double refreshRate, uint manufacturerID, uint pixelClock, ADLX_DISPLAY_TYPE type, ADLX_DISPLAY_CONNECTOR_TYPE connectorType, ADLX_DISPLAY_SCAN_TYPE scanType, ulong uniqueId, string edid, int gpuUniqueId)
        {
            Name = name;
            NativeResolutionWidth = nativeResolutionWidth;
            NativeResolutionHeight = nativeResolutionHeight;
            RefreshRate = refreshRate;
            ManufacturerID = manufacturerID;
            PixelClock = pixelClock;
            Type = type;
            ConnectorType = connectorType;
            ScanType = scanType;
            UniqueId = uniqueId;
            Edid = edid;
            GpuUniqueId = gpuUniqueId;
        }

        internal unsafe DisplayInfo(IADLXDisplay* pDisplay)
        {
            static void EnsureSuccess(ADLX_RESULT result, string message)
            {
                if (result != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(result, message);
            }

            sbyte* namePtr = null;
            EnsureSuccess(pDisplay->Name(&namePtr), "Failed to query display name");
            Name = ADLXUtils.MarshalString(&namePtr);

            sbyte* edidPtr = null;
            EnsureSuccess(pDisplay->EDID(&edidPtr), "Failed to query display EDID");
            Edid = ADLXUtils.MarshalString(&edidPtr);

            int w = 0, h = 0;
            EnsureSuccess(pDisplay->NativeResolution(&w, &h), "Failed to query display native resolution");
            NativeResolutionWidth = w;
            NativeResolutionHeight = h;

            double rr = 0;
            EnsureSuccess(pDisplay->RefreshRate(&rr), "Failed to query display refresh rate");
            RefreshRate = rr;

            uint mid = 0;
            EnsureSuccess(pDisplay->ManufacturerID(&mid), "Failed to query display manufacturer ID");
            ManufacturerID = mid;

            uint pc = 0;
            EnsureSuccess(pDisplay->PixelClock(&pc), "Failed to query display pixel clock");
            PixelClock = pc;

            ADLX_DISPLAY_TYPE dt = default;
            EnsureSuccess(pDisplay->DisplayType(&dt), "Failed to query display type");
            Type = dt;

            ADLX_DISPLAY_CONNECTOR_TYPE ct = default;
            EnsureSuccess(pDisplay->ConnectorType(&ct), "Failed to query display connector type");
            ConnectorType = ct;

            ADLX_DISPLAY_SCAN_TYPE st = default;
            EnsureSuccess(pDisplay->ScanType(&st), "Failed to query display scan type");
            ScanType = st;

            nuint uid = 0;
            EnsureSuccess(pDisplay->UniqueId(&uid), "Failed to query display unique id");
            UniqueId = (ulong)uid;

            IADLXGPU* pGpu = null;
            EnsureSuccess(pDisplay->GetGPU(&pGpu), "Failed to resolve display GPU");
            using var gpu = new ComPtr<IADLXGPU>(pGpu);
            int gpuId = 0;
            EnsureSuccess(gpu.Get()->UniqueId(&gpuId), "Failed to query GPU unique id for display");
            GpuUniqueId = gpuId;
        }
    }

    public readonly struct GammaInfo
    {
        public bool IsSupported { get; init; }
        public bool IsCurrentReGammaSRGB { get; init; }
        public bool IsCurrentReGammaBT709 { get; init; }
        public bool IsCurrentReGammaPQ { get; init; }
        public bool IsCurrentReGammaPQ2084 { get; init; }
        public bool IsCurrentReGamma36 { get; init; }
        public bool HasRegammaCoefficient { get; init; }
        public ADLX_RegammaCoeff RegammaCoefficient { get; init; }
        public bool HasReGammaRamp { get; init; }
        public ADLX_GammaRamp ReGammaRamp { get; init; }
        public bool HasDeGammaRamp { get; init; }
        public ADLX_GammaRamp DeGammaRamp { get; init; }

        [JsonConstructor]
        public GammaInfo(bool isSupported)
        {
            IsSupported = isSupported;
            IsCurrentReGammaSRGB = false;
            IsCurrentReGammaBT709 = false;
            IsCurrentReGammaPQ = false;
            IsCurrentReGammaPQ2084 = false;
            IsCurrentReGamma36 = false;
            HasRegammaCoefficient = false;
            RegammaCoefficient = default;
            HasReGammaRamp = false;
            ReGammaRamp = default;
            HasDeGammaRamp = false;
            DeGammaRamp = default;
        }

        internal unsafe GammaInfo(IADLXDisplayGamma* pGamma)
        {
            if (pGamma == null) throw new ArgumentNullException(nameof(pGamma));

            bool supported = false;
            pGamma->IsSupportedReGammaSRGB(&supported);
            bool supportedBt709 = false;
            pGamma->IsSupportedReGammaBT709(&supportedBt709);
            bool supportedPq = false;
            pGamma->IsSupportedReGammaPQ(&supportedPq);
            bool supportedPq2084 = false;
            pGamma->IsSupportedReGammaPQ2084Interim(&supportedPq2084);
            bool supported36 = false;
            pGamma->IsSupportedReGamma36(&supported36);

            IsSupported = supported || supportedBt709 || supportedPq || supportedPq2084 || supported36;

            bool currentSRGB = false, currentBT709 = false, currentPQ = false, currentPQ2084 = false, current36 = false;
            pGamma->IsCurrentReGammaSRGB(&currentSRGB);
            pGamma->IsCurrentReGammaBT709(&currentBT709);
            pGamma->IsCurrentReGammaPQ(&currentPQ);
            pGamma->IsCurrentReGammaPQ2084Interim(&currentPQ2084);
            pGamma->IsCurrentReGamma36(&current36);
            IsCurrentReGammaSRGB = currentSRGB;
            IsCurrentReGammaBT709 = currentBT709;
            IsCurrentReGammaPQ = currentPQ;
            IsCurrentReGammaPQ2084 = currentPQ2084;
            IsCurrentReGamma36 = current36;

            bool hasCoeff = false;
            pGamma->IsCurrentRegammaCoefficient(&hasCoeff);
            HasRegammaCoefficient = hasCoeff;
            RegammaCoefficient = default;
            if (hasCoeff)
            {
                ADLX_RegammaCoeff coeff = default;
                if (pGamma->GetGammaCoefficient(&coeff) == ADLX_RESULT.ADLX_OK)
                {
                    RegammaCoefficient = coeff;
                }
            }

            bool hasReRamp = false, hasDeRamp = false;
            pGamma->IsCurrentReGammaRamp(&hasReRamp);
            pGamma->IsCurrentDeGammaRamp(&hasDeRamp);
            HasReGammaRamp = hasReRamp;
            HasDeGammaRamp = hasDeRamp;
            ReGammaRamp = default;
            DeGammaRamp = default;

            if (hasReRamp)
            {
                ADLX_GammaRamp ramp = default;
                if (pGamma->GetGammaRamp(&ramp) == ADLX_RESULT.ADLX_OK)
                {
                    ReGammaRamp = ramp;
                }
            }

            if (hasDeRamp)
            {
                ADLX_GammaRamp ramp = default;
                if (pGamma->GetGammaRamp(&ramp) == ADLX_RESULT.ADLX_OK)
                {
                    DeGammaRamp = ramp;
                }
            }
        }
    }

    public readonly struct GamutInfo
    {
        public bool IsWhitePointSupported { get; init; }
        public bool IsGamutSupported { get; init; }
        public bool IsCurrent5000K { get; init; }
        public bool IsCurrent6500K { get; init; }
        public bool IsCurrent7500K { get; init; }
        public bool IsCurrent9300K { get; init; }
        public bool IsCurrentCustomWhitePoint { get; init; }
        public bool IsCurrent709 { get; init; }
        public bool IsCurrent601 { get; init; }
        public bool IsCurrentAdobe { get; init; }
        public bool IsCurrentCieRgb { get; init; }
        public bool IsCurrent2020 { get; init; }
        public bool IsCurrentCustomColorSpace { get; init; }
        public ADLX_GamutColorSpace CurrentGamutSpace { get; init; }
        public ADLX_Point WhitePoint { get; init; }
        public bool HasWhitePoint { get; init; }

        [JsonConstructor]
        public GamutInfo(bool isWhitePointSupported, bool isGamutSupported)
        {
            IsWhitePointSupported = isWhitePointSupported;
            IsGamutSupported = isGamutSupported;
            IsCurrent5000K = false;
            IsCurrent6500K = false;
            IsCurrent7500K = false;
            IsCurrent9300K = false;
            IsCurrentCustomWhitePoint = false;
            IsCurrent709 = false;
            IsCurrent601 = false;
            IsCurrentAdobe = false;
            IsCurrentCieRgb = false;
            IsCurrent2020 = false;
            IsCurrentCustomColorSpace = false;
            CurrentGamutSpace = default;
            WhitePoint = default;
            HasWhitePoint = false;
        }

        internal unsafe GamutInfo(IADLXDisplayGamut* pGamut)
        {
            if (pGamut == null) throw new ArgumentNullException(nameof(pGamut));

            bool wp = false, gamut = false;
            pGamut->IsSupportedCustomWhitePoint(&wp);
            pGamut->IsSupportedCustomColorSpace(&gamut);
            IsWhitePointSupported = wp;
            IsGamutSupported = gamut;

            bool cur5000 = false, cur6500 = false, cur7500 = false, cur9300 = false, curCustomWhite = false;
            pGamut->IsCurrent5000kWhitePoint(&cur5000);
            pGamut->IsCurrent6500kWhitePoint(&cur6500);
            pGamut->IsCurrent7500kWhitePoint(&cur7500);
            pGamut->IsCurrent9300kWhitePoint(&cur9300);
            pGamut->IsCurrentCustomWhitePoint(&curCustomWhite);
            IsCurrent5000K = cur5000;
            IsCurrent6500K = cur6500;
            IsCurrent7500K = cur7500;
            IsCurrent9300K = cur9300;
            IsCurrentCustomWhitePoint = curCustomWhite;

            bool cur709 = false, cur601 = false, curAdobe = false, curCIERgb = false, cur2020 = false, curCustomSpace = false;
            pGamut->IsCurrentCCIR709ColorSpace(&cur709);
            pGamut->IsCurrentCCIR601ColorSpace(&cur601);
            pGamut->IsCurrentAdobeRgbColorSpace(&curAdobe);
            pGamut->IsCurrentCIERgbColorSpace(&curCIERgb);
            pGamut->IsCurrentCCIR2020ColorSpace(&cur2020);
            pGamut->IsCurrentCustomColorSpace(&curCustomSpace);
            IsCurrent709 = cur709;
            IsCurrent601 = cur601;
            IsCurrentAdobe = curAdobe;
            IsCurrentCieRgb = curCIERgb;
            IsCurrent2020 = cur2020;
            IsCurrentCustomColorSpace = curCustomSpace;

            ADLX_GamutColorSpace currentSpace = default;
            pGamut->GetGamutColorSpace(&currentSpace);
            CurrentGamutSpace = currentSpace;

            ADLX_Point whitePoint = default;
            HasWhitePoint = pGamut->GetWhitePoint(&whitePoint) == ADLX_RESULT.ADLX_OK;
            WhitePoint = whitePoint;
        }
    }

    public readonly struct ThreeDLUTInfo
    {
        public bool IsSceSupported { get; init; }
        public bool IsSceVividGamingSupported { get; init; }
        public bool IsSceDynamicContrastSupported { get; init; }
        public bool IsUser3DLutSupported { get; init; }
        public bool IsCurrentSceDisabled { get; init; }
        public bool IsCurrentSceVividGaming { get; init; }
        public bool HasDynamicContrast { get; init; }
        public int CurrentDynamicContrast { get; init; }
        public ADLX_IntRange DynamicContrastRange { get; init; }

        [JsonConstructor]
        public ThreeDLUTInfo(bool isSceSupported, bool isSceVividGamingSupported, bool isSceDynamicContrastSupported, bool isUser3DLutSupported)
        {
            IsSceSupported = isSceSupported;
            IsSceVividGamingSupported = isSceVividGamingSupported;
            IsSceDynamicContrastSupported = isSceDynamicContrastSupported;
            IsUser3DLutSupported = isUser3DLutSupported;
            IsCurrentSceDisabled = false;
            IsCurrentSceVividGaming = false;
            HasDynamicContrast = false;
            CurrentDynamicContrast = 0;
            DynamicContrastRange = default;
        }

        internal unsafe ThreeDLUTInfo(IADLXDisplay3DLUT* p3dLut)
        {
            bool sce = false, vivid = false, dynamic = false, user = false;
            p3dLut->IsSupportedSCE(&sce);
            p3dLut->IsSupportedSCEVividGaming(&vivid);
            p3dLut->IsSupportedSCEDynamicContrast(&dynamic);
            p3dLut->IsSupportedUser3DLUT(&user);
            IsSceSupported = sce;
            IsSceVividGamingSupported = vivid;
            IsSceDynamicContrastSupported = dynamic;
            IsUser3DLutSupported = user;

            bool curDisabled = false, curVivid = false;
            p3dLut->IsCurrentSCEDisabled(&curDisabled);
            p3dLut->IsCurrentSCEVividGaming(&curVivid);
            IsCurrentSceDisabled = curDisabled;
            IsCurrentSceVividGaming = curVivid;

            ADLX_IntRange range = default;
            int currentContrast = 0;
            HasDynamicContrast = false;
            DynamicContrastRange = default;
            CurrentDynamicContrast = 0;
            if (IsSceDynamicContrastSupported && p3dLut->GetSCEDynamicContrastRange(&range) == ADLX_RESULT.ADLX_OK)
            {
                if (p3dLut->GetSCEDynamicContrast(&currentContrast) == ADLX_RESULT.ADLX_OK)
                {
                    HasDynamicContrast = true;
                    DynamicContrastRange = range;
                    CurrentDynamicContrast = currentContrast;
                }
            }
        }
    }

    public readonly struct ConnectivityExperienceInfo
    {
        public bool IsHdmiQualityDetectionSupported { get; init; }
        public bool IsHdmiQualityDetectionEnabled { get; init; }
        public bool IsDpLinkRateSupported { get; init; }
        public ADLX_DP_LINK_RATE DpLinkRate { get; init; }
        public bool IsRelativePreEmphasisSupported { get; init; }
        public int RelativePreEmphasis { get; init; }
        public bool IsRelativeVoltageSwingSupported { get; init; }
        public int RelativeVoltageSwing { get; init; }

        [JsonConstructor]
        public ConnectivityExperienceInfo(bool isHdmiQualityDetectionSupported, bool isHdmiQualityDetectionEnabled, bool isDpLinkRateSupported, ADLX_DP_LINK_RATE dpLinkRate, bool isRelativePreEmphasisSupported, int relativePreEmphasis, bool isRelativeVoltageSwingSupported, int relativeVoltageSwing)
        {
            IsHdmiQualityDetectionSupported = isHdmiQualityDetectionSupported;
            IsHdmiQualityDetectionEnabled = isHdmiQualityDetectionEnabled;
            IsDpLinkRateSupported = isDpLinkRateSupported;
            DpLinkRate = dpLinkRate;
            IsRelativePreEmphasisSupported = isRelativePreEmphasisSupported;
            RelativePreEmphasis = relativePreEmphasis;
            IsRelativeVoltageSwingSupported = isRelativeVoltageSwingSupported;
            RelativeVoltageSwing = relativeVoltageSwing;
        }

        internal unsafe ConnectivityExperienceInfo(IADLXDisplayConnectivityExperience* pConn)
        {
            bool supported = false, enabled = false;
            pConn->IsSupportedHDMIQualityDetection(&supported);
            IsHdmiQualityDetectionSupported = supported;
            pConn->IsEnabledHDMIQualityDetection(&enabled);
            IsHdmiQualityDetectionEnabled = enabled;

            supported = false;
            pConn->IsSupportedDPLink(&supported);
            IsDpLinkRateSupported = supported;
            ADLX_DP_LINK_RATE rate = default;
            if (IsDpLinkRateSupported && pConn->GetDPLinkRate(&rate) == ADLX_RESULT.ADLX_OK)
            {
                DpLinkRate = rate;
            }
            else
            {
                DpLinkRate = default;
            }

            int relPre = 0;
            IsRelativePreEmphasisSupported = pConn->GetRelativePreEmphasis(&relPre) == ADLX_RESULT.ADLX_OK;
            RelativePreEmphasis = relPre;

            int relVolt = 0;
            IsRelativeVoltageSwingSupported = pConn->GetRelativeVoltageSwing(&relVolt) == ADLX_RESULT.ADLX_OK;
            RelativeVoltageSwing = relVolt;
        }
    }

    public readonly struct CustomResolutionInfo
    {
        public bool IsSupported { get; init; }
        public IReadOnlyList<DisplayResolutionInfo> Resolutions { get; init; }

        [JsonConstructor]
        public CustomResolutionInfo(bool isSupported, IReadOnlyList<DisplayResolutionInfo> resolutions)
        {
            IsSupported = isSupported;
            Resolutions = resolutions;
        }

        internal unsafe CustomResolutionInfo(IADLXDisplayCustomResolution* pCustomRes)
        {
            bool supported = false;
            pCustomRes->IsSupported(&supported);
            IsSupported = supported;

            var resolutions = new List<DisplayResolutionInfo>();
            if (IsSupported)
            {
                IADLXDisplayResolutionList* pResList;
                pCustomRes->GetResolutionList(&pResList);
                using var resList = new ComPtr<IADLXDisplayResolutionList>(pResList);
                for (uint i = 0; i < resList.Get()->Size(); i++)
                {
                    IADLXDisplayResolution* pRes;
                    resList.Get()->At(i, &pRes);
                    using var res = new ComPtr<IADLXDisplayResolution>(pRes);
                    resolutions.Add(new DisplayResolutionInfo(res.Get()));
                }
            }
            Resolutions = resolutions;
        }
    }

    public readonly struct DisplayResolutionInfo
    {
        public int ResWidth { get; init; }
        public int ResHeight { get; init; }
        public int RefreshRate { get; init; }

        [JsonConstructor]
        public DisplayResolutionInfo(int resWidth, int resHeight, int refreshRate)
        {
            ResWidth = resWidth;
            ResHeight = resHeight;
            RefreshRate = refreshRate;
        }

        internal unsafe DisplayResolutionInfo(IADLXDisplayResolution* pRes)
        {
            ADLX_CustomResolution res = default;
            pRes->GetValue(&res);
            ResWidth = res.resWidth;
            ResHeight = res.resHeight;
            RefreshRate = res.refreshRate;
        }
    }

    public readonly struct CustomColorInfo
    {
        public bool IsSupported { get; init; }
        public bool IsHueSupported { get; init; }
        public int Hue { get; init; }
        public bool IsSaturationSupported { get; init; }
        public int Saturation { get; init; }
        public bool IsBrightnessSupported { get; init; }
        public int Brightness { get; init; }
        public bool IsContrastSupported { get; init; }
        public int Contrast { get; init; }
        public bool IsTemperatureSupported { get; init; }
        public int Temperature { get; init; }

        [JsonConstructor]
        public CustomColorInfo(bool isSupported, bool isHueSupported, int hue, bool isSaturationSupported, int saturation, bool isBrightnessSupported, int brightness, bool isContrastSupported, int contrast, bool isTemperatureSupported, int temperature)
        {
            IsSupported = isSupported; IsHueSupported = isHueSupported; Hue = hue;
            IsSaturationSupported = isSaturationSupported; Saturation = saturation;
            IsBrightnessSupported = isBrightnessSupported; Brightness = brightness;
            IsContrastSupported = isContrastSupported; Contrast = contrast;
            IsTemperatureSupported = isTemperatureSupported; Temperature = temperature;
        }

        internal unsafe CustomColorInfo(IADLXDisplayCustomColor* pCustomColor)
        {
            bool supported = false;
            pCustomColor->IsHueSupported(&supported); IsHueSupported = supported;
            int hue = 0; pCustomColor->GetHue(&hue); Hue = hue;

            supported = false;
            pCustomColor->IsSaturationSupported(&supported); IsSaturationSupported = supported;
            int sat = 0; pCustomColor->GetSaturation(&sat); Saturation = sat;

            supported = false;
            pCustomColor->IsBrightnessSupported(&supported); IsBrightnessSupported = supported;
            int bright = 0; pCustomColor->GetBrightness(&bright); Brightness = bright;

            supported = false;
            pCustomColor->IsContrastSupported(&supported); IsContrastSupported = supported;
            int cont = 0; pCustomColor->GetContrast(&cont); Contrast = cont;

            supported = false;
            pCustomColor->IsTemperatureSupported(&supported); IsTemperatureSupported = supported;
            int temp = 0; pCustomColor->GetTemperature(&temp); Temperature = temp;

            IsSupported = IsHueSupported || IsSaturationSupported || IsBrightnessSupported || IsContrastSupported || IsTemperatureSupported;
        }
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLXDisplayListChangedListener backed by a managed delegate.
    /// </summary>
    public sealed unsafe class DisplayListListenerHandle : SafeHandle
    {
        public delegate bool OnDisplayListChanged(IADLXDisplayList* pNewDisplays);
        private static readonly ConcurrentDictionary<IntPtr, OnDisplayListChanged> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnDisplayListChangedThunk;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private DisplayListListenerHandle(OnDisplayListChanged cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static DisplayListListenerHandle Create(OnDisplayListChanged cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new DisplayListListenerHandle(cb);
        }

        public IADLXDisplayListChangedListener* GetListener() => (IADLXDisplayListChangedListener*)handle;

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnDisplayListChangedThunk(IntPtr pThis, IntPtr pNewDisplays)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb((IADLXDisplayList*)pNewDisplays) ? (byte)1 : (byte)0;
            }
            return 0;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }

    public sealed unsafe class DisplayGamutListenerHandle : SafeHandle
    {
        public delegate bool OnDisplayGamutChanged(IntPtr pEvent);
        private static readonly ConcurrentDictionary<IntPtr, OnDisplayGamutChanged> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnDisplayGamutChangedThunk;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private DisplayGamutListenerHandle(OnDisplayGamutChanged cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static DisplayGamutListenerHandle Create(OnDisplayGamutChanged cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new DisplayGamutListenerHandle(cb);
        }

        public IADLXDisplayGamutChangedListener* GetListener() => (IADLXDisplayGamutChangedListener*)handle;

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnDisplayGamutChangedThunk(IntPtr pThis, IntPtr pEvent)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb(pEvent) ? (byte)1 : (byte)0;
            }
            return 0;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }

    public sealed unsafe class DisplayGammaListenerHandle : SafeHandle
    {
        public delegate bool OnDisplayGammaChanged(IntPtr pEvent);
        private static readonly ConcurrentDictionary<IntPtr, OnDisplayGammaChanged> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnDisplayGammaChangedThunk;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private DisplayGammaListenerHandle(OnDisplayGammaChanged cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static DisplayGammaListenerHandle Create(OnDisplayGammaChanged cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new DisplayGammaListenerHandle(cb);
        }

        public IADLXDisplayGammaChangedListener* GetListener() => (IADLXDisplayGammaChangedListener*)handle;

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnDisplayGammaChangedThunk(IntPtr pThis, IntPtr pEvent)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb(pEvent) ? (byte)1 : (byte)0;
            }
            return 0;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }

    public sealed unsafe class Display3DLutListenerHandle : SafeHandle
    {
        public delegate bool OnDisplay3DLutChanged(IntPtr pEvent);
        private static readonly ConcurrentDictionary<IntPtr, OnDisplay3DLutChanged> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnDisplay3DLutChangedThunk;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private Display3DLutListenerHandle(OnDisplay3DLutChanged cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static Display3DLutListenerHandle Create(OnDisplay3DLutChanged cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new Display3DLutListenerHandle(cb);
        }

        public IADLXDisplay3DLUTChangedListener* GetListener() => (IADLXDisplay3DLUTChangedListener*)handle;

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnDisplay3DLutChangedThunk(IntPtr pThis, IntPtr pEvent)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb(pEvent) ? (byte)1 : (byte)0;
            }
            return 0;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }

    /// <summary>
    /// Safe handle for an unmanaged IADLXDisplaySettingsChangedListener backed by a managed delegate.
    /// </summary>
    public sealed unsafe class DisplaySettingsListenerHandle : SafeHandle
    {
        public delegate bool OnDisplaySettingsChanged(IntPtr pEvent);
        private static readonly ConcurrentDictionary<IntPtr, OnDisplaySettingsChanged> _map = new();
        private static readonly IntPtr _thunkPtr = (IntPtr)(delegate* unmanaged[Stdcall]<IntPtr, IntPtr, byte>)&OnDisplaySettingsChangedThunk;
        private readonly GCHandle _gcHandle;
        private readonly IntPtr _vtbl;

        private DisplaySettingsListenerHandle(OnDisplaySettingsChanged cb) : base(IntPtr.Zero, true)
        {
            _gcHandle = GCHandle.Alloc(cb);
            _vtbl = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(_vtbl, _thunkPtr);

            var inst = Marshal.AllocHGlobal(IntPtr.Size);
            Marshal.WriteIntPtr(inst, _vtbl);
            handle = inst;
            _map[inst] = cb;
        }

        public static DisplaySettingsListenerHandle Create(OnDisplaySettingsChanged cb)
        {
            if (cb == null) throw new ArgumentNullException(nameof(cb));
            return new DisplaySettingsListenerHandle(cb);
        }

        public IADLXDisplaySettingsChangedListener* GetListener() => (IADLXDisplaySettingsChangedListener*)handle;

        protected override bool ReleaseHandle()
        {
            _map.TryRemove(handle, out _);
            if (_gcHandle.IsAllocated) _gcHandle.Free();
            if (_vtbl != IntPtr.Zero) Marshal.FreeHGlobal(_vtbl);
            if (handle != IntPtr.Zero) Marshal.FreeHGlobal(handle);
            return true;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvStdcall) })]
        private static byte OnDisplaySettingsChangedThunk(IntPtr pThis, IntPtr pEvent)
        {
            if (_map.TryGetValue(pThis, out var cb))
            {
                return cb(pEvent) ? (byte)1 : (byte)0;
            }
            return 0;
        }

        public override bool IsInvalid => handle == IntPtr.Zero;
    }
    #endregion
}
