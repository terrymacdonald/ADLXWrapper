using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Facade tests for ADLXDesktopServicesHelper via system helper.
    /// </summary>
    [SupportedOSPlatform("windows")]
    [Collection("DisplaySerial")]
    public sealed unsafe class ADLXDesktopServicesHelperFacadeTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly ADLXDesktopServicesHelper? _desktopHelper;
        private readonly string _skipReason = string.Empty;

        public ADLXDesktopServicesHelperFacadeTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                if (!_system.TryGetDesktopServicesNative(out var desktopServices) ||
                    !_system.TryGetDisplayServicesNative(out var displayServices))
                {
                    _skipReason = "Desktop or display services not supported on this system.";
                    return;
                }
                _desktopHelper = new ADLXDesktopServicesHelper(desktopServices, displayServices);
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _skipReason = "Desktop services not supported on this system.";
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _desktopHelper?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanEnumerateDesktops()
        {
            Skip.If(_api == null || _system == null || _desktopHelper == null, _skipReason);

            var desktops = _desktopHelper.EnumerateDesktops();
            _output.WriteLine($"Found {desktops.Count} desktop(s).");
            foreach (var d in desktops)
            {
                _output.WriteLine($"Desktop type: {d.Type}, {d.Width}x{d.Height}");
            }
        }
    }
}
