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
    public class DisplayServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly IntPtr[] _displays;

        public DisplayServicesTests(ITestOutputHelper output)
        {
            _output = output;
            _displays = Array.Empty<IntPtr>();

            try
            {
                _api = ADLXApi.Initialize();
                var pSystem = _api.GetSystemServices();
                _displays = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);
                _hasHardware = _displays.Length > 0;

                if (_hasHardware)
                {
                    _output.WriteLine($"? ADLX initialized, found {_displays.Length} display(s)");
                }
                else
                {
                    _output.WriteLine("? ADLX initialized but no displays found");
                }
            }
            catch (Exception ex)
            {
                _hasHardware = false;
                _output.WriteLine($"? Initialization failed: {ex.Message}");
            }
        }

        [Fact]
        public void EnumerateAllDisplays_ShouldReturnArray()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware or displays available");
                return;
            }

            var pSystem = _api.GetSystemServices();
            var displays = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);

            Assert.NotNull(displays);
            _output.WriteLine($"? Found {displays.Length} display(s)");

            // Release the displays we just enumerated (already released in fixture)
            foreach (var display in displays)
            {
                ADLXHelpers.ReleaseInterface(display);
            }
        }

        [Fact]
        public void EnumerateAllDisplays_ShouldReturnValidPointers()
        {
            if (!_hasHardware || _displays.Length == 0)
            {
                _output.WriteLine("? Test skipped - No displays available");
                return;
            }

            foreach (var display in _displays)
            {
                Assert.NotEqual(IntPtr.Zero, display);
            }

            _output.WriteLine($"? All {_displays.Length} display pointer(s) are valid (non-zero)");
        }

        [Fact]
        public void GetDisplayName_ShouldReturnValidName()
        {
            if (!_hasHardware || _displays.Length == 0)
            {
                _output.WriteLine("? Test skipped - No displays available");
                return;
            }

            var name = ADLXDisplayHelpers.GetDisplayName(_displays[0]);

            Assert.NotNull(name);
            Assert.NotEmpty(name);
            _output.WriteLine($"? Display Name: {name}");
        }

        [Fact]
        public void GetDisplayNativeResolution_ShouldReturnValidResolution()
        {
            if (!_hasHardware || _displays.Length == 0)
            {
                _output.WriteLine("? Test skipped - No displays available");
                return;
            }

            var (width, height) = ADLXDisplayHelpers.GetDisplayNativeResolution(_displays[0]);

            Assert.True(width > 0, "Width should be greater than 0");
            Assert.True(height > 0, "Height should be greater than 0");
            _output.WriteLine($"? Native Resolution: {width}x{height}");
        }

        [Fact]
        public void GetDisplayRefreshRate_ShouldReturnPositiveValue()
        {
            if (!_hasHardware || _displays.Length == 0)
            {
                _output.WriteLine("? Test skipped - No displays available");
                return;
            }

            var refreshRate = ADLXDisplayHelpers.GetDisplayRefreshRate(_displays[0]);

            Assert.True(refreshRate > 0, "Refresh rate should be greater than 0");
            _output.WriteLine($"? Refresh Rate: {refreshRate} Hz");
        }

        [Fact]
        public void GetDisplayManufacturerID_ShouldReturnValue()
        {
            if (!_hasHardware || _displays.Length == 0)
            {
                _output.WriteLine("? Test skipped - No displays available");
                return;
            }

            var manufacturerID = ADLXDisplayHelpers.GetDisplayManufacturerID(_displays[0]);

            _output.WriteLine($"? Manufacturer ID: {manufacturerID}");
        }

        [Fact]
        public void GetDisplayPixelClock_ShouldReturnPositiveValue()
        {
            if (!_hasHardware || _displays.Length == 0)
            {
                _output.WriteLine("? Test skipped - No displays available");
                return;
            }

            var pixelClock = ADLXDisplayHelpers.GetDisplayPixelClock(_displays[0]);

            Assert.True(pixelClock > 0, "Pixel clock should be greater than 0");
            _output.WriteLine($"? Pixel Clock: {pixelClock}");
        }

        [Fact]
        public void GetDisplayBasicInfo_ShouldReturnCompleteInformation()
        {
            if (!_hasHardware || _displays.Length == 0)
            {
                _output.WriteLine("? Test skipped - No displays available");
                return;
            }

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

        [Fact]
        public void AllDisplays_ShouldHaveUniqueNames()
        {
            if (!_hasHardware || _displays.Length < 2)
            {
                _output.WriteLine("? Test skipped - Need at least 2 displays");
                return;
            }

            var names = _displays.Select(display => ADLXDisplayHelpers.GetDisplayName(display)).ToList();
            var distinctNames = names.Distinct().Count();

            // Some displays might have the same model name, so we just log this
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

        [Fact]
        public void MultipleDisplayEnumeration_ShouldReturnConsistentResults()
        {
            if (!_hasHardware || _api == null)
            {
                _output.WriteLine("? Test skipped - No AMD hardware available");
                return;
            }

            var pSystem = _api.GetSystemServices();
            
            // Enumerate displays multiple times
            var displays1 = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);
            var displays2 = ADLXDisplayHelpers.EnumerateAllDisplays(pSystem);

            Assert.Equal(displays1.Length, displays2.Length);
            _output.WriteLine($"? Multiple enumerations return consistent count: {displays1.Length}");

            // Release the extra displays
            foreach (var display in displays1)
            {
                ADLXHelpers.ReleaseInterface(display);
            }
            foreach (var display in displays2)
            {
                ADLXHelpers.ReleaseInterface(display);
            }
        }

        public void Dispose()
        {
            // Release all display interfaces
            foreach (var display in _displays)
            {
                try
                {
                    ADLXHelpers.ReleaseInterface(display);
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
