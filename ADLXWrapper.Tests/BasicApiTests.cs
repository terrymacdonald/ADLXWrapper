using Xunit;
using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Basic API tests for ADLX wrapper
    /// Tests initialization, version queries, and cleanup
    /// </summary>
    [SupportedOSPlatform("windows")]
    public unsafe class BasicApiTests : IDisposable
    {
        private readonly ADLXApiHelper? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;

        public BasicApiTests(ITestOutputHelper output)
        {
            // Stage 1: Check for AMD GPU hardware via PCI
            if (!ADLXHardwareDetection.HasAMDGPU(out string hwError))
            {
                _hasHardware = false;
                _hasDll = false;
                _skipReason = hwError;
                return;
            }
            _hasHardware = true;

            var gpuNames = ADLXHardwareDetection.GetAMDGPUNames();
            if (gpuNames.Length > 0)
            {
            }

            // Stage 2: Check for ADLX DLL availability
            if (!ADLXApiHelper.IsADLXDllAvailable(out string dllError))
            {
                _hasDll = false;
                _skipReason = dllError;
                return;
            }
            _hasDll = true;

            // Stage 3: Try to initialize ADLX API
            try
            {
                _api = ADLXApiHelper.Initialize();
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _api?.Dispose();
        }

        [SkippableFact]
        public void Initialize_ShouldSucceed()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            Assert.NotNull(_api);
        }

        [SkippableFact]
        public void GetVersion_ShouldReturnValidVersion()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            var version = _api!.GetVersion();
            
            Assert.NotNull(version);
            Assert.NotEmpty(version);
        }

        [SkippableFact]
        public void GetFullVersion_ShouldReturnNonZero()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            var fullVersion = _api!.GetFullVersion();
            
            Assert.NotEqual(0UL, fullVersion);
        }

        [SkippableFact]
        public void GetSystemServices_ShouldReturnValidPointer()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);
            using var handle = _api!.GetSystemServicesHandle();
            Assert.False(handle.IsInvalid);
        }

        [SkippableFact]
        public void Dispose_ShouldNotThrow()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            // Create a new instance to test disposal
            using var testApi = ADLXApiHelper.Initialize();
            var exception = Record.Exception(() => testApi.Dispose());
            
            Assert.Null(exception);
        }

        [SkippableFact]
        public void DisposeMultipleTimes_ShouldBeIdempotent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            // Create a new instance to test multiple disposals
            var testApi = ADLXApiHelper.Initialize();
            
            // Multiple dispose calls should be safe
            testApi.Dispose();
            testApi.Dispose();
            testApi.Dispose();
            
        }

        [SkippableFact]
        public void AfterDispose_MethodsShouldThrowObjectDisposedException()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            var testApi = ADLXApiHelper.Initialize();
            testApi.Dispose();

            // After dispose, methods should throw ObjectDisposedException
            Assert.Throws<ObjectDisposedException>(() => testApi.GetVersion());
            Assert.Throws<ObjectDisposedException>(() => testApi.GetFullVersion());
            Assert.Throws<ObjectDisposedException>(() => testApi.GetSystemServicesHandle());
        }

        [SkippableFact]
        public void UsingStatement_ShouldAutomaticallyDispose()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            ADLXApiHelper? testApi = null;
            
            using (testApi = ADLXApiHelper.Initialize())
            {
                Assert.NotNull(testApi);
                var version = testApi.GetVersion();
                Assert.NotEmpty(version);
            }

            // After using block, should be disposed
            Assert.Throws<ObjectDisposedException>(() => testApi!.GetVersion());
        }

        [SkippableFact]
        public void InitializeMultipleTimes_ShouldReturnSeparateInstances()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            using var api1 = ADLXApiHelper.Initialize();
            using var api2 = ADLXApiHelper.Initialize();

            Assert.NotNull(api1);
            Assert.NotNull(api2);
            Assert.NotSame(api1, api2);
        }
    }
}

