using System;

namespace ADLXWrapper
{
    /// <summary>
    /// Flattened GPU façade with identity metadata. Topology enumeration can be added as needed.
    /// </summary>
    public sealed unsafe class AdlxGpu : IDisposable
    {
        private ComPtr<IADLXGPU> _gpu;
        private readonly GpuInfo _identity;
        private bool _disposed;

        public AdlxGpu(IADLXGPU* pGpu)
        {
            if (pGpu == null) throw new ArgumentNullException(nameof(pGpu));
            _gpu = new ComPtr<IADLXGPU>(pGpu);
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

        private void ThrowIfDisposed()
        {
            if (_disposed) throw new ObjectDisposedException(nameof(AdlxGpu));
        }

        public void Dispose()
        {
            if (_disposed) return;
            _gpu.Dispose();
            _disposed = true;
        }
    }
}
