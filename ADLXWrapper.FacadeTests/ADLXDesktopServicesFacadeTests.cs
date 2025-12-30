using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public class ADLXDesktopServicesFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLXDesktopServicesFacadeTests(FacadeSessionFixture fixture)
    {
        _fixture = fixture;
    }

    private void SkipIfUnavailable()
    {
        Skip.If(!string.IsNullOrWhiteSpace(_fixture.SkipReason), _fixture.SkipReason);
    }

    private IReadOnlyList<ADLXDesktop> GetDesktopsOrSkip()
    {
        SkipIfUnavailable();
        try
        {
            var desktops = _fixture.System!.EnumerateDesktops();
            Skip.If(desktops.Count == 0, "No desktops returned by ADLX.");
            return desktops;
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Desktop services not supported on this hardware/driver.");
        }
    }

    private static void DisposeRest(IReadOnlyList<ADLXDesktop> desktops, int startAt = 1)
    {
        for (int i = startAt; i < desktops.Count; i++)
        {
            desktops[i].Dispose();
        }
    }

    [SkippableFact]
    public void Desktops_enumerate_facade()
    {
        var desktops = GetDesktopsOrSkip();
        using var first = desktops[0];
        DisposeRest(desktops);

        Assert.False(first.Identity.Width == 0 && first.Identity.Height == 0);
    }

    [SkippableFact]
    public void Desktop_identity_facade()
    {
        var desktops = GetDesktopsOrSkip();
        using var desktop = desktops[0];
        DisposeRest(desktops);

        Assert.True(desktop.Width > 0);
        Assert.True(desktop.Height > 0);
        Assert.True(desktop.TopLeftX != 0 || desktop.TopLeftY != 0 || desktop.Width > 0 || desktop.Height > 0);
    }

    [SkippableFact]
    public void Desktop_displays_facade()
    {
        var desktops = GetDesktopsOrSkip();
        using var desktop = desktops[0];
        DisposeRest(desktops);

        IReadOnlyList<ADLXDisplay> displays;
        try
        {
            displays = desktop.EnumerateDisplaysForDesktop();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Display enumeration for desktop not supported.");
        }

        Skip.If(displays.Count == 0, "No displays on this desktop.");

        using var firstDisplay = displays[0];
        for (int i = 1; i < displays.Count; i++) displays[i].Dispose();
        Assert.True(firstDisplay.NativeResolutionWidth > 0);
    }

    [SkippableFact]
    public void Desktop_display_infos_facade()
    {
        var desktops = GetDesktopsOrSkip();
        using var desktop = desktops[0];
        DisposeRest(desktops);

        var infos = desktop.EnumerateDisplayInfosForDesktop();
        Skip.If(infos.Count == 0, "No display infos on this desktop.");
        Assert.True(infos.All(i => i.NativeResolutionWidth > 0 && i.NativeResolutionHeight > 0));
    }

    [SkippableFact]
    public void Desktop_gpu_resolution_facade()
    {
        var desktops = GetDesktopsOrSkip();
        using var desktop = desktops[0];
        DisposeRest(desktops);

        ADLXGPU gpu;
        try
        {
            gpu = desktop.GetGPU();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Resolving GPU for desktop not supported.");
        }

        using (gpu)
        {
            Assert.False(string.IsNullOrWhiteSpace(gpu.Name));
        }
    }
}
