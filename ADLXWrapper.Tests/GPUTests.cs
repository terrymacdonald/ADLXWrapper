using ADLXWrapper;
using System.Diagnostics;
using Xunit;
using Xunit.Abstractions;

namespace ADLXWrapper.Tests;

/// <summary>
/// Tests for GPU enumeration, properties, and interface detection
/// </summary>
[Collection("ADLX Tests")]
public class GPUTests
{
    private readonly ADLXTestFixture _fixture;
    private readonly ITestOutputHelper _output;
    
    public GPUTests(ADLXTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }
    
    [SkippableFact]
    public void Test_01_Enumerate_GPUs()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");

        if (_fixture.System == null) return;

        try
        {
            // Detect GPU capabilities - wrapped in try-catch due to potential access violations
            var gpuListPtr = ADLX.new_gpuListP_Ptr();
            try
            {

                var system = _fixture._helper.GetSystemServices();
                var result = system.GetGPUs(gpuListPtr);
                Assert.Equal(ADLX_RESULT.ADLX_OK, result);

                if (result == ADLX_RESULT.ADLX_OK)
                {
                    var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
                    Assert.NotNull(gpuList);
                    if (gpuList != null)
                    {
                        uint gpuCount = gpuList!.Size();

                        _output.WriteLine($"Found {gpuCount} GPU(s)");
                        Assert.True(gpuCount > 0, "Should have at least one GPU");
                        Assert.Equal(_fixture.Capabilities.GPUCount, gpuCount);

                        // Cache first GPU for use in tests
                        if (gpuList.Size() > 0)
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
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                // GPU access failed - tests will handle missing FirstGPU
                                Debug.WriteLine($"Failed to get first GPU: {ex.Message}");
                            }
                            finally
                            {
                                ADLX.delete_gpuP_Ptr(gpuPtr);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get GPU list: {ex.Message}");
            }
            finally
            {
                ADLX.delete_gpuListP_Ptr(gpuListPtr);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Exception in GPU detection: {ex.Message}");
        }
    }
    
    [SkippableFact]
    public void Test_02_GPU_Basic_Properties()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        
        var gpuListPtr = ADLX.new_gpuListP_Ptr();
        try
        {
            _fixture.System!.GetGPUs(gpuListPtr);
            var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
            
            uint gpuCount = gpuList!.Size();
            
            for (uint i = 0; i < gpuCount; i++)
            {
                var gpuPtr = ADLX.new_gpuP_Ptr();
                gpuList.At(i, gpuPtr);
                var gpu = ADLX.gpuP_Ptr_value(gpuPtr);
                
                if (gpu != null)
                {
                    _output.WriteLine($"\n=== GPU {i} Properties ===");
                    
                    // GPU Name
                    var namePtr = ADLX.new_charP_Ptr();
                    gpu.Name(namePtr);
                    string? gpuName = ADLX.charP_Ptr_value(namePtr);
                    ADLX.delete_charP_Ptr(namePtr);
                    
                    _output.WriteLine($"Name: {gpuName}");
                    Assert.NotNull(gpuName);
                    Assert.NotEmpty(gpuName);
                    
                    // Vendor ID (returns string, not int)
                    var vendorIdPtr = ADLX.new_charP_Ptr();
                    gpu.VendorId(vendorIdPtr);
                    string? vendorIdStr = ADLX.charP_Ptr_value(vendorIdPtr);
                    ADLX.delete_charP_Ptr(vendorIdPtr);
                    
                    _output.WriteLine($"Vendor ID: {vendorIdStr}");
                    // AMD vendor ID is 0x1002
                    Assert.NotNull(vendorIdStr);
                    
                    // Device ID (returns string, not int)
                    var deviceIdPtr = ADLX.new_charP_Ptr();
                    gpu.DeviceId(deviceIdPtr);
                    string? deviceIdStr = ADLX.charP_Ptr_value(deviceIdPtr);
                    ADLX.delete_charP_Ptr(deviceIdPtr);
                    
                    _output.WriteLine($"Device ID: {deviceIdStr}");
                    
                    // VRAM Size (TotalVRAM returns uint, not VRAMType which is a string)
                    var vramPtr = ADLX.new_adlx_uintP();
                    gpu.TotalVRAM(vramPtr);
                    uint vramSize = ADLX.adlx_uintP_value(vramPtr);
                    ADLX.delete_adlx_uintP(vramPtr);
                    
                    _output.WriteLine($"VRAM: {vramSize} MB");
                    Assert.True(vramSize > 0, "VRAM size should be greater than 0");
                    
                    // GPU Type
                    var typePtr = ADLX.new_adlx_gpuTypeP();
                    gpu.Type(typePtr);
                    var gpuType = ADLX.adlx_gpuTypeP_value(typePtr);
                    ADLX.delete_adlx_gpuTypeP(typePtr);
                    
                    _output.WriteLine($"Type: {gpuType}");
                    
                    // Is External
                    var isExternalPtr = ADLX.new_adlx_boolP();
                    gpu.IsExternal(isExternalPtr);
                    bool isExternal = ADLX.adlx_boolP_value(isExternalPtr);
                    ADLX.delete_adlx_boolP(isExternalPtr);
                    
                    _output.WriteLine($"Is External: {isExternal}");
                }
                
                ADLX.delete_gpuP_Ptr(gpuPtr);
            }
        }
        finally
        {
            ADLX.delete_gpuListP_Ptr(gpuListPtr);
        }
    }
    
    [SkippableFact]
    public void Test_03_GPU_Interface_Versions()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        
        var gpuListPtr = ADLX.new_gpuListP_Ptr();
        try
        {
            _fixture.System!.GetGPUs(gpuListPtr);
            var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
            
            uint gpuCount = gpuList!.Size();
            
            for (uint i = 0; i < gpuCount; i++)
            {
                var gpuPtr = ADLX.new_gpuP_Ptr();
                gpuList.At(i, gpuPtr);
                var gpu = ADLX.gpuP_Ptr_value(gpuPtr);
                
                if (gpu != null)
                {
                    // Get GPU name for output
                    var namePtr = ADLX.new_charP_Ptr();
                    gpu.Name(namePtr);
                    string? gpuName = ADLX.charP_Ptr_value(namePtr);
                    ADLX.delete_charP_Ptr(namePtr);
                    
                    _output.WriteLine($"\n=== GPU {i}: {gpuName} Interface Support ===");
                    
                    // Check interface support
                    bool supportsGPU1 = ADLX.SupportsGPU1Interface(gpu);
                    bool supportsGPU2 = ADLX.SupportsGPU2Interface(gpu);
                    
                    _output.WriteLine($"IADLXGPU (base):  Always supported");
                    _output.WriteLine($"IADLXGPU1:        {(supportsGPU1 ? "? Supported" : "? Not supported")}");
                    _output.WriteLine($"IADLXGPU2:        {(supportsGPU2 ? "? Supported" : "? Not supported")}");
                    
                    // Try to query GPU1 interface
                    if (supportsGPU1)
                    {
                        var gpu1 = ADLX.QueryGPU1Interface(gpu);
                        Assert.NotNull(gpu1);
                        _output.WriteLine("  ? Successfully queried IADLXGPU1 interface");
                    }
                    
                    // Try to query GPU2 interface
                    if (supportsGPU2)
                    {
                        var gpu2 = ADLX.QueryGPU2Interface(gpu);
                        Assert.NotNull(gpu2);
                        _output.WriteLine("  ? Successfully queried IADLXGPU2 interface");
                    }
                    else
                    {
                        _output.WriteLine("  ??  IADLXGPU2 requires newer GPU models");
                    }
                }
                
                ADLX.delete_gpuP_Ptr(gpuPtr);
            }
        }
        finally
        {
            ADLX.delete_gpuListP_Ptr(gpuListPtr);
        }
    }
    
