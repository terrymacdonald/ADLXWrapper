using Xunit;
using System;
using System.Threading;
using ADLXWrapper.Bindings; // Use the new bindings project

namespace ADLXWrapper.Tests
{
    public class SystemServicesTests : IDisposable
    {
        private readonly ADLXHelper _adlxHelper;

        public SystemServicesTests()
        {
            _adlxHelper = new ADLXHelper();
            _adlxHelper.Initialize();
        }

        public void Dispose()
        {
            _adlxHelper.Terminate();
            _adlxHelper.Dispose();
        }

        [Fact]
        public void InitializeAndTerminateADLX_ShouldReturnSuccess()
        {
            // The constructor and Dispose methods already test this.
            // If they fail, the test will fail.
        }

        [Fact]
        public void QuerySystemServices_ShouldReturnValidInterface()
        {
            IADLXSystem sys = _adlxHelper.GetSystemServices();
            Assert.NotNull(sys);
            sys.Dispose();
        }

        [Fact]
        public void QuerySystem1Interface_ShouldReturnValidInterface()
        {
            IADLXSystem sys = _adlxHelper.GetSystemServices();
            Assert.NotNull(sys);

            SWIGTYPE_p_p_void sys1_ptr = ADLX.new_void_ptr();
            ADLX_RESULT res = sys.QueryInterface(IADLXSystem1.IID(), sys1_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem1 sys1 = new IADLXSystem1(ADLX.void_ptr_value(sys1_ptr), true);
            Assert.NotNull(sys1);

            sys1.Dispose();
            sys.Dispose();
            ADLX.delete_void_ptr(sys1_ptr);
        }

        [Fact]
        public void QuerySystem2Interface_ShouldReturnValidInterface()
        {
            IADLXSystem sys = _adlxHelper.GetSystemServices();
            Assert.NotNull(sys);

            SWIGTYPE_p_p_void sys2_ptr = ADLX.new_void_ptr();
            ADLX_RESULT res = sys.QueryInterface(IADLXSystem2.IID(), sys2_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem2 sys2 = new IADLXSystem2(ADLX.void_ptr_value(sys2_ptr), true);
            Assert.NotNull(sys2);

            sys2.Dispose();
            sys.Dispose();
            ADLX.delete_void_ptr(sys2_ptr);
        }
    }
}
