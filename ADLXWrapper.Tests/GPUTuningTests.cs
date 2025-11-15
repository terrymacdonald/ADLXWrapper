using Xunit;
using Xunit.Abstractions;
using ADLXWrapper;

namespace ADLXWrapper.Tests;

/// <summary>
/// Tests for GPU tuning features (read-only tests to avoid modifying user settings)
/// </summary>
[Collection("ADLX Tests")]
public class GPUTuningTests
{
    private readonly ADLXTestFixture _fixture;
    private readonly ITestOutputHelper _output;
    
    public GPUTuningTests(ADLXTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }
    
    [SkippableFact]
    public void Test_01_Get_GPU_Tuning_Services()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        
        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            var result = _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            
            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            Assert.NotNull(tuningServices);
            
            _output.WriteLine("? GPU tuning services retrieved successfully");
        }
        finally
        {
            ADLX.delete_gpuTuningP_Ptr(tuningServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_02_Manual_Fan_Tuning_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        
        var gpu = GetFirstGPU();
        if (gpu == null)
        {
            Skip.Always("Could not get first GPU");
        }
        
        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            
            _output.WriteLine("=== Manual Fan Tuning Support ===");
            
            // Check if manual fan tuning is supported
            var supportedPtr = ADLX.new_adlx_boolP();
            try
            {
                var result = tuningServices!.IsSupportedManualFanTuning(gpu, supportedPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    bool supported = ADLX.adlx_boolP_value(supportedPtr);
                    _output.WriteLine($"Manual Fan Tuning Supported: {supported}");
                    
                    if (supported)
                    {
                        _output.WriteLine("? GPU supports manual fan tuning");
                    }
                    else
                    {
                        _output.WriteLine("??  Manual fan tuning not supported on this GPU");
                    }
                }
            }
            finally
            {
                ADLX.delete_adlx_boolP(supportedPtr);
            }
        }
        finally
        {
            ADLX.delete_gpuTuningP_Ptr(tuningServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_03_Manual_Power_Tuning_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        
        var gpu = GetFirstGPU();
        if (gpu == null)
        {
            Skip.Always("Could not get first GPU");
        }
        
        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            
            _output.WriteLine("=== Manual Power Tuning Support ===");
            
            // Check if manual power tuning is supported
            var supportedPtr = ADLX.new_adlx_boolP();
            try
            {
                var result = tuningServices!.IsSupportedManualPowerTuning(gpu, supportedPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    bool supported = ADLX.adlx_boolP_value(supportedPtr);
                    _output.WriteLine($"Manual Power Tuning Supported: {supported}");
                    
                    if (supported)
                    {
                        _output.WriteLine("? GPU supports manual power tuning");
                    }
                    else
                    {
                        _output.WriteLine("??  Manual power tuning not supported on this GPU");
                    }
                }
            }
            finally
            {
                ADLX.delete_adlx_boolP(supportedPtr);
            }
        }
        finally
        {
            ADLX.delete_gpuTuningP_Ptr(tuningServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_04_Manual_Graphics_Tuning_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        
        var gpu = GetFirstGPU();
        if (gpu == null)
        {
            Skip.Always("Could not get first GPU");
        }
        
        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            
            _output.WriteLine("=== Manual Graphics Tuning Support ===");
            
            // Check if manual graphics tuning is supported
            var supportedPtr = ADLX.new_adlx_boolP();
            try
            {
                var result = tuningServices!.IsSupportedManualGFXTuning(gpu, supportedPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    bool supported = ADLX.adlx_boolP_value(supportedPtr);
                    _output.WriteLine($"Manual Graphics Tuning Supported: {supported}");
                    
                    if (supported)
                    {
                        _output.WriteLine("? GPU supports manual graphics tuning");
                    }
                    else
                    {
                        _output.WriteLine("??  Manual graphics tuning not supported on this GPU");
                    }
                }
            }
            finally
            {
                ADLX.delete_adlx_boolP(supportedPtr);
            }
        }
        finally
        {
            ADLX.delete_gpuTuningP_Ptr(tuningServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_05_Auto_Tuning_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        
        var gpu = GetFirstGPU();
        if (gpu == null)
        {
            Skip.Always("Could not get first GPU");
        }
        
        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            
            _output.WriteLine("=== Auto Tuning Support ===");
            
            // Check if auto tuning is supported
            var supportedPtr = ADLX.new_adlx_boolP();
            try
            {
                var result = tuningServices!.IsSupportedAutoTuning(gpu, supportedPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    bool supported = ADLX.adlx_boolP_value(supportedPtr);
                    _output.WriteLine($"Auto Tuning Supported: {supported}");
                    
                    if (supported)
                    {
                        _output.WriteLine("? GPU supports auto tuning");
                    }
                    else
                    {
                        _output.WriteLine("??  Auto tuning not supported on this GPU");
                    }
                }
            }
            finally
            {
                ADLX.delete_adlx_boolP(supportedPtr);
            }
        }
        finally
        {
            ADLX.delete_gpuTuningP_Ptr(tuningServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_06_Preset_Tuning_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        
        var gpu = GetFirstGPU();
        if (gpu == null)
        {
            Skip.Always("Could not get first GPU");
        }
        
        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            
            _output.WriteLine("=== Preset Tuning Support ===");
            
            // Check if preset tuning is supported
            var supportedPtr = ADLX.new_adlx_boolP();
            try
            {
                var result = tuningServices!.IsSupportedPresetTuning(gpu, supportedPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    bool supported = ADLX.adlx_boolP_value(supportedPtr);
                    _output.WriteLine($"Preset Tuning Supported: {supported}");
                    
                    if (supported)
                    {
                        _output.WriteLine("? GPU supports preset tuning");
                    }
                    else
                    {
                        _output.WriteLine("??  Preset tuning not supported on this GPU");
                    }
                }
            }
            finally
            {
                ADLX.delete_adlx_boolP(supportedPtr);
            }
        }
        finally
        {
            ADLX.delete_gpuTuningP_Ptr(tuningServicesPtr);
        }
    }
    
    // Helper method to get first GPU
    private IADLXGPU? GetFirstGPU()
    {
        var gpuListPtr = ADLX.new_gpuListP_Ptr();
        try
        {
            var result = _fixture.System!.GetGPUs(gpuListPtr);
            if (result != ADLX_RESULT.ADLX_OK)
                return null;
            
            var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
            if (gpuList == null)
                return null;
            
            uint gpuCount = gpuList.Size();
            if (gpuCount == 0)
                return null;
            
            var gpuPtr = ADLX.new_gpuP_Ptr();
            gpuList.At(0, gpuPtr);
            var gpu = ADLX.gpuP_Ptr_value(gpuPtr);
            ADLX.delete_gpuP_Ptr(gpuPtr);
            
            return gpu;
        }
        finally
        {
            ADLX.delete_gpuListP_Ptr(gpuListPtr);
        }
    }
}
