using Xunit;
using System;
using ADLXWrapper; // Use the new bindings project

namespace ADLXWrapper.Tests
{
    public class GpuServicesTests : IDisposable
    {
        private readonly ADLXHelper _adlxHelper;
        private readonly IADLXSystem _sys;
        private readonly IADLXGPUList _gpus;

        public GpuServicesTests()
        {
            _adlxHelper = new ADLXHelper();
            _adlxHelper.Initialize();
            _sys = _adlxHelper.GetSystemServices();
            _gpus = _sys.GetGPUs();
        }

        public void Dispose()
        {
            _gpus.Dispose();
            _sys.Dispose();
            _adlxHelper.Terminate();
            _adlxHelper.Dispose();
        }

        [Fact]
        public void EnumerateGPUs_ShouldReturnAtLeastOneGPU()
        {
            Assert.NotNull(_gpus);
            Assert.True(_gpus.Size() > 0);

            SWIGTYPE_p_p_adlx__IADLXGPU gpu_ptr = ADLX.new_gpu_ptr();
            ADLX_RESULT res = _gpus.At(0, gpu_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXGPU gpu = new IADLXGPU(ADLX.gpu_ptr_value(gpu_ptr), true);
            Assert.NotNull(gpu);

            string gpuName = "";
            res = gpu.Name(ref gpuName);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.False(string.IsNullOrEmpty(gpuName));

            gpu.Dispose();
            ADLX.delete_gpu_ptr(gpu_ptr);
        }

        [Fact]
        public void QueryGPU1Interface_ShouldReturnValidInterface()
        {
            SWIGTYPE_p_p_adlx__IADLXGPU gpu_ptr = ADLX.new_gpu_ptr();
            ADLX_RESULT res = _gpus.At(0, gpu_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXGPU gpu = new IADLXGPU(ADLX.gpu_ptr_value(gpu_ptr), true);
            Assert.NotNull(gpu);

            SWIGTYPE_p_p_void gpu1_ptr = ADLX.new_void_ptr();
            res = gpu.QueryInterface(IADLXGPU1.IID(), gpu1_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXGPU1 gpu1 = new IADLXGPU1(ADLX.void_ptr_value(gpu1_ptr), true);
            Assert.NotNull(gpu1);

            gpu1.Dispose();
            gpu.Dispose();
            ADLX.delete_gpu_ptr(gpu_ptr);
            ADLX.delete_void_ptr(gpu1_ptr);
        }

        [Fact]
        public void QueryGPU2Interface_ShouldReturnValidInterface()
        {
            SWIGTYPE_p_p_adlx__IADLXGPU gpu_ptr = ADLX.new_gpu_ptr();
            ADLX_RESULT res = _gpus.At(0, gpu_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXGPU gpu = new IADLXGPU(ADLX.gpu_ptr_value(gpu_ptr), true);
            Assert.NotNull(gpu);

            SWIGTYPE_p_p_void gpu2_ptr = ADLX.new_void_ptr();
            res = gpu.QueryInterface(IADLXGPU2.IID(), gpu2_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXGPU2 gpu2 = new IADLXGPU2(ADLX.void_ptr_value(gpu2_ptr), true);
            Assert.NotNull(gpu2);

            gpu2.Dispose();
            gpu.Dispose();
            ADLX.delete_gpu_ptr(gpu_ptr);
            ADLX.delete_void_ptr(gpu2_ptr);
        }
    }
}
