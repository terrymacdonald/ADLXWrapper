using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

[Collection("NativeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLXDesktopServicesNativeTests
{
    private readonly NativeSession _session;

    public ADLXDesktopServicesNativeTests(NativeSessionFixture fixture)
    {
        _session = fixture.Session;
    }

    [SkippableFact]
    public void Desktop_services_acquire_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetDesktopServicesComPtrOrSkip(out _);
    }

    [SkippableFact]
    public void Desktop_list_enumeration_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetDesktopServicesComPtrOrSkip(out var services);
        uint numDesktops = 0;
        var countResult = services->GetNumberOfDesktops(&numDesktops);
        Skip.If(countResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop count not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, countResult);

        using var listPtr = GetDesktopListOrSkip(services, out var list);
        var size = list->Size();
        Skip.If(size == 0, "No desktops returned by ADLX.");
        Assert.Equal(numDesktops, size);
    }

    [SkippableFact]
    public void Desktop_orientation_all_desktops_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDesktop((services, desktop, index) =>
        {
            ADLX_ORIENTATION orientation = 0;
            AssertResultOrContinue(desktop->Orientation(&orientation));
        });
    }

    [SkippableFact]
    public void Desktop_size_all_desktops_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDesktop((services, desktop, index) =>
        {
            int width = 0, height = 0;
            if (AssertResultOrContinue(desktop->Size(&width, &height)))
            {
                Assert.True(width > 0 && height > 0);
            }
        });
    }

    [SkippableFact]
    public void Desktop_top_left_all_desktops_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDesktop((services, desktop, index) =>
        {
            ADLX_Point topLeft = default;
            AssertResultOrContinue(desktop->TopLeft(&topLeft));
        });
    }

    [SkippableFact]
    public void Desktop_type_all_desktops_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDesktop((services, desktop, index) =>
        {
            ADLX_DESKTOP_TYPE desktopType = 0;
            AssertResultOrContinue(desktop->Type(&desktopType));
        });
    }

    [SkippableFact]
    public void Desktop_display_count_all_desktops_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDesktop((services, desktop, index) =>
        {
            uint displayCount = 0;
            if (AssertResultOrContinue(desktop->GetNumberOfDisplays(&displayCount)))
            {
                Assert.True(displayCount > 0);
            }
        });
    }

    [SkippableFact]
    public void Desktop_display_list_all_desktops_native()
    {
        SkipIfNoAdlxSupport();
        ForEachDesktop((services, desktop, index) =>
        {
            IADLXDisplayList* displayList = null;
            if (AssertResultOrContinue(desktop->GetDisplays(&displayList)))
            {
                using var dispListPtr = new ComPtr<IADLXDisplayList>(displayList);
                var dispSize = displayList->Size();
                Assert.True(dispSize >= 0);
            }
        });
    }

    [SkippableFact]
    public void Desktop_events_list_listener_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetDesktopServicesComPtrOrSkip(out var services);

        IADLXDesktopChangedHandling* handling = null;
        var result = services->GetDesktopChangedHandling(&handling);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop changed handling not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var handlingPtr = new ComPtr<IADLXDesktopChangedHandling>(handling);
        using var listener = new DummyDesktopListChangedListener();

        var addResult = handling->AddDesktopListEventListener(listener.Pointer);
        Skip.If(addResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop list event listeners not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, addResult);

        var removeResult = handling->RemoveDesktopListEventListener(listener.Pointer);
        Assert.Equal(ADLX_RESULT.ADLX_OK, removeResult);
    }

    private void SkipIfNoAdlxSupport()
    {
        Skip.IfNot(ADLXApiHelper.IsADLXDllAvailable(out var dllError), $"ADLX DLL unavailable: {dllError}");
        Skip.IfNot(ADLXHardwareDetection.HasAMDGPU(out var gpuError), $"AMD GPU not detected: {gpuError}");
    }

    private unsafe ComPtr<IADLXDesktopServices> GetDesktopServicesComPtrOrSkip(out IADLXDesktopServices* services)
    {
        services = null;
        ADLX_RESULT result;
        var system = _session.System;
        fixed (IADLXDesktopServices** pServices = &services)
        {
            result = system->GetDesktopsServices(pServices);
        }

        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return new ComPtr<IADLXDesktopServices>(services);
    }

    private unsafe ComPtr<IADLXDesktopList> GetDesktopListOrSkip(IADLXDesktopServices* services, out IADLXDesktopList* list)
    {
        IADLXDesktopList* local = null;
        var result = services->GetDesktops(&local);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Desktop enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        list = local;
        return new ComPtr<IADLXDesktopList>(local);
    }

    private unsafe void ForEachDesktop(DesktopAction action)
    {
        using var servicesPtr = GetDesktopServicesComPtrOrSkip(out var services);
        using var listPtr = GetDesktopListOrSkip(services, out var list);

        var count = list->Size();
        Skip.If(count == 0, "No desktops returned by ADLX.");

        for (uint i = 0; i < count; i++)
        {
            IADLXDesktop* desktop = null;
            var atResult = list->At(i, &desktop);
            if (atResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                continue;
            Assert.Equal(ADLX_RESULT.ADLX_OK, atResult);

            using var desktopPtr = new ComPtr<IADLXDesktop>(desktop);
            action(services, desktop, i);
        }
    }

    private static bool AssertResultOrContinue(ADLX_RESULT result)
    {
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            return false;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return true;
    }

    private sealed unsafe class DummyDesktopListChangedListener : IDisposable
    {
        private IntPtr _vtable;
        private IntPtr _instance;

        public IADLXDesktopListChangedListener* Pointer => (IADLXDesktopListChangedListener*)_instance;

        public DummyDesktopListChangedListener()
        {
            _vtable = Marshal.AllocHGlobal(IntPtr.Size);
            *((IntPtr*)_vtable) = (IntPtr)(delegate* unmanaged[Stdcall]<IADLXDesktopListChangedListener*, IADLXDesktopList*, byte>)&OnChanged;

            _instance = Marshal.AllocHGlobal(sizeof(IADLXDesktopListChangedListener));
            ((IADLXDesktopListChangedListener*)_instance)->lpVtbl = (void**)_vtable;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static byte OnChanged(IADLXDesktopListChangedListener* self, IADLXDesktopList* list) => 1;

        public void Dispose()
        {
            if (_instance != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_instance);
                _instance = IntPtr.Zero;
            }

            if (_vtable != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_vtable);
                _vtable = IntPtr.Zero;
            }
        }
    }

    private unsafe delegate void DesktopAction(IADLXDesktopServices* services, IADLXDesktop* desktop, uint index);
}
