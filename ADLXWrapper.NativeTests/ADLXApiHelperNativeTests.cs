using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

[Collection("NativeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLXApiHelperNativeTests
{
    private readonly NativeSession _session;

    public ADLXApiHelperNativeTests(NativeSessionFixture fixture)
    {
        _session = fixture.Session;
    }

    private static void SkipIfNoAdlxSupport()
    {
        Skip.IfNot(ADLXApiHelper.IsADLXDllAvailable(out var dllError), $"ADLX DLL unavailable: {dllError}");
        Skip.IfNot(ADLXHardwareDetection.HasAMDGPU(out var gpuError), $"AMD GPU not detected: {gpuError}");
    }

    [SkippableFact]
    public void Initialize_and_get_system_services_native()
    {
        SkipIfNoAdlxSupport();

        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)_session.System);
        Assert.NotEqual<ulong>(0, _session.FullVersion);
        Assert.False(string.IsNullOrWhiteSpace(_session.Version));
    }

    [SkippableFact]
    public unsafe void Gpu_list_is_non_empty_native()
    {
        SkipIfNoAdlxSupport();

        IADLXGPUList* gpuList = null;
        var listResult = _session.System->GetGPUs(&gpuList);
        Skip.If(listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, listResult);

        using var gpuListPtr = new ComPtr<IADLXGPUList>(gpuList);
        var count = gpuList->Size();
        Skip.If(count == 0, "No GPUs returned by ADLX.");
    }

    [SkippableFact]
    public unsafe void Gpu_first_entry_has_name_native()
    {
        SkipIfNoAdlxSupport();

        IADLXGPUList* gpuList;
        var listResult = _session.System->GetGPUs(&gpuList);
        Skip.If(listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, listResult);

        using var gpuListPtr = new ComPtr<IADLXGPUList>(gpuList);
        var count = gpuList->Size();
        Skip.If(count == 0, "No GPUs returned by ADLX.");

        IADLXGPU* gpu;
        var gpuResult = gpuList->At(0, &gpu);
        Skip.If(gpuResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU access not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, gpuResult);

        using var gpuPtr = new ComPtr<IADLXGPU>(gpu);
        sbyte* namePtr = null;
        var nameResult = gpu->Name(&namePtr);
        Skip.If(nameResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU name not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, nameResult);

        var name = Marshal.PtrToStringAnsi((IntPtr)namePtr) ?? string.Empty;
        Assert.False(string.IsNullOrWhiteSpace(name));
    }

    [SkippableFact]
    public unsafe void Enumerate_displays_native()
    {
        SkipIfNoAdlxSupport();

        var system = _session.System;
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)system);

        IADLXDisplayServices* displayServices;
        var displayServicesResult = system->GetDisplaysServices(&displayServices);
        Skip.If(displayServicesResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, displayServicesResult);

        using var displayServicesPtr = new ComPtr<IADLXDisplayServices>(displayServices);
        IADLXDisplayList* displayList;
        var listResult = displayServices->GetDisplays(&displayList);
        Skip.If(listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, listResult);

        using var displayListPtr = new ComPtr<IADLXDisplayList>(displayList);
        var count = displayList->Size();
        Skip.If(count == 0, "No displays returned by ADLX.");

        IADLXDisplay* display;
        var displayResult = displayList->At(0, &display);
        Skip.If(displayResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display access not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, displayResult);

        using var displayPtr = new ComPtr<IADLXDisplay>(display);

        sbyte* namePtr = null;
        var nameResult = display->Name(&namePtr);
        Skip.If(nameResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display name not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, nameResult);

        var name = Marshal.PtrToStringAnsi((IntPtr)namePtr) ?? string.Empty;
        Assert.False(string.IsNullOrWhiteSpace(name));

        int width = 0, height = 0;
        var resResult = display->NativeResolution(&width, &height);
        Skip.If(resResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display native resolution not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, resResult);
        Skip.If(width == 0 || height == 0, "Display native resolution not reported.");

        double refreshRate = 0;
        var rrResult = display->RefreshRate(&refreshRate);
        Skip.If(rrResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display refresh rate not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, rrResult);
        Skip.If(refreshRate <= 0, "Display refresh rate not reported.");
    }

    [SkippableFact]
    public unsafe void System_query_interface_system1_native()
    {
        SkipIfNoAdlxSupport();

        IADLXSystem1* system1 = null;
        ADLX_RESULT result;

        var iid = "IADLXSystem1\0";
        fixed (char* iidChars = iid)
        {
            result = _session.System->QueryInterface((ushort*)iidChars, (void**)&system1);
        }

        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE, $"IADLXSystem1 not supported on this hardware/driver: {result}.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        using var sys1Ptr = new ComPtr<IADLXSystem1>(system1);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)system1);
    }

    [SkippableFact]
    public unsafe void System_query_interface_system2_native()
    {
        SkipIfNoAdlxSupport();

        IADLXSystem2* system2 = null;
        ADLX_RESULT result;

        var iid = "IADLXSystem2\0";
        fixed (char* iidChars = iid)
        {
            result = _session.System->QueryInterface((ushort*)iidChars, (void**)&system2);
        }

        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE, $"IADLXSystem2 not supported on this hardware/driver: {result}.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        using var sys2Ptr = new ComPtr<IADLXSystem2>(system2);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)system2);
    }

}
