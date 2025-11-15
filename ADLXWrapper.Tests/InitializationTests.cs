using Xunit;
using Xunit.Abstractions;
using ADLXWrapper;

namespace ADLXWrapper.Tests;

/// <summary>
/// Tests for ADLX initialization, versioning, and basic system operations
/// </summary>
[Collection("ADLX Tests")]
public class InitializationTests
{
    private readonly ADLXTestFixture _fixture;
    private readonly ITestOutputHelper _output;
    
    public InitializationTests(ADLXTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }
    
    [Fact]
    public void Test_01_ADLX_Runtime_Availability()
    {
        // This test always runs to detect hardware
        bool isAvailable = ADLX.IsADLXRuntimeAvailable();
        
        _output.WriteLine($"ADLX Runtime Available: {isAvailable}");
        
        if (!isAvailable)
        {
            _output.WriteLine("??  No AMD hardware or drivers detected");
            _output.WriteLine("    This is expected on non-AMD systems");
        }
        
        Assert.Equal(_fixture.IsAMDHardwareAvailable, isAvailable);
    }
    
    [SkippableFact]
    public void Test_02_ADLX_Initialization()
    {
        Skip.IfNot(_fixture.IsAMDHardwareAvailable, _fixture.SkipReason ?? "No AMD hardware");
        
        // Fixture already initialized, verify it worked
        Assert.True(_fixture.IsADLXSupported, "ADLX should be supported with AMD hardware");
        Assert.NotNull(_fixture.System);
        
        _output.WriteLine("? ADLX initialized successfully");
        _output.WriteLine($"  System interface: {(_fixture.System != null ? "Available" : "Not Available")}");
    }
    
    [SkippableFact]
    public void Test_03_System_Interface_Versions()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        
        _output.WriteLine("System Interface Support:");
        _output.WriteLine($"  IADLXSystem (base):  Always supported");
        _output.WriteLine($"  IADLXSystem1:        {(_fixture.SupportsSystem1 ? "? Supported" : "? Not supported")}");
        _output.WriteLine($"  IADLXSystem2:        {(_fixture.SupportsSystem2 ? "? Supported" : "? Not supported")}");
        
        // Base system always supported if initialized
        Assert.NotNull(_fixture.System);
        
