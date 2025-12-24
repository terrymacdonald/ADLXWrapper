using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Flattened display façade with identity metadata, support-guarded feature accessors, and native list exposure.
    /// </summary>
    public sealed unsafe class ADLXDisplay : IDisposable
    {
        private ComPtr<IADLXDisplayServices> _displayServices;
        private ComPtr<IADLXDisplay> _display;
        private ComPtr<IADLXDesktopServices>? _desktopServices;
        private readonly DisplayInfo _identity;
        private bool _disposed;

        /// <summary>
        /// Creates a managed display facade from native display and display services (optional desktop services for ownership queries).
        /// </summary>
        /// <param name="pDisplayServices">Native display services pointer.</param>
        /// <param name="pDisplay">Native display pointer.</param>
        /// <param name="pDesktopServices">Optional desktop services pointer used to resolve owning desktop.</param>
        public ADLXDisplay(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay, IADLXDesktopServices* pDesktopServices = null)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            _displayServices = new ComPtr<IADLXDisplayServices>(pDisplayServices);
            _display = new ComPtr<IADLXDisplay>(pDisplay);
            if (pDesktopServices != null)
            {
                ADLXUtils.AddRefInterface((IntPtr)pDesktopServices);
                _desktopServices = new ComPtr<IADLXDesktopServices>(pDesktopServices);
            }
            _identity = new DisplayInfo(pDisplay);
        }

        public string Name { get { ThrowIfDisposed(); return _identity.Name; } }
        public string Edid { get { ThrowIfDisposed(); return _identity.Edid; } }
        public int Width { get { ThrowIfDisposed(); return _identity.Width; } }
        public int Height { get { ThrowIfDisposed(); return _identity.Height; } }
        public double RefreshRate { get { ThrowIfDisposed(); return _identity.RefreshRate; } }
        public uint ManufacturerId { get { ThrowIfDisposed(); return _identity.ManufacturerID; } }
        public uint PixelClock { get { ThrowIfDisposed(); return _identity.PixelClock; } }
        public ADLX_DISPLAY_TYPE Type { get { ThrowIfDisposed(); return _identity.Type; } }
        public ADLX_DISPLAY_CONNECTOR_TYPE ConnectorType { get { ThrowIfDisposed(); return _identity.ConnectorType; } }
        public ADLX_DISPLAY_SCAN_TYPE ScanType { get { ThrowIfDisposed(); return _identity.ScanType; } }
        public ulong UniqueId { get { ThrowIfDisposed(); return _identity.UniqueId; } }
        public int GpuUniqueId { get { ThrowIfDisposed(); return _identity.GpuUniqueId; } }

        /// <summary>
        /// Query FreeSync support/enabled state.
        /// </summary>
        public (bool supported, bool enabled) GetFreeSyncState()
        {
            ThrowIfDisposed();
            return CreateDisplayServicesHelper().GetFreeSyncState(_display.Get());
        }

        /// <summary>
        /// Enables or disables FreeSync if supported.
        /// </summary>
        /// <param name="enable">True to enable FreeSync.</param>
        public void SetFreeSync(bool enable)
        {
            ThrowIfDisposed();
            CreateDisplayServicesHelper().SetFreeSyncEnabled(_display.Get(), enable);
        }

        /// <summary>
        /// Query GPU scaling support/enabled state.
        /// </summary>
        public (bool supported, bool enabled) GetGpuScalingState()
        {
            ThrowIfDisposed();
            return CreateDisplayServicesHelper().GetGPUScalingState(_display.Get());
        }

        /// <summary>
        /// Enables or disables GPU scaling if supported.
        /// </summary>
        /// <param name="enable">True to enable GPU scaling.</param>
        public void SetGpuScaling(bool enable)
        {
            ThrowIfDisposed();
            CreateDisplayServicesHelper().SetGPUScalingEnabled(_display.Get(), enable);
        }

        /// <summary>
        /// Query scaling mode support/value.
        /// </summary>
        public (bool supported, ADLX_SCALE_MODE mode) GetScalingMode()
        {
            ThrowIfDisposed();
            return CreateDisplayServicesHelper().GetScalingMode(_display.Get());
        }

        public DisplayInfo GetDisplayInfo()
        {
            ThrowIfDisposed();
            return _identity;
        }

        /// <summary>
        /// Sets the display scaling mode if supported.
        /// </summary>
        /// <param name="mode">Scaling mode to apply.</param>
        public void SetScalingMode(ADLX_SCALE_MODE mode)
        {
            ThrowIfDisposed();
            CreateDisplayServicesHelper().SetScalingMode(_display.Get(), mode);
        }

        /// <summary>
        /// Enumerate custom resolutions as managed DTOs.
        /// </summary>
        public IEnumerable<DisplayResolutionInfo> EnumerateCustomResolutions()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.EnumerateCustomResolutions(_display.Get());
        }

        /// <summary>
        /// Native custom resolution list accessor. Caller must dispose the returned ComPtr and any retained items.
        /// </summary>
        public ComPtr<IADLXDisplayResolutionList> GetCustomResolutionListNative()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return new ComPtr<IADLXDisplayResolutionList>(helper.GetCustomResolutionListNative(_display.Get()));
        }

        /// <summary>
        /// Virtual Super Resolution support/enabled state.
        /// </summary>
        public (bool supported, bool enabled) GetVirtualSuperResolutionState()
        {
            ThrowIfDisposed();
            return CreateDisplayServicesHelper().GetVirtualSuperResolutionState(_display.Get());
        }

        public void SetVirtualSuperResolution(bool enable)
        {
            ThrowIfDisposed();
            CreateDisplayServicesHelper().SetVirtualSuperResolutionEnabled(_display.Get(), enable);
        }

        /// <summary>
        /// Integer scaling support/enabled state.
        /// </summary>
        public (bool supported, bool enabled) GetIntegerScalingState()
        {
            ThrowIfDisposed();
            return CreateDisplayServicesHelper().GetIntegerScalingState(_display.Get());
        }

        public void SetIntegerScaling(bool enable)
        {
            ThrowIfDisposed();
            CreateDisplayServicesHelper().SetIntegerScalingEnabled(_display.Get(), enable);
        }

        /// <summary>
        /// HDCP support/enabled state.
        /// </summary>
        public (bool supported, bool enabled) GetHdcpState()
        {
            ThrowIfDisposed();
            return CreateDisplayServicesHelper().GetHDCPState(_display.Get());
        }

        public void SetHdcp(bool enable)
        {
            ThrowIfDisposed();
            CreateDisplayServicesHelper().SetHDCPEnabled(_display.Get(), enable);
        }

        /// <summary>
        /// Vari-Bright support/enabled/mode state.
        /// </summary>
        public (bool supported, bool enabled, VariBrightMode mode) GetVariBrightState()
        {
            ThrowIfDisposed();
            return CreateDisplayServicesHelper().GetVariBrightState(_display.Get());
        }

        public void SetVariBright(bool enable, VariBrightMode mode)
        {
            ThrowIfDisposed();
            CreateDisplayServicesHelper().SetVariBright(_display.Get(), enable, mode);
        }

        /// <summary>
        /// Color depth support/value.
        /// </summary>
        public (bool supported, ADLX_COLOR_DEPTH current) GetColorDepthState()
        {
            ThrowIfDisposed();
            return CreateDisplayServicesHelper().GetColorDepthState(_display.Get());
        }

        public void SetColorDepth(ADLX_COLOR_DEPTH depth)
        {
            ThrowIfDisposed();
            CreateDisplayServicesHelper().SetColorDepth(_display.Get(), depth);
        }

        /// <summary>
        /// Pixel format support/value.
        /// </summary>
        public (bool supported, ADLX_PIXEL_FORMAT current) GetPixelFormatState()
        {
            ThrowIfDisposed();
            return CreateDisplayServicesHelper().GetPixelFormatState(_display.Get());
        }

        public void SetPixelFormat(ADLX_PIXEL_FORMAT format)
        {
            ThrowIfDisposed();
            CreateDisplayServicesHelper().SetPixelFormat(_display.Get(), format);
        }

        /// <summary>
        /// Custom color info and application.
        /// </summary>
        public CustomColorInfo GetCustomColor()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.GetCustomColor(_display.Get());
        }

        public void ApplyCustomColor(CustomColorInfo info)
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            helper.ApplyCustomColor(_display.Get(), info);
        }

        /// <summary>
        /// Gamma info and reapply.
        /// </summary>
        public GammaInfo GetGamma()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.GetGamma(_display.Get());
        }

        public void ReapplyGamma()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            helper.ReapplyGamma(_display.Get());
        }

        /// <summary>
        /// Gamut info and reapply.
        /// </summary>
        public GamutInfo GetGamut()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.GetGamut(_display.Get());
        }

        public void ReapplyGamut()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            helper.ReapplyGamut(_display.Get());
        }

        /// <summary>
        /// 3DLUT info and reapply.
        /// </summary>
        public ThreeDLUTInfo GetThreeDLut()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.GetThreeDLut(_display.Get());
        }

        public void ReapplyThreeDLut()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            helper.ReapplyThreeDLut(_display.Get());
        }

        /// <summary>
        /// Display connectivity experience info and application.
        /// </summary>
        public ConnectivityExperienceInfo GetConnectivityExperience()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.GetConnectivityExperience(_display.Get());
        }

        public void ApplyConnectivityExperience(ConnectivityExperienceInfo info)
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            helper.ApplyConnectivityExperience(_display.Get(), info);
        }

        /// <summary>
        /// Display blanking support/state.
        /// </summary>
        public (bool supported, bool blanked) GetDisplayBlankingState()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.GetDisplayBlankingState(_display.Get());
        }

        public void SetDisplayBlanked(bool enabled)
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            helper.SetDisplayBlanked(_display.Get(), enabled);
        }

        /// <summary>
        /// FreeSync Color Accuracy support/enabled state.
        /// </summary>
        public (bool supported, bool enabled) GetFreeSyncColorAccuracyState()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.GetFreeSyncColorAccuracyState(_display.Get());
        }

        public void SetFreeSyncColorAccuracy(bool enable)
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            helper.SetFreeSyncColorAccuracyEnabled(_display.Get(), enable);
        }

        /// <summary>
        /// Dynamic Refresh Rate Control support/enabled state.
        /// </summary>
        public (bool supported, bool enabled) GetDynamicRefreshRateControlState()
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.GetDynamicRefreshRateControlState(_display.Get());
        }

        public void SetDynamicRefreshRateControl(bool enable)
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            helper.SetDynamicRefreshRateControlEnabled(_display.Get(), enable);
        }

        /// <summary>
        /// Subscribe to display settings change events. Returns a handle the caller must dispose (or call the remove helper) to unsubscribe.
        /// </summary>
        public DisplaySettingsListenerHandle AddDisplaySettingsEventListener(DisplaySettingsListenerHandle.OnDisplaySettingsChanged callback)
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.AddDisplaySettingsEventListener(callback);
        }

        public void RemoveDisplaySettingsEventListener(DisplaySettingsListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid) return;
            using var helper = CreateDisplayServicesHelper();
            helper.RemoveDisplaySettingsEventListener(handle, disposeHandle);
        }

        /// <summary>
        /// Subscribe to gamma change events.
        /// </summary>
        public DisplayGammaListenerHandle AddDisplayGammaEventListener(DisplayGammaListenerHandle.OnDisplayGammaChanged callback)
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.AddDisplayGammaEventListener(callback);
        }

        public void RemoveDisplayGammaEventListener(DisplayGammaListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid) return;
            using var helper = CreateDisplayServicesHelper();
            helper.RemoveDisplayGammaEventListener(handle, disposeHandle);
        }

        /// <summary>
        /// Subscribe to gamut change events.
        /// </summary>
        public DisplayGamutListenerHandle AddDisplayGamutEventListener(DisplayGamutListenerHandle.OnDisplayGamutChanged callback)
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.AddDisplayGamutEventListener(callback);
        }

        public void RemoveDisplayGamutEventListener(DisplayGamutListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid) return;
            using var helper = CreateDisplayServicesHelper();
            helper.RemoveDisplayGamutEventListener(handle, disposeHandle);
        }

        /// <summary>
        /// Subscribe to 3DLUT change events.
        /// </summary>
        public Display3DLutListenerHandle AddDisplay3dLutEventListener(Display3DLutListenerHandle.OnDisplay3DLutChanged callback)
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.AddDisplay3DLutEventListener(callback);
        }

        public void RemoveDisplay3dLutEventListener(Display3DLutListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid) return;
            using var helper = CreateDisplayServicesHelper();
            helper.RemoveDisplay3DLutEventListener(handle, disposeHandle);
        }

        /// <summary>
        /// Subscribe to display list change events.
        /// </summary>
        public DisplayListListenerHandle AddDisplayListEventListener(DisplayListListenerHandle.OnDisplayListChanged callback)
        {
            ThrowIfDisposed();
            using var helper = CreateDisplayServicesHelper();
            return helper.AddDisplayListEventListener(callback);
        }

        public void RemoveDisplayListEventListener(DisplayListListenerHandle handle, bool disposeHandle = true)
        {
            ThrowIfDisposed();
            if (handle == null || handle.IsInvalid) return;
            using var helper = CreateDisplayServicesHelper();
            helper.RemoveDisplayListEventListener(handle, disposeHandle);
        }

        /// <summary>
        /// GPU that drives this display.
        /// </summary>
        public ADLXGPU GetGPU()
        {
            ThrowIfDisposed();
            IADLXGPU* pGpu = null;
            var result = _display.Get()->GetGPU(&pGpu);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pGpu == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU lookup for display not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to resolve GPU for display");

            IADLXDesktopServices* desktopServices = _desktopServices.HasValue ? _desktopServices.Value.Get() : null;
            return new ADLXGPU(pGpu, _displayServices.Get(), desktopServices);
        }

        /// <summary>
        /// Desktop that contains this display (requires desktop services supplied to the display instance).
        /// </summary>
        public ADLXDesktop GetDesktop()
        {
            ThrowIfDisposed();
            if (!_desktopServices.HasValue || _desktopServices.Value.Get() == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services were not provided for this display instance");

            var desktopServices = _desktopServices.Value.Get();
            IADLXDesktopList* pDesktopList = null;
            var result = desktopServices->GetDesktops(&pDesktopList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate desktops while resolving display owner");

            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);
            var targetId = _identity.UniqueId;

            for (uint i = 0; i < desktopList.Get()->Size(); i++)
            {
                IADLXDesktop* pDesktop = null;
                var itemResult = desktopList.Get()->At(i, &pDesktop);
                if (itemResult != ADLX_RESULT.ADLX_OK || pDesktop == null)
                {
                    if (pDesktop != null)
                        ADLXUtils.ReleaseInterface((IntPtr)pDesktop);
                    throw new ADLXException(itemResult, "Failed to access desktop while resolving display owner");
                }

                using var desktopHelper = new ADLXDesktopServicesHelper(desktopServices);
                var displayListPtr = desktopHelper.GetDesktopDisplayListNative(pDesktop);
                using var displayList = new ComPtr<IADLXDisplayList>(displayListPtr);
                var displayCount = displayList.Get()->Size();
                var match = false;
                for (uint d = 0; d < displayCount; d++)
                {
                    IADLXDisplay* pCandidate = null;
                    displayList.Get()->At(d, &pCandidate);
                    using var candidate = new ComPtr<IADLXDisplay>(pCandidate);
                    nuint uid = 0;
                    candidate.Get()->UniqueId(&uid);
                    if ((ulong)uid == targetId)
                    {
                        match = true;
                        break;
                    }
                }

                if (match)
                {
                    return new ADLXDesktop(desktopServices, pDesktop);
                }

                ADLXUtils.ReleaseInterface((IntPtr)pDesktop);
            }

            throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop containing display was not found");
        }

        /// <summary>
        /// Desktop identity that contains this display (requires desktop services supplied to the display instance).
        /// </summary>
        public DesktopInfo GetDesktopInfo()
        {
            using var desktop = GetDesktop();
            return desktop.Identity;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ADLXDisplay));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _display.Dispose();
            _desktopServices?.Dispose();
            _displayServices.Dispose();
            _disposed = true;
        }

        private ADLXDisplayServicesHelper CreateDisplayServicesHelper()
        {
            IADLXDesktopServices* desktopServices = _desktopServices.HasValue ? _desktopServices.Value.Get() : null;
            // Helpers AddRef their inputs; safe to dispose helper without affecting this façade.
            return new ADLXDisplayServicesHelper(_displayServices.Get(), desktopServices);
        }
    }
}

