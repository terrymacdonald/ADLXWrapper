using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Flattened display façade with identity metadata, support-guarded feature accessors, and native list exposure.
    /// </summary>
    public sealed unsafe class AdlxDisplay : IDisposable
    {
        private ComPtr<IADLXDisplayServices> _displayServices;
        private ComPtr<IADLXDisplay> _display;
        private readonly DisplayInfo _identity;
        private bool _disposed;

        public AdlxDisplay(IADLXDisplayServices* pDisplayServices, IADLXDisplay* pDisplay)
        {
            if (pDisplayServices == null) throw new ArgumentNullException(nameof(pDisplayServices));
            if (pDisplay == null) throw new ArgumentNullException(nameof(pDisplay));

            _displayServices = new ComPtr<IADLXDisplayServices>(pDisplayServices);
            _display = new ComPtr<IADLXDisplay>(pDisplay);
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
        public DisplayInfo Identity { get { ThrowIfDisposed(); return _identity; } }

        /// <summary>
        /// Query FreeSync support/enabled state.
        /// </summary>
        public (bool supported, bool enabled) GetFreeSyncState()
        {
            ThrowIfDisposed();
            using var fs = new ComPtr<IADLXDisplayFreeSync>((IADLXDisplayFreeSync*)ADLXDisplaySettingsHelpers.GetFreeSyncHandle((IntPtr)_displayServices.Get(), (IntPtr)_display.Get()));
            return ADLXDisplaySettingsHelpers.GetFreeSyncState((IntPtr)fs.Get());
        }

        public void SetFreeSyncEnabled(bool enable)
        {
            ThrowIfDisposed();
            using var fs = new ComPtr<IADLXDisplayFreeSync>((IADLXDisplayFreeSync*)ADLXDisplaySettingsHelpers.GetFreeSyncHandle((IntPtr)_displayServices.Get(), (IntPtr)_display.Get()));
            ADLXDisplaySettingsHelpers.SetFreeSyncEnabled((IntPtr)fs.Get(), enable);
        }

        /// <summary>
        /// Query GPU scaling support/enabled state.
        /// </summary>
        public (bool supported, bool enabled) GetGpuScalingState()
        {
            ThrowIfDisposed();
            using var scaling = new ComPtr<IADLXDisplayGPUScaling>((IADLXDisplayGPUScaling*)ADLXDisplaySettingsHelpers.GetGPUScalingHandle((IntPtr)_displayServices.Get(), (IntPtr)_display.Get()));
            return ADLXDisplaySettingsHelpers.GetGPUScalingState((IntPtr)scaling.Get());
        }

        public void SetGpuScalingEnabled(bool enable)
        {
            ThrowIfDisposed();
            using var scaling = new ComPtr<IADLXDisplayGPUScaling>((IADLXDisplayGPUScaling*)ADLXDisplaySettingsHelpers.GetGPUScalingHandle((IntPtr)_displayServices.Get(), (IntPtr)_display.Get()));
            ADLXDisplaySettingsHelpers.SetGPUScalingEnabled((IntPtr)scaling.Get(), enable);
        }

        /// <summary>
        /// Query scaling mode support/value.
        /// </summary>
        public (bool supported, ADLX_SCALE_MODE mode) GetScalingMode()
        {
            ThrowIfDisposed();
            using var scalingMode = new ComPtr<IADLXDisplayScalingMode>((IADLXDisplayScalingMode*)ADLXDisplaySettingsHelpers.GetScalingModeHandle((IntPtr)_displayServices.Get(), (IntPtr)_display.Get()));
            return ADLXDisplaySettingsHelpers.GetScalingMode((IntPtr)scalingMode.Get());
        }

        public void SetScalingMode(ADLX_SCALE_MODE mode)
        {
            ThrowIfDisposed();
            using var scalingMode = new ComPtr<IADLXDisplayScalingMode>((IADLXDisplayScalingMode*)ADLXDisplaySettingsHelpers.GetScalingModeHandle((IntPtr)_displayServices.Get(), (IntPtr)_display.Get()));
            ADLXDisplaySettingsHelpers.SetScalingMode((IntPtr)scalingMode.Get(), mode);
        }

        /// <summary>
        /// Enumerate custom resolutions as managed DTOs.
        /// </summary>
        public IEnumerable<DisplayResolutionInfo> EnumerateCustomResolutions()
        {
            ThrowIfDisposed();
            return ADLXDisplaySettingsHelpers.EnumerateCustomResolutions(_displayServices.Get(), _display.Get());
        }

        /// <summary>
        /// Native custom resolution list accessor. Caller must dispose the returned ComPtr and any retained items.
        /// </summary>
        public ComPtr<IADLXDisplayResolutionList> GetCustomResolutionListNative()
        {
            ThrowIfDisposed();
            return new ComPtr<IADLXDisplayResolutionList>(ADLXDisplaySettingsHelpers.GetCustomResolutionListNative(_displayServices.Get(), _display.Get()));
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(AdlxDisplay));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _display.Dispose();
            _displayServices.Dispose();
            _disposed = true;
        }
    }
}

