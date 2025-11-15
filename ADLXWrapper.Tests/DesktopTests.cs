using Xunit;
using Xunit.Abstractions;
using ADLXWrapper;

namespace ADLXWrapper.Tests;

/// <summary>
/// Tests for desktop services and Eyefinity configuration
/// </summary>
[Collection("ADLX Tests")]
public class DesktopTests
{
    private readonly ADLXTestFixture _fixture;
    private readonly ITestOutputHelper _output;
    
    public DesktopTests(ADLXTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }
    
    [SkippableFact]
    public void Test_01_Get_Desktop_Services()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        
        var desktopServicesPtr = ADLX.new_desktopSerP_Ptr();
        try
        {
            var result = _fixture.System!.GetDesktopsServices(desktopServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            
            var desktopServices = ADLX.desktopSerP_Ptr_value(desktopServicesPtr);
            Assert.NotNull(desktopServices);
            
            _output.WriteLine("? Desktop services retrieved successfully");
        }
        finally
        {
            ADLX.delete_desktopSerP_Ptr(desktopServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_02_Enumerate_Desktops()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsDesktopServices, "Desktop services not supported");
        
        var desktopServicesPtr = ADLX.new_desktopSerP_Ptr();
        try
        {
            _fixture.System!.GetDesktopsServices(desktopServicesPtr);
            var desktopServices = ADLX.desktopSerP_Ptr_value(desktopServicesPtr);
            
            // Get number of desktops
            var numDesktopsPtr = ADLX.new_adlx_uintP();
            try
            {
                var result = desktopServices!.GetNumberOfDesktops(numDesktopsPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    uint numDesktops = ADLX.adlx_uintP_value(numDesktopsPtr);
                    _output.WriteLine($"Number of desktops: {numDesktops}");
                    Assert.True(numDesktops > 0, "Should have at least one desktop");
                }
            }
            finally
            {
                ADLX.delete_adlx_uintP(numDesktopsPtr);
            }
            
            // Get desktop list
            var desktopListPtr = ADLX.new_desktopListP_Ptr();
            try
            {
                var result = desktopServices!.GetDesktops(desktopListPtr);
                Assert.Equal(ADLX_RESULT.ADLX_OK, result);
                
                var desktopList = ADLX.desktopListP_Ptr_value(desktopListPtr);
                Assert.NotNull(desktopList);
                
                uint desktopCount = desktopList!.Size();
                _output.WriteLine($"Desktop list size: {desktopCount}");
                
                // Enumerate each desktop
                for (uint i = 0; i < desktopCount; i++)
                {
                    var desktopPtr = ADLX.new_desktopP_Ptr();
                    desktopList.At(i, desktopPtr);
                    var desktop = ADLX.desktopP_Ptr_value(desktopPtr);
                    
                    if (desktop != null)
                    {
                        _output.WriteLine($"\n=== Desktop {i} ===");
                        
                        // Get desktop type
                        var typePtr = ADLX.new_adlx_desktopTypeP();
                        try
                        {
                            desktop.Type(typePtr);
                            var desktopType = ADLX.adlx_desktopTypeP_value(typePtr);
                            _output.WriteLine($"Type: {desktopType}");
                        }
                        finally
                        {
                            ADLX.delete_adlx_desktopTypeP(typePtr);
                        }
                    }
                    
                    ADLX.delete_desktopP_Ptr(desktopPtr);
                }
            }
            finally
            {
                ADLX.delete_desktopListP_Ptr(desktopListPtr);
            }
        }
        finally
        {
            ADLX.delete_desktopSerP_Ptr(desktopServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_03_Desktop_GPU_Association()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsDesktopServices, "Desktop services not supported");
        
        var desktopServicesPtr = ADLX.new_desktopSerP_Ptr();
        try
        {
            _fixture.System!.GetDesktopsServices(desktopServicesPtr);
            var desktopServices = ADLX.desktopSerP_Ptr_value(desktopServicesPtr);
            
            var desktopListPtr = ADLX.new_desktopListP_Ptr();
            try
            {
                desktopServices!.GetDesktops(desktopListPtr);
                var desktopList = ADLX.desktopListP_Ptr_value(desktopListPtr);
                
                if (desktopList != null && desktopList.Size() > 0)
                {
                    var desktopPtr = ADLX.new_desktopP_Ptr();
                    desktopList.At(0, desktopPtr);
                    var desktop = ADLX.desktopP_Ptr_value(desktopPtr);
                    
                    if (desktop != null)
                    {
                        _output.WriteLine("=== Desktop GPU Association ===");
                        
                        // Get associated GPU
                        var gpuPtr = ADLX.new_gpuP_Ptr();
                        try
                        {
                            var result = desktop.GPU(gpuPtr);
                            if (result == ADLX_RESULT.ADLX_OK)
                            {
                                var gpu = ADLX.gpuP_Ptr_value(gpuPtr);
                                if (gpu != null)
                                {
                                    var namePtr = ADLX.new_charP_Ptr();
                                    try
                                    {
                                        gpu.Name(namePtr);
                                        string? gpuName = ADLX.charP_Ptr_value(namePtr);
                                        _output.WriteLine($"Associated GPU: {gpuName}");
                                        Assert.NotNull(gpuName);
                                    }
                                    finally
                                    {
                                        ADLX.delete_charP_Ptr(namePtr);
                                    }
                                }
                            }
                        }
                        finally
                        {
                            ADLX.delete_gpuP_Ptr(gpuPtr);
                        }
                    }
                    
                    ADLX.delete_desktopP_Ptr(desktopPtr);
                }
            }
            finally
            {
                ADLX.delete_desktopListP_Ptr(desktopListPtr);
            }
        }
        finally
        {
            ADLX.delete_desktopSerP_Ptr(desktopServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_04_Eyefinity_Support_Detection()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.DisplayCount >= 2, "Need at least 2 displays for Eyefinity");
        
        var desktopServicesPtr = ADLX.new_desktopSerP_Ptr();
        try
        {
            _fixture.System!.GetDesktopsServices(desktopServicesPtr);
            var desktopServices = ADLX.desktopSerP_Ptr_value(desktopServicesPtr);
            
            _output.WriteLine("=== Eyefinity Support Detection ===");
            
            // Get SimpleEyefinity interface
            var simpleEyefinityPtr = ADLX.new_simpleEyefinityP_Ptr();
            try
            {
                var result = desktopServices!.GetSimpleEyefinity(simpleEyefinityPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    var simpleEyefinity = ADLX.simpleEyefinityP_Ptr_value(simpleEyefinityPtr);
                    if (simpleEyefinity != null)
                    {
                        // Check if Eyefinity is supported
                        var supportedPtr = ADLX.new_adlx_boolP();
                        try
                        {
                            result = simpleEyefinity.IsSupported(supportedPtr);
                            if (result == ADLX_RESULT.ADLX_OK)
                            {
                                bool supported = ADLX.adlx_boolP_value(supportedPtr);
                                _output.WriteLine($"Eyefinity Supported: {supported}");
                                
                                if (supported)
                                {
                                    _output.WriteLine("? System supports Eyefinity configuration");
                                }
                                else
                                {
                                    _output.WriteLine("??  Eyefinity not supported (may need compatible displays)");
                                }
                            }
                        }
                        finally
                        {
                            ADLX.delete_adlx_boolP(supportedPtr);
                        }
                    }
                }
                else
                {
                    _output.WriteLine($"SimpleEyefinity not available: {result}");
                }
            }
            finally
            {
                ADLX.delete_simpleEyefinityP_Ptr(simpleEyefinityPtr);
            }
        }
        finally
        {
            ADLX.delete_desktopSerP_Ptr(desktopServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_05_Eyefinity_Current_State()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.DisplayCount >= 2, "Need at least 2 displays for Eyefinity");
        
        var desktopServicesPtr = ADLX.new_desktopSerP_Ptr();
        try
        {
            _fixture.System!.GetDesktopsServices(desktopServicesPtr);
            var desktopServices = ADLX.desktopSerP_Ptr_value(desktopServicesPtr);
            
            _output.WriteLine("=== Current Eyefinity State ===");
            
            var simpleEyefinityPtr = ADLX.new_simpleEyefinityP_Ptr();
            try
            {
                var result = desktopServices!.GetSimpleEyefinity(simpleEyefinityPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    var simpleEyefinity = ADLX.simpleEyefinityP_Ptr_value(simpleEyefinityPtr);
                    if (simpleEyefinity != null)
                    {
                        // Check if Eyefinity is currently enabled
                        var enabledPtr = ADLX.new_adlx_boolP();
                        try
                        {
                            result = simpleEyefinity.IsEnabled(enabledPtr);
                            if (result == ADLX_RESULT.ADLX_OK)
                            {
                                bool enabled = ADLX.adlx_boolP_value(enabledPtr);
                                _output.WriteLine($"Eyefinity Currently Enabled: {enabled}");
                            }
                        }
                        finally
                        {
                            ADLX.delete_adlx_boolP(enabledPtr);
                        }
                    }
                }
            }
            finally
            {
                ADLX.delete_simpleEyefinityP_Ptr(simpleEyefinityPtr);
            }
        }
        finally
        {
            ADLX.delete_desktopSerP_Ptr(desktopServicesPtr);
        }
    }
    
    // ============================================================================
    // OPTIONAL TESTS - Modify System Configuration
    // These tests are excluded by default. Run with:
    //   dotnet test --filter "Category=CreateEyefinity"
    // ============================================================================
    
    /// <summary>
    /// ?? WARNING: This test will temporarily modify your display configuration!
    /// 
    /// This test creates an Eyefinity desktop (combining multiple displays into one large surface)
    /// and then restores the original configuration. Your displays will reconfigure during this test.
    /// 
    /// Requirements:
    /// - 2 or more compatible displays connected
    /// - Displays must support Eyefinity (same resolution/refresh rate recommended)
    /// - No applications should be using fullscreen mode
    /// 
    /// Run explicitly with: dotnet test --filter "Category=CreateEyefinity"
    /// </summary>
    [Trait("Category", "CreateEyefinity")]
    [Trait("Category", "Integration")]
    [SkippableFact]
    public void Optional_Test_Eyefinity_Create_And_Restore()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.DisplayCount >= 2, "Need at least 2 displays for Eyefinity");
        Skip.IfNot(_fixture.Capabilities.SupportsDesktopServices, "Desktop services not supported");
        
        _output.WriteLine("================================================================================");
        _output.WriteLine("??  WARNING: EYEFINITY CREATE/RESTORE TEST");
        _output.WriteLine("================================================================================");
        _output.WriteLine("This test will MODIFY your desktop configuration!");
        _output.WriteLine("- Your displays will reconfigure (may go black briefly)");
        _output.WriteLine("- Open windows may be repositioned");
        _output.WriteLine("- The test will restore your original configuration when complete");
        _output.WriteLine("================================================================================");
        _output.WriteLine("");
        
        var desktopServicesPtr = ADLX.new_desktopSerP_Ptr();
        try
        {
            _fixture.System!.GetDesktopsServices(desktopServicesPtr);
            var desktopServices = ADLX.desktopSerP_Ptr_value(desktopServicesPtr);
            
            // Get SimpleEyefinity interface
            var simpleEyefinityPtr = ADLX.new_simpleEyefinityP_Ptr();
            try
            {
                var result = desktopServices!.GetSimpleEyefinity(simpleEyefinityPtr);
                if (result != ADLX_RESULT.ADLX_OK)
                {
                    Skip.Always($"SimpleEyefinity not available: {result}");
                }
                
                var simpleEyefinity = ADLX.simpleEyefinityP_Ptr_value(simpleEyefinityPtr);
                if (simpleEyefinity == null)
                {
                    Skip.Always("SimpleEyefinity interface is null");
                }
                
                // Step 1: Check if Eyefinity is supported
                _output.WriteLine("Step 1: Checking Eyefinity support...");
                var supportedPtr = ADLX.new_adlx_boolP();
                try
                {
                    result = simpleEyefinity.IsSupported(supportedPtr);
                    if (result != ADLX_RESULT.ADLX_OK)
                    {
                        Skip.Always($"Could not check Eyefinity support: {result}");
                    }
                    
                    bool supported = ADLX.adlx_boolP_value(supportedPtr);
                    if (!supported)
                    {
                        Skip.Always("Eyefinity not supported on this system (displays may not be compatible)");
                    }
                    
                    _output.WriteLine("? Eyefinity is supported");
                }
                finally
                {
                    ADLX.delete_adlx_boolP(supportedPtr);
                }
                
                // Step 2: Save current Eyefinity state
                _output.WriteLine("\nStep 2: Checking current Eyefinity state...");
                var wasEnabledPtr = ADLX.new_adlx_boolP();
                bool wasEnabled = false;
                try
                {
                    result = simpleEyefinity.IsEnabled(wasEnabledPtr);
                    if (result == ADLX_RESULT.ADLX_OK)
                    {
                        wasEnabled = ADLX.adlx_boolP_value(wasEnabledPtr);
                        _output.WriteLine($"Original Eyefinity state: {(wasEnabled ? "Enabled" : "Disabled")}");
                    }
                }
                finally
                {
                    ADLX.delete_adlx_boolP(wasEnabledPtr);
                }
                
                try
                {
                    // Step 3: If Eyefinity not already enabled, create it
                    if (!wasEnabled)
                    {
                        _output.WriteLine("\nStep 3: Creating Eyefinity desktop...");
                        _output.WriteLine("??  Your displays will reconfigure now...");
                        
                        result = simpleEyefinity.Create();
                        
                        if (result == ADLX_RESULT.ADLX_OK)
                        {
                            _output.WriteLine("? Eyefinity desktop created successfully");
                            
                            // Verify it's enabled
                            var enabledPtr = ADLX.new_adlx_boolP();
                            try
                            {
                                simpleEyefinity.IsEnabled(enabledPtr);
                                bool isEnabled = ADLX.adlx_boolP_value(enabledPtr);
                                Assert.True(isEnabled, "Eyefinity should be enabled after creation");
                                _output.WriteLine("? Verified: Eyefinity is now enabled");
                            }
                            finally
                            {
                                ADLX.delete_adlx_boolP(enabledPtr);
                            }
                        }
                        else if (result == ADLX_RESULT.ADLX_ALREADY_ENABLED)
                        {
                            _output.WriteLine("??  Eyefinity was already enabled");
                        }
                        else
                        {
                            _output.WriteLine($"? Failed to create Eyefinity: {result}");
                            _output.WriteLine($"  Error: {ADLX.GetADLXErrorDescription(result)}");
                            throw new Exception($"Eyefinity creation failed: {result}");
                        }
                        
                        // Step 4: Destroy Eyefinity to restore original state
                        _output.WriteLine("\nStep 4: Restoring original desktop configuration...");
                        _output.WriteLine("??  Your displays will reconfigure again...");
                        
                        result = simpleEyefinity.Destroy();
                        
                        if (result == ADLX_RESULT.ADLX_OK)
                        {
                            _output.WriteLine("? Eyefinity desktop destroyed successfully");
                            
                            // Verify it's disabled
                            var disabledPtr = ADLX.new_adlx_boolP();
                            try
                            {
                                simpleEyefinity.IsEnabled(disabledPtr);
                                bool isEnabled = ADLX.adlx_boolP_value(disabledPtr);
                                Assert.False(isEnabled, "Eyefinity should be disabled after destruction");
                                _output.WriteLine("? Verified: Eyefinity is now disabled");
                                _output.WriteLine("? Original desktop configuration restored");
                            }
                            finally
                            {
                                ADLX.delete_adlx_boolP(disabledPtr);
                            }
                        }
                        else
                        {
                            _output.WriteLine($"? Failed to destroy Eyefinity: {result}");
                            _output.WriteLine($"  Error: {ADLX.GetADLXErrorDescription(result)}");
                            _output.WriteLine("??  WARNING: Your desktop configuration may not be restored!");
                            _output.WriteLine("   You may need to manually disable Eyefinity in AMD Software");
                            throw new Exception($"Eyefinity destruction failed: {result}");
                        }
                    }
                    else
                    {
                        // Eyefinity was already enabled - just verify we can access it
                        _output.WriteLine("\n??  Eyefinity was already enabled before test");
                        _output.WriteLine("   Skipping create/destroy cycle to preserve your configuration");
                        _output.WriteLine("   Test considered successful - Eyefinity APIs are accessible");
                    }
                    
                    _output.WriteLine("\n================================================================================");
                    _output.WriteLine("? TEST COMPLETED SUCCESSFULLY");
                    _output.WriteLine("  Your desktop configuration should be restored to its original state");
                    _output.WriteLine("================================================================================");
                }
                catch (Exception ex)
                {
                    _output.WriteLine("\n================================================================================");
                    _output.WriteLine("? TEST FAILED WITH EXCEPTION");
                    _output.WriteLine($"  {ex.Message}");
                    _output.WriteLine("================================================================================");
                    
                    // Attempt to restore state on failure
                    if (!wasEnabled)
                    {
                        _output.WriteLine("\nAttempting emergency restoration...");
                        try
                        {
                            var emergencyResult = simpleEyefinity.Destroy();
                            if (emergencyResult == ADLX_RESULT.ADLX_OK)
                            {
                                _output.WriteLine("? Emergency restoration successful");
                            }
                            else
                            {
                                _output.WriteLine($"? Emergency restoration failed: {emergencyResult}");
                                _output.WriteLine("??  Please manually disable Eyefinity in AMD Software");
                            }
                        }
                        catch
                        {
                            _output.WriteLine("? Emergency restoration threw exception");
                            _output.WriteLine("??  Please manually disable Eyefinity in AMD Software");
                        }
                    }
                    
                    throw;
                }
            }
            finally
            {
                ADLX.delete_simpleEyefinityP_Ptr(simpleEyefinityPtr);
            }
        }
        finally
        {
            ADLX.delete_desktopSerP_Ptr(desktopServicesPtr);
        }
    }
}
