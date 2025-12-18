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
        private readonly ADLXDisplayServicesHelper? _displayHelper;
        private readonly AdlxInterfaceHandle? _displayHandle;

        public DisplaySettingsTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                _displayHelper = new ADLXDisplayServicesHelper(_system.GetDisplayServicesNative());

                var displayHandles = _displayHelper.EnumerateDisplayHandles();
                if (displayHandles.Length == 0)
                {
                    _skipReason = "No displays found.";
                    return;
                }

                _displayHandle = displayHandles[0];
                for (int i = 1; i < displayHandles.Length; i++)
                {
                    displayHandles[i].Dispose();
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
            if (_displayHandle.HasValue) _displayHandle.Value.Dispose();
            _displayHelper?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanGetDisplaySettings()
        {
            Skip.If(_api == null || _system == null || _displayHelper == null || !_displayHandle.HasValue, _skipReason);

            var displayServices = _displayHelper.GetDisplayServicesNative();
            var display = _displayHandle.Value.As<IADLXDisplay>();

            var gamma = ADLXDisplaySettingsHelpers.GetGamma(displayServices, display);
            _output.WriteLine($"Gamma supported: {gamma.IsSupported}");

            var gamut = ADLXDisplaySettingsHelpers.GetGamut(displayServices, display);
            _output.WriteLine($"Gamut supported: {gamut.IsGamutSupported}");

            var customColor = ADLXDisplaySettingsHelpers.GetCustomColor(displayServices, display);
            _output.WriteLine($"Custom Color supported: {customColor.IsSupported}");
        }
    }
}