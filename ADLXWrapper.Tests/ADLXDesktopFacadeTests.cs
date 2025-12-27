using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Desktop facade tests.
    /// </summary>
    [SupportedOSPlatform("windows")]
    [Collection("DisplaySerial")]
    public sealed unsafe class ADLXDesktopFacadeTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly ADLXDesktopServicesHelper? _desktopHelper;
        private readonly ADLXDesktop? _desktop;
        private readonly string _skipReason = string.Empty;

        public ADLXDesktopFacadeTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = _api.GetSystemServices();

                // Need desktop services to enumerate desktops.
                if (!_system.TryGetDesktopServicesNative(out var desktopServices) ||
                    !_system.TryGetDisplayServicesNative(out var displayServices))
                {
                    _skipReason = "Desktop or display services not supported on this system.";
                    return;
                }

                _desktopHelper = new ADLXDesktopServicesHelper(desktopServices, displayServices);

                var desktops = _desktopHelper.EnumerateADLXDesktops();
                if (desktops.Count == 0)
                {
                    _skipReason = "No desktops found.";
                    return;
                }
                _desktop = desktops[0];
                for (int i = 1; i < desktops.Count; i++) desktops[i].Dispose();
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _skipReason = "Desktop services are not supported on this system.";
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _desktopHelper?.Dispose();
            _desktop?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void DesktopInfo_ShouldBeAccessible()
        {
            Skip.If(_api == null || _system == null || _desktop == null, _skipReason);

            var info = _desktop.Identity;
            _output.WriteLine($"Desktop type: {info.Type}, size: {info.Width}x{info.Height}, origin: ({info.TopLeftX},{info.TopLeftY})");
            Assert.True(info.Width >= 0);
            Assert.True(info.Height >= 0);
        }

        [SkippableFact]
        public void CanEnumerateDisplaysForDesktop()
        {
            Skip.If(_api == null || _system == null || _desktop == null, _skipReason);

            try
            {
                var displays = _desktop.EnumerateDisplaysForDesktop();
                _output.WriteLine($"Desktop has {displays.Count} display(s).");
                foreach (var d in displays) d.Dispose();
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Display services not supported for this desktop.");
            }
        }
    }
}
