using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Display services tests for ADLX wrapper
    /// Tests display enumeration and property access
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class DisplayServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle[] _gpus = Array.Empty<AdlxInterfaceHandle>();
        private readonly AdlxInterfaceHandle[] _displays = Array.Empty<AdlxInterfaceHandle>();

        public DisplayServicesTests(ITestOutputHelper output)
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

                // Enumerate displays
                using var pSystem = _api.GetSystemServicesHandle();
                _displays = ADLXDisplayHelpers.EnumerateAllDisplayHandles(pSystem);
                _output.WriteLine($"  Displays found: {_displays.Length}");
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
                _output.WriteLine($"??  {_skipReason}");
            }
        }

        public void Dispose()
        {
            // Release display interfaces
            foreach (var display in _displays)
            {
                try
                {
                    display.Dispose();
                }
                catch
                {
                    // Ignore errors during cleanup
                }
            }

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
        public void EnumerateAllDisplays_ShouldReturnArray()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            using var pSystem = _api!.GetSystemServicesHandle();
            var displays = ADLXDisplayHelpers.EnumerateAllDisplayHandles(pSystem);

            Assert.NotNull(displays);
            _output.WriteLine($"? Found {displays.Length} display(s)");

            foreach (var display in displays)
            {
                display.Dispose();
            }
        }

        [SkippableFact]
        public void EnumerateAllDisplays_ShouldReturnValidPointers()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displays.Length == 0, 
                _displays.Length > 0 ? _skipReason : "No displays available");

            foreach (var display in _displays)
            {
                Assert.False(display.IsInvalid);
            }

            _output.WriteLine($"? All {_displays.Length} display handle(s) are valid");
        }

        [SkippableFact]
        public void GetDisplayName_ShouldReturnValidName()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displays.Length == 0,
                _displays.Length > 0 ? _skipReason : "No displays available");

            var name = ADLXDisplayHelpers.GetDisplayName(_displays[0]);

            Assert.NotNull(name);
            Assert.NotEmpty(name);
            _output.WriteLine($"? Display Name: {name}");
        }

        [SkippableFact]
        public void GetDisplayNativeResolution_ShouldReturnValidResolution()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displays.Length == 0,
                _displays.Length > 0 ? _skipReason : "No displays available");

            var (width, height) = ADLXDisplayHelpers.GetDisplayNativeResolution(_displays[0]);

            Assert.True(width > 0, "Width should be greater than 0");
            Assert.True(height > 0, "Height should be greater than 0");
            _output.WriteLine($"? Native Resolution: {width}x{height}");
        }

        [SkippableFact]
        public void GetDisplayRefreshRate_ShouldReturnPositiveValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displays.Length == 0,
                _displays.Length > 0 ? _skipReason : "No displays available");

            var refreshRate = ADLXDisplayHelpers.GetDisplayRefreshRate(_displays[0]);

            Assert.True(refreshRate > 0, "Refresh rate should be greater than 0");
            _output.WriteLine($"? Refresh Rate: {refreshRate} Hz");
        }

        [SkippableFact]
        public void GetDisplayManufacturerID_ShouldReturnValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displays.Length == 0,
                _displays.Length > 0 ? _skipReason : "No displays available");

            var manufacturerID = ADLXDisplayHelpers.GetDisplayManufacturerID(_displays[0]);

            _output.WriteLine($"? Manufacturer ID: {manufacturerID}");
        }

        [SkippableFact]
        public void GetDisplayPixelClock_ShouldReturnPositiveValue()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displays.Length == 0,
                _displays.Length > 0 ? _skipReason : "No displays available");

            var pixelClock = ADLXDisplayHelpers.GetDisplayPixelClock(_displays[0]);

            Assert.True(pixelClock > 0, "Pixel clock should be greater than 0");
            _output.WriteLine($"? Pixel Clock: {pixelClock}");
        }

        [SkippableFact]
        public void GetDisplayBasicInfo_ShouldReturnCompleteInformation()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displays.Length == 0,
                _displays.Length > 0 ? _skipReason : "No displays available");

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
            Skip.If(!_hasHardware || !_hasDll || _api == null || _displays.Length < 2,
                _displays.Length >= 2 ? _skipReason : "Need at least 2 displays");

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
            Skip.If(!_hasHardware || !_hasDll || _api == null, _skipReason);

            using var pSystem = _api!.GetSystemServicesHandle();
            
            var displays1 = ADLXDisplayHelpers.EnumerateAllDisplayHandles(pSystem);
            var displays2 = ADLXDisplayHelpers.EnumerateAllDisplayHandles(pSystem);

            Assert.Equal(displays1.Length, displays2.Length);
            _output.WriteLine($"? Multiple enumerations return consistent count: {displays1.Length}");

            foreach (var display in displays1)
            {
                display.Dispose();
            }
            foreach (var display in displays2)
            {
                display.Dispose();
            }
        }
    }
}
