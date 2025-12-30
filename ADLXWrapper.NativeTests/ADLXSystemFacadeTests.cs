using System;
using System.Runtime.Versioning;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// System-level facade smoke tests.
    /// </summary>
    [SupportedOSPlatform("windows")]
    [Collection("DisplaySerial")]
    public sealed unsafe class ADLXSystemFacadeTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApiHelper? _api;
        private readonly ADLXSystemServicesHelper? _system;
        private readonly string _skipReason = string.Empty;

        public ADLXSystemFacadeTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = _api.GetSystemServices();
            }
            catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _skipReason = "ADLX not supported on this system.";
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
        public void CanEnumerateDisplaysAndGpus()
        {
            Skip.If(_api == null || _system == null, _skipReason);

            var displays = _system.EnumerateDisplays();
            var gpus = _system.EnumerateADLXGPUs();

            _output.WriteLine($"Displays: {displays.Count}, GPUs: {gpus.Count}");
            foreach (var d in displays) _output.WriteLine($"Display: {d.Name} ({d.UniqueId})");
            foreach (var g in gpus) _output.WriteLine($"GPU: {g.Name} ({g.UniqueId})");

            foreach (var d in displays) d.Dispose();
            foreach (var g in gpus) g.Dispose();
        }
    }
}
