using Xunit;
using System;
using System.Linq;
using ADLXWrapper;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Display services tests for ADLX wrapper
    /// Tests display enumeration and property access
    /// </summary>
    public class DisplayServicesTests : IClassFixture<ADLXTestFixture>
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXTestFixture _fixture;
        private readonly IntPtr[] _displays;

        public DisplayServicesTests(ITestOutputHelper output, ADLXTestFixture fixture)
        {
            _output = output;
            _fixture = fixture;
            _displays = Array.Empty<IntPtr>();
            
            // Write diagnostics once per test class instance
            _fixture.WriteDiagnostics(_output);

            // Enumerate displays if we have hardware
            if (_fixture.CanRunTests)
            {
                var pSystem = _fixture.Api!.GetSystemServices();
                _displays = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);
                _output.WriteLine($"   Found {_displays.Length} display(s) connected");
            }
        }

        [SkippableFact]
        public void EnumerateAllDisplays_ShouldReturnArray()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var pSystem = _fixture.Api!.GetSystemServices();
            var displays = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);

            Assert.NotNull(displays);
            _output.WriteLine($"? Found {displays.Length} display(s)");

            foreach (var display in displays)
            {
                ADLXHelpers.ReleaseInterface(display);
            }
        }

        [SkippableFact]
        public void EnumerateAllDisplays_ShouldReturnValidPointers()
        {
            Skip.IfNot(_fixture.CanRunTests && _displays.Length > 0, 
                _fixture.CanRunTests ? "No displays available" : _fixture.SkipReason);

            foreach (var display in _displays)
            {
                Assert.NotEqual(IntPtr.Zero, display);
            }

            _output.WriteLine($"? All {_displays.Length} display pointer(s) are valid (non-zero)");
        }

        [SkippableFact]
        public void GetDisplayName_ShouldReturnValidName()
        {
            Skip.IfNot(_fixture.CanRunTests && _displays.Length > 0,
                _fixture.CanRunTests ? "No displays available" : _fixture.SkipReason);

            var name = ADLXDisplayHelpers.GetDisplayName(_displays[0]);

            Assert.NotNull(name);
            Assert.NotEmpty(name);
            _output.WriteLine($"? Display Name: {name}");
        }

        [SkippableFact]
        public void GetDisplayNativeResolution_ShouldReturnValidResolution()
        {
            Skip.IfNot(_fixture.CanRunTests && _displays.Length > 0,
                _fixture.CanRunTests ? "No displays available" : _fixture.SkipReason);

            var (width, height) = ADLXDisplayHelpers.GetDisplayNativeResolution(_displays[0]);

            Assert.True(width > 0, "Width should be greater than 0");
            Assert.True(height > 0, "Height should be greater than 0");
            _output.WriteLine($"? Native Resolution: {width}x{height}");
        }

        [SkippableFact]
        public void GetDisplayRefreshRate_ShouldReturnPositiveValue()
        {
            Skip.IfNot(_fixture.CanRunTests && _displays.Length > 0,
                _fixture.CanRunTests ? "No displays available" : _fixture.SkipReason);

            var refreshRate = ADLXDisplayHelpers.GetDisplayRefreshRate(_displays[0]);

            Assert.True(refreshRate > 0, "Refresh rate should be greater than 0");
            _output.WriteLine($"? Refresh Rate: {refreshRate} Hz");
        }

        [SkippableFact]
        public void GetDisplayManufacturerID_ShouldReturnValue()
        {
            Skip.IfNot(_fixture.CanRunTests && _displays.Length > 0,
                _fixture.CanRunTests ? "No displays available" : _fixture.SkipReason);

            var manufacturerID = ADLXDisplayHelpers.GetDisplayManufacturerID(_displays[0]);

            _output.WriteLine($"? Manufacturer ID: {manufacturerID}");
        }

        [SkippableFact]
        public void GetDisplayPixelClock_ShouldReturnPositiveValue()
        {
            Skip.IfNot(_fixture.CanRunTests && _displays.Length > 0,
                _fixture.CanRunTests ? "No displays available" : _fixture.SkipReason);

            var pixelClock = ADLXDisplayHelpers.GetDisplayPixelClock(_displays[0]);

            Assert.True(pixelClock > 0, "Pixel clock should be greater than 0");
            _output.WriteLine($"? Pixel Clock: {pixelClock}");
        }

        [SkippableFact]
        public void GetDisplayBasicInfo_ShouldReturnCompleteInformation()
        {
            Skip.IfNot(_fixture.CanRunTests && _displays.Length > 0,
                _fixture.CanRunTests ? "No displays available" : _fixture.SkipReason);

            var info = ADLXDisplayInfo.GetBasicInfo(_displays[0]);

            Assert.NotNull(info.Name);
            Assert.NotEmpty(info.Name);
            Assert.True(info.Width > 0);
            Assert.True(info.Height > 0);
            Assert.True(info.RefreshRate > 0);
            Assert.True(info.PixelClock > 0);

            _output.WriteLine("? Display Basic Info Retrieved:");
            _output.WriteLine($"  Name: {info.Name}");
            _output.WriteLine($"  Resolution: {info.Width}x{info.Height}");
            _output.WriteLine($"  Refresh Rate: {info.RefreshRate} Hz");
            _output.WriteLine($"  Manufacturer ID: {info.ManufacturerID}");
            _output.WriteLine($"  Pixel Clock: {info.PixelClock}");
        }

        [SkippableFact]
        public void AllDisplays_ShouldHaveNames()
        {
            Skip.IfNot(_fixture.CanRunTests && _displays.Length >= 2,
                _fixture.CanRunTests ? "Need at least 2 displays" : _fixture.SkipReason);

            var names = _displays.Select(display => ADLXDisplayHelpers.GetDisplayName(display)).ToList();
            var distinctNames = names.Distinct().Count();

            _output.WriteLine($"? Found {distinctNames} distinct display name(s) among {_displays.Length} displays");
            foreach (var name in names)
            {
                _output.WriteLine($"  - {name}");
            }
        }

        [Fact]
        public void GetDisplayName_WithNullPointer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ADLXDisplayHelpers.GetDisplayName(IntPtr.Zero));
            _output.WriteLine("? GetDisplayName correctly throws on null pointer");
        }

        [Fact]
        public void GetDisplayNativeResolution_WithNullPointer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ADLXDisplayHelpers.GetDisplayNativeResolution(IntPtr.Zero));
            _output.WriteLine("? GetDisplayNativeResolution correctly throws on null pointer");
        }

        [Fact]
        public void GetDisplayRefreshRate_WithNullPointer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ADLXDisplayHelpers.GetDisplayRefreshRate(IntPtr.Zero));
            _output.WriteLine("? GetDisplayRefreshRate correctly throws on null pointer");
        }

        [Fact]
        public void EnumerateAllDisplays_WithNullPointer_ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => ADLXDisplayHelpers.EnumerateAllDisplays(IntPtr.Zero));
            _output.WriteLine("? EnumerateAllDisplays correctly throws on null pointer");
        }

        [SkippableFact]
        public void MultipleDisplayEnumeration_ShouldReturnConsistentResults()
        {
            Skip.IfNot(_fixture.CanRunTests, _fixture.SkipReason);

            var pSystem = _fixture.Api!.GetSystemServices();
            
            var displays1 = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);
            var displays2 = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);

            Assert.Equal(displays1.Length, displays2.Length);
            _output.WriteLine($"? Multiple enumerations return consistent count: {displays1.Length}");

            foreach (var display in displays1)
            {
                ADLXHelpers.ReleaseInterface(display);
            }
            foreach (var display in displays2)
            {
                ADLXHelpers.ReleaseInterface(display);
            }
        }
    }
}
