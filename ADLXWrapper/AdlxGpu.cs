using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Flattened GPU façade with identity metadata and topology listeners.
    /// </summary>
    public sealed unsafe class ADLXGPU : IDisposable
    {
        private ComPtr<IADLXGPU> _gpu;
        private ComPtr<IADLXDisplayServices>? _displayServices;
        private ComPtr<IADLXDesktopServices>? _desktopServices;
        private readonly GpuInfo _identity;
        private bool _disposed;

        /// <summary>
        /// Creates a managed GPU facade from a native GPU pointer, optionally wiring display/desktop services for topology helpers.
        /// </summary>
        /// <param name="pGpu">Native GPU pointer.</param>
        /// <param name="pDisplayServices">Optional display services pointer for display enumeration.</param>
        /// <param name="pDesktopServices">Optional desktop services pointer for desktop enumeration.</param>
        public ADLXGPU(IADLXGPU* pGpu, IADLXDisplayServices* pDisplayServices = null, IADLXDesktopServices* pDesktopServices = null)
        {
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));
            _gpu = new ComPtr<IADLXGPU>(pGpu);
            if (pDisplayServices != null)
            {
                ADLXUtils.AddRefInterface((IntPtr)pDisplayServices);
                _displayServices = new ComPtr<IADLXDisplayServices>(pDisplayServices);
            }
            if (pDesktopServices != null)
            {
                ADLXUtils.AddRefInterface((IntPtr)pDesktopServices);
                _desktopServices = new ComPtr<IADLXDesktopServices>(pDesktopServices);
            }
            _identity = new GpuInfo(pGpu);
        }

        public GpuInfo Identity { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity; } }
        public string Name { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.Name; } }
        public string VendorId { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.VendorId; } }
        public int UniqueId { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.UniqueId; } }
        public uint TotalVRAM { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.TotalVRAM; } }
        public string VRAMType { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.VRAMType; } }
        public bool IsExternal { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.IsExternal; } }
        public bool HasDesktops { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.HasDesktops; } }
        public string DeviceId { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.DeviceId; } }
        public string PNPString { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.PNPString; } }
        public string DriverPath { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.DriverPath; } }
        public ADLX_GPU_TYPE GPUType { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.GPUType; } }
        public ADLX_ASIC_FAMILY_TYPE AsicFamilyType { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.AsicFamilyType; } }
        public ADLX_PCI_BUS_TYPE PciBusType { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.PciBusType; } }
        public uint PciBusLaneWidth { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.PciBusLaneWidth; } }
        public ADLX_MGPU_MODE MultiGpuMode { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.MultiGpuMode; } }
        public string ProductName { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.ProductName; } }
        public string SubSystemId { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.SubSystemId; } }
        public string SubSystemVendorId { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.SubSystemVendorId; } }
        public string RevisionId { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.RevisionId; } }
        public string DriverVersion { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.DriverVersion; } }
        public string AMDSoftwareVersion { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.AMDSoftwareVersion; } }
        public string AMDWindowsDriverVersion { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.AMDWindowsDriverVersion; } }
        public ADLX_LUID Luid { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.Luid; } }

        /// <summary>
        /// Enumerates managed displays driven by this GPU. Callers must dispose each display.
        /// </summary>
        public IReadOnlyList<ADLXDisplay> EnumerateDisplaysForGPU()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            using var displayHelper = CreateDisplayServicesHelper();
            return displayHelper.EnumerateADLXDisplaysForGpu(_identity.UniqueId);
        }

        /// <summary>
        /// Enumerates managed desktops containing displays from this GPU. Callers must dispose each desktop.
        /// </summary>
        public IReadOnlyList<ADLXDesktop> EnumerateDesktopsForGPU()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            using var desktopHelper = CreateDesktopServicesHelper();
            return desktopHelper.EnumerateADLXDesktopsForGpu(_identity.UniqueId);
        }

        /// <summary>
        /// Subscribe to display list change events. Returns a handle that can be disposed or passed to the remove helper.
        /// </summary>
        public DisplayListListenerHandle AddDisplayListEventListener(DisplayListListenerHandle.OnDisplayListChanged callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            using var helper = CreateDisplayServicesHelper();
            return helper.AddDisplayListEventListener(callback);
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
            if (handle == null || handle.IsInvalid) return;
            using var helper = CreateDisplayServicesHelper();
            helper.RemoveDisplayListEventListener(handle, disposeHandle);
        }

        /// <summary>
        /// Subscribe to desktop list change events. Returns a handle that can be disposed or passed to the remove helper.
        /// </summary>
        public DesktopListListenerHandle AddDesktopListEventListener(DesktopListListenerHandle.OnDesktopListChanged callback)
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            using var helper = CreateDesktopServicesHelper();
            return helper.AddDesktopListEventListener(callback);
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
            if (handle == null || handle.IsInvalid) return;
            using var helper = CreateDesktopServicesHelper();
            helper.RemoveDesktopListEventListener(handle, disposeHandle);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(ADLXGPU));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _desktopServices?.Dispose();
            _displayServices?.Dispose();
            _gpu.Dispose();
            _disposed = true;
        }

        private ADLXDisplayServicesHelper CreateDisplayServicesHelper()
        {
            if (!_displayServices.HasValue)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services were not provided for this GPU instance");
            IADLXDesktopServices* desktopServices = _desktopServices.HasValue ? _desktopServices.Value.Get() : null;
            return new ADLXDisplayServicesHelper(_displayServices.Value.Get(), desktopServices);
        }

        private ADLXDesktopServicesHelper CreateDesktopServicesHelper()
        {
            if (!_desktopServices.HasValue)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services were not provided for this GPU instance");
            IADLXDisplayServices* displayServices = _displayServices.HasValue ? _displayServices.Value.Get() : null;
            return new ADLXDesktopServicesHelper(_desktopServices.Value.Get(), displayServices);
        }
    }
}

