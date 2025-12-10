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
    public unsafe class DisplaySettingsTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXDisplay* _display;
        private readonly IADLXDisplayServices* _displayServices;

        public DisplaySettingsTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                var system = _api.GetSystemServices();
                _displayServices = ADLXDisplayHelpers.GetDisplayServices(system);

                _displayServices->GetDisplays(out var displayList);
                if (displayList->Size() == 0)
                {
                    _skipReason = "No displays found.";
                    return;
                }
                displayList->At(0, out _display);
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            if (_display != null) ((IUnknown*)_display)->Release();
            if (_displayServices != null) ((IUnknown*)_displayServices)->Release();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetDisplaySettings()
        {
            Skip.If(_api == null || _display == null || _displayServices == null, _skipReason);

            var gamma = ADLXDisplaySettingsHelpers.GetGamma(_displayServices, _display);
            Assert.NotNull(gamma);
            _output.WriteLine($"Gamma supported: {gamma.IsSupported}");

            var gamut = ADLXDisplaySettingsHelpers.GetGamut(_displayServices, _display);
            Assert.NotNull(gamut);
            _output.WriteLine($"Gamut supported: {gamut.IsGamutSupported}");

            var customColor = ADLXDisplaySettingsHelpers.GetCustomColor(_displayServices, _display);
            Assert.NotNull(customColor);
            _output.WriteLine($"Custom Color supported: {customColor.IsSupported}");
        }
    }
}