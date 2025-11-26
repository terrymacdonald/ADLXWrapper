using Xunit;
using System;
using ADLXWrapper;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Basic API tests for ADLX wrapper
    /// Tests initialization, version queries, and cleanup
    /// </summary>
    public class BasicApiTests : IClassFixture<ADLXTestFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXTestFixture _fixture;

        public BasicApiTests(ITestOutputHelper output, ADLXTestFixture fixture)
        {
            _output = output;
            _fixture = fixture;
            
            // Write diagnostics once per test class instance
            _fixture.WriteDiagnostics(_output);
        }

        [SkippableFact]
        public void Initialize_ShouldSucceed()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            Assert.NotNull(_fixture.Api);
            _output.WriteLine("? ADLXApi instance is not null");
        }

        [SkippableFact]
        public void GetVersion_ShouldReturnValidVersion()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var version = _fixture.Api!.GetVersion();
            
            Assert.NotNull(version);
            Assert.NotEmpty(version);
            _output.WriteLine($"? ADLX Version: {version}");
        }

        [SkippableFact]
        public void GetFullVersion_ShouldReturnNonZero()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var fullVersion = _fixture.Api!.GetFullVersion();
            
            Assert.NotEqual(0UL, fullVersion);
            _output.WriteLine($"? ADLX Full Version: {fullVersion}");
        }

        [SkippableFact]
        public void GetSystemServices_ShouldReturnValidPointer()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var pSystemServices = _fixture.Api!.GetSystemServices();
            
            Assert.NotEqual(IntPtr.Zero, pSystemServices);
            _output.WriteLine($"? System services pointer: 0x{pSystemServices:X}");
        }

        [SkippableFact]
        public void Dispose_ShouldNotThrow()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            // Create a new instance to test disposal
            using var testApi = ADLXApi.Initialize();
            var exception = Record.Exception(() => testApi.Dispose());
            
            Assert.Null(exception);
            _output.WriteLine("? Dispose completed without exception");
        }

        [SkippableFact]
        public void DisposeMultipleTimes_ShouldBeIdempotent()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            // Create a new instance to test multiple disposals
            var testApi = ADLXApi.Initialize();
            
            // Multiple dispose calls should be safe
            testApi.Dispose();
            testApi.Dispose();
            testApi.Dispose();
            
            _output.WriteLine("? Multiple Dispose calls handled correctly");
        }

        [SkippableFact]
        public void AfterDispose_MethodsShouldThrowObjectDisposedException()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var testApi = ADLXApi.Initialize();
            testApi.Dispose();

            // After dispose, methods should throw ObjectDisposedException
            Assert.Throws<ObjectDisposedException>(() => testApi.GetVersion());
            Assert.Throws<ObjectDisposedException>(() => testApi.GetFullVersion());
            Assert.Throws<ObjectDisposedException>(() => testApi.GetSystemServices());
            Assert.Throws<ObjectDisposedException>(() => testApi.EnumerateGPUs());
            
            _output.WriteLine("? Methods correctly throw ObjectDisposedException after disposal");
        }

        [SkippableFact]
        public void UsingStatement_ShouldAutomaticallyDispose()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            ADLXApi? testApi = null;
            
            using (testApi = ADLXApi.Initialize())
            {
                Assert.NotNull(testApi);
                var version = testApi.GetVersion();
                Assert.NotEmpty(version);
                _output.WriteLine($"? Inside using block, version: {version}");
            }

            // After using block, should be disposed
            Assert.Throws<ObjectDisposedException>(() => testApi!.GetVersion());
            _output.WriteLine("? Using statement correctly disposed the instance");
        }

        [SkippableFact]
        public void InitializeMultipleTimes_ShouldReturnSeparateInstances()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            using var api1 = ADLXApi.Initialize();
            using var api2 = ADLXApi.Initialize();

            Assert.NotNull(api1);
            Assert.NotNull(api2);
            Assert.NotSame(api1, api2);
            
            _output.WriteLine("? Multiple Initialize calls return separate instances");
        }
    }
}
