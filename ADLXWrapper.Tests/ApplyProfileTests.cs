using System;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Round-trip profile apply for displays and desktops using the facade APIs.
    /// Skips on systems without AMD hardware or the ADLX DLL.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class ApplyProfileTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly ADLXSystemServices? _sys;
        private readonly string _skipReason = string.Empty;

        public ApplyProfileTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                _sys = _api.GetSystemServicesFacade();
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _sys?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void DisplayProfile_RoundTrips_WithSkipLogging()
        {
            Skip.If(_api == null || _sys == null, _skipReason);
            Skip.If(!ADLXHardwareDetection.HasAMDGPU(out var hwReason), hwReason);
            if (!ADLXApi.IsADLXDllAvailable(out var dllReason))
            {
                Skip.If(true, dllReason);
            }

            var display = _sys.EnumerateAllDisplays().FirstOrDefault();
            if (display == null)
            {
                Skip.If(true, "No displays found.");
            }

            using (display!)
            {
                var profile = display.GetProfile();
                var skips = new System.Collections.Generic.List<string>();
                display.ApplyProfile(profile, s => skips.Add(s));
                _output.WriteLine($"Applied profile to display {display.Name}; skips: {string.Join("; ", skips)}");
            }
        }

        [SkippableFact]
        public void DesktopProfile_RoundTrips()
        {
            Skip.If(_api == null || _sys == null, _skipReason);
            Skip.If(!ADLXHardwareDetection.HasAMDGPU(out var hwReason), hwReason);
            if (!ADLXApi.IsADLXDllAvailable(out var dllReason))
            {
                Skip.If(true, dllReason);
            }

            var desktop = _sys.EnumerateAllDesktops().FirstOrDefault();
            if (desktop == null)
            {
                Skip.If(true, "No desktops found.");
            }

            using (desktop!)
            {
                var profile = desktop.GetProfile();
                desktop.ApplyProfile(profile);
                _output.WriteLine($"Applied desktop profile {profile.Type} {profile.Width}x{profile.Height} at ({profile.TopLeftX},{profile.TopLeftY})");
            }
        }
    }
}
