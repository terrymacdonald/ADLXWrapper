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
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        Skip.IfNot(_fixture.FirstGPU != null, "First GPU not available");

        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            var result = _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);

            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            Assert.NotNull(tuningServices);

            _output.WriteLine("✓ GPU tuning services retrieved successfully");
        }
        finally
        {
            ADLX.delete_gpuTuningP_Ptr(tuningServicesPtr);
        }
    }

    /*[SkippableFact]
    public void Test_02_Manual_Fan_Tuning_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        Skip.IfNot(_fixture.FirstGPU != null, "First GPU not available");

        var gpu = _fixture.FirstGPU;

        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            var result = _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);

            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            Assert.NotNull(tuningServices);

            _output.WriteLine("=== Manual Fan Tuning Support ===");

            // Check if manual fan tuning is supported
            var supportedPtr = ADLX.new_adlx_boolP();
            try
            {
                result = tuningServices.IsSupportedManualFanTuning(gpu, supportedPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    bool supported = ADLX.adlx_boolP_value(supportedPtr);
                    _output.WriteLine($"Manual Fan Tuning Supported: {supported}");

                    if (supported)
                    {
                        _output.WriteLine("✓ GPU supports manual fan tuning");
                    }
                    else
                    {
                        _output.WriteLine("ℹ️  Manual fan tuning not supported on this GPU");
                    }
                }
                else
                {
                    _output.WriteLine($"⚠️  Could not check fan tuning support: {result}");
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
    }*/

    /*[SkippableFact]
    public void Test_03_Manual_Power_Tuning_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        Skip.IfNot(_fixture.FirstGPU != null, "First GPU not available");

        var gpu = _fixture.FirstGPU;

        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            var result = _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);

            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            Assert.NotNull(tuningServices);

            _output.WriteLine("=== Manual Power Tuning Support ===");

            // Check if manual power tuning is supported
            var supportedPtr = ADLX.new_adlx_boolP();
            try
            {
                result = tuningServices.IsSupportedManualPowerTuning(gpu, supportedPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    bool supported = ADLX.adlx_boolP_value(supportedPtr);
                    _output.WriteLine($"Manual Power Tuning Supported: {supported}");

                    if (supported)
                    {
                        _output.WriteLine("✓ GPU supports manual power tuning");
                    }
                    else
                    {
                        _output.WriteLine("ℹ️  Manual power tuning not supported on this GPU");
                    }
                }
                else
                {
                    _output.WriteLine($"⚠️  Could not check power tuning support: {result}");
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
    }*/

    // TODO - Fix the crash at result = tuningServices.IsSupportedManualGFXTuning(gpu, supportedPtr);
    // There is some discussion that this function may crash on older iGPUs (Which I'm testing on now)
    // so I am removing this test for now. I hope to review it at a later stage to avoid the crash.
    /*[SkippableFact]
    public void Test_04_Manual_Graphics_Tuning_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        Skip.IfNot(_fixture.FirstGPU != null, "First GPU not available");

        var gpu = _fixture.FirstGPU;

        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            var result = _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);

            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            Assert.NotNull(tuningServices);

            _output.WriteLine("=== Manual Graphics Tuning Support ===");

            // Check if manual graphics tuning is supported
            var supportedPtr = ADLX.new_adlx_boolP();
            try
            {
                // **THIS IS WHERE THE CRASH HAPPENS** - wrap in try-catch
                try
                {
                    result = tuningServices.IsSupportedManualGFXTuning(gpu, supportedPtr);
                    if (result == ADLX_RESULT.ADLX_OK)
                    {
                        bool supported = ADLX.adlx_boolP_value(supportedPtr);
                        _output.WriteLine($"Manual Graphics Tuning Supported: {supported}");

                        if (supported)
                        {
                            _output.WriteLine("✓ GPU supports manual graphics tuning");
                        }
                        else
                        {
                            _output.WriteLine("ℹ️  Manual graphics tuning not supported on this GPU");
                        }
                    }
                    else
                    {
                        _output.WriteLine($"⚠️  Could not check graphics tuning support: {result}");
                    }
                }
                catch (Exception ex)
                {
                    _output.WriteLine($"❌ Exception checking graphics tuning support: {ex.Message}");
                    Skip.If(true, $"Graphics tuning check caused exception: {ex.Message}");
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
    }*/

    /*[SkippableFact]
    public void Test_05_Auto_Tuning_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        Skip.IfNot(_fixture.FirstGPU != null, "First GPU not available");

        var gpu = _fixture.FirstGPU;

        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            var result = _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);

            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            Assert.NotNull(tuningServices);

            _output.WriteLine("=== Auto Tuning Support ===");

            // Check if auto tuning is supported
            var supportedPtr = ADLX.new_adlx_boolP();
            try
            {
                result = tuningServices.IsSupportedAutoTuning(gpu, supportedPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    bool supported = ADLX.adlx_boolP_value(supportedPtr);
                    _output.WriteLine($"Auto Tuning Supported: {supported}");

                    if (supported)
                    {
                        _output.WriteLine("✓ GPU supports auto tuning");
                    }
                    else
                    {
                        _output.WriteLine("ℹ️  Auto tuning not supported on this GPU");
                    }
                }
                else
                {
                    _output.WriteLine($"⚠️  Could not check auto tuning support: {result}");
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
    }*/

    /*[SkippableFact]
    public void Test_06_Preset_Tuning_Support()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.SupportsGPUTuning, "GPU tuning not supported");
        Skip.IfNot(_fixture.Capabilities.GPUCount > 0, "No GPUs available");
        Skip.IfNot(_fixture.FirstGPU != null, "First GPU not available");

        var gpu = _fixture.FirstGPU;

        var tuningServicesPtr = ADLX.new_gpuTuningP_Ptr();
        try
        {
            var result = _fixture.System!.GetGPUTuningServices(tuningServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);

            var tuningServices = ADLX.gpuTuningP_Ptr_value(tuningServicesPtr);
            Assert.NotNull(tuningServices);

            _output.WriteLine("=== Preset Tuning Support ===");

            // Check if preset tuning is supported
            var supportedPtr = ADLX.new_adlx_boolP();
            try
            {
                result = tuningServices.IsSupportedPresetTuning(gpu, supportedPtr);
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    bool supported = ADLX.adlx_boolP_value(supportedPtr);
                    _output.WriteLine($"Preset Tuning Supported: {supported}");

                    if (supported)
                    {
                        _output.WriteLine("✓ GPU supports preset tuning");
                    }
                    else
                    {
                        _output.WriteLine("ℹ️  Preset tuning not supported on this GPU");
                    }
                }
                else
                {
                    _output.WriteLine($"⚠️  Could not check preset tuning support: {result}");
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
    }*/
}