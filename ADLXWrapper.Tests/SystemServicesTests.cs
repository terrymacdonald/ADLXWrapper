using Xunit;
using System;
using System.Threading;

namespace ADLXWrapper.Tests
{
    public class SystemServicesTests
    {
        [Fact]
        public void ADLX_Initialize_ShouldReturnOK()
        {
            global::ADLX.ADLX_RESULT res = global::ADLX.ADLXInitialize();
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            global::ADLX.ADLXTerminate();
        }

        [Fact]
        public void ADLX_InitializeAndTerminate_ShouldReturnOK()
        {
            global::ADLX.ADLX_RESULT res = global::ADLX.ADLXInitialize();
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            res = global::ADLX.ADLXTerminate();
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
        }

        [Fact]
        public void ADLX_GetSystemServices_ShouldReturnNotNull()
        {
            global::ADLX.ADLX_RESULT res = global::ADLX.ADLXInitialize();
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);

            global::ADLXWrapper.IADLXSystem sys = null;
            res = global::ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            sys.Release();
            global::ADLX.ADLXTerminate();
        }

        [Fact]
        public void ADLX_GetGPUs_ShouldReturnNotNull()
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

            gpus.Release();
            sys.Release();
            global::ADLX.ADLXTerminate();
        }
    }
}
