using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Facade tests for ADLXDisplayServicesHelper via system helper.
    /// </summary>
    [SupportedOSPlatform("windows")]
    [Collection("DisplaySerial")]
    public sealed unsafe class ADLXDisplayServicesHelperFacadeTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly ADLXDisplayServicesHelper? _displayHelper;
        private readonly string _skipReason = string.Empty;

        public ADLXDisplayServicesHelperFacadeTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                if (!_system.TryGetDisplayServicesNative(out var displayServices) ||
                    !_system.TryGetDesktopServicesNative(out var desktopServices))
                {
                    _skipReason = "Display or desktop services not supported on this system.";
                    return;
                }
                _displayHelper = new ADLXDisplayServicesHelper(displayServices, desktopServices);
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _skipReason = "Display services not supported on this system.";
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _displayHelper?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanEnumerateDisplayHandles()
        {
            Skip.If(_api == null || _system == null || _displayHelper == null, _skipReason);

            if (!_displayHelper.TryEnumerateDisplayHandles(out var handles))
            {
                Skip.If(true, "Display services are not supported on this system.");
            }

            _output.WriteLine($"Found {handles.Length} display handle(s).");
            foreach (var h in handles) h.Dispose();
        }
    }
}
