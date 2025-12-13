using System;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Tests for Desktop and Eyefinity services.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class DesktopServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly string _skipReason = string.Empty;
        private readonly ADLXSystemServices? _system;

        public DesktopServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                _system = _api.GetSystemServicesFacade();
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanEnumerateDesktops()
        {
            Skip.If(_api == null || _system == null, _skipReason);

            var desktops = _system.EnumerateAllDesktops().ToList();
            _output.WriteLine($"Found {desktops.Count} desktop(s).");
            Assert.NotNull(desktops);
        }

        [SkippableFact]
        public void CanGetDesktopProfiles()
        {
            Skip.If(_api == null || _system == null, _skipReason);

            var desktops = _system.EnumerateAllDesktops();
            if (!desktops.Any())
            {
                Skip.If(true, "No desktops found.");
            }

            foreach (var desktop in desktops)
            {
                using (desktop)
                {
                    var profile = desktop.GetProfile();
                    _output.WriteLine($"Desktop {profile.Type} {profile.Width}x{profile.Height} at ({profile.TopLeftX},{profile.TopLeftY}) displays: {profile.Displays.Count}");
                }
            }
        }
    }
}