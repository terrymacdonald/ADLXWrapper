using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public class ADLXApiHelperFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLXApiHelperFacadeTests(FacadeSessionFixture fixture)
    {
        _fixture = fixture;
    }

    private void SkipIfUnavailable()
    {
        Skip.If(!string.IsNullOrWhiteSpace(_fixture.SkipReason), _fixture.SkipReason);
    }

    [SkippableFact]
    public void Initialize_and_get_system_services_facade()
    {
        SkipIfUnavailable();

        Assert.NotNull(_fixture.Api);
        Assert.NotNull(_fixture.System);
        Assert.False(string.IsNullOrWhiteSpace(_fixture.Api!.GetVersion()));
        Assert.NotEqual<ulong>(0, _fixture.Api.GetFullVersion());
    }

    [SkippableFact]
    public void Enumerate_gpus_facade()
    {
        SkipIfUnavailable();

        var system = _fixture.System!;
        IReadOnlyList<ADLXGPU> gpus;

        try
        {
            gpus = system.EnumerateADLXGPUs();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("GPU enumeration not supported on this hardware/driver.");
        }

        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");

        using var gpu = gpus[0];
        Assert.False(string.IsNullOrWhiteSpace(gpu.Name));
        Assert.NotEqual(0, gpu.UniqueId);
        Assert.True(gpu.TotalVRAM > 0);
    }

    [SkippableFact]
    public void Enumerate_displays_facade()
    {
        SkipIfUnavailable();

        var system = _fixture.System!;
        IReadOnlyList<ADLXDisplay> displays;

        try
        {
            displays = system.EnumerateDisplays();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Display enumeration not supported on this hardware/driver.");
        }

        Skip.If(displays.Count == 0, "No displays returned by ADLX.");

        using var display = displays[0];
        Assert.False(string.IsNullOrWhiteSpace(display.Name));
        Assert.True(display.NativeResolutionWidth > 0);
        Assert.True(display.NativeResolutionHeight > 0);
        Assert.True(display.RefreshRate > 0);
    }
}

public sealed class FacadeSessionFixture : IDisposable
{
    public ADLXApiHelper? Api { get; }
    public ADLXSystemServicesHelper? System { get; }
    public string? SkipReason { get; }

    public FacadeSessionFixture()
    {
        if (!ADLXApiHelper.IsADLXDllAvailable(out var dllError))
        {
            SkipReason = $"ADLX DLL unavailable: {dllError}";
            return;
        }

        if (!ADLXHardwareDetection.HasAMDGPU(out var gpuError))
        {
            SkipReason = $"AMD GPU not detected: {gpuError}";
            return;
        }

        try
        {
            Api = ADLXApiHelper.Initialize();
            System = Api.GetSystemServices();
        }
        catch (DllNotFoundException ex)
        {
            SkipReason = $"ADLX DLL load failed: {ex.Message}";
        }
        catch (ADLXException ex)
        {
            SkipReason = $"ADLX initialization failed: {ex.Result} ({ex.Message})";
        }
    }

    public void Dispose()
    {
        System?.Dispose();
        Api?.Dispose();
    }
}

[CollectionDefinition("FacadeSessionCollection")]
public class FacadeSessionCollection : ICollectionFixture<FacadeSessionFixture>
{
    // Intentionally empty; wires fixture to tests.
}
