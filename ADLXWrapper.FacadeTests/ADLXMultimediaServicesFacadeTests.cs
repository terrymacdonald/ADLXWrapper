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
        try
        {
            return _fixture.System!.GetMultimediaServices();
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            throw new Xunit.SkipException("Multimedia services not supported on this hardware/driver.");
        }
    }

    private unsafe ADLXInterfaceHandle GetFirstGpuHandleOrSkip()
    {
        var handles = _fixture.System!.EnumerateGPUsHandle();
        Skip.If(handles.Length == 0, "No GPUs returned by ADLX.");
        return handles[0];
    }

    [SkippableFact]
    public void Multimedia_video_upscale_facade()
    {
        using var multimedia = GetMultimediaOrSkip();
        unsafe
        {
            using var gpuHandle = GetFirstGpuHandleOrSkip();
            if (!multimedia.TryGetVideoUpscale(gpuHandle.As<IADLXGPU>(), out var info))
                throw new Xunit.SkipException("Video upscale not supported on this GPU.");

            Skip.If(!info.IsSupported, "Video upscale reported unsupported.");
            Assert.True(info.SharpnessRange.minValue <= info.Sharpness && info.Sharpness <= info.SharpnessRange.maxValue);
        }
    }

    [SkippableFact]
    public void Multimedia_video_super_resolution_facade()
    {
        using var multimedia = GetMultimediaOrSkip();
        unsafe
        {
            using var gpuHandle = GetFirstGpuHandleOrSkip();
            if (!multimedia.TryGetVideoSuperResolution(gpuHandle.As<IADLXGPU>(), out var info))
                throw new Xunit.SkipException("Video super resolution not supported on this GPU.");

            Skip.If(!info.IsSupported, "Video super resolution reported unsupported.");
            Assert.IsType<bool>(info.IsEnabled);
        }
    }
}
