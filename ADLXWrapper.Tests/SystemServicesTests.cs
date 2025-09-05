using Xunit;
using System;
using System.Threading;
using ADLXWrapper.Bindings; // Use the new bindings project

namespace ADLXWrapper.Tests
{
    public class SystemServicesTests
    {
        [Fact]
        public void InitializeAndTerminateADLX_ShouldReturnSuccess()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            res = ADLXHelper.Terminate();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
        }

        [Fact]
        public void QuerySystemServices_ShouldReturnValidInterface()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem sys = null;
            res = ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            sys.Release();
            ADLXHelper.Terminate();
        }

        [Fact]
        public void QuerySystem1Interface_ShouldReturnValidInterface()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem sys = null;
            res = ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            IADLXSystem1 sys1 = null;
            res = sys.QueryInterface(out sys1);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys1);

            sys1.Release();
            sys.Release();
            ADLXHelper.Terminate();
        }

        [Fact]
        public void QuerySystem2Interface_ShouldReturnValidInterface()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem sys = null;
            res = ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            IADLXSystem2 sys2 = null;
            res = sys.QueryInterface(out sys2);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys2);

            sys2.Release();
            sys.Release();
            ADLXHelper.Terminate();
        }
    }
}