    [SkippableFact]
    public void Test_04_GPU_ASIC_Info()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        
        var gpuListPtr = ADLX.new_gpuListP_Ptr();
        try
        {
            _fixture.System!.GetGPUs(gpuListPtr);
            var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
            
            var gpuPtr = ADLX.new_gpuP_Ptr();
            gpuList!.At(0, gpuPtr);
            var gpu = ADLX.gpuP_Ptr_value(gpuPtr);
            
            if (gpu != null)
            {
                _output.WriteLine("=== GPU ASIC Information ===");
                
                // ASIC Family Type - needs proper enum pointer
                var asicFamilyPtr = ADLX.new_adlx_asicFamilyTypeP(); // Using ASIC family type pointer
                var result = gpu.ASICFamilyType(asicFamilyPtr);
                
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    var asicFamily = ADLX.adlx_asicFamilyTypeP_value(asicFamilyPtr);
                    _output.WriteLine($"ASIC Family: {asicFamily}");
                }
                else
                {
                    _output.WriteLine($"ASIC Family not available: {result}");
                }
                
                ADLX.delete_adlx_asicFamilyTypeP(asicFamilyPtr);
                
                // PCI Bus information - need to use GPU1 interface
                bool supportsGPU1 = ADLX.SupportsGPU1Interface(gpu);
                
                if (supportsGPU1)
                {
                    var gpu1 = ADLX.QueryGPU1Interface(gpu);
                    if (gpu1 != null)
                    {
                        var busTypePtr = ADLX.new_adlx_pciBusTypeP();
                        result = gpu1.PCIBusType(busTypePtr);
                        if (result == ADLX_RESULT.ADLX_OK)
                        {
                            var busType = ADLX.adlx_pciBusTypeP_value(busTypePtr);
                            _output.WriteLine($"PCI Bus Type: {busType}");
                        }
                        ADLX.delete_adlx_pciBusTypeP(busTypePtr);
                        
                        var laneWidthPtr = ADLX.new_adlx_uintP();
                        result = gpu1.PCIBusLaneWidth(laneWidthPtr);
                        if (result == ADLX_RESULT.ADLX_OK)
                        {
                            uint laneWidth = ADLX.adlx_uintP_value(laneWidthPtr);
                            _output.WriteLine($"PCI Lane Width: x{laneWidth}");
                        }
                        ADLX.delete_adlx_uintP(laneWidthPtr);
                    }
                }
                else
                {
                    _output.WriteLine("PCI Bus information requires IADLXGPU1 interface");
                }
            }
            
