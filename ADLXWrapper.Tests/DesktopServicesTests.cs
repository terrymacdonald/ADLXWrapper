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
        private readonly ADLXDesktopServicesHelper? _desktopHelper;

        public DesktopServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                _desktopHelper = new ADLXDesktopServicesHelper(_system.GetDesktopServicesNative(), _system.GetDisplayServicesNative());
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

            var desktops = _desktopHelper.EnumerateDesktops().ToList();
            _output.WriteLine($"Found {desktops.Count} desktop(s).");
            Assert.NotNull(desktops);
        }

        [SkippableFact]
        public void CanGetEyefinityInfo()
        {
            Skip.If(_api == null || _system == null || _desktopHelper == null, _skipReason);

            try
            {
                var eyefinityInfo = _desktopHelper.GetSimpleEyefinity();
                _output.WriteLine($"Eyefinity supported: {eyefinityInfo.IsSupported}");
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Eyefinity is not supported on this system.");
            }
        }

        [SkippableFact]
        public void CanEnumerateADLXDesktops()
        {
            Skip.If(_api == null || _system == null || _desktopHelper == null, _skipReason);

            try
            {
                var desktops = _desktopHelper.EnumerateADLXDesktops().ToList();
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
            Skip.If(_api == null || _system == null || _desktopHelper == null, _skipReason);

            try
            {
                var handle = _desktopHelper.AddDesktopListEventListener(_ => { return; });
                _desktopHelper.RemoveDesktopListEventListener(handle);
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Desktop change handling is not supported on this system.");
            }
        }
    }
}
