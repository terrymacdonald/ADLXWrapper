using System;
using System.Linq;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Tests for GPU enumeration and property access via ADLXGpuHelpers.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class CoreApiTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXGPUList* _gpuList;

        public CoreApiTests(ITestOutputHelper output)
        {
            _output = output;

            // Stage 1: Check for AMD GPU hardware via PCI
            if (!HardwareDetection.HasAMDGPU(out string hwError))
            {
                _hasHardware = false;
                _hasDll = false;
                _skipReason = hwError;
                _output.WriteLine($"??  {hwError}");
                return;
            }
            _hasHardware = true;

            var gpuNames = HardwareDetection.GetAMDGPUNames();
            if (gpuNames.Length > 0)
            {
                _output.WriteLine($"? AMD GPU detected: {string.Join(", ", gpuNames)}");
            }

            // Stage 2: Check for ADLX DLL availability
            if (!ADLXApi.IsADLXDllAvailable(out string dllError))
            {
                _hasDll = false;
                _skipReason = dllError;
                _output.WriteLine($"??  {dllError}");
                return;
            }
            _hasDll = true;

            // Stage 3: Try to initialize ADLX API
            try
            {
                _api = ADLXApi.Initialize();
                var system = _api.GetSystemServices();
                system->GetGPUs(out _gpuList);

            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
                _output.WriteLine($"??  {_skipReason}");
            }
        }

        public void Dispose()
        {
            if (_gpuList != null) ((IUnknown*)_gpuList)->Release();
            _api?.Dispose();
        }

        [SkippableFact]
        public void EnumerateGPUs_ShouldReturnArray()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            var gpus = ADLXGpuHelpers.EnumerateAllGpus(_api!.GetSystemServices()).ToList();

            Assert.NotNull(gpus);
            Assert.NotEmpty(gpus);
            _output.WriteLine($"? Found {gpus.Length} GPU(s)");
        }

        [SkippableFact]
        public void GetBasicInfo_ShouldReturnCompleteInformation()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _gpuList == null || _gpuList->Size() == 0, _skipReason);

            _gpuList->At(0, out var pGpu);
            using var gpu = new ComPtr<IADLXGPU>(pGpu);
            var info = new GpuInfo(gpu.Get());

            Assert.NotNull(info.Name);
            Assert.NotEmpty(info.Name);
            Assert.NotNull(info.VendorId);
            Assert.NotEmpty(info.VendorId);
            Assert.True(info.TotalVRAM > 0);
            Assert.NotNull(info.VRAMType);
            Assert.NotEmpty(info.VRAMType);
        }

        [SkippableFact]
        public void AllGPUs_ShouldHaveUniqueIds()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _gpuList == null || _gpuList->Size() < 2, _skipReason);

            var gpus = ADLXGpuHelpers.EnumerateAllGpus(_api!.GetSystemServices()).ToList();
            var uniqueIds = gpus.Select(g => g.UniqueId).ToList();
            var distinctIds = uniqueIds.Distinct().Count();

            Assert.Equal(uniqueIds.Count, distinctIds);
        }
    }
}
