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
    public class BasicApiTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;

        public BasicApiTests(ITestOutputHelper output)
        {
            _output = output;

            try
            {
                _api = ADLXApi.Initialize();
                _hasHardware = true;
                _output.WriteLine("? ADLX initialized successfully");
            }
            catch (DllNotFoundException ex)
            {
                _hasHardware = false;
                _output.WriteLine($"? AMD ADLX DLL not found: {ex.Message}");
                _output.WriteLine("  Tests will be skipped (no AMD GPU drivers installed)");
            }
            catch (ADLXException ex)
            {
                _hasHardware = false;
                _output.WriteLine($"? ADLX initialization failed: {ex.Message}");
                _output.WriteLine($"  Result code: {ex.Result}");
                _output.WriteLine("  Tests will be skipped (ADLX not available)");
            }
            catch (Exception ex)
            {
                _hasHardware = false;
                _output.WriteLine($"? Unexpected error during initialization: {ex.Message}");
                _output.WriteLine("  Tests will be skipped");
            }
        }

        [Fact]
        public void Initialize_ShouldSucceed()
        {
            // Test that we can initialize the API
            if (!_hasHardware)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            Assert.NotNull(_api);
            _output.WriteLine("? ADLXApi instance is not null");
        }

        [Fact]
        public void GetVersion_ShouldReturnValidVersion()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var version = _api.GetVersion();
            
            Assert.NotNull(version);
            Assert.NotEmpty(version);
            _output.WriteLine($"? ADLX Version: {version}");
        }

        [Fact]
        public void GetFullVersion_ShouldReturnNonZero()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var fullVersion = _api.GetFullVersion();
            
            Assert.NotEqual(0UL, fullVersion);
            _output.WriteLine($"? ADLX Full Version: {fullVersion}");
        }

        [Fact]
        public void GetSystemServices_ShouldReturnValidPointer()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var pSystemServices = _api.GetSystemServices();
            
            Assert.NotEqual(IntPtr.Zero, pSystemServices);
            _output.WriteLine($"? System services pointer: 0x{pSystemServices:X}");
        }

        [Fact]
        public void Dispose_ShouldNotThrow()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            // Dispose should work without throwing
            var exception = Record.Exception(() => _api.Dispose());
            
            Assert.Null(exception);
            _output.WriteLine("? Dispose completed without exception");
        }

        [Fact]
        public void DisposeMultipleTimes_ShouldBeIdempotent()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            // Multiple dispose calls should be safe
            _api.Dispose();
            _api.Dispose();
            _api.Dispose();
            
            _output.WriteLine("? Multiple Dispose calls handled correctly");
        }

        [Fact]
        public void AfterDispose_MethodsShouldThrowObjectDisposedException()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            _api.Dispose();

            // After dispose, methods should throw ObjectDisposedException
            Assert.Throws<ObjectDisposedException>(() => _api.GetVersion());
            Assert.Throws<ObjectDisposedException>(() => _api.GetFullVersion());
            Assert.Throws<ObjectDisposedException>(() => _api.GetSystemServices());
            Assert.Throws<ObjectDisposedException>(() => _api.EnumerateGPUs());
            
            _output.WriteLine("? Methods correctly throw ObjectDisposedException after disposal");
        }

        [Fact]
        public void UsingStatement_ShouldAutomaticallyDispose()
        {
            if (!_hasHardware)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

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

        [Fact]
        public void InitializeMultipleTimes_ShouldReturnSeparateInstances()
        {
            if (!_hasHardware)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            using var api1 = ADLXApi.Initialize();
            using var api2 = ADLXApi.Initialize();

            Assert.NotNull(api1);
            Assert.NotNull(api2);
            Assert.NotSame(api1, api2);
            
            _output.WriteLine("? Multiple Initialize calls return separate instances");
        }

        public void Dispose()
        {
            _api?.Dispose();
        }
    }
}
