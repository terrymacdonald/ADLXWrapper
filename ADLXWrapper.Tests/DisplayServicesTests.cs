using System;
using System.Linq;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Tests for basic display enumeration.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public unsafe class DisplayServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly ADLXDisplayServicesHelper? _displayServices;
        private readonly string _skipReason = string.Empty;

        public DisplayServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
                _displayServices = new ADLXDisplayServicesHelper(_system.GetDisplayServicesNative());
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _displayServices?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanEnumerateDisplays()
        {
            Skip.If(_api == null || _system == null || _displayServices == null, _skipReason);

            try
            {
                var displays = _displayServices.EnumerateDisplays();
                _output.WriteLine($"Found {displays.Count} display(s).");
                foreach (var d in displays)
                {
                    _output.WriteLine($"Display: {d.Name}, Id: {d.UniqueId}");
                    d.Dispose();
                }
                Assert.NotNull(displays);
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Display services are not supported on this system.");
            }
        }

        [SkippableFact]
        public void CanEnumerateAdlxDisplayFacades()
        {
            Skip.If(_api == null || _system == null || _displayServices == null, _skipReason);

            try
            {
                var facades = _displayServices.EnumerateAdlxDisplays().ToList();
                _output.WriteLine($"Found {facades.Count} display façade(s).");
                foreach (var d in facades)
                {
                    _output.WriteLine($"Display: {d.Name}, Id: {d.UniqueId}");
                    d.Dispose();
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Display services are not supported on this system.");
            }
        }

        [SkippableFact]
        public void CanAddAndRemoveDisplayListListener()
        {
            Skip.If(_api == null || _system == null || _displayServices == null, _skipReason);

            try
            {
                var handle = _displayServices.AddDisplayListEventListener(_ => false);
                _displayServices.RemoveDisplayListEventListener(handle);
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                Skip.If(true, "Display change handling is not supported on this system.");
            }
        }
    }
}
