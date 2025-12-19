using System;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLX3DSettingsServices selecting the highest available interface and exposing change handling.
    /// </summary>
    public sealed unsafe class ADLX3DSettingsServicesHelper : IDisposable
    {
        private ComPtr<IADLX3DSettingsServices> _services;
        private ComPtr<IADLX3DSettingsServices1>? _services1;
        private ComPtr<IADLX3DSettingsServices2>? _services2;
        private ComPtr<IADLX3DSettingsChangedHandling>? _changedHandling;
        private bool _disposed;

        public ADLX3DSettingsServicesHelper(IADLX3DSettingsServices* services, bool addRef = true)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (addRef)
            {
                ADLXHelpers.AddRefInterface((IntPtr)services);
            }
            _services = new ComPtr<IADLX3DSettingsServices>(services);
            TryUpgradeServices(services);
        }

        public IADLX3DSettingsServices* Get3DSettingsServicesNative()
        {
            ThrowIfDisposed();
            return GetHighestServices();
        }

        public AdlxInterfaceHandle Get3DSettingsServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(Get3DSettingsServicesNative(), addRef: true);
        }

        public IADLX3DSettingsChangedHandling* Get3DSettingsChangedHandlingNative()
        {
            ThrowIfDisposed();
            if (_changedHandling.HasValue)
                return _changedHandling.Value.Get();

            IADLX3DSettingsChangedHandling* handling = null;
            var result = GetHighestServices()->Get3DSettingsChangedHandling(&handling);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || handling == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "3D settings change handling not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get 3D settings change handling");

            _changedHandling = new ComPtr<IADLX3DSettingsChangedHandling>(handling);
            return handling;
        }

        public AdlxInterfaceHandle Get3DSettingsChangedHandling()
        {
            return AdlxInterfaceHandle.From(Get3DSettingsChangedHandlingNative(), addRef: true);
        }

        public void Dispose()
        {
            if (_disposed) return;
            _changedHandling?.Dispose();
            _services2?.Dispose();
            _services1?.Dispose();
            _services.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ADLX3DSettingsServicesHelper));
        }

        ~ADLX3DSettingsServicesHelper()
        {
            if (!_disposed)
            {
                Dispose();
            }
        }

        private void TryUpgradeServices(IADLX3DSettingsServices* services)
        {
            if (services == null) return;

            if (ADLXHelpers.TryQueryInterface((IntPtr)services, nameof(IADLX3DSettingsServices2), out var p2))
            {
                _services2 = new ComPtr<IADLX3DSettingsServices2>((IADLX3DSettingsServices2*)p2);
                return;
            }

            if (ADLXHelpers.TryQueryInterface((IntPtr)services, nameof(IADLX3DSettingsServices1), out var p1))
            {
                _services1 = new ComPtr<IADLX3DSettingsServices1>((IADLX3DSettingsServices1*)p1);
            }
        }

        private IADLX3DSettingsServices* GetHighestServices()
        {
            if (_services2.HasValue)
                return (IADLX3DSettingsServices*)_services2.Value.Get();
            if (_services1.HasValue)
                return (IADLX3DSettingsServices*)_services1.Value.Get();
            return _services.Get();
        }
    }
}
