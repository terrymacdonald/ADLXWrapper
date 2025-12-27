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
    [Collection("DisplaySerial")]
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
                _displayServices = new ADLXDisplayServicesHelper(_system.GetDisplayServicesNative(), _system.GetDesktopServicesNative());
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

            if (!_displayServices.TryEnumerateDisplays(out var displays))
            {
                Skip.If(true, "Display services are not supported on this system.");
            }

            _output.WriteLine($"Found {displays.Count} display(s).");
            foreach (var d in displays)
            {
                _output.WriteLine($"Display: {d.Name}, Id: {d.UniqueId}");
                d.Dispose();
            }
            Assert.NotNull(displays);
        }

        [SkippableFact]
        public void CanEnumerateADLXDisplayFacades()
        {
            Skip.If(_api == null || _system == null || _displayServices == null, _skipReason);

            if (!_displayServices.TryEnumerateADLXDisplays(out var facades))
            {
                Skip.If(true, "Display services are not supported on this system.");
            }

            _output.WriteLine($"Found {facades.Count} display façade(s).");
            foreach (var d in facades.ToList())
            {
                _output.WriteLine($"Display: {d.Name}, Id: {d.UniqueId}");
                d.Dispose();
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

        [SkippableFact]
        public void EnumerateDisplayInfos_ShouldExposeDimensions()
        {
            Skip.If(_api == null || _system == null || _displayServices == null, _skipReason);

            if (!_displayServices.TryEnumerateDisplayHandles(out var handles))
            {
                Skip.If(true, "Display services are not supported on this system.");
            }

            Skip.If(handles.Length == 0, "No displays found.");

            using var first = handles[0];
            var info = _displayServices.GetDisplayInfo(first.As<IADLXDisplay>());
            _output.WriteLine($"Display: {info.Name}, {info.NativeResolutionWidth}x{info.NativeResolutionHeight} @ {info.RefreshRate}Hz, connector={info.ConnectorType}, type={info.Type}");

            Assert.True(info.NativeResolutionWidth > 0);
            Assert.True(info.NativeResolutionHeight > 0);
            Assert.True(info.RefreshRate > 0);

            for (int i = 1; i < handles.Length; i++)
            {
                handles[i].Dispose();
            }
        }
    }

    /// <summary>
    /// Serializes display enumeration tests to avoid native driver instability when run in parallel.
    /// </summary>
    [CollectionDefinition("DisplaySerial", DisableParallelization = true)]
    public class DisplaySerialDefinition
    {
    }
}
