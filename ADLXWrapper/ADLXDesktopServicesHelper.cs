using System;
using System.Collections.Generic;

namespace ADLXWrapper
{
    /// <summary>
    /// Wrapper over IADLXDesktopServices providing managed/native accessors and Eyefinity helpers.
    /// </summary>
    public sealed unsafe class ADLXDesktopServicesHelper : IDisposable
    {
        private ComPtr<IADLXDesktopServices> _desktopServices;
        private ComPtr<IADLXDesktopChangedHandling>? _desktopChangedHandling;
        private bool _disposed;

        public ADLXDesktopServicesHelper(IADLXDesktopServices* desktopServices, bool addRef = true)
        {
            if (desktopServices == null) throw new ArgumentNullException(nameof(desktopServices));
            if (addRef)
            {
                ADLXUtils.AddRefInterface((IntPtr)desktopServices);
            }
            _desktopServices = new ComPtr<IADLXDesktopServices>(desktopServices);
        }

        public IADLXDesktopServices* GetDesktopServicesNative()
        {
            ThrowIfDisposed();
            return _desktopServices.Get();
        }

        public AdlxInterfaceHandle GetDesktopServices()
        {
            ThrowIfDisposed();
            return AdlxInterfaceHandle.From(GetDesktopServicesNative(), addRef: true);
        }

        public IEnumerable<DesktopInfo> EnumerateDesktops()
        {
            ThrowIfDisposed();
            var services = _desktopServices.Get();
            if (services == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported by this ADLX system");

            IADLXDesktopList* pDesktopList = null;
            var result = services->GetDesktops(&pDesktopList);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pDesktopList == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to enumerate desktops");

            var desktops = new List<DesktopInfo>();
            using var desktopList = new ComPtr<IADLXDesktopList>(pDesktopList);
            for (uint i = 0; i < desktopList.Get()->Size(); i++)
            {
                IADLXDesktop* pDesktop = null;
                desktopList.Get()->At(i, &pDesktop);
                using var desktop = new ComPtr<IADLXDesktop>(pDesktop);
                desktops.Add(new DesktopInfo(desktop.Get()));
            }

            return desktops;
        }

        public AdlxInterfaceHandle[] EnumerateDesktopHandles()
        {
            ThrowIfDisposed();
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
            var handles = new AdlxInterfaceHandle[count];

            for (uint i = 0; i < count; i++)
            {
                IADLXDesktop* pDesktop = null;
                desktopList.Get()->At(i, &pDesktop);
                handles[i] = AdlxInterfaceHandle.From(pDesktop, addRef: false);
            }

            return handles;
        }

        public IADLXDesktopList* GetDesktopListNative()
        {
            ThrowIfDisposed();
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

        public AdlxInterfaceHandle GetDesktopChangedHandling()
        {
            return AdlxInterfaceHandle.From(GetDesktopChangedHandlingNative(), addRef: true);
        }

        public SimpleEyefinityInfo GetSimpleEyefinity()
        {
            ThrowIfDisposed();
            IADLXSimpleEyefinity* pSimple = null;
            var result = _desktopServices.Get()->GetSimpleEyefinity(&pSimple);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pSimple == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Simple Eyefinity not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");

            using var simple = new ComPtr<IADLXSimpleEyefinity>(pSimple);
            return new SimpleEyefinityInfo(simple.Get());
        }

        public AdlxInterfaceHandle GetSimpleEyefinityHandle()
        {
            ThrowIfDisposed();
            IADLXSimpleEyefinity* pSimple = null;
            var result = _desktopServices.Get()->GetSimpleEyefinity(&pSimple);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || pSimple == null)
                throw new ADLXException(ADLX_RESULT.ADLX_NOT_SUPPORTED, "Simple Eyefinity not supported by this ADLX system");
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");

            return AdlxInterfaceHandle.From(pSimple, addRef: false);
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
            _desktopServices.Dispose();
            _disposed = true;
            GC.SuppressFinalize(this);
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
}

