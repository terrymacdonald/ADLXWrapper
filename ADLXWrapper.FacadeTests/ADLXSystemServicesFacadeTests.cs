using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public class ADLXSystemServicesFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLXSystemServicesFacadeTests(FacadeSessionFixture fixture)
    {
        _fixture = fixture;
    }

    private void SkipIfUnavailable()
    {
        Skip.If(!string.IsNullOrWhiteSpace(_fixture.SkipReason), _fixture.SkipReason);
    }

    [SkippableFact]
    public void System_get_display_services_facade()
    {
        SkipIfUnavailable();
        try
        {
            using var helper = _fixture.System!.GetDisplayServices();
            Assert.NotNull(helper);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Display services not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void System_get_desktop_services_facade()
    {
        SkipIfUnavailable();
        try
        {
            using var helper = _fixture.System!.GetDesktopServices();
            Assert.NotNull(helper);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Desktop services not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void System_get_gpu_tuning_services_facade()
    {
        SkipIfUnavailable();
        try
        {
            using var helper = _fixture.System!.GetGPUTuningServices();
            Assert.NotNull(helper);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("GPU tuning services not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void System_get_three_d_settings_services_facade()
    {
        SkipIfUnavailable();
        try
        {
            using var helper = _fixture.System!.Get3DSettingsServices();
            Assert.NotNull(helper);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("3D settings services not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void System_get_multimedia_services_facade()
    {
        SkipIfUnavailable();
        try
        {
            using var helper = _fixture.System!.GetMultimediaServices();
            Assert.NotNull(helper);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Multimedia services not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void System_get_performance_monitoring_services_facade()
    {
        SkipIfUnavailable();
        try
        {
            using var helper = _fixture.System!.GetPerformanceMonitoringServices();
            Assert.NotNull(helper);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Performance monitoring services not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void System_get_power_tuning_services_facade()
    {
        SkipIfUnavailable();
        try
        {
            using var helper = _fixture.System!.GetPowerTuningServices();
            Assert.NotNull(helper);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Power tuning services not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void System_gpu_identity_facade()
    {
        SkipIfUnavailable();
        IReadOnlyList<ADLXGPU> gpus;
        try
        {
            gpus = _fixture.System!.EnumerateADLXGPUs();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("GPU enumeration not supported on this hardware/driver.");
        }

        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        using var gpu = gpus[0];
        Assert.False(string.IsNullOrWhiteSpace(gpu.Name));
        Assert.False(string.IsNullOrWhiteSpace(gpu.VendorId));
        Assert.True(gpu.UniqueId != 0);
        Assert.True(gpu.TotalVRAM >= 0);
        Assert.False(string.IsNullOrWhiteSpace(gpu.VRAMType));
    }

    [SkippableFact]
    public void System_gpu_identity_deep_facade()
    {
        SkipIfUnavailable();
        IReadOnlyList<ADLXGPU> gpus;
        try
        {
            gpus = _fixture.System!.EnumerateADLXGPUs();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("GPU enumeration not supported on this hardware/driver.");
        }

        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        using var gpu = gpus[0];
        Assert.True(Enum.IsDefined(typeof(ADLX_GPU_TYPE), gpu.GPUType));
        Assert.True(Enum.IsDefined(typeof(ADLX_ASIC_FAMILY_TYPE), gpu.AsicFamilyType));
        Assert.True(Enum.IsDefined(typeof(ADLX_PCI_BUS_TYPE), gpu.PciBusType));
        Assert.True(gpu.PciBusLaneWidth >= 0);
        Assert.True(Enum.IsDefined(typeof(ADLX_MGPU_MODE), gpu.MultiGpuMode));
        Assert.NotNull(gpu.ProductName);
        Assert.NotNull(gpu.SubSystemId);
        Assert.NotNull(gpu.SubSystemVendorId);
        Assert.NotNull(gpu.RevisionId);
        Assert.NotNull(gpu.DriverVersion);
        Assert.NotNull(gpu.AMDSoftwareVersion);
        Assert.NotNull(gpu.AMDWindowsDriverVersion);

        var hasDriverStrings = !string.IsNullOrEmpty(gpu.DriverVersion) ||
                               !string.IsNullOrEmpty(gpu.AMDSoftwareVersion) ||
                               !string.IsNullOrEmpty(gpu.AMDWindowsDriverVersion);
        var hasLuid = gpu.Luid.lowPart != 0 || gpu.Luid.highPart != 0;

        Skip.If(!hasDriverStrings && !hasLuid, "Driver/LUID identity not exposed by this ADLX/driver version.");

        if (hasLuid)
        {
            Assert.True(gpu.Luid.lowPart != 0 || gpu.Luid.highPart != 0);
        }
    }

    [SkippableFact]
    public void System_display_identity_facade()
    {
        SkipIfUnavailable();
        IReadOnlyList<ADLXDisplay> displays;
        try
        {
            displays = _fixture.System!.EnumerateDisplays();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Display enumeration not supported on this hardware/driver.");
        }

        Skip.If(displays.Count == 0, "No displays returned by ADLX.");
        using var display = displays[0];
        Assert.False(string.IsNullOrWhiteSpace(display.Name));
        Assert.False(string.IsNullOrWhiteSpace(display.Edid));
        Assert.True(display.NativeResolutionWidth > 0);
        Assert.True(display.NativeResolutionHeight > 0);
        Assert.True(display.RefreshRate > 0);
        Assert.NotEqual<uint>(0, display.ManufacturerId);
    }

    [SkippableFact]
    public void System_display_identity_extras_facade()
    {
        SkipIfUnavailable();
        IReadOnlyList<ADLXDisplay> displays;
        try
        {
            displays = _fixture.System!.EnumerateDisplays();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Display enumeration not supported on this hardware/driver.");
        }

        Skip.If(displays.Count == 0, "No displays returned by ADLX.");
        using var display = displays[0];
        Assert.True(Enum.IsDefined(typeof(ADLX_DISPLAY_TYPE), display.Type));
        Assert.True(Enum.IsDefined(typeof(ADLX_DISPLAY_CONNECTOR_TYPE), display.ConnectorType));
        Assert.True(Enum.IsDefined(typeof(ADLX_DISPLAY_SCAN_TYPE), display.ScanType));
        Assert.NotEqual(0, display.GpuUniqueId);
    }
}
