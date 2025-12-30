using System;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// GPU facade tests.
    /// </summary>
    [SupportedOSPlatform("windows")]
    [Collection("DisplaySerial")]
    public sealed unsafe class ADLXGpuFacadeTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly ADLXGPU? _gpu;
        private readonly string _skipReason = string.Empty;

        public ADLXGpuFacadeTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = _api.GetSystemServices();
                var gpus = _system.EnumerateADLXGPUs();
                if (gpus.Count == 0)
                {
                    _skipReason = "No GPUs found.";
                    return;
                }
                _gpu = gpus[0];
                foreach (var g in gpus.Skip(1)) g.Dispose();
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _skipReason = "GPU services not supported on this system.";
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _gpu?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void GpuInfo_ShouldBePopulated()
        {
            Skip.If(_api == null || _system == null || _gpu == null, _skipReason);
            var info = _gpu.Identity;
            _output.WriteLine($"GPU: {info.Name}, UID: {info.UniqueId}, Vendor: {info.VendorId}, VRAM: {info.TotalVRAM}");
            Assert.NotNull(info.Name);
            Assert.NotEmpty(info.Name);
        }

        [SkippableFact]
        public void CanEnumerateDisplaysAndDesktopsForGpu()
        {
            Skip.If(_api == null || _system == null || _gpu == null, _skipReason);

            try
            {
                var displays = _gpu.EnumerateDisplaysForGPU();
                var desktops = _gpu.EnumerateDesktopsForGPU();
                _output.WriteLine($"Displays on GPU: {displays.Count}, Desktops: {desktops.Count}");
                foreach (var d in displays) d.Dispose();
                foreach (var d in desktops) d.Dispose();
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Display or desktop services are not supported on this system.");
            }
        }
    }
}
