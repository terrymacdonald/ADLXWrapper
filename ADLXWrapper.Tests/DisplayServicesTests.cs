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
        private readonly string _skipReason = string.Empty;

        public DisplayServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApiHelper.Initialize();
                _system = new ADLXSystemServicesHelper(_api.GetSystemServicesNative());
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
        public void CanEnumerateDisplays()
        {
            Skip.If(_api == null || _system == null, _skipReason);

            var displays = ADLXDisplayHelpers.EnumerateAllDisplays(_system.GetSystemServicesNative()).ToList();
            _output.WriteLine($"Found {displays.Count} display(s).");
            Assert.NotNull(displays);
        }
    }
}