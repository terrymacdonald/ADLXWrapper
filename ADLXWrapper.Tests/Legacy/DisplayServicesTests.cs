using System;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Display enumeration tests using only the display facade.
    /// </summary>
    [SupportedOSPlatform("windows")]
    [Collection("DisplaySerial")]
    public sealed unsafe class DisplayServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly IReadOnlyList<ADLXDisplay>? _displays;
        private readonly string _skipReason = string.Empty;

        public DisplayServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                _displays = _system.EnumerateDisplays();
                if (_displays.Count == 0)
                {
                    _skipReason = "No displays found.";
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _skipReason = "Display services are not supported on this system.";
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            if (_displays != null)
            {
                foreach (var d in _displays)
                {
                    d.Dispose();
                }
            }
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanEnumerateDisplays()
        {
            Skip.If(_api == null || _system == null || _displays == null || _displays.Count == 0, _skipReason);

            _output.WriteLine($"Found {_displays.Count} display(s).");
            foreach (var d in _displays)
            {
                _output.WriteLine($"Display: {d.Name}, Id: {d.UniqueId}");
            }
            Assert.NotNull(_displays);
        }

        [SkippableFact]
        public void EnumerateDisplayInfos_ShouldExposeDimensions()
        {
            Skip.If(_api == null || _system == null || _displays == null || _displays.Count == 0, _skipReason);

            var first = _displays[0];
            var info = first.GetDisplayInfo();
            _output.WriteLine($"Display: {info.Name}, {info.NativeResolutionWidth}x{info.NativeResolutionHeight} @ {info.RefreshRate}Hz, connector={info.ConnectorType}, type={info.Type}");

            Assert.True(info.NativeResolutionWidth > 0);
            Assert.True(info.NativeResolutionHeight > 0);
            Assert.True(info.RefreshRate > 0);
        }
    }

    /// <summary>
    /// Serializes display enumeration tests to avoid native driver instability when run in parallel.
    /// </summary>
    [CollectionDefinition("DisplaySerial", DisableParallelization = true)]
    public class DisplaySerialDefinition
    {
    }
}
