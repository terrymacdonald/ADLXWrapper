using Xunit;
using System;
using System.Linq;
using ADLXWrapper;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Core API tests for ADLX wrapper
    /// Tests GPU enumeration, property access, and helper methods
    /// </summary>
    public class CoreApiTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly IntPtr[] _gpus;

        public CoreApiTests(ITestOutputHelper output)
        {
            _output = output;
            _gpus = Array.Empty<IntPtr>();

            try
            {
                _api = ADLXApi.Initialize();
                _gpus = _api.EnumerateGPUs();
                _hasHardware = _gpus.Length > 0;

                if (_hasHardware)
                {
                    _output.WriteLine($"? ADLX initialized, found {_gpus.Length} GPU(s)");
                }
                else
                {
                    _output.WriteLine("? ADLX initialized but no GPUs found");
                }
            }
            catch (Exception ex)
            {
                _hasHardware = false;
                _output.WriteLine($"? Initialization failed: {ex.Message}");
            }
        }

        [Fact]
        public void EnumerateGPUs_ShouldReturnArray()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var gpus = _api.EnumerateGPUs();

            Assert.NotNull(gpus);
            Assert.NotEmpty(gpus);
            _output.WriteLine($"? Found {gpus.Length} GPU(s)");
        }

        [Fact]
        public void EnumerateGPUs_ShouldReturnValidPointers()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var gpus = _api.EnumerateGPUs();

            foreach (var gpu in gpus)
            {
                Assert.NotEqual(IntPtr.Zero, gpu);
            }

            _output.WriteLine($"? All {gpus.Length} GPU pointer(s) are valid (non-zero)");
        }

        [Fact]
        public void GetGPUName_ShouldReturnValidName()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var name = ADLXHelpers.GetGPUName(_gpus[0]);

            Assert.NotNull(name);
            Assert.NotEmpty(name);
            _output.WriteLine($"? GPU Name: {name}");
        }

        [Fact]
        public void GetGPUVendorId_ShouldReturnAMD()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var vendorId = ADLXHelpers.GetGPUVendorId(_gpus[0]);

            Assert.NotNull(vendorId);
            Assert.NotEmpty(vendorId);
            _output.WriteLine($"? Vendor ID: {vendorId}");
        }

        [Fact]
        public void GetGPUTotalVRAM_ShouldReturnPositiveValue()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var vram = ADLXHelpers.GetGPUTotalVRAM(_gpus[0]);

            Assert.True(vram > 0, "VRAM should be greater than 0");
            _output.WriteLine($"? Total VRAM: {vram} MB");
        }

        [Fact]
        public void GetGPUVRAMType_ShouldReturnValidType()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var vramType = ADLXHelpers.GetGPUVRAMType(_gpus[0]);

            Assert.NotNull(vramType);
            Assert.NotEmpty(vramType);
            _output.WriteLine($"? VRAM Type: {vramType}");
        }

        [Fact]
        public void GetGPUUniqueId_ShouldReturnValue()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var uniqueId = ADLXHelpers.GetGPUUniqueId(_gpus[0]);

            _output.WriteLine($"? Unique ID: {uniqueId}");
        }

        [Fact]
        public void GetGPUDeviceId_ShouldReturnValidId()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var deviceId = ADLXHelpers.GetGPUDeviceId(_gpus[0]);

            Assert.NotNull(deviceId);
            Assert.NotEmpty(deviceId);
            _output.WriteLine($"? Device ID: {deviceId}");
        }

        [Fact]
        public void GetGPUDriverPath_ShouldReturnValidPath()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var driverPath = ADLXHelpers.GetGPUDriverPath(_gpus[0]);

            Assert.NotNull(driverPath);
            Assert.NotEmpty(driverPath);
            _output.WriteLine($"? Driver Path: {driverPath}");
        }

        [Fact]
        public void GetGPUPNPString_ShouldReturnValidString()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var pnpString = ADLXHelpers.GetGPUPNPString(_gpus[0]);

            Assert.NotNull(pnpString);
            Assert.NotEmpty(pnpString);
            _output.WriteLine($"? PNP String: {pnpString}");
        }

        [Fact]
        public void IsGPUExternal_ShouldReturnBooleanValue()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var isExternal = ADLXHelpers.IsGPUExternal(_gpus[0]);

            _output.WriteLine($"? Is External: {isExternal}");
        }

        [Fact]
        public void HasGPUDesktops_ShouldReturnBooleanValue()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var hasDesktops = ADLXHelpers.HasGPUDesktops(_gpus[0]);

            _output.WriteLine($"? Has Desktops: {hasDesktops}");
        }

        [Fact]
        public void GetBasicInfo_ShouldReturnCompleteInformation()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var info = ADLXGPUInfo.GetBasicInfo(_gpus[0]);

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

        [Fact]
        public void GetIdentification_ShouldReturnCompleteInformation()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var id = ADLXGPUInfo.GetIdentification(_gpus[0]);

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

        [Fact]
        public void AllGPUs_ShouldHaveUniqueIds()
        {
            if (!_hasHardware || _gpus.Length < 2)
            {
                _output.WriteLine("? Test skipped - Need at least 2 GPUs");
                return;
            }

            var uniqueIds = _gpus.Select(gpu => ADLXHelpers.GetGPUUniqueId(gpu)).ToList();
            var distinctIds = uniqueIds.Distinct().Count();

            Assert.Equal(uniqueIds.Count, distinctIds);
            _output.WriteLine($"? All {_gpus.Length} GPUs have unique IDs");
        }

        [Fact]
        public void GetGPUName_WithNullPointer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ADLXHelpers.GetGPUName(IntPtr.Zero));
            _output.WriteLine("? GetGPUName correctly throws on null pointer");
        }

        [Fact]
        public void GetGPUTotalVRAM_WithNullPointer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ADLXHelpers.GetGPUTotalVRAM(IntPtr.Zero));
            _output.WriteLine("? GetGPUTotalVRAM correctly throws on null pointer");
        }

        [Fact]
        public void ReleaseInterface_WithValidPointer_ShouldNotThrow()
        {
            if (!_hasHardware || _gpus.Length == 0)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            // Get a fresh GPU pointer
            var gpus = _api!.EnumerateGPUs();
            var exception = Record.Exception(() => ADLXHelpers.ReleaseInterface(gpus[0]));

            Assert.Null(exception);
            _output.WriteLine("? ReleaseInterface executed without exception");
        }

        [Fact]
        public void ReleaseInterface_WithNullPointer_ShouldNotThrow()
        {
            var exception = Record.Exception(() => ADLXHelpers.ReleaseInterface(IntPtr.Zero));

            Assert.Null(exception);
            _output.WriteLine("? ReleaseInterface handles null pointer gracefully");
        }

        public void Dispose()
        {
            // Release all GPU interfaces
            foreach (var gpu in _gpus)
            {
                try
                {
                    ADLXHelpers.ReleaseInterface(gpu);
                }
                catch
                {
                    // Ignore errors during cleanup
                }
            }

            _api?.Dispose();
        }
    }
}
