using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Facade-level display tests that avoid native pointers.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public sealed class DisplayFacadeTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly AdlxDisplay? _display;
        private readonly string _skipReason = string.Empty;

        public DisplayFacadeTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = _api.GetSystemServices();

                var displays = _system.EnumerateDisplays();
                if (displays.Count == 0)
                {
                    _skipReason = "No displays found.";
                    return;
                }

                // Take the first display and dispose the rest.
                _display = displays[0];
                for (int i = 1; i < displays.Count; i++)
                {
                    displays[i].Dispose();
                }
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _skipReason = "Display services are not supported on this system.";
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        [SkippableFact]
        public void Facade_Getters_DoNotRequirePointers()
        {
            Skip.If(_api == null || _system == null || _display == null, _skipReason);

            var vsr = _display.GetVirtualSuperResolutionState();
            _output.WriteLine($"VSR supported: {vsr.supported}, enabled: {vsr.enabled}");

            var scaling = _display.GetScalingMode();
            _output.WriteLine($"Scaling supported: {scaling.supported}, mode: {scaling.mode}");

            var freesync = _display.GetFreeSyncState();
            _output.WriteLine($"FreeSync supported: {freesync.supported}, enabled: {freesync.enabled}");
        }

        public void Dispose()
        {
            _display?.Dispose();
            _system?.Dispose();
            _api?.Dispose();
        }
    }
}
