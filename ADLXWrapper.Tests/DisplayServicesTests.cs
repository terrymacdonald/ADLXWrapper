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
    public class DisplayServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly string _skipReason = string.Empty;

        public DisplayServicesTests(ITestOutputHelper output)
        {
            _output = output;
            try
            {
                _api = ADLXApi.Initialize();
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        public void Dispose()
        {
            _api?.Dispose();
        }

        [SkippableFact]
        public void CanEnumerateDisplays()
        {
            Skip.If(_api == null, _skipReason);

            var displays = _api.GetSystemServicesFacade().EnumerateAllDisplays().ToList();
            _output.WriteLine($"Found {displays.Count} display(s).");
            Assert.NotNull(displays);
        }
    }
}