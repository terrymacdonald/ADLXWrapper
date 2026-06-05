using System.Linq;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.FacadeTests;

[Collection("FacadeSessionCollection")]
[SupportedOSPlatform("windows")]
public class ADLXMultimediaServicesFacadeTests
{
    private readonly FacadeSessionFixture _fixture;

    public ADLXMultimediaServicesFacadeTests(FacadeSessionFixture fixture)
    {
        _fixture = fixture;
    }

    private void SkipIfUnavailable()
    {
        Skip.If(!string.IsNullOrWhiteSpace(_fixture.SkipReason), _fixture.SkipReason);
    }

    private ADLXMultimediaServicesHelper GetMultimediaOrSkip()
    {
        SkipIfUnavailable();
        var helper = _fixture.System!.GetMultimediaServices();
        Skip.If(helper == null, "Multimedia services not supported on this hardware/driver.");
        return helper!;
    }

    private int GetFirstGpuUniqueIdOrSkip()
    {
        var gpus = _fixture.System!.EnumerateGPUs().ToList();
        Skip.If(gpus.Count == 0, "No GPUs returned by ADLX.");
        return gpus[0].UniqueId;
    }

    [SkippableFact]
    public void Multimedia_video_upscale_facade()
    {
        using var multimedia = GetMultimediaOrSkip();
        var gpuUniqueId = GetFirstGpuUniqueIdOrSkip();
        if (!multimedia.TryGetVideoUpscale(gpuUniqueId, out var info))
            throw new Xunit.SkipException("Video upscale not supported on this GPU.");

        Skip.If(!info.IsSupported, "Video upscale reported unsupported.");
        Assert.True(info.SharpnessRange.MinValue <= info.Sharpness && info.Sharpness <= info.SharpnessRange.MaxValue);
    }

    [SkippableFact]
    public void Multimedia_video_super_resolution_facade()
    {
        using var multimedia = GetMultimediaOrSkip();
        var gpuUniqueId = GetFirstGpuUniqueIdOrSkip();
        if (!multimedia.TryGetVideoSuperResolution(gpuUniqueId, out var info))
            throw new Xunit.SkipException("Video super resolution not supported on this GPU.");

        Skip.If(!info.IsSupported, "Video super resolution reported unsupported.");
        Assert.IsType<bool>(info.IsEnabled);
    }
}
