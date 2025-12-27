using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Display settings exercised via the display facade.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public sealed unsafe class DisplaySettingsTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly ADLXDisplay? _display;
        private readonly string _skipReason = string.Empty;

        public DisplaySettingsTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                var displays = _system.EnumerateDisplays();
                if (displays.Count == 0)
                {
                    _skipReason = "No displays found.";
                    return;
                }

                _display = displays[0];
                for (int i = 1; i < displays.Count; i++)
                {
                    displays[i].Dispose();
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
            _display?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetDisplaySettings()
        {
            Skip.If(_api == null || _system == null || _display == null, _skipReason);

            var gamma = _display.GetGamma();
            _output.WriteLine($"Gamma supported: {gamma.IsSupported}");

            var gamut = _display.GetGamut();
            _output.WriteLine($"Gamut supported: {gamut.IsGamutSupported}");

            var customColor = _display.GetCustomColor();
            _output.WriteLine($"Custom Color supported: {customColor.IsSupported}");
        }
    }
}
