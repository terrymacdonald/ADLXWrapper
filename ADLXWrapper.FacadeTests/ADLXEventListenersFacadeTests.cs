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
        Skip.If(displayServices == null, "Display services not supported on this hardware/driver.");
        var handle = displayServices.AddDisplayListEventListener(_ => true);
        Skip.If(handle == null, "Display list listener not supported on this hardware/driver.");
        Assert.NotNull(handle);
        displayServices.RemoveDisplayListEventListener(handle);
    }

    [SkippableFact]
    public void Desktop_list_listener_facade()
    {
        SkipIfUnavailable();
        using var desktopServices = _fixture.System!.GetDesktopServices();
        Skip.If(desktopServices == null, "Desktop services not supported on this hardware/driver.");
        var handle = desktopServices.AddDesktopListEventListener(_ => { });
        Skip.If(handle == null, "Desktop list listener not supported on this hardware/driver.");
        Assert.NotNull(handle);
        desktopServices.RemoveDesktopListEventListener(handle);
    }

    [SkippableFact]
    public void Three_d_settings_listener_facade()
    {
        SkipIfUnavailable();
        using var helper = _fixture.System!.Get3DSettingsServices();
        Skip.If(helper == null, "3D settings services not supported on this hardware/driver.");
        var handle = helper.Add3DSettingsEventListener(_ => true);
        Skip.If(handle == null, "3D settings listener not supported on this hardware/driver.");
        Assert.NotNull(handle);
        helper.Remove3DSettingsEventListener(handle);
    }

    [SkippableFact]
    public void Power_tuning_listener_facade()
    {
        SkipIfUnavailable();
        using var helper = _fixture.System!.GetPowerTuningServices();
        Skip.If(helper == null, "Power tuning services not supported by this ADLX system.");
        var handle = helper.AddPowerTuningEventListener(_ => true);
        Skip.If(handle == null, "Power tuning listener not supported by this ADLX system.");
        Assert.NotNull(handle);
        helper.RemovePowerTuningEventListener(handle);
    }

    [SkippableFact]
    public void Multimedia_listener_facade()
    {
        SkipIfUnavailable();
        using var helper = _fixture.System!.GetMultimediaServices();
        Skip.If(helper == null, "Multimedia services not supported by this ADLX system.");
        var handle = helper.AddMultimediaEventListener(_ => true);
        Skip.If(handle == null, "Multimedia listener not supported by this ADLX system.");
        Assert.NotNull(handle);
        helper.RemoveMultimediaEventListener(handle);
    }
}
