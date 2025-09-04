using Xunit;
using System;
using ADLXWrapper;

namespace ADLXWrapper.Tests
{
    public class GpuServicesTests
    {
        [Fact]
        public void ADLX_GetGPUs_ShouldReturnAtLeastOneGPU()
        {
            global::ADLX.ADLX_RESULT res = global::ADLX.ADLXInitialize();
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);

            global::ADLXWrapper.IADLXSystem sys = null;
            res = global::ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            global::ADLXWrapper.IADLXGPUList gpus = null;
            res = sys.GetGPUs(ref gpus);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpus);
            Assert.True(gpus.Size() > 0);

            global::ADLXWrapper.IADLXGPU gpu = null;
            res = gpus.At(0, ref gpu);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpu);

            string gpuName = "";
            res = gpu.Name(ref gpuName);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.False(string.IsNullOrEmpty(gpuName));

            gpu.Release();
            gpus.Release();
            sys.Release();
            global::ADLX.ADLXTerminate();
        }

        [Fact]
        public void ADLX_GetGPUDetails_ShouldReturnValidDetails()
        {
            global::ADLX.ADLX_RESULT res = global::ADLX.ADLXInitialize();
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);

            global::ADLXWrapper.IADLXSystem sys = null;
            res = global::ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            global::ADLXWrapper.IADLXGPUList gpus = null;
            res = sys.GetGPUs(ref gpus);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpus);
            Assert.True(gpus.Size() > 0);

            global::ADLXWrapper.IADLXGPU gpu = null;
            res = gpus.At(0, ref gpu);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpu);

            string name = "";
            res = gpu.Name(ref name);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.False(string.IsNullOrEmpty(name));

            string driverPath = "";
            res = gpu.DriverPath(ref driverPath);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.False(string.IsNullOrEmpty(driverPath));

            uint vendorId = 0;
            res = gpu.VendorId(ref vendorId);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.True(vendorId > 0);

            gpu.Release();
            gpus.Release();
            sys.Release();
            global::ADLX.ADLXTerminate();
        }
    }
}
