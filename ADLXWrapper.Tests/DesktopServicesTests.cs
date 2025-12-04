using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Desktop and Eyefinity service smoke tests (enumeration only; no config changes).
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class DesktopServicesTests : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly ADLXApi? _api;
        private readonly bool _hasHardware;
        private readonly bool _hasDll;
        private readonly string _skipReason = string.Empty;
        private readonly AdlxInterfaceHandle[] _desktops = Array.Empty<AdlxInterfaceHandle>();
        private readonly AdlxInterfaceHandle? _desktopServices;

        public DesktopServicesTests(ITestOutputHelper output)
        {
            _output = output;
            _desktopServices = null;

            if (!HardwareDetection.HasAMDGPU(out var hwError))
            {
                _hasHardware = false;
                _hasDll = false;
                _skipReason = hwError;
                _output.WriteLine($"??  {hwError}");
                return;
            }
            _hasHardware = true;

            if (!ADLXApi.IsADLXDllAvailable(out var dllError))
            {
                _hasDll = false;
                _skipReason = dllError;
                _output.WriteLine($"??  {dllError}");
                return;
            }
            _hasDll = true;

            try
            {
                _api = ADLXApi.Initialize();
                using var system = _api.GetSystemServicesHandle();
                var ds = ADLXDesktopHelpers.GetDesktopServicesHandle(system);
                _desktopServices = ds;
                _desktops = ADLXDesktopHelpers.EnumerateAllDesktopHandles(ds);
                _output.WriteLine($"? Desktop services acquired. Desktops: {_desktops.Length}");
            }
            catch (Exception ex)
            {
                _skipReason = $"ADLX initialization failed: {ex.Message}";
                _output.WriteLine($"??  {_skipReason}");
            }
        }

        public void Dispose()
        {
            foreach (var desktop in _desktops)
            {
                try { desktop.Dispose(); } catch { }
            }

            try { _desktopServices?.Dispose(); } catch { }
            _api?.Dispose();
        }

        [SkippableFact]
        public void GetDesktopServices_ShouldSucceed()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _desktopServices is null, _skipReason);
            Assert.False(_desktopServices!.IsInvalid);
        }

        [SkippableFact]
        public void EnumerateDesktops_ShouldReturnHandles()
        {
            Skip.If(!_hasHardware || !_hasDll || _api == null || _desktopServices is null, _skipReason);

            _output.WriteLine($"? Enumerated {_desktops.Length} desktop(s)");
            foreach (var desktop in _desktops)
            {
                Assert.False(desktop.IsInvalid);
                var type = ADLXDesktopHelpers.GetDesktopType(desktop);
                var size = ADLXDesktopHelpers.GetDesktopSize(desktop);
                _output.WriteLine($"  Desktop: {type}, {size.Width}x{size.Height}");
            }
        }
    }
}
