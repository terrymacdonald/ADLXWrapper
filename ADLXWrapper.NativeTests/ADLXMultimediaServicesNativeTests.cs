using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

[Collection("NativeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLXMultimediaServicesNativeTests
{
    private readonly NativeSession _session;

    public ADLXMultimediaServicesNativeTests(NativeSessionFixture fixture)
    {
        _session = fixture.Session;
    }

    [SkippableFact]
    public void Multimedia_services_acquire_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetMultimediaServicesComPtrOrSkip(out _);
    }

    [SkippableFact]
    public void Multimedia_services_query_interface_via_system2_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetMultimediaServicesComPtrOrSkip(out var services);
        QueryInterfaceOrSkip((IADLXInterface*)services, nameof(IADLXMultimediaServices));
    }

    [SkippableFact]
    public void Multimedia_video_upscale_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetMultimediaServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLXVideoUpscale* upscale = null;
            var result = services->GetVideoUpscale(gpu, &upscale);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return;

            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var upscalePtr = new ComPtr<IADLXVideoUpscale>(upscale);

            bool supported = false;
            if (!AssertResultOrContinue(upscale->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(upscale->IsEnabled(&enabled));

            ADLX_IntRange range = default;
            AssertResultOrContinue(upscale->GetSharpnessRange(&range));

            int sharpness = 0;
            AssertResultOrContinue(upscale->GetSharpness(&sharpness));
        });
    }

    [SkippableFact]
    public void Multimedia_video_super_resolution_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetMultimediaServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            IADLXVideoSuperResolution* vsr = null;
            var result = services->GetVideoSuperResolution(gpu, &vsr);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                return;

            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            using var vsrPtr = new ComPtr<IADLXVideoSuperResolution>(vsr);

            bool supported = false;
            if (!AssertResultOrContinue(vsr->IsSupported(&supported)) || !supported)
                return;

            bool enabled = false;
            AssertResultOrContinue(vsr->IsEnabled(&enabled));
        });
    }

    [SkippableFact]
    public void Multimedia_change_listener_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetMultimediaServicesComPtrOrSkip(out var services);

        IADLXMultimediaChangedHandling* handling = null;
        var result = services->GetMultimediaChangedHandling(&handling);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Multimedia changed handling not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var handlingPtr = new ComPtr<IADLXMultimediaChangedHandling>(handling);
        using var listener = new DummyMultimediaChangedListener();

        var addResult = handling->AddMultimediaEventListener(listener.Pointer);
        Skip.If(addResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Multimedia change listeners not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, addResult);

        var removeResult = handling->RemoveMultimediaEventListener(listener.Pointer);
        Assert.Equal(ADLX_RESULT.ADLX_OK, removeResult);
    }

    private void SkipIfNoAdlxSupport()
    {
        Skip.IfNot(ADLXApiHelper.IsADLXDllAvailable(out var dllError), $"ADLX DLL unavailable: {dllError}");
        Skip.IfNot(ADLXHardwareDetection.HasAMDGPU(out var gpuError), $"AMD GPU not detected: {gpuError}");
    }

    private unsafe ComPtr<IADLXMultimediaServices> GetMultimediaServicesComPtrOrSkip(out IADLXMultimediaServices* services)
    {
        services = null;
        IADLXSystem2* system2 = null;
        var qi = QueryInterface((IADLXInterface*)_session.System, nameof(IADLXSystem2), (void**)&system2);
        Skip.If(qi == ADLX_RESULT.ADLX_NOT_SUPPORTED || qi == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE, "IADLXSystem2 not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, qi);
        using var system2Ptr = new ComPtr<IADLXSystem2>(system2);

        IADLXMultimediaServices* local = null;
        var result = system2->GetMultimediaServices(&local);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Multimedia services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        services = local;
        return new ComPtr<IADLXMultimediaServices>(local);
    }

    private unsafe ComPtr<IADLXGPUList> GetGpuListOrSkip(out IADLXGPUList* list)
    {
        IADLXGPUList* local = null;
        var result = _session.System->GetGPUs(&local);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        list = local;
        return new ComPtr<IADLXGPUList>(local);
    }

    private static bool AssertResultOrContinue(ADLX_RESULT result)
    {
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE)
            return false;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return true;
    }

    private static unsafe ADLX_RESULT QueryInterface(IADLXInterface* iface, string name, void** obj)
    {
        var terminated = name + "\0";
        fixed (char* chars = terminated)
        {
            return iface->QueryInterface((ushort*)chars, obj);
        }
    }

    private static unsafe void QueryInterfaceOrSkip(IADLXInterface* iface, string name)
    {
        void* queried = null;
        var result = QueryInterface(iface, name, &queried);
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE)
            return;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)queried);
        ((IADLXInterface*)queried)->Release();
    }

    private unsafe delegate void GpuVisitor(IADLXGPU* gpu);

    private unsafe void ForEachGpu(IADLXGPUList* list, GpuVisitor visitor)
    {
        var count = list->Size();
        Skip.If(count == 0, "No GPUs returned by ADLX.");

        for (uint i = 0; i < count; i++)
        {
            IADLXGPU* gpu = null;
            var gpuResult = list->At(i, &gpu);
            if (gpuResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                continue;

            Assert.Equal(ADLX_RESULT.ADLX_OK, gpuResult);
            using var gpuPtr = new ComPtr<IADLXGPU>(gpu);
            visitor(gpu);
        }
    }

    private sealed unsafe class DummyMultimediaChangedListener : IDisposable
    {
        private IntPtr _vtable;
        private IntPtr _instance;

        public IADLXMultimediaChangedEventListener* Pointer => (IADLXMultimediaChangedEventListener*)_instance;

        public DummyMultimediaChangedListener()
        {
            _vtable = Marshal.AllocHGlobal(IntPtr.Size);
            *((IntPtr*)_vtable) = (IntPtr)(delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEventListener*, IADLXMultimediaChangedEvent*, byte>)&OnChanged;

            _instance = Marshal.AllocHGlobal(sizeof(IADLXMultimediaChangedEventListener));
            ((IADLXMultimediaChangedEventListener*)_instance)->lpVtbl = (void**)_vtable;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static byte OnChanged(IADLXMultimediaChangedEventListener* self, IADLXMultimediaChangedEvent* evt) => 1;

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
}
