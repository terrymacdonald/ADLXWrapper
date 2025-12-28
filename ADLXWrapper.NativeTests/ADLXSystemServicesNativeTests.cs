using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

[Collection("NativeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLXSystemServicesNativeTests
{
    private readonly NativeSession _session;

    public ADLXSystemServicesNativeTests(NativeSessionFixture fixture)
    {
        _session = fixture.Session;
    }

    [SkippableFact]
    public void System_get_display_services_native()
    {
        SkipIfNoAdlxSupport();

        IADLXDisplayServices* displayServices = null;
        var result = _session.System->GetDisplaysServices(&displayServices);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLXDisplayServices>(displayServices);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)displayServices);
    }

    [SkippableFact]
    public void System_get_performance_monitoring_services_native()
    {
        SkipIfNoAdlxSupport();

        IADLXPerformanceMonitoringServices* services = null;
        var result = _session.System->GetPerformanceMonitoringServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Performance monitoring services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLXPerformanceMonitoringServices>(services);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)services);
    }

    [SkippableFact]
    public void System1_get_power_tuning_services_native()
    {
        SkipIfNoAdlxSupport();

        if (!TryGetSystem1(out var system1, out var skipReason))
        {
            Skip.If(true, skipReason);
        }

        IADLXPowerTuningServices* services = null;
        var result = system1->GetPowerTuningServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Power tuning services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLXPowerTuningServices>(services);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)services);
    }

    [SkippableFact]
    public void System2_get_multimedia_services_native()
    {
        SkipIfNoAdlxSupport();

        if (!TryGetSystem2(out var system2, out var skipReason))
        {
            Skip.If(true, skipReason);
        }

        IADLXMultimediaServices* services = null;
        var result = system2->GetMultimediaServices(&services);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Multimedia services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var servicesPtr = new ComPtr<IADLXMultimediaServices>(services);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)services);
    }

    [SkippableFact]
    public void System2_get_gpu_apps_list_changed_handling_native()
    {
        SkipIfNoAdlxSupport();

        if (!TryGetSystem2(out var system2, out var skipReason))
        {
            Skip.If(true, skipReason);
        }

        IADLXGPUAppsListChangedHandling* handling = null;
        var result = system2->GetGPUAppsListChangedHandling(&handling);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU apps list changed handling not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var handlingPtr = new ComPtr<IADLXGPUAppsListChangedHandling>(handling);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)handling);
    }

    private void SkipIfNoAdlxSupport()
    {
        Skip.IfNot(ADLXApiHelper.IsADLXDllAvailable(out var dllError), $"ADLX DLL unavailable: {dllError}");
        Skip.IfNot(ADLXHardwareDetection.HasAMDGPU(out var gpuError), $"AMD GPU not detected: {gpuError}");
    }

    private unsafe bool TryGetSystem1(out IADLXSystem1* system1, out string skipReason)
    {
        system1 = null;
        skipReason = string.Empty;

        // Try friendly helper first to avoid hand-building IID strings
        if (ADLXUtils.TryQueryInterface((IntPtr)_session.System, nameof(IADLXSystem1), out var ifacePtr) && ifacePtr != IntPtr.Zero)
        {
            system1 = (IADLXSystem1*)ifacePtr;
            return true;
        }

        // Fall back to explicit QueryInterface to capture the ADLX_RESULT for skip vs fail handling
        ADLX_RESULT result;
        void* queried = null;
        var iidTerminated = nameof(IADLXSystem1) + "\0";
        fixed (char* iidChars = iidTerminated)
        {
            result = _session.System->QueryInterface((ushort*)iidChars, &queried);
        }

        if (result == ADLX_RESULT.ADLX_OK && queried != null)
        {
            system1 = (IADLXSystem1*)queried;
            return true;
        }

        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE)
        {
            skipReason = $"IADLXSystem1 not supported on this hardware/driver: {result}.";
            system1 = null;
            return false;
        }

        throw new ADLXException(result, "QueryInterface for IADLXSystem1 failed.");
    }

    private unsafe bool TryGetSystem2(out IADLXSystem2* system2, out string skipReason)
    {
        system2 = null;
        skipReason = string.Empty;

        // Try friendly helper first to avoid hand-building IID strings
        if (ADLXUtils.TryQueryInterface((IntPtr)_session.System, nameof(IADLXSystem2), out var ifacePtr) && ifacePtr != IntPtr.Zero)
        {
            system2 = (IADLXSystem2*)ifacePtr;
            return true;
        }

        // Fall back to explicit QueryInterface to capture the ADLX_RESULT for skip vs fail handling
        ADLX_RESULT result;
        void* queried = null;
        var iidTerminated = nameof(IADLXSystem2) + "\0";
        fixed (char* iidChars = iidTerminated)
        {
            result = _session.System->QueryInterface((ushort*)iidChars, &queried);
        }

        if (result == ADLX_RESULT.ADLX_OK && queried != null)
        {
            system2 = (IADLXSystem2*)queried;
            return true;
        }

        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE)
        {
            skipReason = $"IADLXSystem2 not supported on this hardware/driver: {result}.";
            system2 = null;
            return false;
        }

        throw new ADLXException(result, "QueryInterface for IADLXSystem2 failed.");
    }
}
