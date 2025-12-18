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
    public unsafe class GpuServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXGPUList* _gpuList = null;

        public GpuServicesTests(ITestOutputHelper output)
        {
            _output = output;

            if (!ADLXHardwareDetection.HasAMDGPU(out string hwError))
            {
                _hasHardware = false;
                _hasDll = false;
                _skipReason = hwError;
                _output.WriteLine($"??  {hwError}");
                return;
            }
            _hasHardware = true;

            var gpuNames = ADLXHardwareDetection.GetAMDGPUNames();
            if (gpuNames.Length > 0)
            {
                _output.WriteLine($"? AMD GPU detected: {string.Join(", ", gpuNames)}");
            }

            if (!ADLXApiHelper.IsADLXDllAvailable(out string dllError))
            {
                _hasDll = false;
                _skipReason = dllError;
                _output.WriteLine($"??  {dllError}");
                return;
            }
            _hasDll = true;

            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                var system = _system.GetSystemServicesNative();
                IADLXGPUList* gpuList = null;
                var result = system->GetGPUs(&gpuList);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    _gpuList = gpuList;
                }
                else
                {
                    _skipReason = $"GetGPUs failed: {result}";
                }
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
                _output.WriteLine($"??  {_skipReason}");
            }
        }

        public void Dispose()
        {
            if (_gpuList != null) ((IADLXInterface*)_gpuList)->Release();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void EnumerateGPUs_ShouldReturnArray()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null, _skipReason);

            var gpus = ADLXGpuHelpers.EnumerateAllGpus(_system.GetSystemServicesNative()).ToList();

            Assert.NotNull(gpus);
            Assert.NotEmpty(gpus);
            _output.WriteLine($"? Found {gpus.Count} GPU(s)");
        }

        [SkippableFact]
        public void GetBasicInfo_ShouldReturnCompleteInformation()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null || _gpuList == null || _gpuList->Size() == 0, _skipReason);

            IADLXGPU* pGpu = null;
            _gpuList->At(0, &pGpu);
            using var gpu = AdlxInterfaceHandle.From(pGpu, addRef: false);
            var info = ADLXGPUInfo.GetBasicInfo((IntPtr)gpu.As<IADLXGPU>());

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
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null || _gpuList == null || _gpuList->Size() < 2, _skipReason);

            var gpus = ADLXGpuHelpers.EnumerateAllGpus(_system.GetSystemServicesNative()).ToList();
            var uniqueIds = gpus.Select(g => g.UniqueId).ToList();
            var distinctIds = uniqueIds.Distinct().Count();

            Assert.Equal(uniqueIds.Count, distinctIds);
        }
    }
}
