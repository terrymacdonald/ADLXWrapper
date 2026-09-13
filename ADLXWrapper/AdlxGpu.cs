using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Flattened GPU faÃ§ade with identity metadata and topology listeners.
    /// </summary>
    public sealed unsafe class ADLXGPU : IDisposable
    {
        private ComPtr<IADLXGPU> _gpu;
        private ComPtr<IADLXGPU3>? _gpu3;
        private ComPtr<IADLXDisplayServices>? _displayServices;
        private ComPtr<IADLXDesktopServices>? _desktopServices;
        private readonly GpuDto _identity;
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
            _identity = new GpuDto(pGpu);
        }

        public GpuDto Identity { get { ThrowIfDisposed();
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
        public string AMDSoftwareEdition { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.AMDSoftwareEdition; } }
        public uint AMDSoftwareReleaseYear { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.AMDSoftwareReleaseYear; } }
        public uint AMDSoftwareReleaseMonth { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.AMDSoftwareReleaseMonth; } }
        public uint AMDSoftwareReleaseDay { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.AMDSoftwareReleaseDay; } }
        public LuidDto Luid { get { ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead(); return _identity.Luid; } }

        /// <summary>
        /// Gets extended GPU architecture and VRAM information available through IADLXGPU3.
        /// </summary>
        /// <exception cref="ADLXException">If IADLXGPU3 is unsupported or data retrieval fails.</exception>
        public Gpu3InfoDto GetGpu3Info()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var gpu3 = GetGpu3();

            sbyte* microArchitecture = null;
            EnsureSuccess(gpu3->MicroArchitecture(&microArchitecture), "Failed to query GPU microarchitecture");
            uint highestVramBandwidth = 0;
            EnsureSuccess(gpu3->HighestVRAMBandwidth(&highestVramBandwidth), "Failed to query GPU highest VRAM bandwidth");
            uint invisibleVram = 0;
            EnsureSuccess(gpu3->InvisibleVRAM(&invisibleVram), "Failed to query GPU invisible VRAM");
            uint visibleVram = 0;
            EnsureSuccess(gpu3->VisibleVRAM(&visibleVram), "Failed to query GPU visible VRAM");
            uint vramVendorRevId = 0;
            EnsureSuccess(gpu3->VRAMVendorRevId(&vramVendorRevId), "Failed to query GPU VRAM vendor revision id");
            uint vramBandwidth = 0;
            EnsureSuccess(gpu3->VRAMBandwidth(&vramBandwidth), "Failed to query GPU VRAM bandwidth");
            uint vramBitRate = 0;
            EnsureSuccess(gpu3->VRAMBitRate(&vramBitRate), "Failed to query GPU VRAM bit rate");

            return new Gpu3InfoDto(ADLXUtils.MarshalString(&microArchitecture), highestVramBandwidth, invisibleVram, visibleVram, vramVendorRevId, vramBandwidth, vramBitRate);
        }

        /// <summary>
        /// Returns whether this GPU supports the ADLX GPU stress test feature.
        /// </summary>
        public bool IsStressTestSupported()
        {
            ThrowIfDisposed();
            using var _sync = ADLXSync.EnterRead();
            var gpu3 = GetGpu3();
            bool supported = false;
            var result = gpu3->IsSupportedStressTest(&supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return false;
            EnsureSuccess(result, "Failed to query GPU stress test support");
            return supported;
        }

        /// <summary>
        /// Starts a GPU stress test for the requested duration in seconds.
        /// The returned operation must remain undisposed until it completes.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="durationSeconds"/> is zero.</exception>
        /// <exception cref="ADLXException">If stress testing is unsupported or ADLX cannot start it.</exception>
        public ADLXGpuStressTest StartStressTest(uint durationSeconds)
        {
            ThrowIfDisposed();
            if (durationSeconds == 0)
                throw new ArgumentOutOfRangeException(nameof(durationSeconds), "Stress test duration must be greater than zero.");

            using var _sync = ADLXSync.EnterRead();
            var gpu3 = GetGpu3();
            bool supported = false;
            var supportResult = gpu3->IsSupportedStressTest(&supported);
            if (supportResult == ADLX_RESULT.ADLX_NOT_SUPPORTED || !supported)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU stress testing is not supported by this GPU");
            EnsureSuccess(supportResult, "Failed to query GPU stress test support");

            var operation = new ADLXGpuStressTest(gpu3, UniqueId, durationSeconds);
            var startResult = gpu3->StartStressTest(operation.GetListener(), durationSeconds);
            if (startResult != ADLX_RESULT.ADLX_OK)
            {
                operation.ReleaseAfterStartFailure();
                throw new ADLXException(startResult, "Failed to start GPU stress test");
            }

            return operation;
        }

        /// <summary>
        /// Tries to start a GPU stress test without throwing when the feature is unsupported.
        /// </summary>
        public bool TryStartStressTest(uint durationSeconds, out ADLXGpuStressTest? operation)
        {
            try
            {
                operation = StartStressTest(durationSeconds);
                return true;
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                operation = null;
                return false;
            }
        }

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
            _gpu3?.Dispose();
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

        private IADLXGPU3* GetGpu3()
        {
            if (_gpu3.HasValue)
                return _gpu3.Value.Get();
            if (!ADLXUtils.TryQueryInterface((IntPtr)_gpu.Get(), nameof(IADLXGPU3), out var gpu3) || gpu3 == IntPtr.Zero)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "IADLXGPU3 is not supported by this GPU");
            _gpu3 = new ComPtr<IADLXGPU3>((IADLXGPU3*)gpu3);
            return _gpu3.Value.Get();
        }

        private static void EnsureSuccess(ADLX_RESULT result, string message)
        {
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, message);
        }
    }

    /// <summary>
    /// Extended read-only GPU information provided by IADLXGPU3.
    /// </summary>
    public readonly struct Gpu3InfoDto
    {
        public string MicroArchitecture { get; init; }
        public uint HighestVRAMBandwidth { get; init; }
        public uint InvisibleVRAM { get; init; }
        public uint VisibleVRAM { get; init; }
        public uint VRAMVendorRevId { get; init; }
        public uint VRAMBandwidth { get; init; }
        public uint VRAMBitRate { get; init; }

        public Gpu3InfoDto(string microArchitecture, uint highestVramBandwidth, uint invisibleVram, uint visibleVram, uint vramVendorRevId, uint vramBandwidth, uint vramBitRate)
        {
            MicroArchitecture = microArchitecture;
            HighestVRAMBandwidth = highestVramBandwidth;
            InvisibleVRAM = invisibleVram;
            VisibleVRAM = visibleVram;
            VRAMVendorRevId = vramVendorRevId;
            VRAMBandwidth = vramBandwidth;
            VRAMBitRate = vramBitRate;
        }
    }
}

