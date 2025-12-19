using System;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLXPowerTuningServices selecting the highest available interface (if extended) and exposing change handling.
    /// </summary>
    public sealed unsafe class ADLXPowerTuningServicesHelper : IDisposable
    {
        private ComPtr<IADLXPowerTuningServices> _services;
        private ComPtr<IADLXPowerTuningServices1>? _services1;
        private ComPtr<IADLXPowerTuningChangedHandling>? _changedHandling;
        private bool _disposed;

        public ADLXPowerTuningServicesHelper(IADLXPowerTuningServices* services, bool addRef = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXHelpers.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLXPowerTuningServices>(services);
            TryUpgradeServices(services);
        }

        public IADLXPowerTuningServices* GetPowerTuningServicesNative()
        {
            ThrowIfDisposed();
            return GetHighestServices();
        }

        public AdlxInterfaceHandle GetPowerTuningServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(GetPowerTuningServicesNative(), addRef: true);
        }

        public bool IsGPUConnectSupported()
        {
            ThrowIfDisposed();
            var services1 = GetPowerTuningServices1();
            bool supported = false;
            var result = services1->IsGPUConnectSupported(&supported);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUConnect not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to query GPUConnect support");

            return supported;
        }

        public AdlxInterfaceHandle[] EnumerateGPUConnectGpuHandles()
        {
            ThrowIfDisposed();
            var services1 = GetPowerTuningServices1();

            IADLXGPU2List* pList = null;
            var result = services1->GetGPUConnectGPUs(&pList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUConnect GPU enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate GPUConnect-capable GPUs");

            using var list = new ComPtr<IADLXGPU2List>(pList);
            var count = list.Get()->Size();
            var handles = new AdlxInterfaceHandle[count];
            for (uint i = 0; i < count; i++)
            {
                IADLXGPU2* gpu = null;
                list.Get()->At(i, &gpu);
                handles[i] = AdlxInterfaceHandle.From(gpu, addRef: false);
            }

            return handles;
        }

        public IADLXGPU2List* GetGPUConnectGpuListNative()
        {
            ThrowIfDisposed();
            var services1 = GetPowerTuningServices1();

            IADLXGPU2List* pList = null;
            var result = services1->GetGPUConnectGPUs(&pList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPUConnect GPU enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate GPUConnect-capable GPUs");

            return pList; // caller must wrap in ComPtr and dispose
        }

        public IADLXPowerTuningChangedHandling* GetPowerTuningChangedHandlingNative()
        {
            ThrowIfDisposed();
            if (_changedHandling.HasValue)
                return _changedHandling.Value.Get();

            IADLXPowerTuningChangedHandling* handling = null;
            var result = GetHighestServices()->GetPowerTuningChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Power tuning change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get power tuning change handling");

            _changedHandling = new ComPtr<IADLXPowerTuningChangedHandling>(handling);
            return handling;
        }

        public AdlxInterfaceHandle GetPowerTuningChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetPowerTuningChangedHandlingNative(), addRef: true);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _changedHandling?.Dispose();
            _services1?.Dispose();
            _services.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ADLXPowerTuningServicesHelper));
        }

        ~ADLXPowerTuningServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }

        private void TryUpgradeServices(IADLXPowerTuningServices* services)
        {
            if (services == null) return;

            if (ADLXHelpers.TryQueryInterface((IntPtr)services, nameof(IADLXPowerTuningServices1), out var p1))
            {
                _services1 = new ComPtr<IADLXPowerTuningServices1>((IADLXPowerTuningServices1*)p1);
            }
        }

        private IADLXPowerTuningServices1* GetPowerTuningServices1()
        {
            if (_services1.HasValue)
                return _services1.Value.Get();

            if (ADLXHelpers.TryQueryInterface((IntPtr)_services.Get(), nameof(IADLXPowerTuningServices1), out var p1))
            {
                _services1 = new ComPtr<IADLXPowerTuningServices1>((IADLXPowerTuningServices1*)p1);
                return _services1.Value.Get();
            }

            throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Extended power tuning services not supported by this ADLX system");
        }

        private IADLXPowerTuningServices* GetHighestServices()
        {
            if (_services1.HasValue)
                return (IADLXPowerTuningServices*)_services1.Value.Get();
            return _services.Get();
        }
    }
}
