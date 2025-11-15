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
            Skip.If(true, "IADLXSystem2 not supported on this hardware");
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
            Skip.If(true, "Could not query IADLXSystem2 interface");
        }
        
        _output.WriteLine("=== Multimedia Services (IADLXSystem2 Feature) ===");
        _output.WriteLine("??  Note: SWIG bindings do not generate pointer wrapper functions for");
        _output.WriteLine("   SWIGTYPE_p_p_adlx__IADLXMultimediaServices, so we cannot fully test");
        _output.WriteLine("   this interface through C# bindings. This is a SWIG generation limitation.");
        _output.WriteLine("   The API is available in C++ and can be accessed via QueryInterface.");
        
        // TODO: SWIG doesn't generate new_multimediaServicesP_Ptr() helper functions
        // for SWIGTYPE_p_p_adlx__IADLXMultimediaServices. This is a known SWIG limitation
        // where not all pointer types get full wrapper generation.
        //
        // To properly test this, the SWIG interface file would need custom typemaps or
        // %newobject directives for these specific IADLXSystem2 methods.
        
        _output.WriteLine("? IADLXSystem2 interface is available (multimedia services untestable via bindings)");
    }
    
    [SkippableFact]
    public void Test_03_GPU_Apps_List_Changed_Handling()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.SupportsSystem2, "IADLXSystem2 not supported");
        
        var system2 = ADLX.QuerySystem2Interface(_fixture.System!);
        if (system2 == null)
        {
            Skip.If(true, "Could not query IADLXSystem2 interface");
        }
        
        _output.WriteLine("=== GPU App List Changed Handling (IADLXSystem2 Feature) ===");
        _output.WriteLine("??  Note: SWIG bindings do not generate pointer wrapper functions for");
        _output.WriteLine("   SWIGTYPE_p_p_adlx__IADLXGPUAppsListChangedHandling, so we cannot");
        _output.WriteLine("   fully test this interface through C# bindings. This is a SWIG limitation.");
        _output.WriteLine("   The API is available in C++ and can be accessed via QueryInterface.");
        
        // TODO: SWIG doesn't generate new_gpuAppsListChangedHandlingP_Ptr() helper
        // functions for SWIGTYPE_p_p_adlx__IADLXGPUAppsListChangedHandling.
        // This is the same SWIG limitation as with multimedia services.
        
        _output.WriteLine("? IADLXSystem2 interface is available (GPU apps list handling untestable via bindings)");
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
            Skip.If(true, "Could not query IADLXSystem2 interface");
        }
        
        _output.WriteLine("? IADLXSystem2 interface: Supported");
        _output.WriteLine("");
        _output.WriteLine("IADLXSystem2 Features:");
        _output.WriteLine("  - Multimedia Services:          Available in C++ (untestable via C# bindings)");
        _output.WriteLine("  - GPU Apps List Changed:        Available in C++ (untestable via C# bindings)");
        _output.WriteLine("");
        _output.WriteLine("??  Note: SWIG bindings have limitations with some IADLXSystem2 pointer types.");
        _output.WriteLine("   These features work correctly in C++ but cannot be fully tested through");
        _output.WriteLine("   the C# SWIG bindings due to missing pointer wrapper generation.");
        _output.WriteLine("");
        _output.WriteLine("??  IADLXSystem2 features require:");
        _output.WriteLine("   - AMD Radeon RX 6000 series or newer (RDNA 2+ architecture)");
        _output.WriteLine("   - AMD Adrenalin driver 23.2.1 or newer");
    }
}
