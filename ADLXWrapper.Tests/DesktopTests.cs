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
}
