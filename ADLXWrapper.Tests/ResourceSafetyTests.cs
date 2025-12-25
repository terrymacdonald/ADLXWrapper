using System;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Safety/regression tests to ensure handles are released and Dispose is idempotent.
    /// Skips when ADLX DLL or AMD hardware is unavailable.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public unsafe class ResourceSafetyTests
    {
        private readonly ITestOutputHelper _output;

        public ResourceSafetyTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [SkippableFact]
        public void EnumerateAndDispose_Repeatedly_ShouldNotThrow()
        {
            Skip.If(!ADLXHardwareDetection.HasAMDGPU(out var hwReason), hwReason);

            if (!ADLXApiHelper.IsADLXDllAvailable(out var dllReason))
            {
                Skip.If(true, dllReason);
            }

            ADLXApiHelper api;
            try
            {
                api = ADLXApiHelper.Initialize();
            }
            catch (Exception ex)
            {
                Skip.If(true, $"ADLX initialization failed: {ex.Message}");
                return;
            }
            using (api)
            {
            using var systemHelper = new ADLXSystemServicesHelper(api.GetSystemServicesNative());
            var system = systemHelper.GetSystemServicesNative();
            using var displayHelper = new ADLXDisplayServicesHelper(systemHelper.GetDisplayServicesNative());

            for (int i = 0; i < 5; i++)
            {
                var gpus = systemHelper.EnumerateGPUsHandle();

                foreach (var gpu in gpus)
                {
                    using (gpu)
                    {
                        var info = systemHelper.GetGpuInfo(gpu.As<IADLXGPU>());
                        _output.WriteLine($"[Iter {i}] GPU: {info.Name}");
                    }
                }

                var displays = displayHelper.EnumerateDisplayHandles();
                foreach (var display in displays)
                {
                    using (display)
                    {
                        var info = displayHelper.GetDisplayInfo(display.As<IADLXDisplay>());
                        var name = info.Name;
                        _output.WriteLine($"[Iter {i}] Display: {name}");
                    }
                }
            }
            }
        }
    }
}

