using Xunit;
using System;
using ADLXWrapper.Bindings; // Use the new bindings project

namespace ADLXWrapper.Tests
{
    public class GpuServicesTests
    {
        [Fact]
        public void EnumerateGPUs_ShouldReturnAtLeastOneGPU()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem sys = null;
            res = ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            IADLXGPUList gpus = null;
            res = sys.GetGPUs(ref gpus);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpus);
            Assert.True(gpus.Size() > 0);

            IADLXGPU gpu = null;
            res = gpus.At(0, ref gpu);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpu);

            string gpuName = "";
            res = gpu.Name(ref gpuName);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.False(string.IsNullOrEmpty(gpuName));

            gpu.Release();
            gpus.Release();
            sys.Release();
            ADLXHelper.Terminate();
        }

        [Fact]
        public void QueryGPU1Interface_ShouldReturnValidInterface()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem sys = null;
            res = ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            IADLXGPUList gpus = null;
            res = sys.GetGPUs(ref gpus);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpus);
            Assert.True(gpus.Size() > 0);

            IADLXGPU gpu = null;
            res = gpus.At(0, ref gpu);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpu);

            IADLXGPU1 gpu1 = null;
            res = gpu.QueryInterface(out gpu1);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpu1);

            gpu1.Release();
            gpu.Release();
            gpus.Release();
            sys.Release();
            ADLXHelper.Terminate();
        }

        [Fact]
        public void QueryGPU2Interface_ShouldReturnValidInterface()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem sys = null;
            res = ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            IADLXGPUList gpus = null;
            res = sys.GetGPUs(ref gpus);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpus);
            Assert.True(gpus.Size() > 0);

            IADLXGPU gpu = null;
            res = gpus.At(0, ref gpu);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpu);

            IADLXGPU2 gpu2 = null;
            res = gpu.QueryInterface(out gpu2);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(gpu2);

            gpu2.Release();
            gpu.Release();
            gpus.Release();
            sys.Release();
            ADLXHelper.Terminate();
        }
    }
}
