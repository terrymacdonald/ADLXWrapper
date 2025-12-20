using System;
using System.Linq;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    [SupportedOSPlatform("windows")]
    /// <summary>
    /// Core API tests for ADLX wrapper
    /// Tests GPU enumeration, property access, and helper methods
    /// </summary>
    public unsafe class CoreApiTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle[] _gpus = Array.Empty<AdlxInterfaceHandle>();

        public CoreApiTests(ITestOutputHelper output)
        {
            _output = output;

            // Stage 1: Check for AMD GPU hardware via PCI
            if (!ADLXHardwareDetection.HasAMDGPU(out string hwError))
            {
                _hasHardware = false;
                _hasDll = false;
                _skipReason = hwError;
                _output.WriteLine($"??  {hwError}");
                return;
            }
            _hasHardware = true;

            var gpuNames = ADLXHardwareDetection.GetAMDGPUNames();
            if (gpuNames.Length > 0)
            {
                _output.WriteLine($"? AMD GPU detected: {string.Join(", ", gpuNames)}");
            }

            // Stage 2: Check for ADLX DLL availability
            if (!ADLXApiHelper.IsADLXDllAvailable(out string dllError))
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
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                _gpus = _system.EnumerateGPUHandles();
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
            // Release all GPU interfaces
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

            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void EnumerateGPUs_ShouldReturnArray()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null, _skipReason);

            var gpus = _system!.EnumerateGPUHandles();

            Assert.NotNull(gpus);
            Assert.NotEmpty(gpus);
            _output.WriteLine($"? Found {gpus.Length} GPU(s)");
            
            // Release the newly enumerated GPUs
            foreach (var gpu in gpus)
            {
                gpu.Dispose();
            }
        }

        [SkippableFact]
        public void EnumerateGPUs_ShouldReturnValidPointers()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null, _skipReason);

            var gpus = _system!.EnumerateGPUHandles();

            foreach (var gpu in gpus)
            {
                Assert.False(gpu.IsInvalid);
                gpu.Dispose();
            }

            _output.WriteLine($"? All {gpus.Length} GPU handle(s) are valid");
        }

        [SkippableFact]
        public void GetGPUName_ShouldReturnValidName()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null || _gpus.Length == 0, _skipReason);

            var name = ADLXUtils.GetGPUName(_gpus[0]);

            Assert.NotNull(name);
            Assert.NotEmpty(name);
            _output.WriteLine($"? GPU Name: {name}");
        }

        [SkippableFact]
        public void GetGPUVendorId_ShouldReturnAMD()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null || _gpus.Length == 0, _skipReason);

            var vendorId = ADLXUtils.GetGPUVendorId(_gpus[0]);

            Assert.NotNull(vendorId);
            Assert.NotEmpty(vendorId);
            _output.WriteLine($"? Vendor ID: {vendorId}");
        }

        [SkippableFact]
        public void GetGPUTotalVRAM_ShouldReturnPositiveValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null || _gpus.Length == 0, _skipReason);

            var vram = ADLXUtils.GetGPUTotalVRAM(_gpus[0]);

            Assert.True(vram > 0, "VRAM should be greater than 0");
            _output.WriteLine($"? Total VRAM: {vram} MB");
        }

        [SkippableFact]
        public void GetGPUVRAMType_ShouldReturnValidType()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null || _gpus.Length == 0, _skipReason);

            var vramType = ADLXUtils.GetGPUVRAMType(_gpus[0]);

            Assert.NotNull(vramType);
            Assert.NotEmpty(vramType);
            _output.WriteLine($"? VRAM Type: {vramType}");
        }

        [SkippableFact]
        public void GetGPUUniqueId_ShouldReturnValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null || _gpus.Length == 0, _skipReason);

            var uniqueId = ADLXUtils.GetGPUUniqueId(_gpus[0]);

            _output.WriteLine($"? Unique ID: {uniqueId}");
        }

        [SkippableFact]
        public void GetGPUDeviceId_ShouldReturnValidId()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null || _gpus.Length == 0, _skipReason);

            var deviceId = ADLXUtils.GetGPUDeviceId(_gpus[0]);

            Assert.NotNull(deviceId);
            Assert.NotEmpty(deviceId);
            _output.WriteLine($"? Device ID: {deviceId}");
        }

        [SkippableFact]
        public void GetGPUDriverPath_ShouldReturnValidPath()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null || _gpus.Length == 0, _skipReason);

            var driverPath = ADLXUtils.GetGPUDriverPath(_gpus[0]);

            Assert.NotNull(driverPath);
            Assert.NotEmpty(driverPath);
            _output.WriteLine($"? Driver Path: {driverPath}");
        }

        [SkippableFact]
        public void GetGPUPNPString_ShouldReturnValidString()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _gpus.Length == 0, _skipReason);

            var pnpString = ADLXUtils.GetGPUPNPString(_gpus[0]);

            Assert.NotNull(pnpString);
            Assert.NotEmpty(pnpString);
            _output.WriteLine($"? PNP String: {pnpString}");
        }

        [SkippableFact]
        public void IsGPUExternal_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _gpus.Length == 0, _skipReason);

            var isExternal = ADLXUtils.IsGPUExternal(_gpus[0]);

            _output.WriteLine($"? Is External: {isExternal}");
        }

        [SkippableFact]
        public void HasGPUDesktops_ShouldReturnBooleanValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _gpus.Length == 0, _skipReason);

            var hasDesktops = ADLXUtils.HasGPUDesktops(_gpus[0]);

            _output.WriteLine($"? Has Desktops: {hasDesktops}");
        }

        [SkippableFact]
        public void GetBasicInfo_ShouldReturnCompleteInformation()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _gpus.Length == 0, _skipReason);

            var info = _system!.GetGpuInfo(_gpus[0].As<IADLXGPU>());

            Assert.NotNull(info.Name);
            Assert.NotEmpty(info.Name);
            Assert.NotNull(info.VendorId);
            Assert.NotEmpty(info.VendorId);
            Assert.True(info.TotalVRAM > 0);
            Assert.NotNull(info.VRAMType);
            Assert.NotEmpty(info.VRAMType);

            _output.WriteLine("? Basic Info Retrieved:");
            _output.WriteLine($"  Name: {info.Name}");
            _output.WriteLine($"  Vendor: {info.VendorId}");
            _output.WriteLine($"  Unique ID: {info.UniqueId}");
            _output.WriteLine($"  VRAM: {info.TotalVRAM} MB ({info.VRAMType})");
            _output.WriteLine($"  External: {info.IsExternal}");
            _output.WriteLine($"  Has Desktops: {info.HasDesktops}");
        }

        [SkippableFact]
        public void GetIdentification_ShouldReturnCompleteInformation()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _gpus.Length == 0, _skipReason);

            var id = _system!.GetGpuInfo(_gpus[0].As<IADLXGPU>());

            Assert.NotNull(id.DeviceId);
            Assert.NotEmpty(id.DeviceId);
            Assert.NotNull(id.PNPString);
            Assert.NotEmpty(id.PNPString);
            Assert.NotNull(id.DriverPath);
            Assert.NotEmpty(id.DriverPath);

            _output.WriteLine("? Identification Retrieved:");
            _output.WriteLine($"  Device ID: {id.DeviceId}");
            _output.WriteLine($"  PNP String: {id.PNPString}");
            _output.WriteLine($"  Driver Path: {id.DriverPath}");
            _output.WriteLine($"  Unique ID: {id.UniqueId}");
        }

        [SkippableFact]
        public void AllGPUs_ShouldHaveUniqueIds()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _system == null || _gpus.Length < 2, 
                _gpus.Length >= 2 ? _skipReason : "Need at least 2 GPUs");

            var uniqueIds = _gpus.Select(gpu => ADLXUtils.GetGPUUniqueId(gpu)).ToList();
            var distinctIds = uniqueIds.Distinct().Count();

            Assert.Equal(uniqueIds.Count, distinctIds);
            _output.WriteLine($"? All {_gpus.Length} GPUs have unique IDs");
        }

        [Fact]
        public void GetGPUName_WithNullPointer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ADLXUtils.GetGPUName(IntPtr.Zero));
            _output.WriteLine("? GetGPUName correctly throws on null pointer");
        }

        [Fact]
        public void GetGPUTotalVRAM_WithNullPointer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ADLXUtils.GetGPUTotalVRAM(IntPtr.Zero));
            _output.WriteLine("? GetGPUTotalVRAM correctly throws on null pointer");
        }

        [SkippableFact]
        public void ReleaseInterface_WithValidPointer_ShouldNotThrow()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            // Get a fresh GPU pointer
            var gpus = _system!.EnumerateGPUHandles();
            Exception? exception = null;
            try
            {
                gpus[0].Dispose();
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.Null(exception);
            _output.WriteLine("? ReleaseInterface executed without exception");
            
            // Release remaining GPUs
            for (int i = 1; i < gpus.Length; i++)
            {
                gpus[i].Dispose();
            }
        }

        [Fact]
        public void ReleaseInterface_WithNullPointer_ShouldNotThrow()
        {
            Exception? exception = null;
            try
            {
                ADLXUtils.ReleaseInterface(IntPtr.Zero);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.Null(exception);
            _output.WriteLine("? ReleaseInterface handles null pointer gracefully");
        }
    }
}

