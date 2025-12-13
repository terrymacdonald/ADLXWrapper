using System;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Tests for various display settings services.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class DisplaySettingsTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxDisplay? _display;

        public DisplaySettingsTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                var displays = _api.GetSystemServices().EnumerateAllDisplays();
                _display = displays.FirstOrDefault();
                if (_display == null)
                {
                    _skipReason = "No displays found.";
                }
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _display?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetDisplaySettings()
        {
            Skip.If(_api == null || _display == null, _skipReason);

            var gamma = _display.GetGamma();
            _output.WriteLine($"Gamma supported: {gamma.Supported}");

            var gamut = _display.GetGamut();
            _output.WriteLine($"Gamut supported: {gamut.Supported}");

            var customColor = _display.GetCustomColor();
            _output.WriteLine($"Custom Color supported: {customColor.IsHueSupported || customColor.IsSaturationSupported || customColor.IsBrightnessSupported || customColor.IsContrastSupported || customColor.IsTemperatureSupported}");
        }
    }
}