using Xunit;
using Xunit.Abstractions;
using ADLXWrapper;

namespace ADLXWrapper.Tests;

/// <summary>
/// Tests for IADLXSystem2-specific features (newer GPU models required)
/// </summary>
[Collection("ADLX Tests")]
public class System2Tests
{
    private readonly ADLXTestFixture _fixture;
    private readonly ITestOutputHelper _output;
    
    public System2Tests(ADLXTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }
    
    [SkippableFact]
    public void Test_01_System2_Interface_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        
        _output.WriteLine("=== IADLXSystem2 Interface Support ===");
        _output.WriteLine($"IADLXSystem2 Supported: {_fixture.SupportsSystem2}");
        
        if (!_fixture.SupportsSystem2)
        {
            _output.WriteLine("\n??  IADLXSystem2 interface not supported");
            _output.WriteLine("    Requirements:");
            _output.WriteLine("    - AMD Radeon RX 6000 series or newer");
            _output.WriteLine("    - AMD Adrenalin driver 23.2.1 or newer");
            Skip.Always("IADLXSystem2 not supported on this hardware");
        }
        
        // Try to query System2 interface
        var system2 = ADLX.QuerySystem2Interface(_fixture.System!);
        Assert.NotNull(system2);
        
        _output.WriteLine("? Successfully queried IADLXSystem2 interface");
    }
    
    [SkippableFact]
    public void Test_02_Multimedia_Services()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.SupportsSystem2, "IADLXSystem2 not supported");
        
        var system2 = ADLX.QuerySystem2Interface(_fixture.System!);
        if (system2 == null)
        {
            Skip.Always("Could not query IADLXSystem2 interface");
        }
        
        _output.WriteLine("=== Multimedia Services (IADLXSystem2 Feature) ===");
        
        var multimediaServicesPtr = ADLX.new_voidP_Ptr();
        try
        {
            var result = system2.GetMultimediaServices(multimediaServicesPtr);
            
            if (result == ADLX_RESULT.ADLX_OK)
            {
                _output.WriteLine("? Multimedia services retrieved successfully");
                _output.WriteLine("??  This feature requires newer AMD GPUs (RDNA 2 or later)");
            }
            else if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _output.WriteLine("??  Multimedia services not supported on this GPU");
                _output.WriteLine("    This feature requires RDNA 2 architecture or newer");
            }
            else
            {
                _output.WriteLine($"Multimedia services query returned: {result}");
                _output.WriteLine($"Error: {ADLX.GetADLXErrorDescription(result)}");
            }
        }
        finally
        {
            ADLX.delete_voidP_Ptr(multimediaServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_03_GPU_Apps_List_Changed_Handling()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.SupportsSystem2, "IADLXSystem2 not supported");
        
        var system2 = ADLX.QuerySystem2Interface(_fixture.System!);
        if (system2 == null)
        {
            Skip.Always("Could not query IADLXSystem2 interface");
        }
        
        _output.WriteLine("=== GPU App List Changed Handling (IADLXSystem2 Feature) ===");
        
        var appListHandlingPtr = ADLX.new_voidP_Ptr();
        try
        {
            var result = system2.GetGPUAppsListChangedHandling(appListHandlingPtr);
            
            if (result == ADLX_RESULT.ADLX_OK)
            {
                _output.WriteLine("? GPU App List Changed Handling retrieved successfully");
                _output.WriteLine("??  This allows monitoring of GPU application assignments");
            }
            else if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
            {
                _output.WriteLine("??  GPU App List Changed Handling not supported");
                _output.WriteLine("    This feature requires specific driver versions");
            }
            else
            {
                _output.WriteLine($"GPU App List Changed Handling query returned: {result}");
                _output.WriteLine($"Error: {ADLX.GetADLXErrorDescription(result)}");
            }
        }
        finally
        {
            ADLX.delete_voidP_Ptr(appListHandlingPtr);
        }
    }
    
    [SkippableFact]
    public void Test_04_System2_Capabilities_Summary()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.SupportsSystem2, "IADLXSystem2 not supported");
        
        _output.WriteLine("=== IADLXSystem2 Capabilities Summary ===");
        
        var system2 = ADLX.QuerySystem2Interface(_fixture.System!);
        if (system2 == null)
        {
            Skip.Always("Could not query IADLXSystem2 interface");
        }
        
        // Check multimedia services
        var multimediaPtr = ADLX.new_voidP_Ptr();
        try
        {
            var multimediaResult = system2.GetMultimediaServices(multimediaPtr);
            bool multimediaSupported = multimediaResult == ADLX_RESULT.ADLX_OK;
            _output.WriteLine($"Multimedia Services:           {(multimediaSupported ? "? Supported" : "? Not supported")}");
        }
        finally
        {
            ADLX.delete_voidP_Ptr(multimediaPtr);
        }
        
        // Check GPU app list handling
        var appListPtr = ADLX.new_voidP_Ptr();
        try
        {
            var appListResult = system2.GetGPUAppsListChangedHandling(appListPtr);
            bool appListSupported = appListResult == ADLX_RESULT.ADLX_OK;
            _output.WriteLine($"GPU Apps List Changed Handling: {(appListSupported ? "? Supported" : "? Not supported")}");
        }
        finally
        {
            ADLX.delete_voidP_Ptr(appListPtr);
        }
        
        _output.WriteLine("\n??  IADLXSystem2 features are GPU and driver version dependent");
    }
}
