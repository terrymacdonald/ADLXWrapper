using Xunit;
using System;
using System.Threading;
using ADLXWrapper; // Use the new bindings project

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
            IADLXSystem system = _adlxHelper.GetSystemServices();
            Assert.NotNull(system);
            system.Dispose();
        }

        [Fact]
        public void QuerySystem1Interface_ShouldReturnValidInterface()
        {
            IADLXSystem system = _adlxHelper.GetSystemServices();
            Assert.NotNull(system);            

            bool supportsSystem1 = ADLX.SupportsSystem1Interface(system);
            if (supportsSystem1)
            {
                IADLXSystem1 system1 = ADLX.QuerySystem1Interface(system);
                Assert.NotNull(system1);
                system1.Dispose();

            }
            system.Dispose();
        }

        [Fact]
        public void QuerySystem2Interface_ShouldReturnValidInterface()
        {
            IADLXSystem system = _adlxHelper.GetSystemServices();
            Assert.NotNull(system);

            bool supportsSystem2 = ADLX.SupportsSystem2Interface(system);
            if (supportsSystem2)
            {
                IADLXSystem2 system2 = ADLX.QuerySystem2Interface(system);
                Assert.NotNull(system2);
                system2.Dispose();

            }
            system.Dispose();
        }
    }
}