        if (!_fixture.SupportsSystem2)
        {
            _output.WriteLine("\n??  IADLXSystem2 requires:");
            _output.WriteLine("    - AMD Radeon RX 6000 series or newer");
            _output.WriteLine("    - AMD Adrenalin driver 23.2.1 or newer");
        }
    }
    
    [SkippableFact]
    public void Test_04_Hardware_Capabilities_Summary()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        
        _output.WriteLine("=== Hardware Capabilities ===");
        _output.WriteLine($"GPUs detected:               {_fixture.Capabilities.GPUCount}");
        _output.WriteLine($"Displays detected:           {_fixture.Capabilities.DisplayCount}");
        _output.WriteLine($"GPU1 interface support:      {_fixture.Capabilities.SupportsGPU1}");
        _output.WriteLine($"GPU2 interface support:      {_fixture.Capabilities.SupportsGPU2}");
        _output.WriteLine($"Desktop services:            {_fixture.Capabilities.SupportsDesktopServices}");
        _output.WriteLine($"Performance monitoring:      {_fixture.Capabilities.SupportsPerformanceMonitoring}");
        _output.WriteLine($"GPU tuning:                  {_fixture.Capabilities.SupportsGPUTuning}");
        
        // At minimum, we should have one GPU
        Assert.True(_fixture.Capabilities.GPUCount > 0, "Should detect at least one GPU");
    }
    
    [SkippableFact]
    public void Test_05_EnhancedADLXHelper_Initialization()
    {
        Skip.IfNot(_fixture.IsAMDHardwareAvailable, "No AMD hardware available");
        
        // Test that we can create and initialize a separate helper
        using var helper = new EnhancedADLXHelper();
        
        var result = helper.Initialize();
        
        _output.WriteLine($"Helper initialization result: {result}");
        _output.WriteLine($"Is initialized: {helper.IsInitialized()}");
        
        if (result == ADLX_RESULT.ADLX_OK)
        {
            Assert.True(helper.IsInitialized());
            Assert.NotNull(helper.GetSystemServices());
            
            _output.WriteLine("? Separate EnhancedADLXHelper initialized successfully");
            
            helper.Terminate();
        }
        else
        {
            _output.WriteLine($"??  Helper initialization returned: {result}");
            _output.WriteLine($"   Error description: {ADLX.GetADLXErrorDescription(result)}");
        }
    }
    
    [SkippableFact]
    public void Test_06_ADLXLoader_LowLevel_Initialization()
    {
        Skip.IfNot(_fixture.IsAMDHardwareAvailable, "No AMD hardware available");
        
        // Test low-level ADLXLoader class
        using var loader = new ADLXLoader();
        
        bool loaded = loader.Load();
        _output.WriteLine($"DLL loaded: {loaded}");
        
        if (!loaded)
        {
            Skip.IfNot(loaded, "Could not load ADLX DLL");
        }
        
        Assert.True(loader.IsLoaded());
        
        // Query version
        var versionPtr = ADLX.new_charP_Ptr();
        try
        {
            var result = loader.QueryVersion(versionPtr);
            if (result == ADLX_RESULT.ADLX_OK)
            {
                string? version = ADLX.charP_Ptr_value(versionPtr);
                _output.WriteLine($"ADLX Version: {version}");
                Assert.NotNull(version);
            }
        }
        finally
        {
            ADLX.delete_charP_Ptr(versionPtr);
        }
        
        // Query full version
        var fullVersionPtr = ADLX.new_adlx_uint64P();
        try
        {
            var result = loader.QueryFullVersion(fullVersionPtr);
            if (result == ADLX_RESULT.ADLX_OK)
            {
                ulong fullVersion = ADLX.adlx_uint64P_value(fullVersionPtr);
                _output.WriteLine($"ADLX Full Version: 0x{fullVersion:X16}");
            }
        }
        finally
        {
            ADLX.delete_adlx_uint64P(fullVersionPtr);
        }
        
        // Initialize
        var systemPtr = ADLX.new_systemP_Ptr();
        try
        {
            ulong version = MakeFullVersion(ADLX.ADLX_VER_MAJOR, ADLX.ADLX_VER_MINOR);
            var result = loader.Initialize(version, systemPtr);
            
            _output.WriteLine($"Initialization result: {result}");
            
            if (result == ADLX_RESULT.ADLX_OK)
            {
                var system = ADLX.systemP_Ptr_value(systemPtr);
                Assert.NotNull(system);
                _output.WriteLine("? Low-level initialization successful");
            }
        }
        finally
        {
            loader.Terminate();
            ADLX.delete_systemP_Ptr(systemPtr);
        }
        
        loader.Unload();
        Assert.False(loader.IsLoaded());
    }
    
    [Fact]
    public void Test_07_Error_Description_Helper()
    {
        // Test error description function
        var descriptions = new[]
        {
            (ADLX_RESULT.ADLX_OK, "Success"),
            (ADLX_RESULT.ADLX_FAIL, "General failure"),
            (ADLX_RESULT.ADLX_INVALID_ARGS, "Invalid arguments"),
            (ADLX_RESULT.ADLX_NOT_SUPPORTED, "Not supported"),
        };
        
        _output.WriteLine("Error Description Helper:");
        foreach (var (code, expected) in descriptions)
        {
            string? description = ADLX.GetADLXErrorDescription(code);
            _output.WriteLine($"  {code}: {description}");
            Assert.Contains(expected, description, StringComparison.OrdinalIgnoreCase);
        }
    }

    // Helper method to create full version
    private static ulong MakeFullVersion(int major, int minor)
    {
        return ((ulong)major << 32) | (ulong)minor;
    }
}
