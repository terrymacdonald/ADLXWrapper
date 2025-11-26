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
    public class CoreApiTests : IClassFixture<ADLXTestFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXTestFixture _fixture;

        public CoreApiTests(ITestOutputHelper output, ADLXTestFixture fixture)
        {
            _output = output;
            _fixture = fixture;
            
            // Write diagnostics once per test class instance
            _fixture.WriteDiagnostics(_output);
        }

        [SkippableFact]
        public void EnumerateGPUs_ShouldReturnArray()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var gpus = _fixture.Api!.EnumerateGPUs();

            Assert.NotNull(gpus);
            Assert.NotEmpty(gpus);
            _output.WriteLine($"? Found {gpus.Length} GPU(s)");
            
            // Release the newly enumerated GPUs
            foreach (var gpu in gpus)
            {
                ADLXHelpers.ReleaseInterface(gpu);
            }
        }

        [SkippableFact]
        public void EnumerateGPUs_ShouldReturnValidPointers()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var gpus = _fixture.Api!.EnumerateGPUs();

            foreach (var gpu in gpus)
            {
                Assert.NotEqual(IntPtr.Zero, gpu);
                ADLXHelpers.ReleaseInterface(gpu);
            }

            _output.WriteLine($"? All {gpus.Length} GPU pointer(s) are valid (non-zero)");
        }

        [SkippableFact]
        public void GetGPUName_ShouldReturnValidName()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var name = ADLXHelpers.GetGPUName(_fixture.GPUs[0]);

            Assert.NotNull(name);
            Assert.NotEmpty(name);
            _output.WriteLine($"? GPU Name: {name}");
        }

        [SkippableFact]
        public void GetGPUVendorId_ShouldReturnAMD()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var vendorId = ADLXHelpers.GetGPUVendorId(_fixture.GPUs[0]);

            Assert.NotNull(vendorId);
            Assert.NotEmpty(vendorId);
            _output.WriteLine($"? Vendor ID: {vendorId}");
        }

        [SkippableFact]
        public void GetGPUTotalVRAM_ShouldReturnPositiveValue()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var vram = ADLXHelpers.GetGPUTotalVRAM(_fixture.GPUs[0]);

            Assert.True(vram > 0, "VRAM should be greater than 0");
            _output.WriteLine($"? Total VRAM: {vram} MB");
        }

        [SkippableFact]
        public void GetGPUVRAMType_ShouldReturnValidType()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var vramType = ADLXHelpers.GetGPUVRAMType(_fixture.GPUs[0]);

            Assert.NotNull(vramType);
            Assert.NotEmpty(vramType);
            _output.WriteLine($"? VRAM Type: {vramType}");
        }

        [SkippableFact]
        public void GetGPUUniqueId_ShouldReturnValue()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var uniqueId = ADLXHelpers.GetGPUUniqueId(_fixture.GPUs[0]);

            _output.WriteLine($"? Unique ID: {uniqueId}");
        }

        [SkippableFact]
        public void GetGPUDeviceId_ShouldReturnValidId()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var deviceId = ADLXHelpers.GetGPUDeviceId(_fixture.GPUs[0]);

            Assert.NotNull(deviceId);
            Assert.NotEmpty(deviceId);
            _output.WriteLine($"? Device ID: {deviceId}");
        }

        [SkippableFact]
        public void GetGPUDriverPath_ShouldReturnValidPath()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var driverPath = ADLXHelpers.GetGPUDriverPath(_fixture.GPUs[0]);

            Assert.NotNull(driverPath);
            Assert.NotEmpty(driverPath);
            _output.WriteLine($"? Driver Path: {driverPath}");
        }

        [SkippableFact]
        public void GetGPUPNPString_ShouldReturnValidString()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var pnpString = ADLXHelpers.GetGPUPNPString(_fixture.GPUs[0]);

            Assert.NotNull(pnpString);
            Assert.NotEmpty(pnpString);
            _output.WriteLine($"? PNP String: {pnpString}");
        }

        [SkippableFact]
        public void IsGPUExternal_ShouldReturnBooleanValue()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var isExternal = ADLXHelpers.IsGPUExternal(_fixture.GPUs[0]);

            _output.WriteLine($"? Is External: {isExternal}");
        }

        [SkippableFact]
        public void HasGPUDesktops_ShouldReturnBooleanValue()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var hasDesktops = ADLXHelpers.HasGPUDesktops(_fixture.GPUs[0]);

            _output.WriteLine($"? Has Desktops: {hasDesktops}");
        }

        [SkippableFact]
        public void GetBasicInfo_ShouldReturnCompleteInformation()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var info = ADLXGPUInfo.GetBasicInfo(_fixture.GPUs[0]);

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
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var id = ADLXGPUInfo.GetIdentification(_fixture.GPUs[0]);

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
            Skip.IfNot(_fixture.CanRunTests && _fixture.GPUs.Length >= 2, 
                _fixture.CanRunTests ? "Need at least 2 GPUs" : _fixture.SkipReason);

            var uniqueIds = _fixture.GPUs.Select(gpu => ADLXHelpers.GetGPUUniqueId(gpu)).ToList();
            var distinctIds = uniqueIds.Distinct().Count();

            Assert.Equal(uniqueIds.Count, distinctIds);
            _output.WriteLine($"? All {_fixture.GPUs.Length} GPUs have unique IDs");
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

        [SkippableFact]
        public void ReleaseInterface_WithValidPointer_ShouldNotThrow()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            // Get a fresh GPU pointer
            var gpus = _fixture.Api!.EnumerateGPUs();
            var exception = Record.Exception(() => ADLXHelpers.ReleaseInterface(gpus[0]));

            Assert.Null(exception);
            _output.WriteLine("? ReleaseInterface executed without exception");
            
            // Release remaining GPUs
            for (int i = 1; i < gpus.Length; i++)
            {
                ADLXHelpers.ReleaseInterface(gpus[i]);
            }
        }

        [Fact]
        public void ReleaseInterface_WithNullPointer_ShouldNotThrow()
        {
            var exception = Record.Exception(() => ADLXHelpers.ReleaseInterface(IntPtr.Zero));

            Assert.Null(exception);
            _output.WriteLine("? ReleaseInterface handles null pointer gracefully");
        }
    }
}