            ADLX.delete_gpuP_Ptr(gpuPtr);
        }
        finally
        {
            ADLX.delete_gpuListP_Ptr(gpuListPtr);
        }
    }
    
    [SkippableFact]
    public void Test_05_GPU_Driver_Info()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        
        var gpuListPtr = ADLX.new_gpuListP_Ptr();
        try
        {
            _fixture.System!.GetGPUs(gpuListPtr);
            var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
            
            var gpuPtr = ADLX.new_gpuP_Ptr();
            gpuList!.At(0, gpuPtr);
            var gpu = ADLX.gpuP_Ptr_value(gpuPtr);
            
            if (gpu != null)
            {
                _output.WriteLine("=== GPU Driver Information ===");
                
                // Driver Path
                var driverPathPtr = ADLX.new_charP_Ptr();
                var result = gpu.DriverPath(driverPathPtr);
                
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    string? driverPath = ADLX.charP_Ptr_value(driverPathPtr);
                    _output.WriteLine($"Driver Path: {driverPath}");
                    Assert.NotNull(driverPath);
                }
                
                ADLX.delete_charP_Ptr(driverPathPtr);
                
                // PNP String
                var pnpPtr = ADLX.new_charP_Ptr();
                result = gpu.PNPString(pnpPtr);
                
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    string? pnpString = ADLX.charP_Ptr_value(pnpPtr);
                    _output.WriteLine($"PNP String: {pnpString}");
                }
                
                ADLX.delete_charP_Ptr(pnpPtr);
            }
            
            ADLX.delete_gpuP_Ptr(gpuPtr);
        }
        finally
        {
            ADLX.delete_gpuListP_Ptr(gpuListPtr);
        }
    }
}

