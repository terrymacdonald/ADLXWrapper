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
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly string _skipReason = string.Empty;
        private readonly IADLXDesktopServices* _desktopServices;

        public DesktopServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                var system = _system.GetSystemServicesNative();
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
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanEnumerateDesktops()
        {
            Skip.If(_api == null || _system == null || _desktopServices == null, _skipReason);

            var desktops = ADLXDesktopHelpers.EnumerateAllDesktops(_system.GetSystemServicesNative()).ToList();
            _output.WriteLine($"Found {desktops.Count} desktop(s).");
            Assert.NotNull(desktops);
        }

        [SkippableFact]
        public void CanGetEyefinityInfo()
        {
            Skip.If(_api == null || _system == null || _desktopServices == null, _skipReason);

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

        [SkippableFact]
        public void CanEnumerateAdlxDesktops()
        {
            Skip.If(_api == null || _system == null || _desktopServices == null, _skipReason);

            try
            {
                using var helper = new ADLXDesktopServicesHelper(_desktopServices, _system.GetDisplayServicesNative());
                var desktops = helper.EnumerateAdlxDesktops().ToList();
                _output.WriteLine($"Found {desktops.Count} desktop façade(s).");
                foreach (var d in desktops)
                {
                    _output.WriteLine($"Desktop type: {d.Type}, Width: {d.Width}, Height: {d.Height}");
                    d.Dispose();
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Desktop services are not supported on this system.");
            }
        }

        [SkippableFact]
        public void CanAddAndRemoveDesktopListListener()
        {
            Skip.If(_api == null || _system == null || _desktopServices == null, _skipReason);

            try
            {
                using var helper = new ADLXDesktopServicesHelper(_desktopServices, _system.GetDisplayServicesNative());
                var handle = helper.AddDesktopListEventListener(_ => { return; });
                helper.RemoveDesktopListEventListener(handle);
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Desktop change handling is not supported on this system.");
            }
        }
    }
}
