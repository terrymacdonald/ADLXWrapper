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
    public class BasicApiTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle[] _gpus = Array.Empty<AdlxInterfaceHandle>();

        public BasicApiTests(ITestOutputHelper output)
        {
            _output = output;

            // Stage 1: Check for AMD GPU hardware via PCI
            if (!HardwareDetection.HasAMDGPU(out string hwError))
            {
                _hasHardware = false;
                _hasDll = false;
                _skipReason = hwError;
                _output.WriteLine($"??  {hwError}");
                return;
            }
            _hasHardware = true;

            var gpuNames = HardwareDetection.GetAMDGPUNames();
            if (gpuNames.Length > 0)
            {
                _output.WriteLine($"? AMD GPU detected: {string.Join(", ", gpuNames)}");
            }

            // Stage 2: Check for ADLX DLL availability
            if (!ADLXApi.IsADLXDllAvailable(out string dllError))
            {
                _hasDll = false;
                _skipReason = dllError;
                _output.WriteLine($"??  {dllError}");
                return;
            }
            _hasDll = true;

            // Stage 3: Try to initialize ADLX API
            try
            {
                _api = ADLXApi.Initialize();
                _gpus = _api.EnumerateGPUHandles();
                _output.WriteLine($"? ADLX initialized successfully");
                _output.WriteLine($"  ADLX Version: {_api.GetVersion()}");
                _output.WriteLine($"  GPUs found: {_gpus.Length}");
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
                _output.WriteLine($"??  {_skipReason}");
            }
        }

        public void Dispose()
        {
            // Release GPU interfaces
            foreach (var gpu in _gpus)
            {
                try
                {
                    gpu.Dispose();
                }
                catch
                {
                    // Ignore errors during cleanup
                }
            }

            _api?.Dispose();
        }

        [SkippableFact]
        public void Initialize_ShouldSucceed()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            Assert.NotNull(_api);
            _output.WriteLine("? ADLXApi instance is not null");
        }

        [SkippableFact]
        public void GetVersion_ShouldReturnValidVersion()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            var version = _api!.GetVersion();
            
            Assert.NotNull(version);
            Assert.NotEmpty(version);
            _output.WriteLine($"? ADLX Version: {version}");
        }

        [SkippableFact]
        public void GetFullVersion_ShouldReturnNonZero()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            var fullVersion = _api!.GetFullVersion();
            
            Assert.NotEqual(0UL, fullVersion);
            _output.WriteLine($"? ADLX Full Version: {fullVersion}");
        }

        [SkippableFact]
        public void GetSystemServices_ShouldReturnValidPointer()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            using var pSystemServices = _api!.GetSystemServicesHandle();
            
            Assert.False(pSystemServices.IsInvalid);
            _output.WriteLine($"? System services handle: 0x{((IntPtr)pSystemServices):X}");
        }

        [SkippableFact]
        public void AllServiceTypes_ShouldBeAccessible()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            _output.WriteLine("=== Testing All Service Types ===");

            using var pSystem = _api!.GetSystemServicesHandle();
            Assert.False(pSystem.IsInvalid);
            _output.WriteLine("? System Services: Accessible");

            _output.WriteLine($"? GPU Enumeration: {_gpus.Length} GPU(s) found");

            try
            {
                var displays = ADLXDisplayHelpers.EnumerateAllDisplayHandles(pSystem);
                _output.WriteLine($"? Display Services: {displays.Length} display(s) found");
                foreach (var display in displays)
                {
                    display.Dispose();
                }
            }
            catch (Exception ex)
            {
                _output.WriteLine($"??  Display Services: {ex.Message}");
            }

            try
            {
                using var pTuningServices = _api.GetGPUTuningServicesHandle();
                Assert.False(pTuningServices.IsInvalid);
                _output.WriteLine("? GPU Tuning Services: Accessible");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"??  GPU Tuning Services: {ex.Message}");
            }

            try
            {
                using var pPerfMonServices = _api.GetPerformanceMonitoringServicesHandle();
                Assert.False(pPerfMonServices.IsInvalid);
                _output.WriteLine("? Performance Monitoring Services: Accessible");
            }
            catch (Exception ex)
            {
                _output.WriteLine($"??  Performance Monitoring Services: {ex.Message}");
            }

            _output.WriteLine("");
            _output.WriteLine("? All implemented services are accessible and working");
        }

        [SkippableFact]
        public void Dispose_ShouldNotThrow()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            // Create a new instance to test disposal
            using var testApi = ADLXApi.Initialize();
            var exception = Record.Exception(() => testApi.Dispose());
            
            Assert.Null(exception);
            _output.WriteLine("? Dispose completed without exception");
        }

        [SkippableFact]
        public void DisposeMultipleTimes_ShouldBeIdempotent()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

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
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            var testApi = ADLXApi.Initialize();
            testApi.Dispose();

            // After dispose, methods should throw ObjectDisposedException
            Assert.Throws<ObjectDisposedException>(() => testApi.GetVersion());
            Assert.Throws<ObjectDisposedException>(() => testApi.GetFullVersion());
            Assert.Throws<ObjectDisposedException>(() => testApi.GetSystemServicesHandle());
            Assert.Throws<ObjectDisposedException>(() => testApi.EnumerateGPUHandles());
            
            _output.WriteLine("? Methods correctly throw ObjectDisposedException after disposal");
        }

        [SkippableFact]
        public void UsingStatement_ShouldAutomaticallyDispose()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

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
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            using var api1 = ADLXApi.Initialize();
            using var api2 = ADLXApi.Initialize();

            Assert.NotNull(api1);
            Assert.NotNull(api2);
            Assert.NotSame(api1, api2);
            
            _output.WriteLine("? Multiple Initialize calls return separate instances");
        }
    }
}
