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
                if (!_system.TryGetDesktopServicesNative(out var desktopServices) ||
                    !_system.TryGetDisplayServicesNative(out var displayServices))
                {
                    _skipReason = "Desktop or display services not supported by this ADLX system.";
                    return;
                }

                _desktopHelper = new ADLXDesktopServicesHelper(desktopServices, displayServices);
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

        [SkippableFact]
        public void EnumerateDesktopDisplays_ShouldReturnDisplayInfos()
        {
            Skip.If(_api == null || _system == null || _desktopHelper == null, _skipReason);

            try
            {
                var handles = _desktopHelper.EnumerateDesktopHandles();
                Skip.If(handles.Length == 0, "No desktops found.");

                using var first = handles[0];
                var displays = _desktopHelper.EnumerateDesktopDisplays(first.As<IADLXDesktop>());
                _output.WriteLine($"Desktop has {displays.Count} display(s).");
                if (displays.Count > 0)
                {
                    var info = displays[0];
                    _output.WriteLine($"Display: {info.Name}, {info.Width}x{info.Height}, type={info.Type}, connector={info.ConnectorType}");
                    Assert.True(info.Width > 0);
                    Assert.True(info.Height > 0);
                }

                for (int i = 1; i < handles.Length; i++)
                {
                    handles[i].Dispose();
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Desktop services are not supported on this system.");
            }
        }
    }
}
