using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLXEventListenersFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLXEventListenersFacadeTests(FacadeSessionFixture fixture)
    {
        _fixture = fixture;
    }

    private void SkipIfUnavailable()
    {
        Skip.If(!string.IsNullOrWhiteSpace(_fixture.SkipReason), _fixture.SkipReason);
    }

    [SkippableFact]
    public void Display_list_listener_facade()
    {
        SkipIfUnavailable();
        using var displayServices = _fixture.System!.GetDisplayServices();
        try
        {
            var handle = displayServices.AddDisplayListEventListener(_ => true);
            Assert.NotNull(handle);
            displayServices.RemoveDisplayListEventListener(handle);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Display list listener not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void Desktop_list_listener_facade()
    {
        SkipIfUnavailable();
        using var desktopServices = _fixture.System!.GetDesktopServices();
        try
        {
            var handle = desktopServices.AddDesktopListEventListener(_ => { });
            Assert.NotNull(handle);
            desktopServices.RemoveDesktopListEventListener(handle);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Desktop list listener not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void Three_d_settings_listener_facade()
    {
        SkipIfUnavailable();
        using var helper = _fixture.System!.Get3DSettingsServices();
        try
        {
            var handle = helper.Add3DSettingsEventListener(_ => true);
            Assert.NotNull(handle);
            helper.Remove3DSettingsEventListener(handle);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("3D settings listener not supported on this hardware/driver.");
        }
    }

    [SkippableFact]
    public void Power_tuning_listener_facade()
    {
        SkipIfUnavailable();
        try
        {
            using var helper = _fixture.System!.GetPowerTuningServices();
            var handle = helper.AddPowerTuningEventListener(_ => true);
            Assert.NotNull(handle);
            helper.RemovePowerTuningEventListener(handle);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Power tuning listener not supported by this ADLX system.");
        }
    }

    [SkippableFact]
    public void Multimedia_listener_facade()
    {
        SkipIfUnavailable();
        try
        {
            using var helper = _fixture.System!.GetMultimediaServices();
            var handle = helper.AddMultimediaEventListener(_ => true);
            Assert.NotNull(handle);
            helper.RemoveMultimediaEventListener(handle);
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Multimedia listener not supported by this ADLX system.");
        }
    }
}
