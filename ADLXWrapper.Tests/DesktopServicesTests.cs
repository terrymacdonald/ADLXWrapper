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
    public unsafe class DesktopServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXDesktopServices* _desktopServices;

        public DesktopServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
                var system = _api.GetSystemServices();
                _desktopServices = ADLXDesktopHelpers.GetDesktopServices(system);
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            if (_desktopServices != null) ((IUnknown*)_desktopServices)->Release();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanEnumerateDesktops()
        {
            Skip.If(_api == null || _desktopServices == null, _skipReason);

            var desktops = ADLXDesktopHelpers.EnumerateAllDesktops(_api.GetSystemServices()).ToList();
            _output.WriteLine($"Found {desktops.Count} desktop(s).");
            Assert.NotNull(desktops);
        }

        [SkippableFact]
        public void CanGetEyefinityInfo()
        {
            Skip.If(_api == null || _desktopServices == null, _skipReason);

            try
            {
                var eyefinityInfo = ADLXDesktopHelpers.GetSimpleEyefinity(_desktopServices);
                _output.WriteLine($"Eyefinity supported: {eyefinityInfo.IsSupported}");
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Eyefinity is not supported on this system.");
            }
        }
    }
}