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
}
