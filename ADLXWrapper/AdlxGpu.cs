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

        public GpuInfo Identity { get { ThrowIfDisposed(); return _identity; } }
        public string Name { get { ThrowIfDisposed(); return _identity.Name; } }
        public string VendorId { get { ThrowIfDisposed(); return _identity.VendorId; } }
        public int UniqueId { get { ThrowIfDisposed(); return _identity.UniqueId; } }
        public uint TotalVRAM { get { ThrowIfDisposed(); return _identity.TotalVRAM; } }
        public string VRAMType { get { ThrowIfDisposed(); return _identity.VRAMType; } }
        public bool IsExternal { get { ThrowIfDisposed(); return _identity.IsExternal; } }
        public bool HasDesktops { get { ThrowIfDisposed(); return _identity.HasDesktops; } }
        public string DeviceId { get { ThrowIfDisposed(); return _identity.DeviceId; } }
        public string PNPString { get { ThrowIfDisposed(); return _identity.PNPString; } }
        public string DriverPath { get { ThrowIfDisposed(); return _identity.DriverPath; } }

        /// <summary>
        /// Enumerate façade displays driven by this GPU.
        /// </summary>
        public IReadOnlyList<ADLXDisplay> EnumerateDisplaysForGPU()
        {
            ThrowIfDisposed();
            using var displayHelper = CreateDisplayServicesHelper();
            return displayHelper.EnumerateADLXDisplaysForGpu(_identity.UniqueId);
        }

        /// <summary>
        /// Enumerate façade desktops containing displays from this GPU.
        /// </summary>
        public IReadOnlyList<ADLXDesktop> EnumerateDesktopsForGPU()
        {
            ThrowIfDisposed();
            using var desktopHelper = CreateDesktopServicesHelper();
            return desktopHelper.EnumerateADLXDesktopsForGpu(_identity.UniqueId);
        }

        /// <summary>
        /// Subscribe to display list change events. Returns a handle that can be disposed or passed to the remove helper.
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
        /// Subscribe to desktop list change events. Returns a handle that can be disposed or passed to the remove helper.
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
