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
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXDisplay* _display;
        private readonly IADLXDisplayServices* _displayServices;

        public DisplaySettingsTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                var system = _system.GetSystemServicesNative();
                _displayServices = ADLXDisplayHelpers.GetDisplayServices(system);

                IADLXDisplayList* displayList = null;
                var result = _displayServices->GetDisplays(&displayList);
                if (result != ADLX_RESULT.ADLX_OK || displayList == null || displayList->Size() == 0)
                {
                    _skipReason = "No displays found.";
                    return;
                }
                IADLXDisplay* pDisplay = null;
                displayList->At(0, &pDisplay);
                _display = pDisplay;
                ((IADLXInterface*)displayList)->Release();
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
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetDisplaySettings()
        {
            Skip.If(_api == null || _system == null || _display == null || _displayServices == null, _skipReason);

            var gamma = ADLXDisplaySettingsHelpers.GetGamma(_displayServices, _display);
            _output.WriteLine($"Gamma supported: {gamma.IsSupported}");

            var gamut = ADLXDisplaySettingsHelpers.GetGamut(_displayServices, _display);
            _output.WriteLine($"Gamut supported: {gamut.IsGamutSupported}");

            var customColor = ADLXDisplaySettingsHelpers.GetCustomColor(_displayServices, _display);
            _output.WriteLine($"Custom Color supported: {customColor.IsSupported}");
        }
    }
}