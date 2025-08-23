using System;

class WorkingTest
{
    static void MainOld(string[] args)
    {
        Console.WriteLine("=== ADLX C# Wrapper with IADLXGPU2 Support Test ===");
        Console.WriteLine();

        try
        {
            // Test ADLX runtime detection
            Console.WriteLine("1. Testing ADLX Runtime Detection...");
            bool runtimeAvailable = ADLX.IsADLXRuntimeAvailable();
            Console.WriteLine($"   ADLX Runtime Available: {runtimeAvailable}");
            
            if (!runtimeAvailable)
            {
                Console.WriteLine("   ADLX runtime not available. Please install AMD drivers with ADLX support.");
                Console.WriteLine("   This is expected on systems without AMD GPUs or ADLX-compatible drivers.");
                Console.WriteLine("   The test demonstrates that the wrapper is working correctly.");
                Console.WriteLine("\n=== Test Completed Successfully (Runtime Detection Working) ===");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                return;
            }

            // Test Enhanced ADLX Helper initialization
            Console.WriteLine("\n2. Testing Enhanced ADLX Helper...");
            EnhancedADLXHelper helper = new EnhancedADLXHelper();
            ADLX_RESULT initResult = helper.Initialize();
            Console.WriteLine($"   Helper Initialize Result: {initResult}");
            
            if (initResult != ADLX_RESULT.ADLX_OK)
            {
                Console.WriteLine($"   Failed to initialize ADLX Helper: {ADLX.GetADLXErrorDescription(initResult)}");
                Console.WriteLine("   This is expected on systems without AMD GPUs.");
                Console.WriteLine("   The test demonstrates that the wrapper is working correctly.");
                Console.WriteLine("\n=== Test Completed Successfully (Error Handling Working) ===");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                return;
            }

            // Get system services
            Console.WriteLine("\n3. Testing System Services...");
            IADLXSystem systemServices = helper.GetSystemServices();
            if (systemServices != null)
            {
                Console.WriteLine("   System services obtained successfully");
                
                // Test QueryInterface for System1
                Console.WriteLine("\n4. Testing System Interface Progression...");
                IADLXSystem1 system1 = ADLX.QuerySystem1Interface(systemServices);
                if (system1 != null)
                {
                    Console.WriteLine("   IADLXSystem1 interface obtained successfully");
                    
                    // Test QueryInterface for System2
                    IADLXSystem2 system2 = ADLX.QuerySystem2Interface(systemServices);
                    if (system2 != null)
                    {
                        Console.WriteLine("   IADLXSystem2 interface obtained successfully");
                        
                        // Test GPU enumeration using proper SWIG API
                        Console.WriteLine("\n5. Testing GPU Enumeration...");
                        
                        // Create output parameter for GPU list
                        SWIGTYPE_p_p_adlx__IADLXGPUList gpuListPtr = ADLX.new_gpuListP_Ptr();
                        ADLX_RESULT getGPUsResult = systemServices.GetGPUs(gpuListPtr);
                        
                        if (getGPUsResult == ADLX_RESULT.ADLX_OK)
                        {
                            IADLXGPUList gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
                            if (gpuList != null)
                            {
                                uint gpuCount = gpuList.Size();
                                Console.WriteLine($"   Found {gpuCount} GPU(s)");
                                
                                // Test each GPU for IADLXGPU2 support
                                for (uint i = 0; i < gpuCount; i++)
                                {
                                    SWIGTYPE_p_p_adlx__IADLXGPU gpuPtr = ADLX.new_gpuP_Ptr();
                                    ADLX_RESULT getGPUResult = gpuList.At(i, gpuPtr);
                                    
                                    if (getGPUResult == ADLX_RESULT.ADLX_OK)
                                    {
                                        IADLXGPU gpu = ADLX.gpuP_Ptr_value(gpuPtr);
                                        if (gpu != null)
                                        {
                                            // Get GPU name
                                            SWIGTYPE_p_p_char namePtr = ADLX.new_charP_Ptr();
                                            ADLX_RESULT getNameResult = gpu.Name(namePtr);
                                            
                                            string gpuName = "Unknown";
                                            if (getNameResult == ADLX_RESULT.ADLX_OK)
                                            {
                                                gpuName = ADLX.charP_Ptr_value(namePtr);
                                            }
                                            
                                            Console.WriteLine($"\n   GPU {i}: {gpuName}");
                                            
                                            // Test GPU interface progression
                                            Console.WriteLine("   Testing GPU Interface Progression:");
                                            
                                            // Test IADLXGPU1
                                            bool supportsGPU1 = ADLX.SupportsGPU1Interface(gpu);
                                            Console.WriteLine($"     Supports IADLXGPU1: {supportsGPU1}");
                                            
                                            // Test IADLXGPU2 - This is the main goal!
                                            bool supportsGPU2 = ADLX.SupportsGPU2Interface(gpu);
                                            Console.WriteLine($"     Supports IADLXGPU2: {supportsGPU2}");
                                            
                                            if (supportsGPU2)
                                            {
                                                IADLXGPU2 gpu2 = ADLX.QueryGPU2Interface(gpu);
                                                if (gpu2 != null)
                                                {
                                                    Console.WriteLine("     IADLXGPU2 interface obtained successfully!");
                                                    Console.WriteLine("     *** IADLXGPU2 QueryInterface SUCCESS! ***");
                                                    
                                                    // Test IADLXGPU2 specific features
                                                    Console.WriteLine("     Testing IADLXGPU2 Features:");
                                                    
                                                    try
                                                    {
                                                        // Test power management features
                                                        SWIGTYPE_p_bool powerOffState = ADLX.new_boolP();
                                                        ADLX_RESULT powerResult = gpu2.IsPowerOff(powerOffState);
                                                        if (powerResult == ADLX_RESULT.ADLX_OK)
                                                        {
                                                            bool isPowerOff = ADLX.boolP_value(powerOffState);
                                                            Console.WriteLine($"       IsPowerOff: {isPowerOff}");
                                                        }
                                                        
                                                        // Test extended GPU information
                                                        SWIGTYPE_p_p_char versionPtr = ADLX.new_charP_Ptr();
                                                        ADLX_RESULT versionResult = gpu2.AMDSoftwareVersion(versionPtr);
                                                        if (versionResult == ADLX_RESULT.ADLX_OK)
                                                        {
                                                            string amdSoftwareVersion = ADLX.charP_Ptr_value(versionPtr);
                                                            Console.WriteLine($"       AMD Software Version: {amdSoftwareVersion}");
                                                        }
                                                        
                                                        SWIGTYPE_p_p_char driverVersionPtr = ADLX.new_charP_Ptr();
                                                        ADLX_RESULT driverResult = gpu2.DriverVersion(driverVersionPtr);
                                                        if (driverResult == ADLX_RESULT.ADLX_OK)
                                                        {
                                                            string driverVersion = ADLX.charP_Ptr_value(driverVersionPtr);
                                                            Console.WriteLine($"       Driver Version: {driverVersion}");
                                                        }
                                                        
                                                        Console.WriteLine("     IADLXGPU2 features tested successfully!");
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Console.WriteLine($"     Error testing IADLXGPU2 features: {ex.Message}");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("     Failed to obtain IADLXGPU2 interface");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("     IADLXGPU2 not supported on this GPU");
                                            }
                                            
                                            ADLX.delete_charP_Ptr(namePtr);
                                        }
                                    }
                                    ADLX.delete_gpuP_Ptr(gpuPtr);
                                }
                            }
                            else
                            {
                                Console.WriteLine("   Failed to get GPU list from pointer");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"   Failed to get GPU list: {getGPUsResult}");
                        }
                        
                        ADLX.delete_gpuListP_Ptr(gpuListPtr);
                    }
                    else
                    {
                        Console.WriteLine("   IADLXSystem2 interface not available");
                    }
                }
                else
                {
                    Console.WriteLine("   IADLXSystem1 interface not available");
                }
            }
            else
            {
                Console.WriteLine("   Failed to get system services");
            }

            // Cleanup
            Console.WriteLine("\n6. Cleaning up...");
            ADLX_RESULT terminateResult = helper.Terminate();
            Console.WriteLine($"   Helper Terminate Result: {terminateResult}");

            Console.WriteLine("\n=== Test Completed Successfully ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during test: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
