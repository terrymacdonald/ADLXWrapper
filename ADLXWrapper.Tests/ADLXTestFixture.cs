using Xunit;
using Xunit.Abstractions;
using ADLXWrapper;

namespace ADLXWrapper.Tests;

/// <summary>
/// Shared test fixture for ADLX tests that handles initialization and hardware detection
/// </summary>
public class ADLXTestFixture : IDisposable
{
    private readonly EnhancedADLXHelper _helper;
    private bool _isInitialized;

    public IADLXSystem? System { get; private set; }
    public IADLXSystem1? System1 { get; private set; }
    public IADLXSystem2? System2 { get; private set; }
    public IADLXGPU? FirstGPU { get; private set; }  // Cache first GPU
    public bool IsAMDHardwareAvailable { get; private set; }
    public bool IsADLXSupported { get; private set; }
    public bool SupportsSystem1 { get; private set; }
    public bool SupportsSystem2 { get; private set; }
    public string? SkipReason { get; private set; }
    public HardwareCapabilities Capabilities { get; }

    public ADLXTestFixture()
    {
        _helper = new EnhancedADLXHelper();
        Capabilities = new HardwareCapabilities();

        // Attempt initialization
        InitializeADLX();

        // Detect hardware capabilities if initialized
        if (_isInitialized && System != null)
        {
            DetectCapabilities();
        }
    }

    private void InitializeADLX()
    {
        // Check if ADLX runtime is available
        if (!ADLX.IsADLXRuntimeAvailable())
        {
            SkipReason = "ADLX runtime not available - AMD drivers not installed or not AMD hardware";
            IsADLXSupported = false;
            IsAMDHardwareAvailable = false;
            return;
        }

        IsAMDHardwareAvailable = true;

        // Try to initialize ADLX
        ADLX_RESULT result = _helper.Initialize();

        if (result != ADLX_RESULT.ADLX_OK)
        {
            SkipReason = $"ADLX initialization failed: {ADLX.GetADLXErrorDescription(result)}";
            IsADLXSupported = false;
            return;
        }

        // Get system services
        System = _helper.GetSystemServices();
        if (System == null)
        {
            SkipReason = "Failed to get system services";
            IsADLXSupported = false;
            return;
        }

        _isInitialized = true;
        IsADLXSupported = true;

        // Detect system interface versions and get extended interfaces if supported
        SupportsSystem1 = ADLX.SupportsSystem1Interface(System);
        if (SupportsSystem1)
        {
            // Query for IADLXSystem1 interface
            System1 = ADLX.QuerySystem1Interface(System);
        }

        SupportsSystem2 = ADLX.SupportsSystem2Interface(System);
        if (SupportsSystem2)
        {
            // Query for IADLXSystem2 interface
            System2 = ADLX.QuerySystem2Interface(System);
        }
    }

