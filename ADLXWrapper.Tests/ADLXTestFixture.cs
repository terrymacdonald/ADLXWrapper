using System;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests
{
    /// <summary>
    /// Shared test fixture for ADLX tests
    /// Performs hardware detection, DLL check, and ADLX initialization once per test class
    /// </summary>
    public class ADLXTestFixture : IDisposable
    {
        /// <summary>
        /// True if AMD GPU hardware was detected via PCI
        /// </summary>
        public bool HasAMDHardware { get; private set; }

        /// <summary>
        /// True if ADLX DLL is available in the search path
        /// </summary>
        public bool HasADLXDll { get; private set; }

        /// <summary>
        /// The initialized ADLX API instance (null if initialization failed)
        /// </summary>
        public ADLXApi? Api { get; private set; }

        /// <summary>
        /// Enumerated GPUs from ADLX (empty if none found)
        /// </summary>
        public IntPtr[] GPUs { get; private set; }

        /// <summary>
        /// Reason why tests should be skipped (empty if tests can run)
        /// </summary>
        public string SkipReason { get; private set; }

        /// <summary>
        /// Detected AMD GPU names from PCI detection
        /// </summary>
        public string[] DetectedGPUNames { get; private set; }

        public ADLXTestFixture()
        {
            GPUs = Array.Empty<IntPtr>();
            SkipReason = string.Empty;
            DetectedGPUNames = Array.Empty<string>();

            // Step 1: Check for AMD Hardware via PCI
            if (!HardwareDetection.HasAMDGPU(out string hwError))
            {
                HasAMDHardware = false;
                HasADLXDll = false;
                SkipReason = hwError;
                return;
            }

            HasAMDHardware = true;
            DetectedGPUNames = HardwareDetection.GetAMDGPUNames();

            // Step 2: Check for ADLX DLL
            if (!ADLXApi.IsADLXDllAvailable(out string dllError))
            {
                HasADLXDll = false;
                SkipReason = dllError;
                return;
            }

            HasADLXDll = true;

            // Step 3: Try to initialize ADLX
            try
            {
                Api = ADLXApi.Initialize();
                GPUs = Api.EnumerateGPUs();

                if (GPUs.Length == 0)
                {
                    SkipReason = "ADLX initialized but no GPUs enumerated";
                }
            }
            catch (Exception ex)
            {
                SkipReason = $"ADLX initialization failed: {ex.Message}";
            }
        }

        /// <summary>
        /// True if all prerequisites are met and tests can run
        /// </summary>
        public bool CanRunTests => HasAMDHardware && HasADLXDll && Api != null && GPUs.Length > 0;

        /// <summary>
        /// Write diagnostic information to test output
        /// </summary>
        public void WriteDiagnostics(ITestOutputHelper output)
        {
            if (!CanRunTests)
            {
                output.WriteLine($"?? Tests will be skipped: {SkipReason}");

                if (!HasAMDHardware)
                {
                    output.WriteLine("   ? No AMD GPU hardware detected via PCI");
                }
                else if (DetectedGPUNames.Length > 0)
                {
                    output.WriteLine($"   ? AMD GPU detected: {string.Join(", ", DetectedGPUNames)}");
                }

                if (HasAMDHardware && !HasADLXDll)
                {
                    output.WriteLine("   ? ADLX DLL not found in search path");
                    output.WriteLine("   ? Please ensure AMD Adrenalin drivers are installed");
                }
            }
            else
            {
                output.WriteLine($"? Test environment ready: {GPUs.Length} GPU(s) available");
                if (DetectedGPUNames.Length > 0)
                {
                    output.WriteLine($"   Detected GPUs: {string.Join(", ", DetectedGPUNames)}");
                }
                output.WriteLine($"   ADLX Version: {Api!.GetVersion()}");
            }
        }

        public void Dispose()
        {
            // Release all GPU interfaces
            foreach (var gpu in GPUs)
            {
                try
                {
                    ADLXHelpers.ReleaseInterface(gpu);
                }
                catch
                {
                    // Ignore errors during cleanup
                }
            }

            // Dispose ADLX API
            Api?.Dispose();
        }
    }
}