    private void DetectCapabilities()
    {
        if (System == null) return;

        // Detect GPU capabilities
        var gpuListPtr = ADLX.new_gpuListP_Ptr();
        try
        {
            var result = System.GetGPUs(gpuListPtr);
            if (result == ADLX_RESULT.ADLX_OK)
            {
                var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
                if (gpuList != null)
                {
                    Capabilities.GPUCount = gpuList.Size();

                    // Cache first GPU for use in tests
                    if (Capabilities.GPUCount > 0)
                    {
                        var gpuPtr = ADLX.new_gpuP_Ptr();
                        try
                        {
                            var atResult = gpuList.At(0, gpuPtr);
                            if (atResult == ADLX_RESULT.ADLX_OK)
                            {
                                var gpu = ADLX.gpuP_Ptr_value(gpuPtr);

                                if (gpu != null)
                                {
                                    FirstGPU = gpu;  // Cache the GPU reference
                                    Capabilities.SupportsGPU1 = ADLX.SupportsGPU1Interface(gpu);
                                    Capabilities.SupportsGPU2 = ADLX.SupportsGPU2Interface(gpu);
                                }
                            }
                        }
                        finally
                        {
                            ADLX.delete_gpuP_Ptr(gpuPtr);
                        }
                    }
                }
            }
        }
        finally
        {
            ADLX.delete_gpuListP_Ptr(gpuListPtr);
        }

        // Detect display capabilities
        var displayServicesPtr = ADLX.new_displaySerP_Ptr();
        try
        {
            var result = System.GetDisplaysServices(displayServicesPtr);
            if (result == ADLX_RESULT.ADLX_OK)
            {
                var displayServices = ADLX.displaySerP_Ptr_value(displayServicesPtr);
                if (displayServices != null)
                {
                    var displayListPtr = ADLX.new_displayListP_Ptr();
                    try
                    {
                        result = displayServices.GetDisplays(displayListPtr);

                        if (result == ADLX_RESULT.ADLX_OK)
                        {
                            var displayList = ADLX.displayListP_Ptr_value(displayListPtr);
                            if (displayList != null)
                            {
                                Capabilities.DisplayCount = displayList.Size();
                            }
                        }
                    }
                    finally
                    {
                        ADLX.delete_displayListP_Ptr(displayListPtr);
                    }
                }
            }
        }
        finally
        {
            ADLX.delete_displaySerP_Ptr(displayServicesPtr);
        }

        // Detect desktop services
        var desktopServicesPtr = ADLX.new_desktopSerP_Ptr();
        try
        {
            var result = System.GetDesktopsServices(desktopServicesPtr);
            Capabilities.SupportsDesktopServices = (result == ADLX_RESULT.ADLX_OK);
        }
        finally
        {
            ADLX.delete_desktopSerP_Ptr(desktopServicesPtr);
        }

        // Detect performance monitoring
        var perfServicesPtr = ADLX.new_performanceP_Ptr();
        try
        {
            var result = System.GetPerformanceMonitoringServices(perfServicesPtr);
            Capabilities.SupportsPerformanceMonitoring = (result == ADLX_RESULT.ADLX_OK);
        }
        finally
        {
            ADLX.delete_performanceP_Ptr(perfServicesPtr);
        }

        // Detect GPU tuning services
        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            var result = System.GetGPUTuningServices(tuningServicesPtr);
            Capabilities.SupportsGPUTuning = (result == ADLX_RESULT.ADLX_OK);
        }
        finally
        {
            ADLX.delete_gpuTuningP_Ptr(tuningServicesPtr);
        }
    }

    public void Dispose()
    {
        // Clear cached references before termination
        FirstGPU = null;
        System1 = null;
        System2 = null;
        System = null;

        if (_isInitialized)
        {
            _helper.Terminate();
        }
        _helper.Dispose();
    }
}

/// <summary>
/// Hardware capabilities detected during fixture initialization
/// </summary>
public class HardwareCapabilities
{
    public uint GPUCount { get; set; }
    public uint DisplayCount { get; set; }
    public bool SupportsGPU1 { get; set; }
    public bool SupportsGPU2 { get; set; }
    public bool SupportsDesktopServices { get; set; }
    public bool SupportsPerformanceMonitoring { get; set; }
    public bool SupportsGPUTuning { get; set; }

    public override string ToString()
    {
        return $"GPUs: {GPUCount}, Displays: {DisplayCount}, " +
               $"GPU1: {SupportsGPU1}, GPU2: {SupportsGPU2}, " +
               $"Desktop: {SupportsDesktopServices}, " +
               $"Perf: {SupportsPerformanceMonitoring}, " +
               $"Tuning: {SupportsGPUTuning}";
    }
}

/// <summary>
/// Collection definition for xUnit to share the fixture across test classes
/// </summary>
[CollectionDefinition("ADLX Tests")]
public class ADLXTestCollection : ICollectionFixture<ADLXTestFixture>
{
    // This class has no code, and is never created.
    // Its purpose is simply to be the place to apply [CollectionDefinition] and all the ICollectionFixture<> interfaces.
}